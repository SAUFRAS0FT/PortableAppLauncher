using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.ComponentModel.Design;
//using Toolbelt.Drawing;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;
using System.Drawing.Text;

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
            AddingPackageOperationEvent += OnAddingPackageEvent;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread t = new Thread(() => DisplayAppListAsync());
            t.Start();
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
            if (ExeLocation == null) return null;
            if (!(File.Exists(ExeLocation))) return null;
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

        //Executed (on main thread) when an adding package operation finish it traitment
        private void OnAddingPackageEvent(object? sender, AddingPackageOperationEventArg e) {
            if (e.Statut != AddingPackageOperationEventArg.OperationState.Sucess) {
                string MessageBoxContent = "An error occured when adding your application package. Details:" + Environment.NewLine + e.Message;
                if (e.ErrorException != null) {
                    MessageBoxContent += Environment.NewLine + "Exception message:" + Environment.NewLine  + e.ErrorException.Message;
                }
                MessageBox.Show(MessageBoxContent);
                HideAddingPanel();
            } else {
                HideAddingPanel();
                Thread t = new Thread(() => DisplayAppListAsync());
                t.Start();
            }
        }

        private class AddingPackageOperationEventArg : EventArgs {

            public OperationState Statut = OperationState.Unstarted;
            public string? Message = null;
            public Exception? ErrorException = null;
            public string? Param1 = null;

            public enum OperationState
            {
                Unstarted,
                Threading,
                Sucess,
                Fail
            }

        }

        private event EventHandler<AddingPackageOperationEventArg> AddingPackageOperationEvent;
        private delegate void AddingPackageOperationEventDelegate(AddingPackageOperationEventArg e);

        //Use to fire the event (Invoke on main thread !)
        private void RaiseAddingPackageOperationEvent(AddingPackageOperationEventArg e) {
            AddingPackageOperationEvent(this, e);
        }


        private void AddPackageByExecutableAsync(string ExecutableLocation) {
            bool PrintDebug = false;
            AddingPackageOperationEventDelegate dAddingPackageOperationEvent = new AddingPackageOperationEventDelegate(RaiseAddingPackageOperationEvent);
            AddingPackageOperationEventArg EventArg = new AddingPackageOperationEventArg();
            EventArg.Param1 = ExecutableLocation;

            FileInfo fInfo = new FileInfo(ExecutableLocation);
            string AppName = fInfo.Name.Replace(".exe", "");

            string AppNewPath = Path.Combine(DB.DefaultAppSpaceLocation, AppName);
            int version = 2;
            while (Directory.Exists(AppNewPath)) {
                AppNewPath = Path.Combine(DB.DefaultAppSpaceLocation, AppName) + "(" + version + ")";
                version++;
            }
            try {
                Directory.CreateDirectory(AppNewPath);
                if (PrintDebug) Debug.WriteLine("Directory created");
            } catch (Exception ex) {
                EventArg.Statut = AddingPackageOperationEventArg.OperationState.Fail;
                EventArg.Message = "Fail to create directory: " + AppNewPath;
                EventArg.ErrorException = ex;
                this.Invoke(dAddingPackageOperationEvent, EventArg);
                return;
            }
            
            string AppNewExecutableLocation = Path.Combine(AppNewPath, fInfo.Name);

            try {
                var ExeBytes = File.ReadAllBytes(ExecutableLocation);
                if (PrintDebug) Debug.WriteLine("Executable file loaded in RAM");
                File.WriteAllBytes(AppNewExecutableLocation, ExeBytes);
                if (PrintDebug) Debug.WriteLine("File writed on disk from RAM sucess");
            } catch (Exception ex) {
                EventArg.Statut = AddingPackageOperationEventArg.OperationState.Fail;
                EventArg.Message = $"Failed to copy the specified executable:{Environment.NewLine}{ExecutableLocation}{Environment.NewLine}to{Environment.NewLine}{AppNewExecutableLocation}";
                EventArg.ErrorException = ex;
                this.Invoke(dAddingPackageOperationEvent, EventArg);
                return;
            }
            

            string? AppIconLocation = Path.Combine(AppNewPath, "AppIcon.png");
            Image? AppIcon = GetExeLargestIcon(AppNewExecutableLocation);
            if (PrintDebug) Debug.WriteLine("Icon getted");
            if (AppIcon != null) {
                try {
                    AppIcon.Save(AppIconLocation, System.Drawing.Imaging.ImageFormat.Png);
                } catch {
                    AppIconLocation = "";
                    EventArg.Message = "Failed to get app icon";
                }
                
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
            if (PrintDebug) Debug.WriteLine("ApplicationPackage created");
            try {
                DB.Apps.Add(app);
                if (PrintDebug) Debug.WriteLine("Package added to RAM database");
                DB.Save(DB.DatabaseLocation);
                if (PrintDebug) Debug.WriteLine("Database saved to disk from ram");
            } catch (Exception ex) {
                EventArg.Statut = AddingPackageOperationEventArg.OperationState.Fail;
                EventArg.Message = $"Failed to save the application to the database ({DB.DatabaseLocation}";
                EventArg.ErrorException = ex;
                this.Invoke(dAddingPackageOperationEvent, EventArg);
                return;
            }

            EventArg.Statut = AddingPackageOperationEventArg.OperationState.Sucess;
            this.Invoke(dAddingPackageOperationEvent, EventArg);
            if (PrintDebug) Debug.WriteLine("Finish event invoked");
        }

        private void AddPackageByFolderAsync(string BasePath) {
            bool PrintDebug = true;
            if (PrintDebug) Debug.WriteLine("AddPackageByFolderAsync()");
            AddingPackageOperationEventDelegate dAddingPackageOperationEvent = new AddingPackageOperationEventDelegate(RaiseAddingPackageOperationEvent);
            AddingPackageOperationEventArg EventArg = new AddingPackageOperationEventArg();
            EventArg.Param1 = BasePath;
            EventArg.Statut = AddingPackageOperationEventArg.OperationState.Threading;

            DirectoryInfo dInfo = new DirectoryInfo(BasePath);
            List<string> exeFiles = new List<string>();
            if (PrintDebug) Debug.WriteLine("Search for exe files:");
            foreach (string file in Directory.EnumerateFiles(BasePath, "*.exe", System.IO.SearchOption.AllDirectories)) {
                exeFiles.Add(file);
                if (PrintDebug) Debug.WriteLine("    - " + file);
            }

            if (exeFiles.Count == 0) {
                if (PrintDebug) Debug.WriteLine("No executable binary found");
                EventArg.Statut = AddingPackageOperationEventArg.OperationState.Fail;
                EventArg.Message = "Unable to add folder path '" + dInfo.Name + "' to the launcher because it doesn't contain any executable file";
                this.Invoke(dAddingPackageOperationEvent, EventArg);
                return;
            }

            string? exeFile = null;
            if (exeFiles.Count > 1) {
                if (PrintDebug) Debug.WriteLine("More than 1 executable file was found...");
                List<string> exeFilesName = new List<string>();
                foreach (string FullExeFile in exeFiles) {
                    exeFilesName.Add(FullExeFile.Replace(BasePath + "\\", ""));
                }
                SelectDialog sDialog = new SelectDialog(exeFilesName) { Title = "Select the executable", Caption = "Severals executables are detected in the folder you provided. Please select one of these:" };
                if (PrintDebug) Debug.WriteLine("Prompt user to select an executable file");
                sDialog.ShowDialog();
                if (sDialog.Completed) {
                    if (sDialog.SelectedItem == null) { return; }
                    exeFile = Path.Combine(dInfo.FullName, sDialog.SelectedItem);
                    if (PrintDebug) Debug.WriteLine("User has selected this executable file: " + exeFile);
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
            if (PrintDebug) Debug.WriteLine("New destination path: " + NewPathLocation);
            if (PrintDebug) Debug.WriteLine("Starting copy of '" + BasePath + "' to '" + NewPathLocation + "'");
            CopyFilesRecursively(BasePath, NewPathLocation);

            FileInfo ExeFileInfo = new FileInfo(exeFile);
            //string NewExeFileLocation = Path.Combine(NewPathLocation, ExeFileInfo.Name);
            string SourceExeRelativeLocation = ExeFileInfo.FullName.Replace(dInfo.FullName, "");
            string NewExeFileLocation = NewPathLocation + SourceExeRelativeLocation;
            string? AppIconLocation = Path.Combine(NewPathLocation, "AppIcon.png");
            if (PrintDebug) Debug.WriteLine("Start extracting icon of binary '" + NewExeFileLocation);
            Image? AppIcon = GetExeLargestIcon(NewExeFileLocation);
            

            if (AppIcon != null) {
                try {
                    AppIcon.Save(AppIconLocation, System.Drawing.Imaging.ImageFormat.Png);
                    if (PrintDebug) Debug.WriteLine("Icon sucessfully saved to '" + AppIconLocation + "'");
                } catch (Exception ex) {
                    EventArg.Message = "Can't save app icon";
                    EventArg.ErrorException = ex;
                    AppIconLocation = "";
                }
                AppIcon.Dispose();
            } else {
                EventArg.Message = "Can't extract app icon";
                AppIconLocation = "";
            }

            if (PrintDebug) Debug.WriteLine("Creating application package...");
            ApplicationPackage app = new ApplicationPackage() {
                Id = DB.GetNewID(),
                ExeLocation = NewExeFileLocation,
                Name = ExeFileInfo.Name.Replace(".exe", ""),
                DisplayIndex = DB.Apps.Count,
                PathLocation = NewPathLocation,
                IconLocation = AppIconLocation
            };
            if (PrintDebug) Debug.WriteLine("Application package created");
            if (PrintDebug) Debug.WriteLine("Starting adding the package to database and them save it");
            try {
                DB.Apps.Add(app);
                DB.Save(DB.DatabaseLocation);
                if (PrintDebug) Debug.WriteLine("Sucessfully added package to database and save it");
            } catch (Exception ex) {
                EventArg.Statut = AddingPackageOperationEventArg.OperationState.Fail;
                EventArg.Message = "Can't save the database to " + DB.DatabaseLocation;
                this.Invoke(dAddingPackageOperationEvent, EventArg);
                return;
            }
            if (PrintDebug) Debug.WriteLine("Raise event");
            EventArg.Statut = AddingPackageOperationEventArg.OperationState.Sucess;
            this.Invoke(dAddingPackageOperationEvent, EventArg);
        }

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
            Thread t = new Thread(() => DisplayAppListAsync());
            t.Start();
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
                    Thread t = new Thread(() => DisplayAppListAsync());
                    t.Start();
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
                    Thread t = new Thread(() => DisplayAppListAsync());
                    t.Start();
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
                    //AddPackageByFolder(locations[0]);
                    //DisplayAppList();
                    //HideAddingPanel();
                    Thread t = new Thread(() => AddPackageByFolderAsync(locations[0]));
                    t.Start();
                } else {
                    if (fsi.Extension == ".exe") {
                        //AddPackageByExecutable(locations[0]);
                        //DisplayAppList();
                        Thread t = new Thread( () => AddPackageByExecutableAsync(locations[0]) );
                        t.Start();
                    }
                }
            } else {
                AddPackageByFiles(locations);
                Thread t = new Thread(() => DisplayAppListAsync());
                t.Start();
                HideAddingPanel();
            }
            
        }
        #endregion

        #region "Display List Methods"

        private delegate void NoParamDelegate();
        private void ClearFlowLayoutPanel() {
            flowLayoutPanel1.Controls.Clear();
        }

        private delegate void AddToFlowLayoutPanelDelegate(Control ctrl);
        private void AddToFlowLayoutPanel(Control ctrl) {
            flowLayoutPanel1.Controls.Add(ctrl);
        }

        private void DisplayAppListAsync(List<ApplicationPackage> list = null) {
            NoParamDelegate dClearFlowLayoutPanel = new NoParamDelegate(ClearFlowLayoutPanel);
            AddToFlowLayoutPanelDelegate dAddToFlowLayoutPanel = new AddToFlowLayoutPanelDelegate(AddToFlowLayoutPanel);
            if (list == null) { list = PackagesDatabase.GetAppsListOrderByDisplayIndex(DB.Apps); }
            this.Invoke(dClearFlowLayoutPanel);
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
                this.Invoke(dAddToFlowLayoutPanel, PanelApp);
            }
        }

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