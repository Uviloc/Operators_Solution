using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Drawing.Design;
using OperatorsSolution;
using System.Windows.Forms.Design;


#if HAS_XPRESSION
using XPression;
#endif

namespace OperatorsSolution
{
    #region >----------------- Collection Classes: ---------------------
    public class ClipPath()
    {
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





        // Object Changes
        [Category("Changes"),
        Description("Texts in the scene that need to be changed.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<ObjectChange> ObjectChanges { get; set; } = [];



        //// ButtonText
        //[Category(".Operation > Button"),
        //Description("(OPTIONAL) What text the button will change to. Default: 'Show + Same as next [Clip]'."),
        //DefaultValue("Show + Same as next [Clip]")]
        //public string? ButtonText { get; set; } = "Show + Same as next [Clip]";
    }

    public class ObjectChange()
    {
        [Category("Object Change")]
        public string? SceneObject { get; set; }

        // SET LATER TO SOMETHING FROM DATA MANAGER
        [Category("Object Change")]
        public string? SetTo { get; set; }
    }


    // Done to dynamicly set the Scene to the previous Scene in the list
    public class ClipPathCollection : Collection<ClipPath>
    {
        protected override void InsertItem(int index, ClipPath item)
        {
            // If there are already items in the collection, set the new item's Scene to the last item's Scene
            if (this.Count > 0 && item.Scene == null)
            {
                item.Scene = this.Last().Scene;
            }

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
        [Category(".Operation > Search"),
        Description("Add clips to be played here.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ClipPathCollection ClipPaths { get; set; } = [];

        // ScenePreview
        [Category(".Operation > Search"),
        Description("The scene from which the preview is taken.")]
        public string? Scene { get; set; }
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
                CommonFunctions.ControlWarning(this, "Please attach a function for the button: " + this.Text + "\n" + "See: Properties > Events (Lighting bolt) > Click");
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
        public event EventHandler? Enter;
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseHover(e);
            Enter?.Invoke(this, e);
        }

        [Category(".Operation > Events")]
        [Description("Occurs when the mouse leaves the visible part of the control.")]
        public event EventHandler? Leave;
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseHover(e);
            Leave?.Invoke(this, e);
        }
        #endregion

        #region >----------------- Recategorize some properties: ---------------------

        // BackColor
        [Category(".Operation > Visuals"),
        Description("The background color of the control."),
        DisplayName("Background Color"),
        DefaultValue(typeof(Color), "110, 110, 110")]
        public new Color BackColor
        {
            get => base.BackColor;
            set => base.BackColor = value;
        }


        // ForeColor
        [Category(".Operation > Visuals"),
        Description("The foreground color of this component, which is used to display text."),
        DisplayName("Font Color"),
        DefaultValue(typeof(Color), "White")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        public new Color ForeColor
        {
            get => base.ForeColor;
            set => base.ForeColor = value;
        }


        // Font
        [Category(".Operation > Visuals"),
        Description("The font used to display text with the control."),
        DisplayName("Font"),
        DefaultValue(typeof(Font), "Arial, 11pt")]
        public new Font Font
        {
            get => base.Font;
            set => base.Font = value;
        }


        // FlatStyle
        [Category(".Operation > Visuals"),
        Description("Determins the appearence of the control when a user moves the mouse over the control and clicks."),
        DisplayName("FlatStyle"),
        DefaultValue(typeof(FlatStyle), "Popup")]
        public new FlatStyle FlatStyle
        {
            get => base.FlatStyle;
            set => base.FlatStyle = value;
        }


        // Cursor
        [Category(".Operation > Visuals"),
        Description("The cursor that appears when the pointer moves over the control."),
        DisplayName("Cursor"),
        DefaultValue(typeof(Cursor), "Default")]
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
        [Category(".Operation > Visuals"),
        Description("The text associated with the control."),
        DefaultValue(typeof(string), "Show [Scene]")]
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
        }
        #endregion
    }
}



