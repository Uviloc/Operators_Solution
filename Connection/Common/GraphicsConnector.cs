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
using OperatorsSolution.GraphicsProgramFunctions;
using OperatorsSolution.Program;
using OperatorsSolution.Controls;

namespace OperatorsSolution.Common
{
    /// <summary>
    /// Distribute the given function to the corresponding Graphics Program functions.
    /// The Graphics Program is set in the project settings.
    /// </summary>
    internal class GraphicsConnector
    {
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
        public static void DisplayPreview(object? sender, PictureBox previewBox)
        {
            switch (Properties.Settings.Default.GraphicsSoftware)
            {
                // XPRESSION:
                case GraphicsSoftware.XPression:
#if HAS_XPRESSION   // If XPression reference exists, compile this code
                    // Check if Dongle is in computer
                    if (!IsXPressionDonglePresent()) return;
                    XP_Functions.DisplayPreview(sender, previewBox);
#else
                    MessageBox.Show("xpression.net.dll reference could not be found!");
#endif
                    break;

                // CASPARCG:
                case GraphicsSoftware.CasparCG:
#if HAS_CASPARCG   // If CasparCG reference exists, compile this code
                    CasparCG_Functions.DisplayPreview(sender, previewBox);
#else
                    MessageBox.Show("CasparCG.dll reference could not be found!");
#endif
                    break;

                // VMIX:
                case GraphicsSoftware.vMix:
#if HAS_VMIX        // If vMix reference exists, compile this code
                    VMIX_Functions.DisplayPreview(sender, previewBox);
#else
                    MessageBox.Show("vMix.dll reference could not be found!");
#endif
                    break;
            }
        }
        #endregion

        #region >----------------- Remove Preview: ---------------------
        /// <summary>
        /// Removes the preview in the given preview box.
        /// </summary>
        /// <param name = "previewBox">The PictureBox control element where the preview should be removed.</param>
        public static void RemovePreview(PictureBox previewBox)
        {
            switch (Properties.Settings.Default.GraphicsSoftware)
            {
                // XPRESSION:
                case GraphicsSoftware.XPression:
#if HAS_XPRESSION   // If XPression reference exists, compile this code
                    // Check if Dongle is in computer
                    if (!IsXPressionDonglePresent()) return;
                    XP_Functions.RemovePreview(previewBox);
#else
                    MessageBox.Show("xpression.net.dll reference could not be found!");
#endif
                    break;

                // CASPARCG:
                case GraphicsSoftware.CasparCG:
#if HAS_CASPARCG   // If CasparCG reference exists, compile this code
                    CasparCG_Functions.RemovePreview(sender, previewBox);
#else
                    MessageBox.Show("CasparCG.dll reference could not be found!");
#endif
                    break;

                // VMIX:
                case GraphicsSoftware.vMix:
#if HAS_VMIX        // If vMix reference exists, compile this code
                    VMIX_Functions.RemovePreview(sender, previewBox);
#else
                    MessageBox.Show("vMix.dll reference could not be found!");
#endif
                    break;
            }
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
            switch (Properties.Settings.Default.GraphicsSoftware)
            {
                // XPRESSION:
                case GraphicsSoftware.XPression:
#if HAS_XPRESSION   // If XPression reference exists, compile this code
                    // Check if Dongle is in computer
                    if (!IsXPressionDonglePresent()) return;
                    XP_Functions.TriggerClip(operatorButton, clipIndex);
#else
                    MessageBox.Show("xpression.net.dll reference could not be found!");
#endif
                    break;

                // CASPARCG:
                case GraphicsSoftware.CasparCG:
#if HAS_CASPARCG   // If CasparCG reference exists, compile this code
                    CasparCG_Functions.RemovePreview(sender, previewBox);
#else
                    MessageBox.Show("CasparCG.dll reference could not be found!");
#endif
                    break;

                // VMIX:
                case GraphicsSoftware.vMix:
#if HAS_VMIX        // If vMix reference exists, compile this code
                    VMIX_Functions.RemovePreview(sender, previewBox);
#else
                    MessageBox.Show("vMix.dll reference could not be found!");
#endif
                    break;
            }
        }
        #endregion

        // Change text/material
    }
}
