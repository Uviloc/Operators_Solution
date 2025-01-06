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
using Console = System.Diagnostics.Debug;


namespace OperatorsSolution.Controls
{
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(Button))]
    public partial class OperatorButton : Button
    {
        #region >----------------- Add properties: ---------------------
        // PreviewBox
        [Browsable(false)]
        public PictureBox? PreviewBox { get; set; }
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
        [DefaultValue(typeof(string), "[Show] Scene")]
        public new string Text
        {
            get => base.Text;
            set
            {
                if (value.Contains("operatorButton") || value == "[Show] Scene")
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
        private readonly string textDefault = "[Show] Scene";

        public OperatorButton()
        {
            FlatStyle = flatStyleDefault;
            BackColor = backColorDefault;
            ForeColor = foreColorDefault;
            Cursor = cursorDefault;
            Font = fontDefault;
            Text = "[Show] Scene";
            //Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Size = new Size(200, 150);

            if (FindForm() is PluginBaseForm form)
            {
                PreviewBox = form.PreviewBox;
            }
        }

        public override string ToString()
        {
            return Text;
        }
        #endregion
    }
}