namespace OperatorsSolution
{
    partial class OpSol_Form : Form
    {
        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            treeViewModules = new TreeView();
            InnerPannel = new Panel();
            moduleLoader1 = new ModuleLoader();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(239, 66);
            button1.Name = "button1";
            button1.Size = new Size(123, 37);
            button1.TabIndex = 1;
            button1.Text = "Settings?";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnOpenSettings_Click;
            // 
            // button2
            // 
            button2.Location = new Point(228, 109);
            button2.Name = "button2";
            button2.Size = new Size(207, 34);
            button2.TabIndex = 5;
            button2.Text = "Open project";
            button2.UseVisualStyleBackColor = true;
            button2.Click += OpenProject;
            // 
            // treeViewModules
            // 
            treeViewModules.Location = new Point(31, 254);
            treeViewModules.Name = "treeViewModules";
            treeViewModules.Size = new Size(491, 822);
            treeViewModules.TabIndex = 6;
            // 
            // InnerPannel
            // 
            InnerPannel.Location = new Point(623, 47);
            InnerPannel.Name = "InnerPannel";
            InnerPannel.Size = new Size(1631, 1389);
            InnerPannel.TabIndex = 9;
            // 
            // moduleLoader1
            // 
            moduleLoader1.FormModuleDisplay = InnerPannel;
            moduleLoader1.Location = new Point(31, 1422);
            moduleLoader1.Name = "moduleLoader1";
            moduleLoader1.Size = new Size(112, 34);
            moduleLoader1.TabIndex = 10;
            moduleLoader1.Text = "moduleLoader1";
            moduleLoader1.TreeviewExplorer = treeViewModules;
            // 
            // OpSol_Form
            // 
            AllowDrop = true;
            BackColor = Color.FromArgb(50, 50, 50);
            ClientSize = new Size(2290, 1479);
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
            ResumeLayout(false);
        }

        private Button button1;
        private Button button2;
        private TreeView treeViewModules;
        private Panel InnerPannel;
        private ModuleLoader moduleLoader1;
    }
}
