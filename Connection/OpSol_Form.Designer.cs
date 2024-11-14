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
            logic_Button1 = new Logic_Button();
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
            InnerPannel.Location = new Point(583, 23);
            InnerPannel.Name = "InnerPannel";
            InnerPannel.Size = new Size(1669, 1408);
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
            clipPath2.Clip = null;
            operatorButton1.ClipPaths.Add(clipPath1);
            operatorButton1.ClipPaths.Add(clipPath2);
            operatorButton1.Location = new Point(213, 118);
            operatorButton1.Name = "operatorButton1";
            operatorButton1.PreviewBox = null;
            operatorButton1.Scene = null;
            operatorButton1.Size = new Size(332, 182);
            operatorButton1.TabIndex = 11;
            operatorButton1.UseVisualStyleBackColor = false;
            // 
            // logic_Button1
            // 
            logic_Button1.Location = new Point(44, 339);
            logic_Button1.LogicName = null;
            logic_Button1.Name = "logic_Button1";
            logic_Button1.Size = new Size(249, 201);
            logic_Button1.TabIndex = 12;
            logic_Button1.Text = "logic_Button1";
            logic_Button1.UseVisualStyleBackColor = true;
            // 
            // OpSol_Form
            // 
            AllowDrop = true;
            BackColor = Color.FromArgb(50, 50, 50);
            ClientSize = new Size(2290, 1479);
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
            ResumeLayout(false);
        }

        private Button button1;
        private Button button2;
        private TreeView treeViewModules;
        private Panel InnerPannel;
        private ModuleLoader moduleLoader1;
        private OperatorButton operatorButton1;
        private Logic_Button logic_Button1;
    }
}
