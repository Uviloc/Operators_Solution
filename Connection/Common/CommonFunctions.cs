using OperatorsSolution.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Console = System.Diagnostics.Debug;

namespace OperatorsSolution.Common
{
    #region >----------------- Interfaces: ---------------------
    public interface IFormPlugin
    {
        Form GetForm();
        void SaveSettings();
        string FormName { get; }
        GraphicsSoftware GraphicsSoftware { get; set; }
        string? ProjectFile { get; set; }
    }

    public interface IStylePlugin
    {
        // All style parts needed like: backcolor, forecolor, etc
        string StyleName { get; }
    }

    public interface IGraphicProgram
    {
        GraphicsSoftwareInfo GraphicsSoftwareInfo { get; }

        static abstract void DisplayPreview(object? sender, PictureBox previewBox);
        static abstract void RemovePreview(PictureBox previewBox);
        static abstract void TriggerClip(OperatorButton operatorButton, int clipIndex);
    }
    #endregion

    #region >----------------- Types and Graphics Software: ---------------------
    public enum GraphicsSoftware
    {
        XPression,
        CasparCG,
        vMix
    }

    public class GraphicsSoftwareInfo(GraphicsSoftware graphicsSoftware, string graphicsProgramName, string fileExtension)
    {
        public GraphicsSoftware GraphicsSoftware { get; set; } = graphicsSoftware;
        public string GraphicsProgramName { get; set; } = graphicsProgramName;
        public string FileExtension { get; set; } = fileExtension;
    }


    public enum PluginType
    {
        Interfaces,
        //Graphics_Program_Functions,
        Databases,
        Visual_Styles
    }

    //public class PluginTypeInfo(PluginType pluginType, string folderName)
    //{
    //    public PluginType PluginType { get; set; } = pluginType;
    //    public string FolderName { get; set; } = folderName;
    //}

    //public static class PluginTypeRegistry
    //{
    //    public static readonly Dictionary<PluginType, PluginTypeInfo> PluginMappings = new()
    //    {
    //        { PluginType.Interfaces, new PluginTypeInfo(PluginType.Interfaces, "Forms") },
    //        { PluginType.Visual_Styles, new PluginTypeInfo(PluginType.Visual_Styles, "Visual Styles") },
    //        { PluginType.Graphics_Program_Functions, new PluginTypeInfo(PluginType.Graphics_Program_Functions, "Graphics Program Functions") },
    //        { PluginType.Databases, new PluginTypeInfo(PluginType.Databases, "Databases") }
    //    };
    //}
    #endregion

    //public class Assemblies
    //{
    //    public Dictionary<string, Assembly> GraphicsAssemblies { get; set; }
    //}

    #region >----------------- Common Functions: ---------------------
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

        //public static List<T> GetControlsByType<T>(Interfaces form)
        //{
        //    List<T> controls = [];
        //    foreach (Control control in form.Controls)
        //    {
        //        if (control is T matchingControl) controls.Add(matchingControl);
        //    }
        //    return controls;
        //}
    }
    #endregion



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
    //        if (reduceScaleChangesOn is Panel panel && panel.Tag is Interfaces form)
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
