using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace OperatorsSolution
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(Button))] // Optional addition: add icon with > [ToolboxBitmap(typeof(Button), "Button.bmp")]
    public class OperatorButton : Button
    {
        private Color backColorDefault = Color.FromArgb(110, 110, 110);
        private Color foreColorDefault = Color.White;
        private Font fontDefault = new Font("Arial", 11f);
        private FlatStyle flatStyleDefault = FlatStyle.Popup;
        private Cursor cursorDefault = Cursors.Hand;
        private string textDefault = "Show [Scene]";



        // Scene
        [Category(".Operation > Search"),
        Description("Which scene this button will trigger.")]
        public string? Scene { get; set; }

        // Scene Director
        [Category(".Operation > Search"),
        Description("(OPTIONAL) What scene director the clip is located in. Default: Same as [Scene]"),
        DefaultValue("Same as [Scene]")]
        public string? SceneDirector { get; set; }

        // Clip
        [Category(".Operation > Search"),
        Description("Which clip in this scene will trigger.")]
        public string? Clip { get; set; }

        // Track
        [Category(".Operation > Search"),
        Description("(OPTIONAL) Which clip track the clip is in. Default: 'StateTrack'."),
        DefaultValue("StateTrack")]
        public string? Track { get; set; }



        // Channel
        [Category(".Operation > Output"),
        Description("On what channel the clip will be displayed."),
        DefaultValue(0)]
        public int Channel { get; set; }

        // Layer
        [Category(".Operation > Output"),
        Description("On what layer the clip will be displayed."),
        DefaultValue(0)]
        public int Layer { get; set; }



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
            SceneDirector = "Same as [Scene]";
            Track = "StateTrack";
            this.Click += testFunction;
        }

        public void testFunction(object? sender, EventArgs e)
        {
            OpSol_Form opSol_Form = new OpSol_Form();
            this.Click -= testFunction;
            this.Click += opSol_Form.Btn_TriggerScene_Click;
            opSol_Form.Btn_TriggerScene_Click(sender, e);
        }
    }




    public class OperatorComboBox : ComboBox
    {

    }
}