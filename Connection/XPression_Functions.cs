#if HAS_XPRESSION
using XPression;
using System;
using System.Collections.Generic;
using System.Threading.Channels;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;


namespace OperatorsSolution
{
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
        public static void DisplayPreview(object? sender, PictureBox previewBox)
        {
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
                        previewBox.SizeMode = PictureBoxSizeMode.Zoom;
                        previewBox.Image = image;
                    }
                }
                else
                {
                    CommonFunctions.ControlWarning(button, "Warning: There is no Scene on button: " + button.Text + "!");
                }
            }
        }

        public static void RemovePreview(PictureBox previewBox)
        {
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

        private static Bitmap ConvertToBitmap(xpImage thumbNailImage)
        {
            Bitmap bmp;
            bmp = xpTools.xpImageToBitmap(thumbNailImage);
            return bmp;
        }
        #endregion

        #region >----------------- Trigger clip: ---------------------
        public static void TriggerClip(OperatorButton operatorButton, int clipIndex)
        {
            // Set all needed variables to the assigned properties in the ClipPath
            ClipPathCollection clipPath = operatorButton.ClipPaths;
            if (clipPath == null || clipPath.Count == 0)
            {
                return;
            }
            //string? scene = clipPath[clipIndex].Scene;
            string? scene = operatorButton.Scene;
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
                SetAllSceneMaterials(SceneGraphic, clipPath[clipIndex].ObjectChanges);
                PlaySceneState(SceneGraphic, sceneDirector, clip, track, channel, layer);
            }
            else
            {
                CommonFunctions.ControlWarning(operatorButton, "Warning: " + scene + ">" + track + ">" + clip + " on button: " + operatorButton.Text + " could not be found!");
            }
        }
        #endregion


        #region UNUSED:
        #region >----------------- XPression play preview scene: ---------------------
        ///// <summary>
        ///// Plays out the scene in XPression when this project is open in XPression.
        ///// </summary>
        ///// <param name = "scene">The xpScene in which the clip is located in.</param>
        ///// <param name = "sceneDirectorName">The name of the scene director.</param>
        ///// /// <param name = "clip">The name of the clip that needs to be played.</param>
        ///// <param name = "previewChannel">The channel on which the clip needs to play.</param>
        ///// <param name="layer">The layer on which the clip needs to play.</param>
        ///// <returns>true if succsesfull, false if the defaultFrame could not be found</returns>
        //public static bool PlayPreview(xpScene scene, string? sceneDirectorName, string clip, string? track, out xpImage? image, int previewChannel = 1, int layer = 1)
        //{
        //    image = null;
        //    // Set to be a default of the same name as its scene if it was not filled in
        //    if (string.IsNullOrWhiteSpace(sceneDirectorName) || sceneDirectorName == "Same as [Scene]")
        //    {
        //        sceneDirectorName = scene.Name;
        //        //sceneDirectorName = scene.SceneDirector
        //    }
        //    if (string.IsNullOrWhiteSpace(track))
        //    {
        //        track = "StateTrack";
        //    }

        //    if (!scene.GetSceneDirectorByName(sceneDirectorName, out xpSceneDirector Scene_Director) ||
        //        !Scene_Director.GetTrackByName(track, out xpSceneDirectorTrack Scene_State_Track) ||
        //        !Scene_State_Track.GetClipByName(clip, out xpSceneDirectorClip State_Clip))
        //    {
        //        return false;
        //    }

        //    ////int defaultFrame = Scene_Director.DefaultFrameMarker;
        //    //int lastFrameOfClip = State_Clip.Position + State_Clip.Duration - 1;
        //    //Scene_Director.PlayRange(lastFrameOfClip, lastFrameOfClip);
        //    //scene.SetOnline(previewChannel, layer);
        //    //Scene_Director.PlayRange(lastFrameOfClip, lastFrameOfClip + 1, true);
        //    ////scene.GetPreviewSceneDirector(out xpSceneDirector previewSD);
        //    ////previewSD.
        //    //scene.GetRenderedFrame(lastFrameOfClip, scene.Width, scene.Height, out image);

        //    scene.GetThumbnail(out image);
        //    return true;
        //}
        #endregion

        #region >----------------- XPression get single object: ---------------------
        /// <summary>
        /// Gives the material of the specified object in a scene, given that this project is open in XPression.
        /// </summary>
        /// <param name="objectName">The name of the object (in XPression) to find the material of.</param>
        /// <param name="scene">The scene in which this material needs to be searched.</param>
        /// <returns>The IxpBaseObject as the material.</returns>
        //public static IxpBaseObject getMaterialFromScene(string objectName, xpScene scene)
        //{
        //    scene.GetObjectByName(objectName, out xpBaseObject sceneObject);

        //    return sceneObject;
        //}
        #endregion

        #region >----------------- XPression play preview scene: ---------------------
        ///// <summary>
        ///// Plays out the scene in XPression when this project is open in XPression.
        ///// </summary>
        ///// <param name = "scene">The xpScene in which the clip is located in.</param>
        ///// <param name = "sceneDirectorName">The name of the scene director.</param>
        ///// <param name = "previewChannel">The channel on which the clip needs to play.</param>
        ///// <param name="layer">The layer on which the clip needs to play.</param>
        ///// <returns>true if succsesfull, false if the defaultFrame could not be found</returns>
        //public static bool StopPreview(xpScene scene)
        //{
        //    // Set to be a default of the same name as its scene if it was not filled in
        //    //if (string.IsNullOrWhiteSpace(sceneDirectorName) || sceneDirectorName == "Same as [Scene]")
        //    //{
        //    //    sceneDirectorName = scene.Name;
        //    //    //sceneDirectorName = scene.SceneDirector
        //    //}
        //    //if (string.IsNullOrWhiteSpace(track))
        //    //{
        //    //    track = "StateTrack";
        //    //}

        //    //if (!scene.GetSceneDirectorByName(sceneDirectorName, out xpSceneDirector Scene_Director) ||
        //    //    !Scene_Director.GetTrackByName(track, out xpSceneDirectorTrack Scene_State_Track))
        //    //{ return false; }

        //    //int defaultFrame = Scene_Director.DefaultFrameMarker;
        //    //Scene_Director.PlayRange(defaultFrame, defaultFrame + 1);
        //    scene.SetPreview();
        //    return true;
        //}
        #endregion
        #endregion
    }
}
#endif