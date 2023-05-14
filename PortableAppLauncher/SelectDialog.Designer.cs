namespace PortableAppLauncher
{
    partial class SelectDialog
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
            this.LBL_Caption = new System.Windows.Forms.Label();
            this.LB = new System.Windows.Forms.ListBox();
            this.BTN_OK = new System.Windows.Forms.Button();
            this.BTN_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LBL_Caption
            // 
            this.LBL_Caption.Location = new System.Drawing.Point(8, 7);
            this.LBL_Caption.Name = "LBL_Caption";
            this.LBL_Caption.Size = new System.Drawing.Size(409, 38);
            this.LBL_Caption.TabIndex = 0;
            this.LBL_Caption.Text = "Select an item:";
            // 
            // LB
            // 
            this.LB.FormattingEnabled = true;
            this.LB.ItemHeight = 15;
            this.LB.Location = new System.Drawing.Point(8, 48);
            this.LB.Name = "LB";
            this.LB.Size = new System.Drawing.Size(409, 124);
            this.LB.TabIndex = 1;
            this.LB.SelectedIndexChanged += new System.EventHandler(this.LB_SelectedIndexChanged);
            // 
            // BTN_OK
            // 
            this.BTN_OK.Location = new System.Drawing.Point(342, 178);
            this.BTN_OK.Name = "BTN_OK";
            this.BTN_OK.Size = new System.Drawing.Size(75, 23);
            this.BTN_OK.TabIndex = 2;
            this.BTN_OK.Text = "OK";
            this.BTN_OK.UseVisualStyleBackColor = true;
            this.BTN_OK.Click += new System.EventHandler(this.BTN_OK_Click);
            // 
            // BTN_Cancel
            // 
            this.BTN_Cancel.Location = new System.Drawing.Point(261, 178);
            this.BTN_Cancel.Name = "BTN_Cancel";
            this.BTN_Cancel.Size = new System.Drawing.Size(75, 23);
            this.BTN_Cancel.TabIndex = 3;
            this.BTN_Cancel.Text = "Cancel";
            this.BTN_Cancel.UseVisualStyleBackColor = true;
            this.BTN_Cancel.Click += new System.EventHandler(this.BTN_Cancel_Click);
            // 
            // SelectDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 207);
            this.ControlBox = false;
            this.Controls.Add(this.BTN_Cancel);
            this.Controls.Add(this.BTN_OK);
            this.Controls.Add(this.LB);
            this.Controls.Add(this.LBL_Caption);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SelectDialog";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SelectDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Label LBL_Caption;
        private ListBox LB;
        private Button BTN_OK;
        private Button BTN_Cancel;
    }
}