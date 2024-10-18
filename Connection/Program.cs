namespace OperatorsSolution
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Load any assemblies dynamicly so that if they do not exist it skips them
            //AssemblyLoader.LoadAssemblies();

            Application.Run(new OpSol_Form());
        }
    }
}