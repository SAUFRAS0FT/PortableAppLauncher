using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.ComponentModel.Design;
//using Toolbelt.Drawing;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;

namespace PortableAppLauncher
{
    public partial class Form1 : Form
    {

        public PackagesDatabase DB = new PackagesDatabase();

        #region "Base"
        public Form1()
        {
            InitializeComponent();
            try {
                DB.Load(DB.DatabaseLocation);
                Debug.WriteLine("Database loaded sucessfully !");
            } catch (Exception ex) {
                if (ex is FileNotFoundException) {
                    DB.Save(DB.DatabaseLocation);
                    Debug.WriteLine("Database created sucessfully !");
                }
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DisplayAppList();
        }
        #endregion

        #region "Fonctions"
        private int GetItemAppID(object? item) {
            if (item == null) { return -1; }
            int AppID = -1;
            if (item is Panel) {
                Panel target = (Panel)item;
                AppID = Convert.ToInt32(target.Name.Replace("PanelApp_", ""));
            }
            if (item is PictureBox) {
                PictureBox target = (PictureBox)item;
                AppID = Convert.ToInt32(target.Name.Replace("PictureBoxApp_", ""));
            }
            if (item is Label) {
                Label target = (Label)item;
                AppID = Convert.ToInt32(target.Name.Replace("LabelAppName_", ""));
            }
            return AppID;
        }

        private Panel? GetAppItemByAppID(int AppID) {
            foreach (Control ctrl in flowLayoutPanel1.Controls) {
                if (ctrl is Panel) {
                    Panel target = (Panel)ctrl;
                    if (target.Name == "PanelApp_" + AppID) { return target; }
                }
            }
            return null;
        }

        private void CopyFilesRecursively(string sourcePath, string targetPath) {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", System.IO.SearchOption.AllDirectories)) {
                try {
                    Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
                } catch { Debug.WriteLine($"Unable to create the following directory: {dirPath.Replace(sourcePath, targetPath)}"); }
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", System.IO.SearchOption.AllDirectories)) {
                try {
                    File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
                } catch { Debug.WriteLine($"Unable to copy the file {sourcePath} from {targetPath}"); }
            }
        }

        private Image? GetExeLargestIcon(string ExeLocation) {
            TsudaKageyu.IconExtractor ie = new TsudaKageyu.IconExtractor(ExeLocation);
            if (ie.Count == 0) { return null; }
            Icon ico = null;
            ico = ie.GetIcon(0);
            if (ico == null) { return null;}
            Icon[] Icons;
            Icons = SplitIcon(ico);

            Image returnImage = null;
            foreach (Icon icon in Icons) {
                try {
                    var iconData = GetIconData(icon);
                    int iconSize = iconData.Length;
                    Image IconImg = ToBitmap(icon);
                    if (returnImage == null) {
                        returnImage = IconImg;
                    } else {
                        if (IconImg.Size.Height > returnImage.Size.Height) {
                            returnImage = IconImg;
                        }
                    }
                } catch { Debug.WriteLine($"Unable to acess icon 0, {icon.Height} px from executable {ExeLocation}"); continue; }
            }
            return returnImage;
        }

        #region "Icons Utils"

        /// <summary>
        /// Split an Icon consists of multiple icons into an array of Icon each
        /// consists of single icons.
        /// </summary>
        /// <param name="icon">A System.Drawing.Icon to be split.</param>
        /// <returns>An array of System.Drawing.Icon.</returns>
        public static Icon[] SplitIcon(Icon icon) {
            if (icon == null)
                throw new ArgumentNullException("icon");

            // Get an .ico file in memory, then split it into separate icons.

            var src = GetIconData(icon);

            var splitIcons = new List<Icon>();
            {
                int count = BitConverter.ToUInt16(src, 4);

                for (int i = 0; i < count; i++) {
                    int length = BitConverter.ToInt32(src, 6 + 16 * i + 8);    // ICONDIRENTRY.dwBytesInRes
                    int offset = BitConverter.ToInt32(src, 6 + 16 * i + 12);   // ICONDIRENTRY.dwImageOffset

                    using (var dst = new BinaryWriter(new MemoryStream(6 + 16 + length))) {
                        // Copy ICONDIR and set idCount to 1.

                        dst.Write(src, 0, 4);
                        dst.Write((short)1);

                        // Copy ICONDIRENTRY and set dwImageOffset to 22.

                        dst.Write(src, 6 + 16 * i, 12); // ICONDIRENTRY except dwImageOffset
                        dst.Write(22);                   // ICONDIRENTRY.dwImageOffset

                        // Copy a picture.

                        dst.Write(src, offset, length);

                        // Create an icon from the in-memory file.

                        dst.BaseStream.Seek(0, SeekOrigin.Begin);
                        splitIcons.Add(new Icon(dst.BaseStream));
                    }
                }
            }

            return splitIcons.ToArray();
        }

        private static byte[] GetIconData(Icon icon) {
            using (var ms = new MemoryStream()) {
                icon.Save(ms);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Converts an Icon to a GDI+ Bitmap preserving the transparent area.
        /// </summary>
        /// <param name="icon">An System.Drawing.Icon to be converted.</param>
        /// <returns>A System.Drawing.Bitmap Object.</returns>
        public static Bitmap ToBitmap(Icon icon) {
            if (icon == null)
                throw new ArgumentNullException("icon");

            // Quick workaround: Create an .ico file in memory, then load it as a Bitmap.

            using (var ms = new MemoryStream()) {
                icon.Save(ms);
                using (var bmp = (Bitmap)Image.FromStream(ms)) {
                    return new Bitmap(bmp);
                }
            }
        }

        /// <summary>
        /// Gets the bit depth of an Icon.
        /// </summary>
        /// <param name="icon">An System.Drawing.Icon object.</param>
        /// <returns>Bit depth of the icon.</returns>
        /// <remarks>
        /// This method takes into account the PNG header.
        /// If the icon has multiple variations, this method returns the bit 
        /// depth of the first variation.
        /// </remarks>
        public static int GetBitCount(Icon icon) {
            if (icon == null)
                throw new ArgumentNullException("icon");

            // Get an .ico file in memory, then read the header.

            var data = GetIconData(icon);
            if (data.Length >= 51
                && data[22] == 0x89 && data[23] == 0x50 && data[24] == 0x4e && data[25] == 0x47
                && data[26] == 0x0d && data[27] == 0x0a && data[28] == 0x1a && data[29] == 0x0a
                && data[30] == 0x00 && data[31] == 0x00 && data[32] == 0x00 && data[33] == 0x0d
                && data[34] == 0x49 && data[35] == 0x48 && data[36] == 0x44 && data[37] == 0x52) {
                // The picture is PNG. Read IHDR chunk.

                switch (data[47]) {
                    case 0:
                        return data[46];
                    case 2:
                        return data[46] * 3;
                    case 3:
                        return data[46];
                    case 4:
                        return data[46] * 2;
                    case 6:
                        return data[46] * 4;
                    default:
                        // NOP
                        break;
                }
            } else if (data.Length >= 22) {
                // The picture is not PNG. Read ICONDIRENTRY structure.

                return BitConverter.ToUInt16(data, 12);
            }

            throw new ArgumentException("The icon is corrupt. Couldn't read the header.", "icon");
        }

        #endregion

        #endregion

        #region "Adding New Package"

        private void AddPackageByFiles(string[] files) {
            string TempPath = Path.Combine(DB.DefaultAppSpaceLocation, "temp");
            Directory.CreateDirectory(TempPath);

            foreach (string file in files) {
                FileSystemInfo fsi = new FileInfo(file);
                string FileDestination = Path.Combine(TempPath, fsi.Name);
                FileAttributes attr = File.GetAttributes(file);

                if (attr.HasFlag(FileAttributes.Directory)) {
                    DirectoryInfo dInfo = new DirectoryInfo(file);
                    string NewDirLocation = Path.Combine(TempPath, dInfo.Name);
                    CopyFilesRecursively(file, NewDirLocation);
                } else {
                    var FileBytes = File.ReadAllBytes(file);
                    File.WriteAllBytes(FileDestination, FileBytes);
                }

            }

            List<string> exeFiles = new List<string>();
            foreach (string file in Directory.EnumerateFiles(TempPath, "*.exe", System.IO.SearchOption.AllDirectories)) {
                exeFiles.Add(file);
            }

            if (exeFiles.Count == 0) {
                MessageBox.Show("Unable to add those files to the launcher because it doesn't contain any executable file");
                Directory.Delete(TempPath);
                return;
            }
            string? exeFile = null;
            if (exeFiles.Count > 1) {
                List<string> exeFilesName = new List<string>();
                foreach (string FullExeFile in exeFiles) {
                    exeFilesName.Add(FullExeFile.Replace(TempPath + "\\", ""));
                }
                SelectDialog sDialog = new SelectDialog(exeFilesName) { Title = "Select the executable", Caption = "Severals executables are detected in the folder you provided. Please select one of these:" };
                sDialog.ShowDialog();
                if (sDialog.Completed) {
                    if (sDialog.SelectedItem == null) { return; }
                    exeFile = Path.Combine(TempPath, sDialog.SelectedItem);
                }
            } else {
                exeFile = exeFiles[0];
            }

            if (exeFile == null) { MessageBox.Show("Unable to find the executable.");  return;  }

            string? AppIconLocation = Path.Combine(TempPath, "AppIcon.png");
            Image? AppIcon = GetExeLargestIcon(exeFile);

            if (AppIcon != null) {
                AppIcon.Save(AppIconLocation, System.Drawing.Imaging.ImageFormat.Png);
                AppIcon.Dispose();
            } else {
                AppIconLocation = "";
            }



            FileInfo ExeFileInfo = new FileInfo(exeFile);
            string AppName = ExeFileInfo.Name.Replace(".exe", "");
            string NewPath = TempPath.Replace("temp", AppName);

            int version = 2;
            while(Directory.Exists(NewPath)) {
                NewPath = TempPath.Replace("temp", AppName) + "(" + version + ")";
                version++;
            }
            Directory.Move(TempPath, NewPath);


            ApplicationPackage app = new ApplicationPackage() {
                Id = DB.GetNewID(),
                ExeLocation = exeFile.Replace(TempPath, NewPath),
                Name = AppName,
                DisplayIndex = DB.Apps.Count,
                PathLocation = NewPath,
                IconLocation = AppIconLocation.Replace(TempPath, NewPath)
            };

            DB.Apps.Add(app);
            DB.Save(DB.DatabaseLocation);
        }

        private void AddPackageByFolder(string BasePath) {
            DirectoryInfo dInfo = new DirectoryInfo(BasePath);
            List<string> exeFiles = new List<string>();

            foreach (string file in Directory.EnumerateFiles(BasePath, "*.exe", System.IO.SearchOption.AllDirectories)) {
                exeFiles.Add(file);
            }

            if (exeFiles.Count == 0) {
                MessageBox.Show("Unable to add folder path '" + dInfo.Name + "' to the launcher because it doesn't contain any executable file");
                return;
            }

            string? exeFile = null;
            if (exeFiles.Count > 1) {
                List<string> exeFilesName= new List<string>();
                foreach (string FullExeFile in exeFiles) {
                    exeFilesName.Add(FullExeFile.Replace(BasePath + "\\", ""));
                }
                SelectDialog sDialog = new SelectDialog(exeFilesName) { Title = "Select the executable", Caption = "Severals executables are detected in the folder you provided. Please select one of these:" };
                sDialog.ShowDialog();
                if (sDialog.Completed) {
                    if (sDialog.SelectedItem == null) { return; }
                    exeFile = Path.Combine(dInfo.FullName, sDialog.SelectedItem);
                }
            } else {
                exeFile = exeFiles[0];
            }

            string NewPathLocation = Path.Combine(DB.DefaultAppSpaceLocation, dInfo.Name);

            int version = 2;
            while (Directory.Exists(NewPathLocation)) {
                NewPathLocation = Path.Combine(DB.DefaultAppSpaceLocation, dInfo.Name) + "(" + version + ")";
                version++;
            }

            CopyFilesRecursively(BasePath, NewPathLocation);

            FileInfo ExeFileInfo = new FileInfo(exeFile);
            //string NewExeFileLocation = Path.Combine(NewPathLocation, ExeFileInfo.Name);
            string SourceExeRelativeLocation = ExeFileInfo.FullName.Replace(dInfo.FullName, "");
            string NewExeFileLocation = NewPathLocation + SourceExeRelativeLocation;

            string? AppIconLocation = Path.Combine(NewPathLocation, "AppIcon.png");
            Image? AppIcon = GetExeLargestIcon(NewExeFileLocation);

            if (AppIcon != null) {
                AppIcon.Save(AppIconLocation, System.Drawing.Imaging.ImageFormat.Png);
                AppIcon.Dispose();
            } else {
                AppIconLocation = "";
            }

            ApplicationPackage app = new ApplicationPackage() {
                Id = DB.GetNewID(),
                ExeLocation = NewExeFileLocation,
                Name = ExeFileInfo.Name.Replace(".exe",""),
                DisplayIndex = DB.Apps.Count,
                PathLocation = NewPathLocation,
                IconLocation = AppIconLocation
            };

            DB.Apps.Add(app);
            DB.Save(DB.DatabaseLocation);
        }

        private void AddPackageByExecutable(string ExecutableLocation) {
            FileInfo fInfo = new FileInfo(ExecutableLocation);
            string AppName = fInfo.Name.Replace(".exe", "");

            string AppNewPath = Path.Combine(DB.DefaultAppSpaceLocation, AppName);
            int version = 2;
            while (Directory.Exists(AppNewPath)) {
                AppNewPath = Path.Combine(DB.DefaultAppSpaceLocation, AppName) + "(" + version + ")";
                version++;
            }
            Directory.CreateDirectory(AppNewPath);
            string AppNewExecutableLocation = Path.Combine(AppNewPath, fInfo.Name);
            
            var ExeBytes = File.ReadAllBytes(ExecutableLocation);
            File.WriteAllBytes(AppNewExecutableLocation, ExeBytes);

            string? AppIconLocation = Path.Combine(AppNewPath, "AppIcon.png");
            Image? AppIcon = GetExeLargestIcon(AppNewExecutableLocation);

            if (AppIcon != null) {
                AppIcon.Save(AppIconLocation, System.Drawing.Imaging.ImageFormat.Png);
                AppIcon.Dispose();
            } else {
                AppIconLocation = "";
            }

            ApplicationPackage app = new ApplicationPackage() {
                Id = DB.GetNewID(),
                ExeLocation = AppNewExecutableLocation,
                Name = AppName,
                DisplayIndex = DB.Apps.Count,
                PathLocation = AppNewPath,
                IconLocation = AppIconLocation
            };

            DB.Apps.Add(app);
            DB.Save(DB.DatabaseLocation);
        }
        #endregion

        #region "Item Event"

        ApplicationPackage? MenuStripSelectedApp = null;
        private void AppItem_MouseDown(object? sender, MouseEventArgs e) {
            int AppID = GetItemAppID(sender);
            var app = DB.GetAppByID(AppID);
            if (app == null) { return; }
            
            if (e.Button== MouseButtons.Left) {
                app.Execute();
            }

            if (e.Button== MouseButtons.Right) {
                ItemMenuStrip.Text = app.Name;
                MenuStripSelectedApp = app;
                Panel? target = GetAppItemByAppID(AppID);
                if (target != null) {
                    ItemMenuStrip.Show(target, e.Location);
                } else {
                    ItemMenuStrip.Show(Cursor.Position);
                }
            }
        }

        private void AppItem_MouseUp(object? sender, MouseEventArgs e) {

        }

        private void AppItem_MouseEnter(object? sender, EventArgs e) {
            int AppID = GetItemAppID(sender);
            if (AppID == -1) { return; }
            Panel? target = GetAppItemByAppID(AppID);
            if (target == null) { return; }
            target.BorderStyle = BorderStyle.FixedSingle;
        }

        private void AppItem_MouseLeave(object? sender, EventArgs e) {
            int AppID = GetItemAppID(sender);
            if (AppID == -1) { return; }
            Panel? target = GetAppItemByAppID(AppID);
            if (target == null) { return; }
            target.BorderStyle = BorderStyle.None;
        }

        private void exécuterToolStripMenuItem_Click(object sender, EventArgs e) {
            if (MenuStripSelectedApp == null) { return; }
            MenuStripSelectedApp.Execute();
        }

        private void exécuterEnTantQuadministrateurToolStripMenuItem_Click(object sender, EventArgs e) {
            if (MenuStripSelectedApp == null) { return; }
            MenuStripSelectedApp.Execute(true);
        }

        private void modifierToolStripMenuItem_Click(object sender, EventArgs e) {
            if (MenuStripSelectedApp == null) { return; }
            var editor = new ApplicationPackageEditor(MenuStripSelectedApp);
            editor.ShowDialog();
            if (editor.app != null) {
                try {
                    DB.Update(editor.app);
                } catch (Exception ex) {
                    MessageBox.Show("Unable to save changes ! Inner Exception info:" + Environment.NewLine + ex.Message);
                }
                editor.Dispose();
            }
            DisplayAppList();
        }

        private void ouvrirLemplacementDeLapplicationToolStripMenuItem_Click(object sender, EventArgs e) {
            if (MenuStripSelectedApp == null) { return; }
            try {
                Process.Start("explorer.exe", "/e,/select," + MenuStripSelectedApp.ExeLocation);
            } catch (Exception ex) {
                MessageBox.Show("Unable to reveal the binary file in Windows Explorer. Details:" + Environment.NewLine + ex.Message);
            }
        }

        private void menuShellWindowsToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void supprimerToolStripMenuItem_Click(object sender, EventArgs e) {
            if (MenuStripSelectedApp == null) { return; }
            DialogResult response = MessageBox.Show("Do you also want to delete app file(s) from the file system (full uninstall)", "Full Delete ?", MessageBoxButtons.YesNoCancel);
            if (response == DialogResult.None || response == DialogResult.Cancel) {
                return;
            }
            if (response == DialogResult.Yes) {
                if (MenuStripSelectedApp.PathLocation == null) {
                    MessageBox.Show("Delete operation aborded because the path location isn't specified. Please fill this value or selected 'No' next time.");
                    return; 
                }
                try {
                    System.IO.Directory.Delete(MenuStripSelectedApp.PathLocation, true);
                } catch (Exception ex) {
                    MessageBox.Show("Unable to delete the app directory located at:" + Environment.NewLine + MenuStripSelectedApp.PathLocation + Environment.NewLine + "Exception info:" + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine + "The app will stay in the launcher.");
                    return;
                }
                int AppIndex = DB.GetAppIndexByID(MenuStripSelectedApp.Id);
                if (AppIndex == -1) {
                    MessageBox.Show("The app isn't in the database. App already deleted");
                    return;
                }
                try {
                    DB.Apps.RemoveAt(AppIndex);
                } catch (Exception ex) {
                    MessageBox.Show("Failed to delete the AppIndex " + AppIndex + " from the database. Exception:" + Environment.NewLine + ex.Message);
                    return;
                }
                try {
                    DB.Save(DB.DatabaseLocation);
                    DisplayAppList();
                } catch (Exception ex) {
                    MessageBox.Show("Failed to save database changes. Exception:" + Environment.NewLine + ex.Message);
                    return;
                }
            }
            if (response == DialogResult.No) {
                int AppIndex = DB.GetAppIndexByID(MenuStripSelectedApp.Id);
                if (AppIndex == -1) {
                    MessageBox.Show("The app isn't in the database. App already deleted");
                    return;
                }
                try {
                    DB.Apps.RemoveAt(AppIndex);
                } catch (Exception ex) {
                    MessageBox.Show("Failed to delete the AppIndex " + AppIndex + " from the database. Exception:" + Environment.NewLine + ex.Message);
                    return;
                }
                try {
                    DB.Save(DB.DatabaseLocation);
                    DisplayAppList();
                } catch (Exception ex) {
                    MessageBox.Show("Failed to save database changes. Exception:" + Environment.NewLine + ex.Message);
                    return;
                }
            }
        }

        #endregion

        #region "Import Methods"

        private void Form1_DragEnter(object sender, DragEventArgs e) {
            if (e.Data == null) { return; }
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effect = DragDropEffects.Copy;
                ShowAddingPanel();
            }
        }

        private void Form1_DragDrop(object sender, DragEventArgs e) {
            //Add new package:
            ShowAddingPanel(true);
            if (e.Data == null) { HideAddingPanel();  return; }
            string[] locations = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (locations == null) { HideAddingPanel();  return; }
            if (locations.Length == 0) { HideAddingPanel();  return; }
            if (locations.Length == 1) {
                FileSystemInfo fsi = new FileInfo(locations[0]);
                FileAttributes attr = File.GetAttributes(locations[0]);
                if (attr.HasFlag(FileAttributes.Directory)) {
                    AddPackageByFolder(locations[0]);
                    DisplayAppList();
                } else {
                    if (fsi.Extension == ".exe") {
                        AddPackageByExecutable(locations[0]);
                        DisplayAppList();
                    }
                }
            } else {
                AddPackageByFiles(locations);
                DisplayAppList();
            }
            HideAddingPanel();
        }
        #endregion

        #region "Display List Methods"

        private void DisplayAppList(List<ApplicationPackage> list = null) {
            if (list == null) { list = PackagesDatabase.GetAppsListOrderByDisplayIndex(DB.Apps); }
            flowLayoutPanel1.Controls.Clear();
            foreach (ApplicationPackage app in list) {
                var PanelApp = new Panel() { 
                    Size = new Size(128, 168),
                    Name = "PanelApp_" + app.Id,
                    Margin = new Padding(8, 8, 8, 8)
                };
                PanelApp.MouseDown += AppItem_MouseDown;
                PanelApp.MouseUp += AppItem_MouseUp;
                PanelApp.MouseEnter += AppItem_MouseEnter;
                PanelApp.MouseLeave += AppItem_MouseLeave;

                var PbApp = new PictureBox() {
                    Size = new Size(128, 128),
                    Name = "PictureBoxApp_" + app.Id,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Location = new Point(0, 0)
                };
                if (File.Exists(app.IconLocation)) {
                    PbApp.ImageLocation = app.IconLocation;
                }
                PbApp.MouseDown += AppItem_MouseDown;
                PbApp.MouseUp += AppItem_MouseUp;
                PbApp.MouseEnter += AppItem_MouseEnter;
                PbApp.MouseLeave += AppItem_MouseLeave;

                var LblAppName = new Label() {
                    Name = "LabelAppName_" + app.Id,
                    Text = app.Name,
                    AutoSize = false,
                    AutoEllipsis = true,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(128, 40),
                    Location = new Point(0, 128)
                };
                LblAppName.MouseDown += AppItem_MouseDown;
                LblAppName.MouseUp += AppItem_MouseUp;
                LblAppName.MouseEnter += AppItem_MouseEnter;
                LblAppName.MouseLeave += AppItem_MouseLeave;

                PanelApp.Controls.Add(PbApp);
                PanelApp.Controls.Add(LblAppName);
                flowLayoutPanel1.Controls.Add(PanelApp);
            }
        }

        private void Search(string Keywords) {
            List<ApplicationPackage> list = new List<ApplicationPackage>();
            foreach (ApplicationPackage app in DB.Apps) {
                if (app.Name != null) {
                    if (app.Name.ToLower().Contains(Keywords.ToLower())) { list.Add(app); continue; }
                }

                if (app.Tags.Count > 0) {
                    foreach (string tag in app.Tags) {
                        if (tag.ToLower().Contains(Keywords.ToLower())) { list.Add(app); break; }
                    }
                }
            }

            DisplayAppList(list);
        }


        #endregion

        #region "UI"

        private void ShowAddingPanel(bool Dropped = false) {
            CenterAddingPanel();
            flowLayoutPanel1.Visible = false;
            TB_Search.Visible = false;
            if (Dropped == true) {
                progressBar_Adding.Visible = true;
                LBL_Adding_Caption.Text = "Please wait while adding your app...";
                PB_Add_Icon.Image = Properties.Resources.add_green_128px;

            } else {
                progressBar_Adding.Visible = false;
                LBL_Adding_Caption.Text = "Drop application package (exe file, directory, files) here to add your app to the launcher.";
                PB_Add_Icon.Image = Properties.Resources.add_white_128px;
            }
            Panel_Adding.Visible = true;
        }

        private void HideAddingPanel() {
            Panel_Adding.Visible = false;
            flowLayoutPanel1.Visible = true;
            TB_Search.Visible = true;
            TB_Search.Select();
        }

        private void CenterAddingPanel() {
            Point AddingPanelLocation = new Point(0, 0);
            int Yoffset = -31; //Windows 10 top bar height;
            AddingPanelLocation.X = (this.Size.Width / 2) - (Panel_Adding.Size.Width / 2);
            AddingPanelLocation.Y = (this.Size.Height / 2) - (Panel_Adding.Size.Height / 2) + Yoffset;

            Panel_Adding.Location = AddingPanelLocation;
        }

        #endregion

        #region "Controls"

        private void TB_Search_TextChanged(object sender, EventArgs e) {
            Search(TB_Search.Text);
        }

        private void BTN_Settings_Enter(object sender, EventArgs e) {
            BTN_Settings.ImageIndex = 1;
        }

        private void BTN_Settings_Leave(object sender, EventArgs e) {
            BTN_Settings.ImageIndex = 0;
        }
        private void BTN_Settings_MouseEnter(object sender, EventArgs e) {
            BTN_Settings.ImageIndex = 1;
        }

        private void BTN_Settings_MouseLeave(object sender, EventArgs e) {
            BTN_Settings.ImageIndex = 0;
        }
        private void Form1_DragLeave(object sender, EventArgs e) {
            HideAddingPanel();
        }

        private void BTN_Settings_Click(object sender, EventArgs e) {
            MessageBox.Show("Available soon (UI customization, global settings, app spaces...)");
        }

        #endregion


    }
}