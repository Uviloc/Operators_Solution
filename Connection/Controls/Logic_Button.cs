using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
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
    public partial class Logic_Button : Control
    {
        #region >----------------- Section class: ---------------------
        public enum ButtonType
        {
            ToggleButton,
            ScriptButton
        }

        [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
        public class ButtonTypeVisibilityAttribute(ButtonType buttonType) : Attribute
        {
            public ButtonType ButtonType { get; } = buttonType;
        }


        public partial class Section
        {
            private ButtonType _buttonType = ButtonType.ToggleButton;

            // Button Type
            [Category(".Logic")]
            [Description("The type of button that is used for this section.")]
            [DefaultValue(ButtonType.ToggleButton)]
            public ButtonType ButtonType
            {
                get => _buttonType;
                set
                {
                    if (_buttonType != value)
                    {
                        _buttonType = value;

                        // Notify that properties have changed dynamically
                        TypeDescriptor.Refresh(this);
                    }
                }
            }

            // Scenes
            [Category("Search")]
            [Description("Add scenes to be played here.")]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
            [ButtonTypeVisibility(ButtonType.ScriptButton)]
            public List<Script_Button.Scene> Scenes
            {
                get => (Button as Script_Button)?.Scenes ?? [];
                set
                {
                    if (Button is Script_Button scriptButton)
                        scriptButton.Scenes = value;
                }
            }

            // Text
            [Category("Visuals")]
            [Description("The text associated with the control.")]
            [ButtonTypeVisibility(ButtonType.ToggleButton)]
            public string Text
            {
                get => (Button as Toggle_Button)?.Text ?? "[Show] SceneName";
                set
                {
                    if (Button is Toggle_Button toggleButton)
                        toggleButton.Text = value;
                }
            }

            // Scene Name
            [Category("Search")]
            [Description("The name of the scene in the chosen graphics program.")]
            [ButtonTypeVisibility(ButtonType.ToggleButton)]
            public string? SceneName
            {
                get => (Button as Toggle_Button)?.SceneName;
                set
                {
                    if (Button is Toggle_Button toggleButton)
                        toggleButton.SceneName = value;
                }
            }


            // Private button property (not shown in Properties Window)
            [Browsable(false)]
            public OperatorButton? Button { get; set; }
        }
        #endregion

        #region >----------------- Add properties + Set events: ---------------------
        [Category(".Operation > Layout")]
        [Description("The buttons to display.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BindingList<Section> Buttons { get; set; } = [];

        public Logic_Button()
        {
            Buttons.ListChanged += (sender, e) =>
            {
                UpdateButtons();
            };
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (Buttons.Count == 0)
            {
                Buttons.Add(new Section { ButtonType = ButtonType.ScriptButton });
                Buttons.Add(new Section { ButtonType = ButtonType.ToggleButton });
                Buttons.Add(new Section { ButtonType = ButtonType.ToggleButton });
            }
            UpdateButtons();
        }
        #endregion

        #region >----------------- Functions: ---------------------
        public void UpdateButtons()
        {
            Controls.Clear();

            foreach (Section section in Buttons)
            {
                TypeDescriptor.RemoveProvider(TypeDescriptor.GetProvider(section), section);

                Control button = section.ButtonType switch
                {
                    ButtonType.ToggleButton => new Toggle_Button(),
                    ButtonType.ScriptButton => new Script_Button(),
                    _ => throw new NotSupportedException($"Unsupported button type: {section.ButtonType}")
                };

                section.Button = button as OperatorButton;

                Controls.Add(button);

                TypeDescriptor.AddProvider(new Common.SectionTypeDescriptionProvider(), section);
                TypeDescriptor.Refresh(section);
            }

            PerformLayout();
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

            if (Buttons.Count == 0) return;

            // Layout logic similar to your earlier implementation
            if (Buttons.Count == 1)
            {
                Buttons[0].Button?.SetBounds(0, 0, width, height);
            }
            else if (Buttons.Count == 2)
            {
                Buttons[0].Button?.SetBounds(0, 0, width, height / 2);
                Buttons[1].Button?.SetBounds(0, height / 2, width, height / 2);
            }
            else if (Buttons.Count == 3)
            {
                int halfHeight = height / 2;
                int bottomHeight = height - halfHeight;

                Buttons[0].Button?.SetBounds(0, 0, width, halfHeight);
                Buttons[1].Button?.SetBounds(0, halfHeight, width / 2, bottomHeight);
                Buttons[2].Button?.SetBounds(width / 2, halfHeight, width / 2, bottomHeight);
            }
            else
            {
                int buttonHeight = height / Buttons.Count;
                for (int i = 0; i < Buttons.Count; i++)
                {
                    Buttons[i].Button?.SetBounds(0, i * buttonHeight, width, buttonHeight);
                }
            }
        }
        #endregion
    }
}

