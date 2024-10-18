using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Reflection;
using System.ComponentModel.Design;
using System.Diagnostics;
using Operators_Solution;

namespace OperatorsSolution
{
    public class ClipPath()
    {
        // Scene
        [Category(".Operation > Search"),
        Description("Which scene this button will trigger.")]
        public string? Scene { get; set; }

        // Scene Director
        [Category(".Operation > Search"),
        Description("(OPTIONAL) What scene director the clip is located in. Default: Same as [Scene]"),
        DefaultValue("Same as [Scene]")]
        public string? SceneDirector { get; set; } = "Same as [Scene]";

        // Clip
        [Category(".Operation > Search"),
        Description("Which clip in this scene will trigger.")]
        public string? Clip { get; set; }

        // Track
        [Category(".Operation > Search"),
        Description("(OPTIONAL) Which clip track the clip is in. Default: 'StateTrack'."),
        DefaultValue("StateTrack")]
        public string? Track { get; set; } = "StateTrack";




        // Channel
        [Category(".Operation > Output"),
        Description("On what channel the clip will be displayed."),
        DefaultValue(0)]
        public int Channel { get; set; } = 0;

        // Layer
        [Category(".Operation > Output"),
        Description("On what layer the clip will be displayed."),
        DefaultValue(0)]
        public int Layer { get; set; } = 0;
    }

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(Button))] // Optional addition: add icon with > [ToolboxBitmap(typeof(Button), "Button.bmp")]
    public class OperatorButton : Button
    {
        private readonly Color backColorDefault = Color.FromArgb(110, 110, 110);
        private readonly Color foreColorDefault = Color.White;
        private readonly Font fontDefault = new("Arial", 11f);
        private readonly FlatStyle flatStyleDefault = FlatStyle.Popup;
        private readonly Cursor cursorDefault = Cursors.Hand;
        private readonly string textDefault = "Show [Scene]";


        // ClipPath
        [Category(".Operation > Search"),
        Description("Add clips to be played here.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<ClipPath>? ClipPath { get; set; } = [];


        #region
        // Recategorize some properties: 
        //________________________________________________________________________________________________

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

        // Set defaults:
        //__________________________________________________________________________________________________

        public OperatorButton()
        {
            FlatStyle = flatStyleDefault;
            BackColor = backColorDefault;
            ForeColor = foreColorDefault;
            Cursor = cursorDefault;
            Font = fontDefault;
            Text = "Show [Scene]";
            //SceneDirector = "Same as [Scene]";
            //Track = "StateTrack";
            //this.Click += BlankFunction;
        }

        public void BlankFunction(EventArgs e, object? sender = null)
        {
            //this.Click -= BlankFunction;
            if (sender is OperatorButton button)
            {
                CommonFunctions.ControlWarning(button, "Please attach a function for the button: " + button.Text + "\n" + "See: Properties > Events (Lighting bolt) > Click");
            }
        }

        protected override void OnClick(EventArgs e)
        {
            //// Check if any additional handlers are attached to the Click event
            //if (Click != null && Click.GetInvocationList().Length == 1)
            //{
            //    // Only execute this if there are no other handlers
            //    BlankFunction(e);
            //}

            //// Call the base method to ensure the click event propagates properly
            //base.OnClick(e);
        }
    }




    public class OperatorComboBox : ComboBox
    {

    }
}