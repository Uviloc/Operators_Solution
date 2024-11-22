using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Reflection;
using OperatorsSolution.Common;


namespace OperatorsSolution.Controls
{
    #region >----------------- ObjectChanges Collection Class: ---------------------
    public class ObjectChange()
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
                return "No clip set!";
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
    public class ClipPath()
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
        Description("(OPTIONAL) What scene director the clip is located in. Default: Same as [Scene]"),
        DefaultValue("Same as [Scene]")]
        public string? SceneDirector { get; set; } = "Same as [Scene]";

        // Clip
        [Category("Search"),
        Description("Which clip in this scene will trigger.")]
        public string? Clip { get; set; }

        // Track
        [Category("Search"),
        Description("(OPTIONAL) Which clip track the clip is in. Default: 'StateTrack'."),
        DefaultValue("StateTrack")]
        public string? Track { get; set; } = "StateTrack";



        // Channel
        [Category("Output"),
        Description("On what channel the clip will be displayed."),
        DefaultValue(0)]
        public int Channel { get; set; } = 0;

        // Layer
        [Category("Output"),
        Description("On what layer the clip will be displayed."),
        DefaultValue(0)]
        public int Layer { get; set; } = 0;



        //// Object Changes
        //[Category("Changes"),
        //Description("Texts in the scene that need to be changed.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //public List<ObjectChange> ObjectChanges { get; set; } = [];


        // Override to string for nameplate
        public override string ToString()
        {
            if(string.IsNullOrWhiteSpace(Clip))
            {
                return "No clip set!";
            } else
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


    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(Button))]
    public class OperatorButton : Button
    {
        #region >----------------- Add properties: ---------------------
        // ClipPath
        [Category(".Operation > Search")]
        [Description("Add clips to be played here.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ClipPathCollection ClipPaths { get; set; } = [];

        // ScenePreview
        [Category(".Operation > Search")]
        [Description("The scene from which the preview is taken.")]
        public string? Scene { get; set; }

        // PreviewBox           TO BE MOVED TO PROJECT SETTINGS
        [Category(".Operation > Search")]
        [Description("The PictureBox where the preview will be displayed.")]
        public PictureBox? PreviewBox { get; set; }

        // ObjectChanges
        [Category(".Operation > Scene Changes")]
        [Description("A list of changes that are made to the scene before displaying.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<ObjectChange> ObjectChanges { get; set; } = [];
        #endregion

        #region >----------------- Recategorize some events: ---------------------

        [Category(".Operation > Events")]
        [Description("Occurs when the component is clicked.")]
        public new event EventHandler? Click;
        protected override void OnClick(EventArgs e)
        {
            // Call the base class method
            base.OnClick(e);

            // Check if there are any subscribers to the Click event
            if (Click == null || Click.GetInvocationList().Length == 0)
            {
                CommonFunctions.ControlWarning(this, "Please attach a function for the button: " + Text + "\n" + "See: Properties > Events (Lighting bolt) > Click");
            }
            else
            {
                // If there are subscribers, invoke the Click event
                Click?.Invoke(this, e);
            }
        }

        [Category(".Operation > Events")]
        [Description("Occurs when the mouse remains stationary inside of the control for an amount of time.")]
        public event EventHandler? Hover;
        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            Hover?.Invoke(this, e);
        }

        [Category(".Operation > Events")]
        [Description("Occurs when the mouse enters the visible part of the control.")]
        public new event EventHandler? Enter;
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Enter?.Invoke(this, e);
        }

        [Category(".Operation > Events")]
        [Description("Occurs when the mouse leaves the visible part of the control.")]
        public new event EventHandler? Leave;
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            Leave?.Invoke(this, e);
        }
        #endregion

        #region >----------------- Recategorize some properties: ---------------------

        // BackColor
        [Category(".Operation > Visuals")]
        [Description("The background color of the control.")]
        [DisplayName("Background Color")]
        [DefaultValue(typeof(Color), "110, 110, 110")]
        public new Color BackColor
        {
            get => base.BackColor;
            set => base.BackColor = value;
        }


        // ForeColor
        [Category(".Operation > Visuals")]
        [Description("The foreground color of this component, which is used to display text.")]
        [DisplayName("Font Color")]
        [DefaultValue(typeof(Color), "White")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        public new Color ForeColor
        {
            get => base.ForeColor;
            set => base.ForeColor = value;
        }


        // Font
        [Category(".Operation > Visuals")]
        [Description("The font used to display text with the control.")]
        [DisplayName("Font")]
        [DefaultValue(typeof(Font), "Arial, 11pt")]
        public new Font Font
        {
            get => base.Font;
            set => base.Font = value;
        }


        // FlatStyle
        [Category(".Operation > Visuals")]
        [Description("Determins the appearence of the control when a user moves the mouse over the control and clicks.")]
        [DisplayName("FlatStyle")]
        [DefaultValue(typeof(FlatStyle), "Popup")]
        public new FlatStyle FlatStyle
        {
            get => base.FlatStyle;
            set => base.FlatStyle = value;
        }


        // Cursor
        [Category(".Operation > Visuals")]
        [Description("The cursor that appears when the pointer moves over the control.")]
        [DisplayName("Cursor")]
        [DefaultValue(typeof(Cursor), "Default")]
        public new Cursor Cursor
        {
            get => base.Cursor;
            set
            {
                if (value == Cursors.Default)
                {
                    base.Cursor = cursorDefault;
                }
                else
                {
                    base.Cursor = value;
                }
            }
        }


        // Text
        [Category(".Operation > Visuals")]
        [Description("The text associated with the control.")]
        [DefaultValue(typeof(string), "Show [Scene]")]
        public new string Text
        {
            get => base.Text;
            set
            {
                if (value.Contains("operatorButton") || value == "Show [Scene]")
                {
                    base.Text = textDefault;
                }
                else
                {
                    base.Text = value;
                }
            }
        }
        #endregion

        #region >----------------- Set defaults: ---------------------
        private readonly Color backColorDefault = Color.FromArgb(110, 110, 110);
        private readonly Color foreColorDefault = Color.White;
        private readonly Font fontDefault = new("Arial", 11f);
        private readonly FlatStyle flatStyleDefault = FlatStyle.Popup;
        private readonly Cursor cursorDefault = Cursors.Hand;
        private readonly string textDefault = "Show [Scene]";

        public OperatorButton()
        {
            FlatStyle = flatStyleDefault;
            BackColor = backColorDefault;
            ForeColor = foreColorDefault;
            Cursor = cursorDefault;
            Font = fontDefault;
            Text = "Show [Scene]";
            Click += PlayScenes;
            Enter += DisplayPreview;
            Leave += RemovePreview;
            //Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Size = new Size(200, 150);
        }
        #endregion


        
        #region >----------------- Functions: ---------------------

        /*
        Needs to play the scene in the graphics program
        Needs to change the button text to the next clip
        Needs 
         */

        private int index = 0;
        public void PlayScenes(object? sender, EventArgs e)
        {
            // MOVE ELSEWHERE?????:
            if (sender is OperatorButton button && button.ClipPaths != null)
            {
                // Warn and exit if there are no assigned scenes:
                if (button.ClipPaths.Count == 0)
                {
                    CommonFunctions.ControlWarning(button, "Please add ClipPaths to the button: " + button.Text);
                    return;
                }


                // Play the clip that this item is pointing to:
                GraphicsConnector.TriggerClip(button, index);

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

        public void DisplayPreview(object? sender, EventArgs e)
        {
            if (PreviewBox == null) return;
            GraphicsConnector.DisplayPreview(sender, PreviewBox);
        }

        public void RemovePreview(object? sender, EventArgs e)
        {
            if (PreviewBox == null) return;
            GraphicsConnector.RemovePreview(PreviewBox);
        }
        #endregion
    }
}