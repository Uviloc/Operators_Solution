namespace OperatorsSolution
{
    partial class OpSol_Form : Form
    {
        private void InitializeComponent()
        {
            ClipPath clipPath1 = new ClipPath();
            ClipPath clipPath2 = new ClipPath();
            operatorButton1 = new OperatorButton();
            SuspendLayout();
            // 
            // operatorButton1
            // 
            clipPath1.Clip = "inDuoTitle";
            clipPath1.Scene = "FightGraphic";
            clipPath2.Clip = "toFightGraphic";
            clipPath2.Scene = "FightGraphic";
            operatorButton1.ClipPath.Add(clipPath1);
            operatorButton1.ClipPath.Add(clipPath2);
            operatorButton1.Location = new Point(361, 275);
            operatorButton1.Name = "operatorButton1";
            operatorButton1.Size = new Size(112, 34);
            operatorButton1.TabIndex = 0;
            operatorButton1.UseVisualStyleBackColor = false;
            operatorButton1.Click += Trigger_Clips;
            // 
            // OpSol_Form
            // 
            AllowDrop = true;
            BackColor = Color.FromArgb(50, 50, 50);
            ClientSize = new Size(1097, 829);
            Controls.Add(operatorButton1);
            ForeColor = SystemColors.ControlText;
            Name = "OpSol_Form";
            ShowIcon = false;
            Text = "Operators Solution";
            TopMost = true;
            ResumeLayout(false);
        }

        private OperatorButton operatorButton2;
        private OperatorButton operatorButton1;
    }
}
