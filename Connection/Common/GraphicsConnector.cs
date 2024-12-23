using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Console = System.Diagnostics.Debug;

#if HAS_XPRESSION
using XPression;
#endif

#if HAS_CASPARCG
using CasparCG;
#endif

#if HAS_VMIX
using vMix;
#endif

using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using OperatorsSolution.Program;
using OperatorsSolution.Controls;
using System.Reflection;
using System.Configuration;

namespace OperatorsSolution.Common
{
    /// <summary>
    /// Distribute the given function to the corresponding Graphics Program functions.
    /// The Graphics Program is set in the project settings.
    /// </summary>
    internal class GraphicsConnector
    {
        //public static void TryInvokeFunction(string assemblyPath, string namespaceName, string className, string methodName)
        //{
        //    try
        //    {
        //        // Load the assembly
        //        Assembly assembly = Assembly.LoadFrom(assemblyPath);

        //        // Find the type (class) within the namespace
        //        Type type = assembly.GetType($"{namespaceName}.{className}");
        //        if (type == null)
        //        {
        //            Console.WriteLine($"Class '{className}' in namespace '{namespaceName}' not found.");
        //            return;
        //        }

        //        // Find the method inside the class
        //        MethodInfo method = type.GetMethod(methodName);
        //        if (method == null)
        //        {
        //            Console.WriteLine($"Method '{methodName}' not found in class '{className}'.");
        //            return;
        //        }

        //        // Invoke the method (example without parameters)
        //        object classInstance = Activator.CreateInstance(type);
        //        method.Invoke(classInstance, null); // Pass parameters as needed
        //    }
        //    catch (FileNotFoundException)
        //    {
        //        Console.WriteLine($"Assembly '{assemblyPath}' not found.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error: {ex.Message}");
        //    }
        //}






        public static void TestFunc(Control something)
        {

        }







        /// <summary>
        /// Check to see if XPression is working (mostly used to check if XPression dongle with licence is present).
        /// </summary>
        /// /// <returns>true if succsesfull, false if the XPression is not functioning</returns>
        public static bool IsXPressionDonglePresent()
        {
            try
            {
#if HAS_XPRESSION
                xpEngine XPression = new();
                return true;
#endif
            }
            catch
            {
                MessageBox.Show("XPression Dongle is not connected. Features are disabled.");
            }
            return false;
        }


        #region >----------------- Display Preview: ---------------------
        /// <summary>
        /// Displays a preview of the buttons scene in the given preview box.
        /// </summary>
        /// <param name = "sender">The button with the scene information on it.</param>
        /// <param name = "previewBox">The PictureBox control element where the preview should be displayed.</param>
        public static void DisplayPreview(Control sender, PictureBox previewBox)
        {
            if (sender.FindForm() is not IFormPlugin parentForm)
                return;
            ApplicationSettingsBase settings = parentForm.ApplicationSettings;

            if (settings["GraphicsSoftwareInfo"] is not GraphicsSoftwareInfo info)
                return;
            // USE THIS BELOW FOR GETTING THE CORRECT CLASSES FROM THE CLASS NAME STRING
            //public IGraphicProgram? ResolveGraphicsSoftwareClass()
            //{
            //    var type = Type.GetType(GraphicsSoftwareClassName);
            //    return type != null ? Activator.CreateInstance(type) as IGraphicProgram : null;
            //}





            //switch (info.GraphicsSoftwareClassName)
            //{
            //    case 
            //}


            //            switch (Properties.Settings.Default.GraphicsSoftware)
            //            {
            //                // XPRESSION:
            //                case XPression:
            //#if HAS_XPRESSION   // If XPression reference exists, compile this code
            //                    // Check if Dongle is in computer
            //                    if (!CheckForDongle()) return;
            //                    Graphics_Program_Functions.XPression.DisplayPreview(sender, previewBox);
            //#else
            //                    MessageBox.Show("xpression.net.dll reference could not be found!");
            //#endif
            //                    break;

            //                // CASPARCG:
            //                case GraphicsSoftware.CasparCG:
            //#if HAS_CASPARCG   // If CasparCG reference exists, compile this code
            //                    CasparCG_Functions.DisplayPreview(sender, previewBox);
            //#else
            //                    MessageBox.Show("CasparCG.dll reference could not be found!");
            //#endif
            //                    break;

            //                // VMIX:
            //                case GraphicsSoftware.vMix:
            //#if HAS_VMIX        // If vMix reference exists, compile this code
            //                    VMIX_Functions.DisplayPreview(sender, previewBox);
            //#else
            //                    MessageBox.Show("vMix.dll reference could not be found!");
            //#endif
            //                    break;
            //}
        }
        #endregion

        #region >----------------- Remove Preview: ---------------------
        /// <summary>
        /// Removes the preview in the given preview box.
        /// </summary>
        /// <param name = "previewBox">The PictureBox control element where the preview should be removed.</param>
        public static void RemovePreview(PictureBox previewBox)
        {
//            switch (Properties.Settings.Default.GraphicsSoftware)
//            {
//                // XPRESSION:
//                case GraphicsSoftware.XPression:
//#if HAS_XPRESSION   // If XPression reference exists, compile this code
//                    // Check if Dongle is in computer
//                    if (!CheckForDongle()) return;
//                    Graphics_Program_Functions.XPression.RemovePreview(previewBox);
//#else
//                    MessageBox.Show("xpression.net.dll reference could not be found!");
//#endif
//                    break;

//                // CASPARCG:
//                case GraphicsSoftware.CasparCG:
//#if HAS_CASPARCG   // If CasparCG reference exists, compile this code
//                    CasparCG_Functions.RemovePreview(sender, previewBox);
//#else
//                    MessageBox.Show("CasparCG.dll reference could not be found!");
//#endif
//                    break;

//                // VMIX:
//                case GraphicsSoftware.vMix:
//#if HAS_VMIX        // If vMix reference exists, compile this code
//                    VMIX_Functions.RemovePreview(sender, previewBox);
//#else
//                    MessageBox.Show("vMix.dll reference could not be found!");
//#endif
//                    break;
//            }
        }
        #endregion

        #region >----------------- Trigger clip: ---------------------
        /// <summary>
        /// Plays out the clip at clipIndex given in the operatorButton in XPression.
        /// </summary>
        /// <param name = "operatorButton">The button control that has the clip path list.</param>
        /// <param name = "clipIndex">Which clip to trigger in the clip path list.</param>
        public static void TriggerClip(OperatorButton operatorButton, int clipIndex)
        {
//            switch (Properties.Settings.Default.GraphicsSoftware)
//            {
//                // XPRESSION:
//                case GraphicsSoftware.XPression:
//#if HAS_XPRESSION   // If XPression reference exists, compile this code
//                    // Check if Dongle is in computer
//                    if (!CheckForDongle()) return;
//                    Graphics_Program_Functions.XPression.TriggerClip(operatorButton, clipIndex);
//#else
//                    MessageBox.Show("xpression.net.dll reference could not be found!");
//#endif
//                    break;

//                // CASPARCG:
//                case GraphicsSoftware.CasparCG:
//#if HAS_CASPARCG   // If CasparCG reference exists, compile this code
//                    CasparCG_Functions.RemovePreview(sender, previewBox);
//#else
//                    MessageBox.Show("CasparCG.dll reference could not be found!");
//#endif
//                    break;

//                // VMIX:
//                case GraphicsSoftware.vMix:
//#if HAS_VMIX        // If vMix reference exists, compile this code
//                    VMIX_Functions.RemovePreview(sender, previewBox);
//#else
//                    MessageBox.Show("vMix.dll reference could not be found!");
//#endif
//                    break;
//            }
        }
        #endregion

        // Change text/material
    }
}
