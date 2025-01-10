using System;
using System.Reflection;
using System.Configuration;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Collections.Generic;
using Console = System.Diagnostics.Debug;

/// <summary>
/// This namespace contains common classes, interfaces, and functions used across the application, 
/// including graphics software management, plugin definitions, and utility functions.
/// </summary>
namespace OperatorsSolution.Common
{
    #region >----------------- Interfaces: ---------------------
    public interface IFormPlugin
    {
        /// <summary>
        /// Gets the form associated with the plugin.
        /// </summary>
        /// <returns>The form object.</returns>
        Form GetForm();

        // Additional methods and properties related to form settings and graphics software.

        /// <summary>
        /// Gets the application settings used by the plugin.
        /// </summary>
        ApplicationSettingsBase ApplicationSettings { get; }

        /// <summary>
        /// Gets the name of the form.
        /// </summary>
        string FormName { get; }
    }

    public interface IStylePlugin
    {
        /// <summary>
        /// Represents all style properties required for the plugin (e.g., colors, fonts).
        /// </summary>
        string StyleName { get; }
    }

    public interface IGraphicProgram
    {
        /// <summary>
        /// Gets information about the graphics software.
        /// </summary>
        GraphicsSoftwareInfo GraphicsSoftwareInfo { get; }

        /// <summary>
        /// Displays a preview in the specified picture box.
        /// </summary>
        /// <param name="sender">The object triggering the display.</param>
        /// <param name="previewBox">The PictureBox for displaying the preview.</param>
        static abstract void DisplayPreview(object? sender, PictureBox previewBox);

        /// <summary>
        /// Removes the preview from the specified picture box.
        /// </summary>
        /// <param name="previewBox">The PictureBox to clear.</param>
        static abstract void RemovePreview(PictureBox previewBox);

        /// <summary>
        /// Toggles a clip on or off.
        /// </summary>
        /// <param name="sender">The object triggering the toggle.</param>
        /// <param name="isOn">True to turn the clip on; false to turn it off.</param>
        static abstract void ToggleClip(object sender, bool isOn);
    }
    #endregion

    #region >----------------- Types and Graphics Software: ---------------------
    [Serializable]
    [XmlRoot("GraphicsSoftwareInfo")]
    public class GraphicsSoftwareInfo
    {
        /// <summary>
        /// Parameterless constructor for XML serialization.
        /// </summary>
        public GraphicsSoftwareInfo() { }

        /// <summary>
        /// Initializes a new instance of the GraphicsSoftwareInfo class.
        /// </summary>
        /// <param name="graphicsSoftwareClass">The class name of the graphics software.</param>
        /// <param name="graphicsSoftwareName">The display name of the graphics software.</param>
        /// <param name="fileExtension">The associated file extension.</param>
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
        /// <summary>
        /// A list of registered graphics software information.
        /// </summary>
        public static List<GraphicsSoftwareInfo> ExistingGraphicsSoftware { get; } = new();

        /// <summary>
        /// Stores instances of classes implementing IGraphicProgram.
        /// </summary>
        public static readonly Dictionary<string, object> ClassInstances = new();

        /// <summary>
        /// Initializes and registers graphics software programs.
        /// </summary>
        public static void InitializeGraphicsPrograms()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => typeof(IGraphicProgram).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);

            foreach (var type in types)
            {
                if (type.FullName is not string fullTypeName || ClassInstances.ContainsKey(fullTypeName))
                    continue;

                var instance = Activator.CreateInstance(type);
                if (instance == null)
                    continue;

                ClassInstances[fullTypeName] = instance;

                if (type.GetProperty("GraphicsSoftwareInfo")?.GetValue(instance) is GraphicsSoftwareInfo softwareInfo &&
                    !ExistingGraphicsSoftware.Contains(softwareInfo))
                {
                    ExistingGraphicsSoftware.Add(softwareInfo);
                }
            }
        }

        /// <summary>
        /// Gets an existing instance of a class by name.
        /// </summary>
        /// <param name="className">The name of the class.</param>
        /// <returns>The instance if found, or null otherwise.</returns>
        public static object? GetInstance(string className)
        {
            ClassInstances.TryGetValue(className, out var instance);
            return instance;
        }
    }

    public enum PluginType
    {
        Interfaces,
        //Graphics_Program_Functions,
        //API,
        Databases,
        Visual_Styles
    }
    #endregion

    #region >----------------- Common Functions: ---------------------
    public static class CommonFunctions
    {
        /// <summary>
        /// Displays a warning message box and highlights the specified control.
        /// </summary>
        /// <param name="control">The control to highlight.</param>
        /// <param name="message">The warning message to display.</param>
        public static void ControlWarning(Control control, string message)
        {
            Color originalColor = control.BackColor;
            control.BackColor = Color.Red;
            MessageBox.Show(message);
            control.BackColor = originalColor;
        }

        /// <summary>
        /// Highlights a button on hover.
        /// </summary>
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

        #region >----------------- Trigger Method from String: ---------------------
        /// <summary>
        /// Invokes a method by its name on a class, passing the specified parameters.
        /// </summary>
        /// <param name="className">The fully qualified name of the class.</param>
        /// <param name="methodName">The name of the method to invoke.</param>
        /// <param name="parameters">The parameters to pass to the method.</param>
        /// <returns>The result of the method invocation, or null if not successful.</returns>
        public static object? TriggerMethodBasedOnString(this string className, string methodName, object[] parameters)
        {
            Type? classType = Type.GetType(className);
            if (classType == null)
            {
                Console.WriteLine($"Class '{className}' not found.");
                return null;
            }

            // Check for an existing instance in the registry
            object? classInstance = GraphicsSoftwareRegistry.GetInstance(className) ?? Activator.CreateInstance(classType);
            //object? classInstance = GraphicsSoftwareRegistry.GetInstance(className);
            //if (classInstance == null)
            //{
            //    Console.WriteLine($"No existing instance for '{className}'. Creating a new one.");
            //    classInstance = Activator.CreateInstance(classType);
            //    if (classInstance != null)
            //    {
            //        GraphicsSoftwareRegistry.ClassInstances[className] = classInstance; // Cache the new instance
            //    }
            //}

            if (classInstance == null)
            {
                Console.WriteLine($"Could not create an instance of '{className}'.");
                return null;
            }

            MethodInfo? methodInfo = classType.GetMethods()
                .Where(m => m.Name == methodName)
                .FirstOrDefault(m =>
                {
                    var methodParams = m.GetParameters();
                    if (methodParams.Length != parameters.Length) return false;

                    // Ensure all parameter types are compatible
                    for (int i = 0; i < methodParams.Length; i++)
                    {
                        if (parameters[i] != null && !methodParams[i].ParameterType.IsAssignableFrom(parameters[i].GetType()))
                            return false;
                    }
                    return true;
                });

            if (methodInfo == null)
            {
                Console.WriteLine($"Method '{methodName}' not found in class '{className}'.");
                return null;
            }

            // Invoke the method
            try
            {
                return methodInfo.Invoke(classInstance, parameters);
            }
            catch (TargetInvocationException ex)
            {
                Console.WriteLine($"Error during method invocation: {ex.InnerException?.Message}");
                return null;
            }
        }
        #endregion
    }
    #endregion



    #region >----------------- Custom Animation Function: ---------------------  // NOT IMPLEMENTED
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