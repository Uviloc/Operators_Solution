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
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

        private SQLiteConnection connection;

        // Loads data into the TabControl and initializes it
        private void LoadData(string connectionString, TabControl dataViewer)
        {
            try
            {
                InitializeConnection(connectionString);
                dataViewer.TabPages.Clear();

                foreach (var table in GetTables(connection))
                {
                    AddTableToTabControl(dataViewer, table);
                }

                //AddNewTabPage(dataViewer);
                AddNewTabButton(dataViewer);
                dataViewer.KeyDown += (sender, e) => HandleKeyDown(e, dataViewer); // Handle Ctrl+S for saving
                dataViewer.Deselecting += (sender, e) => HandleTabDeselection(e); // Handle tab unloading
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        // Initializes the SQLite connection
        private void InitializeConnection(string connectionString)
        {
            connection?.Close();
            if (connection == null || connection.State != ConnectionState.Open)
            {
                connection = new SQLiteConnection(connectionString);
            }
            connection.Open();
        }

        // Adds a table as a new tab with a DataGridView
        private void AddTableToTabControl(TabControl dataViewer, string table)
        {
            DataGridView dataGridView = CreateDataGridView();
            DataTable dataTable = LoadTableData(table, connection);

            dataGridView.DataSource = dataTable;
            dataGridView.CellValueChanged += (sender, e) => SaveTable(GetDataAdapter(table, connection), dataTable);

            TabPage page = new()
            {
                Text = table,
                Tag = GetDataAdapter(table, connection),
            };
            page.Controls.Add(dataGridView);
            dataViewer.TabPages.Add(page);

            page.MouseDoubleClick += (sender, e) => StartTabEditing(dataViewer, page, page.Bounds);

            HideRowIDColumn(dataGridView, dataTable);
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
            string query = $"SELECT rowid AS RowID, * FROM {table}";
            SQLiteDataAdapter dataAdapter = new(query, connection);

            DataTable dataTable = new();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        // Returns a new SQLiteDataAdapter for a table
        private static SQLiteDataAdapter GetDataAdapter(string table, SQLiteConnection connection)
        {
            string query = $"SELECT rowid AS RowID, * FROM {table}";
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
                MessageBox.Show("Error hiding RowID column: " + ex.Message);
            }
        }

        //// Adds a "new tab" button at the end of the TabControl
        //private static void AddNewTabPage(TabControl dataViewer)
        //{
        //    //TextBox textBox = CreateTextBoxForEditing(tabControl, tabRect);
        //    if (dataViewer.TabCount > 0)
        //        TabPage? lastTab = dataViewer.TabPages[dataViewer.TabCount-1];
        //    Rectangle textRect;
        //    if (lastTab == null)
        //    {
        //        textRect = new(dataViewer.Location, new(200, dataViewer.ItemSize.Height));
        //    }
        //    else
        //    {
        //        textRect = new(new Point(lastTab.Location.X + lastTab.Width, lastTab.Location.Y), new(200, lastTab.Height));
        //    }

        //    TextBox textBox = new()
        //    {
        //        Text = "Table name",
        //        TextAlign = HorizontalAlignment.Center,
        //        Multiline = true,
        //        BorderStyle = BorderStyle.None,
        //        //Font = tabControl.Font,
        //        //Location = dataViewer.TabPagesp[dataViewer.TabCount]..PointToScreen(tabRect.Location),
        //        Bounds = textRect,
        //    };

        //    Control? parent = dataViewer.Parent;
        //    parent?.Controls.Add(textBox);
        //    textBox.BringToFront();
        //    textBox.Focus();
        //    TabPage newPage = new()
        //    {
        //        Text = "         +         ",
        //        Tag = "pageAdder",
        //    };
        //    dataViewer.Selecting += AddPageHandler;
        //    dataViewer.TabPages.Add(newPage);
        //}

        private static void AddNewTabButton(TabControl dataViewer)
        {
            // Find the position where the new tab would appear (the last tab position)
            Point buttonLocation;
            if (dataViewer.TabCount > 0)
            {
                // Get the location of the last tab
                TabPage lastTab = dataViewer.TabPages[dataViewer.TabCount - 1];
                buttonLocation = new Point(lastTab.Location.X + lastTab.Width, lastTab.Location.Y);
            }
            else
            {
                // If no tabs are present, position the button at the start
                buttonLocation = new Point(dataViewer.Location.X, dataViewer.Location.Y);
            }

            // Create a new Button and set its properties
            Button addButton = new Button
            {
                Text = "+", // You can customize the button text
                Location = buttonLocation,
                Size = new Size(200, dataViewer.ItemSize.Height), // Adjust button size to match the expected tab size
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.LightGray, // Optional: Style the button to match tab appearance
                Font = new Font("Arial", 10, FontStyle.Bold), // Optional: Style the button's font
            };

            // Handle the button click event to add a new tab when clicked
            addButton.Click += (sender, e) =>
            {
                // Add your logic here to handle adding a new tab
                AddTab(dataViewer); // For example, you can call another method to add a tab
            };

            // Add the button to the parent container (the container holding the TabControl)
            Control? parent = dataViewer.Parent;
            parent?.Controls.Add(addButton); // Add the button in the same container as the TabControl
            addButton.BringToFront(); // Ensure it appears above other controls
        }

        // Method to add a new tab (you can customize this logic)
        private static void AddTab(TabControl dataViewer)
        {
            // This is where you handle creating the new tab
            TabPage newTab = new TabPage("New Table")
            {
                Tag = "newTable" // You can customize this further
            };
            dataViewer.TabPages.Add(newTab);
        }

        // Handles adding a new page when the "+" tab is clicked
        private static void AddPageHandler(object? sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage?.Tag?.ToString() == "pageAdder" && sender is TabControl dataViewer)
                StartTabEditing(dataViewer, e.TabPage, dataViewer.GetTabRect(e.TabPageIndex));
        }

        // Starts editing the tab name by adding a TextBox
        private static void StartTabEditing(TabControl tabControl, TabPage tabPage, Rectangle tabRect)
        {
            TextBox textBox = CreateTextBoxForEditing(tabControl, tabRect);

            Control? parent = tabControl.Parent;
            parent?.Controls.Add(textBox);
            textBox.BringToFront();
            textBox.Focus();

            // Adjust position to align with the tab
            if (parent != null)
                textBox.Location = parent.PointToClient(tabControl.PointToScreen(tabRect.Location));

            textBox.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    FinishTabEditing(tabControl, tabPage, textBox);
                }
            };

            // Ensure text is selected after rendering
            Task.Delay(50).ContinueWith(_ => textBox.Invoke(() => { textBox.Focus(); textBox.SelectAll(); }));
        }

        // Creates and configures the TextBox for editing tab names
        private static TextBox CreateTextBoxForEditing(TabControl tabControl, Rectangle tabRect)
        {
            return new TextBox
            {
                Text = "Table name",
                TextAlign = HorizontalAlignment.Center,
                Multiline = true,
                BorderStyle = BorderStyle.None,
                Font = tabControl.Font,
                Location = tabControl.PointToScreen(tabRect.Location),
                Size = new Size(tabRect.Width, tabRect.Height)
            };
        }

        // Finishes editing the tab name and updates the TabPage
        private static void FinishTabEditing(TabControl tabControl, TabPage tabPage, TextBox textBox)
        {
            if (!string.IsNullOrWhiteSpace(textBox.Text))
            {
                tabPage.Text = textBox.Text.Trim();
            }

            Control? parent = tabControl.Parent;
            parent?.Controls.Remove(textBox);
            textBox.Dispose();
        }

        // Retrieves table names from the database
        public static IEnumerable<string> GetTables(SQLiteConnection connection)
        {
            const string query = "SELECT name FROM sqlite_master WHERE type='table' AND name NOT LIKE 'sqlite_%';";
            return connection.Query<string>(query);
        }

        // Handles Ctrl+S to trigger saving
        private void HandleKeyDown(KeyEventArgs e, TabControl dataViewer)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                SaveDataInAllTabs(dataViewer);
            }
        }

        // Saves data from all DataGrids in the TabControl
        private void SaveDataInAllTabs(TabControl dataViewer)
        {
            MessageBox.Show("All table saving not implemented yet");
            //foreach (TabPage tab in dataViewer.TabPages)
            //{
            //    var tag = tab.Tag as dynamic;
            //    if (tag != null)
            //    {
            //        if (tag.DataTable is DataTable dataTable && dataTable.GetChanges() != null)
            //            SaveTable(GetDataAdapter(tab.Text, connection), dataTable);
            //    }
            //}
        }

        // Save data from a DataTable to the database
        private static void SaveTable(SQLiteDataAdapter dataAdapter, DataTable? dataTable)
        {
            MessageBox.Show("Single table saving not implemented yet");
            //try
            //{
            //    if (dataTable == null) return;

            //    // If the DataAdapter was properly initialized with the DataTable, we should just update it.
            //    dataAdapter.Update(dataTable);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error saving data: " + ex.Message);
            //}
        }

        // Handles tab deselection and prompts for saving unsaved changes
        private void HandleTabDeselection(TabControlCancelEventArgs e)
        {
            MessageBox.Show("Deselect saving not implemented yet");
            //var tag = e.TabPage?.Tag;
            //if (tag == null)
            //    return;

            //DataTable dataTable = tag.DataTable as DataTable;
            //SQLiteDataAdapter dataAdapter = tag.DataAdapter as SQLiteDataAdapter;

            //if (dataTable == null || dataAdapter == null || dataTable.GetChanges() == null)
            //    return;

            //DialogResult result = MessageBox.Show("Do you want to save changes to this table?", "Save Data", MessageBoxButtons.YesNo);
            //if (result == DialogResult.Yes)
            //{
            //    SaveTable(dataAdapter, dataTable);
            //}
        }

        private static void AskConfirmation(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (sender is not DataGridView grid) return;

                var selectedRows = grid.SelectedRows;
                int rowCount = selectedRows.Count;

                // Check if any rows are selected
                if (rowCount > 0 && AskConfirmation(rowCount))
                {
                    foreach (DataGridViewRow row in selectedRows)
                    {
                        if (!row.IsNewRow) grid.Rows.Remove(row);
                    }
                }
                e.Handled = true; // Prevent default delete handling
            }
        }

        private static bool AskConfirmation(int rowCount)
        {
            DialogResult dialogResult = MessageBox.Show(
                $"Are you sure you want to delete {rowCount} row(s)?",
                "Confirm choice!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            return dialogResult == DialogResult.Yes;
        }

        //private static Array GetAllTables(SQLiteConnection connection)
        //{
        //    List<>

        //    string query = "SELECT name FROM sqlite_master" +
        //        "WHERE type='table'" +
        //        "AND name NOT LIKE 'sqlite_%';";

        //    try
        //    {

        //        DataTable table = GetDataTable(query);

        //        // Return all table names in the ArrayList

        //        foreach (DataRow row in table.Rows)
        //        {
        //            list.Add(row.ItemArray[0].ToString());
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //    return list;
        //}

        //public static DataTable GetDataTable(string sql)
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        using (var c = new SQLiteConnection(dbConnection))
        //        {
        //            c.Open();
        //            using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
        //            {
        //                using (SQLiteDataReader rdr = cmd.ExecuteReader())
        //                {
        //                    dt.Load(rdr);
        //                    return dt;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        return null;
        //    }
        //}

        //private void SaveButton_Click(object? sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (dataAdapter == null || dataTable == null) return;

        //        // Update the database with changes made in the DataGridView
        //        dataAdapter.Update(dataTable);
        //        MessageBox.Show("Changes saved successfully!");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error saving data: " + ex.Message);
        //    }
        //}
        #endregion

        //private void Increment(object sender, EventArgs e)
        //{
        //    if (dataGridView4 == null || dataTable == null || connection == null || connection.State != ConnectionState.Open)
        //    {
        //        MessageBox.Show("Please load a database first.");
        //        return;
        //    }

        //    try
        //    {
        //        // Ensure at least one row is selected
        //        if (dataGridView4.SelectedRows.Count == 0)
        //        {
        //            MessageBox.Show("Please select one or more rows to increment.");
        //            return;
        //        }

        //        foreach (DataGridViewRow selectedRow in dataGridView4.SelectedRows)
        //        {
        //            // Get the primary key or unique identifier for the row
        //            if (selectedRow.Cells["Id"] != null && selectedRow.Cells["Age"] != null &&
        //                int.TryParse(selectedRow.Cells["Id"].Value?.ToString(), out int id) &&
        //                int.TryParse(selectedRow.Cells["Age"].Value?.ToString(), out int age))
        //            {
        //                // Increment the age value in the database
        //                string query = "UPDATE People SET Age = @NewAge WHERE Id = @Id";
        //                using (var command = new SQLiteCommand(query, connection))
        //                {
        //                    command.Parameters.AddWithValue("@NewAge", age + 1);
        //                    command.Parameters.AddWithValue("@Id", id);
        //                    command.ExecuteNonQuery();
        //                }
        //            }
        //        }

        //        // Refresh the DataGridView by reloading data from the database
        //        LoadData(connection.ConnectionString, dataGridView4);

        //        MessageBox.Show("Selected Ages incremented by 1 and saved to the database!");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error incrementing ages: " + ex.Message);
        //    }
        //}
    }
}