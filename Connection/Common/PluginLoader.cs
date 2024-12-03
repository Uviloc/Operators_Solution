using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
                    // Handle each plugin type dynamically
                    switch (type)
                    {
                        case PluginType.Interfaces:
                            if (Path.GetExtension(filePath).Equals(".dll", StringComparison.OrdinalIgnoreCase))
                                LoadAssemblyPlugin(filePath, folder, operationTreeview);
                            break;

                        case PluginType.Graphics_Program_Functions:
                            if (Path.GetExtension(filePath).Equals(".dll", StringComparison.OrdinalIgnoreCase))
                                LoadAssemblyPlugin(filePath);
                            break;

                        case PluginType.Visual_Styles:
                            if (Path.GetExtension(filePath).Equals(".dll", StringComparison.OrdinalIgnoreCase))
                            {
                                //LoadAssemblyPlugin(filePath);
                            }
                            break;

                        case PluginType.Databases:
                            if (Path.GetExtension(filePath).Equals(".db", StringComparison.OrdinalIgnoreCase))
                            {
                                LoadDatabases(filePath, folder, databaseTreeview);
                                Console.WriteLine($"[Plugin Loader] Database filePath detected: {filePath}");
                            }
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
        // ALL IN ONE FUNCTION???: CASE FOR DLL > LOAD AS ASSEMBLY, IF DB > RELATIVEPATH, IF DB OR FORM > INTO TREEVIEW, IF STYLE > INTO OPTIONS
        private static void LoadDatabases(string file, string folder, TreeView treeviewExplorer)
        {
            string[] relativePath = Path.GetRelativePath(folder, file).Split('\\');
            AddToTreeView(file, relativePath.Last().Replace(".db", ""), relativePath, treeviewExplorer.Nodes);
        }

        private static void LoadAssemblyPlugin(string file, string? folder = null, TreeView? treeviewExplorer = null)
        {
            try
            {
                var context = new AssemblyLoadContext(file, isCollectible: true);
                Assembly assembly = context.LoadFromAssemblyPath(file);

                // Find all types in the assembly that implement plugins
                foreach (Type type in assembly.GetTypes())
                {
                    if (!typeof(IFormPlugin).IsAssignableFrom(type) && type.IsInterface && type.IsAbstract) continue;

                    // Create an instance of the plugin
                    var typeInstance = Activator.CreateInstance(type);

                    if (typeInstance is not IFormPlugin moduleForm) continue;




                    if (treeviewExplorer == null || folder == null) continue;
                    string[] relativePath = Path.GetRelativePath(folder, file).Split('\\');
                    // Add the plugin form to the TreeView
                    AddToTreeView(moduleForm, moduleForm.FormName, relativePath, treeviewExplorer.Nodes);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"[Plugin Loader] Error loading assembly {file}: {ex.Message}", "Plugin Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region >----------------- AddToTreeView: ---------------------
        private static TreeNode AddToTreeView(object tag, string nodeName, string[] filePath, TreeNodeCollection nodes)
        {
            // If no nodes are provided, use the root nodes
            //nodes ??= treeviewExplorer?.Nodes;

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