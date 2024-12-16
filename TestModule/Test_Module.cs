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
using static OperatorsSolution.Common.PluginLoader;
using static OperatorsSolution.OpSol_Form;
using static OperatorsSolution.CustomForm;
using static OperatorsSolution.Common.CommonFunctions;
using Console = System.Diagnostics.Debug;
using OperatorsSolution;

namespace TestModule
{
    public partial class Test_Module : CustomForm, IFormPlugin
    {
        public string FormName => "FormNameShort";

        public GraphicsSoftware GraphicsSoftware { get; set; }
        public string? ProjectFile { get; set; }
        
        public Form GetForm()
        {
            return this;
        }


        public Test_Module()
        {
            InitializeComponent();
            InitializeSettings();
        }

        public void InitializeSettings()
        {
            GraphicsSoftware = GUID.Settings.Default.GraphicsProgram;
            ProjectFile = GUID.Settings.Default.ProjectFile;
        }

        public void SaveSettings()
        {
            GUID.Settings.Default.GraphicsProgram = GraphicsSoftware;
            GUID.Settings.Default.ProjectFile = ProjectFile;
            GUID.Settings.Default.Save();
        }
    }
}
