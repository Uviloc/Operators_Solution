using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatorsSolution.Common
{
    public class CommonFunctions
    {
        /// <summary>
        /// Common function to trigger a message box and highlight the control at fault.
        /// </summary>
        /// <param name = "control">The control to highlight.</param>
        /// <param name = "message">The message to display in the message box.</param>
        public static void ControlWarning(Control control, string message)
        {
            Color originalColor = control.BackColor;
            control.BackColor = Color.Red;
            MessageBox.Show(message);
            control.BackColor = originalColor;
        }

        //public static List<T> GetControlsByType<T>(Form form)
        //{
        //    List<T> controls = [];
        //    foreach (Control control in form.Controls)
        //    {
        //        if (control is T matchingControl) controls.Add(matchingControl);
        //    }
        //    return controls;
        //}
    }

    public enum GraphicsSoftware
    {
        XPression,
        CasparCG,
        vMix
    }
}
