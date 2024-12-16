using OperatorsSolution;
using OperatorsSolution.Controls;

namespace TestModule
{
    partial class Test_Module : CustomForm
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
            OperatorsSolution.GraphicsProgramFunctions.ClipPath clipPath1 = new OperatorsSolution.GraphicsProgramFunctions.ClipPath();
            OperatorsSolution.GraphicsProgramFunctions.ClipPath clipPath2 = new OperatorsSolution.GraphicsProgramFunctions.ClipPath();
            previewBox = new PictureBox();
            logic_Button3 = new Logic_Button();
            logic_Button4 = new Logic_Button();
            operatorButton9 = new OperatorButton();
            operatorButton10 = new OperatorButton();
            operatorButton11 = new OperatorButton();
            textBox1 = new TextBox();
            toggle_Button1 = new Toggle_Button();
            script_Button1 = new Script_Button();
            ((System.ComponentModel.ISupportInitialize)previewBox).BeginInit();
            SuspendLayout();
            // 
            // previewBox
            // 
            previewBox.Location = new Point(30, 693);
            previewBox.Name = "previewBox";
            previewBox.Size = new Size(653, 389);
            previewBox.TabIndex = 9;
            previewBox.TabStop = false;
            // 
            // logic_Button3
            // 
            logic_Button3.Location = new Point(748, 425);
            logic_Button3.Name = "logic_Button3";
            logic_Button3.Size = new Size(316, 225);
            logic_Button3.TabIndex = 10;
            logic_Button3.Text = "logic_Button3";
            // 
            // logic_Button4
            // 
            logic_Button4.Location = new Point(1282, 22);
            logic_Button4.Name = "logic_Button4";
            logic_Button4.Size = new Size(185, 147);
            logic_Button4.TabIndex = 10;
            logic_Button4.Text = "logic_Button3";
            // 
            // operatorButton9
            // 
            operatorButton9.Location = new Point(1058, 22);
            operatorButton9.Name = "operatorButton9";
            operatorButton9.PreviewBox = null;
            operatorButton9.Size = new Size(191, 87);
            operatorButton9.TabIndex = 11;
            operatorButton9.UseVisualStyleBackColor = false;
            // 
            // operatorButton10
            // 
            operatorButton10.Location = new Point(1058, 115);
            operatorButton10.Name = "operatorButton10";
            operatorButton10.PreviewBox = null;
            operatorButton10.Size = new Size(191, 65);
            operatorButton10.TabIndex = 11;
            operatorButton10.UseVisualStyleBackColor = false;
            // 
            // operatorButton11
            // 
            operatorButton11.Location = new Point(50, 22);
            operatorButton11.Name = "operatorButton11";
            operatorButton11.PreviewBox = null;
            operatorButton11.Size = new Size(300, 223);
            operatorButton11.TabIndex = 11;
            operatorButton11.UseVisualStyleBackColor = false;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(1270, 214);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(174, 31);
            textBox1.TabIndex = 12;
            // 
            // toggle_Button1
            // 
            clipPath1.Clip = null;
            clipPath1.Scene = "";
            toggle_Button1.ClipIn = clipPath1;
            clipPath2.Clip = null;
            clipPath2.Scene = "";
            toggle_Button1.ClipOut = clipPath2;
            toggle_Button1.Location = new Point(50, 425);
            toggle_Button1.Name = "toggle_Button1";
            toggle_Button1.PreviewBox = null;
            toggle_Button1.SceneName = "ThisScene";
            toggle_Button1.Size = new Size(300, 225);
            toggle_Button1.TabIndex = 13;
            toggle_Button1.Text = "[Show] ThisScene";
            toggle_Button1.UseVisualStyleBackColor = false;
            // 
            // script_Button1
            // 
            script_Button1.Location = new Point(418, 425);
            script_Button1.Name = "script_Button1";
            script_Button1.PreviewBox = null;
            script_Button1.Size = new Size(300, 225);
            script_Button1.TabIndex = 14;
            script_Button1.Text = "script_Button1";
            script_Button1.UseVisualStyleBackColor = false;
            // 
            // Test_Module
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1479, 1109);
            Controls.Add(script_Button1);
            Controls.Add(toggle_Button1);
            Controls.Add(textBox1);
            Controls.Add(operatorButton11);
            Controls.Add(operatorButton10);
            Controls.Add(operatorButton9);
            Controls.Add(logic_Button4);
            Controls.Add(logic_Button3);
            Controls.Add(previewBox);
            Name = "Test_Module";
            Text = "Test_Module";
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
        private Toggle_Button toggle_Button1;
        private Script_Button script_Button1;
    }
}