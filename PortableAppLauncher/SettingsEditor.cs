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
    public partial class SettingsEditor : Form
    {
        private SettingsManager Settings = SettingsManager.GetInstance();
        private bool ImposeRestart = false;
        public SettingsEditor() {
            InitializeComponent();
        }

        private void SettingsEditor_Load(object sender, EventArgs e) {
            DisplaySettings();
        }

        private void DisplaySettings() {
            if (Settings == null) { return; }
            ColorBox_Launcher_BackgroundColor.BackColor = Settings.LAUNCHER_UI_BACKGROUND_COLOR;
            ColorBox_Launcher_TextColor.BackColor = Settings.LAUNCHER_UI_TEXT_COLOR;
            NUM_Launcher_Opacity.Value = Settings.LAUNCHER_UI_OPACITY;
            TB_Launcher_Title.Text = Settings.LAUNCHER_TITLE;
            CB_Launcher_RestoreLocation.Checked = Settings.LAUNCHER_RESTORE_LOCATION;
            CB_Launcher_RestoreSize.Checked = Settings.LAUNCHER_RESTORE_SIZE;
            NUM_LAUNCHER_GRID_ELEMENT_MARGIN_LEFT.Value = Settings.LAUNCHER_GRID_ELEMENT_MARGIN.Left;
            NUM_LAUNCHER_GRID_ELEMENT_MARGIN_TOP.Value = Settings.LAUNCHER_GRID_ELEMENT_MARGIN.Top;
            NUM_LAUNCHER_GRID_ELEMENT_MARGIN_RIGHT.Value = Settings.LAUNCHER_GRID_ELEMENT_MARGIN.Right;
            NUM_LAUNCHER_GRID_ELEMENT_MARGIN_BOTTOM.Value = Settings.LAUNCHER_GRID_ELEMENT_MARGIN.Bottom;
            NUM_LAUNCHER_GRID_ELEMENT_ICON_SIZE.Value = Settings.LAUNCHER_GRID_ELEMENT_ICON_SIZE;
            NUM_LAUNCHER_GRID_ELEMENT_LABEL_HEIGHT.Value = Settings.LAUNCHER_GRID_ELEMENT_LABEL_HEIGHT;
            TB_LAUNCHER_GRID_ELEMENT_LABEL_FONT.Text = Settings.LAUNCHER_GRID_ELEMENT_LABEL_FONT.ToString();
            TB_GENERAL_DATABASE_LOCATION.Text = Settings.GENERAL_DATABASE_LOCATION;
            TB_GENERAL_APP_SPACE_LOCATION.Text = Settings.GENERAL_APP_SPACE_LOCATION;
        }

        private void BTN_Save_Click(object sender, EventArgs e) {
            Settings.LAUNCHER_UI_BACKGROUND_COLOR = ColorBox_Launcher_BackgroundColor.BackColor;
            Settings.LAUNCHER_UI_TEXT_COLOR= ColorBox_Launcher_TextColor.BackColor;
            Settings.LAUNCHER_UI_OPACITY = Convert.ToUInt32(NUM_Launcher_Opacity.Value);
            Settings.LAUNCHER_TITLE = TB_Launcher_Title.Text;
            Settings.LAUNCHER_RESTORE_LOCATION = CB_Launcher_RestoreLocation.Checked;
            Settings.LAUNCHER_RESTORE_SIZE = CB_Launcher_RestoreSize.Checked;
            Settings.LAUNCHER_GRID_ELEMENT_MARGIN = new Padding(Convert.ToInt32(NUM_LAUNCHER_GRID_ELEMENT_MARGIN_LEFT.Value), Convert.ToInt32(NUM_LAUNCHER_GRID_ELEMENT_MARGIN_TOP.Value), Convert.ToInt32(NUM_LAUNCHER_GRID_ELEMENT_MARGIN_RIGHT.Value), Convert.ToInt32(NUM_LAUNCHER_GRID_ELEMENT_MARGIN_BOTTOM.Value));
            Settings.LAUNCHER_GRID_ELEMENT_ICON_SIZE = Convert.ToInt32(NUM_LAUNCHER_GRID_ELEMENT_ICON_SIZE.Value);
            Settings.LAUNCHER_GRID_ELEMENT_LABEL_HEIGHT = Convert.ToInt32(NUM_LAUNCHER_GRID_ELEMENT_LABEL_HEIGHT.Value);
            if (Settings.GENERAL_DATABASE_LOCATION != TB_GENERAL_DATABASE_LOCATION.Text) { Settings.GENERAL_DATABASE_LOCATION = TB_GENERAL_DATABASE_LOCATION.Text; ImposeRestart = true; }
            if (Settings.GENERAL_APP_SPACE_LOCATION != TB_GENERAL_APP_SPACE_LOCATION.Text) {
                if (Directory.Exists(TB_GENERAL_APP_SPACE_LOCATION.Text) == false) {
                    try {
                        Directory.CreateDirectory(TB_GENERAL_APP_SPACE_LOCATION.Text);
                    } catch (Exception ex) {
                        MessageBox.Show("Can't create the new Apps Space Directory at:" + Environment.NewLine + TB_GENERAL_APP_SPACE_LOCATION.Text + Environment.NewLine + Environment.NewLine + "Exception detail:" + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine + "Please create the specified directory manually or please change again the App Workspace Directory to his original value", "Warning changing app default directory", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                Settings.GENERAL_APP_SPACE_LOCATION = TB_GENERAL_APP_SPACE_LOCATION.Text;
                ImposeRestart = true;
            }
            
            try {
                Settings.Save(SettingsManager.SettingsFileLocation);
                if (ImposeRestart) {
                    MessageBox.Show("Application must restart to make settings changes.", "Settings saved sucessfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Restart();
                } else {
                    MessageBox.Show("Some settings require an app restart to take effect", "Settings saved sucessfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            } catch (Exception ex) {
                MessageBox.Show("Unable to save settings ! Detail:" + Environment.NewLine + ex.Message);
            }
        }

        #region "Controles"
        private void ColorBox_Launcher_BackgroundColor_Click(object sender, EventArgs e) {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK) {
                ColorBox_Launcher_BackgroundColor.BackColor = cd.Color;
            }
        }

        private void ColorBox_Launcher_TextColor_Click(object sender, EventArgs e) {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK) {
                ColorBox_Launcher_TextColor.BackColor = cd.Color;
            }
        }

        private void BTN_BrowseLabelElementsFont_Click(object sender, EventArgs e) {
            FontDialog fd = new FontDialog();
            fd.Font = Settings.LAUNCHER_GRID_ELEMENT_LABEL_FONT;
            if (fd.ShowDialog() == DialogResult.OK) {
                TB_LAUNCHER_GRID_ELEMENT_LABEL_FONT.Text = fd.Font.ToString();
                Settings.LAUNCHER_GRID_ELEMENT_LABEL_FONT = fd.Font;
            }
        }
        private void BTN_Browse_GENERAL_DATABASE_LOCATION_Click(object sender, EventArgs e) {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Serialized JSON|*.json";
            opf.InitialDirectory = Application.StartupPath;
            if (opf.ShowDialog() == DialogResult.OK) {
                TB_GENERAL_DATABASE_LOCATION.Text = opf.FileName;
            }
        }

        private void BTN_Browse_GENERAL_APP_SPACE_LOCATION_Click(object sender, EventArgs e) {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.InitialDirectory = Application.StartupPath;
            if (fbd.ShowDialog() == DialogResult.OK) {
                TB_GENERAL_APP_SPACE_LOCATION.Text = fbd.SelectedPath;
            }
        }
        #endregion
    }
}
