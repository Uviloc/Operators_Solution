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
            OperatorsSolution.Graphics_Program_Functions.ObjectChange objectChange1 = new OperatorsSolution.Graphics_Program_Functions.ObjectChange();
            OperatorsSolution.Graphics_Program_Functions.ObjectChange objectChange2 = new OperatorsSolution.Graphics_Program_Functions.ObjectChange();
            OperatorsSolution.Graphics_Program_Functions.ObjectChange objectChange3 = new OperatorsSolution.Graphics_Program_Functions.ObjectChange();
            Script_Button.Scene scene1 = new Script_Button.Scene();
            logic_Button1 = new Logic_Button();
            toggle_Button1 = new Toggle_Button();
            script_Button1 = new Script_Button();
            score_Module1 = new Score_Module();
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
            section2.Text = "SomeSceneName";
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
            logic_Button1.Location = new Point(835, 39);
            logic_Button1.Name = "logic_Button1";
            logic_Button1.Size = new Size(543, 473);
            logic_Button1.TabIndex = 1;
            logic_Button1.Text = "logic_Button1";
            // 
            // toggle_Button1
            // 
            toggle_Button1.ClipIn.Clip = "inDuoTitle";
            toggle_Button1.ClipIn.Scene = "FightGraphic";
            toggle_Button1.ClipOut.Clip = "out";
            toggle_Button1.ClipOut.Scene = "FightGraphic";
            toggle_Button1.Location = new Point(199, 39);
            toggle_Button1.Name = "toggle_Button1";
            objectChange1.SceneObject = "PlayerName";
            objectChange1.SetTo = "Somebodies Name";
            objectChange2.SceneObject = "Wins";
            objectChange2.SetTo = "9";
            objectChange3.SceneObject = "Flag";
            objectChange3.SetTo = "Netherlands";
            toggle_Button1.ObjectChanges.Add(objectChange1);
            toggle_Button1.ObjectChanges.Add(objectChange2);
            toggle_Button1.ObjectChanges.Add(objectChange3);
            toggle_Button1.PreviewBox = null;
            toggle_Button1.SceneName = "GraphicFights";
            toggle_Button1.Size = new Size(591, 473);
            toggle_Button1.TabIndex = 0;
            toggle_Button1.Text = "GraphicFights";
            toggle_Button1.UseVisualStyleBackColor = false;
            // 
            // script_Button1
            // 
            script_Button1.Location = new Point(199, 539);
            script_Button1.Name = "script_Button1";
            script_Button1.PreviewBox = null;
            scene1.ButtonText = null;
            scene1.PlayAllChildrenAtButtonPress = false;
            scene1.SceneName = null;
            script_Button1.Scenes.Add(scene1);
            script_Button1.Size = new Size(591, 515);
            script_Button1.TabIndex = 2;
            script_Button1.Text = "script_Button1";
            script_Button1.UseVisualStyleBackColor = false;
            // 
            // score_Module1
            // 
            score_Module1.Location = new Point(835, 539);
            score_Module1.Name = "score_Module1";
            score_Module1.Size = new Size(543, 515);
            score_Module1.TabIndex = 3;
            // 
            // Test_Module
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1479, 1109);
            Controls.Add(score_Module1);
            Controls.Add(script_Button1);
            Controls.Add(logic_Button1);
            Controls.Add(toggle_Button1);
            Name = "Test_Module";
            Text = "Test_Module";
            ResumeLayout(false);
        }

        #endregion

        private Toggle_Button toggle_Button1;
        private Logic_Button logic_Button1;
        private Script_Button script_Button1;
        private Score_Module score_Module1;
    }
}