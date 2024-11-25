using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatorsSolution.Common
{
    public interface IModuleForm
    {
        Form GetForm();
        string FormName { get; }
    }


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

    public enum FileLoadingTypes
    {
        Form,
        GraphicsFunctions,
        Databases,
        VisualStyles
    }



    #region >----------------- Custom Animation Function: ---------------------
    //public class Limits
    //{
    //    public int Min { get; set; }
    //    public int Max { get; set; }
    //}

    ///// <summary>
    ///// Changes the material or value given in the objectChange in a scene from XPression.
    ///// </summary>
    ///// <param name="control">The control to be changed.</param>
    ///// <param name="animationStep">The step that the animation should change the control.</param>
    ///// /// <param name="limits">The limits of where the animation should stop.</param>
    //private void AnimateControl(Control control, int animationStep, Limits limits, Func<>? intermediateFunction = null)
    //{
    //    control.Width += animationStep;

    //    // Slow down redraws for form panel to reduce flickering
    //    if (control.Width % 5 == 0 && reduceScaleChangesOn != null)
    //    {
    //        if (reduceScaleChangesOn is Panel panel && panel.Tag is Form form)
    //        {
    //            ScaleFormToFitPanel(form, panel);
    //        }
    //    }

    //    if (ControlPanel.Width >= originalControlPanelWidth)
    //    {
    //        controlPanelOpen = true;
    //        controlPanelChanging = false;
    //        controlPanelAnimationTimer.Stop();
    //    }
    //}
    #endregion
}
