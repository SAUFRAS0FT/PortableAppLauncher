using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PortableAppLauncher
{
    public class SettingsManager
    {
        #region "Singleton Pattern"
        private static SettingsManager? _instance;
        private static readonly object _instanceLock = new object();
        public static readonly string SettingsFileLocation = Path.Combine(Application.StartupPath, "UserPreferences.ini");

        public static SettingsManager GetInstance() {
            lock (_instanceLock ) {
                if (_instance == null) {
                    _instance = new SettingsManager();
                }
                return _instance;
            }
        }
        #endregion

        #region "Properties"
        public uint LAUNCHER_UI_OPACITY = 90;
        public Size LAUNCHER_UI_SIZE = new Size(800, 500);
        
        public Color LAUNCHER_UI_BACKGROUND_COLOR = Color.Black;
        public Color LAUNCHER_UI_TEXT_COLOR = Color.White;

        public string LAUNCHER_TITLE = "Portable Application Launcher";

        public Point? LAUNCHER_LOCATION;
        public bool LAUNCHER_RESTORE_LOCATION = true;
        public bool LAUNCHER_RESTORE_SIZE = true;

        public Padding LAUNCHER_GRID_ELEMENT_MARGIN = new Padding(8,8,8,8);
        public int LAUNCHER_GRID_ELEMENT_ICON_SIZE = 128;
        public int LAUNCHER_GRID_ELEMENT_LABEL_HEIGHT = 40;
        public Font LAUNCHER_GRID_ELEMENT_LABEL_FONT = new Font("Segoe UI", 12f, FontStyle.Regular);

        public bool LAUNCHER_COMPORTMENT_LAUNCH_AND_HIDE = false;
        public LaunchAndHideAction GENERAL_HIDE_ACTION = LaunchAndHideAction.Close_App;

        #endregion

        #region "Enums"
        public enum LaunchAndHideAction {
            Close_App = 1,
            Minimize_Taskbar = 2,
            Minimize_StatutBar = 3
        }
        #endregion

        #region "Serialization"

        public void Save(string Location) {
            if (Location == null) { throw new Exception("Location parameter isn't provided"); }
            string? JsonString = null;
            try {
                JsonString = JsonConvert.SerializeObject(this);
                if (JsonString == null) { throw new Exception("Serialization result is null !"); }
            } catch {
                throw new Exception("Enable de serialize PackagesDatabase object to string using JsonFormatter");
            }
            try {
                StreamWriter sw = new StreamWriter(Location);
                sw.Write(JsonString);
                sw.Close();
            } catch {
                throw new Exception("Enable to write serialized database to file at " + Location);
            }
        }

        public static void LoadInstance(string Location) {
            if (File.Exists(Location) == false) { throw new FileNotFoundException("The serialized file provided doesn't exist ! (" + Location + ")"); }
            string? JsonString = null;
            try {
                StreamReader sr = new StreamReader(Location);
                JsonString = sr.ReadToEnd();
                sr.Close();
            } catch {
                throw new Exception("Enable to read serialized file from " + Location);
            }
            if (JsonString == null) { throw new Exception("Deserialization result is null !"); }
            try {
                SettingsManager? TempObject = JsonConvert.DeserializeObject<SettingsManager>(JsonString);
                if (TempObject == null) { throw new Exception("The deserialization process return a null object"); }
                _instance = TempObject;
            } catch {
                throw new Exception("Enable de deserialize PackagesDatabase object from string using JsonFormatter");
            }
        }

        #endregion
    }
}
