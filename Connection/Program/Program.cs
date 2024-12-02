using System.Reflection;

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

        //private static Assembly? CurrentDomain_AssemblyResolve(object? sender, ResolveEventArgs args)
        //{
        //    // Define the custom folder to load from
        //    string customFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CustomLibraries");

        //    // Try loading the assembly
        //    string assemblyPath = Path.Combine(customFolder, new AssemblyName(args.Name).Name + ".dll");

        //    if (File.Exists(assemblyPath))
        //    {
        //        return Assembly.LoadFrom(assemblyPath);  // Load the assembly from the custom path
        //    }

        //    return null;  // If the assembly isn't found, return null (let .NET handle it normally)
        //}
    }
}