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
            this.BTN_Save = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NUM_DisplayIndex)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Application id = ";
            // 
            // TB_ID
            // 
            this.TB_ID.Location = new System.Drawing.Point(199, 5);
            this.TB_ID.Name = "TB_ID";
            this.TB_ID.Size = new System.Drawing.Size(100, 23);
            this.TB_ID.TabIndex = 1;
            // 
            // TB_Name
            // 
            this.TB_Name.Location = new System.Drawing.Point(199, 34);
            this.TB_Name.Name = "TB_Name";
            this.TB_Name.Size = new System.Drawing.Size(367, 23);
            this.TB_Name.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Application name = ";
            // 
            // TB_IconLocation
            // 
            this.TB_IconLocation.Location = new System.Drawing.Point(199, 63);
            this.TB_IconLocation.Name = "TB_IconLocation";
            this.TB_IconLocation.Size = new System.Drawing.Size(367, 23);
            this.TB_IconLocation.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Icon Location = ";
            // 
            // TB_PathLocation
            // 
            this.TB_PathLocation.Location = new System.Drawing.Point(199, 92);
            this.TB_PathLocation.Name = "TB_PathLocation";
            this.TB_PathLocation.Size = new System.Drawing.Size(367, 23);
            this.TB_PathLocation.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Path Location = ";
            // 
            // TB_ExeLocation
            // 
            this.TB_ExeLocation.Location = new System.Drawing.Point(199, 121);
            this.TB_ExeLocation.Name = "TB_ExeLocation";
            this.TB_ExeLocation.Size = new System.Drawing.Size(367, 23);
            this.TB_ExeLocation.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "Exe Location = ";
            // 
            // CB_RunAsAdmin
            // 
            this.CB_RunAsAdmin.AutoSize = true;
            this.CB_RunAsAdmin.Location = new System.Drawing.Point(199, 150);
            this.CB_RunAsAdmin.Name = "CB_RunAsAdmin";
            this.CB_RunAsAdmin.Size = new System.Drawing.Size(100, 19);
            this.CB_RunAsAdmin.TabIndex = 10;
            this.CB_RunAsAdmin.Text = "Run as Admin";
            this.CB_RunAsAdmin.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 177);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 15);
            this.label6.TabIndex = 11;
            this.label6.Text = "Display Index = ";
            // 
            // NUM_DisplayIndex
            // 
            this.NUM_DisplayIndex.Location = new System.Drawing.Point(199, 175);
            this.NUM_DisplayIndex.Name = "NUM_DisplayIndex";
            this.NUM_DisplayIndex.Size = new System.Drawing.Size(100, 23);
            this.NUM_DisplayIndex.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 204);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 15);
            this.label8.TabIndex = 15;
            this.label8.Text = "Search Tags = ";
            // 
            // LB_Tags
            // 
            this.LB_Tags.FormattingEnabled = true;
            this.LB_Tags.ItemHeight = 15;
            this.LB_Tags.Location = new System.Drawing.Point(199, 204);
            this.LB_Tags.Name = "LB_Tags";
            this.LB_Tags.Size = new System.Drawing.Size(367, 94);
            this.LB_Tags.TabIndex = 17;
            // 
            // BTN_Save
            // 
            this.BTN_Save.Location = new System.Drawing.Point(491, 315);
            this.BTN_Save.Name = "BTN_Save";
            this.BTN_Save.Size = new System.Drawing.Size(75, 23);
            this.BTN_Save.TabIndex = 18;
            this.BTN_Save.Text = "Save";
            this.BTN_Save.UseVisualStyleBackColor = true;
            this.BTN_Save.Click += new System.EventHandler(this.BTN_Save_Click);
            // 
            // ApplicationPackageEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 350);
            this.Controls.Add(this.BTN_Save);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ApplicationPackageEditor";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Application Package Editor";
            this.Load += new System.EventHandler(this.ApplicationPackageEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NUM_DisplayIndex)).EndInit();
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
    }
}