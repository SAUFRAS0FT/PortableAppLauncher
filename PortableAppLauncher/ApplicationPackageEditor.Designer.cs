namespace PortableAppLauncher
{
    partial class ApplicationPackageEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.TB_ID = new System.Windows.Forms.TextBox();
            this.TB_Name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_IconLocation = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_PathLocation = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TB_ExeLocation = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.CB_RunAsAdmin = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.NUM_DisplayIndex = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.LB_Tags = new System.Windows.Forms.ListBox();
            this.TagsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.supprimerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BTN_Save = new System.Windows.Forms.Button();
            this.PB_AppIcon = new System.Windows.Forms.PictureBox();
            this.BTN_ChangeIcon = new System.Windows.Forms.Button();
            this.BrowseImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.BTN_ExtractIcon = new System.Windows.Forms.Button();
            this.panelControls = new System.Windows.Forms.Panel();
            this.BTN_DetailSwitch = new System.Windows.Forms.Button();
            this.BrowseExeDialog = new System.Windows.Forms.OpenFileDialog();
            this.BTN_BrowseIcon = new System.Windows.Forms.Button();
            this.BTN_BrowsePathLocation = new System.Windows.Forms.Button();
            this.BTN_BrowseExe = new System.Windows.Forms.Button();
            this.TB_Tag_NewTag = new System.Windows.Forms.TextBox();
            this.BTN_Tags_Add = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NUM_DisplayIndex)).BeginInit();
            this.TagsContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PB_AppIcon)).BeginInit();
            this.panelControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 236);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Application id = ";
            // 
            // TB_ID
            // 
            this.TB_ID.Location = new System.Drawing.Point(146, 233);
            this.TB_ID.Name = "TB_ID";
            this.TB_ID.Size = new System.Drawing.Size(194, 23);
            this.TB_ID.TabIndex = 1;
            // 
            // TB_Name
            // 
            this.TB_Name.Location = new System.Drawing.Point(280, 9);
            this.TB_Name.Name = "TB_Name";
            this.TB_Name.Size = new System.Drawing.Size(290, 23);
            this.TB_Name.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(146, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Application name = ";
            // 
            // TB_IconLocation
            // 
            this.TB_IconLocation.Location = new System.Drawing.Point(146, 262);
            this.TB_IconLocation.Name = "TB_IconLocation";
            this.TB_IconLocation.Size = new System.Drawing.Size(424, 23);
            this.TB_IconLocation.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 265);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Icon Location = ";
            // 
            // TB_PathLocation
            // 
            this.TB_PathLocation.Location = new System.Drawing.Point(146, 291);
            this.TB_PathLocation.Name = "TB_PathLocation";
            this.TB_PathLocation.Size = new System.Drawing.Size(424, 23);
            this.TB_PathLocation.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 294);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Path Location = ";
            // 
            // TB_ExeLocation
            // 
            this.TB_ExeLocation.Location = new System.Drawing.Point(146, 320);
            this.TB_ExeLocation.Name = "TB_ExeLocation";
            this.TB_ExeLocation.Size = new System.Drawing.Size(424, 23);
            this.TB_ExeLocation.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 323);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "Exe Location = ";
            // 
            // CB_RunAsAdmin
            // 
            this.CB_RunAsAdmin.AutoSize = true;
            this.CB_RunAsAdmin.Location = new System.Drawing.Point(280, 38);
            this.CB_RunAsAdmin.Name = "CB_RunAsAdmin";
            this.CB_RunAsAdmin.Size = new System.Drawing.Size(100, 19);
            this.CB_RunAsAdmin.TabIndex = 10;
            this.CB_RunAsAdmin.Text = "Run as Admin";
            this.CB_RunAsAdmin.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(146, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 15);
            this.label6.TabIndex = 11;
            this.label6.Text = "Display Index = ";
            // 
            // NUM_DisplayIndex
            // 
            this.NUM_DisplayIndex.Location = new System.Drawing.Point(280, 63);
            this.NUM_DisplayIndex.Name = "NUM_DisplayIndex";
            this.NUM_DisplayIndex.Size = new System.Drawing.Size(100, 23);
            this.NUM_DisplayIndex.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(146, 99);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 15);
            this.label8.TabIndex = 15;
            this.label8.Text = "Search Tags = ";
            // 
            // LB_Tags
            // 
            this.LB_Tags.ContextMenuStrip = this.TagsContextMenuStrip;
            this.LB_Tags.FormattingEnabled = true;
            this.LB_Tags.ItemHeight = 15;
            this.LB_Tags.Location = new System.Drawing.Point(280, 99);
            this.LB_Tags.Name = "LB_Tags";
            this.LB_Tags.Size = new System.Drawing.Size(290, 64);
            this.LB_Tags.TabIndex = 17;
            // 
            // TagsContextMenuStrip
            // 
            this.TagsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.supprimerToolStripMenuItem});
            this.TagsContextMenuStrip.Name = "TagsContextMenuStrip";
            this.TagsContextMenuStrip.Size = new System.Drawing.Size(130, 26);
            // 
            // supprimerToolStripMenuItem
            // 
            this.supprimerToolStripMenuItem.Name = "supprimerToolStripMenuItem";
            this.supprimerToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.supprimerToolStripMenuItem.Text = "Supprimer";
            this.supprimerToolStripMenuItem.Click += new System.EventHandler(this.supprimerToolStripMenuItem_Click);
            // 
            // BTN_Save
            // 
            this.BTN_Save.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_Save.Location = new System.Drawing.Point(495, 4);
            this.BTN_Save.Name = "BTN_Save";
            this.BTN_Save.Size = new System.Drawing.Size(75, 23);
            this.BTN_Save.TabIndex = 18;
            this.BTN_Save.Text = "Save";
            this.BTN_Save.UseVisualStyleBackColor = true;
            this.BTN_Save.Click += new System.EventHandler(this.BTN_Save_Click);
            // 
            // PB_AppIcon
            // 
            this.PB_AppIcon.Location = new System.Drawing.Point(12, 12);
            this.PB_AppIcon.Name = "PB_AppIcon";
            this.PB_AppIcon.Size = new System.Drawing.Size(128, 128);
            this.PB_AppIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PB_AppIcon.TabIndex = 19;
            this.PB_AppIcon.TabStop = false;
            // 
            // BTN_ChangeIcon
            // 
            this.BTN_ChangeIcon.Location = new System.Drawing.Point(12, 146);
            this.BTN_ChangeIcon.Name = "BTN_ChangeIcon";
            this.BTN_ChangeIcon.Size = new System.Drawing.Size(128, 23);
            this.BTN_ChangeIcon.TabIndex = 20;
            this.BTN_ChangeIcon.Text = "Browse new icon";
            this.BTN_ChangeIcon.UseVisualStyleBackColor = true;
            this.BTN_ChangeIcon.Click += new System.EventHandler(this.BTN_ChangeIcon_Click);
            // 
            // BrowseImageDialog
            // 
            this.BrowseImageDialog.Filter = "Portable Network Image|*.png|JPEG|*.jpg|JPEG|*.jpeg|Bitmap File|*.bmp|All Files|*" +
    ".*";
            this.BrowseImageDialog.Title = "Select an image file to set as application icon";
            // 
            // BTN_ExtractIcon
            // 
            this.BTN_ExtractIcon.Location = new System.Drawing.Point(12, 175);
            this.BTN_ExtractIcon.Name = "BTN_ExtractIcon";
            this.BTN_ExtractIcon.Size = new System.Drawing.Size(128, 23);
            this.BTN_ExtractIcon.TabIndex = 21;
            this.BTN_ExtractIcon.Text = "Extract icon from exe";
            this.BTN_ExtractIcon.UseVisualStyleBackColor = true;
            this.BTN_ExtractIcon.Click += new System.EventHandler(this.BTN_ExtractIcon_Click);
            // 
            // panelControls
            // 
            this.panelControls.Controls.Add(this.BTN_DetailSwitch);
            this.panelControls.Controls.Add(this.BTN_Save);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControls.Location = new System.Drawing.Point(0, 208);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(583, 32);
            this.panelControls.TabIndex = 22;
            // 
            // BTN_DetailSwitch
            // 
            this.BTN_DetailSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.BTN_DetailSwitch.Location = new System.Drawing.Point(12, 4);
            this.BTN_DetailSwitch.Name = "BTN_DetailSwitch";
            this.BTN_DetailSwitch.Size = new System.Drawing.Size(178, 23);
            this.BTN_DetailSwitch.TabIndex = 19;
            this.BTN_DetailSwitch.Text = "Show Advanced Options";
            this.BTN_DetailSwitch.UseVisualStyleBackColor = true;
            this.BTN_DetailSwitch.Click += new System.EventHandler(this.BTN_DetailSwitch_Click);
            // 
            // BrowseExeDialog
            // 
            this.BrowseExeDialog.Filter = "Executable File|*.exe|Dll File|*.dll|Icon file|*.ico";
            this.BrowseExeDialog.Title = "Select executable file that contain icon";
            // 
            // BTN_BrowseIcon
            // 
            this.BTN_BrowseIcon.Location = new System.Drawing.Point(117, 262);
            this.BTN_BrowseIcon.Name = "BTN_BrowseIcon";
            this.BTN_BrowseIcon.Size = new System.Drawing.Size(24, 24);
            this.BTN_BrowseIcon.TabIndex = 23;
            this.BTN_BrowseIcon.Text = "...";
            this.BTN_BrowseIcon.UseVisualStyleBackColor = true;
            this.BTN_BrowseIcon.Click += new System.EventHandler(this.BTN_BrowseIcon_Click);
            // 
            // BTN_BrowsePathLocation
            // 
            this.BTN_BrowsePathLocation.Location = new System.Drawing.Point(117, 291);
            this.BTN_BrowsePathLocation.Name = "BTN_BrowsePathLocation";
            this.BTN_BrowsePathLocation.Size = new System.Drawing.Size(24, 24);
            this.BTN_BrowsePathLocation.TabIndex = 24;
            this.BTN_BrowsePathLocation.Text = "...";
            this.BTN_BrowsePathLocation.UseVisualStyleBackColor = true;
            this.BTN_BrowsePathLocation.Click += new System.EventHandler(this.BTN_BrowsePathLocation_Click);
            // 
            // BTN_BrowseExe
            // 
            this.BTN_BrowseExe.Location = new System.Drawing.Point(117, 319);
            this.BTN_BrowseExe.Name = "BTN_BrowseExe";
            this.BTN_BrowseExe.Size = new System.Drawing.Size(24, 24);
            this.BTN_BrowseExe.TabIndex = 25;
            this.BTN_BrowseExe.Text = "...";
            this.BTN_BrowseExe.UseVisualStyleBackColor = true;
            this.BTN_BrowseExe.Click += new System.EventHandler(this.BTN_BrowseExe_Click);
            // 
            // TB_Tag_NewTag
            // 
            this.TB_Tag_NewTag.Location = new System.Drawing.Point(280, 175);
            this.TB_Tag_NewTag.Name = "TB_Tag_NewTag";
            this.TB_Tag_NewTag.Size = new System.Drawing.Size(205, 23);
            this.TB_Tag_NewTag.TabIndex = 26;
            this.TB_Tag_NewTag.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_Tag_NewTag_KeyDown);
            // 
            // BTN_Tags_Add
            // 
            this.BTN_Tags_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_Tags_Add.Location = new System.Drawing.Point(495, 175);
            this.BTN_Tags_Add.Name = "BTN_Tags_Add";
            this.BTN_Tags_Add.Size = new System.Drawing.Size(75, 23);
            this.BTN_Tags_Add.TabIndex = 27;
            this.BTN_Tags_Add.Text = "Add";
            this.BTN_Tags_Add.UseVisualStyleBackColor = true;
            this.BTN_Tags_Add.Click += new System.EventHandler(this.BTN_Tags_Add_Click);
            // 
            // ApplicationPackageEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 240);
            this.Controls.Add(this.BTN_Tags_Add);
            this.Controls.Add(this.TB_Tag_NewTag);
            this.Controls.Add(this.panelControls);
            this.Controls.Add(this.BTN_ExtractIcon);
            this.Controls.Add(this.BTN_ChangeIcon);
            this.Controls.Add(this.PB_AppIcon);
            this.Controls.Add(this.LB_Tags);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.NUM_DisplayIndex);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.CB_RunAsAdmin);
            this.Controls.Add(this.TB_ExeLocation);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TB_PathLocation);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TB_IconLocation);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TB_Name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TB_ID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BTN_BrowseExe);
            this.Controls.Add(this.BTN_BrowsePathLocation);
            this.Controls.Add(this.BTN_BrowseIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ApplicationPackageEditor";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Application Package Editor";
            this.Load += new System.EventHandler(this.ApplicationPackageEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NUM_DisplayIndex)).EndInit();
            this.TagsContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PB_AppIcon)).EndInit();
            this.panelControls.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TextBox TB_ID;
        private TextBox TB_Name;
        private Label label2;
        private TextBox TB_IconLocation;
        private Label label3;
        private TextBox TB_PathLocation;
        private Label label4;
        private TextBox TB_ExeLocation;
        private Label label5;
        private CheckBox CB_RunAsAdmin;
        private Label label6;
        private NumericUpDown NUM_DisplayIndex;
        private Label label8;
        private ListBox LB_Tags;
        private Button BTN_Save;
        private PictureBox PB_AppIcon;
        private Button BTN_ChangeIcon;
        private OpenFileDialog BrowseImageDialog;
        private Button BTN_ExtractIcon;
        private Panel panelControls;
        private Button BTN_DetailSwitch;
        private OpenFileDialog BrowseExeDialog;
        private Button BTN_BrowseIcon;
        private Button BTN_BrowsePathLocation;
        private Button BTN_BrowseExe;
        private TextBox TB_Tag_NewTag;
        private Button BTN_Tags_Add;
        private ContextMenuStrip TagsContextMenuStrip;
        private ToolStripMenuItem supprimerToolStripMenuItem;
    }
}