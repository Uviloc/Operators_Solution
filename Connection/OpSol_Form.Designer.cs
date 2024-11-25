using OperatorsSolution.Controls;

namespace OperatorsSolution
{
    partial class OpSol_Form : CustomForm
    {
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpSol_Form));
            button2 = new Button();
            treeViewModules = new TreeView();
            treeView1 = new TreeView();
            tabControl1 = new TabControl();
            Operation = new TabPage();
            DataBase = new TabPage();
            Settings = new TabPage();
            textBox1 = new TextBox();
            button1 = new Button();
            textBox2 = new TextBox();
            ProjectFile = new TextBox();
            textBox5 = new TextBox();
            GraphicsSoftwareOption = new ComboBox();
            textBox6 = new TextBox();
            textBox8 = new TextBox();
            textBox4 = new TextBox();
            textBox3 = new TextBox();
            textBox7 = new TextBox();
            CollapseControlPanel1 = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            innerPanel2 = new TabControl();
            tab1 = new TabPage();
            label1 = new Label();
            tab2 = new TabPage();
            tab3 = new TabPage();
            controlPanel = new Panel();
            ToolTip = new ToolTip(components);
            tabControl1.SuspendLayout();
            Operation.SuspendLayout();
            DataBase.SuspendLayout();
            Settings.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            innerPanel2.SuspendLayout();
            tab1.SuspendLayout();
            controlPanel.SuspendLayout();
            SuspendLayout();
            // 
            // button2
            // 
            button2.BackColor = Color.White;
            button2.ForeColor = Color.Black;
            button2.Location = new Point(16, 138);
            button2.Name = "button2";
            button2.Size = new Size(200, 34);
            button2.TabIndex = 5;
            button2.Text = "Open project";
            button2.UseVisualStyleBackColor = false;
            button2.Click += OpenProject;
            // 
            // treeViewModules
            // 
            treeViewModules.BackColor = Color.Black;
            treeViewModules.Dock = DockStyle.Fill;
            treeViewModules.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            treeViewModules.ForeColor = Color.White;
            treeViewModules.LineColor = Color.White;
            treeViewModules.Location = new Point(0, 0);
            treeViewModules.Name = "treeViewModules";
            treeViewModules.Size = new Size(609, 1264);
            treeViewModules.TabIndex = 6;
            treeViewModules.NodeMouseDoubleClick += OpenExternalForm;
            // 
            // treeView1
            // 
            treeView1.BackColor = Color.Black;
            treeView1.Font = new Font("Segoe UI", 14F);
            treeView1.ForeColor = Color.White;
            treeView1.Location = new Point(9, 96);
            treeView1.MinimumSize = new Size(0, 10);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(597, 1062);
            treeView1.TabIndex = 0;
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            tabControl1.Appearance = TabAppearance.Buttons;
            tabControl1.Controls.Add(Operation);
            tabControl1.Controls.Add(DataBase);
            tabControl1.Controls.Add(Settings);
            tabControl1.ItemSize = new Size(20000, 200);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Margin = new Padding(0);
            tabControl1.Name = "tabControl1";
            tabControl1.Padding = new Point(20, 3);
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(617, 1472);
            tabControl1.SizeMode = TabSizeMode.FillToRight;
            tabControl1.TabIndex = 0;
            tabControl1.SelectedIndexChanged += TabChange;
            // 
            // Operation
            // 
            Operation.Controls.Add(treeViewModules);
            Operation.Location = new Point(4, 204);
            Operation.Margin = new Padding(0);
            Operation.Name = "Operation";
            Operation.Size = new Size(609, 1264);
            Operation.TabIndex = 0;
            Operation.Text = "         Operation         ";
            // 
            // DataBase
            // 
            DataBase.BackColor = Color.FromArgb(60, 60, 60);
            DataBase.Controls.Add(treeView1);
            DataBase.ForeColor = Color.White;
            DataBase.Location = new Point(4, 204);
            DataBase.Name = "DataBase";
            DataBase.Padding = new Padding(3);
            DataBase.Size = new Size(609, 1264);
            DataBase.TabIndex = 1;
            DataBase.Text = "         DataBase         ";
            // 
            // Settings
            // 
            Settings.AutoScroll = true;
            Settings.BackColor = Color.FromArgb(60, 60, 60);
            Settings.Controls.Add(textBox1);
            Settings.Controls.Add(button1);
            Settings.Controls.Add(textBox2);
            Settings.Controls.Add(button2);
            Settings.Controls.Add(ProjectFile);
            Settings.Controls.Add(textBox5);
            Settings.Controls.Add(GraphicsSoftwareOption);
            Settings.Controls.Add(textBox6);
            Settings.Controls.Add(textBox8);
            Settings.Controls.Add(textBox4);
            Settings.Controls.Add(textBox3);
            Settings.Controls.Add(textBox7);
            Settings.ForeColor = Color.White;
            Settings.Location = new Point(4, 204);
            Settings.Name = "Settings";
            Settings.Size = new Size(609, 1264);
            Settings.TabIndex = 2;
            Settings.Text = "         Settings         ";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(16, 25);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(165, 33);
            textBox1.TabIndex = 11;
            textBox1.Text = "Graphics Program:";
            // 
            // button1
            // 
            button1.Location = new Point(458, 101);
            button1.Name = "button1";
            button1.Size = new Size(35, 31);
            button1.TabIndex = 15;
            button1.Text = "X";
            button1.UseVisualStyleBackColor = true;
            button1.Click += RemoveProjectFileRef;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(16, 645);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(498, 589);
            textBox2.TabIndex = 12;
            textBox2.Text = resources.GetString("textBox2.Text");
            // 
            // ProjectFile
            // 
            ProjectFile.Location = new Point(180, 101);
            ProjectFile.Name = "ProjectFile";
            ProjectFile.PlaceholderText = "Select project file";
            ProjectFile.ReadOnly = true;
            ProjectFile.Size = new Size(283, 31);
            ProjectFile.TabIndex = 14;
            ProjectFile.Click += ProjectSelection;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(16, 223);
            textBox5.Name = "textBox5";
            textBox5.ReadOnly = true;
            textBox5.Size = new Size(165, 31);
            textBox5.TabIndex = 13;
            textBox5.Text = "Preview on hover";
            // 
            // GraphicsSoftwareOption
            // 
            GraphicsSoftwareOption.FormattingEnabled = true;
            GraphicsSoftwareOption.Items.AddRange(new object[] { "1", "2", "3" });
            GraphicsSoftwareOption.Location = new Point(180, 25);
            GraphicsSoftwareOption.Name = "GraphicsSoftwareOption";
            GraphicsSoftwareOption.Size = new Size(182, 33);
            GraphicsSoftwareOption.TabIndex = 9;
            GraphicsSoftwareOption.Text = "Graphics Program";
            GraphicsSoftwareOption.SelectionChangeCommitted += SaveGraphicsSoftwareOption;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(16, 277);
            textBox6.Name = "textBox6";
            textBox6.ReadOnly = true;
            textBox6.Size = new Size(165, 31);
            textBox6.TabIndex = 13;
            textBox6.Text = "Visual style";
            // 
            // textBox8
            // 
            textBox8.Location = new Point(16, 382);
            textBox8.Name = "textBox8";
            textBox8.ReadOnly = true;
            textBox8.Size = new Size(398, 31);
            textBox8.TabIndex = 13;
            textBox8.Text = "Display keyboard shortcuts underneath controls";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(298, 141);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(216, 31);
            textBox4.TabIndex = 13;
            textBox4.Text = "Open project automaticly";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(16, 101);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(165, 31);
            textBox3.TabIndex = 13;
            textBox3.Text = "Project file:";
            // 
            // textBox7
            // 
            textBox7.Location = new Point(16, 327);
            textBox7.Name = "textBox7";
            textBox7.ReadOnly = true;
            textBox7.Size = new Size(182, 31);
            textBox7.TabIndex = 13;
            textBox7.Text = "Keyboard shortcut list";
            // 
            // CollapseControlPanel1
            // 
            CollapseControlPanel1.BackColor = Color.FromArgb(150, 10, 10, 10);
            CollapseControlPanel1.Dock = DockStyle.Fill;
            CollapseControlPanel1.FlatAppearance.BorderSize = 0;
            CollapseControlPanel1.FlatStyle = FlatStyle.Flat;
            CollapseControlPanel1.Font = new Font("Arial", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CollapseControlPanel1.ForeColor = Color.White;
            CollapseControlPanel1.Location = new Point(626, 3);
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
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(innerPanel2, 2, 0);
            tableLayoutPanel1.Controls.Add(CollapseControlPanel1, 1, 0);
            tableLayoutPanel1.Controls.Add(controlPanel, 0, 0);
            tableLayoutPanel1.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tableLayoutPanel1.Location = new Point(12, 15);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(2234, 1478);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // innerPanel2
            // 
            innerPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            innerPanel2.Appearance = TabAppearance.FlatButtons;
            innerPanel2.Controls.Add(tab1);
            innerPanel2.Controls.Add(tab2);
            innerPanel2.Controls.Add(tab3);
            innerPanel2.ItemSize = new Size(0, 50);
            innerPanel2.Location = new Point(653, 0);
            innerPanel2.Margin = new Padding(0);
            innerPanel2.Name = "innerPanel2";
            innerPanel2.SelectedIndex = 0;
            innerPanel2.Size = new Size(1581, 1478);
            innerPanel2.TabIndex = 4;
            innerPanel2.SizeChanged += FormModulePanel_SizeChanged;
            // 
            // tab1
            // 
            tab1.BackColor = Color.FromArgb(32, 32, 32);
            tab1.Controls.Add(label1);
            tab1.Location = new Point(4, 54);
            tab1.Name = "tab1";
            tab1.Size = new Size(1573, 1420);
            tab1.TabIndex = 0;
            tab1.Text = "Operation";
            // 
            // label1
            // 
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(1573, 1420);
            label1.TabIndex = 3;
            label1.Text = "Open a project or database on the left.";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tab2
            // 
            tab2.BackColor = Color.FromArgb(32, 32, 32);
            tab2.Location = new Point(4, 54);
            tab2.Name = "tab2";
            tab2.Size = new Size(1573, 1420);
            tab2.TabIndex = 1;
            tab2.Text = "DataBase";
            // 
            // tab3
            // 
            tab3.BackColor = Color.FromArgb(32, 32, 32);
            tab3.Location = new Point(4, 54);
            tab3.Name = "tab3";
            tab3.Size = new Size(1573, 1420);
            tab3.TabIndex = 2;
            tab3.Text = "Settings";
            // 
            // controlPanel
            // 
            controlPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            controlPanel.AutoScroll = true;
            controlPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            controlPanel.Controls.Add(tabControl1);
            controlPanel.Location = new Point(3, 3);
            controlPanel.MaximumSize = new Size(617, 1000000000);
            controlPanel.Name = "controlPanel";
            controlPanel.Size = new Size(617, 1472);
            controlPanel.TabIndex = 17;
            // 
            // OpSol_Form
            // 
            AllowDrop = true;
            AutoScaleMode = AutoScaleMode.Inherit;
            BackColor = Color.FromArgb(32, 32, 32);
            ClientSize = new Size(2258, 1505);
            ContentsPanel = innerPanel2;
            ControlPanel = controlPanel;
            Controls.Add(tableLayoutPanel1);
            ForeColor = Color.FromArgb(60, 60, 60);
            MinimumSize = new Size(1150, 500);
            Name = "OpSol_Form";
            ShowIcon = false;
            TabControl = tabControl1;
            Text = "Operators Solution";
            TopMost = true;
            TreeviewExplorer = treeViewModules;
            tabControl1.ResumeLayout(false);
            Operation.ResumeLayout(false);
            DataBase.ResumeLayout(false);
            Settings.ResumeLayout(false);
            Settings.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            innerPanel2.ResumeLayout(false);
            tab1.ResumeLayout(false);
            controlPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        private Button button2;
        private TreeView treeViewModules;
        private Button CollapseControlPanel1;
        private System.ComponentModel.IContainer components;
        private TableLayoutPanel tableLayoutPanel1;
        private TabControl tabControl1;
        private TabPage Operation;
        private TabPage DataBase;
        private TabPage Settings;
        private Panel controlPanel;
        private Button button1;
        private TextBox ProjectFile;
        private TextBox textBox3;
        private TextBox textBox2;
        private TextBox textBox1;
        private ComboBox GraphicsSoftwareOption;
        private TreeView treeView1;
        private ToolTip ToolTip;
        private TextBox textBox5;
        private TextBox textBox4;
        private TextBox textBox8;
        private TextBox textBox7;
        private TextBox textBox6;
        private TabControl innerPanel2;
        private TabPage tab1;
        private Label label1;
        private TabPage tab2;
        private TabPage tab3;
    }
}
