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
using static OperatorsSolution.Common.CommonFunctions;
using Console = System.Diagnostics.Debug;

namespace TestModule1
{
    public partial class Test_Module : Form, IFormPlugin
    {
        public string FormName => "Test Module Form1";
        public Form GetForm()
        {
            return this;
        }


        public Test_Module()
        {
            InitializeComponent();
        }
    }
}
