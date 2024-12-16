using OperatorsSolution.Common;
using OperatorsSolution.GraphicsProgramFunctions;
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
                    _sceneName = value;
                    base.Text = "[Show] " + (_sceneName ?? "");
                }
            }
        }

        // Text
        [Category(".Operation > Visuals")]
        [Description("The text associated with the control.")]
        [DefaultValue(typeof(string), "[Show] SceneName")]
        public new string Text
        {
            get => base.Text;
            set
            {
                if (value.Contains("toggle_Button") || value == "[Show] SceneName")
                {
                    base.Text = "[Show] " + (_sceneName ?? ""); // Use SceneName if available
                }
                else
                {
                    base.Text = value;
                }
            }
        }
        #endregion


        public Toggle_Button()
        {
            Click += ToggleScene;
            Enter += DisplayPreview;
            Leave += RemovePreview;
            //Text = "[Show] " + SceneName;
        }

        #region >----------------- Functions: ---------------------
        private bool buttonIsOn = false;
        public void ToggleScene(object? sender, EventArgs e)
        {
            if (sender is not Toggle_Button button)
                return;

            // Warn and exit if there are no assigned scenes:
            if (button.ClipIn == null || button.ClipOut == null)
            {
                CommonFunctions.ControlWarning(button, "Please add Clips to the button: " + button.Text);
                return;
            }

            if (!buttonIsOn)
            {
                //GraphicsConnector.TriggerClip(SceneName, ClipIn);
                button.Text = "[Hide] " + SceneName;
                buttonIsOn = true;
            }
            else
            {
                //GraphicsConnector.TriggerClip(SceneName, ClipOut);
                button.Text = "[Show] " + SceneName;
                buttonIsOn = false;
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
