using System;
using System.IO;
using System.Reflection;

public class AssemblyLoader
{

    private readonly static string[] assemblies = ["xpression.net", "xpToolsLib.net"];
    public static void LoadAssemblies()
    {
        if (Environment.GetEnvironmentVariable("HAS_XPRESSION") == "true")
        {
            try
        {
            string gacPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "assembly", "GAC_MSIL");

                foreach (string assembly in assemblies)
                {
                    // Dynamically load the references
                    string? dllPath = FindAssemblyInGAC(gacPath, assembly);
                    if (!string.IsNullOrEmpty(dllPath))
                    {
                        Assembly.LoadFrom(dllPath);
                        Console.WriteLine(assembly + " loaded successfully.");
                    }
                    else
                    {
                        Console.WriteLine(assembly + " failed to be loaded");
                    }
                }

                //    // Dynamically load xpression.net.dll
                //    string? xpressionDllPath = FindAssemblyInGAC(gacPath, "xpression.net");
                //if (!string.IsNullOrEmpty(xpressionDllPath))
                //{
                //    Assembly.LoadFrom(xpressionDllPath);
                //    Console.WriteLine("xpression.net loaded successfully.");
                //}
                //else
                //{
                //    Console.WriteLine("xpression.net failed to be loaded");
                //}

                //// Dynamically load xpToolsLib.net.dll
                //string? xpToolsLibDllPath = FindAssemblyInGAC(gacPath, "xpToolsLib.net");
                //if (!string.IsNullOrEmpty(xpToolsLibDllPath))
                //{
                //    Assembly.LoadFrom(xpToolsLibDllPath);
                //    Console.WriteLine("xpToolsLib.net loaded successfully.");
                //}
                //else
                //{
                //    Console.WriteLine("xpToolsLib.net failed to be loaded");
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading assemblies: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("HAS_XPRESSION is not set. Assemblies will not be loaded.");
        }
    }

    private static string? FindAssemblyInGAC(string gacPath, string assemblyFolderName)
    {
        // Target the specific folder for the assembly (e.g., xpression.net or xpToolsLib.net)
        string assemblyFolderPath = Path.Combine(gacPath, assemblyFolderName);

        if (Directory.Exists(assemblyFolderPath))
        {
            // Look for the .dll in the versioned subfolder
            var directories = Directory.GetDirectories(assemblyFolderPath);
            foreach (var directory in directories)
            {
                string assemblyPath = Path.Combine(directory, $"{assemblyFolderName}.dll");
                if (File.Exists(assemblyPath))
                {
                    return assemblyPath;
                }
            }
        }
        return null;
    }
}
