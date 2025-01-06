using OperatorsSolution.Common;
//using OperatorsSolution.Graphics_Program_Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Console = System.Diagnostics.Debug;


namespace OperatorsSolution.Controls
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(Button))]
    public partial class Toggle_Button : OperatorButton
    {
        #region >----------------- Add properties: ---------------------
        private string? _sceneName; // Backing field for SceneName

        // Scene Name
        [Category(".Operation > Search")]
        [Description("The name of the scene in the chosen graphics program.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(typeof(string), "[Show] SceneName")]
        public new string Text
        {
            //get => base.Text[7..];
            get => base.Text.StartsWith("[Show] ") ? base.Text.Substring(7) : base.Text;
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
            //Enter += DisplayPreview;
            //Leave += RemovePreview;
        }

        #region >----------------- Functions: ---------------------
        [Browsable(false)]
        public bool buttonIsOn = true;
        public void ToggleScene(object? sender, EventArgs e)
        {
            if (sender is not Toggle_Button button)
                return;

            if (this.FindForm() is not IFormPlugin parentForm)
                return;

            ApplicationSettingsBase settings = parentForm.ApplicationSettings;
            //if (settings["GraphicsSoftwareInfo"] is not GraphicsSoftwareInfo info || info.GraphicsSoftwareClassName is not string className)
            //{
            //    MessageBox.Show("There is no selected Graphics Software set in the forms settings. Please do so at the button next to the form.");
            //    return;
            //}

            string className = "OperatorsSolution.Graphics_Program_Functions.XPression";
            string methodName = "ToggleClip";
            object[]? parameters = [button, buttonIsOn];

            CommonFunctions.TriggerMethodBasedOnString(className, methodName, parameters);

            button.Text = buttonIsOn ? "[Hide] " + button.Text : "[Show] " + button.Text;
            buttonIsOn = !buttonIsOn;
        }


        //public void DisplayPreview(object? sender, EventArgs e)
        //{
        //    if (PreviewBox == null) return;
        //    //GraphicsConnector.DisplayPreview(sender, PreviewBox);
        //}

        //public void RemovePreview(object? sender, EventArgs e)
        //{
        //    if (PreviewBox == null) return;
        //    GraphicsConnector.RemovePreview(PreviewBox);
        //}
        #endregion
    }
}
