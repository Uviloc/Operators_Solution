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












/*
 Some type of UI for having some clips be available when others are active
Likely best with indentation in the ToString()
 
 Have the buttons that are assigned to that clip be deactivated when the condition is false
 
 
 
 
 
 */





namespace OperatorsSolution.Controls
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(Button))]
    public class Logic_Button : Control
    {
        private List<OperatorButton> _buttons;

        //Buttons;
        [Category(".Operation > Layout")]
        [Description("The number of buttons to display.")]
        public List<OperatorButton> Buttons {
            get => _buttons;
            set
            {
                if (value.Count != _buttons.Count && value.Count >= 1)
                {
                    _buttons = value;
                    UpdateButtons();
                }
            }
        }

        //[Category(".Operation > Layout")]
        //[Description("The number of buttons to display.")]
        //[DefaultValue(3)]
        //public int ButtonAmount
        //{
        //    get => buttonAmount;
        //    set
        //    {
        //        if (value != buttonAmount && value >= 1)
        //        {
        //            buttonAmount = value;
        //            UpdateButtons();
        //        }
        //    }
        //}

        public Logic_Button()
        {
            _buttons =
            [
                new OperatorButton(),
                new OperatorButton(),
                new OperatorButton()
            ];
            UpdateButtons();
        }

        //private void UpdateButtons()
        //{
        //    // Remove existing buttons
        //    foreach (var button in _buttons)
        //    {
        //        Controls.Remove(button);
        //        button.Dispose();
        //    }

        //    _buttons.Clear();

        //    // Create new buttons
        //    for (int i = 0; i < _buttons.Count; i++)
        //    {
        //        var button = new OperatorButton
        //        {
        //            Text = $"Button {i + 1}",
        //            Parent = this // Automatically adds to Controls
        //        };
        //        _buttons.Add(button);
        //    }

        //    PerformLayout(); // Trigger layout recalculation
        //}

        //protected override void OnResize(EventArgs e)
        //{
        //    base.OnResize(e);
        //    PerformLayout(); // Trigger layout recalculation on resize
        //}

        //protected override void OnLayout(LayoutEventArgs levent)
        //{
        //    base.OnLayout(levent);

        //    // Layout logic
        //    int width = ClientSize.Width;
        //    int height = ClientSize.Height;

        //    if (Buttons.Count == 0)
        //    {
        //        return;
        //    }
        //    else if (Buttons.Count == 1)
        //    {
        //        // Single button fills the entire control
        //        Buttons[0].SetBounds(0, 0, width, height);
        //    }
        //    else if (Buttons.Count == 2)
        //    {
        //        // Two buttons split vertically
        //        Buttons[0].SetBounds(0, 0, width, height / 2);
        //        Buttons[1].SetBounds(0, height / 2, width, height / 2);
        //    }
        //    else if (Buttons.Count == 3)
        //    {
        //        // One large button on top and two buttons side-by-side at the bottom
        //        int halfHeight = height / 2;
        //        int bottomHeight = height - halfHeight;

        //        Buttons[0].SetBounds(0, 0, width, halfHeight);
        //        Buttons[1].SetBounds(0, halfHeight, width / 2, bottomHeight);
        //        Buttons[2].SetBounds(width / 2, halfHeight, width / 2, bottomHeight);
        //    }
        //    else if (Buttons.Count == 4)
        //    {
        //        // One large button on top and two buttons side-by-side at the bottom
        //        int halfHeight = height / 2;
        //        int bottomHeight = height - halfHeight;

        //        int thirdWidth = width / 3;

        //        Buttons[0].SetBounds(0, 0, width, halfHeight);
        //        Buttons[1].SetBounds(0, halfHeight, thirdWidth, bottomHeight);
        //        Buttons[2].SetBounds(thirdWidth, halfHeight, thirdWidth, bottomHeight);
        //        Buttons[3].SetBounds(2 * thirdWidth, halfHeight, thirdWidth, bottomHeight);
        //    }
        //    else
        //    {
        //        // Default fallback: evenly distribute buttons
        //        int buttonHeight = height / Buttons.Count;
        //        for (int i = 0; i < Buttons.Count; i++)
        //        {
        //            Buttons[i].SetBounds(0, i * buttonHeight, width, buttonHeight);
        //        }
        //    }
        //}

        private int _buttonCount = 3;

        private void UpdateButtons()
        {
            // Remove existing buttons from the form
            foreach (var button in _buttons)
            {
                Controls.Remove(button);
                button.Dispose();
            }

            // Clear the buttons list
            _buttons.Clear();

            // Create new buttons based on the current size of _buttons
            for (int i = 0; i < _buttonCount; i++) // Use a separate count property for button amount
            {
                var button = new OperatorButton
                {
                    Text = $"Button {i + 1}",
                    Parent = this // Automatically adds to Controls
                };
                _buttons.Add(button);
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

            int width = ClientSize.Width;
            int height = ClientSize.Height;

            if (_buttons.Count == 0) return;

            if (_buttons.Count == 1)
            {
                // Single button fills the entire control
                _buttons[0].SetBounds(0, 0, width, height);
            }
            else if (_buttons.Count == 2)
            {
                // Two buttons split vertically
                _buttons[0].SetBounds(0, 0, width, height / 2);
                _buttons[1].SetBounds(0, height / 2, width, height / 2);
            }
            else if (_buttons.Count == 3)
            {
                // One large button on top and two buttons side-by-side at the bottom
                int halfHeight = height / 2;
                int bottomHeight = height - halfHeight;

                _buttons[0].SetBounds(0, 0, width, halfHeight);
                _buttons[1].SetBounds(0, halfHeight, width / 2, bottomHeight);
                _buttons[2].SetBounds(width / 2, halfHeight, width / 2, bottomHeight);
            }
            else if (_buttons.Count == 4)
            {
                // One large button on top and three buttons side-by-side at the bottom
                int halfHeight = height / 2;
                int bottomHeight = height - halfHeight;

                int thirdWidth = width / 3;

                _buttons[0].SetBounds(0, 0, width, halfHeight);
                _buttons[1].SetBounds(0, halfHeight, thirdWidth, bottomHeight);
                _buttons[2].SetBounds(thirdWidth, halfHeight, thirdWidth, bottomHeight);
                _buttons[3].SetBounds(2 * thirdWidth, halfHeight, thirdWidth, bottomHeight);
            }
            else
            {
                // Default fallback: evenly distribute buttons
                int buttonHeight = height / _buttons.Count;
                for (int i = 0; i < _buttons.Count; i++)
                {
                    _buttons[i].SetBounds(0, i * buttonHeight, width, buttonHeight);
                }
            }
        }

    }
}

