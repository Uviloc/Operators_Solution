using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;

#if HAS_XPRESSION
using XPression;
#endif

#if HAS_CASPARCG
//using casparcg
#endif

#if HAS_VMIX
//using vmix
#endif


namespace OperatorsSolution
{
    public partial class OpSol_Form : Form
    {
        public OpSol_Form()
        {
            InitializeComponent();
        }

        #region >----------------- Trigger clips: ---------------------
        private int index = 0;
        public void Trigger_Clips(object? sender, EventArgs e)
        {
            if (sender is OperatorButton button && button.ClipPaths != null)
            {
                // Warn and exit if there are no assigned scenes:
                if (button.ClipPaths.Count == 0)
                {
                    CommonFunctions.ControlWarning(button, "Please add ClipPaths to the button: " + button.Text);
                    return;
                }


                // Play the clip that this item is pointing to:
                TriggerClip(button, index);

                if (index < button.ClipPaths.Count - 1)
                {
                    index++;
                }
                else
                {
                    index = 0;
                }
            }
        }
        #endregion





        #region >----------------- Trigger clip: ---------------------
        private void TriggerClip(OperatorButton operatorButton, int clipIndex)
        {
            // Set all needed variables to the assigned properties in the ClipPath
            ClipPathCollection clipPath = operatorButton.ClipPaths;
            if (clipPath == null || clipPath.Count == 0)
            {
                return;
            }
            string? scene = clipPath[clipIndex].Scene;
            string? clip = clipPath[clipIndex].Clip;
            string? track = clipPath[clipIndex].Track;
            int channel = clipPath[clipIndex].Channel;
            int layer = clipPath[clipIndex].Layer;

            // Check if any of the fields are empty:
            if (string.IsNullOrWhiteSpace(scene) ||
                string.IsNullOrWhiteSpace(clip))
            {
                CommonFunctions.ControlWarning(operatorButton, "Warning: Scene on button: " + operatorButton.Text + " must be set!");
                return;
            }


            // Change what functions to execute dependant on the settings (GraphicsSoftware) and check if the dll reference exists when they are called
            switch(Properties.Settings.Default.GraphicsSoftware)
            {
                case GraphicsSoftware.XPression:
#if HAS_XPRESSION   // If XPression reference exists, play the clip
                    xpEngine XPression = new();
                    if (XPression.GetSceneByName(scene, out xpScene SceneGraphic, true))
                    {
                        XP_Functions.SetAllSceneMaterials(SceneGraphic, clipPath[clipIndex].ObjectChanges);
                        XP_Functions.PlaySceneState(SceneGraphic, scene, clip, track, channel, layer);
                    }
                    else
                    {
                        CommonFunctions.ControlWarning(operatorButton, "Warning: " + scene + ">" + track + ">" + clip + " on button: " + operatorButton.Text + " could not be found!");
                    }
#else
                    MessageBox.Show("xpression.net reference could not be found!");
#endif
                    break;


                case GraphicsSoftware.CasparCG:
#if HAS_CASPARCG   // If CasparCG reference exists, play the clip
                    MessageBox.Show("WOW seems someone got hold of a timemachine");
#else
                    MessageBox.Show("CasparCG reference could not be found!");
#endif
                    break;


                case GraphicsSoftware.vMix:
#if HAS_VMIX        // If vMix reference exists, play the clip
                    MessageBox.Show("WOW seems someone got hold of a timemachine");
#else
                    MessageBox.Show("vMix reference could not be found!");
#endif
                    break;
            }
        }
        #endregion



        #region >----------------- Testing Preview on Hover: ---------------------
#if HAS_XPRESSION
        private void DisplayPreview(object sender, EventArgs e)
        {
            if (sender is OperatorButton button && button.ClipPaths != null)
            {
                ClipPathCollection clipPath = button.ClipPaths;
                int clipIndex = 0;


                if (clipPath == null || clipPath.Count == 0)
                {
                    return;
                }
                string? scene = clipPath[clipIndex].Scene;
                string? clip = clipPath[clipIndex].Clip;
                string? track = clipPath[clipIndex].Track;
                int channel = clipPath[clipIndex].Channel;
                int layer = clipPath[clipIndex].Layer;

                // Check if any of the fields are empty:
                if (string.IsNullOrWhiteSpace(scene) ||
                    string.IsNullOrWhiteSpace(clip))
                {
                    CommonFunctions.ControlWarning(button, "Warning: Scene on button: " + button.Text + " must be set!");
                    return;
                }



                // If XPression reference exists, play the clip

                xpEngine XPression = new();
                if (XPression.GetSceneByName(scene, out xpScene SceneGraphic, true))
                {
                    //XP_Functions.PlaySceneState(SceneGraphic, scene, clip, track, channel, layer);

                    SceneGraphic.GetPreviewSceneDirector(out xpSceneDirector director);
                    MessageBox.Show(director.Name);
                    director.SetAsDefault();
                    MessageBox.Show(director.PlayMode.ToString());
                    
                }
                else
                {
                    CommonFunctions.ControlWarning(button, "Warning: " + scene + ">" + track + ">" + clip + " on button: " + button.Text + " could not be found!");
                }


                //XPression.GetRenderInfo(out int frameBufferIndex, out int field);
                //XPression.GetInputFrameBuffer(1, out xpInputFrameBuffer preview);
                //if(frameBufferIndex != null && fi)
                //{
                    //MessageBox.Show(frameBufferIndex.ToString() + "  |  " + field.ToString());
                //} else
                //{
                //    MessageBox.Show("frameBufferIndex is NULL");
                //}
            }
        }
#endif
#endregion



        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //[DesignerCategory()]
        private void btnOpenSettings_Click(object sender, EventArgs e)
        {
            // Open the settings form
            Settings settingsForm = new();
            settingsForm.Show();  // Open as a modal dialog
            settingsForm.Focus();
        }
    }
}