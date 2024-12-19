using OperatorsSolution.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Console = System.Diagnostics.Debug;

namespace OperatorsSolution
{
    public partial class FormSettings : UserControl
    {
        public required IFormPlugin LinkedForm { get; set; }
        public FormSettings()
        {
            InitializeComponent();
        }

        #region >----------------- Settings: ---------------------
        public void InitializeSettings()
        {
            ApplicationSettingsBase settings = LinkedForm.ApplicationSettings;

            // Graphics Software:
            GraphicsSoftwareOption.DataSource = GraphicsSoftwareRegistry.ExistingGraphicsSoftware;
            //GraphicsSoftwareOption.SelectedItem = settings["GraphicsSoftwareInfo"] as GraphicsSoftwareInfo ?? null;
            //GraphicsSoftwareOption.SelectedItem = settings["GraphicsSoftwareInfo"] is GraphicsSoftwareInfo info ? info : GraphicsSoftwareOption.SelectedItem;
            //GraphicsSoftwareOption.SelectedItem = settings["GraphicsSoftwareInfo"] as GraphicsSoftwareInfo;
            //GraphicsSoftwareOption.SelectedText = GraphicsSoftwareOption.SelectedItem == null ? "Graphics Software" : string.Empty;
            Console.WriteLine(GraphicsSoftwareOption.DataSource);

            if (settings["GraphicsSoftwareInfo"] is GraphicsSoftwareInfo info && !string.IsNullOrEmpty(info.GraphicsProgramName))
            {
                Console.WriteLine(info.GraphicsSoftwareClassName);
                GraphicsSoftwareOption.SelectedItem = info;
                GraphicsSoftwareInfo? info2 = GraphicsSoftwareOption.SelectedItem as GraphicsSoftwareInfo;
                Console.WriteLine(info2?.GraphicsSoftwareClassName);
            }
            else
            {
                GraphicsSoftwareOption.SelectedItem = null;
                GraphicsSoftwareOption.SelectedText = "Graphics Software";
            }




            // Project file:
            if (settings["ProjectFile"] is not string projectFile)
                return;

            if (!string.IsNullOrWhiteSpace(projectFile))
            {
                string projectName = projectFile.Split('\\').Last();
                projectFileTextBox.Text = projectName;
                ToolTip.SetToolTip(projectFileTextBox, projectFile);
            }
        }

        private void SaveGraphicsSoftwareOption(object sender, EventArgs e)
        {
            // Set the 'graphicsSoftware' setting to the selected ComboBox item
            if (GraphicsSoftwareOption.SelectedItem is not GraphicsSoftwareInfo selectedSoftwareInfo)
                return;
            
            ApplicationSettingsBase settings = LinkedForm.ApplicationSettings;
            settings["GraphicsSoftwareInfo"] = selectedSoftwareInfo;
            settings.Save();

            Console.WriteLine(LinkedForm.FormName + "  |  " + LinkedForm.ApplicationSettings["GraphicsSoftwareInfo"]);
            string settingsPath = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
            Console.WriteLine($"Settings file location: {settingsPath}");
        }

        private void ProjectSelection(object sender, EventArgs e)
        {
            //ApplicationSettingsBase settings = LinkedForm.ApplicationSettings;
            //if (settings["ProjectFile"] is not string projectFile)
            //    return;

            //if (sender is not TextBox textBox) return;

            //OpenFileDialog openFileDialog = new();

            //switch (LinkedForm.GraphicsSoftware)
            //{
            //    case GraphicsSoftware.XPression:
            //        openFileDialog.Filter = "XPression files (*.xpf;*.xpp)|*.xpf;*.xpp";                // SHOULD NOT DEPEND ON THIS, HAVE THIS INFO IN ENUM?
            //        break;
            //    case GraphicsSoftware.CasparCG:
            //        //openFileDialog.Filter = "CasparCG files (*.;*.)";
            //        break;
            //    case GraphicsSoftware.vMix:
            //        //openFileDialog.Filter = "vMix files (*.;*.)";
            //        break;
            //}

            //DialogResult result = openFileDialog.ShowDialog();
            //if (result == DialogResult.OK)
            //{
            //    string file = openFileDialog.FileName;
            //    try
            //    {
            //        string projectName = file.Split('\\').Last();
            //        textBox.Text = projectName;
            //        ToolTip.SetToolTip(projectFileTextBox, file);
            //        LinkedForm.ProjectFile = file;
            //        LinkedForm.SaveSettings();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Failed setting project file: " + ex);
            //    }
            //}
        }

        private void RemoveProjectFileRef(object sender, EventArgs e)
        {
            //projectFileTextBox.Text = null;
            //ToolTip.SetToolTip(projectFileTextBox, "");
            //LinkedForm.ProjectFile = string.Empty;
            //LinkedForm.SaveSettings();
        }
        #endregion

        #region >----------------- Open project file: ---------------------
        private void OpenProject(object? sender, EventArgs e)               // CASPAR NEEDS SERVER TO OPEN FIRST
        {
            //string? projectFilePath = LinkedForm.ProjectFile;

            //if (string.IsNullOrWhiteSpace(projectFilePath))
            //{
            //    MessageBox.Show("There was no selected project. (Project settings>Project file)");
            //    return;
            //}

            ////string fileName = projectFilePath.Split('\\').Last();

            //System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            //{
            //    FileName = projectFilePath,
            //    UseShellExecute = true
            //});
        }
        #endregion

        //public void HidePopupOnOutsideClick(object sender, MouseEventArgs e)
        //{
        //    if (this != null && this.Visible && !this.Bounds.Contains(e.Location))
        //    {
        //        this.Visible = false;
        //        this.MouseDown -= HidePopupOnOutsideClick; // Unsubscribe after hiding
        //    }
        //}
    }
}
