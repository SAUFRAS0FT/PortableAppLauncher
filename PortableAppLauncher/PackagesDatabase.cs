using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace PortableAppLauncher
{
    public class PackagesDatabase
    {

        public List<ApplicationPackage> Apps = new List<ApplicationPackage>();
        public int LastAppID = 0;

        public string DefaultAppSpaceLocation = Path.Combine(Environment.CurrentDirectory, "Apps");
        public string DatabaseLocation = Path.Combine(Environment.CurrentDirectory, "AppDatabase.json");

        #region "Database Management"

        public int GetNewID() {
            LastAppID++;
            try {
                Save(DatabaseLocation);
            } catch {
                Debug.WriteLine("PackagesDatabase.GetNewID() : Failed to saved current App ID = " + LastAppID);
            }
            return LastAppID;
        }

        public ApplicationPackage? GetAppByID(int id) { 
            foreach (ApplicationPackage app in Apps) {
                if (app.Id == id) return app;
            }
            return null;
        }

        public int GetAppIndexByID(int id) {
            for (int i = 0; i < Apps.Count; i++) {
                if (Apps[i].Id == id) return i;
            }
            return -1;
        }

        public bool Contains(ApplicationPackage Application) {
            foreach (ApplicationPackage app in Apps) {
                if (app.Id == Application.Id) { return true; }
            }
            return false;
        }

        public void Update(ApplicationPackage app) {
            int AppIndex = GetAppIndexByID(app.Id);
            if (AppIndex == -1) { throw new Exception("The app id " + app.Id + "isn't in the database"); }
            try {
                Apps.RemoveAt(AppIndex);
            } catch (Exception ex){
                throw new Exception("Unable to remove the app to the database at index " + AppIndex + ". Inner Exception info:" + Environment.NewLine + ex.Message);
            }
            Apps.Add(app);
            try {
                this.Save(DatabaseLocation);
            } catch (Exception ex) {
                throw new Exception("Unable to save change in the database file. Inner exception info:" + Environment.NewLine + ex.Message);
            }
        }

        public static List<ApplicationPackage> GetAppsListOrderByDisplayIndex(List<ApplicationPackage> Apps) {
            List<ApplicationPackage> sortedApps = new List<ApplicationPackage>(Apps);
            sortedApps.Sort((app1, app2) => app1.DisplayIndex.CompareTo(app2.DisplayIndex));
            return sortedApps;
        }

        #endregion

        #region "Serialization"

        public void Save(string Location)
        {
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

        public void Load(string Location)
        {
            if (File.Exists(Location) == false) { throw new FileNotFoundException("The database file provided doesn't exist ! (" + Location + ")"); }
            string? JsonString = null;
            try {
                StreamReader sr = new StreamReader(Location);
                JsonString = sr.ReadToEnd();
                sr.Close();
            } catch {
                throw new Exception("Enable to read serialized database to file from " + Location);
            }
            if (JsonString == null) { throw new Exception("Deserialization result is null !"); }
            try {
                PackagesDatabase? TempObject = JsonConvert.DeserializeObject<PackagesDatabase>(JsonString);
                if (TempObject == null) { throw new Exception("The deserialization process return a null object"); }
                this.Apps = TempObject.Apps;
                this.LastAppID = TempObject.LastAppID;
                this.DefaultAppSpaceLocation = TempObject.DefaultAppSpaceLocation;
                this.DatabaseLocation = TempObject.DatabaseLocation;
            } catch {
                throw new Exception("Enable de deserialize PackagesDatabase object from string using JsonFormatter");
            }
        }

        #endregion
    }
}
