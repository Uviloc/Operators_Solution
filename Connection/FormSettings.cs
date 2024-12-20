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

        #region >----------------- Initialize Settings: ---------------------
        public void InitializeSettings()
        {
            ApplicationSettingsBase settings = LinkedForm.ApplicationSettings;

            // Graphics Software:
            GraphicsSoftwareOption.DataSource = GraphicsSoftwareRegistry.ExistingGraphicsSoftware;
            if (settings["GraphicsSoftwareInfo"] is GraphicsSoftwareInfo info && !string.IsNullOrEmpty(info.GraphicsProgramName))
            {
                GraphicsSoftwareOption.SelectedItem = info;
            }
            else
            {
                GraphicsSoftwareOption.SelectedItem = null;
                GraphicsSoftwareOption.SelectedText = "Graphics Software";
            }

            // Set event handler after data is loaded in to not have this trigger early
            GraphicsSoftwareOption.SelectedValueChanged += SaveGraphicsSoftwareOption;


            // Project file:
            if (settings["ProjectFile"] is string projectFile && !string.IsNullOrWhiteSpace(projectFile))
            {
                string projectName = projectFile.Split('\\').Last();
                projectFileTextBox.Text = projectName;
                ToolTip.SetToolTip(projectFileTextBox, projectFile);
            }
        }
        #endregion

        #region >----------------- Change Settings: ---------------------

        private void SaveGraphicsSoftwareOption(object? sender, EventArgs e)
        {
            // Set the 'graphicsSoftware' setting to the selected ComboBox item
            if (GraphicsSoftwareOption.SelectedItem is not GraphicsSoftwareInfo selectedSoftwareInfo)
                return;
            
            ApplicationSettingsBase settings = LinkedForm.ApplicationSettings;
            settings["GraphicsSoftwareInfo"] = selectedSoftwareInfo;
            settings.Save();
        }

        private void ProjectSelection(object sender, EventArgs e)
        {
            if (sender is not TextBox textBox)
                return;

            OpenFileDialog openFileDialog = new();

            ApplicationSettingsBase settings = LinkedForm.ApplicationSettings;
            if (settings["GraphicsSoftwareInfo"] is GraphicsSoftwareInfo info && !string.IsNullOrEmpty(info.FileExtension))
            {
                openFileDialog.Filter = info.FileExtension;
            }

            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = openFileDialog.FileName;
                try
                {
                    string projectName = file.Split('\\').Last();
                    textBox.Text = projectName;
                    ToolTip.SetToolTip(projectFileTextBox, file);
                    settings["ProjectFile"] = file;
                    settings.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed setting project file: " + ex);
                }
            }
        }

        private void RemoveProjectFileRef(object sender, EventArgs e)
        {
            ApplicationSettingsBase settings = LinkedForm.ApplicationSettings;
            projectFileTextBox.Text = null;
            ToolTip.SetToolTip(projectFileTextBox, "");
            settings["ProjectFile"] = string.Empty;
            settings.Save();
        }
        #endregion

        #region >----------------- Open project file: ---------------------
        private void OpenProject(object? sender, EventArgs e)               // CASPAR NEEDS SERVER TO OPEN FIRST
        {
            ApplicationSettingsBase settings = LinkedForm.ApplicationSettings;
            //string? projectFilePath = settings["ProjectFile"] as string;

            if (settings["ProjectFile"] is not string projectFile || string.IsNullOrWhiteSpace(projectFile))
            {
                MessageBox.Show("There was no selected project. (Project settings>Project file)");
                return;
            }

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = projectFile,
                UseShellExecute = true
            });
        }
        #endregion
    }
}
