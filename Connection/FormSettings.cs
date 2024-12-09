using OperatorsSolution.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            //InitializeSettings();
        }

        #region >----------------- Settings: ---------------------
        public void InitializeSettings()
        {
            // Graphics Software:
            GraphicsSoftwareOption.DataSource = Enum.GetValues(typeof(GraphicsSoftware));
            GraphicsSoftwareOption.SelectedItem = LinkedForm.GraphicsSoftware;

            // Project file:
            string? projectFile = LinkedForm.ProjectFile;
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
            if (GraphicsSoftwareOption.SelectedItem is not null and GraphicsSoftware selectedSoftware)
            {
                LinkedForm.GraphicsSoftware = selectedSoftware;
                LinkedForm.SaveSettings();
                Console.WriteLine(LinkedForm.FormName + "  |  " + LinkedForm.GraphicsSoftware);
            }
        }

        private void ProjectSelection(object sender, EventArgs e)
        {
            if (sender is not TextBox textBox) return;

            OpenFileDialog openFileDialog = new();

            switch (LinkedForm.GraphicsSoftware)
            {
                case GraphicsSoftware.XPression:
                    openFileDialog.Filter = "XPression files (*.xpf;*.xpp)|*.xpf;*.xpp";                // SHOULD NOT DEPEND ON THIS, HAVE THIS INFO IN ENUM?
                    break;
                case GraphicsSoftware.CasparCG:
                    //openFileDialog.Filter = "CasparCG files (*.;*.)";
                    break;
                case GraphicsSoftware.vMix:
                    //openFileDialog.Filter = "vMix files (*.;*.)";
                    break;
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
                    LinkedForm.ProjectFile = file;
                    LinkedForm.SaveSettings();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed setting project file: " + ex);
                }
            }
        }

        private void RemoveProjectFileRef(object sender, EventArgs e)
        {
            projectFileTextBox.Text = null;
            ToolTip.SetToolTip(projectFileTextBox, "");
            LinkedForm.ProjectFile = string.Empty;
            LinkedForm.SaveSettings();
        }
        #endregion

        #region >----------------- Open project file: ---------------------
        private void OpenProject(object? sender, EventArgs e)               // CASPAR NEEDS SERVER TO OPEN FIRST
        {
            string? projectFilePath = LinkedForm.ProjectFile;

            if (string.IsNullOrWhiteSpace(projectFilePath))
            {
                MessageBox.Show("There was no selected project. (Project settings>Project file)");
                return;
            }

            //string fileName = projectFilePath.Split('\\').Last();

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = projectFilePath,
                UseShellExecute = true
            });
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
