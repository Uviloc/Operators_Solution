using OperatorsSolution.GraphicsProgramFunctions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = System.Diagnostics.Debug;

namespace OperatorsSolution.Controls
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(Button))]
    public class Toggle_Button : OperatorButton
    {
        //Button Text
        //Scene Name
        //Scene changes
        //ClipPath in
        //ClipPath out


        #region >----------------- Add properties: ---------------------
        // Scene Name
        [Category(".Operation > Search")]
        [Description("The PictureBox where the preview will be displayed.")]                // CHANGE
        public string? SceneName { get; set; }

        // ClipPath in
        [Category(".Operation > Search")]
        [Description("The PictureBox where the preview will be displayed.")]                // CHANGE
        public ClipPath? ClipIn { get; set; }

        // ClipPath out
        [Category(".Operation > Search")]
        [Description("The PictureBox where the preview will be displayed.")]                // CHANGE
        public ClipPath? ClipOut { get; set; }
        #endregion
    }
}
