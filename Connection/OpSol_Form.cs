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

        #region >__________________________________________________________________________ XPression: __________________________________________________________________________

#if HAS_XPRESSION
        private xpEngine XPression = new();
#endif

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
            //List<ClipPath>? clipPath = operatorButton.ClipPath; // If we go back to not having the ClipPathCollectionClass
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



            // If XPression reference exists, play the clip
#if HAS_XPRESSION
            if (XPression.GetSceneByName(scene, out xpScene SceneGraphic, true))
            {
                XPConnections.PlaySceneState(SceneGraphic, scene, clip, track, channel, layer);
            }
            else
            {
                CommonFunctions.ControlWarning(operatorButton, "Warning: " + scene + ">" + track + ">" + clip + " on button: " + operatorButton.Text + " could not be found!");
            }
#endif
        }
        #endregion




        private void DisplayPreview(object sender, EventArgs e)
        {
            if (sender is OperatorButton button && button.ClipPaths != null)
            {
                XPression.GetRenderInfo(out int frameBufferIndex, out int field);
                //XPression.GetInputFrameBuffer(1, out xpInputFrameBuffer preview);
                //if(frameBufferIndex != null && fi)
                //{
                    MessageBox.Show(frameBufferIndex.ToString() + "  |  " + field.ToString());
                //} else
                //{
                //    MessageBox.Show("frameBufferIndex is NULL");
                //}
            }
        }
        #endregion



















        private void btnOpenSettings_Click(object sender, EventArgs e)
        {
            // Open the settings form
            Settings settingsForm = new();
            settingsForm.Show();  // Open as a modal dialog
            settingsForm.Focus();
        }
    }
}