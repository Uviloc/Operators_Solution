using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Operators_Solution
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }


        private void BtnOK_Click(object sender, EventArgs e)
        {
            // Set the 'software' setting to the selected ComboBox item
            if (comboBox1.SelectedItem != null)
            {
                Properties.Settings.Default.GraphicsProgram = comboBox1.SelectedItem.ToString();
                Properties.Settings.Default.Save();  // Save the setting
            }
            this.Close();  // Close the settings form
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedItem = Properties.Settings.Default.GraphicsProgram;
        }
    }
}
