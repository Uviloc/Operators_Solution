using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            InitializeGraphicsProgram();
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

        private void InitializeGraphicsProgram()
        {
            GraphicsSoftwareOption.DataSource = Enum.GetValues(typeof(GraphicsSoftware));
            GraphicsSoftwareOption.SelectedItem = Properties.Settings.Default.GraphicsSoftware;
        }
    }
}
