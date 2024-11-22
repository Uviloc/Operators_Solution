using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Threading.Channels;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using System.Reflection;
using OperatorsSolution.Program;
using OperatorsSolution.GraphicsProgramFunctions;

namespace OperatorsSolution
{
    public partial class OpSol_Form : CustomForm
    {
        public OpSol_Form()
        {
            InitializeComponent();
            InitializeCollapseControlPanelTimer();

            if (TreeviewExplorer != null)
                ModuleLoader.LoadModules(TreeviewExplorer);
        }

        #region >----------------- Collapse Control Panel: ---------------------
        private bool controlPanelOpen = true;
        private bool controlPanelChanging = false;
        private readonly int animationDuration = 3;

        //private int originalControlPanelWidth;
        //private Point originalControlPanelPos;
        //private int originalControlPanelLeft;
        private System.Windows.Forms.Timer? controlPanelAnimationTimer;

        private void InitializeCollapseControlPanelTimer()
        {
            if (ControlPanel == null) return;
            //originalControlPanelWidth = ControlPanel.Width;
            //originalControlPanelPos = ControlPanel.Location;
            //originalControlPanelLeft = ControlPanel.Left;

            controlPanelAnimationTimer = new System.Windows.Forms.Timer{ Interval = 1 };
            controlPanelAnimationTimer.Tick += ControlPanelTimer_Tick;
        }

        private void CollapseControlPanel(object? sender, EventArgs e)
        {
            if (ControlPanel == null || controlPanelAnimationTimer == null) return;
            controlPanelAnimationTimer.Start();
            controlPanelChanging = true;
            if (sender is Button collapseButton)
            {
                collapseButton.Text = controlPanelOpen ? ">" : "<";
            }
        }

        private void ButtonEnter(object? sender, EventArgs e)
        {
            if (sender is not Control control) return;
            if (control.Tag is not Color originalColor) control.Tag = originalColor = control.BackColor;
            control.BackColor = Color.FromArgb(originalColor.R + 50, originalColor.G + 50, originalColor.B + 50);
        }
        private void ButtonLeave(object? sender, EventArgs e)
        {
            if (sender is not Control control) return;
            if (control.Tag is not Color originalColor) control.Tag = originalColor = control.BackColor;
            control.BackColor = originalColor;
        }


        private void ControlPanelTimer_Tick(object? sender, EventArgs e)
        {
            if (ControlPanel == null || controlPanelAnimationTimer == null) return;
            int animationSpeed = 100/animationDuration;

            if (controlPanelOpen)
            {
                ControlPanel.Width -= animationSpeed;

                // Slow down redraws for form panel to reduce flickering
                //if (ControlPanel.Width % 20 == 0)
                //{
                //    ScaleFormToFitPanel(FormModulePanel);
                //}

                if (ControlPanel.Width <= ControlPanel.MinimumSize.Width)
                {
                    controlPanelOpen = false;
                    controlPanelChanging = false;
                    controlPanelAnimationTimer.Stop();
                    ScaleFormToFitPanel(FormModulePanel);
                }
            }
            else
            {
                ControlPanel.Width += animationSpeed;

                // Slow down redraws for form panel to reduce flickering
                //if (ControlPanel.Width % 20 == 0)
                //{
                //    ScaleFormToFitPanel(FormModulePanel);
                //}

                if (ControlPanel.Width >= ControlPanel.MaximumSize.Width)
                {
                    controlPanelOpen = true;
                    controlPanelChanging = false;
                    controlPanelAnimationTimer.Stop();
                    ScaleFormToFitPanel(FormModulePanel);
                }
            }
            //if (controlPanelOpen)
            //{
            //    ControlPanel.Left -= animationSpeed;
            //    if (ControlPanel.Left <= originalControlPanelWidth)
            //    {
            //        ControlPanel.Left = originalControlPanelWidth;
            //        controlPanelOpen = false;
            //        controlPanelAnimationTimer.Stop();
            //    }
            //}
            //else
            //{
            //    ControlPanel.Left += animationSpeed;
            //    if (ControlPanel.Left >= originalControlPanelLeft)
            //    {
            //        ControlPanel.Left = originalControlPanelLeft;
            //        controlPanelOpen = true;
            //        controlPanelAnimationTimer.Stop();
            //    }
            //}
            //if (controlPanelOpen)
            //{
            //    ControlPanel.Location = new Point(ControlPanel.Location.X - animationSpeed, 0);
            //    if (ControlPanel.Location.X <= originalControlPanelPos.X)
            //    {
            //        controlPanelOpen = false;
            //        controlPanelAnimationTimer.Stop();
            //    }
            //}
            //else
            //{
            //    ControlPanel.Location = new Point(ControlPanel.Location.X + animationSpeed, 0);
            //    if (ControlPanel.Location.X >= originalControlPanelPos.X)
            //    {
            //        controlPanelOpen = true;
            //        controlPanelAnimationTimer.Stop();
            //    }
            //}
        }
        #endregion

        #region >----------------- OpenForm: ---------------------
        private void OpenForm(object? sender, TreeNodeMouseClickEventArgs e)
        {
            if (FormModulePanel == null) return;

            if (e.Node.Tag is Common.IModuleForm pluginForm)
            {
                // Get the form from the IModuleForm instance
                Form form = pluginForm.GetForm();

                // Set the form to be a child inside the InnerPannel
                form.TopLevel = false;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.None;

                // Clear the InnerPannel to ensure no other controls are blocking the new form
                FormModulePanel.Controls.Clear();

                // Add the form to the InnerPannel's Controls collection
                FormModulePanel.Controls.Add(form);

                // Store the form for use in the SizeChanged event
                FormModulePanel.Tag = form;

                // Perform scaling
                ScaleFormToFitPanel(FormModulePanel);

                // Show the form inside the panel
                form.Show();
            }
        }
        #endregion

        #region >----------------- Scale Form: ---------------------
        private static void ScaleFormToFitPanel(Form form, Panel panel)
        {
            //// Get the original size of the form
            //Size originalSize = form.Size;

            // Check if the form's original size is already stored
            if (form.Tag is Size originalSize)
            {
                // Reset the form to its original size
                form.Size = originalSize;

                // Reset all controls to their original sizes and locations
                foreach (Control control in form.Controls)
                {
                    if (control.Tag is (Size controlOriginalSize, Point controlOriginalLocation))
                    {
                        control.Size = controlOriginalSize;
                        control.Location = controlOriginalLocation;
                    }
                }
            }
            else
            {
                // Store the original size of the form in its Tag property
                form.Tag = form.Size;

                // Store the original size and location of each control
                foreach (Control control in form.Controls)
                {
                    control.Tag = (control.Size, control.Location);
                }
            }

            // Calculate scaling factors for width and height
            float scaleX = (float)panel.Width / form.Size.Width;
            float scaleY = (float)panel.Height / form.Size.Height;

            // Use non-cumulative scaling
            form.Scale(new SizeF(scaleX, scaleY));
        }
        private static void ScaleFormToFitPanel(object? formModulePanel)
        {
            if (formModulePanel is not Panel panel || panel.Tag is not Form form) return;
            // Check if the form's original size is already stored
            if (form.Tag is not Size originalSize)
            {
                // Store the original size of the form in its Tag property
                form.Tag = form.Size;
                originalSize = form.Size;

                // Store the original size and location of each control
                foreach (Control control in form.Controls)
                {
                    control.Tag = (control.Size, control.Location);
                }
            }

            // Reset all controls to their original sizes and locations
            foreach (Control control in form.Controls)
            {
                if (control.Tag is (Size controlOriginalSize, Point controlOriginalLocation))
                {
                    control.Size = controlOriginalSize;
                    control.Location = controlOriginalLocation;
                }
            }
            //Reset the form to its original size
            form.Size = originalSize;


            // Calculate scaling factors for width and height
            float scaleX = (float)panel.Width / originalSize.Width;
            float scaleY = (float)panel.Height / originalSize.Height;

            // Use non-cumulative scaling
            form.Scale(new SizeF(scaleX, scaleY));
        }

        private void FormModulePanel_SizeChanged(object? sender, EventArgs e)
        {
            if (controlPanelChanging) return;
            ScaleFormToFitPanel(sender);
        }
        #endregion

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

        private void TestButton(object sender, EventArgs e)
        {
            //if (XP_Functions.imageOut == null) return;
            //MessageBox.Show("Kill activated");
            //var adapter = XP_Functions.imageOut.Adapter;
            //adapter = XP_Functions.imageOut.Adapter;
            //adapter = XP_Functions.imageOut.Adapter;
            //adapter = XP_Functions.imageOut.Adapter;
            //adapter = XP_Functions.imageOut.Adapter;
            //adapter = XP_Functions.imageOut.Adapter;
            //adapter = XP_Functions.imageOut.Adapter;
            //adapter = XP_Functions.imageOut.Adapter;
        }
    }
}