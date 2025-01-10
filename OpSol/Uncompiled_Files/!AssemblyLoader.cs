using System;
using System.IO;
using System.Reflection;

namespace OperatorsSolution
{
    public static class AssemblyLoader
    {
        private static readonly string[] _searchPaths =
        [
            @"C:\Program Files\XPressionStudioSCE\net",
            @"C:\Windows\assembly\GAC_MSIL"
        ];

        public static void LoadAssemblies()
        {
            // Only set up custom loading in Release mode
            AppDomain.CurrentDomain.AssemblyResolve += LoadFromCustomPaths;
        }

        private static Assembly? LoadFromCustomPaths(object? sender, ResolveEventArgs args)
        {
            var assemblyName = new AssemblyName(args.Name).Name + ".dll";
            Console.WriteLine("Trying to load: " + assemblyName);

            foreach (var path in _searchPaths)
            {
                var assemblyPath = Path.Combine(path, assemblyName);
                Console.WriteLine($"Attempting to load from: {assemblyPath}");
                if (File.Exists(assemblyPath))
                {
                    MessageBox.Show($"Loaded: {assemblyPath}");
                    return Assembly.LoadFrom(assemblyPath);
                }
            }

            Console.WriteLine($"Assembly {assemblyName} not found in custom paths.");
            return null; // Assembly not found
        }
    }
}