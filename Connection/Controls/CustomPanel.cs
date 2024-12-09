using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatorsSolution.Controls
{
    internal class CustomPanel : Panel
    {
        public CustomPanel()
        {
            // Enable the panel to support transparency
            this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);
            this.BackColor = Color.Transparent;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.FillRectangle(new SolidBrush(this.BackColor), this.ClientRectangle);
        }
    }
}
