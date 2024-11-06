#if HAS_XPRESSION
using XPression;
#endif

#if HAS_CASPARCG
//using casparcg
#endif

#if HAS_VMIX
//using vmix
#endif

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
        public interface IModuleForm
        {
            Form GetForm();
            string FormName { get; }
        }

        public OpSol_Form()
        {
            InitializeComponent();

            LoadModules();
        }

        private void LoadModules()
        {
            string moduleFolder = Path.Combine(Application.StartupPath, "Modules");
            if (!Directory.Exists(moduleFolder))
            {
                Directory.CreateDirectory(moduleFolder);
            }

            // Loop through each DLL in the Modules folder
            foreach (string dll in Directory.GetFiles(moduleFolder, "*.dll"))
            {
                try
                {
                    // Load the assembly
                    Assembly assembly = Assembly.LoadFrom(dll);

                    // Find all types in the assembly that implement IModuleForm
                    foreach (Type type in assembly.GetTypes())
                    {
                        if (typeof(IModuleForm).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                        {
                            // Create an instance of the plugin form
                            IModuleForm moduleForm = (IModuleForm)Activator.CreateInstance(type);

                            // Add the plugin form to the TreeView
                            AddToTreeView(moduleForm);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading plugin {dll}: {ex.Message}", "Plugin Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void AddToTreeView(IModuleForm pluginForm)
        {
            TreeNode node = new TreeNode(pluginForm.FormName) // Use the form's name as the node text
            {
                Tag = pluginForm // Store the plugin form in the Tag property for easy access later
            };
            treeViewModules.Nodes.Add(node);
        }

        private void treeViewModules_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is IModuleForm pluginForm)
            {
                // Get the form from the IModuleForm instance
                Form form = pluginForm.GetForm();

                // Set the form to be a child inside the InnerPannel
                form.TopLevel = false;                // This makes the form not open as a separate window
                form.FormBorderStyle = FormBorderStyle.None; // Remove the border for a seamless look
                form.Dock = DockStyle.Fill;            // Make the form fill the InnerPannel

                // Clear the InnerPannel to ensure no other controls are blocking the new form
                InnerPannel.Controls.Clear();

                // Add the form to the InnerPannel's Controls collection
                InnerPannel.Controls.Add(form);

                // Show the form inside the panel
                form.Show();
            }
        }



        #region >----------------- Open project: ---------------------
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
//            switch (Properties.Settings.Default.GraphicsSoftware)
//            {
//                case GraphicsSoftware.XPression:
//#if HAS_XPRESSION   // If XPression reference exists, compile this code
//                    // Check if Dongle is in computer
//                    if (!IsXPressionDonglePresent()) break;

//                    xpEngine XPression = new();
//                    if (XPression.ActiveProject.FileName == projectFilePath) return;
//                    if (XPression.LoadProject(projectFilePath)) return;
//#else
//                    MessageBox.Show("xpression.net.dll reference could not be found!");
//#endif
//                    break;


//                case GraphicsSoftware.CasparCG:
//#if HAS_CASPARCG   // If CasparCG reference exists, compile this code
//                    MessageBox.Show("WOW seems someone got hold of a timemachine");
//#else
//                    MessageBox.Show("CasparCG.dll reference could not be found!");
//#endif
//                    break;


//                case GraphicsSoftware.vMix:
//#if HAS_VMIX        // If vMix reference exists, compile this code
//                    MessageBox.Show("WOW seems someone got hold of a timemachine");
//#else
//                    MessageBox.Show("vMix.dll reference could not be found!");
//#endif
//                    break;
//            }
//            MessageBox.Show("Project: " + fileName + " could not open!");
        }
        #endregion


        #region >----------------- Trigger clips: ---------------------
        // MOVED TO BUTTON CLASS: GET RID OF THIS
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


        public static void TestingFunction(string somethingProfound)
        {
            MessageBox.Show(somethingProfound);
        }


        #region >----------------- Trigger clip: ---------------------
        public static void TriggerClip(OperatorButton operatorButton, int clipIndex)
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
            string? sceneDirector = clipPath[clipIndex].SceneDirector;
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
#if HAS_XPRESSION   // If XPression reference exists, compile this code
                    // Check if Dongle is in computer
                    if (!IsXPressionDonglePresent()) break;

                    xpEngine XPression = new();
                    if (XPression.GetSceneByName(scene, out xpScene SceneGraphic, true))
                    {
                        XP_Functions.SetAllSceneMaterials(SceneGraphic, clipPath[clipIndex].ObjectChanges);
                        XP_Functions.PlaySceneState(SceneGraphic, sceneDirector, clip, track, channel, layer);
                    }
                    else
                    {
                        CommonFunctions.ControlWarning(operatorButton, "Warning: " + scene + ">" + track + ">" + clip + " on button: " + operatorButton.Text + " could not be found!");
                    }
#else
                    MessageBox.Show("xpression.net.dll reference could not be found!");
#endif
                    break;


                case GraphicsSoftware.CasparCG:
#if HAS_CASPARCG   // If CasparCG reference exists, compile this code
                    MessageBox.Show("WOW seems someone got hold of a timemachine");
#else
                    MessageBox.Show("CasparCG.dll reference could not be found!");
#endif
                    break;


                case GraphicsSoftware.vMix:
#if HAS_VMIX        // If vMix reference exists, compile this code
                    MessageBox.Show("WOW seems someone got hold of a timemachine");
#else
                    MessageBox.Show("vMix.dll reference could not be found!");
#endif
                    break;
            }
        }
        #endregion




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
        




        public void DisplayThumbnail(object sender, EventArgs e)
        {
            if (!IsXPressionDonglePresent()) return;
            if (sender is OperatorButton button && button.Scene != null)
            {
                string scene = button.Scene;

                xpEngine XPression = new();
                if (XPression.GetSceneByName(scene, out xpScene SceneGraphic, true))
                {
                    XP_Functions.GetThumbnail(SceneGraphic, out xpImage? thumbnail);

                    if (thumbnail != null)
                    {
                        Bitmap image = ConvertToBitmap(thumbnail);
                        PreviewBox.SizeMode = PictureBoxSizeMode.Zoom;
                        PreviewBox.Image = image;
                    }
                }
                else
                {
                    CommonFunctions.ControlWarning(button, "Warning: There is no Scene on button: " + button.Text + "!");
                }
            }
        }

        public void RemoveThumbnail(object sender, EventArgs e)
        {
            PreviewBox.Image = null;
        }

        private static Bitmap ConvertToBitmap(xpImage thumbNailImage)
        {
            Bitmap bmp;
            bmp = xpTools.xpImageToBitmap(thumbNailImage);
            return bmp;
        }




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