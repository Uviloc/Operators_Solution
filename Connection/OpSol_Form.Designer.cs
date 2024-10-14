namespace OperatorsSolution
{
    partial class OpSol_Form : Form
    {
        private void InitializeComponent()
        {
            operatorButton1 = new OperatorButton();
            SuspendLayout();
            // 
            // operatorButton1
            // 
            operatorButton1.Clip = "inDuoTitle";
            operatorButton1.Location = new Point(277, 219);
            operatorButton1.Name = "operatorButton1";
            operatorButton1.Scene = "FightGraphic";
            operatorButton1.Size = new Size(358, 180);
            operatorButton1.TabIndex = 0;
            operatorButton1.UseVisualStyleBackColor = false;
            // 
            // OpSol_Form
            // 
            AllowDrop = true;
            BackColor = Color.FromArgb(50, 50, 50);
            ClientSize = new Size(1059, 769);
            Controls.Add(operatorButton1);
            ForeColor = SystemColors.ControlText;
            Name = "OpSol_Form";
            ShowIcon = false;
            Text = "Operators Solution";
            TopMost = true;
            ResumeLayout(false);
        }

        private OperatorButton operatorButton1;
    }
}
