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
    public partial class SelectImageDialog : Form {

        private Icon[] IconList;

        public Icon? SelectedIcon = null;

        public string Title {
            get {
                return this.Text;
            }
            set {
                this.Text = value;
            }
        }

        public SelectImageDialog(Icon[] iconList) {
            InitializeComponent();
            this.IconList = iconList;
        }

        private void SelectImageDialog_Load(object sender, EventArgs e) {
            DrawList();
        }

        private void DrawList() {
            int ItemIndex = 0;
            foreach (Icon ico in IconList) {
                Bitmap? IconBmp = null;
                try {
                    IconBmp = IconUtil.ToBitmap(ico);
                } catch { continue; }
                if (IconBmp == null) continue;

                Panel IconItem = new Panel {
                    Name = "IconItem" + ItemIndex,
                    Size = new Size(128, 148),
                    Tag = ItemIndex
                };

                PictureBox PB_Icon = new PictureBox {
                    Name = "PbIcon" + ItemIndex,
                    Tag = ItemIndex,
                    Size = new Size(IconItem.Width, IconItem.Width),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Image = IconBmp,
                    Location = new Point(0, 0)
                };

                Label LBL_Size = new Label {
                    Name = "LblSize" + ItemIndex,
                    Tag = ItemIndex,
                    AutoSize = false,
                    Size = new Size(IconItem.Width, 20),
                    Location = new Point(0, PB_Icon.Size.Height),
                    Text = $"{IconBmp.Size.Width}px x {IconBmp.Size.Height}px"
                };

                IconItem.Click += OnItemClick;
                PB_Icon.Click += OnItemClick;
                LBL_Size.Click += OnItemClick;

                IconItem.Controls.Add(PB_Icon);
                IconItem.Controls.Add(LBL_Size);

                flowLayoutPanel1.Controls.Add(IconItem);

                ItemIndex++;
            }
        }

        private void OnItemClick(object? sender, EventArgs e) {
            if (sender == null) return;
            if (!(sender is Control)) return;
            Control ctrl = (Control)sender;

            int ItemIndex = Convert.ToInt32(ctrl.Tag);
            this.SelectedIcon = IconList[ItemIndex];
            SelectItemPanel(ItemIndex);
        }

        private void SelectItemPanel(int ItemIndex) {
            UnselectAllItemsPanel();
            foreach (Control ctrl in flowLayoutPanel1.Controls) {
                if (!(ctrl is Panel)) continue;
                Panel panel = (Panel)ctrl;

                if (!(panel.Name.Contains("IconItem"))) { continue; }
                string LoopItemIndexStr = panel.Name.Replace("IconItem", "");
                int LoopItemIndex = -1;
                try { LoopItemIndex = Convert.ToInt32(LoopItemIndexStr); } catch { continue; }
                if (LoopItemIndex == ItemIndex) {
                    panel.BorderStyle = BorderStyle.FixedSingle;
                    BTN_OK.Enabled = true;
                    break;
                }
            }
        }

        private void UnselectAllItemsPanel() {
            foreach (Control ctrl in flowLayoutPanel1.Controls) {
                if (!(ctrl is Panel)) continue;
                Panel panel = (Panel)ctrl;
                if (!(panel.Name.Contains("IconItem"))) { continue; }
                panel.BorderStyle = BorderStyle.None;
            }
        }

        private void BTN_OK_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void BTN_Cancel_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
