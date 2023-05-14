namespace PortableAppLauncher
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.TB_Search = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.ItemMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exécuterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exécuterEnTantQuadministrateurToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ouvrirLemplacementDeLapplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuShellWindowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.supprimerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BTN_Settings = new System.Windows.Forms.Button();
            this.ButtonImageList = new System.Windows.Forms.ImageList(this.components);
            this.PB_Add_Icon = new System.Windows.Forms.PictureBox();
            this.Panel_Adding = new System.Windows.Forms.Panel();
            this.LBL_Adding_Caption = new System.Windows.Forms.Label();
            this.progressBar_Adding = new System.Windows.Forms.ProgressBar();
            this.ItemMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Add_Icon)).BeginInit();
            this.Panel_Adding.SuspendLayout();
            this.SuspendLayout();
            // 
            // TB_Search
            // 
            this.TB_Search.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_Search.BackColor = System.Drawing.Color.Black;
            this.TB_Search.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_Search.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TB_Search.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.TB_Search.Location = new System.Drawing.Point(212, 12);
            this.TB_Search.Margin = new System.Windows.Forms.Padding(16);
            this.TB_Search.Name = "TB_Search";
            this.TB_Search.PlaceholderText = "Type to search...";
            this.TB_Search.Size = new System.Drawing.Size(360, 35);
            this.TB_Search.TabIndex = 0;
            this.TB_Search.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_Search.TextChanged += new System.EventHandler(this.TB_Search_TextChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 69);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(760, 380);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // ItemMenuStrip
            // 
            this.ItemMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exécuterToolStripMenuItem,
            this.exécuterEnTantQuadministrateurToolStripMenuItem,
            this.modifierToolStripMenuItem,
            this.ouvrirLemplacementDeLapplicationToolStripMenuItem,
            this.menuShellWindowsToolStripMenuItem,
            this.supprimerToolStripMenuItem});
            this.ItemMenuStrip.Name = "ItemMenuStrip";
            this.ItemMenuStrip.ShowImageMargin = false;
            this.ItemMenuStrip.Size = new System.Drawing.Size(249, 136);
            // 
            // exécuterToolStripMenuItem
            // 
            this.exécuterToolStripMenuItem.Name = "exécuterToolStripMenuItem";
            this.exécuterToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.exécuterToolStripMenuItem.Text = "Exécuter";
            this.exécuterToolStripMenuItem.Click += new System.EventHandler(this.exécuterToolStripMenuItem_Click);
            // 
            // exécuterEnTantQuadministrateurToolStripMenuItem
            // 
            this.exécuterEnTantQuadministrateurToolStripMenuItem.Name = "exécuterEnTantQuadministrateurToolStripMenuItem";
            this.exécuterEnTantQuadministrateurToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.exécuterEnTantQuadministrateurToolStripMenuItem.Text = "Exécuter en tant qu\'administrateur";
            this.exécuterEnTantQuadministrateurToolStripMenuItem.Click += new System.EventHandler(this.exécuterEnTantQuadministrateurToolStripMenuItem_Click);
            // 
            // modifierToolStripMenuItem
            // 
            this.modifierToolStripMenuItem.Name = "modifierToolStripMenuItem";
            this.modifierToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.modifierToolStripMenuItem.Text = "Modifier";
            this.modifierToolStripMenuItem.Click += new System.EventHandler(this.modifierToolStripMenuItem_Click);
            // 
            // ouvrirLemplacementDeLapplicationToolStripMenuItem
            // 
            this.ouvrirLemplacementDeLapplicationToolStripMenuItem.Name = "ouvrirLemplacementDeLapplicationToolStripMenuItem";
            this.ouvrirLemplacementDeLapplicationToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.ouvrirLemplacementDeLapplicationToolStripMenuItem.Text = "Ouvrir l\'emplacement de l\'application";
            this.ouvrirLemplacementDeLapplicationToolStripMenuItem.Click += new System.EventHandler(this.ouvrirLemplacementDeLapplicationToolStripMenuItem_Click);
            // 
            // menuShellWindowsToolStripMenuItem
            // 
            this.menuShellWindowsToolStripMenuItem.Name = "menuShellWindowsToolStripMenuItem";
            this.menuShellWindowsToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.menuShellWindowsToolStripMenuItem.Text = "Menu shell Windows";
            this.menuShellWindowsToolStripMenuItem.Click += new System.EventHandler(this.menuShellWindowsToolStripMenuItem_Click);
            // 
            // supprimerToolStripMenuItem
            // 
            this.supprimerToolStripMenuItem.Name = "supprimerToolStripMenuItem";
            this.supprimerToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.supprimerToolStripMenuItem.Text = "Supprimer";
            this.supprimerToolStripMenuItem.Click += new System.EventHandler(this.supprimerToolStripMenuItem_Click);
            // 
            // BTN_Settings
            // 
            this.BTN_Settings.FlatAppearance.BorderSize = 0;
            this.BTN_Settings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_Settings.ImageIndex = 0;
            this.BTN_Settings.ImageList = this.ButtonImageList;
            this.BTN_Settings.Location = new System.Drawing.Point(737, 12);
            this.BTN_Settings.Name = "BTN_Settings";
            this.BTN_Settings.Size = new System.Drawing.Size(35, 35);
            this.BTN_Settings.TabIndex = 2;
            this.BTN_Settings.UseVisualStyleBackColor = true;
            this.BTN_Settings.Click += new System.EventHandler(this.BTN_Settings_Click);
            this.BTN_Settings.Enter += new System.EventHandler(this.BTN_Settings_Enter);
            this.BTN_Settings.Leave += new System.EventHandler(this.BTN_Settings_Leave);
            this.BTN_Settings.MouseEnter += new System.EventHandler(this.BTN_Settings_MouseEnter);
            this.BTN_Settings.MouseLeave += new System.EventHandler(this.BTN_Settings_MouseLeave);
            // 
            // ButtonImageList
            // 
            this.ButtonImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.ButtonImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ButtonImageList.ImageStream")));
            this.ButtonImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ButtonImageList.Images.SetKeyName(0, "setting-freepik_white_128px.png");
            this.ButtonImageList.Images.SetKeyName(1, "setting-freepik_green_128px.png");
            // 
            // PB_Add_Icon
            // 
            this.PB_Add_Icon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PB_Add_Icon.Image = global::PortableAppLauncher.Properties.Resources.add_white_128px;
            this.PB_Add_Icon.Location = new System.Drawing.Point(173, 85);
            this.PB_Add_Icon.Name = "PB_Add_Icon";
            this.PB_Add_Icon.Size = new System.Drawing.Size(128, 128);
            this.PB_Add_Icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PB_Add_Icon.TabIndex = 3;
            this.PB_Add_Icon.TabStop = false;
            // 
            // Panel_Adding
            // 
            this.Panel_Adding.Controls.Add(this.LBL_Adding_Caption);
            this.Panel_Adding.Controls.Add(this.progressBar_Adding);
            this.Panel_Adding.Controls.Add(this.PB_Add_Icon);
            this.Panel_Adding.Location = new System.Drawing.Point(78, 30);
            this.Panel_Adding.Name = "Panel_Adding";
            this.Panel_Adding.Size = new System.Drawing.Size(474, 299);
            this.Panel_Adding.TabIndex = 4;
            this.Panel_Adding.Visible = false;
            // 
            // LBL_Adding_Caption
            // 
            this.LBL_Adding_Caption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LBL_Adding_Caption.Location = new System.Drawing.Point(48, 251);
            this.LBL_Adding_Caption.Name = "LBL_Adding_Caption";
            this.LBL_Adding_Caption.Size = new System.Drawing.Size(379, 36);
            this.LBL_Adding_Caption.TabIndex = 5;
            this.LBL_Adding_Caption.Text = "Drop portable application package here";
            this.LBL_Adding_Caption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBar_Adding
            // 
            this.progressBar_Adding.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar_Adding.Location = new System.Drawing.Point(48, 222);
            this.progressBar_Adding.MarqueeAnimationSpeed = 10;
            this.progressBar_Adding.Name = "progressBar_Adding";
            this.progressBar_Adding.Size = new System.Drawing.Size(379, 17);
            this.progressBar_Adding.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar_Adding.TabIndex = 4;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.BTN_Settings);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.TB_Search);
            this.Controls.Add(this.Panel_Adding);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Application Launcher";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.DragLeave += new System.EventHandler(this.Form1_DragLeave);
            this.ItemMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PB_Add_Icon)).EndInit();
            this.Panel_Adding.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox TB_Search;
        private FlowLayoutPanel flowLayoutPanel1;
        private ContextMenuStrip ItemMenuStrip;
        private ToolStripMenuItem exécuterToolStripMenuItem;
        private ToolStripMenuItem exécuterEnTantQuadministrateurToolStripMenuItem;
        private ToolStripMenuItem modifierToolStripMenuItem;
        private ToolStripMenuItem ouvrirLemplacementDeLapplicationToolStripMenuItem;
        private ToolStripMenuItem menuShellWindowsToolStripMenuItem;
        private ToolStripMenuItem supprimerToolStripMenuItem;
        private Button BTN_Settings;
        private ImageList ButtonImageList;
        private PictureBox PB_Add_Icon;
        private Panel Panel_Adding;
        private Label LBL_Adding_Caption;
        private ProgressBar progressBar_Adding;
    }
}