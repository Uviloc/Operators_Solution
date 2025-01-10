using OperatorsSolution.Common;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using Console = System.Diagnostics.Debug;

namespace OperatorsSolution
{
    /// <summary>
    /// Represents a user control for managing application settings, including graphics software options and project file configuration.
    /// </summary>
    public partial class FormSettings : UserControl
    {
        /// <summary>
        /// Gets or sets the linked form plugin that provides application-specific settings.
        /// </summary>
        public required IFormPlugin LinkedForm { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormSettings"/> class.
        /// </summary>
        public FormSettings()
        {
            InitializeComponent();
        }

        #region >----------------- Initialize Settings: ---------------------

        /// <summary>
        /// Initializes the settings for the form, including graphics software options and project file configuration.
        /// </summary>
        public void InitializeSettings()
        {
            ApplicationSettingsBase settings = LinkedForm.ApplicationSettings;

            // Graphics Software:
            GraphicsSoftwareOption.DataSource = GraphicsSoftwareRegistry.ExistingGraphicsSoftware;

            if (settings["GraphicsSoftwareInfo"] is GraphicsSoftwareInfo info &&
                !string.IsNullOrEmpty(info.GraphicsProgramName))
            {
                GraphicsSoftwareOption.SelectedItem = info;
            }
            else
            {
                GraphicsSoftwareOption.SelectedItem = null;
                GraphicsSoftwareOption.SelectedText = "Graphics Software";
            }

            // Set event handler after data is loaded to avoid triggering early
            GraphicsSoftwareOption.SelectedValueChanged += SaveGraphicsSoftwareOption;

            // Project file:
            if (settings["ProjectFile"] is string projectFile &&
                !string.IsNullOrWhiteSpace(projectFile))
            {
                string projectName = projectFile.Split('\\').Last();
                projectFileTextBox.Text = projectName;
                ToolTip.SetToolTip(projectFileTextBox, projectFile);
            }
        }

        #endregion

        #region >----------------- Change Settings: ---------------------

        /// <summary>
        /// Saves the selected graphics software option to the application settings.
        /// </summary>
        private void SaveGraphicsSoftwareOption(object? sender, EventArgs e)
        {
            if (GraphicsSoftwareOption.SelectedItem is not GraphicsSoftwareInfo selectedSoftwareInfo)
                return;

            ApplicationSettingsBase settings = LinkedForm.ApplicationSettings;
            settings["GraphicsSoftwareInfo"] = selectedSoftwareInfo;
            settings.Save();
        }

        /// <summary>
        /// Opens a file dialog to select a project file and updates the application settings.
        /// </summary>
        private void ProjectSelection(object sender, EventArgs e)
        {
            if (sender is not TextBox textBox)
                return;

            OpenFileDialog openFileDialog = new();
            ApplicationSettingsBase settings = LinkedForm.ApplicationSettings;

            if (settings["GraphicsSoftwareInfo"] is GraphicsSoftwareInfo info &&
                !string.IsNullOrEmpty(info.FileExtension))
            {
                openFileDialog.Filter = info.FileExtension;
            }

            DialogResult result = openFileDialog.ShowDialog();

            if (result != DialogResult.OK)
                return;

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
                MessageBox.Show($"Failed setting project file: {ex.Message}");
            }
        }

        /// <summary>
        /// Removes the current project file reference from the application settings.
        /// </summary>
        private void RemoveProjectFileRef(object sender, EventArgs e)
        {
            ApplicationSettingsBase settings = LinkedForm.ApplicationSettings;
            projectFileTextBox.Text = string.Empty;
            ToolTip.SetToolTip(projectFileTextBox, string.Empty);
            settings["ProjectFile"] = string.Empty;
            settings.Save();
        }
        #endregion

        #region >----------------- Open Project File: ---------------------

        /// <summary>
        /// Opens the currently selected project file using the system's default program.
        /// </summary>
        private void OpenProject(object? sender, EventArgs e)                                                   // CASPAR NEEDS SERVER TO OPEN FIRST
        {
            ApplicationSettingsBase settings = LinkedForm.ApplicationSettings;

            if (settings["ProjectFile"] is not string projectFile || string.IsNullOrWhiteSpace(projectFile))
            {
                MessageBox.Show("There was no selected project. (Project settings > Project file)");
                return;
            }

            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = projectFile,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open project file: {ex.Message}");
            }
        }

        #endregion
    }
}
