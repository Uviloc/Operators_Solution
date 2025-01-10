using OperatorsSolution;
using OperatorsSolution.Controls;
using static OperatorsSolution.Controls.Logic_Button;

namespace TestModule2
{
    partial class Test_Module : PluginBaseForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Section section1 = new Section();
            Section section2 = new Section();
            OperatorsSolution.Graphics_Program_Functions.ClipPath clipPath1 = new OperatorsSolution.Graphics_Program_Functions.ClipPath();
            OperatorsSolution.Graphics_Program_Functions.ClipPath clipPath2 = new OperatorsSolution.Graphics_Program_Functions.ClipPath();
            Section section3 = new Section();
            OperatorsSolution.Graphics_Program_Functions.ClipPath clipPath3 = new OperatorsSolution.Graphics_Program_Functions.ClipPath();
            OperatorsSolution.Graphics_Program_Functions.ClipPath clipPath4 = new OperatorsSolution.Graphics_Program_Functions.ClipPath();
            logic_Button1 = new Logic_Button();
            SuspendLayout();
            // 
            // logic_Button1
            // 
            section1.ButtonType = ButtonType.ScriptButton;
            section1.Condition.ConditionType = ConditionType.ToggleButtonState;
            section1.Condition.Parent = logic_Button1;
            section1.Condition.ToggleState = true;
            clipPath1.Clip = null;
            clipPath1.Scene = null;
            section2.ClipIn = clipPath1;
            clipPath2.Clip = null;
            clipPath2.Scene = null;
            section2.ClipOut = clipPath2;
            section2.Condition.ConditionType = ConditionType.ToggleButtonState;
            section2.Condition.Parent = logic_Button1;
            section2.Condition.ToggleButton = null;
            section2.Condition.ToggleState = true;
            section2.Text = "Scene";
            clipPath3.Clip = null;
            clipPath3.Scene = null;
            section3.ClipIn = clipPath3;
            clipPath4.Clip = null;
            clipPath4.Scene = null;
            section3.ClipOut = clipPath4;
            section3.Condition.ConditionType = ConditionType.ToggleButtonState;
            section3.Condition.Parent = logic_Button1;
            section3.Condition.ToggleButton = null;
            section3.Condition.ToggleState = true;
            section3.Text = "Scene";
            logic_Button1.Buttons.Add(section1);
            logic_Button1.Buttons.Add(section2);
            logic_Button1.Buttons.Add(section3);
            logic_Button1.Location = new Point(433, 368);
            logic_Button1.Name = "logic_Button1";
            logic_Button1.Size = new Size(453, 427);
            logic_Button1.TabIndex = 0;
            logic_Button1.Text = "logic_Button1";
            // 
            // Test_Module
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1479, 1109);
            Controls.Add(logic_Button1);
            Name = "Test_Module";
            Text = "Test_Module";
            ResumeLayout(false);
        }

        #endregion

        private Logic_Button logic_Button1;
    }
}