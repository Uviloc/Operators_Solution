using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Threading.Channels;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using System.Reflection;
using OperatorsSolution.Program;
using OperatorsSolution.GraphicsProgramFunctions;
using OperatorsSolution.Common;
using System.Data.Common;
using System.Data.SQLite;
using System.Data;
using Console = System.Diagnostics.Debug;
using System.Windows.Forms;
using Dapper;
using Emitter;

namespace OperatorsSolution
{
    public partial class OpSol_Form : CustomForm
    {
        public OpSol_Form()
        {
            InitializeComponent();
            InitializeSettings();
            InitializeCollapseControlPanelTimer();
            HideTabBar();

            if (OperationTreeview != null && DatabaseTreeview != null)
                PluginLoader.LoadPlugins(OperationTreeview, DatabaseTreeview);
        }


        #region >----------------- Collapse Control Panel: ---------------------
        private bool controlPanelOpen = true;
        private bool controlPanelChanging = false;
        private readonly float animationDuration = 0.3f;

        private System.Windows.Forms.Timer? controlPanelAnimationTimer;

        private void InitializeCollapseControlPanelTimer()
        {
            if (ControlPanel == null) return;

            controlPanelAnimationTimer = new System.Windows.Forms.Timer { Interval = 1 };
            controlPanelAnimationTimer.Tick += ControlPanelTimer_Tick;
        }

        private void CollapseControlPanel(object? sender, EventArgs e)
        {
            if (ControlPanel == null || controlPanelAnimationTimer == null) return;
            controlPanelAnimationTimer.Start();
            controlPanelChanging = true; // Prevent constant scaling of inner form
            if (sender is Button collapseButton)
            {
                collapseButton.Text = controlPanelOpen ? ">" : "<";
            }
        }

        private void ControlPanelTimer_Tick(object? sender, EventArgs e)
        {
            if (ControlPanel == null || controlPanelAnimationTimer == null) return;

            int animationSpeed = (int)Math.Round(15 / animationDuration);
            ControlPanel.Width += controlPanelOpen ? -animationSpeed : animationSpeed;

            // Stop when the panel reaches its target size
            if (controlPanelOpen && ControlPanel.Width <= ControlPanel.MinimumSize.Width ||
                !controlPanelOpen && ControlPanel.Width >= ControlPanel.MaximumSize.Width)
            {
                controlPanelOpen = !controlPanelOpen;
                controlPanelChanging = false;
                controlPanelAnimationTimer.Stop();
                ScaleFormToFitPanel(ContentsPanel);
            }
        }
        #endregion

        private void ButtonEnter(object? sender, EventArgs e)
        {
            if (sender is not Control control) return;
            if (control.Tag is not Color originalColor) control.Tag = originalColor = control.BackColor;
            control.BackColor = Color.FromArgb(originalColor.R + 30, originalColor.G + 30, originalColor.B + 30);
        }
        private void ButtonLeave(object? sender, EventArgs e)
        {
            if (sender is not Control control) return;
            if (control.Tag is not Color originalColor) control.Tag = originalColor = control.BackColor;
            control.BackColor = originalColor;
        }

        #region >----------------- OpenForm: ---------------------
        private void OpenExternalForm(object? sender, TreeNodeMouseClickEventArgs e)
        {
            if (ContentsPanel == null || TabControl == null) return;

            if (e.Node.Tag is Common.IFormPlugin pluginForm)
            {
                // Get the form from the IFormPlugin instance
                Form form = pluginForm.GetForm();

                // Store form in Operation tab to use for scaling and open when set.
                OpenFormInPanel(form, ContentsPanel.TabPages[TabControl.SelectedIndex]);
            }
        }

        private static Form OpenFormInPanel(Form form, object? container)
        {
            if (container is TabControl tabControl)
                container = tabControl.SelectedTab;

            if (container is not Panel panel)
                return form;

            // Set the form to be a child inside the panel
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // Clear the panel to ensure no other controls are blocking the new form
            panel.Controls.Clear();

            // Add the form to the panel's Controls collection
            panel.Controls.Add(form);

            panel.Tag = form;

            // Perform scaling
            ScaleFormToFitPanel(panel);

            // Show the form inside the panel
            form.Show();

            return form;
        }
        #endregion

        #region >----------------- Scale Form: ---------------------
        private static void ScaleFormToFitPanel(object? formModulePanel)
        {
            if (formModulePanel is TabControl tabControl) formModulePanel = tabControl.SelectedTab;
            if (formModulePanel is not Panel panel || panel.Tag is not Form form) return;
            // Check if the form's original size is already stored
            if (form.Tag is not Size originalSize)
            {
                // Store the original size of the form in its Tag property
                form.Tag = form.Size;
                originalSize = form.Size;

                // Store the original size and location of each control
                foreach (Control control in form.Controls)
                {
                    control.Tag = (control.Size, control.Location);
                }
            }

            // Reset all controls to their original sizes and locations
            foreach (Control control in form.Controls)
            {
                if (control.Tag is (Size controlOriginalSize, Point controlOriginalLocation))
                {
                    control.Size = controlOriginalSize;
                    control.Location = controlOriginalLocation;
                }
            }
            //Reset the form to its original size
            form.Size = originalSize;


            // Calculate scaling factors for width and height
            float scaleX = (float)panel.Width / originalSize.Width;
            float scaleY = (float)panel.Height / originalSize.Height;

            // Use non-cumulative scaling
            form.Scale(new SizeF(scaleX, scaleY));
        }

        private void FormModulePanel_SizeChanged(object? sender, EventArgs e)
        {
            if (controlPanelChanging) return;
            ScaleFormToFitPanel(sender);
        }
        #endregion

        #region >----------------- Open project file: ---------------------
        private void OpenProject(object? sender, EventArgs e)
        {
            string projectFilePath = Properties.Settings.Default.ProjectFile;

            if (string.IsNullOrWhiteSpace(projectFilePath))
            {
                MessageBox.Show("There was no selected project. (Project settings>Project file)");
                return;
            }

            //string fileName = projectFilePath.Split('\\').Last();

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = projectFilePath,
                UseShellExecute = true
            });
        }
        #endregion

        #region >----------------- Settings: ---------------------
        private void InitializeSettings()
        {
            // Graphics Software:
            GraphicsSoftwareOption.DataSource = Enum.GetValues(typeof(GraphicsSoftware));
            GraphicsSoftwareOption.SelectedItem = Properties.Settings.Default.GraphicsSoftware;

            // Project file:
            string projectFile = Properties.Settings.Default.ProjectFile;
            if (!string.IsNullOrWhiteSpace(projectFile))
            {
                string projectName = projectFile.Split('\\').Last();
                ProjectFile.Text = projectName;
                ToolTip.SetToolTip(ProjectFile, projectFile);
            }
        }

        private void SaveGraphicsSoftwareOption(object sender, EventArgs e)
        {
            // Set the 'graphicsSoftware' setting to the selected ComboBox item
            if (GraphicsSoftwareOption.SelectedItem is not null and GraphicsSoftware selectedSoftware)
            {
                Properties.Settings.Default.GraphicsSoftware = selectedSoftware;
                Properties.Settings.Default.Save();
            }
        }

        private void ProjectSelection(object sender, EventArgs e)
        {
            if (sender is not TextBox textBox) return;

            OpenFileDialog openFileDialog = new();

            switch (Properties.Settings.Default.GraphicsSoftware)
            {
                case GraphicsSoftware.XPression:
                    openFileDialog.Filter = "XPression files (*.xpf;*.xpp)|*.xpf;*.xpp";                // SHOULD NOT DEPEND ON THIS, HAVE THIS INFO IN ENUM?
                    break;
                case GraphicsSoftware.CasparCG:
                    //openFileDialog.Filter = "CasparCG files (*.;*.)";
                    break;
                case GraphicsSoftware.vMix:
                    //openFileDialog.Filter = "vMix files (*.;*.)";
                    break;
            }

            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = openFileDialog.FileName;
                try
                {
                    string projectName = file.Split('\\').Last();
                    textBox.Text = projectName;
                    ToolTip.SetToolTip(ProjectFile, file);
                    Properties.Settings.Default.ProjectFile = file;
                    Properties.Settings.Default.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed setting project file: " + ex);
                }
            }
        }

        private void RemoveProjectFileRef(object sender, EventArgs e)
        {
            ProjectFile.Text = null;
            ToolTip.SetToolTip(ProjectFile, "");
            Properties.Settings.Default.ProjectFile = string.Empty;
            Properties.Settings.Default.Save();
        }
        #endregion

        #region >----------------- Tab stuff: ---------------------
        private void TabChange(object? sender, EventArgs e)
        {
            if (sender is not TabControl tabControl) return;
            if (ContentsPanel == null) return;

            if (tabControl.SelectedIndex == 2) return; // If its the settings tab, do not execute auto switch

            ContentsPanel.SelectedIndex = tabControl.SelectedIndex;
        }

        private void HideTabBar()
        {
            if (ContentsPanel == null) return;
            ContentsPanel.Appearance = TabAppearance.FlatButtons;
            ContentsPanel.ItemSize = new Size(0, 1);
            ContentsPanel.SizeMode = TabSizeMode.Fixed;
        }
        #endregion

        #region >----------------- Database stuff: ---------------------
        private void OpenDatabase(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (ContentsPanel == null || TabControl == null || DataViewer == null) return;

            if (e.Node.Tag is string dbPath)
            {
                string connectionString = $"Data Source={dbPath};Version=3;";
                LoadData(connectionString, DataViewer);
            }
        }

        private SQLiteConnection? connection;

        // Loads data into the TabControl and initializes it
        private void LoadData(string connectionString, TabControl dataViewer)
        {
            try
            {
                InitializeConnection(connectionString);
                dataViewer.TabPages.Clear();
                dataViewer.ContextMenuStrip = null;

                if (connection == null) return;
                foreach (var table in GetTables(connection))
                {
                    AddTableToTabControl(dataViewer, table);
                    Console.WriteLine(table);
                }

                InitializeContextMenuToTabs(dataViewer);

                InitializeEvents(dataViewer);

                InitializeNewTabButton(dataViewer);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading data: " + ex.Message);
            }
        }

        private void InitializeEvents(TabControl tabControl)
        {
            // Detach existing event handlers before attaching new ones
            tabControl.MouseDoubleClick -= EditTabName;
            tabControl.KeyDown -= DataViewer_KeyDown;
            tabControl.Deselecting -= HandleTabDeselection;

            tabControl.Resize -= NewTabButton;


            // Attach necessary event handlers
            tabControl.MouseDoubleClick += EditTabName;
            tabControl.KeyDown += DataViewer_KeyDown;
            tabControl.Deselecting += HandleTabDeselection;

            tabControl.Resize += NewTabButton;
        }

        private void NewTabButton(object? sender, EventArgs e)
        {
            if (sender is TabControl tabControl)
                InitializeNewTabButton(tabControl);
        }

        // Event handler for MouseDoubleClick
        private void EditTabName(object? sender, MouseEventArgs e)
        {
            if (sender is TabControl tabControl)
                StartTabEditing(tabControl);
        }

        // Event handler for KeyDown
        private static void DataViewer_KeyDown(object? sender, KeyEventArgs e)
        {
            if (sender is not TabControl dataViewer) return;
            // Handles Ctrl+S to trigger saving
            if (e.Control && e.KeyCode == Keys.S)
            {
                SaveDataInAllTabs(dataViewer);
            }
        }

        // Initializes the SQLite connection
        private void InitializeConnection(string connectionString)
        {
            connection?.Close();
            //connection ??= new SQLiteConnection(connectionString);
            if (connection == null || connection.State != ConnectionState.Open)
            {
                connection = new SQLiteConnection(connectionString);
            }
            connection.Open();
        }

        // Adds a table as a new tab with a DataGridView
        private TabPage? AddTableToTabControl(TabControl dataViewer, string table)
        {
            if (connection == null) return null;
            DataGridView dataGridView = CreateDataGridView();

            // If table does not exist yet, create one:
            EnsureTableExistance(table, connection);

            DataTable dataTable = LoadTableData(table, connection);
            SQLiteDataAdapter dataAdapter = GetDataAdapter(table, connection);

            var commandBuilder = new SQLiteCommandBuilder(dataAdapter);

            TableData tableData = new() { DataTable = dataTable, DataAdapter = dataAdapter };

            dataGridView.DataSource = dataTable;
            dataGridView.CellValueChanged += (sender, e) => SaveTable(tableData);

            TabPage page = new()
            {
                Text = table,
                Tag = tableData,
            };
            page.Controls.Add(dataGridView);
            dataViewer.TabPages.Add(page);

            HideRowIDColumn(dataGridView, dataTable);

            return page;
        }

        private void InitializeContextMenuToTabs(TabControl tabControl)
        {
            if (connection == null) return;
            // Create the ContextMenuStrip
            var contextMenu = new ContextMenuStrip();
            var renameItem = new ToolStripMenuItem("Rename table");
            var closeItem = new ToolStripMenuItem("Delete table");
            var duplicateItem = new ToolStripMenuItem("Duplicate table");

            // Add items to the context menu
            contextMenu.Items.Add(renameItem);
            contextMenu.Items.Add(closeItem);
            contextMenu.Items.Add(duplicateItem);

            // Assign event handlers for menu items
            renameItem.Click += (s, e) => RenameTable(tabControl);
            closeItem.Click += (s, e) => DeleteTable(tabControl, connection);
            duplicateItem.Click += (s, e) => DuplicateTable(tabControl, connection);

            // Handle right-click on the tab buttons
            tabControl.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    // Determine the tab index under the mouse
                    for (int i = 0; i < tabControl.TabCount; i++)
                    {
                        Rectangle tabRect = tabControl.GetTabRect(i);
                        if (tabRect.Contains(e.Location))
                        {
                            // Set the selected tab and show the context menu
                            tabControl.SelectedIndex = i;
                            contextMenu.Show(tabControl, e.Location);
                            break;
                        }
                    }
                }
            };
        }


        // Example action handlers for context menu items
        private void RenameTable(TabControl tabControl)
        {
            // Call the renaming function for the selected tab
            StartTabEditing(tabControl);
        }

        private void DeleteTable(TabControl tabControl, SQLiteConnection connection)
        {
            if (tabControl.SelectedTab == null)
                return;

            string tableName = tabControl.SelectedTab.Text;

            if (!AskConfirmation(tableName)) return;

            try
            {
                // Remove the table from SQLite
                using SQLiteCommand command = new($"DROP TABLE IF EXISTS \"{tableName}\";", connection);
                command.ExecuteNonQuery();

                // Remove the corresponding tab from the TabControl
                tabControl.TabPages.Remove(tabControl.SelectedTab);
                InitializeNewTabButton(tabControl);
                Console.WriteLine($"Table '{tableName}' deleted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to delete table: {ex.Message}", "Error");
            }
        }

        private static void DuplicateTable(TabControl tabControl, SQLiteConnection connection)
        {
            if (tabControl.SelectedTab == null)
                return;

            string originalTableName = tabControl.SelectedTab.Text;
            string newTableName = $"{originalTableName}_Copy";

            try
            {
                // Duplicate the table in SQLite
                using SQLiteCommand command = new(
                    $"CREATE TABLE \"{newTableName}\" AS SELECT * FROM \"{originalTableName}\";", connection);
                command.ExecuteNonQuery();

                // Add a new tab for the duplicated table
                TabPage newTab = new(newTableName);
                tabControl.TabPages.Add(newTab);
                Console.WriteLine($"Table '{newTableName}' duplicated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to duplicate table: {ex.Message}", "Error");
            }
        }

        private static void EnsureTableExistance(string tableName, SQLiteConnection connection)
        {
            try
            {
                // Query to check if the table exists
                //string checkTableQuery = $"SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' AND name = \"{tableName}\"";
                string checkTableQuery = "SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' AND name = @TableName";

                int tableCount = connection.ExecuteScalar<int>(checkTableQuery, new { TableName = tableName });

                // If table does not exist, create it
                if (tableCount == 0)
                {
                    string command = $"CREATE TABLE \"{ tableName }\" (Column1 TEXT)";
                    connection.Execute(command, connection);
                    //Console.WriteLine($"Table '{tableName}' has been created.");
                }
                else
                {
                    //Console.WriteLine($"Table '{tableName}' already exists.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ensuring table exists: {ex.Message}");
            }
        }

        // Creates and returns a new DataGridView
        private static DataGridView CreateDataGridView()
        {
            return new DataGridView
            {
                Dock = DockStyle.Fill,
            };
        }

        // Loads data from a table into a DataTable
        private static DataTable LoadTableData(string table, SQLiteConnection connection)
        {
            string query = $"SELECT rowid AS RowID, * FROM \"{table}\"";
            SQLiteDataAdapter dataAdapter = new(query, connection);

            DataTable dataTable = new();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        // Returns a new SQLiteDataAdapter for a table
        private static SQLiteDataAdapter GetDataAdapter(string table, SQLiteConnection connection)
        {
            string query = $"SELECT rowid AS RowID, * FROM \"{table}\"";
            //string query = $"SELECT * FROM {table}";

            return new SQLiteDataAdapter(query, connection);
        }

        // Hides the RowID column in the DataGridView
        private static void HideRowIDColumn(DataGridView dataGridView, DataTable dataTable)
        {
            try
            {
                if (dataTable.Columns.Contains("RowID"))
                    dataGridView.Columns["RowID"].Visible = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error hiding RowID column: " + ex.Message);
            }
        }


        private Button? newTabButton;
        private void InitializeNewTabButton(TabControl dataViewer)
        {
            newTabButton?.Parent?.Controls.Remove(newTabButton);
            newTabButton?.Dispose();
            if (dataViewer.Parent is not Control parent) return;

            // Find the position where the new tab would appear (the last tab position)
            Point buttonLocation;
            if (dataViewer.TabCount > 0)
            {
                Rectangle lastTabRect = dataViewer.GetTabRect(dataViewer.TabCount - 1);
                buttonLocation = new Point(lastTabRect.X + lastTabRect.Width + dataViewer.Location.X, dataViewer.Location.Y);
            }
            else
            {
                // If no tabs are present, position the button at the start
                buttonLocation = new Point(dataViewer.Location.X, dataViewer.Location.Y);
            }

            // Create a new Button and set its properties
            newTabButton = new()
            {
                Text = "+", // You can customize the button text
                Location = buttonLocation,
                Size = new Size(dataViewer.ItemSize.Height, dataViewer.ItemSize.Height),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.LightGray,
                Font = new Font("Arial", 10, FontStyle.Bold),
            };

            // Handle the button click event to add a new tab when clicked
            newTabButton.Click += (sender, e) =>
            {
                dataViewer.SelectedTab = AddTableToTabControl(dataViewer, "New Table");
                StartTabEditing(dataViewer);
                parent.Controls.Remove(newTabButton);
                newTabButton.Dispose();
                InitializeNewTabButton(dataViewer);
            };

            // Add the button to the parent container (the container holding the TabControl)
            parent.Controls.Add(newTabButton);
            newTabButton.BringToFront();
        }

        // Starts editing the tab name by adding a TextBox
        private void StartTabEditing(TabControl tabControl)
        {
            if (tabControl.SelectedTab is not TabPage tabPage || tabControl.Parent is not Control parent)
                return;

            Rectangle tabRect = tabControl.GetTabRect(tabControl.SelectedIndex);
            TextBox textBox = CreateTextBoxForEditing(tabControl, tabRect, tabControl.SelectedTab.Text);

            // Adjust position to align with the tab
            textBox.Location = parent.PointToClient(tabControl.PointToScreen(tabRect.Location));

            parent.Controls.Add(textBox);
            textBox.BringToFront();

            textBox.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    CommitRenameTab(tabPage, textBox.Text);
                    parent.Controls.Remove(textBox);
                    textBox.Dispose();
                    InitializeNewTabButton(tabControl);
                }
            };

            textBox.LostFocus += (s, e) =>
            {
                CommitRenameTab(tabPage, textBox.Text);
                parent.Controls.Remove(textBox);
                textBox.Dispose();
                InitializeNewTabButton(tabControl);
            };

            textBox.Focus();
            textBox.SelectAll();
        }

        // Creates and configures the TextBox for editing tab names
        private static TextBox CreateTextBoxForEditing(TabControl tabControl, Rectangle tabRect, string initialName)
        {
            return new TextBox
            {
                Text = initialName,
                TextAlign = HorizontalAlignment.Center,
                Multiline = true,
                BorderStyle = BorderStyle.None,
                Font = tabControl.Font,
                Location = tabControl.PointToScreen(tabRect.Location),
                Size = new Size(tabRect.Width, tabRect.Height),
            };
        }

        // Finishes editing the tab name and updates the TabPage
        private void CommitRenameTab(TabPage tabPage, string newName)
        {
            if (string.IsNullOrWhiteSpace(newName) || newName == tabPage.Text)
                return;

            string oldName = tabPage.Text;

            try
            {
                using SQLiteCommand command = new($"ALTER TABLE \"{oldName}\" RENAME TO \"{newName}\"", connection);
                command.ExecuteNonQuery();

                tabPage.Text = newName.Trim();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to rename table: {ex.Message}", "Rename Failed");
            }
        }

        // Retrieves table names from the database
        public static IEnumerable<string> GetTables(SQLiteConnection connection)
        {
            try
            {
                const string query = "SELECT name FROM sqlite_master WHERE type='table' AND name NOT LIKE 'sqlite_%';";
                return connection.Query<string>(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving table names: " + ex.Message);
                return [];
            }
        }

        public class TableData
        {
            public DataTable? DataTable { get; set; }
            public SQLiteDataAdapter? DataAdapter { get; set; }
        }

        // Saves data from all DataGrids in the TabControl
        private static void SaveDataInAllTabs(TabControl dataViewer)
        {
            foreach (TabPage tab in dataViewer.TabPages)
            {
                if (tab.Tag is not TableData tableData || tableData.DataTable?.GetChanges() == null)
                    continue;
                SaveTable(tableData);
            }
        }

        // Save data from a DataTable to the database
        private static bool SaveTable(TableData tableData)
        {
            if (tableData.DataTable == null || tableData.DataAdapter == null)
                return false;
            try
            {
                // If the DataAdapter was properly initialized with the DataTable, we should just update it.
                tableData.DataAdapter.Update(tableData.DataTable);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving data: " + ex.Message);
                return false;
            }
        }

        // Handles tab deselection and prompts for saving unsaved changes
        private static void HandleTabDeselection(object? sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage?.Tag is not TableData tableData)
                return;
            
            if (tableData.DataTable?.GetChanges() == null)
                return;

            DialogResult result = MessageBox.Show("Do you want to save changes to this table?", "Save Data", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                bool succeeded = SaveTable(tableData);
                if (!succeeded)
                    e.Cancel = true;
            }
        }

        private static void AskConfirmation(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (sender is not DataGridView grid) return;

                var selectedRows = grid.SelectedRows;
                int rowCount = selectedRows.Count;

                // Check if any rows are selected
                if (rowCount > 0 && AskConfirmation(rowCount + " row(s)"))
                {
                    foreach (DataGridViewRow row in selectedRows)
                    {
                        if (!row.IsNewRow) grid.Rows.Remove(row);
                    }
                }
                e.Handled = true; // Prevent default delete handling
            }
        }

        private static bool AskConfirmation(string deletionObject)
        {
            DialogResult dialogResult = MessageBox.Show(
                $"Are you sure you want to delete {deletionObject}?",
                "Confirm choice!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            return dialogResult == DialogResult.Yes;
        }
        #endregion
    }
}