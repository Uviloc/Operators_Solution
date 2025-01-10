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
    /// <summary>
    /// Represents a custom button control with enhanced functionality and customizable appearance.
    /// The button includes additional properties, categorized events, and default styling to streamline
    /// operations in the application.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(Button))]
    public partial class OperatorButton : Button
    {
        #region >----------------- Add properties: ---------------------
        /// <summary>
        /// Gets or sets the associated preview box for the operator button.
        /// </summary>
        [Browsable(false)]
        public PictureBox? PreviewBox { get; set; }
        #endregion

        #region >----------------- Recategorize some events: ---------------------
        /// <summary>
        /// Occurs when the component is clicked.
        /// </summary>
        [Category(".Operation > Events")]
        [Description("Occurs when the component is clicked.")]
        public new event EventHandler? Click;

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (Click == null || Click.GetInvocationList().Length == 0)
            {
                CommonFunctions.ControlWarning(this, "Please attach a function for the button: " + Text + "\n" + "See: Properties > Events (Lighting bolt) > Click");
            }
            else
            {
                Click?.Invoke(this, e);
            }
        }

        /// <summary>
        /// Occurs when the mouse remains stationary inside the control for a specified duration.
        /// </summary>
        [Category(".Operation > Events")]
        [Description("Occurs when the mouse remains stationary inside of the control for an amount of time.")]
        public event EventHandler? Hover;

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            Hover?.Invoke(this, e);
        }

        /// <summary>
        /// Occurs when the mouse enters the visible part of the control.
        /// </summary>
        [Category(".Operation > Events")]
        [Description("Occurs when the mouse enters the visible part of the control.")]
        public new event EventHandler? Enter;

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Enter?.Invoke(this, e);
        }

        /// <summary>
        /// Occurs when the mouse leaves the visible part of the control.
        /// </summary>
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
        /// <summary>
        /// Gets or sets the background color of the control.
        /// </summary>
        [Category(".Operation > Visuals")]
        [Description("The background color of the control.")]
        [DisplayName("Background Color")]
        [DefaultValue(typeof(Color), "110, 110, 110")]
        public new Color BackColor
        {
            get => base.BackColor;
            set => base.BackColor = value;
        }

        /// <summary>
        /// Gets or sets the foreground color of the control's text.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the font used to display text on the control.
        /// </summary>
        [Category(".Operation > Visuals")]
        [Description("The font used to display text with the control.")]
        [DisplayName("Font")]
        [DefaultValue(typeof(Font), "Arial, 11pt")]
        public new Font Font
        {
            get => base.Font;
            set => base.Font = value;
        }

        /// <summary>
        /// Gets or sets the flat style appearance of the control.
        /// </summary>
        [Category(".Operation > Visuals")]
        [Description("Determines the appearance of the control when a user moves the mouse over the control and clicks.")]
        [DisplayName("FlatStyle")]
        [DefaultValue(typeof(FlatStyle), "Popup")]
        public new FlatStyle FlatStyle
        {
            get => base.FlatStyle;
            set => base.FlatStyle = value;
        }

        /// <summary>
        /// Gets or sets the cursor displayed when the pointer is over the control.
        /// </summary>
        [Category(".Operation > Visuals")]
        [Description("The cursor that appears when the pointer moves over the control.")]
        [DisplayName("Cursor")]
        [DefaultValue(typeof(Cursor), "Default")]
        public new Cursor Cursor
        {
            get => base.Cursor;
            set => base.Cursor = value == Cursors.Default ? cursorDefault : value;
        }

        /// <summary>
        /// Gets or sets the text associated with the control.
        /// </summary>
        [Category(".Operation > Visuals")]
        [Description("The text associated with the control.")]
        [DefaultValue(typeof(string), "[Show] Scene")]
        public new string Text
        {
            get => base.Text;
            set => base.Text = value.Contains("operatorButton") || value == "[Show] Scene" ? textDefault : value;
        }
        #endregion

        #region >----------------- Set defaults: ---------------------
        private readonly Color backColorDefault = Color.FromArgb(110, 110, 110);
        private readonly Color foreColorDefault = Color.White;
        private readonly Font fontDefault = new("Arial", 11f);
        private readonly FlatStyle flatStyleDefault = FlatStyle.Popup;
        private readonly Cursor cursorDefault = Cursors.Hand;
        private readonly string textDefault = "[Show] Scene";

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorButton"/> class with default settings.
        /// </summary>
        public OperatorButton()
        {
            FlatStyle = flatStyleDefault;
            BackColor = backColorDefault;
            ForeColor = foreColorDefault;
            Cursor = cursorDefault;
            Font = fontDefault;
            Text = textDefault;
            Size = new Size(200, 150);

            if (FindForm() is PluginBaseForm form)
                PreviewBox = form.PreviewBox;
        }

        public override string ToString()
        {
            return Text;
        }
        #endregion
    }
}