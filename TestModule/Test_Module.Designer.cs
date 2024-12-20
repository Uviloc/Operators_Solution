using OperatorsSolution;
using OperatorsSolution.Controls;

namespace TestModule2
{
    partial class Test_Module2 : CustomForm
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
            previewBox = new PictureBox();
            logic_Button3 = new Logic_Button();
            logic_Button4 = new Logic_Button();
            operatorButton9 = new OperatorButton();
            operatorButton10 = new OperatorButton();
            operatorButton11 = new OperatorButton();
            textBox1 = new TextBox();
            script_Button1 = new Script_Button();
            toggle_Button1 = new Toggle_Button();
            ((System.ComponentModel.ISupportInitialize)previewBox).BeginInit();
            SuspendLayout();
            // 
            // previewBox
            // 
            previewBox.Location = new Point(12, 693);
            previewBox.Name = "previewBox";
            previewBox.Size = new Size(653, 389);
            previewBox.TabIndex = 9;
            previewBox.TabStop = false;
            // 
            // logic_Button3
            // 
            logic_Button3.Location = new Point(684, 104);
            logic_Button3.Name = "logic_Button3";
            logic_Button3.Size = new Size(316, 225);
            logic_Button3.TabIndex = 10;
            logic_Button3.Text = "logic_Button3";
            // 
            // logic_Button4
            // 
            logic_Button4.Location = new Point(1246, 859);
            logic_Button4.Name = "logic_Button4";
            logic_Button4.Size = new Size(221, 223);
            logic_Button4.TabIndex = 10;
            logic_Button4.Text = "logic_Button3";
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
            script_Button1.Size = new Size(300, 225);
            script_Button1.TabIndex = 14;
            script_Button1.Text = "script_Button1";
            script_Button1.UseVisualStyleBackColor = false;
            // 
            // toggle_Button1
            // 
            toggle_Button1.ClipIn.Channel = 1;
            toggle_Button1.ClipIn.Clip = "in";
            toggle_Button1.ClipIn.Layer = 1;
            toggle_Button1.ClipIn.Scene = "UpcomingEvents";
            toggle_Button1.ClipIn.SceneDirector = "SceneDirector1";
            toggle_Button1.ClipIn.Track = "Track1";
            toggle_Button1.ClipOut.Channel = 1;
            toggle_Button1.ClipOut.Clip = "out";
            toggle_Button1.ClipOut.Layer = 1;
            toggle_Button1.ClipOut.Scene = "UpcomingEvents";
            toggle_Button1.ClipOut.SceneDirector = "SceneDirector1";
            toggle_Button1.ClipOut.Track = "Track1";
            toggle_Button1.Location = new Point(12, 316);
            toggle_Button1.Name = "toggle_Button1";
            toggle_Button1.PreviewBox = null;
            toggle_Button1.SceneName = "UpcomingEvents";
            toggle_Button1.Size = new Size(300, 225);
            toggle_Button1.TabIndex = 15;
            toggle_Button1.Text = "UpcomingEvents";
            toggle_Button1.UseVisualStyleBackColor = false;
            // 
            // Test_Module2
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1479, 1109);
            Controls.Add(toggle_Button1);
            Controls.Add(script_Button1);
            Controls.Add(textBox1);
            Controls.Add(operatorButton11);
            Controls.Add(operatorButton10);
            Controls.Add(operatorButton9);
            Controls.Add(logic_Button4);
            Controls.Add(logic_Button3);
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
        private Logic_Button logic_Button1;
        private OperatorButton operatorButton3;
        private OperatorButton operatorButton4;
        private OperatorButton operatorButton5;
        private OperatorButton operatorButton6;
        private OperatorButton operatorButton7;
        private OperatorButton operatorButton8;
        private Logic_Button logic_Button2;
        private Logic_Button logic_Button3;
        private Logic_Button logic_Button4;
        private OperatorButton operatorButton9;
        private OperatorButton operatorButton10;
        private OperatorButton operatorButton11;
        private TextBox textBox1;
        private Script_Button script_Button1;
        private Toggle_Button toggle_Button1;
    }
}