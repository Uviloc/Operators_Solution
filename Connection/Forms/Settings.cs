using Microsoft.VisualBasic;
using OperatorsSolution.Common;
using OperatorsSolution.Program;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OperatorsSolution
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            InitializeSettings();
        }


        private void InitializeSettings()
        {
            // ComboBox:
            GraphicsSoftwareOption.DataSource = Enum.GetValues(typeof(GraphicsSoftware));
            GraphicsSoftwareOption.SelectedItem = Properties.Settings.Default.GraphicsSoftware;

            string projectFile = Properties.Settings.Default.ProjectFile;
            if (!string.IsNullOrWhiteSpace(projectFile))
            {
                string projectName = projectFile.Split('\\').Last();
                ProjectFile.Text = projectName;
            }
            else ProjectFile.Text = "Select project file";
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            // Set the 'graphicsSoftware' setting to the selected ComboBox item
            if (GraphicsSoftwareOption.SelectedItem is not null and GraphicsSoftware selectedSoftware)
            {
                Properties.Settings.Default.GraphicsSoftware = selectedSoftware;
                Properties.Settings.Default.Save();
            }
            this.Close();
        }

        private void ProjectSelection(object sender, EventArgs e)
        {
            if (sender is not TextBox) return;
            TextBox textBox = (TextBox)sender;

            OpenFileDialog openFileDialog = new();

            switch (Properties.Settings.Default.GraphicsSoftware)
            {
                case GraphicsSoftware.XPression:
                    openFileDialog.Filter = "XPression files (*.xpf;*.xpp)|*.xpf;*.xpp";
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
                    Properties.Settings.Default.ProjectFile = file;
                    Properties.Settings.Default.Save();
                }
                catch
                {
                }
            }
        }

        private void RemoveProjectFileRef(object sender, EventArgs e)
        {
            ProjectFile.Text = "Select project file";
            Properties.Settings.Default.ProjectFile = null;
        }
    }
}

//namespace OperatorsSolution.Properties
//{
//    //[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
//    //[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.11.0.0")]
//    public sealed partial class Settings
//    {
//        //private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));

//        //public static Settings Default
//        //{
//        //    get
//        //    {
//        //        return defaultInstance;
//        //    }
//        //}


//        [global::System.Configuration.UserScopedSettingAttribute()]
//        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
//        [global::System.Configuration.DefaultSettingValueAttribute("")]
//        public GraphicsSoftware GraphicsSoftware
//        {
//            get
//            {
//                return ((GraphicsSoftware)(this["GraphicsSoftware"]));
//            }
//            set
//            {
//                this["GraphicsSoftware"] = value;
//            }
//        }
//    }
//}