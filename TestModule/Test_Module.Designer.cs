using OperatorsSolution.Controls;

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
            ClipPath clipPath13 = new ClipPath();
            ClipPath clipPath14 = new ClipPath();
            ClipPath clipPath15 = new ClipPath();
            ClipPath clipPath16 = new ClipPath();
            ClipPath clipPath17 = new ClipPath();
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
            PreviewBox.Size = new Size(653, 389);
            PreviewBox.TabIndex = 9;
            PreviewBox.TabStop = false;
            // 
            // operatorButton1
            // 
            clipPath1.Clip = "inDuoTitle";
            clipPath1.Scene = "FightGraphic";
            clipPath2.Clip = "toFightGraphic";
            clipPath2.Scene = "";
            clipPath3.Clip = "weightDivisionIn";
            clipPath3.Scene = "";
            clipPath4.Clip = "weightDivisionOut";
            clipPath4.Scene = "";
            clipPath5.Clip = "toReduced";
            clipPath5.Scene = "";
            clipPath6.Clip = "sponsorIn";
            clipPath6.Scene = "";
            clipPath7.Clip = "sponsorOut";
            clipPath7.Scene = "";
            clipPath8.Clip = "weightDivisionIn1";
            clipPath8.Scene = "";
            clipPath9.Clip = "weightDivisionOut1";
            clipPath9.Scene = "";
            clipPath10.Clip = "out";
            clipPath10.Scene = "";
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
            clipPath11.Scene = "UpcomingEvents";
            clipPath11.SceneDirector = "SceneDirector1";
            clipPath11.Track = "Track1";
            clipPath12.Clip = "out";
            clipPath12.Scene = "UpcomingEvents";
            clipPath12.SceneDirector = "SceneDirector1";
            clipPath12.Track = "Track1";
            clipPath13.Clip = "in1";
            clipPath13.Scene = "FighterEntryBlue";
            clipPath14.Clip = "out1";
            clipPath14.Scene = "FighterEntryBlue";
            clipPath15.Clip = "in4";
            clipPath15.Scene = "FighterEntryBlue";
            clipPath16.Clip = "out4";
            clipPath16.Scene = "FighterEntryBlue";
            clipPath17.Clip = "outAll";
            clipPath17.Scene = "FighterEntryBlue";
            operatorButton2.ClipPaths.Add(clipPath11);
            operatorButton2.ClipPaths.Add(clipPath12);
            operatorButton2.ClipPaths.Add(clipPath13);
            operatorButton2.ClipPaths.Add(clipPath14);
            operatorButton2.ClipPaths.Add(clipPath15);
            operatorButton2.ClipPaths.Add(clipPath16);
            operatorButton2.ClipPaths.Add(clipPath17);
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
            ClientSize = new Size(1528, 1121);
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