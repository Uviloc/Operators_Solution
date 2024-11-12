using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatorsSolution
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
