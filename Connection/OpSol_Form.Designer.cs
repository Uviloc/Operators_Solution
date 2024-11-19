using OperatorsSolution.Controls;

namespace OperatorsSolution
{
    partial class OpSol_Form : Form
    {
        private void InitializeComponent()
        {
            ClipPath clipPath1 = new ClipPath();
            ClipPath clipPath2 = new ClipPath();
            button1 = new Button();
            button2 = new Button();
            treeViewModules = new TreeView();
            InnerPannel = new Panel();
            moduleLoader1 = new ModuleLoader();
            operatorButton1 = new OperatorButton();
            pictureBox1 = new PictureBox();
            logic_Button1 = new Logic_Button();
            textBox1 = new TextBox();
            button3 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(435, 503);
            button1.Name = "button1";
            button1.Size = new Size(123, 37);
            button1.TabIndex = 1;
            button1.Text = "Settings?";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnOpenSettings_Click;
            // 
            // button2
            // 
            button2.Location = new Point(351, 546);
            button2.Name = "button2";
            button2.Size = new Size(207, 34);
            button2.TabIndex = 5;
            button2.Text = "Open project";
            button2.UseVisualStyleBackColor = true;
            button2.Click += OpenProject;
            // 
            // treeViewModules
            // 
            treeViewModules.Location = new Point(67, 586);
            treeViewModules.Name = "treeViewModules";
            treeViewModules.Size = new Size(491, 822);
            treeViewModules.TabIndex = 6;
            // 
            // InnerPannel
            // 
            InnerPannel.Location = new Point(651, 23);
            InnerPannel.Name = "InnerPannel";
            InnerPannel.Size = new Size(1601, 1408);
            InnerPannel.TabIndex = 9;
            // 
            // moduleLoader1
            // 
            moduleLoader1.FormModuleDisplay = InnerPannel;
            moduleLoader1.Location = new Point(67, 1433);
            moduleLoader1.Name = "moduleLoader1";
            moduleLoader1.Size = new Size(112, 34);
            moduleLoader1.TabIndex = 10;
            moduleLoader1.Text = "moduleLoader1";
            moduleLoader1.TreeviewExplorer = treeViewModules;
            // 
            // operatorButton1
            // 
            clipPath1.Clip = "in";
            clipPath1.Scene = "UpcomingEvents";
            clipPath1.SceneDirector = "SceneDirector1";
            clipPath1.Track = "Track1";
            clipPath2.Clip = null;
            clipPath2.Scene = "UpcomingEvents";
            clipPath2.SceneDirector = "SceneDirector1";
            clipPath2.Track = "Track1";
            operatorButton1.ClipPaths.Add(clipPath1);
            operatorButton1.ClipPaths.Add(clipPath2);
            operatorButton1.Location = new Point(27, 23);
            operatorButton1.Name = "operatorButton1";
            operatorButton1.PreviewBox = pictureBox1;
            operatorButton1.Scene = "UpcomingEvents";
            operatorButton1.Size = new Size(332, 182);
            operatorButton1.TabIndex = 11;
            operatorButton1.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(385, 36);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(150, 75);
            pictureBox1.TabIndex = 13;
            pictureBox1.TabStop = false;
            // 
            // logic_Button1
            // 
            logic_Button1.ColumnCount = 1;
            logic_Button1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            logic_Button1.Location = new Point(198, 1357);
            logic_Button1.LogicName = null;
            logic_Button1.Name = "logic_Button1";
            logic_Button1.RowCount = 1;
            logic_Button1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            logic_Button1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            logic_Button1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            logic_Button1.Size = new Size(461, 168);
            logic_Button1.TabIndex = 12;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(121, 304);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(311, 31);
            textBox1.TabIndex = 14;
            // 
            // button3
            // 
            button3.Location = new Point(504, 335);
            button3.Name = "button3";
            button3.Size = new Size(126, 115);
            button3.TabIndex = 15;
            button3.Text = "button3";
            button3.UseVisualStyleBackColor = true;
            button3.Click += TestButton;
            // 
            // OpSol_Form
            // 
            AllowDrop = true;
            BackColor = Color.FromArgb(50, 50, 50);
            ClientSize = new Size(2290, 1479);
            Controls.Add(button3);
            Controls.Add(textBox1);
            Controls.Add(pictureBox1);
            Controls.Add(logic_Button1);
            Controls.Add(operatorButton1);
            Controls.Add(moduleLoader1);
            Controls.Add(InnerPannel);
            Controls.Add(treeViewModules);
            Controls.Add(button2);
            Controls.Add(button1);
            ForeColor = SystemColors.ControlText;
            Name = "OpSol_Form";
            ShowIcon = false;
            Text = "Operators Solution";
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Button button1;
        private Button button2;
        private TreeView treeViewModules;
        private Panel InnerPannel;
        private ModuleLoader moduleLoader1;
        private OperatorButton operatorButton1;
        private Logic_Button logic_Button1;
        private PictureBox pictureBox1;
        private TextBox textBox1;
        private Button button3;
    }
}
