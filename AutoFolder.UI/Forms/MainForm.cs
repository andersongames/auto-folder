using AutoFolder.Core;
using AutoFolder.UI.Helpers;
using AutoFolder.UI.Infrastructure;
using AutoFolder.UI.Resources;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
// Alias created to disambiguate between two different "TextBox" classes in .NET:
// - System.Windows.Forms.TextBox: the WinForms input control we actually want to use
// - System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox: a helper for styling, not relevant here
// By using the alias "WinFormsTextBox", we make the code more explicit and avoid ambiguous reference errors.
using WinFormsTextBox = System.Windows.Forms.TextBox;

namespace AutoFolder.UI
{
    public partial class MainForm : Form
    {
        // Keep a single instance; FileOrganizer is stateless and can be reused.
        private readonly FileOrganizer _organizer = new();

        // This field will be responsible for receiving progress updates
        private Progress<ProgressInfo> _progressReporter;

        // Cancellation Token
        private CancellationTokenSource? _cts;

        public MainForm()
        {
            InitializeComponent();

            // Initial UI state
            Text = "AutoFolder";
            btnCancel.Enabled = false; // We'll enable cancel when we add CancellationToken later.
            ThemeHelper.ApplyTheme(this, ThemeHelper.ThemeMode.Auto);
            cmbTheme.Items.AddRange(["Auto", "Light", "Dark"]);
            cmbTheme.SelectedIndex = 0;
            cmbTheme.SelectedIndexChanged += (_, __) =>
            {
                var mode = (ThemeHelper.ThemeMode)cmbTheme.SelectedIndex;
                ThemeHelper.ApplyTheme(this, mode);
            };

            // Wire up basic events
            btnBrowseSource.Click += (_, __) => BrowseFolder(txtSource);
            btnBrowseDestination.Click += (_, __) => BrowseFolder(txtDestination);
            btnRun.Click += async (_, __) => await RunAsync();
            btnCancel.Click += (_, __) => _cts?.Cancel();

            // Initialize progress reporter
            _progressReporter = new Progress<ProgressInfo>(OnProgressReported);
        }

        /// <summary>
        /// Opens a folder picker and assigns the selected path to the given TextBox.
        /// This centralizes folder selection logic (DRY principle).
        /// </summary>
        private void BrowseFolder(WinFormsTextBox target)
        {
            using var dlg = new FolderBrowserDialog
            {
                Description = UiMessages.DirectorySelection,
                UseDescriptionForTitle = true
            };

            // If the current text is a valid directory, start there for convenience.
            if (!string.IsNullOrWhiteSpace(target.Text) && Directory.Exists(target.Text))
                dlg.SelectedPath = target.Text;

            if (dlg.ShowDialog(this) == DialogResult.OK)
                target.Text = dlg.SelectedPath;

            // Auto-fill the destination text box
            if (string.IsNullOrWhiteSpace(txtDestination.Text))
                txtDestination.Text = dlg.SelectedPath;
        }

        /// <summary>
        /// Validates user input before running. Shows a friendly message if something is wrong.
        /// Keep validations small and focused here; complex rules can be moved to a validator class later.
        /// </summary>
        private bool ValidateInputs()
        {
            // Source directory is mandatory
            if (string.IsNullOrWhiteSpace(txtSource.Text) || !Directory.Exists(txtSource.Text))
            {
                ErrorDialog.ShowWarning(this, UiMessages.InvalidSource);
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtDestination.Text))
            {
                string dest = txtDestination.Text.Trim();

                // Destination directory: Requires absolute path
                if (!Path.IsPathRooted(dest))
                {
                    ErrorDialog.ShowWarning(this, UiMessages.InvalidDestination);
                    return false;
                }

                if (!Directory.Exists(dest))
                {
                    if (!FileOrganizer.IsPathSyntacticallyValid(dest))
                    {
                        // Destination directory: If it does not exist, it must be syntactically valid
                        ErrorDialog.ShowWarning(this, UiMessages.InvalidDestination);
                        return false;
                    }

                    if (!chkDryRun.Checked)
                    {
                        // Destination directory: Ask for create the new  directory
                        DialogResult result = MessageBox.Show(
                            this,
                            $"{UiMessages.DestinationNotFound}\n{Path.GetFullPath(dest)} ",
                            UiMessages.DirectoryNotFound,
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Question
                        );

                        if (result == DialogResult.Cancel)
                            return false;
                    }
                }

                // Extra protection: prevents the destination directory from being the program directory
                string exeDir = AppDomain.CurrentDomain.BaseDirectory;
                if (string.Equals(Path.GetFullPath(dest), Path.GetFullPath(exeDir), StringComparison.OrdinalIgnoreCase))
                {
                    ErrorDialog.ShowWarning(this, UiMessages.InvalidDestination);
                    return false;
                }
            }

            // Extension is optional; if provided, normalize to start with dot (".pdf" not "pdf")
            if (!string.IsNullOrWhiteSpace(txtExtension.Text))
                txtExtension.Text = FileOrganizer.NormalizeFileExtension(txtExtension.Text);

            return true;
        }

        /// <summary>
        /// Small helper to disable inputs while the tool is running.
        /// Good UX: prevents mid-run parameter changes.
        /// </summary>
        private void SetBusy(bool isBusy)
        {
            btnRun.Enabled = !isBusy;
            btnCancel.Enabled = isBusy;
            cmbTheme.Enabled = !isBusy;
            txtSource.Enabled = !isBusy;
            btnBrowseSource.Enabled = !isBusy;
            txtDestination.Enabled = !isBusy;
            btnBrowseDestination.Enabled = !isBusy;
            txtExtension.Enabled = !isBusy;
            chkDeleteOriginals.Enabled = !isBusy;
            chkNormalize.Enabled = !isBusy;
            chkDryRun.Enabled = !isBusy;
        }

        /// <summary>
        /// Appends a timestamped line to the log TextBox (UI thread only).
        /// This is intentionally simple; later we can pipe Core logs into here as well.
        /// </summary>
        private void Log(string message)
        {
            if (txtLog.InvokeRequired)
            {
                // If the call is from a different thread, invoke the method on the UI thread
                txtLog.Invoke(new Action(() =>
                {
                    txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
                }));
            }
            else
            {
                // If already on the UI thread, directly access the control
                txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
            }
        }

        /// <summary>
        /// 1) Validates inputs
        /// 2) Reads parameters from the UI
        /// 3) Prepares the UI for a long-running operation (disable inputs)
        /// 4) The actual Core call
        /// </summary>
        private async Task RunAsync()
        {
            if (!ValidateInputs())
                return;

            // Read parameters
            string source = txtSource.Text.Trim();
            string? dest = string.IsNullOrWhiteSpace(txtDestination.Text) ? null : txtDestination.Text.Trim();
            string? ext = string.IsNullOrWhiteSpace(txtExtension.Text) ? null : txtExtension.Text.Trim();
            bool deleteOriginals = chkDeleteOriginals.Checked;
            bool normalize = chkNormalize.Checked;
            bool dryRun = chkDryRun.Checked;

            // Prepare UI
            txtLog.Clear();
            progressBar.Value = 0;
            statusLabel.Text = "Status:";
            SetBusy(true);

            _cts = new CancellationTokenSource();

            try
            {
                Log(UiMessages.Starting);
                // Offload to background thread so the UI stays responsive.
                await Task.Run(() =>
                {
                    _organizer.Organize(
                          sourceDirectory: source,
                          destinationDirectory: dest,          // null => use source inside Core
                          extensionFilter: ext,                // null => process all files
                          deleteOriginals: deleteOriginals,
                          normalizeGroupNames: normalize,
                          dryRun: dryRun,
                          progress: _progressReporter,
                          log: Log,
                          cancellationToken: _cts.Token
                      );
                });

                MessageBox.Show(this, dryRun ? UiMessages.SimulationCompleted : UiMessages.OrganizationCompleted, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OperationCanceledException)
            {
                Log(UiMessages.OperationCanceled);
                MessageBox.Show(this, UiMessages.OperationCanceled, "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Catch-all so the UI never crashes unexpectedly
                ErrorDialog.ShowCritical(this, ex.Message);
                Log($"ERROR: {ex}");
            }
            finally
            {
                SetBusy(false);

                // Restore progress bar and status label
                progressBar.Value = 0;
                statusLabel.Text = UiMessages.Status;
            }
        }

        /// <summary>
        /// Callback executed whenever FileOrganizer reports progress.
        /// This method runs on the UI thread because Progress<T> automatically marshals calls.
        /// </summary>
        /// <param name="info">Progress information (processed count, total, current file, stage)</param>
        private void OnProgressReported(ProgressInfo info)
        {
            if (info.Total > 0)
            {
                // Calculate percentage
                int percent = (int)(info.Processed / (double)info.Total * 100);

                // Clamp to [0, 100] to avoid overflow
                percent = Math.Max(0, Math.Min(100, percent));

                progressBar.Value = percent;
            }

            string stage = info.Stage == "copy" || info.Stage == "dry-run-copy" ? UiMessages.CopyStage : UiMessages.DeleteStage;

            // Update status label
            statusLabel.Text = $"{UiMessages.Status} [{info.Processed}/{info.Total}] / {stage} {Path.GetFileName(info.CurrentFile)}";
        }
    }
}
