using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Console = System.Diagnostics.Debug;

namespace OperatorsSolution
{
    public class CustomForm : Form
    {
        // Panel
        [Category(".Operation")]
        [Description("The PictureBox control where the previews will be displayed.")]
        [Browsable(true)]
        public PictureBox? PreviewBox { get; set; }

        // SHOULD MAYBE HAVE SETTINGS CONTROL HERE SO THAT EXTERNAL FORMS CAN USE THOSE FUNCTIONS EASILY
    }
}
