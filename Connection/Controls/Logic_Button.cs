using System;
using System.Collections.Generic;
using System.ComponentModel;
using OperatorsSolution.Common;
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
        #region >----------------- Condition class: ---------------------
        public enum ConditionType
        {
            ToggleButtonState,
            ScriptButtonState,
            DataExists
        }

        public partial class Condition
        {
            [Browsable(false)]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
            public Control? Parent { get; set; }


            [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
            private ConditionType _conditionType = ConditionType.ToggleButtonState;


            [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
            public ConditionType ConditionType
            {
                get => _conditionType;
                set
                {
                    if (_conditionType != value)
                    {
                        _conditionType = value;

                        // Notify that properties have changed dynamically
                        TypeDescriptor.Refresh(this);
                    }
                }
            }

            private Toggle_Button? _toggleButton;
            [TypeVisibility(ConditionType.ToggleButtonState)]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
            [TypeConverter(typeof(OptionsTypeConverter<Toggle_Button>))]
            public Toggle_Button ToggleButton
            {
                get => _toggleButton ?? new();
                set
                {
                    if (_toggleButton != value)
                        _toggleButton = value;
                }
            }

            [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
            [TypeVisibility(ConditionType.ToggleButtonState)]
            public bool ToggleState { get; set; } = true;


            private Script_Button? _scriptButton;
            [TypeVisibility(ConditionType.ScriptButtonState)]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
            [TypeConverter(typeof(OptionsTypeConverter<Script_Button>))]
            public Script_Button ScriptButton
            {
                get => _scriptButton ?? new();
                set
                {
                    if (_scriptButton != value)
                        _scriptButton = value;
                }
            }

            [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
            [TypeVisibility(ConditionType.ScriptButtonState)]
            public int ScriptState { get; set; }


            [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
            [TypeVisibility(ConditionType.DataExists)]
            public object? Data { get; set; }

            public override string ToString()
            {
                return _conditionType switch {
                    ConditionType.ToggleButtonState => "If \'" + ToggleButton?.Text + "\' state is: " + (ToggleState ? "ON" : "OFF"),
                    ConditionType.ScriptButtonState => "If \'" + ScriptButton?.Text + "\' script pos is: " + ScriptState.ToString(),
                    ConditionType.DataExists => "If \'" + Data?.ToString() + "\' exists",
                    _ => throw new NotSupportedException($"Unsupported condition type: {_conditionType}")
                };
            }
        }
        #endregion

        #region >----------------- Section class: ---------------------
        public enum ButtonType
        {
            ToggleButton,
            ScriptButton
        }

        public partial class Section
        {
            // Synchronize all properties
            public void ApplyPropertiesToButton()
            {
                if (Button is Script_Button scriptButton)
                {
                    scriptButton.Scenes = Scenes;
                }
                else if (Button is Toggle_Button toggleButton)
                {
                    toggleButton.SceneName = SceneName;
                    toggleButton.Text = _text;
                }
            }

            [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
            private ButtonType _buttonType = ButtonType.ToggleButton;

            // Button Type
            [Category(".Logic")]
            [Description("The type of button that is used for this section.")]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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
            //private List<Script_Button.Scene> _scenes = [];
            [Category("Search")]
            [Description("Add scenes to be played here.")]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
            [TypeVisibility(ButtonType.ScriptButton)]
            public List<Script_Button.Scene> Scenes { get; set; } = [];
            //{
            //    get => _scenes ?? [];
            //    set
            //    {
            //        if (_scenes != value)
            //            _scenes = value;
            //    }
            //}

            // Text
            private string _text = "[Show] SceneName";
            [Category("Visuals")]
            [Description("The text associated with the control.")]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
            [TypeVisibility(ButtonType.ToggleButton)]
            [DefaultValue("[Show] SceneName")]
            public string Text
            {
                get => _text;
                set
                {
                    if (_text != value)
                        _text = value;
                }
            }

            // Scene Name
            private string? _sceneName;
            [Category("Search")]
            [Description("The name of the scene in the chosen graphics program.")]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
            [TypeVisibility(ButtonType.ToggleButton)]
            public string? SceneName
            {
                get => _sceneName;
                set
                {
                    if (_sceneName != value)
                    {
                        if (Text.Contains(_sceneName ?? "") || Text.Contains("toggle_Button") || Text.Contains("SceneName") || string.IsNullOrWhiteSpace(Text))
                            Text = "[Show] " + (value ?? "");

                        _sceneName = value;
                    }
                }
            }

            // Condition
            [Category(".Logic")]
            [Description("The conditions for this button to be enabled.")]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
            [TypeConverter(typeof(ExpandableObjectConverter))]
            public Condition Condition { get; set; } = new();

            // Button
            [Browsable(false)]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
            public OperatorButton? Button { get; set; }

            public override string ToString()
            {
                return Text;
            }
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

            //if (Buttons[0].Button is Toggle_Button mainButton && mainButton.buttonIsOn && Buttons[1].Button is OperatorButton button)
            //{

            //    button.Enabled = false;
            //}
            //Click += (sender, e) => { MessageBox.Show("HI"); };
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
                Condition condition = section.Condition;
                condition.Parent = this;

                TypeDescriptor.RemoveProvider(TypeDescriptor.GetProvider(section), section);
                TypeDescriptor.RemoveProvider(TypeDescriptor.GetProvider(condition), condition);

                Control button = section.Button?? section.ButtonType switch
                {
                    ButtonType.ToggleButton => new Toggle_Button(),
                    ButtonType.ScriptButton => new Script_Button(),
                    _ => throw new NotSupportedException($"Unsupported button type: {section.ButtonType}")
                };

                section.Button = button as OperatorButton;
                section.ApplyPropertiesToButton();
                Controls.Add(button);

                TypeDescriptor.AddProvider(new Common.TypeDescriptionProvider<Section>(), section);
                TypeDescriptor.AddProvider(new Common.TypeDescriptionProvider<Condition>(), condition);
                TypeDescriptor.Refresh(section);
            }

            PerformLayout();
        }

        //public void UpdateButtons()
        //{
        //    // Ensure buttons are only updated, not re-created
        //    for (int i = 0; i < Buttons.Count; i++)
        //    {
        //        Section section = Buttons[i];
        //        section.Condition.parent = this;
        //        Condition condition = section.Condition;

        //        // Ensure the correct button type exists for each section
        //        Control? button = Controls.OfType<Control>()
        //            .FirstOrDefault(b => b is Toggle_Button && section.ButtonType == ButtonType.ToggleButton)
        //            ?? Controls.OfType<Control>()
        //            .FirstOrDefault(b => b is Script_Button && section.ButtonType == ButtonType.ScriptButton);

        //        // If no button exists, create and add it
        //        if (button == null)
        //        {
        //            button = section.ButtonType switch
        //            {
        //                ButtonType.ToggleButton => new Toggle_Button(),
        //                ButtonType.ScriptButton => new Script_Button(),
        //                _ => throw new NotSupportedException($"Unsupported button type: {section.ButtonType}")
        //            };

        //            section.Button = button as OperatorButton;
        //            Controls.Add(button);
        //        }

        //        // Set the properties from the Section to the button
        //        if (section.Button is Toggle_Button toggleButton)
        //        {
        //            toggleButton.Text = section.Text; // Set Text from Section
        //            toggleButton.SceneName = section.SceneName; // Set SceneName from Section
        //        }

        //        // Re-add type providers and refresh section
        //        TypeDescriptor.AddProvider(new Common.TypeDescriptionProvider<Section>(), section);
        //        TypeDescriptor.AddProvider(new Common.TypeDescriptionProvider<Condition>(), condition);
        //        TypeDescriptor.Refresh(section);
        //    }

        //    PerformLayout();
        //}



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
            int halfWidth = width/2;
            int halfHeight = height/2;

            if (Buttons.Count == 0) return;

            switch (Buttons.Count)
            {
                case 1:
                    Buttons[0].Button?.SetBounds(0, 0, width, height);
                    break;
                case 2:
                    Buttons[0].Button?.SetBounds(0, 0, width, halfHeight);
                    Buttons[1].Button?.SetBounds(0, halfHeight, width, halfHeight);
                    break;
                case 3:
                    Buttons[0].Button?.SetBounds(0, 0, width, halfHeight);
                    Buttons[1].Button?.SetBounds(0, halfHeight, halfWidth, halfHeight);
                    Buttons[2].Button?.SetBounds(halfWidth, halfHeight, halfWidth, halfHeight);
                    break;
                case 4:
                    Buttons[0].Button?.SetBounds(0, 0, halfWidth, halfHeight);
                    Buttons[1].Button?.SetBounds(halfWidth, 0, halfWidth, halfHeight);
                    Buttons[2].Button?.SetBounds(0, halfHeight, halfWidth, halfHeight);
                    Buttons[3].Button?.SetBounds(halfWidth, halfHeight, halfWidth, halfHeight);
                    break;
                default:
                    int buttonHeight = height / Buttons.Count;
                    for (int i = 0; i < Buttons.Count; i++)
                    {
                        Buttons[i].Button?.SetBounds(0, i * buttonHeight, width, buttonHeight);
                    }
                    break;
            }
        }
        #endregion
    }
}

