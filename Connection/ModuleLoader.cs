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
    public interface IModuleForm
    {
        Form GetForm();
        string FormName { get; }
    }

    public class ModuleLoader
    {
        #region >----------------- LoadModules: ---------------------
        public static void LoadModules(TreeView treeviewExplorer)
        {
            // Get or create "Modules" folder in the app directory
            string moduleFolder = Path.Combine(Application.StartupPath, "Modules");
            if (!Directory.Exists(moduleFolder)) Directory.CreateDirectory(moduleFolder);

            // Loop through each DLL in the Modules folder
            foreach (string file in Directory.GetFiles(moduleFolder, "*.dll"))
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

                        // Add the plugin form to the TreeView
                        AddToTreeView(moduleForm, treeviewExplorer);
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
        private static void AddToTreeView(IModuleForm moduleForm, TreeView treeviewExplorer)
        {
            TreeNode node = new(moduleForm.FormName) // Use the form's name as the node text
            {
                Tag = moduleForm // Store the plugin form in the Tag property for easy access later
            };
            treeviewExplorer.Nodes.Add(node);
        }
        #endregion
    }
}