using OperatorsSolution;
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
using static OperatorsSolution.ModuleLoader;
using static OperatorsSolution.OpSol_Form;

namespace TestModule
{
    public partial class Test_Module : Form, IModuleForm
    {
        public Test_Module()
        {
            InitializeComponent();
        }
        public string FormName => "Test Module Form";

        public Form GetForm()
        {
            return this;
        }
    }
}
