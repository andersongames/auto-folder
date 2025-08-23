namespace AutoFolder.UI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblSource = new Label();
            txtSource = new TextBox();
            btnBrowseSource = new Button();
            lblDestination = new Label();
            lblExtension = new Label();
            txtDestination = new TextBox();
            txtExtension = new TextBox();
            txtLog = new TextBox();
            btnBrowseDestination = new Button();
            btnRun = new Button();
            chkDeleteOriginals = new CheckBox();
            chkNormalize = new CheckBox();
            chkDryRun = new CheckBox();
            btnCancel = new Button();
            progressBar = new ProgressBar();
            logBox = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            statusLabel = new Label();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblSource
            // 
            lblSource.AutoSize = true;
            lblSource.Location = new Point(11, 8);
            lblSource.Name = "lblSource";
            lblSource.Size = new Size(93, 15);
            lblSource.TabIndex = 0;
            lblSource.Text = "Source directory";
            // 
            // txtSource
            // 
            tableLayoutPanel1.SetColumnSpan(txtSource, 3);
            txtSource.Dock = DockStyle.Fill;
            txtSource.Location = new Point(11, 26);
            txtSource.Name = "txtSource";
            txtSource.Size = new Size(852, 23);
            txtSource.TabIndex = 1;
            // 
            // btnBrowseSource
            // 
            btnBrowseSource.Location = new Point(869, 26);
            btnBrowseSource.Name = "btnBrowseSource";
            btnBrowseSource.Size = new Size(75, 23);
            btnBrowseSource.TabIndex = 2;
            btnBrowseSource.Text = "Browse…";
            btnBrowseSource.UseVisualStyleBackColor = true;
            // 
            // lblDestination
            // 
            lblDestination.AutoSize = true;
            lblDestination.Location = new Point(11, 52);
            lblDestination.Name = "lblDestination";
            lblDestination.Size = new Size(172, 15);
            lblDestination.TabIndex = 3;
            lblDestination.Text = "Destination directory (optional)";
            // 
            // lblExtension
            // 
            lblExtension.AutoSize = true;
            lblExtension.Location = new Point(11, 96);
            lblExtension.Name = "lblExtension";
            lblExtension.Size = new Size(195, 15);
            lblExtension.TabIndex = 4;
            lblExtension.Text = "Extension filter (optional, e.g. .mp4)";
            // 
            // txtDestination
            // 
            tableLayoutPanel1.SetColumnSpan(txtDestination, 3);
            txtDestination.Dock = DockStyle.Fill;
            txtDestination.Location = new Point(11, 70);
            txtDestination.Name = "txtDestination";
            txtDestination.Size = new Size(852, 23);
            txtDestination.TabIndex = 5;
            // 
            // txtExtension
            // 
            txtExtension.Dock = DockStyle.Fill;
            txtExtension.Location = new Point(11, 114);
            txtExtension.Name = "txtExtension";
            txtExtension.Size = new Size(195, 23);
            txtExtension.TabIndex = 6;
            // 
            // txtLog
            // 
            tableLayoutPanel1.SetColumnSpan(txtLog, 4);
            txtLog.Dock = DockStyle.Fill;
            txtLog.Location = new Point(11, 282);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Both;
            txtLog.Size = new Size(933, 328);
            txtLog.TabIndex = 7;
            // 
            // btnBrowseDestination
            // 
            btnBrowseDestination.Location = new Point(869, 70);
            btnBrowseDestination.Name = "btnBrowseDestination";
            btnBrowseDestination.Size = new Size(75, 23);
            btnBrowseDestination.TabIndex = 8;
            btnBrowseDestination.Text = "Browse…";
            btnBrowseDestination.UseVisualStyleBackColor = true;
            // 
            // btnRun
            // 
            btnRun.Location = new Point(869, 238);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(75, 23);
            btnRun.TabIndex = 9;
            btnRun.Text = "Run";
            btnRun.UseVisualStyleBackColor = true;
            // 
            // chkDeleteOriginals
            // 
            chkDeleteOriginals.AutoSize = true;
            chkDeleteOriginals.Location = new Point(11, 143);
            chkDeleteOriginals.Name = "chkDeleteOriginals";
            chkDeleteOriginals.Size = new Size(163, 19);
            chkDeleteOriginals.TabIndex = 10;
            chkDeleteOriginals.Text = "Delete originals after copy";
            chkDeleteOriginals.UseVisualStyleBackColor = true;
            // 
            // chkNormalize
            // 
            chkNormalize.AutoSize = true;
            chkNormalize.Location = new Point(11, 168);
            chkNormalize.Name = "chkNormalize";
            chkNormalize.Size = new Size(153, 19);
            chkNormalize.TabIndex = 11;
            chkNormalize.Text = "Normalize group names";
            chkNormalize.UseVisualStyleBackColor = true;
            // 
            // chkDryRun
            // 
            chkDryRun.AutoSize = true;
            chkDryRun.Location = new Point(11, 193);
            chkDryRun.Name = "chkDryRun";
            chkDryRun.Size = new Size(160, 19);
            chkDryRun.TabIndex = 12;
            chkDryRun.Text = "Dry-run (simulation only)";
            chkDryRun.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(788, 238);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 13;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // progressBar
            // 
            tableLayoutPanel1.SetColumnSpan(progressBar, 2);
            progressBar.Dock = DockStyle.Fill;
            progressBar.Location = new Point(11, 238);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(771, 23);
            progressBar.Step = 1;
            progressBar.TabIndex = 14;
            // 
            // logBox
            // 
            logBox.AutoSize = true;
            logBox.Location = new Point(11, 264);
            logBox.Name = "logBox";
            logBox.Size = new Size(27, 15);
            logBox.TabIndex = 15;
            logBox.Text = "Log";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(lblSource, 0, 0);
            tableLayoutPanel1.Controls.Add(txtExtension, 0, 5);
            tableLayoutPanel1.Controls.Add(txtSource, 0, 1);
            tableLayoutPanel1.Controls.Add(lblExtension, 0, 4);
            tableLayoutPanel1.Controls.Add(chkDeleteOriginals, 0, 6);
            tableLayoutPanel1.Controls.Add(lblDestination, 0, 2);
            tableLayoutPanel1.Controls.Add(txtDestination, 0, 3);
            tableLayoutPanel1.Controls.Add(txtLog, 0, 12);
            tableLayoutPanel1.Controls.Add(logBox, 0, 11);
            tableLayoutPanel1.Controls.Add(progressBar, 0, 10);
            tableLayoutPanel1.Controls.Add(chkDryRun, 0, 8);
            tableLayoutPanel1.Controls.Add(chkNormalize, 0, 7);
            tableLayoutPanel1.Controls.Add(btnRun, 3, 10);
            tableLayoutPanel1.Controls.Add(btnBrowseDestination, 3, 3);
            tableLayoutPanel1.Controls.Add(btnBrowseSource, 3, 1);
            tableLayoutPanel1.Controls.Add(btnCancel, 2, 10);
            tableLayoutPanel1.Controls.Add(statusLabel, 0, 9);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Padding = new Padding(8);
            tableLayoutPanel1.RowCount = 13;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(955, 621);
            tableLayoutPanel1.TabIndex = 16;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(11, 215);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(42, 15);
            statusLabel.TabIndex = 16;
            statusLabel.Text = "Status:";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(955, 621);
            Controls.Add(tableLayoutPanel1);
            Name = "MainForm";
            Text = "Auto Folder";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblSource;
        private TextBox txtSource;
        private Button btnBrowseSource;
        private Label lblDestination;
        private Label lblExtension;
        private TextBox txtDestination;
        private TextBox txtExtension;
        private TextBox txtLog;
        private Button btnBrowseDestination;
        private Button btnRun;
        private CheckBox chkDeleteOriginals;
        private CheckBox chkNormalize;
        private CheckBox chkDryRun;
        private Button btnCancel;
        private ProgressBar progressBar;
        private Label logBox;
        private TableLayoutPanel tableLayoutPanel1;
        private Label statusLabel;
    }
}
