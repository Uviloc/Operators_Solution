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
            tabControl1 = new TabControl();
            Operation = new TabPage();
            DataBase = new TabPage();
            Settings = new TabPage();
            CollapseControlPanel1 = new Button();
            ControlPanel1 = new Panel();
            OperationButton = new Button();
            DataBaseButton = new Button();
            SettingsButton = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            InnerPannel.SuspendLayout();
            tabControl1.SuspendLayout();
            ControlPanel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button2.Location = new Point(417, 200);
            button2.Name = "button2";
            button2.Size = new Size(200, 34);
            button2.TabIndex = 5;
            button2.Text = "Open project";
            button2.UseVisualStyleBackColor = true;
            button2.Click += OpenProject;
            // 
            // treeViewModules
            // 
            treeViewModules.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            treeViewModules.BackColor = Color.Black;
            treeViewModules.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            treeViewModules.ForeColor = Color.White;
            treeViewModules.LineColor = Color.White;
            treeViewModules.Location = new Point(5, 240);
            treeViewModules.MinimumSize = new Size(0, 10);
            treeViewModules.Name = "treeViewModules";
            treeViewModules.Size = new Size(612, 1229);
            treeViewModules.TabIndex = 6;
            treeViewModules.NodeMouseDoubleClick += OpenForm;
            // 
            // InnerPannel
            // 
            InnerPannel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            InnerPannel.BorderStyle = BorderStyle.Fixed3D;
            InnerPannel.Controls.Add(tabControl1);
            InnerPannel.Location = new Point(659, 3);
            InnerPannel.Name = "InnerPannel";
            InnerPannel.Size = new Size(1572, 1472);
            InnerPannel.TabIndex = 9;
            InnerPannel.SizeChanged += FormModulePanel_SizeChanged;
            // 
            // tabControl1
            // 
            tabControl1.Appearance = TabAppearance.Buttons;
            tabControl1.Controls.Add(Operation);
            tabControl1.Controls.Add(DataBase);
            tabControl1.Controls.Add(Settings);
            tabControl1.ItemSize = new Size(20000, 200);
            tabControl1.Location = new Point(835, 119);
            tabControl1.Name = "tabControl1";
            tabControl1.Padding = new Point(20, 3);
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(499, 832);
            tabControl1.SizeMode = TabSizeMode.FillToRight;
            tabControl1.TabIndex = 0;
            // 
            // Operation
            // 
            Operation.ForeColor = Color.White;
            Operation.Location = new Point(4, 204);
            Operation.Name = "Operation";
            Operation.Padding = new Padding(3);
            Operation.Size = new Size(491, 624);
            Operation.TabIndex = 0;
            Operation.Text = "Operation";
            // 
            // DataBase
            // 
            DataBase.Location = new Point(4, 204);
            DataBase.Name = "DataBase";
            DataBase.Padding = new Padding(3);
            DataBase.Size = new Size(491, 624);
            DataBase.TabIndex = 1;
            DataBase.Text = "DataBase";
            // 
            // Settings
            // 
            Settings.Location = new Point(4, 204);
            Settings.Name = "Settings";
            Settings.Size = new Size(491, 624);
            Settings.TabIndex = 2;
            Settings.Text = "Settings";
            // 
            // CollapseControlPanel1
            // 
            CollapseControlPanel1.BackColor = Color.FromArgb(150, 10, 10, 10);
            CollapseControlPanel1.Dock = DockStyle.Fill;
            CollapseControlPanel1.FlatAppearance.BorderSize = 0;
            CollapseControlPanel1.FlatStyle = FlatStyle.Flat;
            CollapseControlPanel1.Font = new Font("Arial", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CollapseControlPanel1.ForeColor = Color.White;
            CollapseControlPanel1.Location = new Point(629, 3);
            CollapseControlPanel1.MinimumSize = new Size(30, 100);
            CollapseControlPanel1.Name = "CollapseControlPanel1";
            CollapseControlPanel1.Size = new Size(30, 1472);
            CollapseControlPanel1.TabIndex = 18;
            CollapseControlPanel1.Text = "<";
            CollapseControlPanel1.UseVisualStyleBackColor = false;
            CollapseControlPanel1.Click += CollapseControlPanel;
            CollapseControlPanel1.MouseEnter += ButtonEnter;
            CollapseControlPanel1.MouseLeave += ButtonLeave;
            // 
            // ControlPanel1
            // 
            ControlPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            ControlPanel1.AutoScroll = true;
            ControlPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ControlPanel1.Controls.Add(OperationButton);
            ControlPanel1.Controls.Add(treeViewModules);
            ControlPanel1.Controls.Add(DataBaseButton);
            ControlPanel1.Controls.Add(button2);
            ControlPanel1.Controls.Add(SettingsButton);
            ControlPanel1.Location = new Point(3, 3);
            ControlPanel1.MaximumSize = new Size(640, 1000000000);
            ControlPanel1.Name = "ControlPanel1";
            ControlPanel1.Size = new Size(620, 1472);
            ControlPanel1.TabIndex = 17;
            // 
            // OperationButton
            // 
            OperationButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            OperationButton.Location = new Point(5, -6);
            OperationButton.Name = "OperationButton";
            OperationButton.Size = new Size(200, 200);
            OperationButton.TabIndex = 7;
            OperationButton.Text = "Operation";
            OperationButton.UseVisualStyleBackColor = true;
            // 
            // DataBaseButton
            // 
            DataBaseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            DataBaseButton.Location = new Point(211, -6);
            DataBaseButton.Name = "DataBaseButton";
            DataBaseButton.Size = new Size(200, 200);
            DataBaseButton.TabIndex = 7;
            DataBaseButton.Text = "DataBase";
            DataBaseButton.UseVisualStyleBackColor = true;
            // 
            // SettingsButton
            // 
            SettingsButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            SettingsButton.Location = new Point(417, -6);
            SettingsButton.Name = "SettingsButton";
            SettingsButton.Size = new Size(200, 200);
            SettingsButton.TabIndex = 1;
            SettingsButton.Text = "Settings";
            SettingsButton.UseVisualStyleBackColor = true;
            SettingsButton.Click += btnOpenSettings_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(CollapseControlPanel1, 1, 0);
            tableLayoutPanel1.Controls.Add(ControlPanel1, 0, 0);
            tableLayoutPanel1.Controls.Add(InnerPannel, 2, 0);
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
            AutoScaleMode = AutoScaleMode.Inherit;
            BackColor = Color.FromArgb(32, 32, 32);
            ClientSize = new Size(2258, 1505);
            ControlPanel = ControlPanel1;
            Controls.Add(tableLayoutPanel1);
            ForeColor = Color.FromArgb(60, 60, 60);
            FormModulePanel = InnerPannel;
            MinimumSize = new Size(1150, 500);
            Name = "OpSol_Form";
            ShowIcon = false;
            Text = "Operators Solution";
            TopMost = true;
            TreeviewExplorer = treeViewModules;
            InnerPannel.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
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
        private TabControl tabControl1;
        private TabPage Operation;
        private TabPage DataBase;
        private TabPage Settings;
    }
}
