namespace OperatorsSolution
{
    partial class OpSol_Form : Form
    {
        private void InitializeComponent()
        {
            button1 = new Button();
            operatorButton1 = new OperatorButton();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(946, 763);
            button1.Name = "button1";
            button1.Size = new Size(123, 37);
            button1.TabIndex = 1;
            button1.Text = "Settings?";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnOpenSettings_Click;
            // 
            // operatorButton1
            // 
            operatorButton1.Location = new Point(42, 46);
            operatorButton1.Name = "operatorButton1";
            operatorButton1.Size = new Size(277, 164);
            operatorButton1.TabIndex = 2;
            operatorButton1.UseVisualStyleBackColor = false;
            operatorButton1.Click += DisplayPreview;
            // 
            // OpSol_Form
            // 
            AllowDrop = true;
            BackColor = Color.FromArgb(50, 50, 50);
            ClientSize = new Size(1097, 829);
            Controls.Add(operatorButton1);
            Controls.Add(button1);
            ForeColor = SystemColors.ControlText;
            Name = "OpSol_Form";
            ShowIcon = false;
            Text = "Operators Solution";
            TopMost = true;
            ResumeLayout(false);
        }

        private OperatorButton operatorButton1;
        private Button button1;
    }
}
