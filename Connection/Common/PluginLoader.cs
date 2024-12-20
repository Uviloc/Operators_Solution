using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing.Design;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
using static OperatorsSolution.OpSol_Form;
using Console = System.Diagnostics.Debug;

namespace OperatorsSolution.Common
{
    public class PluginLoader
    {
        private static readonly HashSet<string> LoadedNamespaces = new();

        #region >----------------- Main Process: ---------------------
        public static void LoadPlugins(TreeView operationTreeview, TreeView databaseTreeview)
        {
            foreach (PluginType pluginType in Enum.GetValues<PluginType>())
            {
                string folder = CheckForFolder("Modules\\" + pluginType.ToString().Replace("_", " "));
                LoadFilesFromFolder(folder, pluginType, operationTreeview, databaseTreeview);
            }
        }

        public static string CheckForFolder(string folderName)
        {
            string folder = Path.Combine(Application.StartupPath, folderName);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            return folder;
        }
        #endregion

        #region >----------------- Load Files: ---------------------
        // MAKE FUNCTION BETTER AND LESS INSANE
        public static void LoadFilesFromFolder(string folder, PluginType type, TreeView operationTreeview, TreeView databaseTreeview)
        {
            if (!Directory.Exists(folder)) return;

            string[] filePaths = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories);

            // If the folder is empty, handle the case for .db or form folders
            if (filePaths.Length == 0)
            {
                Console.WriteLine($"[Plugin Loader] No files found in folder: {folder}");
                return;
            }

            // Loop through each filePath in the folder
            foreach (string filePath in filePaths)
            {
                try
                {
                    Assembly? assembly;
                    // Handle each plugin type dynamically
                    switch (type)
                    {
                        case PluginType.Interfaces:
                            if (!Path.GetExtension(filePath).Equals(".dll", StringComparison.OrdinalIgnoreCase)) break;
                            if (!LoadAssemblyPlugin(filePath, out assembly)) break;
                            LoadFormPlugin(assembly, filePath, folder, operationTreeview);
                            break;

                        //case PluginType.Graphics_Program_Functions:
                        //    if (!Path.GetExtension(filePath).Equals(".dll", StringComparison.OrdinalIgnoreCase)) break;
                        //    if (!LoadAssemblyPlugin(filePath, out assembly)) break;
                        //    break;

                        case PluginType.Visual_Styles:
                            if (!Path.GetExtension(filePath).Equals(".dll", StringComparison.OrdinalIgnoreCase)) break;
                            if (!LoadAssemblyPlugin(filePath, out assembly)) break;
                            break;

                        case PluginType.Databases:
                            if (!Path.GetExtension(filePath).Equals(".db", StringComparison.OrdinalIgnoreCase)) break;
                            LoadDatabases(filePath, folder, databaseTreeview);
                            break;

                        default:
                            Console.WriteLine($"[Plugin Loader] Unknown PluginType: {type}");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"[Plugin Loader] Error processing filePath {filePath}: {ex.Message}", "Plugin Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region >----------------- Load Plugins: ---------------------
        private static bool LoadAssemblyPlugin(string file, out Assembly? assembly)
        {
            assembly = null;
            try
            {
                var context = new AssemblyLoadContext(file, isCollectible: true);
                assembly = context.LoadFromAssemblyPath(file);

                // Check for duplicate namespaces
                if (!CheckForDuplicateNamespaces(assembly, file))
                {
                    // Duplicate namespace found, do not load this assembly
                    assembly = null;
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"[Plugin Loader] Error loading assembly {file}: {ex.Message}", "Plugin Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private static void LoadDatabases(string file, string folder, TreeView treeviewExplorer)
        {
            string[] relativePath = Path.GetRelativePath(folder, file).Split('\\');
            AddToTreeView(file, relativePath.Last().Replace(".db", ""), relativePath, treeviewExplorer.Nodes);
            Console.WriteLine($"[Plugin Loader] Database filePath detected: {file}");
        }

        private static void LoadFormPlugin(Assembly? assembly, string file, string? folder = null, TreeView? treeviewExplorer = null)
        {
            if (assembly == null) return;
            // Find all types in the assembly that implement plugins
            foreach (Type type in assembly.GetTypes())
            {
                if (!typeof(IFormPlugin).IsAssignableFrom(type) && type.IsInterface && type.IsAbstract) continue;

                // Create an instance of the plugin
                var typeInstance = Activator.CreateInstance(type);

                if (typeInstance is not IFormPlugin moduleForm) continue;

                // Set custom settings provider for the plugin
                SetPluginSettingsProvider(file, moduleForm);


                if (treeviewExplorer == null || folder == null) continue;
                string[] relativePath = Path.GetRelativePath(folder, file).Split('\\');
                // Add the plugin form to the TreeView
                AddToTreeView(moduleForm, moduleForm.FormName, relativePath, treeviewExplorer.Nodes);
                Console.WriteLine($"[Plugin Loader] .dll filePath detected: {file}");
            }
        }
        #endregion


        //private static void SetPluginSettingsProvider(string pluginFilePath, IFormPlugin moduleForm)
        //{
        //    // Define the plugin settings file path (e.g., based on the plugin's name)
        //    string pluginSettingsFilePath = Path.Combine(Path.GetDirectoryName(pluginFilePath), $"{moduleForm.FormName}_pluginSettings.config");

        //    // Check if the settings file exists for the plugin, if not create it
        //    if (!File.Exists(pluginSettingsFilePath))
        //    {
        //        File.Create(pluginSettingsFilePath).Dispose();
        //    }

        //    // Set the custom settings provider for this plugin
        //    var pluginProvider = new PluginSettingsProvider(pluginSettingsFilePath);
            
        //    settings.Providers.Clear();
        //    settings.Providers.Add(pluginProvider);
        //}

        private static void SetPluginSettingsProvider(string pluginFilePath, IFormPlugin moduleForm)
        {
            string? appDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            if (appDirectory == null)
                return;

            // Define the plugin settings file path
            string pluginSettingsFilePath = Path.Combine(appDirectory, $"{moduleForm.FormName}_pluginSettings.config");

            Console.WriteLine(pluginSettingsFilePath);

            // Check if the settings file exists for the plugin, if not create it
            if (!File.Exists(pluginSettingsFilePath))
            {
                File.Create(pluginSettingsFilePath).Dispose();
            }

            // Create the custom settings provider for the plugin
            var pluginProvider = new PluginSettingsProvider(pluginSettingsFilePath);


            ApplicationSettingsBase settings = moduleForm.ApplicationSettings;
            // Apply the provider to the settings for this plugin
            foreach (SettingsProperty property in settings.Properties)
            {
                property.Provider = pluginProvider;
            }

            settings.Providers.Clear();
            settings.Providers.Add(pluginProvider);
            settings.Reload();
        }

        #region >----------------- Check Duplicate Namespaces: ---------------------
        private static bool CheckForDuplicateNamespaces(Assembly assembly, string filePath)
        {
            try
            {
                var namespacesInAssembly = new HashSet<string>();
                foreach (var type in assembly.GetTypes())
                {
                    if (type.Namespace == null) continue; // Ignore types with no namespace

                    namespacesInAssembly.Add(type.Namespace);
                }

                foreach (var ns in namespacesInAssembly)
                {
                    if (LoadedNamespaces.Contains(ns))
                    {
                        // Log a warning and return false (indicating a duplicate was found)
                        MessageBox.Show($"[Plugin Loader] Duplicate namespace detected:\n{ns} in file: {filePath}.\nPlease change the namespace in this plugin to avoid conflicts.",
                            "Plugin Load Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }

                // No duplicates, add namespaces to the loaded list
                foreach (var ns in namespacesInAssembly)
                {
                    LoadedNamespaces.Add(ns);
                }

                return true; // No duplicates found
            }
            catch (Exception ex)
            {
                MessageBox.Show($"[Plugin Loader] Error checking namespaces in {filePath}: {ex.Message}",
                    "Namespace Check Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion


        #region >----------------- AddToTreeView: ---------------------
        private static TreeNode AddToTreeView(object tag, string nodeName, string[] filePath, TreeNodeCollection nodes)
        {
            // Find an existing node at the current level
            TreeNode? node = nodes
                .Cast<TreeNode>()
                .FirstOrDefault(n => n.Text == filePath[0]);

            // If the node doesn't exist, create and add it
            node ??= new TreeNode(filePath[0]);
            if (!nodes.Contains(node))
            {
                nodes.Add(node);
            }

            // If we've reached the final level, add the form
            if (filePath.Length == 1)
            {
                node.Tag = tag; // Store the plugin form in the Tag property
                node.Text = nodeName; // Set the text to the form name
            }
            else
            {
                // Recursively process the next level
                string[] newFilePath = filePath.Skip(1).ToArray();
                AddToTreeView(tag, nodeName, newFilePath, node.Nodes);
            }

            return node;
        }
        #endregion
    }
}