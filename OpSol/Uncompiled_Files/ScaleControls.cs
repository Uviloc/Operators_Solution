using System.ComponentModel;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace OperatorsSolution
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(Button))]
    public class ScalableControl : Button
    {
        // FIXED, SCALE
        private float ScaleX { get; set; }
        private float ScaleY { get; set; }
        private float PosX { get; set; }
        private float PosY { get; set; }

        private Size OriginalSize { get; set; }
        private Point OriginalLocation { get; set; }

        // Whether the control should scale or just move, and which axis
        public bool ScaleHorizontally { get; set; } = true;
        public bool ScaleVertically { get; set; } = true;
        public bool ScalePosition { get; set; } = true;

        public ScalableControl()
        {
            // Initialize the control's size and location when the control is created
            //this.Resize += (s, e) => ScaleControlToParent();
        }

        // Called when the control is created (whether added via code or Toolbox)
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            // Only scale if the control is running (not in design mode)
            if (this.DesignMode)
                return;

            // Initialize original size and location when the control is created
            SaveOriginalState();

            // Attach to the parent's Resize event
            if (this.Parent != null)
            {
                this.Parent.Resize += Parent_Resize;
            }
        }

        // Handle parent resizing
        private void Parent_Resize(object? sender, EventArgs e)
        {
            ScaleControlToParent();
        }

        // Save the original size and location for relative scaling calculations
        private void SaveOriginalState()
        {
            if (OriginalSize == Size.Empty || OriginalLocation == Point.Empty)
            {
                OriginalSize = this.Size;
                OriginalLocation = this.Location;
            }
            if (this.Parent == null) return;
            //this.Text = "own height: " + this.Height + "\n" + "parents height: " + this.Parent.Height + "\n" + "end result:" + (float)this.Height/(float)this.Parent.Height;
            ScaleX  = (float)this.Width / (float)this.Parent.Width;
            ScaleY = (float)this.Height / (float)this.Parent.Height;
            PosX = (float)this.Location.X / (float)this.Parent.Width;
            PosY = (float)this.Location.Y / (float)this.Parent.Height;
            //this.Text = "width scale: " + ScaleX + "\n" + "height scale: " + ScaleY;
        }

        // Rescale the control based on the parent container's size
        public void ScaleControlToParent()
        {
            // Only scale if the control is running (not in design mode)
            if (this.DesignMode)
                return;

            if (this.Parent == null)
                return;

            // Check if the scale factors are valid (to prevent division by zero)
            if (ScaleX <= 0 || ScaleY <= 0)
                return;

            // Apply scaling to position if ScalePosition is true
            int newX = ScalePosition ? (int)((float)this.Parent.Width * (float)PosX) : OriginalLocation.X;
            int newY = ScalePosition ? (int)((float)this.Parent.Height * (float)PosY) : OriginalLocation.Y;
            this.Location = new Point(newX, newY);
            this.Text = "scale pos: " + ScalePosition + "\n" + "current loc: " + this.Location;

            // Apply scaling to size if ScaleHorizontally or ScaleVertically is true
            int newWidth = ScaleHorizontally ? (int)((float)Parent.Width * (float)ScaleX) : this.Width;
            int newHeight = ScaleVertically ? (int)((float)Parent.Height * (float)ScaleY) : this.Height;

            this.Size = new Size(newWidth, newHeight);
        }

        // Detach from the parent Resize event when the control is disposed
        protected override void Dispose(bool disposing)
        {
            if (disposing && this.Parent != null)
            {
                this.Parent.Resize -= Parent_Resize;
            }
            base.Dispose(disposing);
        }

        //// Override the OnResize method to call the scale method
        //protected override void OnResize(EventArgs e)
        //{
        //    base.OnResize(e);

        //    // Only scale after form has been fully initialized
        //    if (this.Parent != null && !this.DesignMode)
        //    {
        //        ScaleControlToParent();
        //    }
        //}
    }
}