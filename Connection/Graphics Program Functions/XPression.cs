#if HAS_XPRESSION
using XPression;
using System;
using System.Collections.Generic;
using System.Threading.Channels;
using OperatorsSolution.Common;
using OperatorsSolution.Controls;
using System.Reflection;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Reflection.Emit;
using Console = System.Diagnostics.Debug;
using System.Collections.ObjectModel;
using System.ComponentModel;
using OperatorsSolution.Graphics_Program_Functions;

namespace OperatorsSolution.Controls
{
    partial class OperatorButton
    {
        #region >----------------- Add properties: ---------------------
        #endregion
    }

    partial class Script_Button
    {
        #region >----------------- Add properties: ---------------------
        public partial class Scene
        {
            // ScenePreview
            [Category(".Operation > Search")]
            [Description("The scene from which the preview is taken.")]
            public string? SceneName { get; set; }

            [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
            public ClipPathCollection ClipPaths { get; set; } = [];

            // ObjectChanges
            [Category(".Operation > Scene Changes")]
            [Description("A list of changes that are made to the scene before displaying.")]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
            public List<ObjectChange> ObjectChanges { get; set; } = [];
        }
        #endregion
    }

    partial class Toggle_Button
    {
        #region >----------------- Add properties: ---------------------
        // ClipPath in
        [Category(".Operation > Search")]
        [Description("The clipPath for showing the scene.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ClipPath ClipIn { get; set; } = new();

        // ClipPath out
        [Category(".Operation > Search")]
        [Description("The clipPath for hiding the scene.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ClipPath ClipOut { get; set; } = new();

        // ObjectChanges
        [Category(".Operation > Scene Changes")]
        [Description("A list of changes that are made to the scene before displaying.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<ObjectChange> ObjectChanges { get; set; } = [];
        #endregion
    }
}


namespace OperatorsSolution.Graphics_Program_Functions
{
    #region >----------------- ObjectChanges Class: ---------------------
    public class ObjectChange
    {
        [Category("Object Change")]
        public string? SceneObject { get; set; }

        // SET LATER TO SOMETHING FROM DATA MANAGER
        [Category("Object Change")]
        public string? SetTo { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(SceneObject))
            {
                return "No clipPath set!";
            }
            else
            {
                return $"{SceneObject}";
            }
        }
    }

    //public class ObjectChangeCollection : Collection<ObjectChange>
    //{
    //    protected override void InsertItem(int index, ClipPath item)
    //    {
    //        // If there are already items in the collection, set the new item's Scene to the last item's Scene
    //        if (Count > 0)
    //        {
    //            item.Scene ??= this.Last().Scene;


    //            // STILL CHANGES PREVIOUS ITEMS WHEN THEY WERE STATETRACK AND NEW ONE IS SET TO STATETRACK
    //            if (item.Track == "StateTrack")
    //            {
    //                //item.Track = this.Last().Track ?? "StateTrack";
    //                //item.Track = this.Last().Track != "StateTrack" && this.Last().Track != "" ? this.Last().Track : "StateTrack";
    //            }
    //            if (item.SceneDirector == null || item.SceneDirector == "Same as [Scene]")
    //            {
    //                //item.SceneDirector = this.Last().SceneDirector;
    //            }
    //        }

    //        base.InsertItem(index, item);
    //    }
    //}
    #endregion

    #region >----------------- ClipPath Collection Class: ---------------------
    public class ClipPath
    {
        //// ButtonText
        //[Category(".Operation > Button"),
        //Description("(OPTIONAL) What text the button will change to. Default: 'Show + Same as next [Clip]'."),
        //DefaultValue("Show + Same as next [Clip]")]
        //public string? ButtonText { get; set; } = "Show + Same as next [Clip]";


        // Scene
        [Category("Search"),
        Description("Which scene this button will trigger.")]
        public string? Scene { get; set; }

        // Scene Director
        [Category("Search"),
        Description("(OPTIONAL) What scene director the clipPath is located in. Default: Same as [Scene]"),
        DefaultValue("Same as [Scene]")]
        public string? SceneDirector { get; set; } = "Same as [Scene]";

        // Clip
        [Category("Search"),
        Description("Which clipPath in this scene will trigger.")]
        public string? Clip { get; set; }

        // Track
        [Category("Search"),
        Description("(OPTIONAL) Which clipPath track the clipPath is in. Default: 'StateTrack'."),
        DefaultValue("StateTrack")]
        public string? Track { get; set; } = "StateTrack";



        // Channel
        [Category("Output"),
        Description("On what channel the clipPath will be displayed."),
        DefaultValue(0)]
        public int Channel { get; set; } = 0;

        // Layer
        [Category("Output"),
        Description("On what layer the clipPath will be displayed."),
        DefaultValue(0)]
        public int Layer { get; set; } = 0;


        public xpScene? SavedScene;



        //// Object Changes
        //[Category("Changes"),
        //Description("Texts in the scene that need to be changed.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //public List<ObjectChange> ObjectChanges { get; set; } = [];


        // Override to string for nameplate
        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(Clip))
            {
                return "No clipPath set!";
            }
            else
            {
                return $"{Clip}";
            }
        }
    }


    public class ClipPathCollection : Collection<ClipPath>
    {
        protected override void InsertItem(int index, ClipPath item)
        {
            // If there are already items in the collection, set the new item's Scene to the last item's Scene
            if (Count > 0) item.Scene ??= this.Last().Scene;

            //if (Count > 0)
            //{
            //    item.Scene ??= this.Last().Scene;


            //    // STILL CHANGES PREVIOUS ITEMS WHEN THEY WERE STATETRACK AND NEW ONE IS SET TO STATETRACK
            //    if (item.Track == "StateTrack")
            //    {
            //        //item.Track = this.Last().Track ?? "StateTrack";
            //        //item.Track = this.Last().Track != "StateTrack" && this.Last().Track != "" ? this.Last().Track : "StateTrack";
            //    }
            //    if (item.SceneDirector == null || item.SceneDirector == "Same as [Scene]")
            //    {
            //        //item.SceneDirector = this.Last().SceneDirector;
            //    }
            //}

            base.InsertItem(index, item);
        }
    }
    #endregion

    #region >----------------- Scenes Class: ---------------------
    //public class Scene                      // SHOULD KEEP INVENTORY OF SCENES SO DATACHANGES DONT GET LOST
    //{
    //    private xpScene[]? Scenes { get; set; }
    //    [Category("Scene")]
    //    public string? SceneName { get; set; }

    //    // Object Changes
    //    [Category("Scene")]
    //    [Description("Texts in the scene that need to be changed.")]
    //    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    //    public List<ObjectChange> ObjectChanges { get; set; } = [];

    //    [Category("Clips")]
    //    public bool PlayAllClipPathsAtOnce { get; set; }

    //    // ClipPaths
    //    [Category("Clips")]
    //    [Description("Add clips to be played here.")]
    //    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    //    public ClipPathCollection ClipPaths { get; set; } = [];

    //    public override string ToString()
    //    {
    //        if (string.IsNullOrWhiteSpace(SceneName))
    //        {
    //            return "No scene set!";
    //        }
    //        else
    //        {
    //            return $"{SceneName}";
    //        }
    //    }
    //}
    #endregion


    /// <summary>
    /// All functions for the XPression Graphics Program.
    /// </summary>
    internal class XPression : IGraphicProgram
    {
        public GraphicsSoftwareInfo GraphicsSoftwareInfo => new("OperatorsSolutions.Graphics_Program_Functions.XPression", "xPression", "XPression files (*.xpf;*.xpp)|*.xpf;*.xpp");

        public static xpEngine? XpEngine;

        /// <summary>
        /// Sets the xpEngine if XPression is working (mostly used to check if XPression dongle with licence is present).
        /// </summary>
        /// /// <returns>The xpEngine if succsesfull, null if the XPression dongel is not present.</returns>
        public static xpEngine? CheckForDongle()
        {
            try
            {
                XpEngine ??= new();
                return XpEngine;
            }
            catch
            {
                MessageBox.Show("XPression Dongle is not connected. Features are disabled.");
            }
            return null;
        }

        




        #region >----------------- XPression play scene: ---------------------
        /// <summary>
        /// Plays out the scene in XPression when this project is open in XPression.
        /// </summary>
        /// <param name = "scene">The xpScene in which the clipPath is located in.</param>
        /// <param name = "sceneDirectorName">The name of the scene director.</param>
        /// <param name = "clip">The name of the clipPath that needs to be played.</param>
        /// <param name = "channel">The channel on which the clipPath needs to play.</param>
        /// <param name="layer">The layer on which the clipPath needs to play.</param>
        /// <returns>true if succsesfull, false if the clipPath could not be found</returns>
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
            if (!scene.GetObjectByName(objectChange.SceneObject, out xpBaseObject baseObject)) return; // Exit in case no scene object has been found

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
            //if (sender is OperatorButton button && button.SceneName != null)
            //{
            //    string scene = button.SceneName;

            //    xpEngine XPression = new();
            //    if (XPression.GetSceneByName(scene, out xpScene sceneGraphic, true))
            //    {
            //        int preveiwLocation = sceneGraphic.DefaultPreviewFrame;
            //        sceneGraphic.GetRenderedFrame(preveiwLocation, sceneGraphic.Width, sceneGraphic.Height, out imageOut);
            //        previewBox.SizeMode = PictureBoxSizeMode.Zoom;
            //        //RenderRawPixelsDirectly(previewBox, imageOut);

            //        Bitmap image = ConvertToBitmap(imageOut);
            //        previewBox.Image = image;
            //    }
            //    else
            //    {
            //        CommonFunctions.ControlWarning(button, "Warning: There is no Scene on button: " + button.Text + "!");
            //    }
            //}
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
        /// <param name = "scene">The xpScene in which the clipPath is located in.</param>
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



        //private static void RenderRawPixelsDirectly(PictureBox pictureBox, xpImage image)
        //{
        //    var adapter = image.Adapter;

        //    if (adapter is Array byteArray)
        //    {
        //        byte[] rawData = new byte[byteArray.Length];
        //        Buffer.BlockCopy(byteArray, 0, rawData, 0, rawData.Length);

        //        using var ms = new MemoryStream(rawData);
        //        Bitmap bmp = new(ms);

        //        // Assign the bitmap to the PictureBox.Image property
        //        pictureBox.Image?.Dispose(); // Dispose of the old image
        //        pictureBox.Image = bmp;
        //        pictureBox.Invalidate(); // Force redraw
        //    }
        //}


        #region >----------------- Trigger clip: ---------------------
        /// <summary>
        /// Plays out the clipPath at clipIndex given in the operatorButton in XPression.
        /// </summary>
        /// <param name = "operatorButton">The button control that has the clipPath path list.</param>
        /// <param name = "clipIndex">Which clipPath to trigger in the clipPath path list.</param>
        public static void TriggerClip(OperatorButton operatorButton, int clipIndex)    // "PUBLIC STATIC XPSCENE", OR "OUT XPSCENE" then use this to call the same scene if it already exists
                                                                                        // xpEngine.GetSceneCopyByName
        {
            //clipIndex = 0;
            //// Set all needed variables to the assigned properties in the ClipPath
            //ClipPathCollection clipPath = operatorButton.ClipPaths;
            //if (clipPath == null || clipPath.Count == 0)
            //{
            //    return;
            //}
            ////string? scene = clipPath[clipIndex].Scene;
            ////string? scene = operatorButton.Scene;
            //string? scene = clipPath[clipIndex].Scene;
            //string? clipPath = clipPath[clipIndex].Clip;
            //string? track = clipPath[clipIndex].Track;
            //string? sceneDirector = clipPath[clipIndex].SceneDirector;
            //int channel = clipPath[clipIndex].Channel;
            //int layer = clipPath[clipIndex].Layer;

            //// Check if any of the fields are empty:
            //if (string.IsNullOrWhiteSpace(scene) ||
            //    string.IsNullOrWhiteSpace(clipPath))
            //{
            //    CommonFunctions.ControlWarning(operatorButton, "Warning: Scene on button: " + operatorButton.Text + " must be set!");
            //    return;
            //}

            //xpEngine XPression = new();
            //if (XPression.GetSceneByName(scene, out xpScene SceneGraphic, true))
            //{
            //    //SetAllSceneMaterials(SceneGraphic, operatorButton.ObjectChanges);
            //    PlaySceneState(SceneGraphic, sceneDirector, clipPath, track, channel, layer);
            //}
            //else
            //{
            //    CommonFunctions.ControlWarning(operatorButton, "Warning: " + scene + ">" + track + ">" + clipPath + " on button: " + operatorButton.Text + " could not be found!");
            //}
        }


        public static void PlayScriptClip(object? sender, int index)
        {
            if (sender is not Script_Button button)
                return;

            // Warn and exit if there are no assigned scenes:
            if (button.Scenes.Count == 0)
            {
                CommonFunctions.ControlWarning(button, "Please add Clips to the button: " + button.Text);
                return;
            }


            //SOMEHOW MAKE IT SO THAT INDEX COUNTS CLIPS INSIDE SCENES THEN THE NEXT SCENE


            //ClipPath clipPath = button.Scenes[index].ClipPaths
        }

        public static void ToggleClip(object sender, bool isOn)
        {
            if (sender is not Toggle_Button button)
                return;

            // Warn and exit if there are no assigned scenes:
            if (button.ClipIn is not ClipPath clipIn || button.ClipOut is not ClipPath clipOut)
            {
                CommonFunctions.ControlWarning(button, "Please add Clips to the button: " + button.Text);
                return;
            }

            ClipPath clip = isOn ? clipIn : clipOut;

            TriggerClip(sender, clip, button.ObjectChanges);
        }


        private static void TriggerClip(object sender, ClipPath clipPath, List<ObjectChange> objectChanges)
        {
            if (CheckForDongle() is not xpEngine xpEngine)
                return;

            if (sender is not OperatorButton control)
                return;

            
            string? scene = clipPath.Scene;
            string? clip = clipPath.Clip;
            string? track = clipPath.Track;
            string? sceneDirector = clipPath.SceneDirector;
            int channel = clipPath.Channel;
            int layer = clipPath.Layer;

            // Check if any of the fields are empty:
            if (string.IsNullOrWhiteSpace(scene) ||
                string.IsNullOrWhiteSpace(clip))
            {
                CommonFunctions.ControlWarning(control, "Warning: Scene on button: " + control.Text + " must be set!");
                return;
            }

            if (clipPath.SavedScene == null)
            {
                Console.WriteLine("CREATING NEW SCENE");
            }

            xpScene? SceneGraphic = clipPath.SavedScene ?? (xpEngine.GetSceneByName(scene, out xpScene outScene, true) ? outScene : null);
            if (SceneGraphic == null)
            {
                CommonFunctions.ControlWarning(control, "Warning: " + scene + ">" + track + ">" + clipPath + " on button: " + control.Text + " could not be found!");
                return;
            }

            clipPath.SavedScene = SceneGraphic;

            SetAllSceneMaterials(SceneGraphic, objectChanges);
            PlaySceneState(SceneGraphic, sceneDirector, clip, track, channel, layer);
        }
        #endregion
    }
}
#endif