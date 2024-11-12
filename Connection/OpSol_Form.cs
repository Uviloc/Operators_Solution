using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Threading.Channels;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using System.Reflection;

namespace OperatorsSolution
{
    public partial class OpSol_Form : Form
    {
        public OpSol_Form()
        {
            InitializeComponent();

            // Load Modules:
            try
            {
                List<ModuleLoader> moduleLoaders = Controls.OfType<ModuleLoader>().ToList();
                if (moduleLoaders.Count == 0) return;
                moduleLoaders[0].LoadModules();
            }
            catch (Exception ex) { Console.WriteLine("Could not load modules" + ex); }
        }

        #region >----------------- Open project file: ---------------------
        private void OpenProject(object? sender, EventArgs e)
        {
            string projectFilePath = Properties.Settings.Default.ProjectFile;

            if (string.IsNullOrWhiteSpace(projectFilePath))
            {
                MessageBox.Show("There was no selected project. (Project settings>Project file)");
                return;
            }

            string fileName = projectFilePath.Split('\\').Last();

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = projectFilePath,
                UseShellExecute = true
            });
        }
        #endregion

        #region >----------------- Testing Preview on Hover: ---------------------
#if HAS_XPRESSION
        //private void DisplayPreview(object sender, EventArgs e)
        //{
        //    if (sender is OperatorButton button && button.ClipPaths != null)
        //    {
        //        ClipPathCollection clipPath = button.ClipPaths;


        //        if (clipPath == null || clipPath.Count == 0)
        //        {
        //            return;
        //        }
        //        string? scene = clipPath[0].Scene;
        //        string? clip = clipPath[0].Clip;
        //        string? track = null;
        //        int channel = 1;
        //        int layer = clipPath[0].Layer;
        //        string? sceneDirector = clipPath[0].SceneDirector;

        //        // Check if any of the fields are empty:
        //        if (string.IsNullOrWhiteSpace(scene) ||
        //            string.IsNullOrWhiteSpace(clip))
        //        {
        //            CommonFunctions.ControlWarning(button, "Warning: Scene on button: " + button.Text + " must be set!");
        //            return;
        //        }



        //        // If XPression reference exists, play the clip

        //        xpEngine XPression = new();
        //        if (XPression.GetSceneByName(scene, out xpScene SceneGraphic, true))
        //        {
        //            //XP_Functions.PlaySceneState(SceneGraphic, scene, clip, track, channel, layer);

        //            //SceneGraphic.GetPreviewSceneDirector(out xpSceneDirector director);
        //            XP_Functions.PlayPreview(SceneGraphic, sceneDirector, clip, track, out xpImage? previewImage, channel, layer);

        //            if (previewImage != null)
        //            {
        //                Bitmap image = ConvertToBitmap(previewImage);
        //                pictureBox1.Image = image;
        //            }

        //            // GETRENDEREDFRAME
        //            // Hardware setup > add in inputs/outputs > go to preview (> set channel 1 with next as preview in preview & monitor)
        //            // playscene on channel 2 (maybe see if it can be hidden)
        //            // XPRession.GetOutputFrameBuffer > gives output of channel 2 (needs to be set as a preview channel in xpression)
        //            //xpOutputFrameBuffer.getCurrentFrame() > puts out an image

        //        }
        //        else
        //        {
        //            CommonFunctions.ControlWarning(button, "Warning: " + scene + ">" + track + " on button: " + button.Text + " could not be found!");
        //        }


        //        //XPression.GetRenderInfo(out int frameBufferIndex, out int field);
        //        //XPression.GetInputFrameBuffer(1, out xpInputFrameBuffer preview);
        //        //if(frameBufferIndex != null && fi)
        //        //{
        //            //MessageBox.Show(frameBufferIndex.ToString() + "  |  " + field.ToString());
        //        //} else
        //        //{
        //        //    MessageBox.Show("frameBufferIndex is NULL");
        //        //}
        //    }
        //}





        //public void DisplayPreview(object sender, EventArgs e)
        //{
        //    if (!IsXPressionDonglePresent()) return;
        //    if (sender is OperatorButton button && button.Scene != null)
        //    {
        //        string scene = button.Scene;

        //        xpEngine XPression = new();
        //        if (XPression.GetSceneByName(scene, out xpScene SceneGraphic, true))
        //        {
        //            XP_Functions.GetThumbnail(SceneGraphic, out xpImage? thumbnail);

        //            if (thumbnail != null)
        //            {
        //                Bitmap image = ConvertToBitmap(thumbnail);
        //                PreviewBox.SizeMode = PictureBoxSizeMode.Zoom;
        //                PreviewBox.Image = image;
        //            }
        //        }
        //        else
        //        {
        //            CommonFunctions.ControlWarning(button, "Warning: There is no Scene on button: " + button.Text + "!");
        //        }
        //    }
        //}

        //public void RemovePreview(object sender, EventArgs e)
        //{
        //    PreviewBox.Image = null;
        //}

        //private static Bitmap ConvertToBitmap(xpImage thumbNailImage)
        //{
        //    Bitmap bmp;
        //    bmp = xpTools.xpImageToBitmap(thumbNailImage);
        //    return bmp;
        //}




        //private void RemovePreview(object sender, EventArgs e)
        //{
        //    if (sender is OperatorButton button && button.ClipPaths != null)
        //    {
        //        ClipPathCollection clipPath = button.ClipPaths;


        //        if (clipPath == null || clipPath.Count == 0)
        //        {
        //            return;
        //        }
        //        string? scene = clipPath[0].Scene;

        //        xpEngine XPression = new();
        //        if (XPression.GetSceneByName(scene, out xpScene SceneGraphic, true))
        //        {
        //            XP_Functions.StopPreview(SceneGraphic);
        //        }
        //    }
        //}

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