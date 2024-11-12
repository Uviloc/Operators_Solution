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
            ClipPath clipPath2 = new ClipPath();
            ClipPath clipPath3 = new ClipPath();
            ClipPath clipPath4 = new ClipPath();
            ClipPath clipPath5 = new ClipPath();
            ClipPath clipPath6 = new ClipPath();
            ClipPath clipPath7 = new ClipPath();
            ClipPath clipPath8 = new ClipPath();
            ClipPath clipPath9 = new ClipPath();
            ClipPath clipPath10 = new ClipPath();
            ClipPath clipPath11 = new ClipPath();
            ClipPath clipPath12 = new ClipPath();
            PreviewBox = new PictureBox();
            operatorButton1 = new OperatorButton();
            operatorButton2 = new OperatorButton();
            ((System.ComponentModel.ISupportInitialize)PreviewBox).BeginInit();
            SuspendLayout();
            // 
            // PreviewBox
            // 
            PreviewBox.Location = new Point(416, 566);
            PreviewBox.Name = "PreviewBox";
            PreviewBox.Size = new Size(1071, 684);
            PreviewBox.TabIndex = 9;
            PreviewBox.TabStop = false;
            // 
            // operatorButton1
            // 
            clipPath1.Clip = "inDuoTitle";
            clipPath2.Clip = "toFightGraphic";
            clipPath3.Clip = "weightDivisionIn";
            clipPath4.Clip = "weightDivisionOut";
            clipPath5.Clip = "toReduced";
            clipPath6.Clip = "sponsorIn";
            clipPath7.Clip = "sponsorOut";
            clipPath8.Clip = "weightDivisionIn1";
            clipPath9.Clip = "weightDivisionOut1";
            clipPath10.Clip = "out";
            operatorButton1.ClipPaths.Add(clipPath1);
            operatorButton1.ClipPaths.Add(clipPath2);
            operatorButton1.ClipPaths.Add(clipPath3);
            operatorButton1.ClipPaths.Add(clipPath4);
            operatorButton1.ClipPaths.Add(clipPath5);
            operatorButton1.ClipPaths.Add(clipPath6);
            operatorButton1.ClipPaths.Add(clipPath7);
            operatorButton1.ClipPaths.Add(clipPath8);
            operatorButton1.ClipPaths.Add(clipPath9);
            operatorButton1.ClipPaths.Add(clipPath10);
            operatorButton1.Location = new Point(806, 413);
            operatorButton1.Name = "operatorButton1";
            operatorButton1.PreviewBox = PreviewBox;
            operatorButton1.Scene = "FightGraphic";
            operatorButton1.Size = new Size(263, 103);
            operatorButton1.TabIndex = 10;
            operatorButton1.UseVisualStyleBackColor = false;
            // 
            // operatorButton2
            // 
            clipPath11.Clip = "in";
            clipPath11.SceneDirector = "SceneDirector1";
            clipPath11.Track = "Track1";
            clipPath12.Clip = "out";
            clipPath12.SceneDirector = "SceneDirector1";
            clipPath12.Track = "Track1";
            operatorButton2.ClipPaths.Add(clipPath11);
            operatorButton2.ClipPaths.Add(clipPath12);
            operatorButton2.Location = new Point(806, 304);
            operatorButton2.Name = "operatorButton2";
            operatorButton2.PreviewBox = PreviewBox;
            operatorButton2.Scene = "UpcomingEvents";
            operatorButton2.Size = new Size(263, 103);
            operatorButton2.TabIndex = 10;
            operatorButton2.UseVisualStyleBackColor = false;
            // 
            // Test_Module
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1951, 1373);
            Controls.Add(operatorButton2);
            Controls.Add(operatorButton1);
            Controls.Add(PreviewBox);
            Name = "Test_Module";
            Text = "Test_Module";
            ((System.ComponentModel.ISupportInitialize)PreviewBox).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private PictureBox PreviewBox;
        private OperatorButton operatorButton1;
        private OperatorButton operatorButton2;
    }
}