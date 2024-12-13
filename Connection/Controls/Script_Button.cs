using OperatorsSolution.Common;
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
    public partial class Script_Button : OperatorButton
    {
        #region >----------------- Add properties: ---------------------

        #endregion

        public Script_Button()
        {
            Click += PlayScenes;
            Enter += DisplayPreview;
            Leave += RemovePreview;
        }

        #region >----------------- Functions: ---------------------
        private int index = 0;
        public void PlayScenes(object? sender, EventArgs e)
        {
            //// MOVE ELSEWHERE?????:
            //if (sender is Script_Button button && button.Scenes != null)            // SHOULD NOT REFERENCE CLIPPATHS
            //{
            //    // Warn and exit if there are no assigned scenes:
            //    if (button.Scenes.Count == 0)
            //    {
            //        CommonFunctions.ControlWarning(button, "Please add ClipPaths to the button: " + button.Text);
            //        return;
            //    }


            //    // Play the clip that this item is pointing to:
            //    GraphicsConnector.TriggerClip(button, index);

            //    if (index < button.Scenes.Count - 1)
            //    {
            //        index++;
            //    }
            //    else
            //    {
            //        index = 0;
            //    }
            //}
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
