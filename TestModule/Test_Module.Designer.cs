using OperatorsSolution;
using OperatorsSolution.Controls;
using static OperatorsSolution.Controls.Logic_Button;

namespace TestModule2
{
    partial class Test_Module2 : PluginBaseForm
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
            OperatorsSolution.Graphics_Program_Functions.ObjectChange objectChange1 = new OperatorsSolution.Graphics_Program_Functions.ObjectChange();
            Script_Button.Scene scene1 = new Script_Button.Scene();
            OperatorsSolution.Graphics_Program_Functions.ObjectChange objectChange2 = new OperatorsSolution.Graphics_Program_Functions.ObjectChange();
            Section section1 = new Section();
            Section section2 = new Section();
            OperatorsSolution.Graphics_Program_Functions.ClipPath clipPath1 = new OperatorsSolution.Graphics_Program_Functions.ClipPath();
            OperatorsSolution.Graphics_Program_Functions.ClipPath clipPath2 = new OperatorsSolution.Graphics_Program_Functions.ClipPath();
            Section section3 = new Section();
            OperatorsSolution.Graphics_Program_Functions.ClipPath clipPath3 = new OperatorsSolution.Graphics_Program_Functions.ClipPath();
            OperatorsSolution.Graphics_Program_Functions.ClipPath clipPath4 = new OperatorsSolution.Graphics_Program_Functions.ClipPath();
            toggle_Button1 = new Toggle_Button();
            previewBox = new PictureBox();
            operatorButton9 = new OperatorButton();
            operatorButton10 = new OperatorButton();
            operatorButton11 = new OperatorButton();
            textBox1 = new TextBox();
            script_Button1 = new Script_Button();
            operatorButton12 = new OperatorButton();
            operatorButton13 = new OperatorButton();
            operatorButton14 = new OperatorButton();
            operatorButton15 = new OperatorButton();
            operatorButton16 = new OperatorButton();
            operatorButton17 = new OperatorButton();
            operatorButton18 = new OperatorButton();
            logic_Button1 = new Logic_Button();
            ((System.ComponentModel.ISupportInitialize)previewBox).BeginInit();
            SuspendLayout();
            // 
            // toggle_Button1
            // 
            toggle_Button1.ClipIn.Clip = "ieee";
            toggle_Button1.ClipIn.Scene = "SomeScene";
            toggle_Button1.ClipOut.Clip = "ie";
            toggle_Button1.ClipOut.Scene = "SomeScene";
            toggle_Button1.Location = new Point(43, 189);
            toggle_Button1.Name = "toggle_Button1";
            objectChange1.SceneObject = "1";
            objectChange1.SetTo = "o";
            toggle_Button1.ObjectChanges.Add(objectChange1);
            toggle_Button1.PreviewBox = null;
            toggle_Button1.SceneName = "SomeScene";
            toggle_Button1.Size = new Size(200, 150);
            toggle_Button1.TabIndex = 17;
            toggle_Button1.Text = "SomeScene";
            toggle_Button1.UseVisualStyleBackColor = false;
            // 
            // previewBox
            // 
            previewBox.Location = new Point(12, 693);
            previewBox.Name = "previewBox";
            previewBox.Size = new Size(653, 389);
            previewBox.TabIndex = 9;
            previewBox.TabStop = false;
            // 
            // operatorButton9
            // 
            operatorButton9.Location = new Point(1003, 859);
            operatorButton9.Name = "operatorButton9";
            operatorButton9.PreviewBox = null;
            operatorButton9.Size = new Size(191, 87);
            operatorButton9.TabIndex = 11;
            operatorButton9.UseVisualStyleBackColor = false;
            // 
            // operatorButton10
            // 
            operatorButton10.Location = new Point(1003, 1017);
            operatorButton10.Name = "operatorButton10";
            operatorButton10.PreviewBox = null;
            operatorButton10.Size = new Size(191, 65);
            operatorButton10.TabIndex = 11;
            operatorButton10.UseVisualStyleBackColor = false;
            // 
            // operatorButton11
            // 
            operatorButton11.Location = new Point(684, 859);
            operatorButton11.Name = "operatorButton11";
            operatorButton11.PreviewBox = null;
            operatorButton11.Size = new Size(300, 223);
            operatorButton11.TabIndex = 11;
            operatorButton11.UseVisualStyleBackColor = false;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(82, 39);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(174, 31);
            textBox1.TabIndex = 12;
            // 
            // script_Button1
            // 
            script_Button1.Location = new Point(348, 104);
            script_Button1.Name = "script_Button1";
            script_Button1.PreviewBox = null;
            scene1.ButtonText = "LOL";
            objectChange2.SceneObject = "1";
            objectChange2.SetTo = "i";
            scene1.ObjectChanges.Add(objectChange2);
            scene1.PlayAllChildrenAtButtonPress = false;
            scene1.SceneName = "Scene1WOO";
            script_Button1.Scenes.Add(scene1);
            script_Button1.Size = new Size(300, 225);
            script_Button1.TabIndex = 14;
            script_Button1.Text = "Nope";
            script_Button1.UseVisualStyleBackColor = false;
            // 
            // operatorButton12
            // 
            operatorButton12.Location = new Point(0, 0);
            operatorButton12.Name = "operatorButton12";
            operatorButton12.PreviewBox = null;
            operatorButton12.Size = new Size(200, 150);
            operatorButton12.TabIndex = 0;
            operatorButton12.UseVisualStyleBackColor = false;
            // 
            // operatorButton13
            // 
            operatorButton13.Location = new Point(0, 0);
            operatorButton13.Name = "operatorButton13";
            operatorButton13.PreviewBox = null;
            operatorButton13.Size = new Size(200, 150);
            operatorButton13.TabIndex = 0;
            operatorButton13.UseVisualStyleBackColor = false;
            // 
            // operatorButton14
            // 
            operatorButton14.Location = new Point(0, 0);
            operatorButton14.Name = "operatorButton14";
            operatorButton14.PreviewBox = null;
            operatorButton14.Size = new Size(200, 150);
            operatorButton14.TabIndex = 0;
            operatorButton14.UseVisualStyleBackColor = false;
            // 
            // operatorButton15
            // 
            operatorButton15.Location = new Point(0, 0);
            operatorButton15.Name = "operatorButton15";
            operatorButton15.PreviewBox = null;
            operatorButton15.Size = new Size(200, 150);
            operatorButton15.TabIndex = 0;
            operatorButton15.UseVisualStyleBackColor = false;
            // 
            // operatorButton16
            // 
            operatorButton16.Location = new Point(0, 0);
            operatorButton16.Name = "operatorButton16";
            operatorButton16.PreviewBox = null;
            operatorButton16.Size = new Size(200, 150);
            operatorButton16.TabIndex = 0;
            operatorButton16.UseVisualStyleBackColor = false;
            // 
            // operatorButton17
            // 
            operatorButton17.Location = new Point(0, 0);
            operatorButton17.Name = "operatorButton17";
            operatorButton17.PreviewBox = null;
            operatorButton17.Size = new Size(200, 150);
            operatorButton17.TabIndex = 0;
            operatorButton17.UseVisualStyleBackColor = false;
            // 
            // operatorButton18
            // 
            operatorButton18.Location = new Point(0, 0);
            operatorButton18.Name = "operatorButton18";
            operatorButton18.PreviewBox = null;
            operatorButton18.Size = new Size(200, 150);
            operatorButton18.TabIndex = 0;
            operatorButton18.UseVisualStyleBackColor = false;
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
            section2.Condition.ToggleState = true;
            section2.Text = "SceneName";
            clipPath3.Clip = null;
            clipPath3.Scene = null;
            section3.ClipIn = clipPath3;
            clipPath4.Clip = null;
            clipPath4.Scene = null;
            section3.ClipOut = clipPath4;
            section3.Condition.ConditionType = ConditionType.ToggleButtonState;
            section3.Condition.Parent = logic_Button1;
            section3.Condition.ToggleState = true;
            section3.Text = "SceneName";
            logic_Button1.Buttons.Add(section1);
            logic_Button1.Buttons.Add(section2);
            logic_Button1.Buttons.Add(section3);
            logic_Button1.Location = new Point(978, 447);
            logic_Button1.Name = "logic_Button1";
            logic_Button1.Size = new Size(308, 225);
            logic_Button1.TabIndex = 18;
            logic_Button1.Text = "logic_Button1";
            // 
            // Test_Module2
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1479, 1109);
            Controls.Add(logic_Button1);
            Controls.Add(toggle_Button1);
            Controls.Add(script_Button1);
            Controls.Add(textBox1);
            Controls.Add(operatorButton11);
            Controls.Add(operatorButton10);
            Controls.Add(operatorButton9);
            Controls.Add(previewBox);
            Name = "Test_Module2";
            Text = "Test_Module2";
            ((System.ComponentModel.ISupportInitialize)previewBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox previewBox;
        private OperatorButton operatorButton1;
        private OperatorButton operatorButton2;
        private OperatorButton operatorButton3;
        private OperatorButton operatorButton4;
        private OperatorButton operatorButton5;
        private OperatorButton operatorButton6;
        private OperatorButton operatorButton7;
        private OperatorButton operatorButton8;
        private OperatorButton operatorButton9;
        private OperatorButton operatorButton10;
        private OperatorButton operatorButton11;
        private TextBox textBox1;
        private Script_Button script_Button1;
        private Toggle_Button toggle_Button1;
        private OperatorButton operatorButton12;
        private OperatorButton operatorButton13;
        private OperatorButton operatorButton14;
        private OperatorButton operatorButton15;
        private OperatorButton operatorButton16;
        private OperatorButton operatorButton17;
        private OperatorButton operatorButton18;
        private Logic_Button logic_Button1;
    }
}