#if HAS_XPRESSION
using XPression;
#endif

#if HAS_CASPARCG
using CasparCG;
#endif

#if HAS_VMIX
using vMix;
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace OperatorsSolution
{
    // Class intended for distributing functions to the different Graphics Program functions
    internal class GraphicsConnector
    {
        // XPression Detection:
        public static bool IsXPressionDonglePresent()
        {
            try
            {
                xpEngine XPression = new();
                return true;
            }
            catch
            {
                MessageBox.Show("XPression Dongle is not connected. Features are disabled.");
                return false;
            }
        }


        #region >----------------- DisplayPreview: ---------------------
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
