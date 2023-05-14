using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppLauncher
{
    public partial class ApplicationPackageEditor : Form
    {

        public ApplicationPackage? app = null;

        public ApplicationPackageEditor(ApplicationPackage? app) {
            InitializeComponent();
            this.app = app;
        }

        private void ApplicationPackageEditor_Load(object sender, EventArgs e) {
            DisplayObject();
        }

        private void DisplayObject() {
            if (app == null) { this.Close(); }
            TB_ID.Text = Convert.ToString(app.Id);
            TB_Name.Text = app.Name;
            TB_PathLocation.Text = app.PathLocation;
            TB_IconLocation.Text = app.IconLocation;
            TB_ExeLocation.Text = app.ExeLocation;
            NUM_DisplayIndex.Value = app.DisplayIndex;
            CB_RunAsAdmin.Checked = app.RunAsAdmin;
            LB_Tags.Items.Clear();
            foreach (string tag in app.Tags) {
                LB_Tags.Items.Add(tag);
            }
        }

        private void BTN_Save_Click(object sender, EventArgs e) {
            if (app == null) { return; }
            app.Id = Convert.ToInt32(TB_ID.Text);
            app.Name= TB_Name.Text;
            app.PathLocation=TB_PathLocation.Text;
            app.IconLocation=TB_IconLocation.Text;
            app.ExeLocation=TB_ExeLocation.Text;
            app.DisplayIndex = Convert.ToInt32(NUM_DisplayIndex.Value);
            app.RunAsAdmin = CB_RunAsAdmin.Checked;
            app.Tags.Clear();
            foreach (string tag in LB_Tags.Items) {
                app.Tags.Add(tag);
            }
            this.Close();
        }
    }
}
