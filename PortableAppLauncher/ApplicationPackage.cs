using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppLauncher
{
    public class ApplicationPackage
    {
        public int Id = -1;
        public string? Name = null;
        public List<string> Tags = new List<string>();
        public string? PathLocation = null;
        public string? ExeLocation = null;
        public string? IconLocation = null;
        public bool RunAsAdmin = false;

        public int DisplayIndex;

        public ApplicationPackage() { 
            
        }

        public void Execute(bool ForceAdmin = false) {
            if (ForceAdmin) { RunAsAdmin= true; }

            Process p = new Process();
            p.StartInfo.FileName = ExeLocation;
            if (RunAsAdmin) {
                p.StartInfo.UseShellExecute= true;
                p.StartInfo.Verb = "runas";
            }
            try {
                p.Start();
            } catch (Exception ex) {
                MessageBox.Show("Unable to start the process of the binary '" + Name + "' located here:" + Environment.NewLine + ExeLocation + Environment.NewLine + "Exception message:" + Environment.NewLine + ex.Message);
            }

            if (ForceAdmin) { RunAsAdmin = false; }
        }

    }
}
