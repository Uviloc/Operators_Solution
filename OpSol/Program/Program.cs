using OperatorsSolution.Common;
using System.Configuration;
using System.Reflection;
using System.Windows.Forms;
using Console = System.Diagnostics.Debug;

namespace OperatorsSolution.Program
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Specify the location of the user.config file
            string appDirectory = Application.StartupPath; // Application directory
            string userConfigFile = Path.Combine(appDirectory, "user.config");

            // Set the custom settings provider to use this file
            SetCustomSettingsProvider(userConfigFile);





            //// Define the file path for the settings file
            //string settingsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AppSettings.config");

            //// Ensure that the settings use the custom provider
            //Properties.Settings.Default.Providers.Clear();  // Clear the existing providers
            //var customProvider = new CustomSettingsProvider(settingsFilePath);
            //Properties.Settings.Default.Providers.Add(CustomSettingsProvider);  // Add the custom provider

            //AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;


            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Load any assemblies dynamicly so that if they do not exist it skips them
#if !DEBUG
            //AssemblyLoader.LoadAssemblies();
#endif

            Application.Run(new OpSol_Form());
        }

        private static void SetCustomSettingsProvider(string configFilePath)
        {
            string? appDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            if (appDirectory == null)
                return;

            string settingsFilePath = Path.Combine(appDirectory, "user.config");
            Console.WriteLine(settingsFilePath);


            // Check if the settings file exists, if not, create it
            if (!File.Exists(settingsFilePath))
            {
                File.Create(settingsFilePath).Dispose();  // Create and immediately dispose of the file stream
            }
            // Dynamically add the custom settings provider to Settings.Default
            var customProvider = new CustomSettingsProvider(settingsFilePath);
        }
    }
}