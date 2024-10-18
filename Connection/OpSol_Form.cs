using ExcelDataReader;
using System;
using System.Net;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using Rug.Osc;
using System.Linq.Expressions;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using Operators_Solution;

#if HAS_XPRESSION
using XPression;
#endif



namespace OperatorsSolution
{
    public partial class OpSol_Form : Form
    {
        public OpSol_Form()
        {
            InitializeComponent();
        }

#if HAS_XPRESSION
        private xpEngine XPression = new();
        private int index = 0;
        public void Trigger_Clips(object? sender, EventArgs e)
        {
            if (sender is OperatorButton button && button.ClipPath != null)
            {
                // Warning if there are no assigned scenes:
                if (button.ClipPath.Count == 0)
                {
                    CommonFunctions.ControlWarning(button, "Please add ClipPaths to the button: " + button.Text);
                    return;
                }

                // Play the clip that this item is pointing to:
                TriggerClip(button, index);

                if (index < button.ClipPath.Count + 1) {
                    index++;
                } else {
                    index = 0;
                }
            }
        }



        private void TriggerClip(OperatorButton operatorButton, int clipIndex)
        {
            List<ClipPath>? clipPath = operatorButton.ClipPath;
            if (clipPath == null || clipPath.Count == 0)
            {
                return;
            }
            string? scene = clipPath[clipIndex].Scene;
            string? clip = clipPath[clipIndex].Clip;
            string? track = clipPath[clipIndex].Track;
            int channel = clipPath[clipIndex].Channel;
            int layer = clipPath[clipIndex].Layer;


            if (string.IsNullOrWhiteSpace(scene) ||
                string.IsNullOrWhiteSpace(clip))
            {
                CommonFunctions.ControlWarning(operatorButton, "Warning: Scene on button: " + operatorButton.Text + " must be set!");
                return;
            }

            if (XPression.GetSceneByName(scene, out xpScene SceneGraphic, true))
            {
                XPConnections.PlaySceneState(SceneGraphic, scene, clip, track, channel, layer);
            }
            else
            {
                CommonFunctions.ControlWarning(operatorButton, "Warning: " + scene + ">" + track + ">" + clip + " on button: " + operatorButton.Text + " could not be found!");
            }
        }
    }
}