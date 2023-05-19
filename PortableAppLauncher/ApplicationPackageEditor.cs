using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppLauncher
{
    public partial class ApplicationPackageEditor : Form
    {

        public ApplicationPackage? app = null;
        private PackagesDatabase DB = PackagesDatabase.GetInstance();
        private SettingsManager Settings = SettingsManager.GetInstance();

        private readonly Size FormMaxSize = new Size(599, 425);
        private readonly Size FormMinSize = new Size(599, 279);
        private bool AdvancedMode = false;

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
            if (File.Exists(app.IconLocation)) {
                PB_AppIcon.ImageLocation = app.IconLocation;
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

        private string? GetNewAppIconLocation(string Extenssion = ".png") {
            if (app == null) return null;
            string? NewIconLocation = null;

            if (Directory.Exists(app.PathLocation)) {
                NewIconLocation = Path.Combine(app.PathLocation, "CustomAppIcon" + Extenssion);
                int fileVersion = 2;
                while (File.Exists(NewIconLocation)) {
                    NewIconLocation = Path.Combine(app.PathLocation, "CustomAppIcon(" + fileVersion + ")" + Extenssion);
                    fileVersion++;
                }
            } else if (File.Exists(app.ExeLocation)) {
                FileInfo exeInfo = new FileInfo(app.ExeLocation);
                DirectoryInfo? exePathInfo = exeInfo.Directory;
                if (exePathInfo != null) {
                    NewIconLocation = Path.Combine(exePathInfo.FullName, "CustomAppIcon" + Extenssion);
                    int fileVersion = 2;
                    while (File.Exists(NewIconLocation)) {
                        NewIconLocation = Path.Combine(exePathInfo.FullName, "CustomAppIcon(" + fileVersion + ")" + Extenssion);
                        fileVersion++;
                    }
                }
            } else if (app.Name != null) {
                string PotentialPathLocation = Path.Combine(Settings.GENERAL_APP_SPACE_LOCATION, app.Name);
                if (Directory.Exists(PotentialPathLocation)) {
                    NewIconLocation = Path.Combine(PotentialPathLocation, "CustomAppIcon" + Extenssion);
                    int fileVersion = 2;
                    while (File.Exists(NewIconLocation)) {
                        NewIconLocation = Path.Combine(PotentialPathLocation, "CustomAppIcon(" + fileVersion + ")" + Extenssion);
                        fileVersion++;
                    }
                }
            }

            if (NewIconLocation == null) {
                string CustomIconPath = Path.Combine(Settings.GENERAL_APP_SPACE_LOCATION, "CustomIcons");
                if (!(Directory.Exists(CustomIconPath))) {
                    try {
                        Directory.CreateDirectory(CustomIconPath);
                    } catch (Exception ex) {
                        throw new Exception("Unable to create safe custom icon directory.", ex);
                    }
                }
                int fileVersion = 0;
                NewIconLocation = Path.Combine(CustomIconPath, "icon" + fileVersion + Extenssion);
                while (File.Exists(NewIconLocation)) {
                    fileVersion++;
                    NewIconLocation = Path.Combine(CustomIconPath, "icon" + fileVersion + Extenssion);
                }
            }

            return NewIconLocation;
        }

        private void BTN_ChangeIcon_Click(object sender, EventArgs e) {
            if (app == null) return;

            if (BrowseImageDialog.ShowDialog() == DialogResult.OK) {
                FileInfo fInfo = new FileInfo(BrowseImageDialog.FileName);
                
                string? NewIconLocation = null;
                try {
                    NewIconLocation = GetNewAppIconLocation(fInfo.Extension);
                } catch (Exception ex) {
                    string msg = ex.Message;
                    if (ex.InnerException != null) { msg = msg + Environment.NewLine + "Details:" + Environment.NewLine + ex.InnerException.Message; }
                    MessageBox.Show(msg, "Changing app package icon error");
                    return;
                }
                if (NewIconLocation == null) { MessageBox.Show("Can't get new icon location", "Changing app package icon error"); return; }

                try {
                    File.Copy(BrowseImageDialog.FileName, NewIconLocation, false);
                } catch (Exception ex) {
                    MessageBox.Show("Unable to copy the icon you selected to a safe place !" + Environment.NewLine + "Exception details:" + Environment.NewLine + ex.Message, "Changing app package icon error");
                    return;
                }

                app.IconLocation = NewIconLocation;
                PB_AppIcon.ImageLocation = app.IconLocation;
                TB_IconLocation.Text = app.IconLocation;
            }
        }

        private void BTN_ExtractIcon_Click(object sender, EventArgs e) {
            if (app == null) return;
            if (File.Exists(app.ExeLocation)) {
                FileInfo fExeInfo = new FileInfo(app.ExeLocation);
                DirectoryInfo? dExeInfo = fExeInfo.Directory;
                if (dExeInfo != null) BrowseExeDialog.InitialDirectory = dExeInfo.FullName;
                BrowseExeDialog.FileName = app.ExeLocation;
            }
            if (BrowseExeDialog.ShowDialog() != DialogResult.OK) return;

            

            IconPickerDialog iconPickerDialog = new IconPickerDialog();
            iconPickerDialog.FileName = BrowseExeDialog.FileName;
            if (iconPickerDialog.ShowDialog() != DialogResult.OK) return;

            TsudaKageyu.IconExtractor ie = new TsudaKageyu.IconExtractor(iconPickerDialog.FileName);
            if (ie.Count == 0) { MessageBox.Show("The selected file doesn't contain any icons."); return; }

            Icon? ico = null;
            ico = ie.GetIcon(iconPickerDialog.IconIndex);
            if (ico == null) { MessageBox.Show("Unable to extract this icon !");  return; }
            Icon[] Icons;
            Icons = IconUtil.SplitIcon(ico);
            SelectImageDialog sid = new SelectImageDialog(Icons);
            sid.Text = "Choose the icon to extract";
            sid.ShowDialog();
            if (sid.SelectedIcon == null) return;
            Bitmap BitmapIcon = IconUtil.ToBitmap(sid.SelectedIcon);

            string? NewIconLocation = null;
            try {
                NewIconLocation = GetNewAppIconLocation(".png");
            } catch (Exception ex) {
                string msg = ex.Message;
                if (ex.InnerException != null) { msg = msg + Environment.NewLine + "Details:" + Environment.NewLine + ex.InnerException.Message; }
                MessageBox.Show(msg, "Changing app package icon error");
                return;
            }
            if (NewIconLocation == null) { MessageBox.Show("Can't get new icon location", "Changing app package icon error"); return; }

            try {
                FileStream f = File.OpenWrite(NewIconLocation);
                BitmapIcon.Save(f, System.Drawing.Imaging.ImageFormat.Png);
                f.Close();
            } catch (Exception ex) {
                MessageBox.Show("Failed to save the extracted icon to:" + Environment.NewLine + NewIconLocation + Environment.NewLine + Environment.NewLine + "Exception Details:" + Environment.NewLine + ex.Message, "Changing app package icon error");
                return;
            }

            app.IconLocation = NewIconLocation;
            PB_AppIcon.ImageLocation = app.IconLocation;
            TB_IconLocation.Text = app.IconLocation;
        }

        private void BTN_BrowseIcon_Click(object sender, EventArgs e) {
            if (BrowseImageDialog.ShowDialog() == DialogResult.OK) {
                TB_IconLocation.Text = BrowseImageDialog.FileName;
            }
        }

        private void BTN_BrowsePathLocation_Click(object sender, EventArgs e) {
            var fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK) {
                TB_PathLocation.Text = fbd.SelectedPath;
            }
        }

        private void BTN_BrowseExe_Click(object sender, EventArgs e) {
            var opf = new OpenFileDialog();
            opf.Filter = "Executable Binary|*.exe";
            if (opf.ShowDialog() == DialogResult.OK) {
                TB_ExeLocation.Text = opf.FileName;
            }
        }

        private void BTN_DetailSwitch_Click(object sender, EventArgs e) {
            if (AdvancedMode == false) {
                this.Size = FormMaxSize;
                BTN_DetailSwitch.Text = "Hide advanced options";
                AdvancedMode = true;
            } else if (AdvancedMode == true) {
                this.Size = FormMinSize;
                BTN_DetailSwitch.Text = "Show advanced options";
                AdvancedMode = false;
            }
        }

        private void TB_Tag_NewTag_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                BTN_Tags_Add.PerformClick();
            }
        }

        private void BTN_Tags_Add_Click(object sender, EventArgs e) {
            if (app == null) return;
            //app.Tags.Add(TB_Tag_NewTag.Text);
            LB_Tags.Items.Add(TB_Tag_NewTag.Text);
            TB_Tag_NewTag.Clear();
            TB_Tag_NewTag.Select();
        }

        private void supprimerToolStripMenuItem_Click(object sender, EventArgs e) {
            if (app == null) return;
            if (LB_Tags.Items.Count == 0) { return; }
            app.Tags.RemoveAt(LB_Tags.SelectedIndex);
        }
    }
}
