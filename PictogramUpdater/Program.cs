using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Deployment.Application;
using System.Reflection;
using System.Security.Principal;
using System.Diagnostics;
using System.Threading;
using System.Globalization;

namespace PictogramUpdater {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // CreateDesktopIconSafe();

            CultureInfo inputLanguage = System.Windows.Forms.InputLanguage.CurrentInputLanguage.Culture;
            //inputLanguage = new CultureInfo("sv");
            //inputLanguage = new CultureInfo("de");
            //inputLanguage = new CultureInfo("et");
            //inputLanguage = new CultureInfo("en");
            Thread.CurrentThread.CurrentUICulture = inputLanguage;

            if (!IsAdmin() && IsXpOrNewer()) {

                DialogResult result = MessageBox.Show(TextResources.adminRightsNotice, TextResources.adminRightsTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.Cancel) {
                    return;
                }

                runAsAdmin();
            } else {
                Application.Run(new PictogramInstallerForm(inputLanguage));
            }
        }

        private static bool IsAdmin() {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(id);
            bool isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            return isAdmin;
        }

        private static bool IsXpOrNewer() {
            System.OperatingSystem osInfo = System.Environment.OSVersion;
            bool isNT = osInfo.Platform.Equals(System.PlatformID.Win32NT);
            bool isVersionVistaOrNewer = osInfo.Version.Major >= 5;
            return isNT && isVersionVistaOrNewer;
        }

        private static void runAsAdmin() {
            ProcessStartInfo procInfo = new ProcessStartInfo();
            procInfo.UseShellExecute = true;
            procInfo.WorkingDirectory = Environment.CurrentDirectory;
            procInfo.FileName = Application.ExecutablePath;
            procInfo.Verb = "runas";

            try {
                Process.Start(procInfo);
            } catch (Exception ex) {
                MessageBox.Show(TextResources.errorAdminRights + "(" + ex.Message + ")", TextResources.canNotStart);
            }
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

        private static void CreateDesktopIconSafe() {
            try {
                CreateDesktopIcon();
            } catch (Exception) {
                /* ignored. */
            }
        }
    }
}
