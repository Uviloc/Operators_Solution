using OperatorsSolution.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Console = System.Diagnostics.Debug;
using System.Configuration;
using System.Xml.Serialization;

namespace OperatorsSolution.Common
{
    #region >----------------- Interfaces: ---------------------
    public interface IFormPlugin
    {
        Form GetForm();
        //void SaveSettings();
        string FormName { get; }
        //GraphicsSoftwareInfo GraphicsSoftware { get; set; }
        //string? ProjectFile { get; set; }
        ApplicationSettingsBase ApplicationSettings { get; }
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


        //static List<GraphicsSoftwareInfo> ExistingGraphicsSoftware { get; } = [];
        //static void Register(GraphicsSoftwareInfo graphicsSoftwareInfo)
        //{
        //    Console.WriteLine("Registered");
        //    if (!ExistingGraphicsSoftware.Contains(graphicsSoftwareInfo))
        //        ExistingGraphicsSoftware.Add(graphicsSoftwareInfo);
        //}
    }
    #endregion

    #region >----------------- Types and Graphics Software: ---------------------
    //public enum GraphicsSoftware
    //{
    //    XPression,
    //    CasparCG,
    //    vMix
    //}

    //[Serializable]
    //public class GraphicsSoftwareInfo(string graphicsSoftwareClass, string graphicsSoftwareName, string fileExtension)
    //{
    //    public string GraphicsSoftwareClassName { get; set; } = graphicsSoftwareClass;
    //    public string GraphicsProgramName { get; set; } = graphicsSoftwareName;
    //    public string FileExtension { get; set; } = fileExtension;

    //    public override string ToString() => GraphicsProgramName;
    //}

    //[Serializable]
    //public class GraphicsSoftwareInfo
    //{
    //    // Parameterless constructor for XML serialization
    //    public GraphicsSoftwareInfo() { }

    //    public GraphicsSoftwareInfo(string graphicsSoftwareClass, string graphicsSoftwareName, string fileExtension)
    //    {
    //        GraphicsSoftwareClassName = graphicsSoftwareClass;
    //        GraphicsProgramName = graphicsSoftwareName;
    //        FileExtension = fileExtension;
    //    }

    //    [XmlElement]
    //    public string? GraphicsSoftwareClassName { get; set; }

    //    [XmlElement]
    //    public string? GraphicsProgramName { get; set; }

    //    [XmlElement]
    //    public string? FileExtension { get; set; }

    //    public override string ToString() => GraphicsProgramName ?? string.Empty;

    //    public override bool Equals(object? obj)
    //    {
    //        if (obj is not GraphicsSoftwareInfo other)
    //            return false;

    //        return GraphicsSoftwareClassName == other.GraphicsSoftwareClassName &&
    //               GraphicsProgramName == other.GraphicsProgramName &&
    //               FileExtension == other.FileExtension;
    //    }

    //    public override int GetHashCode()
    //    {
    //        return HashCode.Combine(GraphicsSoftwareClassName, GraphicsProgramName, FileExtension);
    //    }
    //}

    //[Serializable]
    //[XmlRoot("GraphicsSoftwareInfo")]
    //public class GraphicsSoftwareInfo
    //{
    //    // Parameterless constructor for XML serialization
    //    public GraphicsSoftwareInfo() { }

    //    public GraphicsSoftwareInfo(string graphicsSoftwareClass, string graphicsSoftwareName, string fileExtension)
    //    {
    //        GraphicsSoftwareClassName = graphicsSoftwareClass;
    //        GraphicsProgramName = graphicsSoftwareName;
    //        FileExtension = fileExtension;
    //    }

    //    [XmlElement]
    //    public string? GraphicsSoftwareClassName { get; set; }

    //    [XmlElement]
    //    public string? GraphicsProgramName { get; set; }

    //    [XmlElement]
    //    public string? FileExtension { get; set; }

    //    public override string ToString() => GraphicsProgramName ?? string.Empty;

    //    public override bool Equals(object? obj)
    //    {
    //        if (obj is not GraphicsSoftwareInfo other)
    //            return false;

    //        return GraphicsSoftwareClassName == other.GraphicsSoftwareClassName &&
    //               GraphicsProgramName == other.GraphicsProgramName &&
    //               FileExtension == other.FileExtension;
    //    }

    //    public override int GetHashCode()
    //    {
    //        return HashCode.Combine(GraphicsSoftwareClassName, GraphicsProgramName, FileExtension);
    //    }
    //}
    [Serializable]
    [XmlRoot("GraphicsSoftwareInfo")]
    public class GraphicsSoftwareInfo
    {
        // Parameterless constructor for XML serialization
        public GraphicsSoftwareInfo() { }

        public GraphicsSoftwareInfo(string graphicsSoftwareClass, string graphicsSoftwareName, string fileExtension)
        {
            GraphicsSoftwareClassName = graphicsSoftwareClass;
            GraphicsProgramName = graphicsSoftwareName;
            FileExtension = fileExtension;
        }

        [XmlElement]
        public string? GraphicsSoftwareClassName { get; set; }

        [XmlElement]
        public string? GraphicsProgramName { get; set; }

        [XmlElement]
        public string? FileExtension { get; set; }

        public override string ToString() => GraphicsProgramName ?? string.Empty;
    }



    public static class GraphicsSoftwareRegistry
    {
        // This list will store all GraphicsSoftwareInfo objects from classes implementing IGraphicProgram
        public static List<GraphicsSoftwareInfo> ExistingGraphicsSoftware { get; } = [];

        public static void InitializeGraphicsPrograms()
        {
            // Get all types that implement IGraphicProgram
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => typeof(IGraphicProgram).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);

            foreach (var type in types)
            {
                // Ensure the type is instantiated (if necessary)
                var instance = Activator.CreateInstance(type);

                // Get the GraphicsSoftwareInfo property from the class
                //GraphicsSoftwareInfo graphicsSoftwareInfoProperty = type.GetProperty("GraphicsSoftwareInfo");
                GraphicsSoftwareInfo? graphicsSoftwareInfoProperty = type.GetProperty("GraphicsSoftwareInfo")?.GetValue(instance) as GraphicsSoftwareInfo;
                //if (type is not IGraphicProgram graphicsProgram)
                //    return;

                //GraphicsSoftwareInfo softwareInfo = graphicsProgram.GraphicsSoftwareInfo;

                if (graphicsSoftwareInfoProperty != null && !ExistingGraphicsSoftware.Contains(graphicsSoftwareInfoProperty))
                {
                    ExistingGraphicsSoftware.Add(graphicsSoftwareInfoProperty);
                }
            }
        }
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

        #region >----------------- Highlight button hover: ---------------------
        public static void ButtonHighlight(object? sender, EventArgs e)
        {
            if (sender is not Control control) return;
            Color originalColor = control.BackColor;

            void mouseLeaveHandler(object? s, EventArgs args)
            {
                control.BackColor = originalColor;

                // Unsubscribe after handling the event
                if (s is Control ctrl)
                {
                    ctrl.MouseLeave -= mouseLeaveHandler;
                }
            }

            control.MouseLeave += mouseLeaveHandler;

            // Change the background color
            control.BackColor = Color.FromArgb(
                Math.Min(originalColor.R + 30, 255),
                Math.Min(originalColor.G + 30, 255),
                Math.Min(originalColor.B + 30, 255)
            );
        }
        #endregion
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
