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
            try {
                Settings.Save(SettingsManager.SettingsFileLocation);
                this.Close();
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
        #endregion


    }
}
