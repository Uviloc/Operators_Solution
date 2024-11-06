namespace OperatorsSolution
{
    partial class OpSol_Form : Form
    {
        private void InitializeComponent()
        {
            ClipPath clipPath2 = new ClipPath();
            button1 = new Button();
            PreviewBox = new PictureBox();
            button2 = new Button();
            treeViewModules = new TreeView();
            operatorButton1 = new OperatorButton();
            operatorButton2 = new OperatorButton();
            InnerPannel = new Panel();
            ((System.ComponentModel.ISupportInitialize)PreviewBox).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(2063, 1368);
            button1.Name = "button1";
            button1.Size = new Size(123, 37);
            button1.TabIndex = 1;
            button1.Text = "Settings?";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnOpenSettings_Click;
            // 
            // PreviewBox
            // 
            PreviewBox.Location = new Point(1551, 1158);
            PreviewBox.Name = "PreviewBox";
            PreviewBox.Size = new Size(506, 298);
            PreviewBox.TabIndex = 4;
            PreviewBox.TabStop = false;
            // 
            // button2
            // 
            button2.Location = new Point(2063, 1422);
            button2.Name = "button2";
            button2.Size = new Size(207, 34);
            button2.TabIndex = 5;
            button2.Text = "Open project";
            button2.UseVisualStyleBackColor = true;
            button2.Click += OpenProject;
            // 
            // treeViewModules
            // 
            treeViewModules.Location = new Point(31, 65);
            treeViewModules.Name = "treeViewModules";
            treeViewModules.Size = new Size(191, 436);
            treeViewModules.TabIndex = 6;
            treeViewModules.NodeMouseDoubleClick += treeViewModules_NodeMouseDoubleClick;
            // 
            // operatorButton1
            // 
            clipPath2.Clip = "inDuoTitle";
            clipPath2.Scene = "FightGraphic";
            operatorButton1.ClipPaths.Add(clipPath2);
            operatorButton1.Location = new Point(2063, 1181);
            operatorButton1.Name = "operatorButton1";
            operatorButton1.Scene = null;
            operatorButton1.Size = new Size(215, 150);
            operatorButton1.TabIndex = 7;
            operatorButton1.UseVisualStyleBackColor = false;
            operatorButton1.Click += Trigger_Clips;
            operatorButton1.Enter += DisplayThumbnail;
            operatorButton1.Leave += RemoveThumbnail;
            // 
            // operatorButton2
            // 
            operatorButton2.Location = new Point(66, 569);
            operatorButton2.Name = "operatorButton2";
            operatorButton2.Scene = "FightGraphic";
            operatorButton2.Size = new Size(112, 34);
            operatorButton2.TabIndex = 8;
            operatorButton2.UseVisualStyleBackColor = false;
            operatorButton2.Hover += DisplayThumbnail;
            // 
            // InnerPannel
            // 
            InnerPannel.Location = new Point(599, 42);
            InnerPannel.Name = "InnerPannel";
            InnerPannel.Size = new Size(956, 1012);
            InnerPannel.TabIndex = 9;
            // 
            // OpSol_Form
            // 
            AllowDrop = true;
            BackColor = Color.FromArgb(50, 50, 50);
            ClientSize = new Size(2290, 1479);
            Controls.Add(operatorButton2);
            Controls.Add(InnerPannel);
            Controls.Add(operatorButton1);
            Controls.Add(treeViewModules);
            Controls.Add(button2);
            Controls.Add(PreviewBox);
            Controls.Add(button1);
            ForeColor = SystemColors.ControlText;
            Name = "OpSol_Form";
            ShowIcon = false;
            Text = "Operators Solution";
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)PreviewBox).EndInit();
            ResumeLayout(false);
        }

        private Button button1;
        private PictureBox PreviewBox;
        private Button button2;
        private TreeView treeViewModules;
        private OperatorButton operatorButton1;
        private OperatorButton operatorButton2;
        private Panel InnerPannel;
    }
}
