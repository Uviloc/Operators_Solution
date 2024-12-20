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
using OperatorsSolution;
using System.Configuration;

namespace TestModule2
{
    public partial class Test_Module2 : CustomForm, IFormPlugin
    {
        public string FormName => "Some other name just to see";
        public ApplicationSettingsBase ApplicationSettings => Settings.Default;

        //public GraphicsSoftwareInfo? GraphicsSoftwareInfo { get; set; }                 //REMOVE
        //public string? ProjectFile { get; set; }                 //REMOVE

        public Form GetForm()
        {
            return this;
        }


        public Test_Module2()
        {
            InitializeComponent();
            //InitializeSettings();
        }

        //public void InitializeSettings()
        //{
        //    //GraphicsSoftwareInfo = Settings.Default.GraphicsSoftwareInfo;
        //    //ProjectFile = Settings.Default.ProjectFile;
        //}

        //public void SaveSettings()
        //{
        //    //Settings.Default.GraphicsSoftwareInfo = GraphicsSoftwareInfo;
        //    //Settings.Default.ProjectFile = ProjectFile;
        //    Settings.Default.Save();
        //}
    }
}
