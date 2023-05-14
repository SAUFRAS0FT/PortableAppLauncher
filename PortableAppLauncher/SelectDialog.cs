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
    public partial class SelectDialog : Form
    {

        List<string> list = new List<string>();
        public string? SelectedItem = null;

        public string? Caption = null;
        public string? Title = null;

        public bool Completed = false;


        public SelectDialog(List<string> list) {
            InitializeComponent();
            this.list = list;
        }

        private void SelectDialog_Load(object sender, EventArgs e) {
            DrawControl();
            DisplayList();
        }

        private void DrawControl() {
            this.Text = Title;
            this.LBL_Caption.Text = Caption;
        }

        private void DisplayList() {
            LB.Items.Clear();
            foreach (string item in list) {
                LB.Items.Add(item);
            }
            if (LB.Items.Count > 0) { LB.SelectedIndex = 0; }
        }

        private void BTN_OK_Click(object sender, EventArgs e) {
            Completed = true;
            this.Close();
        }

        private void BTN_Cancel_Click(object sender, EventArgs e) {
            Completed = false;
            this.Close();
        }

        private void LB_SelectedIndexChanged(object sender, EventArgs e) {
            this.SelectedItem = LB.Items[LB.SelectedIndex].ToString();
        }
    }
}
