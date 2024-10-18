using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operators_Solution
{
    public class CommonFunctions
    {
        public static void ControlWarning(Control control, string message)
        {
            Color originalColor = control.BackColor;
            control.BackColor = Color.Red;
            MessageBox.Show(message);
            control.BackColor = originalColor;
        }
    }
}
