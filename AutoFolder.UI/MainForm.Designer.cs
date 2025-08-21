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
            SuspendLayout();
            // 
            // lblSource
            // 
            lblSource.AutoSize = true;
            lblSource.Location = new Point(12, 9);
            lblSource.Name = "lblSource";
            lblSource.Size = new Size(93, 15);
            lblSource.TabIndex = 0;
            lblSource.Text = "Source directory";
            // 
            // txtSource
            // 
            txtSource.Location = new Point(12, 27);
            txtSource.Name = "txtSource";
            txtSource.Size = new Size(695, 23);
            txtSource.TabIndex = 1;
            // 
            // btnBrowseSource
            // 
            btnBrowseSource.Location = new Point(713, 26);
            btnBrowseSource.Name = "btnBrowseSource";
            btnBrowseSource.Size = new Size(75, 23);
            btnBrowseSource.TabIndex = 2;
            btnBrowseSource.Text = "Browse…";
            btnBrowseSource.UseVisualStyleBackColor = true;
            // 
            // lblDestination
            // 
            lblDestination.AutoSize = true;
            lblDestination.Location = new Point(12, 64);
            lblDestination.Name = "lblDestination";
            lblDestination.Size = new Size(172, 15);
            lblDestination.TabIndex = 3;
            lblDestination.Text = "Destination directory (optional)";
            // 
            // lblExtension
            // 
            lblExtension.AutoSize = true;
            lblExtension.Location = new Point(12, 126);
            lblExtension.Name = "lblExtension";
            lblExtension.Size = new Size(195, 15);
            lblExtension.TabIndex = 4;
            lblExtension.Text = "Extension filter (optional, e.g. .mp4)";
            // 
            // txtDestination
            // 
            txtDestination.Location = new Point(12, 82);
            txtDestination.Name = "txtDestination";
            txtDestination.Size = new Size(695, 23);
            txtDestination.TabIndex = 5;
            // 
            // txtExtension
            // 
            txtExtension.Location = new Point(12, 144);
            txtExtension.Name = "txtExtension";
            txtExtension.Size = new Size(100, 23);
            txtExtension.TabIndex = 6;
            // 
            // txtLog
            // 
            txtLog.Dock = DockStyle.Bottom;
            txtLog.Location = new Point(0, 427);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Both;
            txtLog.Size = new Size(800, 23);
            txtLog.TabIndex = 7;
            // 
            // btnBrowseDestination
            // 
            btnBrowseDestination.Location = new Point(713, 82);
            btnBrowseDestination.Name = "btnBrowseDestination";
            btnBrowseDestination.Size = new Size(75, 23);
            btnBrowseDestination.TabIndex = 8;
            btnBrowseDestination.Text = "Browse…";
            btnBrowseDestination.UseVisualStyleBackColor = true;
            // 
            // btnRun
            // 
            btnRun.Location = new Point(713, 369);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(75, 23);
            btnRun.TabIndex = 9;
            btnRun.Text = "Run";
            btnRun.UseVisualStyleBackColor = true;
            // 
            // chkDeleteOriginals
            // 
            chkDeleteOriginals.AutoSize = true;
            chkDeleteOriginals.Location = new Point(12, 182);
            chkDeleteOriginals.Name = "chkDeleteOriginals";
            chkDeleteOriginals.Size = new Size(163, 19);
            chkDeleteOriginals.TabIndex = 10;
            chkDeleteOriginals.Text = "Delete originals after copy";
            chkDeleteOriginals.UseVisualStyleBackColor = true;
            // 
            // chkNormalize
            // 
            chkNormalize.AutoSize = true;
            chkNormalize.Location = new Point(12, 207);
            chkNormalize.Name = "chkNormalize";
            chkNormalize.Size = new Size(153, 19);
            chkNormalize.TabIndex = 11;
            chkNormalize.Text = "Normalize group names";
            chkNormalize.UseVisualStyleBackColor = true;
            // 
            // chkDryRun
            // 
            chkDryRun.AutoSize = true;
            chkDryRun.Location = new Point(12, 232);
            chkDryRun.Name = "chkDryRun";
            chkDryRun.Size = new Size(160, 19);
            chkDryRun.TabIndex = 12;
            chkDryRun.Text = "Dry-run (simulation only)";
            chkDryRun.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(632, 369);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 13;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(12, 398);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(776, 23);
            progressBar.Step = 1;
            progressBar.TabIndex = 14;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(progressBar);
            Controls.Add(btnCancel);
            Controls.Add(chkDryRun);
            Controls.Add(chkNormalize);
            Controls.Add(chkDeleteOriginals);
            Controls.Add(btnRun);
            Controls.Add(btnBrowseDestination);
            Controls.Add(txtLog);
            Controls.Add(txtExtension);
            Controls.Add(txtDestination);
            Controls.Add(lblExtension);
            Controls.Add(lblDestination);
            Controls.Add(btnBrowseSource);
            Controls.Add(txtSource);
            Controls.Add(lblSource);
            Name = "MainForm";
            Text = "Auto Folder";
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
    }
}
