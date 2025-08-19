using AutoFolder.Core;
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

        public MainForm()
        {
            InitializeComponent();

            // Initial UI state
            Text = "AutoFolder";
            btnCancel.Enabled = false; // We'll enable cancel when we add CancellationToken later.

            // Wire up basic events
                btnBrowseSource.Click += (_, __) => BrowseFolder(txtSource);
                btnBrowseDestination.Click += (_, __) => BrowseFolder(txtDestination);
            btnRun.Click += async (_, __) => await RunAsync();
            btnCancel.Click += (_, __) => MessageBox.Show("Cancel not implemented yet.", "Info");
        }

        /// <summary>
        /// Opens a folder picker and assigns the selected path to the given TextBox.
        /// This centralizes folder selection logic (DRY principle).
        /// </summary>
        private void BrowseFolder(WinFormsTextBox target)
        {
            using var dlg = new FolderBrowserDialog
            {
                Description = "Select a folder",
                UseDescriptionForTitle = true
            };

            // If the current text is a valid directory, start there for convenience.
            if (!string.IsNullOrWhiteSpace(target.Text) && Directory.Exists(target.Text))
                dlg.SelectedPath = target.Text;

            if (dlg.ShowDialog(this) == DialogResult.OK)
                target.Text = dlg.SelectedPath;
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
                MessageBox.Show(this, "Please select a valid source directory.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Destination directory is optional, but if provided must exist (we could also create it automatically later)
            if (!string.IsNullOrWhiteSpace(txtDestination.Text) && !Directory.Exists(txtDestination.Text))
            {
                MessageBox.Show(this, "Destination directory doesn't exist. Please choose an existing folder (or leave it empty).", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Extension is optional; if provided, normalize to start with dot (".pdf" not "pdf")
            if (!string.IsNullOrWhiteSpace(txtExtension.Text))
            {
                var ext = txtExtension.Text.Trim();
                if (!ext.StartsWith(".")) ext = "." + ext;
                txtExtension.Text = ext;
            }

            return true;
        }

        /// <summary>
        /// Small helper to disable inputs while the tool is running.
        /// Good UX: prevents mid-run parameter changes.
        /// </summary>
        private void SetBusy(bool isBusy)
        {
            btnRun.Enabled = !isBusy;
            btnCancel.Enabled = false; // not implemented yet

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
            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
        }

        /// <summary>
        /// 1) Validates inputs
        /// 2) Reads parameters from the UI
        /// 3) Prepares the UI for a long-running operation (disable inputs)
        /// 4) The actual Core call virá no próximo passo (mantemos o método async já pronto)
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
            // progressBar.Value = 0; // We'll wire real progress later
            SetBusy(true);

            try
            {
                // For now, just simulate a quick check to keep things responsive
                Log("Ready to organize files. (Core call will be added next step)");
                await Task.Delay(300);
            }
            catch (Exception ex)
            {
                // Catch-all so the UI never crashes unexpectedly
                MessageBox.Show(this, ex.Message, "Unexpected error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log($"ERROR: {ex}");
            }
            finally
            {
                SetBusy(false);
            }
        }
    }
}
