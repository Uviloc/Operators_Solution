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
        /// <param name = "Scene_Name">The xpScene in which the clip is located in.</param>
        /// <param name = "SD_Name">The name of the scene director.</param>
        /// <param name = "SD_Clip">The name of the clip that needs to be played.</param>
        /// <param name = "channel">The channel on which the clip needs to play.</param>
        /// <param name="layer">The layer on which the clip needs to play.</param>
        /// <returns>true if succsesfull, false if the clip could not be found</returns>
        public static bool PlaySceneState(xpScene Scene_Name, string? SD_Name, string SD_Clip, string? track, int channel = 0, int layer = 0)
        {
            // Set to be a default of the same name as its scene if it was not filled in
            if (string.IsNullOrWhiteSpace(SD_Name) || SD_Name == "Same as [Scene]")
            {
                SD_Name = Scene_Name.ToString();
            }
            if (string.IsNullOrWhiteSpace(track))
            {
                track = "StateTrack";
            }

            if (!Scene_Name.GetSceneDirectorByName(SD_Name, out xpSceneDirector Scene_Director) ||
                !Scene_Director.GetTrackByName(track, out xpSceneDirectorTrack Scene_State_Track) ||
                !Scene_State_Track.GetClipByName(SD_Clip, out xpSceneDirectorClip State_Clip))
            { return false; }

            Scene_Director.PlayRange(State_Clip.Position, State_Clip.Position);
            Scene_Name.SetOnline(channel, layer);
            Scene_Director.PlayRange(State_Clip.Position, State_Clip.Duration + State_Clip.Position, true);
            return true;
        }
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
    }
}
#endif