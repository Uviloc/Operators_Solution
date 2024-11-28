using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Console = System.Diagnostics.Debug;

namespace OperatorsSolution.Controls
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(Button))]
    public class Logic_Button : Control
    {
        private int buttonAmount = 3;
        private readonly List<Button> buttons = [];

        [Category(".Operation > Layout")]
        [Description("The number of buttons to display.")]
        [DefaultValue(3)]
        public int ButtonAmount
        {
            get => buttonAmount;
            set
            {
                if (value != buttonAmount && value >= 1)
                {
                    buttonAmount = value;
                    UpdateButtons();
                }
            }
        }

        public Logic_Button()
        {
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            // Remove existing buttons
            foreach (var button in buttons)
            {
                Controls.Remove(button);
                button.Dispose();
            }

            buttons.Clear();

            // Create new buttons
            for (int i = 0; i < buttonAmount; i++)
            {
                var button = new OperatorButton
                {
                    Text = $"Button {i + 1}",
                    Parent = this // Automatically adds to Controls
                };
                buttons.Add(button);
            }

            PerformLayout(); // Trigger layout recalculation
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            PerformLayout(); // Trigger layout recalculation on resize
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            // Layout logic
            int width = ClientSize.Width;
            int height = ClientSize.Height;

            if (buttons.Count == 0)
            {
                return;
            }
            else if (buttons.Count == 1)
            {
                // Single button fills the entire control
                buttons[0].SetBounds(0, 0, width, height);
            }
            else if (buttons.Count == 2)
            {
                // Two buttons split vertically
                buttons[0].SetBounds(0, 0, width, height / 2);
                buttons[1].SetBounds(0, height / 2, width, height / 2);
            }
            else if (buttons.Count == 3)
            {
                // One large button on top and two buttons side-by-side at the bottom
                int halfHeight = height / 2;
                int bottomHeight = height - halfHeight;

                buttons[0].SetBounds(0, 0, width, halfHeight);
                buttons[1].SetBounds(0, halfHeight, width / 2, bottomHeight);
                buttons[2].SetBounds(width / 2, halfHeight, width / 2, bottomHeight);
            }
            else if (buttons.Count == 4)
            {
                // One large button on top and two buttons side-by-side at the bottom
                int halfHeight = height / 2;
                int bottomHeight = height - halfHeight;

                int thirdWidth = width / 3;

                buttons[0].SetBounds(0, 0, width, halfHeight);
                buttons[1].SetBounds(0, halfHeight, thirdWidth, bottomHeight);
                buttons[2].SetBounds(thirdWidth, halfHeight, thirdWidth, bottomHeight);
                buttons[3].SetBounds(2 * thirdWidth, halfHeight, thirdWidth, bottomHeight);
            }
            else
            {
                // Default fallback: evenly distribute buttons
                int buttonHeight = height / buttons.Count;
                for (int i = 0; i < buttons.Count; i++)
                {
                    buttons[i].SetBounds(0, i * buttonHeight, width, buttonHeight);
                }
            }
        }
    }
}

