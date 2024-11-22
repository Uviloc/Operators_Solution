using OperatorsSolution.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static OperatorsSolution.OpSol_Form;

namespace OperatorsSolution
{
    public class ModuleLoader
    {
        #region >----------------- LoadModules: ---------------------
        public static void LoadModules(TreeView treeviewExplorer)
        {
            // Get or create "Modules" folder in the app directory
            string moduleFolder = Path.Combine(Application.StartupPath, "Modules");
            if (!Directory.Exists(moduleFolder)) Directory.CreateDirectory(moduleFolder);

            // Loop through each DLL in the Modules folder
            foreach (string file in Directory.GetFiles(moduleFolder, "*.dll", SearchOption.AllDirectories))
            {
                try
                {
                    // Load the assembly
                    Assembly assembly = Assembly.LoadFrom(file);

                    // Find all types in the assembly that implement IModuleForm
                    foreach (Type type in assembly.GetTypes())
                    {
                        if (!typeof(IModuleForm).IsAssignableFrom(type) && type.IsInterface && type.IsAbstract) return;

                        // Create an instance of the module form
                        var typeInstance = Activator.CreateInstance(type);
                        if (typeInstance == null) return;
                        IModuleForm moduleForm = (IModuleForm)typeInstance;

                        if (moduleForm == null) return;

                        string[] relativePath = Path.GetRelativePath(moduleFolder, file).Split('\\');

                        // Add the plugin form to the TreeView
                        AddToTreeView(moduleForm, relativePath, treeviewExplorer.Nodes);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading plugin {file}: {ex.Message}", "Plugin Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region >----------------- AddToTreeView: ---------------------
        //private static TreeNode AddToTreeView(IModuleForm moduleForm, string[] filePath, TreeView? treeviewExplorer = null)
        //{
        //    TreeNode? node = treeviewExplorer?.Nodes
        //        .Cast<TreeNode>()
        //        .FirstOrDefault(n => n.Text == filePath[0]);
        //    if (filePath.Length == 1)
        //    {
        //        // Store the plugin form in the Tag property for easy access later
        //        node = new(moduleForm.FormName) { Tag = moduleForm };
        //    }
        //    else
        //    {
        //        node ??= new TreeNode(filePath[0]);
        //        string[] newFilePath = filePath.Skip(1).ToArray();
        //        node.Nodes.Add(AddToTreeView(moduleForm, newFilePath));
        //    }
        //    treeviewExplorer?.Nodes.Add(node);
        //    return node;
        //}

        private static TreeNode AddToTreeView(IModuleForm moduleForm, string[] filePath, TreeNodeCollection nodes)
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
                node.Tag = moduleForm; // Store the plugin form in the Tag property
                node.Text = moduleForm.FormName; // Set the text to the form name
            }
            else
            {
                // Recursively process the next level
                string[] newFilePath = filePath.Skip(1).ToArray();
                AddToTreeView(moduleForm, newFilePath, node.Nodes);
            }

            return node;
        }
        #endregion
    }
}