using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Deployment.Application;
using System.Reflection;

namespace PictogramUpdater {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try {
                CreateDesktopIcon();
            } catch (Exception e) {
                /* ignored. */
            }

            Application.Run(new PictogramInstallerForm());
        }



        /// <summary>
        /// For this to work, the Title and Company in the assembly info (Application|Assembly Information button) 
        /// must match the Product name and Publisher name in ClickOnce settings (Publish|Options button).
        /// </summary>
        private static void CreateDesktopIcon() {
            
            if (!ApplicationDeployment.IsNetworkDeployed) {
                return;
            }

            ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

            if (!ad.IsFirstRun) {
                return;
            }
            

            Assembly assembly = Assembly.GetEntryAssembly();
            string company = string.Empty;


            if (Attribute.IsDefined(assembly, typeof(AssemblyCompanyAttribute))) {
                var ascompany = (AssemblyCompanyAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyCompanyAttribute));
                company = ascompany.Company;
            }

            string title = string.Empty;
            if (Attribute.IsDefined(assembly, typeof(AssemblyTitleAttribute))) {
                var asdescription = (AssemblyTitleAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyTitleAttribute));
                title = asdescription.Title;
            }

            if (!string.IsNullOrEmpty(company)) {
                var desktopPath = string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "\\", title, ".appref-ms");
               
                var shortcutName = string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.Programs), "\\", company, "\\", title, ".appref-ms");

                System.IO.File.Copy(shortcutName, desktopPath, true);
            }

        }
    }




}