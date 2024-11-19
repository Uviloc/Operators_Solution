#if HAS_XPRESSION
using XPression;
using System;
using System.Collections.Generic;
using System.Threading.Channels;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using OperatorsSolution.Common;
using OperatorsSolution.Controls;
using System.Reflection;
using System.Drawing.Imaging;
using System.Windows.Forms;


namespace OperatorsSolution.GraphicsProgramFunctions
{
    /// <summary>
    /// All functions for the XPression Graphics Program.
    /// </summary>
    internal class XP_Functions
    {
        #region >----------------- XPression play scene: ---------------------
        /// <summary>
        /// Plays out the scene in XPression when this project is open in XPression.
        /// </summary>
        /// <param name = "scene">The xpScene in which the clip is located in.</param>
        /// <param name = "sceneDirectorName">The name of the scene director.</param>
        /// <param name = "clip">The name of the clip that needs to be played.</param>
        /// <param name = "channel">The channel on which the clip needs to play.</param>
        /// <param name="layer">The layer on which the clip needs to play.</param>
        /// <returns>true if succsesfull, false if the clip could not be found</returns>
        public static bool PlaySceneState(xpScene scene, string? sceneDirectorName, string clip, string? track, int channel = 0, int layer = 0)
        {
            // Set to be a default of the same name as its scene if it was not filled in
            if (string.IsNullOrWhiteSpace(sceneDirectorName) || sceneDirectorName == "Same as [Scene]")
            {
                sceneDirectorName = scene.Name;
                //sceneDirectorName = scene.SceneDirector
            }
            if (string.IsNullOrWhiteSpace(track))
            {
                track = "StateTrack";
            }

            if (!scene.GetSceneDirectorByName(sceneDirectorName, out xpSceneDirector Scene_Director) ||
                !Scene_Director.GetTrackByName(track, out xpSceneDirectorTrack Scene_State_Track) ||
                !Scene_State_Track.GetClipByName(clip, out xpSceneDirectorClip State_Clip))
            { return false; }

            //int defaultFrame = Scene_Director.DefaultFrameMarker;
            //Scene_Director.PlayRange(defaultFrame, defaultFrame+1);
            //scene.SetOnline(1, layer);

            Scene_Director.PlayRange(State_Clip.Position, State_Clip.Position);
            scene.SetOnline(channel, layer);
            Scene_Director.PlayRange(State_Clip.Position, State_Clip.Duration + State_Clip.Position);
            return true;
        }
        #endregion

        #region >----------------- XPression set objects in scene: ---------------------
        /// <summary>
        /// Changes all materials given in the objectList list in a scene from XPression.
        /// </summary>
        /// <param name="scene">The scene in which the materials or values need to be changed.</param>
        /// <param name="objectList">The list of objects to change (in XPression) the material or value of.</param>
        public static void SetAllSceneMaterials(xpScene scene, List<ObjectChange> objectList)
        {
            xpEngine xpEngine = new();
            if (objectList.Count == 0 || xpEngine == null)
            {
                return;
            }

            foreach (ObjectChange objectChange in objectList)
            {
                SetMaterial(objectChange, scene, xpEngine);
            }
        }
        #endregion

        #region >----------------- XPression set material or value of object: ---------------------
        /// <summary>
        /// Changes the material or value given in the objectChange in a scene from XPression.
        /// </summary>
        /// <param name="scene">The scene in which the materials or values need to be changed.</param>
        /// <param name="objectList">The list of objects to change (in XPression) the material or value of.</param>
        public static void SetMaterial(ObjectChange objectChange, xpScene scene, xpEngine xpEngine)
        {
            // Check if any values are null
            if (objectChange.SceneObject == null)
            {
                return;
            }

            // Check what type of object it is
            if (!scene.GetObjectByName(objectChange.SceneObject, out xpBaseObject baseObject))
            {
                // Exit in case no scene object has been found
                return;
            }

            switch (baseObject)
            {
                case xpTextObject:
                    xpTextObject textObject = (xpTextObject)baseObject;
                    textObject.Text = objectChange.SetTo;
                    break;
                case xpQuadObject:
                    xpQuadObject quad = (xpQuadObject)baseObject;

                    if (xpEngine.GetMaterialByName(objectChange.SetTo, out xpMaterial material))
                    {
                        quad.SetMaterial(0, material);
                    }
                    break;
                default:
                    MessageBox.Show("object type: " + baseObject.TypeName + " cannot be handled.");
                    return;
            }
        }
        #endregion

        #region >----------------- XPression Display/Remove Preview: ---------------------

        public static xpImage? imageOut;
        /// <summary>
        /// Displays a preview of the buttons scene in the given preview box.
        /// </summary>
        /// <param name = "sender">The button with the scene information on it.</param>
        /// <param name = "previewBox">The PictureBox control element where the preview should be displayed.</param>
        public static void DisplayPreview(object? sender, PictureBox previewBox)
        {
            if (sender is OperatorButton button && button.Scene != null)
            {
                string scene = button.Scene;

                xpEngine XPression = new();
                if (XPression.GetSceneByName(scene, out xpScene SceneGraphic, true))
                {
                    int preveiwLocation = SceneGraphic.DefaultPreviewFrame;
                    SceneGraphic.GetRenderedFrame(preveiwLocation, SceneGraphic.Width, SceneGraphic.Height, out imageOut);
                    previewBox.SizeMode = PictureBoxSizeMode.Zoom;
                    //RenderRawPixelsDirectly(previewBox, imageOut);
                    
                    Bitmap image = ConvertToBitmap(imageOut);
                    previewBox.Image = image;
                }
                else
                {
                    CommonFunctions.ControlWarning(button, "Warning: There is no Scene on button: " + button.Text + "!");
                }
            }
        }

        /// <summary>
        /// Removes the preview in the given preview box.
        /// </summary>
        /// <param name = "previewBox">The PictureBox control element where the preview should be removed.</param>
        public static void RemovePreview(PictureBox previewBox)
        {
            previewBox.Image?.Dispose();
            previewBox.Image = null;
        }

        /// <summary>
        /// Plays out the scene in XPression when this project is open in XPression.
        /// </summary>
        /// <param name = "scene">The xpScene in which the clip is located in.</param>
        /// <returns>true if succsesfull, false if the defaultFrame could not be found</returns>
        public static bool GetThumbnail(xpScene scene, out xpImage? image)
        {
            scene.GetThumbnail(out image);
            return true;
        }

        private static Bitmap ConvertToBitmap(xpImage image)
        {
            Bitmap bmp;
            bmp = xpTools.xpImageToBitmap(image);
            return bmp;
        }
        #endregion



        //private static PropertyInfo adapterPropertyInfo = typeof(xpImage).GetProperty("Adapter");
        private static void RenderRawPixelsDirectly(PictureBox pictureBox, xpImage image)
        {
            var adapter = image.Adapter;

            if (adapter is Array byteArray)
            {
                byte[] rawData = new byte[byteArray.Length];
                Buffer.BlockCopy(byteArray, 0, rawData, 0, rawData.Length);

                using var ms = new MemoryStream(rawData);
                Bitmap bmp = new(ms);

                // Assign the bitmap to the PictureBox.Image property
                pictureBox.Image?.Dispose(); // Dispose of the old image
                pictureBox.Image = bmp;
                pictureBox.Invalidate(); // Force redraw
            }



            //var adapter = adapterPropertyInfo?.GetValue(image);  // Use cached PropertyInfo

            //var adapter = image.GetType().GetProperty("Adapter")?.GetValue(image);

            //if (adapter is not Array byteArray) return; // Exit if the adaptor is not an Array for whatever reason

            //byte[] rawData = new byte[byteArray.Length];
            //Buffer.BlockCopy(byteArray, 0, rawData, 0, rawData.Length);

            //using var ms = new MemoryStream(rawData);
            //Bitmap bmp = new(ms);

            //pictureBox.Image?.Dispose(); // Dispose of the old image
            //pictureBox.Image = bmp; // Assign the bitmap to the PictureBox.Image property
            //pictureBox.Invalidate(); // Force redraw
        }


        #region >----------------- Trigger clip: ---------------------
        /// <summary>
        /// Plays out the clip at clipIndex given in the operatorButton in XPression.
        /// </summary>
        /// <param name = "operatorButton">The button control that has the clip path list.</param>
        /// <param name = "clipIndex">Which clip to trigger in the clip path list.</param>
        public static void TriggerClip(OperatorButton operatorButton, int clipIndex)    // "PUBLIC STATIC XPSCENE", OR "OUT XPSCENE" then use this to call the same scene if it already exists
                                                                                        // xpEngine.GetSceneCopyByName
        {
            clipIndex = 0;
            // Set all needed variables to the assigned properties in the ClipPath
            ClipPathCollection clipPath = operatorButton.ClipPaths;
            if (clipPath == null || clipPath.Count == 0)
            {
                return;
            }
            //string? scene = clipPath[clipIndex].Scene;
            //string? scene = operatorButton.Scene;
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

            xpEngine XPression = new();
            if (XPression.GetSceneByName(scene, out xpScene SceneGraphic, true))
            {
                SetAllSceneMaterials(SceneGraphic, operatorButton.ObjectChanges);
                PlaySceneState(SceneGraphic, sceneDirector, clip, track, channel, layer);
            }
            else
            {
                CommonFunctions.ControlWarning(operatorButton, "Warning: " + scene + ">" + track + ">" + clip + " on button: " + operatorButton.Text + " could not be found!");
            }
        }
        #endregion
    }
}
#endif