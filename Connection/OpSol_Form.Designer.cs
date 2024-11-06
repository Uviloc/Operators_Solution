namespace OperatorsSolution
{
    partial class OpSol_Form : Form
    {
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
            ObjectChange objectChange1 = new ObjectChange();
            ObjectChange objectChange2 = new ObjectChange();
            ObjectChange objectChange3 = new ObjectChange();
            ObjectChange objectChange4 = new ObjectChange();
            ClipPath clipPath11 = new ClipPath();
            ObjectChange objectChange5 = new ObjectChange();
            ObjectChange objectChange6 = new ObjectChange();
            ObjectChange objectChange7 = new ObjectChange();
            ObjectChange objectChange8 = new ObjectChange();
            ClipPath clipPath12 = new ClipPath();
            ClipPath clipPath13 = new ClipPath();
            ClipPath clipPath14 = new ClipPath();
            ClipPath clipPath15 = new ClipPath();
            ClipPath clipPath16 = new ClipPath();
            ClipPath clipPath17 = new ClipPath();
            button1 = new Button();
            operatorButton1 = new OperatorButton();
            operatorButton2 = new OperatorButton();
            pictureBox1 = new PictureBox();
            operatorButton3 = new OperatorButton();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(12, 12);
            button1.Name = "button1";
            button1.Size = new Size(123, 37);
            button1.TabIndex = 1;
            button1.Text = "Settings?";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnOpenSettings_Click;
            // 
            // operatorButton1
            // 
            clipPath1.Clip = "in1";
            clipPath1.Scene = "FighterEntryRed";
            clipPath2.Clip = "out1";
            clipPath2.Scene = "FighterEntryRed";
            clipPath3.Clip = "in2";
            clipPath3.Scene = "FighterEntryRed";
            clipPath4.Clip = "out2";
            clipPath4.Scene = "FighterEntryRed";
            clipPath5.Clip = "in3";
            clipPath5.Scene = "FighterEntryRed";
            clipPath6.Clip = "out3";
            clipPath6.Scene = "FighterEntryRed";
            clipPath7.Clip = "in4";
            clipPath7.Scene = "FighterEntryRed";
            clipPath8.Clip = "out4";
            clipPath8.Scene = "FighterEntryRed";
            clipPath9.Clip = "outAll";
            clipPath9.Scene = "FighterEntryRed";
            operatorButton1.ClipPaths.Add(clipPath1);
            operatorButton1.ClipPaths.Add(clipPath2);
            operatorButton1.ClipPaths.Add(clipPath3);
            operatorButton1.ClipPaths.Add(clipPath4);
            operatorButton1.ClipPaths.Add(clipPath5);
            operatorButton1.ClipPaths.Add(clipPath6);
            operatorButton1.ClipPaths.Add(clipPath7);
            operatorButton1.ClipPaths.Add(clipPath8);
            operatorButton1.ClipPaths.Add(clipPath9);
            operatorButton1.Location = new Point(824, 12);
            operatorButton1.Name = "operatorButton1";
            operatorButton1.Scene = null;
            operatorButton1.Size = new Size(261, 169);
            operatorButton1.TabIndex = 2;
            operatorButton1.UseVisualStyleBackColor = false;
            operatorButton1.Click += Trigger_Clips;
            // 
            // operatorButton2
            // 
            clipPath10.Clip = "inDuoTitle";
            objectChange1.SceneObject = "FighterLeft";
            objectChange1.SetTo = "Fred Sikking";
            objectChange2.SceneObject = "FlagLeft";
            objectChange2.SetTo = "The Netherlands";
            objectChange3.SceneObject = "FighterRight";
            objectChange3.SetTo = "Djamara Vers";
            objectChange4.SceneObject = "FlagRight";
            objectChange4.SetTo = "Surinam";
            clipPath10.ObjectChanges.Add(objectChange1);
            clipPath10.ObjectChanges.Add(objectChange2);
            clipPath10.ObjectChanges.Add(objectChange3);
            clipPath10.ObjectChanges.Add(objectChange4);
            clipPath10.Scene = "FightGraphic";
            clipPath11.Clip = "toFightGraphic";
            objectChange5.SceneObject = "FlagLeft";
            objectChange5.SetTo = "The Netherlands";
            objectChange6.SceneObject = "FlagRight";
            objectChange6.SetTo = "Surinam";
            objectChange7.SceneObject = "FighterLeft";
            objectChange7.SetTo = "Fred Sikking";
            objectChange8.SceneObject = "FighterRight";
            objectChange8.SetTo = "Djamara Vers";
            clipPath11.ObjectChanges.Add(objectChange5);
            clipPath11.ObjectChanges.Add(objectChange6);
            clipPath11.ObjectChanges.Add(objectChange7);
            clipPath11.ObjectChanges.Add(objectChange8);
            clipPath11.Scene = "FightGraphic";
            clipPath12.Clip = "toReduced";
            clipPath12.Scene = "FightGraphic";
            clipPath13.Clip = "weightDivisionIn1";
            clipPath13.Scene = "FightGraphic";
            clipPath14.Clip = "weightDivisionOut1";
            clipPath14.Scene = "FightGraphic";
            clipPath15.Clip = "out";
            clipPath15.Scene = "FightGraphic";
            operatorButton2.ClipPaths.Add(clipPath10);
            operatorButton2.ClipPaths.Add(clipPath11);
            operatorButton2.ClipPaths.Add(clipPath12);
            operatorButton2.ClipPaths.Add(clipPath13);
            operatorButton2.ClipPaths.Add(clipPath14);
            operatorButton2.ClipPaths.Add(clipPath15);
            operatorButton2.Location = new Point(12, 79);
            operatorButton2.Name = "operatorButton2";
            operatorButton2.Scene = "FightGraphic";
            operatorButton2.Size = new Size(431, 244);
            operatorButton2.TabIndex = 3;
            operatorButton2.UseVisualStyleBackColor = false;
            operatorButton2.Click += Trigger_Clips;
            operatorButton2.Enter += DisplayThumbnail;
            operatorButton2.Leave += RemoveThumbnail;
            operatorButton2.MouseEnter += DisplayThumbnail;
            operatorButton2.MouseLeave += RemoveThumbnail;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(24, 502);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(506, 298);
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // operatorButton3
            // 
            clipPath16.Clip = "weightDivisionIn";
            clipPath16.Scene = "FightGraphic";
            clipPath17.Clip = "weightDivisionOut";
            clipPath17.Scene = "FightGraphic";
            operatorButton3.ClipPaths.Add(clipPath16);
            operatorButton3.ClipPaths.Add(clipPath17);
            operatorButton3.Location = new Point(460, 203);
            operatorButton3.Name = "operatorButton3";
            operatorButton3.Scene = "FightGraphic";
            operatorButton3.Size = new Size(431, 244);
            operatorButton3.TabIndex = 3;
            operatorButton3.UseVisualStyleBackColor = false;
            operatorButton3.Click += Trigger_Clips;
            operatorButton3.Enter += DisplayThumbnail;
            operatorButton3.Leave += RemoveThumbnail;
            operatorButton3.MouseEnter += DisplayThumbnail;
            operatorButton3.MouseLeave += RemoveThumbnail;
            // 
            // button2
            // 
            button2.Location = new Point(878, 766);
            button2.Name = "button2";
            button2.Size = new Size(207, 34);
            button2.TabIndex = 5;
            button2.Text = "Open project";
            button2.UseVisualStyleBackColor = true;
            button2.Click += OpenProject;
            // 
            // OpSol_Form
            // 
            AllowDrop = true;
            BackColor = Color.FromArgb(50, 50, 50);
            ClientSize = new Size(1097, 829);
            Controls.Add(button2);
            Controls.Add(pictureBox1);
            Controls.Add(operatorButton3);
            Controls.Add(operatorButton2);
            Controls.Add(operatorButton1);
            Controls.Add(button1);
            ForeColor = SystemColors.ControlText;
            Name = "OpSol_Form";
            ShowIcon = false;
            Text = "Operators Solution";
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        private Button button1;
        private OperatorButton operatorButton1;
        private OperatorButton operatorButton2;
        private PictureBox pictureBox1;
        private OperatorButton operatorButton3;
        private Button button2;
    }
}
