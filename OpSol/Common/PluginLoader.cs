using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Reflection;
using System.Runtime.Loader;
using Console = System.Diagnostics.Debug;

namespace OperatorsSolution.Common
{
    /// <summary>
    /// Handles loading plugins from specified directories based on plugin type.
    /// Manages loading assemblies, forms, databases, and ensures no duplicate namespaces.
    /// </summary>
    public class PluginLoader
    {
        private static readonly HashSet<string> LoadedNamespaces = [];

        #region >----------------- Main Process: ---------------------
        /// <summary>
        /// Loads all plugins by iterating through each plugin type and loading files from the corresponding folder.
        /// </summary>
        /// <param name="operationTreeview">TreeView control for loading operation plugins.</param>
        /// <param name="databaseTreeview">TreeView control for loading database plugins.</param>
        public static void LoadPlugins(TreeView operationTreeview, TreeView databaseTreeview)
        {
            foreach (PluginType pluginType in Enum.GetValues<PluginType>())
            {
                string folder = EnsureFolderExists("Modules\\" + pluginType.ToString().Replace("_", " "));
                LoadFilesFromFolder(folder, pluginType, operationTreeview, databaseTreeview);
            }
        }

        /// <summary>
        /// Ensures the folder exists, creating it if necessary.
        /// </summary>
        /// <param name="folderName">The name of the folder to check/create.</param>
        /// <returns>The full path to the folder.</returns>
        private static string EnsureFolderExists(string folderName)
        {
            string folder = Path.Combine(Application.StartupPath, folderName);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            return folder;
        }
        #endregion

        #region >----------------- Load Files: ---------------------
                                                                                                                        // MAKE FUNCTION BETTER AND LESS INSANE
        /// <summary>
        /// Loads files from the specified folder and handles them based on their plugin type.
        /// </summary>
        /// <param name="folder">The path of the folder to load files from.</param>
        /// <param name="type">The type of plugin to load (Interfaces, Visual Styles, Databases).</param>
        /// <param name="operationTreeview">TreeView control for loading operation plugins.</param>
        /// <param name="databaseTreeview">TreeView control for loading database plugins.</param>
        public static void LoadFilesFromFolder(string folder, PluginType type, TreeView operationTreeview, TreeView databaseTreeview)
        {
            if (!Directory.Exists(folder))
                return;

            string[] filePaths = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories);

            // Handle empty folder case
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
                    HandlePluginFile(filePath, folder, type, operationTreeview, databaseTreeview);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"[Plugin Loader] Error processing filePath {filePath}: {ex.Message}", "Plugin Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Handles individual plugin files based on their extension and type.
        /// </summary>
        /// <param name="filePath">The file path of the plugin file.</param>
        /// <param name="folder">The folder name that the plugin resides in.</param>
        /// <param name="type">The type of plugin to load (Interfaces, Visual Styles, Databases).</param>
        /// <param name="operationTreeview">TreeView control for loading operation plugins.</param>
        /// <param name="databaseTreeview">TreeView control for loading database plugins.</param>
        private static void HandlePluginFile(string filePath, string folder, PluginType type, TreeView operationTreeview, TreeView databaseTreeview)
        {
            Assembly? assembly;
            switch (type)
            {
                case PluginType.Interfaces:
                    if (Path.GetExtension(filePath).Equals(".dll", StringComparison.OrdinalIgnoreCase) &&
                        LoadAssemblyPlugin(filePath, out assembly))
                        LoadFormPlugin(assembly, filePath, folder, operationTreeview);
                    break;

                //case PluginType.Graphics_Program_Functions:
                    //if (Path.GetExtension(filePath).Equals(".dll", StringComparison.OrdinalIgnoreCase) &&
                    //    LoadAssemblyPlugin(filePath, out assembly))
                    //    LoadFormPlugin(assembly, filePath, folder, operationTreeview);
                    //break;

                case PluginType.Visual_Styles:
                    //if (Path.GetExtension(filePath).Equals(".dll", StringComparison.OrdinalIgnoreCase) &&
                    //    LoadAssemblyPlugin(filePath, out assembly))
                    //    LoadFormPlugin(assembly, filePath, folder, operationTreeview);
                    //break;

                case PluginType.Databases:
                    if (Path.GetExtension(filePath).Equals(".db", StringComparison.OrdinalIgnoreCase))
                        LoadDatabases(filePath, folder, databaseTreeview);
                    break;

                default:
                    Console.WriteLine($"[Plugin Loader] Unknown PluginType: {type}");
                    break;
            }
        }
        #endregion

        #region >----------------- Load Plugins: ---------------------
        /// <summary>
        /// Loads the assembly from a given file path, ensuring no duplicate namespaces are loaded.
        /// </summary>
        /// <param name="file">The file path of the assembly to load.</param>
        /// <param name="assembly">The loaded assembly (output parameter).</param>
        /// <returns>True if the assembly was loaded successfully; otherwise, false.</returns>
        private static bool LoadAssemblyPlugin(string file, out Assembly? assembly)
        {
            assembly = null;
            try
            {
                var context = new AssemblyLoadContext(file, isCollectible: true);
                assembly = context.LoadFromAssemblyPath(file);

                // Check for duplicate namespaces
                if (CheckForDuplicateNamespaces(assembly, file))
                    return true;

                // Duplicate namespace found, do not load this assembly
                assembly = null;
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"[Plugin Loader] Error loading assembly {file}: {ex.Message}", "Plugin Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Loads a database plugin from the specified file.
        /// </summary>
        /// <param name="file">The file path of the database plugin.</param>
        /// <param name="folder">The folder name that the plugin resides in.</param>
        /// <param name="treeviewExplorer">TreeView control to load the database plugin into.</param>
        private static void LoadDatabases(string file, string folder, TreeView treeviewExplorer)
        {
            string[] relativePath = Path.GetRelativePath(folder, file).Split('\\');
            AddToTreeView(file, relativePath.Last().Replace(".db", ""), relativePath, treeviewExplorer.Nodes);
            Console.WriteLine($"[Plugin Loader] Database filePath detected: {file}");
        }

        /// <summary>
        /// Loads a form plugin from the specified assembly and adds it to the TreeView.
        /// </summary>
        /// <param name="assembly">The assembly containing the form plugin.</param>
        /// <param name="file">The file path of the form plugin.</param>
        /// <param name="folder">The folder name that the plugin resides in. If left null do not load into hierarchy structure.</param>
        /// <param name="treeviewExplorer">TreeView control to load the form plugin into. If left null do not load into hierarchy structure.</param>
        private static void LoadFormPlugin(Assembly? assembly, string file, string? folder = null, TreeView? treeviewExplorer = null)
        {
            if (assembly == null) return;
            // Find all types in the assembly that implement plugins
            foreach (Type type in assembly.GetTypes())
            {
                if (!typeof(IFormPlugin).IsAssignableFrom(type) && type.IsInterface && type.IsAbstract)
                    continue;

                // Create an instance of the plugin
                var typeInstance = Activator.CreateInstance(type);

                if (typeInstance is not IFormPlugin moduleForm)
                    continue;

                // Set custom settings provider for the plugin
                SetPluginSettingsProvider(file, moduleForm);


                if (treeviewExplorer == null || folder == null)
                    continue;

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

        /// <summary>
        /// Sets the custom settings provider for the given plugin form.
        /// </summary>
        /// <param name="pluginFilePath">The file path of the plugin.</param>
        /// <param name="moduleForm">The plugin form instance.</param>
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
                property.Provider = pluginProvider;

            settings.Providers.Clear();
            settings.Providers.Add(pluginProvider);
            settings.Reload();
        }

        #region >----------------- Check Duplicate Namespaces: ---------------------
        /// <summary>
        /// Checks for duplicate namespaces in the given assembly and logs a warning if any are found.
        /// </summary>
        /// <param name="assembly">The assembly to check for duplicate namespaces.</param>
        /// <param name="filePath">The file path of the assembly for logging purposes.</param>
        /// <returns>True if no duplicate namespaces were found, otherwise false.</returns>
        private static bool CheckForDuplicateNamespaces(Assembly assembly, string filePath)
        {
            try
            {
                var namespacesInAssembly = new HashSet<string>();
                foreach (var type in assembly.GetTypes())
                {
                    if (type.Namespace == null) // Ignore types with no namespace
                        continue;

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
                    LoadedNamespaces.Add(ns);

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
        /// <summary>
        /// Adds a node to the TreeView, creating nodes recursively based on the file path.
        /// </summary>
        /// <param name="tag">The object to associate with the TreeNode (usually a plugin form).</param>
        /// <param name="nodeName">The name of the node to display.</param>
        /// <param name="filePath">The path of the file, used to create the hierarchical nodes.</param>
        /// <param name="nodes">The collection of nodes to add to.</param>
        /// <returns>The created or existing TreeNode.</returns>
        private static TreeNode AddToTreeView(object tag, string nodeName, string[] filePath, TreeNodeCollection nodes)
        {
            // Find an existing node at the current level
            TreeNode? node = nodes
                .Cast<TreeNode>()
                .FirstOrDefault(n => n.Text == filePath[0]);

            // If the node doesn't exist, create and add it
            node ??= new TreeNode(filePath[0]);
            if (!nodes.Contains(node))
                nodes.Add(node);

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