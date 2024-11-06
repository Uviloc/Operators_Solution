using OperatorsSolution;

namespace TestModule
{
    partial class Test_Module
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
            ClipPath clipPath1 = new ClipPath();
            operatorButton1 = new OperatorButton();
            PreviewBox = new Panel();
            SuspendLayout();
            // 
            // operatorButton1
            // 
            clipPath1.Clip = "inDuoTitle";
            clipPath1.Scene = "FightGraphic";
            operatorButton1.ClipPaths.Add(clipPath1);
            operatorButton1.Location = new Point(388, 125);
            operatorButton1.Name = "operatorButton1";
            operatorButton1.Scene = "FightGraphic";
            operatorButton1.Size = new Size(112, 34);
            operatorButton1.TabIndex = 0;
            operatorButton1.UseVisualStyleBackColor = false;
            // 
            // PreviewBox
            // 
            PreviewBox.Location = new Point(388, 262);
            PreviewBox.Name = "PreviewBox";
            PreviewBox.Size = new Size(300, 150);
            PreviewBox.TabIndex = 1;
            // 
            // Test_Module
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(PreviewBox);
            Controls.Add(operatorButton1);
            Name = "Test_Module";
            Text = "Test_Module";
            ResumeLayout(false);
        }

        #endregion

        private OperatorButton operatorButton1;
        private Panel PreviewBox;
    }
}