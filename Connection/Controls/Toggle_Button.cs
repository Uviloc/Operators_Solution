using OperatorsSolution.Common;
//using OperatorsSolution.Graphics_Program_Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Console = System.Diagnostics.Debug;


namespace OperatorsSolution.Controls
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(Button))]
    public partial class Toggle_Button : OperatorButton
    {
        //[Browsable(false)]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //private new ClipPathCollection ClipPaths { get; set; }


        #region >----------------- Add properties: ---------------------
        private string? _sceneName; // Backing field for SceneName

        // Scene Name
        [Category(".Operation > Search")]
        [Description("The name of the scene in the chosen graphics program.")]
        public string? SceneName
        {
            get => _sceneName;
            set
            {
                if (_sceneName != value) // Avoid unnecessary updates
                {
                    if (Text.Contains(_sceneName ?? "") || Text.Contains("toggle_Button") || Text.Contains("SceneName") || string.IsNullOrWhiteSpace(Text))
                        base.Text = "[Show] " + (value ?? "");
                    
                    _sceneName = value;
                }
            }
        }

        // Text
        [Category(".Operation > Visuals")]
        [Description("The text associated with the control.")]
        [DefaultValue(typeof(string), "[Show] SceneName")]
        public new string Text
        {
            get => base.Text[7..];
            set
            {
                bool getNameFromScene = value.Contains("toggle_Button") || value.Contains("SceneName") || string.IsNullOrWhiteSpace(value);

                if (value.StartsWith("[Show] ") || value.StartsWith("[Hide] "))
                    base.Text = getNameFromScene ? (_sceneName ?? "") : value;
                else
                    base.Text = getNameFromScene ? "[Show] " + (_sceneName ?? "") : "[Show] " + value;
            }
        }
        #endregion


        public Toggle_Button()
        {
            Click += ToggleScene;
            Enter += DisplayPreview;
            Leave += RemovePreview;
        }

        #region >----------------- Functions: ---------------------
        private bool buttonIsOn = false;
        public void ToggleScene(object? sender, EventArgs e)
        {
            if (sender is not Toggle_Button button)
                return;

            // Warn and exit if there are no assigned scenes:
            if (button.ClipIn == null || button.ClipOut == null)                                                    // MUST BE IN XPRESSION STUFF
            {
                CommonFunctions.ControlWarning(button, "Please add Clips to the button: " + button.Text);
                return;
            }

            if (!buttonIsOn)
            {
                //GraphicsConnector.TriggerClip(SceneName, ClipIn);
                //Form? parentForm = this.FindForm();
                //Console.WriteLine(parentForm?.Name);
                button.Text = "[Hide] " + button.Text;
                buttonIsOn = true;
            }
            else
            {
                //GraphicsConnector.TriggerClip(SceneName, ClipOut);
                button.Text = "[Show] " + button.Text;
                buttonIsOn = false;
            }
        }

        public void DisplayPreview(object? sender, EventArgs e)
        {
            if (PreviewBox == null) return;
            //GraphicsConnector.DisplayPreview(sender, PreviewBox);
        }

        public void RemovePreview(object? sender, EventArgs e)
        {
            if (PreviewBox == null) return;
            GraphicsConnector.RemovePreview(PreviewBox);
        }
        #endregion
    }
}
