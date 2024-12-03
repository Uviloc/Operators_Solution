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
            operationTreeview = new TreeView();
            databaseTreeview = new TreeView();
            controlPanelTabs = new TabControl();
            Operation = new TabPage();
            DataBase = new TabPage();
            Settings = new TabPage();
            settingsGraphicsProgram = new TextBox();
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
            mainLayoutTable = new TableLayoutPanel();
            innerPanelTabs = new TabControl();
            innerPanelOperationTab = new TabPage();
            defaultInstructionsOperation = new Label();
            innerPanelDatabaseTab = new TabPage();
            button4 = new Button();
            dataViewer = new TabControl();
            tabPage1 = new TabPage();
            dataGridView4 = new DataGridView();
            tabPage2 = new TabPage();
            button3 = new Button();
            defaultDatabaseInstructions = new Label();
            settingsTab = new TabPage();
            controlPanel = new Panel();
            ToolTip = new ToolTip(components);
            controlPanelTabs.SuspendLayout();
            Operation.SuspendLayout();
            DataBase.SuspendLayout();
            Settings.SuspendLayout();
            mainLayoutTable.SuspendLayout();
            innerPanelTabs.SuspendLayout();
            innerPanelOperationTab.SuspendLayout();
            innerPanelDatabaseTab.SuspendLayout();
            dataViewer.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView4).BeginInit();
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
            // operationTreeview
            // 
            operationTreeview.BackColor = Color.Black;
            operationTreeview.Dock = DockStyle.Fill;
            operationTreeview.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            operationTreeview.ForeColor = Color.White;
            operationTreeview.LineColor = Color.White;
            operationTreeview.Location = new Point(0, 0);
            operationTreeview.Name = "operationTreeview";
            operationTreeview.Size = new Size(609, 1264);
            operationTreeview.TabIndex = 6;
            operationTreeview.NodeMouseDoubleClick += OpenExternalForm;
            // 
            // databaseTreeview
            // 
            databaseTreeview.BackColor = Color.Black;
            databaseTreeview.Font = new Font("Segoe UI", 14F);
            databaseTreeview.ForeColor = Color.White;
            databaseTreeview.Location = new Point(9, 96);
            databaseTreeview.MinimumSize = new Size(0, 10);
            databaseTreeview.Name = "databaseTreeview";
            databaseTreeview.Size = new Size(597, 1062);
            databaseTreeview.TabIndex = 0;
            databaseTreeview.NodeMouseDoubleClick += OpenDatabase;
            // 
            // controlPanelTabs
            // 
            controlPanelTabs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            controlPanelTabs.Appearance = TabAppearance.Buttons;
            controlPanelTabs.Controls.Add(Operation);
            controlPanelTabs.Controls.Add(DataBase);
            controlPanelTabs.Controls.Add(Settings);
            controlPanelTabs.ItemSize = new Size(20000, 200);
            controlPanelTabs.Location = new Point(0, 0);
            controlPanelTabs.Margin = new Padding(0);
            controlPanelTabs.Name = "controlPanelTabs";
            controlPanelTabs.Padding = new Point(20, 3);
            controlPanelTabs.SelectedIndex = 0;
            controlPanelTabs.Size = new Size(617, 1472);
            controlPanelTabs.SizeMode = TabSizeMode.FillToRight;
            controlPanelTabs.TabIndex = 0;
            controlPanelTabs.SelectedIndexChanged += TabChange;
            // 
            // Operation
            // 
            Operation.Controls.Add(operationTreeview);
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
            DataBase.Controls.Add(databaseTreeview);
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
            Settings.Controls.Add(settingsGraphicsProgram);
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
            // settingsGraphicsProgram
            // 
            settingsGraphicsProgram.Location = new Point(16, 25);
            settingsGraphicsProgram.Multiline = true;
            settingsGraphicsProgram.Name = "settingsGraphicsProgram";
            settingsGraphicsProgram.ReadOnly = true;
            settingsGraphicsProgram.Size = new Size(165, 33);
            settingsGraphicsProgram.TabIndex = 11;
            settingsGraphicsProgram.Text = "Graphics Program:";
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
            // mainLayoutTable
            // 
            mainLayoutTable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainLayoutTable.ColumnCount = 3;
            mainLayoutTable.ColumnStyles.Add(new ColumnStyle());
            mainLayoutTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            mainLayoutTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayoutTable.Controls.Add(innerPanelTabs, 2, 0);
            mainLayoutTable.Controls.Add(CollapseControlPanel1, 1, 0);
            mainLayoutTable.Controls.Add(controlPanel, 0, 0);
            mainLayoutTable.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            mainLayoutTable.Location = new Point(12, 15);
            mainLayoutTable.Name = "mainLayoutTable";
            mainLayoutTable.RowCount = 1;
            mainLayoutTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayoutTable.Size = new Size(2234, 1478);
            mainLayoutTable.TabIndex = 0;
            // 
            // innerPanelTabs
            // 
            innerPanelTabs.Appearance = TabAppearance.FlatButtons;
            innerPanelTabs.Controls.Add(innerPanelOperationTab);
            innerPanelTabs.Controls.Add(innerPanelDatabaseTab);
            innerPanelTabs.Controls.Add(settingsTab);
            innerPanelTabs.Dock = DockStyle.Fill;
            innerPanelTabs.ItemSize = new Size(0, 30);
            innerPanelTabs.Location = new Point(653, 0);
            innerPanelTabs.Margin = new Padding(0);
            innerPanelTabs.Name = "innerPanelTabs";
            innerPanelTabs.SelectedIndex = 0;
            innerPanelTabs.Size = new Size(1581, 1478);
            innerPanelTabs.TabIndex = 4;
            innerPanelTabs.SizeChanged += FormModulePanel_SizeChanged;
            // 
            // innerPanelOperationTab
            // 
            innerPanelOperationTab.BackColor = Color.FromArgb(32, 32, 32);
            innerPanelOperationTab.Controls.Add(defaultInstructionsOperation);
            innerPanelOperationTab.Location = new Point(4, 34);
            innerPanelOperationTab.Name = "innerPanelOperationTab";
            innerPanelOperationTab.Size = new Size(1573, 1440);
            innerPanelOperationTab.TabIndex = 0;
            innerPanelOperationTab.Text = "Operation";
            // 
            // defaultInstructionsOperation
            // 
            defaultInstructionsOperation.Dock = DockStyle.Fill;
            defaultInstructionsOperation.Location = new Point(0, 0);
            defaultInstructionsOperation.Name = "defaultInstructionsOperation";
            defaultInstructionsOperation.Size = new Size(1573, 1440);
            defaultInstructionsOperation.TabIndex = 3;
            defaultInstructionsOperation.Text = "Open a project on the left.";
            defaultInstructionsOperation.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // innerPanelDatabaseTab
            // 
            innerPanelDatabaseTab.BackColor = Color.FromArgb(32, 32, 32);
            innerPanelDatabaseTab.Controls.Add(button4);
            innerPanelDatabaseTab.Controls.Add(dataViewer);
            innerPanelDatabaseTab.Controls.Add(button3);
            innerPanelDatabaseTab.Controls.Add(defaultDatabaseInstructions);
            innerPanelDatabaseTab.Location = new Point(4, 34);
            innerPanelDatabaseTab.Name = "innerPanelDatabaseTab";
            innerPanelDatabaseTab.Size = new Size(1573, 1440);
            innerPanelDatabaseTab.TabIndex = 1;
            innerPanelDatabaseTab.Text = "DataBase";
            // 
            // button4
            // 
            button4.Location = new Point(1064, 1356);
            button4.Name = "button4";
            button4.Size = new Size(186, 45);
            button4.TabIndex = 7;
            button4.Text = "Increment score";
            button4.UseVisualStyleBackColor = true;
            // 
            // dataViewer
            // 
            dataViewer.Appearance = TabAppearance.FlatButtons;
            dataViewer.Controls.Add(tabPage1);
            dataViewer.Controls.Add(tabPage2);
            dataViewer.Location = new Point(87, 165);
            dataViewer.Name = "dataViewer";
            dataViewer.SelectedIndex = 0;
            dataViewer.Size = new Size(1432, 1128);
            dataViewer.TabIndex = 6;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(dataGridView4);
            tabPage1.Location = new Point(4, 37);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1424, 1087);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView4
            // 
            dataGridView4.AllowUserToOrderColumns = true;
            dataGridView4.AllowUserToResizeRows = false;
            dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView4.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView4.Dock = DockStyle.Fill;
            dataGridView4.Location = new Point(3, 3);
            dataGridView4.Name = "dataGridView4";
            dataGridView4.RowHeadersWidth = 62;
            dataGridView4.Size = new Size(1418, 1081);
            dataGridView4.TabIndex = 2;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 37);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1424, 1087);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(1381, 1383);
            button3.Name = "button3";
            button3.Size = new Size(112, 34);
            button3.TabIndex = 5;
            button3.Text = "Save";
            button3.UseVisualStyleBackColor = true;
            // 
            // defaultDatabaseInstructions
            // 
            defaultDatabaseInstructions.Location = new Point(0, 0);
            defaultDatabaseInstructions.Name = "defaultDatabaseInstructions";
            defaultDatabaseInstructions.Size = new Size(175, 71);
            defaultDatabaseInstructions.TabIndex = 4;
            defaultDatabaseInstructions.Text = "Open a database on the left.";
            defaultDatabaseInstructions.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // settingsTab
            // 
            settingsTab.BackColor = Color.FromArgb(32, 32, 32);
            settingsTab.Location = new Point(4, 34);
            settingsTab.Name = "settingsTab";
            settingsTab.Size = new Size(1573, 1440);
            settingsTab.TabIndex = 2;
            settingsTab.Text = "Settings";
            // 
            // controlPanel
            // 
            controlPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            controlPanel.AutoScroll = true;
            controlPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            controlPanel.Controls.Add(controlPanelTabs);
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
            ContentsPanel = innerPanelTabs;
            ControlPanel = controlPanel;
            Controls.Add(mainLayoutTable);
            DatabaseTreeview = databaseTreeview;
            DataViewer = dataViewer;
            ForeColor = Color.FromArgb(60, 60, 60);
            MinimumSize = new Size(1150, 500);
            Name = "OpSol_Form";
            OperationTreeview = operationTreeview;
            ShowIcon = false;
            TabControl = controlPanelTabs;
            Text = "Operators Solution";
            TopMost = true;
            controlPanelTabs.ResumeLayout(false);
            Operation.ResumeLayout(false);
            DataBase.ResumeLayout(false);
            Settings.ResumeLayout(false);
            Settings.PerformLayout();
            mainLayoutTable.ResumeLayout(false);
            innerPanelTabs.ResumeLayout(false);
            innerPanelOperationTab.ResumeLayout(false);
            innerPanelDatabaseTab.ResumeLayout(false);
            dataViewer.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView4).EndInit();
            controlPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        private Button button2;
        private TreeView operationTreeview;
        private Button CollapseControlPanel1;
        private System.ComponentModel.IContainer components;
        private TableLayoutPanel mainLayoutTable;
        private TabControl controlPanelTabs;
        private TabPage Operation;
        private TabPage DataBase;
        private TabPage Settings;
        private Panel controlPanel;
        private Button button1;
        private TextBox ProjectFile;
        private TextBox textBox3;
        private TextBox textBox2;
        private TextBox settingsGraphicsProgram;
        private ComboBox GraphicsSoftwareOption;
        private TreeView databaseTreeview;
        private ToolTip ToolTip;
        private TextBox textBox5;
        private TextBox textBox4;
        private TextBox textBox8;
        private TextBox textBox7;
        private TextBox textBox6;
        private TabControl innerPanelTabs;
        private TabPage innerPanelOperationTab;
        private Label defaultInstructionsOperation;
        private TabPage innerPanelDatabaseTab;
        private TabPage settingsTab;
        private Label defaultDatabaseInstructions;
        private TabControl dataViewer;
        private TabPage tabPage1;
        private DataGridView dataGridView4;
        private TabPage tabPage2;
        private Button button3;
        private Button button4;
    }
}
