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
    public class Material_Accessor : Button
    {
        public Color Color { get; set; }

        public Material_Accessor()
        {
            Click += OpenColorPanel;
            Color = Color.AliceBlue;                // Switch to getting from database or similar persistant location
        }

        private void OpenColorPanel(object? sender, EventArgs e)
        {
            ColorDialog colorDialog = new();
            colorDialog.ShowDialog();
            Color = colorDialog.Color;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            BackColor = Color;
        }
    }
}
