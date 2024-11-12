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

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(Button))]
    public class ModuleLoader : Control
    {
        #region >----------------- Add properties: ---------------------
        // Panel
        [Category(".Operation"),
        Description("The panel control where the loaded forms will be displayed.")]
        public Panel? FormModuleDisplay { get; set; }

        // TreeView
        [Category(".Operation"),
        Description("The treeview control where the forms will be loaded into.")]
        public TreeView? TreeviewExplorer { get; set; }
        #endregion

        #region >----------------- LoadModules: ---------------------
        public void LoadModules()
        {
            if (FormModuleDisplay == null || TreeviewExplorer == null) return;

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
                        AddToTreeView(moduleForm, TreeviewExplorer);
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
        private void AddToTreeView(IModuleForm moduleForm, TreeView treeviewMenu)
        {
            TreeNode node = new(moduleForm.FormName) // Use the form's name as the node text
            {
                Tag = moduleForm // Store the plugin form in the Tag property for easy access later
            };
            treeviewMenu.Nodes.Add(node);

            treeviewMenu.NodeMouseDoubleClick += TreeViewModules_NodeMouseDoubleClick; // TEMPORARY CREATE A TREEVIEW CONTROL TO HAVE THIS SET INSTEAD
        }
        #endregion

        #region >----------------- NodeMouseClickEvent: ---------------------
        // MAKE THIS FUNCTION WORK WITH BOTH DATABASE AND FORMS????
        private void TreeViewModules_NodeMouseDoubleClick(object? sender, TreeNodeMouseClickEventArgs e)
        {
            if (FormModuleDisplay == null) return;

            if (e.Node.Tag is IModuleForm pluginForm)
            {
                // Get the form from the IModuleForm instance
                Form form = pluginForm.GetForm();

                // Set the form to be a child inside the InnerPannel
                form.TopLevel = false;                // This makes the form not open as a separate window
                form.FormBorderStyle = FormBorderStyle.None; // Remove the border for a seamless look
                form.Dock = DockStyle.Fill;            // Make the form fill the InnerPannel

                // Clear the InnerPannel to ensure no other controls are blocking the new form
                FormModuleDisplay.Controls.Clear();

                // Add the form to the InnerPannel's Controls collection
                FormModuleDisplay.Controls.Add(form);

                // Show the form inside the panel
                form.Show();
            }
        }
        #endregion
    }
}