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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
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
            menuStripMain = new MenuStrip();
            fileMenuItem = new ToolStripMenuItem();
            exitMenuItem = new ToolStripMenuItem();
            viewMenuItem = new ToolStripMenuItem();
            themeMenuItem = new ToolStripMenuItem();
            themeAutoMenuItem = new ToolStripMenuItem();
            themeLightMenuItem = new ToolStripMenuItem();
            themeDarkMenuItem = new ToolStripMenuItem();
            helpMenuItem = new ToolStripMenuItem();
            aboutMenuItem = new ToolStripMenuItem();
            tableLayoutPanel1.SuspendLayout();
            menuStripMain.SuspendLayout();
            SuspendLayout();
            // 
            // lblSource
            // 
            lblSource.AutoSize = true;
            lblSource.Location = new Point(11, 36);
            lblSource.Name = "lblSource";
            lblSource.Size = new Size(93, 15);
            lblSource.TabIndex = 0;
            lblSource.Text = "Source directory";
            // 
            // txtSource
            // 
            tableLayoutPanel1.SetColumnSpan(txtSource, 3);
            txtSource.Dock = DockStyle.Fill;
            txtSource.Location = new Point(11, 54);
            txtSource.Name = "txtSource";
            txtSource.Size = new Size(852, 23);
            txtSource.TabIndex = 1;
            // 
            // btnBrowseSource
            // 
            btnBrowseSource.Location = new Point(869, 54);
            btnBrowseSource.Name = "btnBrowseSource";
            btnBrowseSource.Size = new Size(75, 23);
            btnBrowseSource.TabIndex = 2;
            btnBrowseSource.Text = "Browse…";
            btnBrowseSource.UseVisualStyleBackColor = true;
            // 
            // lblDestination
            // 
            lblDestination.AutoSize = true;
            lblDestination.Location = new Point(11, 80);
            lblDestination.Name = "lblDestination";
            lblDestination.Size = new Size(172, 15);
            lblDestination.TabIndex = 3;
            lblDestination.Text = "Destination directory (optional)";
            // 
            // lblExtension
            // 
            lblExtension.AutoSize = true;
            lblExtension.Location = new Point(11, 124);
            lblExtension.Name = "lblExtension";
            lblExtension.Size = new Size(195, 15);
            lblExtension.TabIndex = 4;
            lblExtension.Text = "Extension filter (optional, e.g. .mp4)";
            // 
            // txtDestination
            // 
            tableLayoutPanel1.SetColumnSpan(txtDestination, 3);
            txtDestination.Dock = DockStyle.Fill;
            txtDestination.Location = new Point(11, 98);
            txtDestination.Name = "txtDestination";
            txtDestination.Size = new Size(852, 23);
            txtDestination.TabIndex = 5;
            // 
            // txtExtension
            // 
            txtExtension.Dock = DockStyle.Fill;
            txtExtension.Location = new Point(11, 142);
            txtExtension.Name = "txtExtension";
            txtExtension.Size = new Size(195, 23);
            txtExtension.TabIndex = 6;
            // 
            // txtLog
            // 
            tableLayoutPanel1.SetColumnSpan(txtLog, 4);
            txtLog.Dock = DockStyle.Fill;
            txtLog.Location = new Point(11, 310);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Both;
            txtLog.Size = new Size(933, 300);
            txtLog.TabIndex = 7;
            // 
            // btnBrowseDestination
            // 
            btnBrowseDestination.Location = new Point(869, 98);
            btnBrowseDestination.Name = "btnBrowseDestination";
            btnBrowseDestination.Size = new Size(75, 23);
            btnBrowseDestination.TabIndex = 8;
            btnBrowseDestination.Text = "Browse…";
            btnBrowseDestination.UseVisualStyleBackColor = true;
            // 
            // btnRun
            // 
            btnRun.Location = new Point(869, 266);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(75, 23);
            btnRun.TabIndex = 9;
            btnRun.Text = "Run";
            btnRun.UseVisualStyleBackColor = true;
            // 
            // chkDeleteOriginals
            // 
            chkDeleteOriginals.AutoSize = true;
            chkDeleteOriginals.Location = new Point(11, 171);
            chkDeleteOriginals.Name = "chkDeleteOriginals";
            chkDeleteOriginals.Size = new Size(163, 19);
            chkDeleteOriginals.TabIndex = 10;
            chkDeleteOriginals.Text = "Delete originals after copy";
            chkDeleteOriginals.UseVisualStyleBackColor = true;
            // 
            // chkNormalize
            // 
            chkNormalize.AutoSize = true;
            chkNormalize.Location = new Point(11, 196);
            chkNormalize.Name = "chkNormalize";
            chkNormalize.Size = new Size(153, 19);
            chkNormalize.TabIndex = 11;
            chkNormalize.Text = "Normalize group names";
            chkNormalize.UseVisualStyleBackColor = true;
            // 
            // chkDryRun
            // 
            chkDryRun.AutoSize = true;
            chkDryRun.Location = new Point(11, 221);
            chkDryRun.Name = "chkDryRun";
            chkDryRun.Size = new Size(160, 19);
            chkDryRun.TabIndex = 12;
            chkDryRun.Text = "Dry-run (simulation only)";
            chkDryRun.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(788, 266);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 13;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // progressBar
            // 
            progressBar.BackColor = SystemColors.Control;
            tableLayoutPanel1.SetColumnSpan(progressBar, 2);
            progressBar.Dock = DockStyle.Fill;
            progressBar.Location = new Point(11, 266);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(771, 23);
            progressBar.Step = 1;
            progressBar.TabIndex = 14;
            // 
            // logBox
            // 
            logBox.AutoSize = true;
            logBox.Location = new Point(11, 292);
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
            tableLayoutPanel1.Controls.Add(lblSource, 0, 1);
            tableLayoutPanel1.Controls.Add(txtExtension, 0, 6);
            tableLayoutPanel1.Controls.Add(txtSource, 0, 2);
            tableLayoutPanel1.Controls.Add(lblExtension, 0, 5);
            tableLayoutPanel1.Controls.Add(chkDeleteOriginals, 0, 7);
            tableLayoutPanel1.Controls.Add(lblDestination, 0, 3);
            tableLayoutPanel1.Controls.Add(txtDestination, 0, 4);
            tableLayoutPanel1.Controls.Add(txtLog, 0, 13);
            tableLayoutPanel1.Controls.Add(logBox, 0, 12);
            tableLayoutPanel1.Controls.Add(progressBar, 0, 11);
            tableLayoutPanel1.Controls.Add(chkDryRun, 0, 9);
            tableLayoutPanel1.Controls.Add(chkNormalize, 0, 8);
            tableLayoutPanel1.Controls.Add(btnRun, 3, 11);
            tableLayoutPanel1.Controls.Add(btnBrowseDestination, 3, 4);
            tableLayoutPanel1.Controls.Add(btnBrowseSource, 3, 2);
            tableLayoutPanel1.Controls.Add(btnCancel, 2, 11);
            tableLayoutPanel1.Controls.Add(statusLabel, 0, 10);
            tableLayoutPanel1.Controls.Add(menuStripMain, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Padding = new Padding(8);
            tableLayoutPanel1.RowCount = 14;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
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
            statusLabel.Location = new Point(11, 243);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(42, 15);
            statusLabel.TabIndex = 16;
            statusLabel.Text = "Status:";
            // 
            // menuStripMain
            // 
            tableLayoutPanel1.SetColumnSpan(menuStripMain, 4);
            menuStripMain.Dock = DockStyle.Fill;
            menuStripMain.Items.AddRange(new ToolStripItem[] { fileMenuItem, viewMenuItem, helpMenuItem });
            menuStripMain.Location = new Point(8, 8);
            menuStripMain.Margin = new Padding(0, 0, 0, 4);
            menuStripMain.Name = "menuStripMain";
            menuStripMain.Padding = new Padding(0);
            menuStripMain.Size = new Size(939, 24);
            menuStripMain.TabIndex = 19;
            menuStripMain.Text = "menuStrip1";
            // 
            // fileMenuItem
            // 
            fileMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exitMenuItem });
            fileMenuItem.Name = "fileMenuItem";
            fileMenuItem.Size = new Size(37, 24);
            fileMenuItem.Text = "File";
            // 
            // exitMenuItem
            // 
            exitMenuItem.Name = "exitMenuItem";
            exitMenuItem.Size = new Size(180, 22);
            exitMenuItem.Text = "Exit";
            // 
            // viewMenuItem
            // 
            viewMenuItem.DropDownItems.AddRange(new ToolStripItem[] { themeMenuItem });
            viewMenuItem.Name = "viewMenuItem";
            viewMenuItem.Size = new Size(44, 24);
            viewMenuItem.Text = "View";
            // 
            // themeMenuItem
            // 
            themeMenuItem.DropDownItems.AddRange(new ToolStripItem[] { themeAutoMenuItem, themeLightMenuItem, themeDarkMenuItem });
            themeMenuItem.Name = "themeMenuItem";
            themeMenuItem.Size = new Size(110, 22);
            themeMenuItem.Text = "Theme";
            // 
            // themeAutoMenuItem
            // 
            themeAutoMenuItem.Checked = true;
            themeAutoMenuItem.CheckOnClick = true;
            themeAutoMenuItem.CheckState = CheckState.Checked;
            themeAutoMenuItem.Name = "themeAutoMenuItem";
            themeAutoMenuItem.Size = new Size(101, 22);
            themeAutoMenuItem.Text = "Auto";
            // 
            // themeLightMenuItem
            // 
            themeLightMenuItem.CheckOnClick = true;
            themeLightMenuItem.Name = "themeLightMenuItem";
            themeLightMenuItem.Size = new Size(101, 22);
            themeLightMenuItem.Text = "Light";
            // 
            // themeDarkMenuItem
            // 
            themeDarkMenuItem.CheckOnClick = true;
            themeDarkMenuItem.Name = "themeDarkMenuItem";
            themeDarkMenuItem.Size = new Size(101, 22);
            themeDarkMenuItem.Text = "Dark";
            // 
            // helpMenuItem
            // 
            helpMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutMenuItem });
            helpMenuItem.Name = "helpMenuItem";
            helpMenuItem.Size = new Size(44, 24);
            helpMenuItem.Text = "Help";
            // 
            // aboutMenuItem
            // 
            aboutMenuItem.Name = "aboutMenuItem";
            aboutMenuItem.Size = new Size(107, 22);
            aboutMenuItem.Text = "About";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(955, 621);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStripMain;
            Name = "MainForm";
            Text = "Auto Folder";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            menuStripMain.ResumeLayout(false);
            menuStripMain.PerformLayout();
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
        private MenuStrip menuStripMain;
        private ToolStripMenuItem fileMenuItem;
        private ToolStripMenuItem exitMenuItem;
        private ToolStripMenuItem viewMenuItem;
        private ToolStripMenuItem themeMenuItem;
        private ToolStripMenuItem themeAutoMenuItem;
        private ToolStripMenuItem themeLightMenuItem;
        private ToolStripMenuItem themeDarkMenuItem;
        private ToolStripMenuItem helpMenuItem;
        private ToolStripMenuItem aboutMenuItem;
    }
}
