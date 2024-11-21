using OperatorsSolution.Controls;

namespace OperatorsSolution
{
    partial class OpSol_Form : CustomForm
    {
        private void InitializeComponent()
        {
            button2 = new Button();
            treeViewModules = new TreeView();
            InnerPannel = new Panel();
            CollapseControlPanel1 = new Button();
            ControlPanel1 = new Panel();
            OperationButton = new Button();
            DataBaseButton = new Button();
            SettingsButton = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            ControlPanel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // button2
            // 
            button2.Location = new Point(428, 223);
            button2.Name = "button2";
            button2.Size = new Size(200, 34);
            button2.TabIndex = 5;
            button2.Text = "Open project";
            button2.UseVisualStyleBackColor = true;
            button2.Click += OpenProject;
            // 
            // treeViewModules
            // 
            treeViewModules.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            treeViewModules.Location = new Point(3, 279);
            treeViewModules.MinimumSize = new Size(0, 10);
            treeViewModules.Name = "treeViewModules";
            treeViewModules.Size = new Size(622, 1190);
            treeViewModules.TabIndex = 6;
            treeViewModules.NodeMouseDoubleClick += OpenForm;
            // 
            // InnerPannel
            // 
            InnerPannel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            InnerPannel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            InnerPannel.Location = new Point(637, 3);
            InnerPannel.Name = "InnerPannel";
            InnerPannel.Size = new Size(1594, 1472);
            InnerPannel.TabIndex = 9;
            InnerPannel.SizeChanged += FormModulePanel_SizeChanged;
            // 
            // CollapseControlPanel1
            // 
            CollapseControlPanel1.Location = new Point(582, -20);
            CollapseControlPanel1.Name = "CollapseControlPanel1";
            CollapseControlPanel1.Size = new Size(58, 42);
            CollapseControlPanel1.TabIndex = 18;
            CollapseControlPanel1.Text = "button1";
            CollapseControlPanel1.UseVisualStyleBackColor = true;
            CollapseControlPanel1.Click += CollapseControlPanel;
            // 
            // ControlPanel1
            // 
            ControlPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            ControlPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ControlPanel1.Controls.Add(OperationButton);
            ControlPanel1.Controls.Add(treeViewModules);
            ControlPanel1.Controls.Add(DataBaseButton);
            ControlPanel1.Controls.Add(button2);
            ControlPanel1.Controls.Add(SettingsButton);
            ControlPanel1.Location = new Point(3, 3);
            ControlPanel1.MaximumSize = new Size(640, 1000000000);
            ControlPanel1.Name = "ControlPanel1";
            ControlPanel1.Size = new Size(628, 1472);
            ControlPanel1.TabIndex = 17;
            // 
            // OperationButton
            // 
            OperationButton.Location = new Point(16, 17);
            OperationButton.Name = "OperationButton";
            OperationButton.Size = new Size(200, 200);
            OperationButton.TabIndex = 7;
            OperationButton.Text = "button3";
            OperationButton.UseVisualStyleBackColor = true;
            // 
            // DataBaseButton
            // 
            DataBaseButton.Location = new Point(222, 17);
            DataBaseButton.Name = "DataBaseButton";
            DataBaseButton.Size = new Size(200, 200);
            DataBaseButton.TabIndex = 7;
            DataBaseButton.Text = "button3";
            DataBaseButton.UseVisualStyleBackColor = true;
            // 
            // SettingsButton
            // 
            SettingsButton.Location = new Point(428, 17);
            SettingsButton.Name = "SettingsButton";
            SettingsButton.Size = new Size(200, 200);
            SettingsButton.TabIndex = 1;
            SettingsButton.Text = "Settings?";
            SettingsButton.UseVisualStyleBackColor = true;
            SettingsButton.Click += btnOpenSettings_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(ControlPanel1, 0, 0);
            tableLayoutPanel1.Controls.Add(InnerPannel, 1, 0);
            tableLayoutPanel1.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tableLayoutPanel1.Location = new Point(12, 15);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(2234, 1478);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // OpSol_Form
            // 
            AllowDrop = true;
            BackColor = Color.FromArgb(32, 32, 32);
            ClientSize = new Size(2258, 1505);
            ControlPanel = ControlPanel1;
            Controls.Add(tableLayoutPanel1);
            Controls.Add(CollapseControlPanel1);
            ForeColor = Color.FromArgb(60, 60, 60);
            FormModulePanel = InnerPannel;
            MinimumSize = new Size(1150, 500);
            Name = "OpSol_Form";
            ShowIcon = false;
            Text = "Operators Solution";
            TopMost = true;
            TreeviewExplorer = treeViewModules;
            ControlPanel1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        private Button button2;
        private TreeView treeViewModules;
        private Panel InnerPannel;
        private Panel ControlPanel1;
        private Button OperationButton;
        private Button SettingsButton;
        private Button DataBaseButton;
        private Button CollapseControlPanel1;
        private System.ComponentModel.IContainer components;
        private TableLayoutPanel tableLayoutPanel1;
    }
}
