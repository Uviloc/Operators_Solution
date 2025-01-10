
//public void UpdateButtons()
//{
//    // Remove existing sections and their TypeDescriptor providers
//    foreach (var oldSection in _buttons)
//    {
//        TypeDescriptor.RemoveProvider(TypeDescriptor.GetProvider(oldSection), oldSection);
//    }

//    _buttons.Clear();

//    // Create new Section instances and add the TypeDescriptor provider
//    for (int i = 0; i < _buttons.Count; i++)
//    {
//        Section section = new()
//        {
//            ButtonType = (i % 2 == 0) ? ButtonType.ToggleButton : ButtonType.ScriptButton,
//        };

//        // Create the appropriate button and assign it to the Section
//        Control button = section.ButtonType switch
//        {
//            ButtonType.ToggleButton => new Toggle_Button(),
//            ButtonType.ScriptButton => new Script_Button(),
//            _ => throw new NotSupportedException($"Unsupported button type: {section.ButtonType}")
//        };

//        // Set the button as a property of the section
//        section.Button = button as OperatorButton;

//        // Add the new section to the list
//        _buttons.Add(section);

//        // Add the button to the Controls collection of Logic_Button
//        Controls.Add(button);

//        TypeDescriptor.AddProvider(new Common.SectionTypeDescriptionProvider(), section);
//        TypeDescriptor.Refresh(section);
//    }

//    // Ensure buttons reflect changes in the UI
//    PerformLayout();
//}





//public class SectionCollection : Collection<Section>
//{
//    protected override void InsertItem(int index, Section item)
//    {
//        TypeDescriptor.AddProvider(new Common.SectionTypeDescriptionProvider(), item);
//        TypeDescriptor.Refresh(item);

//        base.InsertItem(index, item);
//    }
//}

//public class SectionCollection(Logic_Button logicButton) : Collection<Section>
//{
//    protected override void InsertItem(int index, Section item)
//    {
//        base.InsertItem(index, item);

//        // Ensure TypeDescriptor provider is added and refreshed
//        TypeDescriptor.AddProvider(new Common.SectionTypeDescriptionProvider(), item);
//        TypeDescriptor.Refresh(item);

//        // Call UpdateButtons to ensure the UI is updated when a new button is added
//        logicButton.UpdateButtons();
//    }

//    protected override void SetItem(int index, Section item)
//    {
//        base.SetItem(index, item);

//        // Ensure TypeDescriptor provider is updated
//        TypeDescriptor.AddProvider(new Common.SectionTypeDescriptionProvider(), item);
//        TypeDescriptor.Refresh(item);

//        // Call UpdateButtons when a button is replaced
//        logicButton.UpdateButtons();
//    }

//    protected override void RemoveItem(int index)
//    {
//        var item = this[index];
//        base.RemoveItem(index);

//        // Remove the TypeDescriptor provider when the item is removed
//        TypeDescriptor.RemoveProvider(TypeDescriptor.GetProvider(item), item);

//        // Call UpdateButtons to ensure the UI is updated when a button is removed
//        logicButton.UpdateButtons();
//    }

//    protected override void ClearItems()
//    {
//        base.ClearItems();

//        // Remove all TypeDescriptor providers when clearing the collection
//        foreach (var item in this)
//        {
//            TypeDescriptor.RemoveProvider(TypeDescriptor.GetProvider(item), item);
//        }

//        // Call UpdateButtons when all items are cleared
//        logicButton.UpdateButtons();
//    }
//}


//public class ObservableSectionList(Action action, Control parent) : Collection<Section>
//{
//    //public event Action? ItemsChanged;

//    protected override void InsertItem(int index, Section item)
//    {
//        //Debug.WriteLine("INSERTING ITEM");
//        //if (ItemsChanged == null)
//        //{
//        //    Debug.WriteLine("NO ACTION SET");
//        //}
//        //TypeDescriptor.AddProvider(new Common.SectionTypeDescriptionProvider(), item);
//        //TypeDescriptor.Refresh(item);
//        base.InsertItem(index, item);
//        //ItemsChanged?.Invoke();
//        UpdateButtons();
//    }

//    protected override void SetItem(int index, Section item)
//    {
//        //Debug.WriteLine("SETTING ITEM");
//        //TypeDescriptor.AddProvider(new Common.SectionTypeDescriptionProvider(), item);
//        //TypeDescriptor.Refresh(item);
//        base.SetItem(index, item);
//        //ItemsChanged?.Invoke();
//        UpdateButtons();
//    }

//    protected override void ClearItems()
//    {
//        //Debug.WriteLine("CLEARING ITEMS");
//        base.ClearItems();
//        //ItemsChanged?.Invoke();
//        UpdateButtons();
//    }



//    public void UpdateButtons()
//    {
//        //Debug.WriteLine("Updating Buttons");

//        foreach (Section section in Items)
//        {
//            TypeDescriptor.RemoveProvider(TypeDescriptor.GetProvider(section), section);

//            Control button = section.ButtonType switch
//            {
//                ButtonType.ToggleButton => new Toggle_Button(),
//                ButtonType.ScriptButton => new Script_Button(),
//                _ => throw new NotSupportedException($"Unsupported button type: {section.ButtonType}")
//            };

//            section.Button = button as OperatorButton;

//            Debug.WriteLine(button.GetType());

//            //Controls.Add(button);
//            //parent.Controls.Add(button);


//            TypeDescriptor.AddProvider(new Common.SectionTypeDescriptionProvider(), section);
//            TypeDescriptor.Refresh(section);
//        }

//        //PerformLayout();
//        //action.Invoke();
//    }
//}





//public Section()
//{
//    UpdateButton(); // Ensure the Button is initialized
//}

//private void UpdateButton()
//{
//    // Dispose of the previous button, if any
//    Button?.Dispose();

//    // Create a new button based on the ButtonType
//    Button = _buttonType switch
//    {
//        ButtonType.ToggleButton => new Toggle_Button(),
//        ButtonType.ScriptButton => new Script_Button(),
//        _ => throw new NotSupportedException($"Unsupported button type: {_buttonType}")
//    };
//}





//public OperatorButton? Button
//{
//    get => _button;
//    set
//    {
//        _button = value;
//        // Update ButtonType if necessary
//        if (_button is Toggle_Button) ButtonType = ButtonType.ToggleButton;
//        else if (_button is Script_Button) ButtonType = ButtonType.ScriptButton;
//    }
//}



//[Category(".Operation > Layout")]
//[Description("The buttons to display.")]
//public SectionCollection Buttons { get; set; }































//[Category(".Operation > Layout")]
//[Description("The number of buttons to display.")]
//[DefaultValue(3)]
//public int ButtonAmount
//{
//    get => buttonAmount;
//    set
//    {
//        if (value != buttonAmount && value >= 1)
//        {
//            buttonAmount = value;
//            UpdateButtons();
//        }
//    }
//}

//private void UpdateButtons()
//{
//    // Remove existing buttons
//    foreach (var button in _buttons)
//    {
//        Controls.Remove(button);
//        button.Dispose();
//    }

//    _buttons.Clear();

//    // Create new buttons
//    for (int i = 0; i < _buttons.Count; i++)
//    {
//        var button = new OperatorButton
//        {
//            Text = $"Button {i + 1}",
//            Parent = this // Automatically adds to Controls
//        };
//        _buttons.Add(button);
//    }

//    PerformLayout(); // Trigger layout recalculation
//}

//protected override void OnResize(EventArgs e)
//{
//    base.OnResize(e);
//    PerformLayout(); // Trigger layout recalculation on resize
//}

//protected override void OnLayout(LayoutEventArgs levent)
//{
//    base.OnLayout(levent);

//    // Layout logic
//    int width = ClientSize.Width;
//    int height = ClientSize.Height;

//    if (Buttons.Count == 0)
//    {
//        return;
//    }
//    else if (Buttons.Count == 1)
//    {
//        // Single button fills the entire control
//        Buttons[0].SetBounds(0, 0, width, height);
//    }
//    else if (Buttons.Count == 2)
//    {
//        // Two buttons split vertically
//        Buttons[0].SetBounds(0, 0, width, height / 2);
//        Buttons[1].SetBounds(0, height / 2, width, height / 2);
//    }
//    else if (Buttons.Count == 3)
//    {
//        // One large button on top and two buttons side-by-side at the bottom
//        int halfHeight = height / 2;
//        int bottomHeight = height - halfHeight;

//        Buttons[0].SetBounds(0, 0, width, halfHeight);
//        Buttons[1].SetBounds(0, halfHeight, width / 2, bottomHeight);
//        Buttons[2].SetBounds(width / 2, halfHeight, width / 2, bottomHeight);
//    }
//    else if (Buttons.Count == 4)
//    {
//        // One large button on top and two buttons side-by-side at the bottom
//        int halfHeight = height / 2;
//        int bottomHeight = height - halfHeight;

//        int thirdWidth = width / 3;

//        Buttons[0].SetBounds(0, 0, width, halfHeight);
//        Buttons[1].SetBounds(0, halfHeight, thirdWidth, bottomHeight);
//        Buttons[2].SetBounds(thirdWidth, halfHeight, thirdWidth, bottomHeight);
//        Buttons[3].SetBounds(2 * thirdWidth, halfHeight, thirdWidth, bottomHeight);
//    }
//    else
//    {
//        // Default fallback: evenly distribute buttons
//        int buttonHeight = height / Buttons.Count;
//        for (int i = 0; i < Buttons.Count; i++)
//        {
//            Buttons[i].SetBounds(0, i * buttonHeight, width, buttonHeight);
//        }
//    }
//}

//private int _buttonCount = 3;

//private void UpdateButtons()
//{
//    // Remove existing buttons from the form
//    foreach (var button in _buttons)
//    {
//        Controls.Remove(button);
//        button.Dispose();
//    }

//    // Clear the buttons list
//    _buttons.Clear();

//    // Create new buttons based on the current size of _buttons
//    for (int i = 0; i < _buttonCount; i++) // Use a separate count property for button amount
//    {
//        var button = new OperatorButton
//        {
//            Text = $"Button {i + 1}",
//            Parent = this // Automatically adds to Controls
//        };
//        _buttons.Add(button);
//    }

//    PerformLayout(); // Trigger layout recalculation
//}

//protected override void OnLayout(LayoutEventArgs levent)
//{
//    base.OnLayout(levent);

//    int width = ClientSize.Width;
//    int height = ClientSize.Height;

//    if (_buttons.Count == 0) return;

//    if (_buttons.Count == 1)
//    {
//        // Single button fills the entire control
//        _buttons[0].SetBounds(0, 0, width, height);
//    }
//    else if (_buttons.Count == 2)
//    {
//        // Two buttons split vertically
//        _buttons[0].SetBounds(0, 0, width, height / 2);
//        _buttons[1].SetBounds(0, height / 2, width, height / 2);
//    }
//    else if (_buttons.Count == 3)
//    {
//        // One large button on top and two buttons side-by-side at the bottom
//        int halfHeight = height / 2;
//        int bottomHeight = height - halfHeight;

//        _buttons[0].SetBounds(0, 0, width, halfHeight);
//        _buttons[1].SetBounds(0, halfHeight, width / 2, bottomHeight);
//        _buttons[2].SetBounds(width / 2, halfHeight, width / 2, bottomHeight);
//    }
//    else if (_buttons.Count == 4)
//    {
//        // One large button on top and three buttons side-by-side at the bottom
//        int halfHeight = height / 2;
//        int bottomHeight = height - halfHeight;

//        int thirdWidth = width / 3;

//        _buttons[0].SetBounds(0, 0, width, halfHeight);
//        _buttons[1].SetBounds(0, halfHeight, thirdWidth, bottomHeight);
//        _buttons[2].SetBounds(thirdWidth, halfHeight, thirdWidth, bottomHeight);
//        _buttons[3].SetBounds(2 * thirdWidth, halfHeight, thirdWidth, bottomHeight);
//    }
//    else
//    {
//        // Default fallback: evenly distribute buttons
//        int buttonHeight = height / _buttons.Count;
//        for (int i = 0; i < _buttons.Count; i++)
//        {
//            _buttons[i].SetBounds(0, i * buttonHeight, width, buttonHeight);
//        }
//    }
//}














//#region >----------------- Open project file: ---------------------
//private void OpenProject(object? sender, EventArgs e)                                                               // CASPAR NEEDS SERVER TO OPEN FIRST
//{
//    string projectFilePath = Properties.Settings.Default.ProjectFile;

//    if (string.IsNullOrWhiteSpace(projectFilePath))
//    {
//        MessageBox.Show("There was no selected project. (Project settings>Project file)");
//        return;
//    }

//    //string fileName = projectFilePath.Split('\\').Last();

//    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
//    {
//        FileName = projectFilePath,
//        UseShellExecute = true
//    });
//}
//#endregion

//#region >----------------- Settings: ---------------------
//private void InitializeSettings()
//{
//    // Graphics Software:
//    //GraphicsSoftwareOption.DataSource = Enum.GetValues(typeof(GraphicsSoftware));                                 // DEPEND ON WHAT ASSEMBLIES ARE LOADED (ALL KEYS IN DICTIONARY) < NOPE
//    GraphicsSoftwareOption.SelectedItem = Properties.Settings.Default.GraphicsSoftware;                             // CHECK IF THIS EXISTS THEN, IF NOT GIVE ERROR WHEN OPENING THE FORM < NOPE

//    // Project file:
//    string projectFile = Properties.Settings.Default.ProjectFile;
//    if (!string.IsNullOrWhiteSpace(projectFile))
//    {
//        string projectName = projectFile.Split('\\').Last();
//        ProjectFile.Text = projectName;
//        ToolTip.SetToolTip(ProjectFile, projectFile);
//    }
//}

//private void SaveGraphicsSoftwareOption(object sender, EventArgs e)
//{
//    // Set the 'graphicsSoftware' setting to the selected ComboBox item
//    //if (GraphicsSoftwareOption.SelectedItem is not null and GraphicsSoftware selectedSoftware)
//    //{
//    //    //Properties.Settings.Default.GraphicsSoftware = selectedSoftware;
//    //    Properties.Settings.Default.Save();
//    //}
//}

//private void ProjectSelection(object sender, EventArgs e)
//{
//    if (sender is not TextBox textBox) return;

//    OpenFileDialog openFileDialog = new();

//    //switch (Properties.Settings.Default.GraphicsSoftware)
//    //{
//    //    case GraphicsSoftware.XPression:
//    //        openFileDialog.Filter = "XPression files (*.xpf;*.xpp)|*.xpf;*.xpp";                // SHOULD NOT DEPEND ON THIS, HAVE THIS INFO IN ENUM?
//    //        break;
//    //    case GraphicsSoftware.CasparCG:
//    //        //openFileDialog.Filter = "CasparCG files (*.;*.)";
//    //        break;
//    //    case GraphicsSoftware.vMix:
//    //        //openFileDialog.Filter = "vMix files (*.;*.)";
//    //        break;
//    //}

//    DialogResult result = openFileDialog.ShowDialog();
//    if (result == DialogResult.OK)
//    {
//        string file = openFileDialog.FileName;
//        try
//        {
//            string projectName = file.Split('\\').Last();
//            textBox.Text = projectName;
//            ToolTip.SetToolTip(ProjectFile, file);
//            Properties.Settings.Default.ProjectFile = file;
//            Properties.Settings.Default.Save();
//        }
//        catch (Exception ex)
//        {
//            MessageBox.Show("Failed setting project file: " + ex);
//        }
//    }
//}

//private void RemoveProjectFileRef(object sender, EventArgs e)
//{
//    ProjectFile.Text = null;
//    ToolTip.SetToolTip(ProjectFile, "");
//    Properties.Settings.Default.ProjectFile = string.Empty;
//    Properties.Settings.Default.Save();
//}
//#endregion























//internal class ClipPathConverter2 : ExpandableObjectConverter

public class ClipPathConverter : ExpandableObjectConverter
{
    public override bool GetStandardValuesSupported(ITypeDescriptorContext? context)
    {
        // Indicates that this property has a list of standard values
        return true;
    }

    public override bool GetStandardValuesExclusive(ITypeDescriptorContext? context)
    {
        // Indicates that the property can only accept values from the list
        return true;
    }

    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext? context)
    {
        // Provide the list of standard values
        var clipPaths = new[]
        {
            new ClipPath(),
            new ClipPath()
        };

        return new StandardValuesCollection(clipPaths);
    }

    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
    {
        // Conversion support (optional, depending on your use case)
        return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object? value, Type destinationType)
    {
        // Convert ClipPath to string for display in the dropdown
        if (destinationType == typeof(string) && value is ClipPath clipPath)
        {
            return clipPath.Scene;
        }

        return base.ConvertTo(context, culture, value, destinationType);
    }

    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type? sourceType)
    {
        // Conversion support (optional, depending on your use case)
        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object? value)
    {
        // Convert string back to ClipPath
        if (value is string stringValue)
        {
            return new ClipPath { Scene = stringValue };
        }

        return base.ConvertFrom(context, culture, value);
    }
}








//private void DisplayNodeButton(object? sender, TreeNodeMouseHoverEventArgs e)
//{
//    if (e.Node is not TreeNode node || sender is not TreeView treeView || treeView.Parent?.Controls is not ControlCollection controls) return;

//    foreach (Control control in controls)
//    {
//        Console.WriteLine(control.Name);
//        if (control.Name == "...")
//        {
//            treeView.Controls.Remove(control);
//            control.Dispose();
//        }
//    }


//    Button dotMenu = new()
//    {
//        Name = "...",
//        //Location = node.Bounds.Location,
//        Bounds = node.Bounds,
//        Width = node.Bounds.Height,
//        //Left = node.Bounds.Width + 100,
//        //Anchor = AnchorStyles.Right,
//        Left = treeView.Width - 100,
//        Text = "...",
//        TextAlign = ContentAlignment.MiddleCenter,
//        ForeColor = Color.White,
//        BackColor = Color.FromArgb(255, 0, 0, 0),
//        FlatStyle = FlatStyle.Flat,
//    };
//    dotMenu.FlatAppearance.BorderSize = 0;
//    node.TreeView.Parent?.Controls.Add(dotMenu);
//    dotMenu.BringToFront();
//}

//private void RemoveNodeButtons(object? sender, MouseEventArgs e)
//{
//    if (sender is not TreeView treeView || treeView.Parent?.Controls is not ControlCollection controls) return;
//    //TreeNode? newlyEnteredNode = treeView.GetNodeAt(e.Location);

//    foreach (Control control in controls)
//    {
//        if (control.Text == "...")
//        {
//            treeView.Controls.Remove(control);
//            control.Dispose();
//        }
//    }
//}














// NOT WORKING CODE:
#region columns stuff temp region:
private static void InitializeContextMenuForColumns(DataGridView dataGridView)
{
    // Create the ContextMenuStrip
    var contextMenu = new ContextMenuStrip();
    var renameItem = new ToolStripMenuItem("Rename column");
    var deleteItem = new ToolStripMenuItem("Delete column");
    var duplicateItem = new ToolStripMenuItem("Duplicate column");
    var changeTypeItem = new ToolStripMenuItem("Change type");

    // Add items to the context menu
    contextMenu.Items.Add(renameItem);
    contextMenu.Items.Add(deleteItem);
    contextMenu.Items.Add(duplicateItem);
    contextMenu.Items.Add(changeTypeItem);

    // Assign event handlers for menu items
    renameItem.Click += (s, e) => RenameColumn(dataGridView);
    deleteItem.Click += (s, e) => DeleteColumn(dataGridView);
    duplicateItem.Click += (s, e) => DuplicateColumn(dataGridView);
    changeTypeItem.Click += (s, e) => ChangeColumnType(dataGridView);

    // Handle right-click on the column headers
    dataGridView.ColumnHeaderMouseClick += (s, e) =>
    {
        if (e.Button == MouseButtons.Right)
        {
            dataGridView.ClearSelection();
            dataGridView.Columns[e.ColumnIndex].Selected = true;
            contextMenu.Show(dataGridView, dataGridView.PointToClient(Cursor.Position));
        }
    };
}

// Rename Column
private static void RenameColumn(DataGridView dataGridView)
{
    if (dataGridView.SelectedColumns.Count > 0)
    {
        var column = dataGridView.SelectedColumns[0];
        var bounds = dataGridView.GetColumnDisplayRectangle(column.Index, true);

        StartEditingWithTextBox(dataGridView, bounds, column.HeaderText, newName =>
        {
            if (!string.IsNullOrWhiteSpace(newName))
                column.HeaderText = newName;
        });
    }
}

// Delete Column
private static void DeleteColumn(DataGridView dataGridView)
{
    if (dataGridView.SelectedColumns.Count > 0)
    {
        var column = dataGridView.SelectedColumns[0];
        if (AskConfirmation($"delete column '{column.HeaderText}'"))
            dataGridView.Columns.Remove(column);
    }
}

// Duplicate Column
private static void DuplicateColumn(DataGridView dataGridView)
{
    if (dataGridView.SelectedColumns.Count > 0)
    {
        var column = dataGridView.SelectedColumns[0];
        var newColumn = (DataGridViewColumn)column.Clone();
        newColumn.HeaderText = $"{column.HeaderText}_Copy";
        dataGridView.Columns.Add(newColumn);
    }
}

// Change Column Type
private static void ChangeColumnType(DataGridView dataGridView)
{
    if (dataGridView.SelectedColumns.Count > 0)
    {
        var column = dataGridView.SelectedColumns[0];
        string[] types = { "Text", "Number", "Date", "Boolean" };
        var bounds = dataGridView.GetColumnDisplayRectangle(column.Index, true);

        PromptForSelection(dataGridView, bounds, types, selectedType =>
        {
            if (!string.IsNullOrWhiteSpace(selectedType))
            {
                // Create a new column based on the selected type
                DataGridViewColumn? newColumn = selectedType switch
                {
                    "Text" => new()
                    {
                        HeaderText = column.HeaderText,
                        Name = column.Name,
                        Width = column.Width,
                        DataPropertyName = column.DataPropertyName
                    },
                    "Number" => new()
                    {
                        HeaderText = column.HeaderText,
                        Name = column.Name,
                        Width = column.Width,
                        DataPropertyName = column.DataPropertyName,
                        ValueType = typeof(int)
                    },
                    "Date" => new()
                    {
                        HeaderText = column.HeaderText,
                        Name = column.Name,
                        Width = column.Width,
                        DataPropertyName = column.DataPropertyName,
                        ValueType = typeof(DateTime)
                    },
                    "Boolean" => new()
                    {
                        HeaderText = column.HeaderText,
                        Name = column.Name,
                        Width = column.Width,
                        DataPropertyName = column.DataPropertyName
                    },
                    _ => null
                };

                if (newColumn != null)
                {
                    // Insert the new column at the same index as the old one
                    int index = column.Index;
                    dataGridView.Columns.Remove(column);
                    dataGridView.Columns.Insert(index, newColumn);
                }
            }
        });
    }
}



private static void PromptForSelection(Control parent, Rectangle bounds, string[] options, Action<string> onCommit)
{
    ContextMenuStrip menu = new();
    foreach (string option in options)
    {
        var menuItem = new ToolStripMenuItem(option);
        menuItem.Click += (s, e) =>
        {
            menu.Hide();
            menu.Dispose();
            onCommit(option);
        };
        menu.Items.Add(menuItem);
    }

    menu.Show(parent, bounds.Location);
}

//private static void StartColumnEditing(DataGridView dataGridView, int columnIndex)
//{
//    var column = dataGridView.Columns[columnIndex];
//    var bounds = dataGridView.GetColumnDisplayRectangle(columnIndex, true);

//    StartEditingWithTextBox(dataGridView, bounds, column.HeaderText, newName =>
//    {
//        if (!string.IsNullOrWhiteSpace(newName))
//            column.HeaderText = newName;
//    });
//}








private Button? newColumnButton;
private void InitializeNewColumnButton(DataGridView dataGridView, TabPage parent)
{
    newColumnButton?.Parent?.Controls.Remove(newColumnButton);
    newColumnButton?.Dispose();

    // Calculate button location (next to the last column)
    Point buttonLocation;
    if (dataGridView.Columns.Count > 0)
    {
        var lastColumn = dataGridView.Columns[dataGridView.Columns.Count - 1];
        var rect = dataGridView.GetCellDisplayRectangle(lastColumn.Index, -1, false);
        buttonLocation = new Point(rect.Right + dataGridView.Location.X, rect.Top + dataGridView.Location.Y);
    }
    else
    {
        buttonLocation = new Point(dataGridView.Location.X, dataGridView.Location.Y);
    }

    // Create the button
    newColumnButton = new()
    {
        Text = "+",
        Location = buttonLocation,
        Size = new Size(30, dataGridView.ColumnHeadersHeight),
        FlatStyle = FlatStyle.Flat,
        BackColor = Color.LightGray,
        Font = new Font("Arial", 10, FontStyle.Bold)
    };

    // Handle the click event
    newColumnButton.Click += (s, e) =>
    {
        var newColumn = new DataGridViewTextBoxColumn { HeaderText = "New Column" };
        dataGridView.Columns.Add(newColumn);
        InitializeNewColumnButton(dataGridView, parent);
    };

    parent.Controls.Add(newColumnButton);
    newColumnButton.BringToFront();
}

// Refresh position on resizing
private void DataGridView_ColumnWidthChanged(object? sender, DataGridViewColumnEventArgs e)
{
    if (sender is DataGridView dataGridView && dataGridView.Parent is TabPage parent)
        InitializeNewColumnButton(dataGridView, parent);
}











private static void StartEditingWithTextBox(Control parent, Rectangle bounds, string initialValue, Action<string> onCommit)
{
    // Create and configure the TextBox
    TextBox textBox = new()
    {
        Text = initialValue,
        TextAlign = HorizontalAlignment.Center,
        Multiline = true,
        BorderStyle = BorderStyle.None,
        Font = parent.Font,
        Location = parent.PointToClient(parent.PointToScreen(bounds.Location)),
        //Location = parent.PointToScreen(bounds.Location),
        //Location = new(parent.Location.X + bounds.Location.X, parent.Location.Y + bounds.Location.Y),
        Size = new Size(bounds.Width, bounds.Height),
    };

    Console.WriteLine("TextBox: " + parent.PointToClient(textBox.Location));
    Console.WriteLine("Parent: " + parent.PointToClient(parent.Location));
    Console.WriteLine("Bounds: " + parent.PointToClient(bounds.Location));


    // Adjust position to align with the tab
    //textBox.Location = parent.PointToClient(parent.PointToScreen(bounds.Location));

    parent.Controls.Add(textBox);
    textBox.BringToFront();
    textBox.Location = parent.PointToClient(textBox.Location);

    // Handle events for committing or canceling changes
    textBox.KeyDown += (sender, e) =>
    {
        if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Escape)
        {
            e.SuppressKeyPress = true;
            parent.Controls.Remove(textBox);
            textBox.Dispose();

            if (e.KeyCode == Keys.Enter)
                onCommit(textBox.Text.Trim());
        }
    };

    textBox.LostFocus += (s, e) =>
    {
        parent.Controls.Remove(textBox);
        textBox.Dispose();
        onCommit(textBox.Text.Trim());
    };

    textBox.Focus();
    textBox.SelectAll();
}

#endregion









//WORKING CODE:
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
//tabControl.ControlAdded -= NewTabButton;
//tabControl.ControlRemoved -= NewTabButton;
//tabControl.SizeChanged -= NewTabButton;


// Attach necessary event handlers
tabControl.MouseDoubleClick += EditTabName;
tabControl.KeyDown += DataViewer_KeyDown;
tabControl.Deselecting += HandleTabDeselection;

tabControl.Resize += NewTabButton;
//tabControl.ControlAdded += NewTabButton;
//tabControl.ControlRemoved += NewTabButton;
//tabControl.SizeChanged += NewTabButton;
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
//var (dataTable, dataAdapter) = LoadTableDataWithCommands(table, connection);
// CommandBuilder to automatically generate INSERT/UPDATE/DELETE commands

dataGridView.DataSource = dataTable;
dataGridView.CellValueChanged += (sender, e) => SaveTable(tableData);

TabPage page = new()
{
Text = table,
//Tag = dataTable,
//Tag = new { DataTable = dataTable, DataAdapter = dataAdapter },
Tag = tableData,
};
page.Controls.Add(dataGridView);
//page.MouseDoubleClick += (sender, e) => StartTabEditing(dataViewer, page, page.Bounds);
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
string command = $"CREATE TABLE \"{tableName}\" (Column1 TEXT)";
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

//// Loads data from a table into a DataTable and sets up the necessary commands
//private static (DataTable DataTable, SQLiteDataAdapter DataAdapter) LoadTableDataWithCommands(string table, SQLiteConnection connection)
//{
//    string query = $"SELECT rowid AS RowID, * FROM {table}";
//    //string query = $"SELECT * FROM {table}";
//    SQLiteDataAdapter dataAdapter = new(query, connection);

//    // Create and configure the DataTable
//    DataTable dataTable = new();
//    dataAdapter.Fill(dataTable);

//    // Set up commands using SQLiteCommandBuilder
//    SQLiteCommandBuilder commandBuilder = new(dataAdapter);
//    dataAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
//    dataAdapter.InsertCommand = commandBuilder.GetInsertCommand();
//    dataAdapter.DeleteCommand = commandBuilder.GetDeleteCommand();

//    return (dataTable, dataAdapter);
//}

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
parent.Controls.Add(newTabButton); // Add the button in the same container as the TabControl
newTabButton.BringToFront(); // Ensure it appears above other controls

//return newTabButton;
}

//// Method to add a new tab (you can customize this logic)
//private static TabPage AddTab(TabControl dataViewer)
//{
//    // This is where you handle creating the new tab
//    TabPage newTab = new("New Table")
//    {
//        Tag = "newTable" // You can customize this further
//    };
//    dataViewer.TabPages.Add(newTab);
//    return newTab;
//}

//// Handles adding a new page when the "+" tab is clicked
//private static void AddPageHandler(object? sender, TabControlCancelEventArgs e)
//{
//    if (e.TabPage?.Tag?.ToString() == "pageAdder" && sender is TabControl dataViewer)
//        StartTabEditing(dataViewer, e.TabPage, dataViewer.GetTabRect(e.TabPageIndex));
//}

// Starts editing the tab name by adding a TextBox
private void StartTabEditing(TabControl tabControl)
{
//MouseEventArgs e = (MouseEventArgs)me;
//if (e.Button == MouseButtons.Left) return;
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

// Ensure text is selected after rendering
//Task.Delay(50).ContinueWith(_ => textBox.Invoke(() => { textBox.Focus(); textBox.SelectAll(); }));
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

//MessageBox.Show($"Table '{oldName}' has been renamed to '{newName}'", "Rename Successful");
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
    //MessageBox.Show("Single table saving not implemented yet");
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
    //if (e.TabPage == null) return;

    //SQLiteDataAdapter dataAdapter = tag.DataAdapter as SQLiteDataAdapter;
    //SQLiteDataAdapter dataAdapter = GetDataAdapter(e.TabPage.Text, connection);
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




private SQLiteConnection? connection;

private void LoadData(string connectionString, TabControl dataViewer)
{
    try
    {
        // Open new connection with the chosen database
        connection?.Close();
        dataViewer.TabPages.Clear();
        if (connection == null || connection.State != ConnectionState.Open) connection = new SQLiteConnection(connectionString);
        connection.Open();

        // Load each table in dataViewer
        foreach (var table in GetTables(connection))
        {
            DataGridView dataGridView = new()
            {
                Dock = DockStyle.Fill,
            };

            // Load data into a DataTable
            //string query = $"SELECT * FROM {table}";
            string query = $"SELECT rowid AS RowID, * FROM {table}";
            SQLiteDataAdapter dataAdapter = new(query, connection);

            // CommandBuilder to automatically generate INSERT/UPDATE/DELETE commands
            var commandBuilder = new SQLiteCommandBuilder(dataAdapter);

            DataTable dataTable = new();
            dataAdapter.Fill(dataTable);


            // Bind the DataTable to the DataGridView
            dataGridView.DataSource = dataTable;


            dataGridView.CellValueChanged += (sender, e) => SaveTable(dataAdapter, dataTable);
            //dataGridView.UserDeletingRow += AskConfirmation;
            dataGridView.KeyDown += AskConfirmation;
            //dataGridView.RowsRemoved += 

            TabPage page = new()
            {
                Text = table,
                Tag = dataAdapter,
            };
            page.Controls.Add(dataGridView);
            dataViewer.TabPages.Add(page);



            // Hide the RowID column
            if (dataTable.Columns.Contains("RowID"))
            {
                dataGridView.Columns["RowID"].Visible = false;
            }
        }
        TabPage newPage = new()
        {
            Text = "         +         ",
            Tag = "pageAdder",
        };
        dataViewer.Selecting += AddPage;
        dataViewer.TabPages.Add(newPage);
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error loading data: " + ex.Message);
    }
}

// Function to get table names from the SQLite database
public static IEnumerable<string> GetTables(SQLiteConnection connection)
{
    string query = "SELECT name FROM sqlite_master WHERE type='table' AND name NOT LIKE 'sqlite_%';";

    // Query the database and return the list of table names
    var tableNames = connection.Query<string>(query);

    return tableNames;
}

private static void AddPage(object? sender, TabControlCancelEventArgs e)
{
    if (e.TabPage?.Tag?.ToString() == "pageAdder")
    {
        if (sender is not TabControl dataViewer) return;
        Rectangle tabRect = dataViewer.GetTabRect(e.TabPageIndex);
        StartTabEditing(dataViewer, e.TabPage, tabRect);
    }
}

private static void StartTabEditing(TabControl tabControl, TabPage tabPage, Rectangle tabRect)
{
    // Create a TextBox for in-place editing
    TextBox textBox = new()
    {
        Text = "Table name",
        TextAlign = HorizontalAlignment.Center,
        Multiline = true,
        BorderStyle = BorderStyle.None,
        Font = tabControl.Font,
        Location = tabControl.PointToScreen(tabRect.Location),
        Size = new Size(tabRect.Width, tabRect.Height)
    };

    // Add the TextBox to the form or the parent container of the TabControl
    Control? parent = tabControl.Parent;
    if (parent == null) return;
    parent.Controls.Add(textBox);
    textBox.BringToFront();
    textBox.Focus();

    // Adjust position to align with the tab
    textBox.Location = parent.PointToClient(tabControl.PointToScreen(tabRect.Location));

    // Handle text changes
    textBox.KeyDown += (s, ev) =>
    {
        if (ev.KeyCode == Keys.Enter || ev.KeyCode == Keys.Escape)
        {
            ev.SuppressKeyPress = true; // Prevent beep on Enter/Escape
            textBox.Leave -= (s, ev) => FinishTabEditing(tabControl, tabPage, textBox);
            FinishTabEditing(tabControl, tabPage, textBox);
        }
    };

    // Delay the selection to ensure it works
    Task.Delay(50).ContinueWith(_ =>
    {
        textBox.Invoke(() =>
        {
            textBox.Focus();
            textBox.SelectAll();
        });
    });
}

private static void FinishTabEditing(TabControl tabControl, TabPage tabPage, TextBox textBox)
{
    // Set the TabPage's text to the TextBox's value
    if (!string.IsNullOrWhiteSpace(textBox.Text))
    {
        tabPage.Text = textBox.Text.Trim();
    }

    // Remove the TextBox
    Control? parent = tabControl.Parent;
    parent?.Controls.Remove(textBox);
    textBox.Dispose();
}














// MODULE LOADER CODE:
//public static void LoadFilesFromFolder(string folder, PluginType type, TreeView treeviewExplorer)
//{
//    // Loop through each DLL in the Modules folder
//    foreach (string file in Directory.GetFiles(folder, "*.dll", SearchOption.AllDirectories))
//    {
//        try
//        {
//            // Load the assembly
//            var context = new AssemblyLoadContext(file, isCollectible: true);
//            Assembly assembly = context.LoadFromAssemblyPath(file);

//            // Find all types in the assembly that implement IFormPlugin
//            foreach (Type type in assembly.GetTypes())
//            {
//                if (!typeof(IFormPlugin).IsAssignableFrom(type) && type.IsInterface && type.IsAbstract) return;

//                // Create an instance of the module form
//                var typeInstance = Activator.CreateInstance(type);
//                if (typeInstance == null) return;
//                IFormPlugin moduleForm = (IFormPlugin)typeInstance;

//                if (moduleForm == null) return;

//                string[] relativePath = Path.GetRelativePath(folder, file).Split('\\');

//                // Add the plugin form to the TreeView
//                //AddToTreeView(moduleForm, relativePath, treeviewExplorer.Nodes);
//            }
//        }
//        catch (Exception ex)
//        {
//            MessageBox.Show($"Error loading module {file}: {ex.Message}", "Plugin Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//        }
//    }
//}



//public static void LoadModules(TreeView treeviewExplorer)
//{
//    // Get or create "Modules" folder in the app directory
//    string moduleFolder = Path.Combine(Application.StartupPath, "Modules");
//    if (!Directory.Exists(moduleFolder))
//        Directory.CreateDirectory(moduleFolder);

//    // Loop through each DLL in the Modules folder
//    foreach (string file in Directory.GetFiles(moduleFolder, "*.dll", SearchOption.AllDirectories))
//    {
//        try
//        {
//            // Load the assembly
//            var context = new AssemblyLoadContext(file, isCollectible: true);
//            Assembly assembly = context.LoadFromAssemblyPath(file);

//            // Find all types in the assembly that implement IFormPlugin
//            foreach (Type type in assembly.GetTypes())
//            {
//                if (!typeof(IFormPlugin).IsAssignableFrom(type) && type.IsInterface && type.IsAbstract) return;

//                // Create an instance of the module form
//                var typeInstance = Activator.CreateInstance(type);
//                if (typeInstance == null) return;
//                IFormPlugin moduleForm = (IFormPlugin)typeInstance;

//                if (moduleForm == null) return;

//                string[] relativePath = Path.GetRelativePath(moduleFolder, file).Split('\\');

//                // Add the plugin form to the TreeView
//                AddToTreeView(moduleForm, moduleForm.FormName, relativePath, treeviewExplorer.Nodes);
//            }
//        }
//        catch (Exception ex)
//        {
//            MessageBox.Show($"Error loading module {file}: {ex.Message}", "Plugin Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//        }
//    }
//}


//private static TreeNode AddToTreeView(IFormPlugin moduleForm, string[] filePath, TreeView? treeviewExplorer = null)
//{
//    TreeNode? node = treeviewExplorer?.Nodes
//        .Cast<TreeNode>()
//        .FirstOrDefault(n => n.Text == filePath[0]);
//    if (filePath.Length == 1)
//    {
//        // Store the plugin form in the Tag property for easy access later
//        node = new(moduleForm.FormName) { Tag = moduleForm };
//    }
//    else
//    {
//        node ??= new TreeNode(filePath[0]);
//        string[] newFilePath = filePath.Skip(1).ToArray();
//        node.Nodes.Add(AddToTreeView(moduleForm, newFilePath));
//    }
//    treeviewExplorer?.Nodes.Add(node);
//    return node;
//}

//private static TreeNode AddToTreeView(IFormPlugin moduleForm, string[] filePath, TreeNodeCollection nodes)
//{
//    // If no nodes are provided, use the root nodes
//    //nodes ??= treeviewExplorer?.Nodes;

//    // Find an existing node at the current level
//    TreeNode? node = nodes
//        .Cast<TreeNode>()
//        .FirstOrDefault(n => n.Text == filePath[0]);

//    // If the node doesn't exist, create and add it
//    node ??= new TreeNode(filePath[0]);
//    if (!nodes.Contains(node))
//    {
//        nodes.Add(node);
//    }

//    // If we've reached the final level, add the form
//    if (filePath.Length == 1)
//    {
//        node.Tag = moduleForm; // Store the plugin form in the Tag property
//        node.Text = moduleForm.FormName; // Set the text to the form name
//    }
//    else
//    {
//        // Recursively process the next level
//        string[] newFilePath = filePath.Skip(1).ToArray();
//        AddToTreeView(moduleForm, newFilePath, node.Nodes);
//    }

//    return node;
//}













//private readonly List<Interfaces> formHistory = [];

//private void SaveFormToHistory(Interfaces form)
//{
//    if (formHistory.Count > 10) formHistory.Remove(formHistory.Last());
//    formHistory.Prepend(form);
//}

//private Interfaces? GetLastFromFormHistory(string formType) //                 CHANGE TO TYPE INSTEAD OF STRING
//{
//    Interfaces? form = formHistory.Find(match: x => x.Tag?.ToString() == formType);
//    return form;
//}




//private void InitializeForms()
//{
//    if (ContentsPanel == null) return;
//    Form form = new DataBaseForm();
//    OpenFormInPanel(form, ContentsPanel.TabPages[1]);
//}


//private static void ScaleFormToFitPanel(Interfaces form, Panel panel)
//{
//    //// Get the original size of the form
//    //Size originalSize = form.Size;

//    // Check if the form's original size is already stored
//    if (form.Tag is Size originalSize)
//    {
//        // Reset the form to its original size
//        form.Size = originalSize;

//        // Reset all controls to their original sizes and locations
//        foreach (Control control in form.Controls)
//        {
//            if (control.Tag is (Size controlOriginalSize, Point controlOriginalLocation))
//            {
//                control.Size = controlOriginalSize;
//                control.Location = controlOriginalLocation;
//            }
//        }
//    }
//    else
//    {
//        // Store the original size of the form in its Tag property
//        form.Tag = form.Size;

//        // Store the original size and location of each control
//        foreach (Control control in form.Controls)
//        {
//            control.Tag = (control.Size, control.Location);
//        }
//    }

//    // Calculate scaling factors for width and height
//    float scaleX = (float)panel.Width / form.Size.Width;
//    float scaleY = (float)panel.Height / form.Size.Height;

//    // Use non-cumulative scaling
//    form.Scale(new SizeF(scaleX, scaleY));
//}



//private static void ScaleFormToFitPanel(object? formModulePanel)
//{
//    if (formModulePanel is TabControl tabControl)
//        formModulePanel = tabControl.SelectedTab;

//    if (formModulePanel is not Panel panel || panel.Tag is not Interfaces form)
//        return;

//    // Check for size change
//    var currentPanelSize = panel.Size;

//    // Retrieve or initialize original size and control data
//    if (form.Tag is not (Size originalSize, Size lastScaledSize, Dictionary<Control, (Size, Point)> controlOriginalData))
//    {
//        originalSize = form.Size;
//        lastScaledSize = currentPanelSize;

//        // Store original size and location for each control
//        controlOriginalData = form.Controls
//            .OfType<Control>()
//            .ToDictionary(control => control, control => (control.Size, control.Location));

//        // Store the initial data in form.Tag
//        form.Tag = (originalSize, lastScaledSize, controlOriginalData);
//    }
//    else if (lastScaledSize == currentPanelSize)
//    {
//        // Skip scaling if panel size hasn't changed
//        return;
//    }

//    // Calculate scaling factors
//    float scaleX = (float)currentPanelSize.Width / originalSize.Width;
//    float scaleY = (float)currentPanelSize.Height / originalSize.Height;

//    if (Math.Abs(scaleX - 1) > 0.01 || Math.Abs(scaleY - 1) > 0.01)
//    {
//        form.SuspendLayout(); // Reduce flickering during scaling

//        // Reset controls to original size and location before scaling
//        foreach (var (control, (controlOriginalSize, controlOriginalLocation)) in controlOriginalData)
//        {
//            control.Size = controlOriginalSize;
//            control.Location = controlOriginalLocation;
//        }

//        // Scale controls
//        foreach (var control in form.Controls.OfType<Control>())
//        {
//            control.Scale(new SizeF(scaleX, scaleY));
//        }

//        // Scale the form itself
//        form.Scale(new SizeF(scaleX, scaleY));

//        // Update last-scaled size in Tag
//        form.Tag = (originalSize, currentPanelSize, controlOriginalData);

//        form.ResumeLayout();
//    }
//}






















#region >----------------- Testing Preview on Hover: ---------------------
#if HAS_XPRESSION
        //private void DisplayPreview(object sender, EventArgs e)
        //{
        //    if (sender is OperatorButton button && button.ClipPaths != null)
        //    {
        //        ClipPathCollection clipPath = button.ClipPaths;


        //        if (clipPath == null || clipPath.Count == 0)
        //        {
        //            return;
        //        }
        //        string? scene = clipPath[0].Scene;
        //        string? clip = clipPath[0].Clip;
        //        string? track = null;
        //        int channel = 1;
        //        int layer = clipPath[0].Layer;
        //        string? sceneDirector = clipPath[0].SceneDirector;

        //        // Check if any of the fields are empty:
        //        if (string.IsNullOrWhiteSpace(scene) ||
        //            string.IsNullOrWhiteSpace(clip))
        //        {
        //            CommonFunctions.ControlWarning(button, "Warning: Scene on button: " + button.Text + " must be set!");
        //            return;
        //        }



        //        // If XPression reference exists, play the clip

        //        xpEngine XPression = new();
        //        if (XPression.GetSceneByName(scene, out xpScene SceneGraphic, true))
        //        {
        //            //XP_Functions.PlaySceneState(SceneGraphic, scene, clip, track, channel, layer);

        //            //SceneGraphic.GetPreviewSceneDirector(out xpSceneDirector director);
        //            XP_Functions.PlayPreview(SceneGraphic, sceneDirector, clip, track, out xpImage? previewImage, channel, layer);

        //            if (previewImage != null)
        //            {
        //                Bitmap image = ConvertToBitmap(previewImage);
        //                pictureBox1.Image = image;
        //            }

        //            // GETRENDEREDFRAME
        //            // Hardware setup > add in inputs/outputs > go to preview (> set channel 1 with next as preview in preview & monitor)
        //            // playscene on channel 2 (maybe see if it can be hidden)
        //            // XPRession.GetOutputFrameBuffer > gives output of channel 2 (needs to be set as a preview channel in xpression)
        //            //xpOutputFrameBuffer.getCurrentFrame() > puts out an image

        //        }
        //        else
        //        {
        //            CommonFunctions.ControlWarning(button, "Warning: " + scene + ">" + track + " on button: " + button.Text + " could not be found!");
        //        }


        //        //XPression.GetRenderInfo(out int frameBufferIndex, out int field);
        //        //XPression.GetInputFrameBuffer(1, out xpInputFrameBuffer preview);
        //        //if(frameBufferIndex != null && fi)
        //        //{
        //            //MessageBox.Show(frameBufferIndex.ToString() + "  |  " + field.ToString());
        //        //} else
        //        //{
        //        //    MessageBox.Show("frameBufferIndex is NULL");
        //        //}
        //    }
        //}





        //public void DisplayPreview(object sender, EventArgs e)
        //{
        //    if (!IsXPressionDonglePresent()) return;
        //    if (sender is OperatorButton button && button.Scene != null)
        //    {
        //        string scene = button.Scene;

        //        xpEngine XPression = new();
        //        if (XPression.GetSceneByName(scene, out xpScene SceneGraphic, true))
        //        {
        //            XP_Functions.GetThumbnail(SceneGraphic, out xpImage? thumbnail);

        //            if (thumbnail != null)
        //            {
        //                Bitmap image = ConvertToBitmap(thumbnail);
        //                PreviewBox.SizeMode = PictureBoxSizeMode.Zoom;
        //                PreviewBox.Image = image;
        //            }
        //        }
        //        else
        //        {
        //            CommonFunctions.ControlWarning(button, "Warning: There is no Scene on button: " + button.Text + "!");
        //        }
        //    }
        //}

        //public void RemovePreview(object sender, EventArgs e)
        //{
        //    PreviewBox.Image = null;
        //}

        //private static Bitmap ConvertToBitmap(xpImage thumbNailImage)
        //{
        //    Bitmap bmp;
        //    bmp = xpTools.xpImageToBitmap(thumbNailImage);
        //    return bmp;
        //}




        //private void RemovePreview(object sender, EventArgs e)
        //{
        //    if (sender is OperatorButton button && button.ClipPaths != null)
        //    {
        //        ClipPathCollection clipPath = button.ClipPaths;


        //        if (clipPath == null || clipPath.Count == 0)
        //        {
        //            return;
        //        }
        //        string? scene = clipPath[0].Scene;

        //        xpEngine XPression = new();
        //        if (XPression.GetSceneByName(scene, out xpScene SceneGraphic, true))
        //        {
        //            XP_Functions.StopPreview(SceneGraphic);
        //        }
        //    }
        //}

#endif
#endregion









// Custom buttons with borderless window. Not needed honestly
private const int HTLEFT = 10;
private const int HTRIGHT = 11;
private const int HTTOP = 12;
private const int HTTOPLEFT = 13;
private const int HTTOPRIGHT = 14;
private const int HTBOTTOM = 15;
private const int HTBOTTOMLEFT = 16;
private const int HTBOTTOMRIGHT = 17;
private const int WM_NCHITTEST = 0x84;
private const int WM_NCLBUTTONDOWN = 0xA1;


// Import user32.dll for moving the window
[DllImport("user32.dll")]
public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
[DllImport("user32.dll")]
public static extern bool ReleaseCapture();


// Variables for custom buttons
public Button? closeButton;
public Button? minimizeButton;
public Button? maximizeButton;
private bool isMaximized = false;

public CustomForm()
{
    // Set the form to be borderless
    this.FormBorderStyle = FormBorderStyle.None;
    this.DoubleBuffered = true;
    this.BackColor = Color.Gray;

    // Add event handlers for moving the form
    this.MouseDown += BorderlessForm_MouseDown;

    // Initialize custom buttons
    InitializeCustomButtons();

    // Handle the form's Load event to position buttons correctly
    this.Load += CustomForm_Load;
    this.Resize += CustomForm_Resize;
}



private void BorderlessForm_MouseDown(object? sender, MouseEventArgs e)
{
    if (e.Button == MouseButtons.Left)
    {
        ReleaseCapture();
        SendMessage(Handle, WM_NCLBUTTONDOWN, 0x2, 0); // Move the window
    }
}

// Override WndProc to handle resizing
protected override void WndProc(ref Message m)
{
    base.WndProc(ref m);

    if (m.Msg == WM_NCHITTEST)
    {
        var cursor = this.PointToClient(Cursor.Position);

        if (cursor.X < 10 && cursor.Y < 10)
            m.Result = (IntPtr)HTTOPLEFT; // Top-left corner
        else if (cursor.X > this.ClientSize.Width - 10 && cursor.Y < 10)
            m.Result = (IntPtr)HTTOPRIGHT; // Top-right corner
        else if (cursor.X < 10 && cursor.Y > this.ClientSize.Height - 10)
            m.Result = (IntPtr)HTBOTTOMLEFT; // Bottom-left corner
        else if (cursor.X > this.ClientSize.Width - 10 && cursor.Y > this.ClientSize.Height - 10)
            m.Result = (IntPtr)HTBOTTOMRIGHT; // Bottom-right corner
        else if (cursor.X < 10)
            m.Result = (IntPtr)HTLEFT; // Left edge
        else if (cursor.X > this.ClientSize.Width - 10)
            m.Result = (IntPtr)HTRIGHT; // Right edge
        else if (cursor.Y < 10)
            m.Result = (IntPtr)HTTOP; // Top edge
        else if (cursor.Y > this.ClientSize.Height - 10)
            m.Result = (IntPtr)HTBOTTOM; // Bottom edge
    }
}

private void CloseButton_Click(object? sender, EventArgs e)
{
    this.Close();
}

private void MinimizeButton_Click(object? sender, EventArgs e)
{
    this.WindowState = FormWindowState.Minimized;
}

private void MaximizeRestoreButton_Click(object? sender, EventArgs e)
{
    if (maximizeButton == null) return;
    if (isMaximized)
    {
        this.WindowState = FormWindowState.Normal;
        isMaximized = false;
        maximizeButton.Text = "□"; // Change to maximize icon
    }
    else
    {
        this.WindowState = FormWindowState.Maximized;
        isMaximized = true;
        maximizeButton.Text = "❐"; // Change to restore icon
    }
}




// Handle form's Load event to position buttons after the form is loaded
private void CustomForm_Load(object? sender, EventArgs e)
{
    PositionButtons();
}

// Handle form resizing to adjust button positions dynamically
private void CustomForm_Resize(object? sender, EventArgs e)
{
    PositionButtons();
}


// Initialize the custom buttons
private void InitializeCustomButtons()
{
    // Close button
    closeButton = new Button
    {
        FlatStyle = FlatStyle.Flat,
        BackColor = Color.Transparent,
        ForeColor = Color.DarkGray,
        Text = "x",
        Font = new Font("Arial", 14, FontStyle.Bold),
        Size = new Size(70, 50),
        FlatAppearance = { BorderSize = 0 },
        Cursor = Cursors.Hand,
        Padding = new Padding(0),
        TextAlign = ContentAlignment.MiddleCenter,
        Anchor = AnchorStyles.Top | AnchorStyles.Right,
        UseVisualStyleBackColor = true,
    };
    closeButton.Click += CloseButton_Click;
    // rgb(232 17 35)
    closeButton.MouseEnter += (sender, e) => { closeButton.BackColor = Color.FromArgb(232, 17, 35); };
    closeButton.MouseLeave += (sender, e) => { closeButton.BackColor = Color.Transparent; };
    this.Controls.Add(closeButton);

    // Maximize/Restore button
    maximizeButton = new Button
    {
        FlatStyle = FlatStyle.Flat,
        BackColor = Color.Transparent,
        ForeColor = Color.White,
        Text = "□",
        Font = new Font("Arial", 18),
        Size = new Size(70, 50),
        FlatAppearance = { BorderSize = 0 },
        Cursor = Cursors.Hand,
        Padding = new Padding(0),
        TextAlign = ContentAlignment.MiddleCenter,
        Anchor = AnchorStyles.Top | AnchorStyles.Right,
        UseVisualStyleBackColor = true,
    };
    maximizeButton.Click += MaximizeRestoreButton_Click;
    maximizeButton.MouseEnter += (sender, e) => { maximizeButton.BackColor = Color.FromArgb(60, 60, 60); };
    maximizeButton.MouseLeave += (sender, e) => { maximizeButton.BackColor = Color.Transparent; };
    this.Controls.Add(maximizeButton);

    // Minimize button
    minimizeButton = new Button
    {
        FlatStyle = FlatStyle.Flat,
        BackColor = Color.Transparent,
        ForeColor = Color.White,
        Text = "-",
        Font = new Font("Arial", 18, FontStyle.Bold),
        Size = new Size(70, 50),
        FlatAppearance = { BorderSize = 0 },
        Cursor = Cursors.Hand,
        Padding = new Padding(0),
        TextAlign = ContentAlignment.MiddleCenter,
        Anchor = AnchorStyles.Top | AnchorStyles.Right,
        UseVisualStyleBackColor = true,
    };
    minimizeButton.Click += MinimizeButton_Click;
    minimizeButton.MouseEnter += (sender, e) => { minimizeButton.BackColor = Color.FromArgb(60, 60, 60); };
    minimizeButton.MouseLeave += (sender, e) => { minimizeButton.BackColor = Color.Transparent; };
    this.Controls.Add(minimizeButton);
}

// Position the buttons based on form size
private void PositionButtons()
{
    if (closeButton == null || minimizeButton == null || maximizeButton == null) return;
    // Position buttons in the top-right corner
    closeButton.Location = new Point(this.Width - 70, 0);
    maximizeButton.Location = new Point(this.Width - 140, 0);
    minimizeButton.Location = new Point(this.Width - 210, 0);
}














// Maybe fix later::

public static void LoadModules(TreeView treeviewExplorer)
{
    // Get or create "Modules" folder in the app directory
    string moduleFolder = Path.Combine(Application.StartupPath, "Modules");
    if (!Directory.Exists(moduleFolder)) Directory.CreateDirectory(moduleFolder);




    SortFiles(moduleFolder, treeviewExplorer);


    //// Loop through each DLL in the Modules folder
    //foreach (string file in Directory.GetFiles(moduleFolder))
    //{
    //    // Add to treeview



    //    try
    //    {
    //        // Load the assembly
    //        Assembly assembly = Assembly.LoadFrom(file);

    //        // Find all types in the assembly that implement IModuleForm
    //        foreach (Type type in assembly.GetTypes())
    //        {
    //            if (!typeof(IModuleForm).IsAssignableFrom(type) && type.IsInterface && type.IsAbstract) return;

    //            // Create an instance of the module form
    //            var typeInstance = Activator.CreateInstance(type);
    //            if (typeInstance == null) return;
    //            IModuleForm moduleForm = (IModuleForm)typeInstance;

    //            if (moduleForm == null) return;

    //            string[] relativePath = Path.GetRelativePath(moduleFolder, file).Split('\\');

    //            // Add the plugin form to the TreeView
    //            AddToTreeView(moduleForm, relativePath, treeviewExplorer);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show($"Error loading plugin {file}: {ex.Message}", "Plugin Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    //    }
    //}
}


public static void SortFiles(string folder, TreeView treeviewExplorer)
{
    foreach (string file in Directory.GetFiles(folder))
    {
        // Check what file type:
        // switch case for:
        // folder: add as node and recursive SortFiles()
        // Form.dll: load into 


        if (file.EndsWith(".dll"))
        {
            if (!LoadDllFile(file, treeviewExplorer, out FileLoadingTypes? fileType, out IModuleForm? form)) return;
            string[] relativePath = Path.GetRelativePath(folder, file).Split('\\');
            if (form != null) AddToTreeView(form, relativePath, treeviewExplorer);
        }


        //string[] relativePath = Path.GetRelativePath(moduleFolder, filePath).Split('\\');

        //// Add the plugin form to the TreeView
        //AddToTreeView(moduleForm, relativePath, treeviewExplorer);
    }
}

public static bool LoadDllFile(string filePath, TreeView treeviewExplorer, out FileLoadingTypes? fileType, out IModuleForm? moduleForm)
{
    fileType = null;
    moduleForm = null;
    try
    {
        // Load the assembly
        Assembly assembly = Assembly.LoadFrom(filePath);

        // Find all types in the assembly that implement IModuleForm
        foreach (Type type in assembly.GetTypes())
        {
            if (!typeof(IModuleForm).IsAssignableFrom(type) && type.IsInterface && type.IsAbstract) return false;

            fileType = FileLoadingTypes.Form;

            // Create an instance of the module form
            var typeInstance = Activator.CreateInstance(type);
            if (typeInstance == null) return false;
            moduleForm = (IModuleForm)typeInstance;

            if (moduleForm == null) return false;
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Error loading plugin {filePath}: {ex.Message}", "Plugin Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
    }
    return true;
}
#endregion

#region >----------------- AddToTreeView: ---------------------
private static TreeNode AddToTreeView(object file, string[] filePath, TreeView? treeviewExplorer = null)
{
    // Store the plugin form in the Tag property for easy access later
    if (file is IModuleForm moduleForm)
    {
        node = new(moduleForm.FormName) { Tag = moduleForm };
    }
    else
    {
        return null;
    }




    TreeNode? node = treeviewExplorer?.Nodes
        .Cast<TreeNode>()
        .FirstOrDefault(n => n.Text == filePath[0]);
    if (filePath.Length == 1)
    {
        // Store the plugin form in the Tag property for easy access later
        if (file is IModuleForm moduleForm)
        {
            node = new(moduleForm.FormName) { Tag = moduleForm };
        }
        else
        {
            return null;
        }
    }
    else
    {
        node ??= new TreeNode(filePath[0]);
        string[] newFilePath = filePath.Skip(1).ToArray();
        node.Nodes.Add(AddToTreeView(file, newFilePath));
    }
    treeviewExplorer?.Nodes.Add(node);
    return node;
}
#endregion


























private static void ScaleFormToFitPanel(Form form, Panel panel)
{
    //// Get the original size of the form
    //Size originalSize = form.Size;

    // Check if the form's original size is already stored
    if (form.Tag is Size originalSize)
    {
        // Reset the form to its original size
        form.Size = originalSize;

        // Reset all controls to their original sizes and locations
        foreach (Control control in form.Controls)
        {
            if (control.Tag is (Size controlOriginalSize, Point controlOriginalLocation))
            {
                control.Size = controlOriginalSize;
                control.Location = controlOriginalLocation;
            }
        }
    }
    else
    {
        // Store the original size of the form in its Tag property
        form.Tag = form.Size;

        // Store the original size and location of each control
        foreach (Control control in form.Controls)
        {
            control.Tag = (control.Size, control.Location);
        }
    }

    // Calculate scaling factors for width and height
    float scaleX = (float)panel.Width / form.Size.Width;
    float scaleY = (float)panel.Height / form.Size.Height;

    // Use non-cumulative scaling
    form.Scale(new SizeF(scaleX, scaleY));
}



#region UNUSED:
#region >----------------- XPression play preview scene: ---------------------
/// <summary>
/// Plays out the scene in XPression when this project is open in XPression.
/// </summary>
/// <param name = "scene">The xpScene in which the clip is located in.</param>
/// <param name = "sceneDirectorName">The name of the scene director.</param>
/// /// <param name = "clip">The name of the clip that needs to be played.</param>
/// <param name = "previewChannel">The channel on which the clip needs to play.</param>
/// <param name="layer">The layer on which the clip needs to play.</param>
/// <returns>true if succsesfull, false if the defaultFrame could not be found</returns>
public static bool PlayPreview(xpScene scene, string? sceneDirectorName, string clip, string? track, out xpImage? image, int previewChannel = 1, int layer = 1)
{
    image = null;
    // Set to be a default of the same name as its scene if it was not filled in
    if (string.IsNullOrWhiteSpace(sceneDirectorName) || sceneDirectorName == "Same as [Scene]")
    {
        sceneDirectorName = scene.Name;
        //sceneDirectorName = scene.SceneDirector
    }
    if (string.IsNullOrWhiteSpace(track))
    {
        track = "StateTrack";
    }

    if (!scene.GetSceneDirectorByName(sceneDirectorName, out xpSceneDirector Scene_Director) ||
        !Scene_Director.GetTrackByName(track, out xpSceneDirectorTrack Scene_State_Track) ||
        !Scene_State_Track.GetClipByName(clip, out xpSceneDirectorClip State_Clip))
    {
        return false;
    }

    ////int defaultFrame = Scene_Director.DefaultFrameMarker;
    //int lastFrameOfClip = State_Clip.Position + State_Clip.Duration - 1;
    //Scene_Director.PlayRange(lastFrameOfClip, lastFrameOfClip);
    //scene.SetOnline(previewChannel, layer);
    //Scene_Director.PlayRange(lastFrameOfClip, lastFrameOfClip + 1, true);
    ////scene.GetPreviewSceneDirector(out xpSceneDirector previewSD);
    ////previewSD.
    //scene.GetRenderedFrame(lastFrameOfClip, scene.Width, scene.Height, out image);

    scene.GetThumbnail(out image);
    return true;
}
#endregion

#region >----------------- XPression get single object: ---------------------
/// < summary >
/// Gives the material of the specified object in a scene, given that this project is open in XPression.
/// </summary>
/// <param name="objectName">The name of the object (in XPression) to find the material of.</param>
/// <param name="scene">The scene in which this material needs to be searched.</param>
/// <returns>The IxpBaseObject as the material.</returns>
public static IxpBaseObject getMaterialFromScene(string objectName, xpScene scene)
{
    scene.GetObjectByName(objectName, out xpBaseObject sceneObject);

    return sceneObject;
}
#endregion

#region >----------------- XPression play preview scene: ---------------------
/// <summary>
/// Plays out the scene in XPression when this project is open in XPression.
/// </summary>
/// <param name = "scene">The xpScene in which the clip is located in.</param>
/// <param name = "sceneDirectorName">The name of the scene director.</param>
/// <param name = "previewChannel">The channel on which the clip needs to play.</param>
/// <param name="layer">The layer on which the clip needs to play.</param>
/// <returns>true if succsesfull, false if the defaultFrame could not be found</returns>
public static bool StopPreview(xpScene scene)
{
    // Set to be a default of the same name as its scene if it was not filled in
    //if (string.IsNullOrWhiteSpace(sceneDirectorName) || sceneDirectorName == "Same as [Scene]")
    //{
    //    sceneDirectorName = scene.Name;
    //    //sceneDirectorName = scene.SceneDirector
    //}
    //if (string.IsNullOrWhiteSpace(track))
    //{
    //    track = "StateTrack";
    //}

    //if (!scene.GetSceneDirectorByName(sceneDirectorName, out xpSceneDirector Scene_Director) ||
    //    !Scene_Director.GetTrackByName(track, out xpSceneDirectorTrack Scene_State_Track))
    //{ return false; }

    //int defaultFrame = Scene_Director.DefaultFrameMarker;
    //Scene_Director.PlayRange(defaultFrame, defaultFrame + 1);
    scene.SetPreview();
    return true;
}
#endregion
#endregion




private Bitmap? cachedBitmap;
private xpImage? cachedImage;
private byte[]? reusableBuffer;
private DateTime lastUpdate = DateTime.MinValue;

private Bitmap OptimizedGetBitmap(xpImage image)
{
    const int updateIntervalMs = 100;

    // Throttle updates
    if ((DateTime.Now - lastUpdate).TotalMilliseconds < updateIntervalMs && cachedImage == image)
    {
        return cachedBitmap;
    }

    lastUpdate = DateTime.Now;

    // Dispose previous bitmap
    DisposeBitmap(ref cachedBitmap);

    // Retrieve raw data
    var adapter = image.GetType().GetProperty("Adapter")?.GetValue(image);
    if (adapter is Array byteArray)
    {
        reusableBuffer = GetReusableBuffer(byteArray.Length);
        Buffer.BlockCopy(byteArray, 0, reusableBuffer, 0, byteArray.Length);

        // Create a new bitmap
        cachedBitmap = CreateBitmapFromRawBmpData(reusableBuffer);
        cachedImage = image;
    }
    else
    {
        throw new InvalidOperationException("Failed to extract BMP data from Adapter.");
    }

    return cachedBitmap;
}

private byte[]? buffer;

private byte[] GetReusableBuffer(int size)
{
    if (buffer == null || buffer.Length < size)
    {
        buffer = new byte[size];
    }
    return buffer;
}

private void DisposeBitmap(ref Bitmap? bmp)
{
    bmp?.Dispose();
    bmp = null;
}

private Bitmap? GetBitmapFromXpImage(xpImage image)
{
    if (cachedImage == image)
    {
        return cachedBitmap;
    }

    // Dispose the previous bitmap
    DisposeBitmap(ref cachedBitmap);

    // Update cache
    cachedImage = image;
    cachedBitmap = ConvertToBitmapFromXpImage(image);

    return cachedBitmap;
}









private static Bitmap ConvertToBitmapFromXpImage(xpImage image)
{
    // Extract the raw BMP data from the Adapter
    var adapter = image.GetType().GetProperty("Adapter")?.GetValue(image);
    if (adapter is Array byteArray)
    {
        byte[] rawData = new byte[byteArray.Length];
        Buffer.BlockCopy(byteArray, 0, rawData, 0, rawData.Length);

        // Create a Bitmap from the raw BMP data
        return CreateBitmapFromRawBmpData(rawData);
    }

    throw new InvalidOperationException("Failed to extract BMP data from Adapter.");
}

private static Bitmap CreateBitmapFromRawBmpData(byte[] rawData)
{
    using var ms = new MemoryStream(rawData);
    return new Bitmap(ms);
}








private static void InspectAdapter(xpImage image)
{
    var adapter = image.GetType().GetProperty("Adapter")?.GetValue(image);
    if (adapter != null)
    {
        byte[] rawData = ExtractRawDataFromAdapter(adapter);
        System.Diagnostics.Debug.WriteLine($"Raw Data Length: {rawData.Length}");
        System.Diagnostics.Debug.WriteLine($"First 16 Bytes: {string.Join(", ", rawData.Take(16))}");
        //var adapterType = adapter.GetType();
        //System.Diagnostics.Debug.WriteLine($"Adapter Type: {adapterType.FullName}");
        //foreach (var property in adapterType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
        //{
        //    System.Diagnostics.Debug.WriteLine($"Property: {property.Name}, Type: {property.PropertyType}");
        //}
        //foreach (var field in adapterType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
        //{
        //    System.Diagnostics.Debug.WriteLine($"Field: {field.Name}, Type: {field.FieldType}");
        //}
    }
    else
    {
        System.Diagnostics.Debug.WriteLine("Adapter is null.");
    }
}

private static byte[] ExtractRawDataFromAdapter(object adapter)
{
    if (adapter is Array byteArray && byteArray.GetType().GetElementType() == typeof(byte))
    {
        byte[] rawData = new byte[byteArray.Length];
        Buffer.BlockCopy(byteArray, 0, rawData, 0, rawData.Length);
        return rawData;
    }
    throw new InvalidOperationException("Adapter is not a valid byte array.");
}











        //private static void RenderRawDataToPictureBox(PictureBox pictureBox, xpImage image)
        //{
        //    var adapter = image.GetType().GetProperty("Adapter")?.GetValue(image);
        //    if (adapter is Array byteArray)
        //    {
        //        byte[] rawData = new byte[byteArray.Length];
        //        Buffer.BlockCopy(byteArray, 0, rawData, 0, rawData.Length);

//        using (var ms = new MemoryStream(rawData))
//        {
//            // Use the Graphics object of the PictureBox to render directly
//            using (var bmp = new Bitmap(ms))
//            {
//                pictureBox.Image = bmp; // Assign directly to the PictureBox
//            }
//        }
//    }
//    else
//    {
//        throw new InvalidOperationException("Failed to extract image data.");
//    }
//}
//private static void RenderRawPixelsDirectly(PictureBox pictureBox, xpImage image, int width, int height)
//{
//    var adapter = image.GetType().GetProperty("Adapter")?.GetValue(image);
//    if (adapter is Array byteArray)
//    {
//        byte[] rawData = new byte[byteArray.Length];
//        Buffer.BlockCopy(byteArray, 0, rawData, 0, rawData.Length);

//        pictureBox.Paint += (s, e) =>
//        {
//            using (var ms = new MemoryStream(rawData))
//            {
//                Bitmap bmp = new Bitmap(ms);

//                // Draw directly to the control
//                e.Graphics.DrawImage(bmp, new Rectangle(0, 0, width, height));
//            }
//        };
//        pictureBox.Invalidate(); // Force redraw
//    }
//}





//private static void RenderRawPixelsDirectly(PictureBox pictureBox, xpImage image, int width, int height)
//{
//    // Detach previous Paint event to avoid multiple handlers
//    pictureBox.Paint -= PictureBox_Paint;

//    void PictureBox_Paint(object s, PaintEventArgs e)
//    {
//        var adapter = image.GetType().GetProperty("Adapter")?.GetValue(image);
//        if (adapter is Array byteArray)
//        {
//            byte[] rawData = new byte[byteArray.Length];
//            Buffer.BlockCopy(byteArray, 0, rawData, 0, rawData.Length);

//            using (var ms = new MemoryStream(rawData))
//            using (var bmp = new Bitmap(ms)) // Ensure bitmap is disposed
//            {
//                e.Graphics.DrawImage(bmp, new Rectangle(0, 0, width, height));
//            }
//        }
//    }

//    // Attach the updated Paint event
//    pictureBox.Paint += PictureBox_Paint;
//    pictureBox.Invalidate(); // Force redraw

//private static void UpdatePictureBoxInBackground(PictureBox pictureBox, xpImage image)
//{
//    Task.Run(() =>
//    {
//        var adapter = image.GetType().GetProperty("Adapter")?.GetValue(image);
//        if (adapter is Array byteArray)
//        {
//            byte[] rawData = new byte[byteArray.Length];
//            Buffer.BlockCopy(byteArray, 0, rawData, 0, rawData.Length);

//            using (var ms = new MemoryStream(rawData))
//            {
//                Bitmap bmp = new Bitmap(ms);

//                // Update PictureBox on the UI thread
//                pictureBox.Invoke((System.Windows.Forms.MethodInvoker)(() =>
//                {
//                    pictureBox.Image?.Dispose(); // Dispose previous image
//                    pictureBox.Image = bmp;
//                }));
//            }
//        }
//    });
//}










< !--Include XPression if it is found -->
	<Choose>
		<When Condition="Exists('$(XPressionFolder)') And Exists('$(XPToolsLibFolder)')">
			<!-- Use Wildcards to find DLL File -->
			<ItemGroup>
				<XPressionFile Include="$(XPressionFolder)\**\*.dll" />
				<XPToolsLibFile Include="$(XPToolsLibFolder)\**\*.dll" />
			</ItemGroup>

			<!-- Set file path into property to use later and define constant HAS_XPRESSION -->
			<PropertyGroup>
				<XPressionPath>@(XPressionFile)</XPressionPath>
				<XPToolsLibPath>@(XPToolsLibFile)</XPToolsLibPath>
				<DefineConstants>$(DefineConstants); HAS_XPRESSION </ DefineConstants >

            </ PropertyGroup >


            < !--Import assembly reference -->
			<ItemGroup>
				<Reference Include="xpression.net">
					<HintPath>$(XPressionPath)</HintPath>
				</Reference>
				<Reference Include="xptoolslib.net">
					<HintPath>$(XPToolsLibPath)</HintPath>
				</Reference>
			</ItemGroup>
		</When>
	</Choose>

	<!-- Ensure that the DLLs are copied to the output directory (e.g., bin\Release) -->
	<Target Name="CopyXPressionDLLs" AfterTargets="Build">
		<Copy SourceFiles="$(XPressionPath)" DestinationFolder="$(OutDir)" />
		<Copy SourceFiles="$(XPToolsLibPath)" DestinationFolder="$(OutDir)" />
	</Target>






    //public ModuleLoader()
    //{
    //    LoadModules();
    //}

//public ModuleLoader()
//{
//    if (TryGetParentForm(out Form? form) && form != null)
//    {
//        form.Shown += LoadModules;
//    }
//}

//// This method checks if the form is shown
//private bool TryGetParentForm(out Form? form)
//{
//    form = null;

//    // Check if the component is assigned to a container
//    if (this.Site == null) return false;

//    // Get the parent container of the component
//    var container = this.Site.Container;

//    // Check if the container is a Form
//    if (container is not Form parentForm) return false;

//    form = parentForm;
//    return true;
//}










#if HAS_XPRESSION
using System;
using System.Collections.Generic;
using System.Threading.Channels;
using XPression;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;


namespace OperatorsSolution
{
    internal class XP_Functions
    {
        #region >----------------- XPression play scene: ---------------------
        /// <summary>
        /// Plays out the scene in XPression when this project is open in XPression.
        /// </summary>
        /// <param name = "Scene_Name">The xpScene in which the clip is located in.</param>
        /// <param name = "SD_Name">The name of the scene director.</param>
        /// <param name = "SD_Clip">The name of the clip that needs to be played.</param>
        /// <param name = "channel">The channel on which the clip needs to play.</param>
        /// <param name="layer">The layer on which the clip needs to play.</param>
        /// <returns>true if succsesfull, false if the clip could not be found</returns>
        public static bool PlaySceneState(xpScene Scene_Name, string? SD_Name, string SD_Clip, string? track, int channel = 0, int layer = 0)
        {
            // Set to be a default of the same name as its scene if it was not filled in
            if (string.IsNullOrWhiteSpace(SD_Name) || SD_Name == "Same as [Scene]")
            {
                SD_Name = Scene_Name.ToString();
            }
            if (string.IsNullOrWhiteSpace(track))
            {
                track = "StateTrack";
            }

            if (!Scene_Name.GetSceneDirectorByName(SD_Name, out xpSceneDirector Scene_Director) ||
                !Scene_Director.GetTrackByName(track, out xpSceneDirectorTrack Scene_State_Track) ||
                !Scene_State_Track.GetClipByName(SD_Clip, out xpSceneDirectorClip State_Clip))
            { return false; }

            Scene_Director.PlayRange(State_Clip.Position, State_Clip.Position);
            Scene_Name.SetOnline(channel, layer);
            Scene_Director.PlayRange(State_Clip.Position, State_Clip.Duration + State_Clip.Position, true);
            return true;
        }
        #endregion

        #region >----------------- XPression get material: ---------------------
        /// <summary>
        /// Gives the material of the specified object in a scene, given that this project is open in XPression.
        /// </summary>
        /// <param name="objectName">The name of the object (in XPression) to find the material of.</param>
        /// <param name="scene">The scene in which this material needs to be searched.</param>
        /// <returns>The IxpBaseObject as the material.</returns>
        public static IxpBaseObject getMaterialFromScene(string objectName, xpScene scene)
        {
            scene.GetObjectByName(objectName, out xpBaseObject sceneObject);

            return sceneObject;
        }
        #endregion


        #region >----------------- XPression set object: ---------------------
        public static void SetAllSceneMaterials(xpScene scene, List<ObjectChange> objectChanges)
        {
            xpEngine xpEngine = new();
            if (objectChanges.Count == 0 || xpEngine == null)
            {
                return;
            }
            
            foreach (ObjectChange objectChange in objectChanges)
            {
                SetMaterial(objectChange, scene, xpEngine);
                //if (objectChange.SceneObject == null)
                //{
                //    return;
                //}

                //if (objectChange.ObjectType == ObjectType.Text)
                //{
                //    xpTextObject textObject = (xpTextObject)XP_Functions.getMaterialFromScene(objectChange.SceneObject, scene);
                //    textObject.Text = objectChange.SetTo;
                //}
                //else if (objectChange.ObjectType == ObjectType.Material)
                //{
                //    scene.GetObjectByName(objectChange.SceneObject, out xpBaseObject baseObject);
                //    xpQuadObject quad = (xpQuadObject)baseObject;

                //    if (xpEngine.GetMaterialByName(objectChange.SetTo, out xpMaterial material))
                //    {
                //        quad.SetMaterial(0, material);
                //    }
                //}


                //if (SceneGraphic.GetObjectByName(objectChange.SceneObject, out xpBaseObject baseObject))
                //{
                //    if (baseObject is xpQuadObject quadObject)
                //    {
                //        if (XPression.GetMaterialByName(objectChange.SetTo, out xpMaterial material))
                //        {
                //            quadObject.SetMaterial(0, material);
                //        }
                //    }
                //    else if (baseObject is xpTextObject textObject)
                //    {
                //        textObject = (xpTextObject)XP_Functions.getMaterialFromScene(objectChange.SceneObject, SceneGraphic);
                //        textObject.Text = objectChange.SetTo;
                //    }
                //}
            }
        }

        public static void SetMaterial(ObjectChange objectChange, xpScene scene, xpEngine xpEngine)
        {
            // Check if any values are null
            if (objectChange.SceneObject == null)
            {
                return;
            }

            // Check what type of object it is
            if (!scene.GetObjectByName(objectChange.SceneObject, out xpBaseObject baseObject))
            {
                // Exit in case no scene object has been found
                return;
            }
            
            switch (baseObject)
            {
                case xpTextObject:
                    //xpTextObject textObject = (xpTextObject)XP_Functions.getMaterialFromScene(objectChange.SceneObject, scene);
                    xpTextObject textObject = (xpTextObject)baseObject;
                    textObject.Text = objectChange.SetTo;
                    break;
                case xpQuadObject:
                    xpQuadObject quad = (xpQuadObject)baseObject;

                    if (xpEngine.GetMaterialByName(objectChange.SetTo, out xpMaterial material))
                    {
                        quad.SetMaterial(0, material);
                    }
                    break;
                default:
                    MessageBox.Show("object type: " + baseObject.TypeName + " cannot be handled.");
                    return;
            }


            //if (objectChange.ObjectType == ObjectType.Text)
            //{
            //    xpTextObject textObject = (xpTextObject)XP_Functions.getMaterialFromScene(objectChange.SceneObject, scene);
            //    textObject.Text = objectChange.SetTo;
            //}
            //else if (objectChange.ObjectType == ObjectType.Material)
            //{
            //    scene.GetObjectByName(objectChange.SceneObject, out xpBaseObject baseObject);
            //    xpQuadObject quad = (xpQuadObject)baseObject;

            //    if (xpEngine.GetMaterialByName(objectChange.SetTo, out xpMaterial material))
            //    {
            //        quad.SetMaterial(0, material);
            //    }
            //}
        }

        // EXAMPLE FOR SETTING TEXT
        //private void button5_Click_2(object sender, EventArgs e)
        //{
        //    if (btn_BlueLT8TKO.Text == "Show Lowerthird")
        //    {
        //        if (XPression.GetSceneByName("Lowerthird", out LowerThird, true))
        //        {
        //            xpTextObject Name = (xpTextObject)XP_Functions.getMaterialFromScene("Name", LowerThird);
        //            xpTextObject Function = (xpTextObject)XP_Functions.getMaterialFromScene("Function", LowerThird);

        //            Name.Text = FighterLeft[0].FullName;
        //            Function.Text = "Fighter";

        //            if (XP_Functions.PlaySceneState(LowerThird, "Lowerthird", "in", 0, 0))
        //            {
        //                btn_BlueLT8TKO.Text = "Hide Lowerthird";
        //            }
        //        }
        //    }
        //    else if (btn_BlueLT8TKO.Text == "Hide Lowerthird")
        //    {
        //        if (XP_Functions.PlaySceneState(LowerThird, "Lowerthird", "out", 0, 0))
        //        {
        //            LowerThird.SetOffline();
        //            btn_BlueLT8TKO.Text = "Show Lowerthird";
        //        }
        //    }
        //}
        #endregion


        // EXAMPLE FOR SETTING SETTING MATERIAL
        //else if (btn_LowerRed8TKO.Text == "Next FighterEntry" && check_Lower4.Checked && status< 4)
        //    {
        //        //Get Materials for Signature
        //        xpQuadObject Signature = new xpQuadObject();

        //LowerThirdRed.GetObjectByName("SignaturePicture", out baseObject);
        //        Signature = (xpQuadObject) baseObject;

        //xpMaterial signatureMaterial = new xpMaterial();

        //        if (XPression.GetMaterialByName(FighterRight[0].SignatureMove, out signatureMaterial))
        //        {
        //            Signature.SetMaterial(0, signatureMaterial);
        //        }

        //        if (XP_Functions.PlaySceneState(LowerThirdRed, "FighterEntryRed", "out" + status.ToString(), 0, 0))
        //        {
        //            XP_Functions.PlaySceneState(LowerThirdRed, "FighterEntryRed", "in4", 0, 0);
        //            btn_LowerRed8TKO.Text = "Hide FighterEntry";
        //            status = 4;
        //        }
        //    }
    }
}
#endif

#region >----------------- Collection Classes: ---------------------
public class ClipPath()
{
    // Scene
    [Category("Search"),
    Description("Which scene this button will trigger.")]
    public string? Scene { get; set; }

    // Scene Director
    [Category("Search"),
    Description("(OPTIONAL) What scene director the clip is located in. Default: Same as [Scene]"),
    DefaultValue("Same as [Scene]")]
    public string? SceneDirector { get; set; } = "Same as [Scene]";

    // Clip
    [Category("Search"),
    Description("Which clip in this scene will trigger.")]
    public string? Clip { get; set; }

    // Track
    [Category("Search"),
    Description("(OPTIONAL) Which clip track the clip is in. Default: 'StateTrack'."),
    DefaultValue("StateTrack")]
    public string? Track { get; set; } = "StateTrack";



    // Channel
    [Category("Output"),
    Description("On what channel the clip will be displayed."),
    DefaultValue(0)]
    public int Channel { get; set; } = 0;

    // Layer
    [Category("Output"),
    Description("On what layer the clip will be displayed."),
    DefaultValue(0)]
    public int Layer { get; set; } = 0;





    // Object Changes
    [Category("Changes"),
    Description("Texts in the scene that need to be changed.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public List<ObjectChange> ObjectChanges { get; set; } = [];



    //// ButtonText
    //[Category(".Operation > Button"),
    //Description("(OPTIONAL) What text the button will change to. Default: 'Show + Same as next [Clip]'."),
    //DefaultValue("Show + Same as next [Clip]")]
    //public string? ButtonText { get; set; } = "Show + Same as next [Clip]";
}

//public enum ObjectType
//{
//    Text,
//    Material
//}

public class ObjectChange()
{
    [Category("Object Change")]
    public string? SceneObject { get; set; }

    // SET LATER TO SOMETHING FROM DATA MANAGER
    [Category("Object Change")]
    public string? SetTo { get; set; }

    //    [Category("Object Change")]
    //    [DefaultValue(ObjectType.Text)]
    //    public ObjectType ObjectType { get; set; } = ObjectType.Text;
}


// Done to dynamicly set the Scene to the previous Scene in the list
public class ClipPathCollection : Collection<ClipPath>
{
    protected override void InsertItem(int index, ClipPath item)
    {
        // If there are already items in the collection, set the new item's Scene to the last item's Scene
        if (this.Count > 0 && item.Scene == null)
        {
            item.Scene = this.Last().Scene;
        }

        base.InsertItem(index, item);
    }
}
    #endregion











    < !--Call the IncludeDll target for XPression -->
	<Target Name="IncludeXPression" DependsOnTargets="IncludeDLL">
		<PropertyGroup>
			<FolderPath>$(XPressionFolder)</FolderPath>
			<ReferenceName>xpression.net</ReferenceName>
		</PropertyGroup>
	</Target>

	<!-- Call the IncludeDll target for xpToolsLib -->
	<Target Name="IncludeXpToolsLib" DependsOnTargets="IncludeDLL">
		<PropertyGroup>
			<FolderPath>$(XpToolsLibFolder)</FolderPath>
			<ReferenceName>xpToolsLib.net</ReferenceName>
		</PropertyGroup>
	</Target>





    <Target Name="IncludeDLL" Condition="Exists('$(FolderPath)') And '$(RefernceName)' != ''">
		<!-- Use Wildcards to find DLL File -->
		<ItemGroup>
			<DllFile Include="$(FolderPath)\**\*.dll" />
		</ItemGroup>

		<!-- Set file path into property to use later and define constant HAS_ReferenceName -->
		<PropertyGroup>
			<DllPath>@(DllFile)</DllPath>
			<DefineConstants>$(DefineConstants); HAS_$(ReferenceName)</DefineConstants>
		</PropertyGroup>

		<!-- Import assembly reference -->
		<ItemGroup>
			<Reference Include="$(ReferenceName)">
				<HintPath>$(DllPath)</HintPath>
			</Reference>
		</ItemGroup>
	</Target>





    <!-- Include XPression if it is found -->
	<Choose>
		<When Condition="Exists('$(XPressionFolder)')">
			<!-- Use Wildcards to find DLL File -->
			<ItemGroup>
				<XPressionFile Include="$(XPressionFolder)\**\*.dll" />
			</ItemGroup>

			<!-- Set file path into property to use later and define constant HAS_XPRESSION -->
			<PropertyGroup>
				<XPressionPath>@(XPressionFile)</XPressionPath>
				<DefineConstants>$(DefineConstants); HAS_XPRESSION </ DefineConstants >

            </ PropertyGroup >


            < !--Import assembly reference -->
			<ItemGroup>
				<Reference Include="xpression.net">
					<HintPath>$(XPressionPath)</HintPath>
				</Reference>
			</ItemGroup>
		</When>
	</Choose>





//private void allButtonHandler_Click(object? sender, EventArgs e)
//{
//    if (sender is OperatorButton button)
//    {
//        //// Unassign allButtonHandler function, assign correct function and trigger immidiately.
//        //button.Click -= allButtonHandler_Click;
//        //button.Click += (s, args) => Btn_TriggerScene_Click(s, args);
//        //buttonHandlers.Btn_TriggerScene_Click(sender, e);
//    }
//}

// Condition="'$(DefineConstants)'=='HAS_XPRESSION'"



< PropertyGroup >

        < DefineConstants Condition = "Exists('..\..\..\..\..\..\..\..\Windows\assembly\GAC_MSIL\xpression.net\10.5.0.5508__9632b4b433765424\xpression.net.dll')

                                And Exists('..\..\..\..\..\..\..\..\Windows\assembly\GAC_MSIL\xpToolsLib.net\10.5.0.5508__28a9dbdb95a1c855\xptoolslib.net.dll')">
			$(DefineConstants); HAS_XPRESSION </ DefineConstants >

    </ PropertyGroup >


    < ItemGroup Condition = "Exists('..\..\..\..\..\..\..\..\Windows\assembly\GAC_MSIL\xpression.net\10.5.0.5508__9632b4b433765424\xpression.net.dll')" >

        < Reference Include = "xpression.net" >

            < HintPath > ..\..\..\..\..\..\..\..\Windows\assembly\GAC_MSIL\xpression.net\10.5.0.5508__9632b4b433765424\xpression.net.dll</HintPath>
		</Reference>
	</ItemGroup>

	<ItemGroup Condition="Exists('..\..\..\..\..\..\..\..\Windows\assembly\GAC_MSIL\xpToolsLib.net\10.5.0.5508__28a9dbdb95a1c855\xptoolslib.net.dll')">
		<Reference Include="xpToolsLib.net">
			<HintPath>..\..\..\..\..\..\..\..\Windows\assembly\GAC_MSIL\xpToolsLib.net\10.5.0.5508__28a9dbdb95a1c855\xptoolslib.net.dll</HintPath>
		</Reference>
	</ItemGroup>

	<PropertyGroup Condition="'$(XpressionNetPath)' != ''">
		<DefineConstants Condition="Exists($(XpressionNetPath))
								And Exists($(XpToolsLibPath))">
			$(DefineConstants); HAS_XPRESSION
        </ DefineConstants >

    </ PropertyGroup >

//// Scene
//[
//    Category(".Operation > Search"),
//    //PropertyOrder(1),
//    Description("Which scene this button will trigger.")
//]
//public string? Scene { get; set; }

    //// Scene Director
    //[
    //    Category(".Operation > Search"),
    //    //PropertyOrder(2),
    //    Description("(OPTIONAL) What scene director the clip is located in. Default: Same as [Scene]"),
    //    DefaultValue("Same as [Scene]")
    //]
    //public string? SceneDirector { get; set; }

    //// Clip
    //[
    //    Category(".Operation > Search"),
    //    //PropertyOrder(3),
    //    Description("Which clip in this scene will trigger.")
    //]
    //public string? Clip { get; set; }

    //// Track
    //[
    //    Category(".Operation > Search"),
    //    //PropertyOrder(3),
    //    Description("(OPTIONAL) Which clip track the clip is in. Default: 'StateTrack'."),
    //    DefaultValue("StateTrack")
    //]
    //public string? Track { get; set; }



    //// Channel
    //[
    //    Category(".Operation > Output"),
    //    //PropertyOrder(1),
    //    Description("On what channel the clip will be displayed."),
    //    DefaultValue(0)
    //]
    //public int Channel { get; set; }

    //// Layer
    //[
    //    Category(".Operation > Output"),
    //    //PropertyOrder(2),
    //    Description("On what layer the clip will be displayed."),
    //    DefaultValue(0)
    //]
    //public int Layer { get; set; }





    // Inside CustomTools:


    //private bool _showOtherAttribute;

    //// Property to influence the visibility of another property
    //[
    //    Category(".Operation > General"),
    //    Description("If true, BasedOnProperty1 will be visible.")
    //]
    //public bool ShowOtherAttribute
    //{
    //    get => _showOtherAttribute;
    //    set
    //    {
    //        if (_showOtherAttribute != value) // Only update if the value has changed
    //        {
    //            _showOtherAttribute = value;
    //            OnPropertyChanged(nameof(ShowOtherAttribute));
    //            OnPropertyChanged(nameof(BasedOnProperty1)); // Notify that BasedOnProperty1 may have changed
    //        }
    //    }
    //}

    //// This property will be conditionally visible based on ShowOtherAttribute
    //[Browsable(false)] // Hidden by default
    //[Category(".Operation > General")]
    //public string BasedOnProperty1
    //{
    //    get { return "This property is conditionally visible."; }
    //}


    //public event EventHandler<PropertyChangedEventArgs> PropertyChanged;

    //protected virtual void OnPropertyChanged(string propertyName)
    //{
    //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //}

    //public protected override ICustomTypeDescriptor GetTypeDescriptor(Type type)
    //{
    //    return new OperatorButtonTypeDescriptor(TypeDescriptor.GetProvider(type).GetTypeDescriptor(type), this);
    //}

    //public void OperatorButton_PropertyChanged(object sender, PropertyChangedEventArgs e)
    //{
    //    if (e.PropertyName == nameof(ShowOtherAttribute))
    //    {
    //        // Notify that BasedOnProperty1 should change visibility
    //        OnPropertyChanged(nameof(BasedOnProperty1));
    //    }
    //}

    //public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
    //{
    //    // Call the base class to get the default property collection
    //    var properties = TypeDescriptor.GetProperties(this, attributes);
    //    var propertyArray = new PropertyDescriptor[properties.Count];

    //    for (int i = 0; i < properties.Count; i++)
    //    {
    //        var property = properties[i];

    //        // Modify the visibility of BasedOnProperty1
    //        if (property.Name == nameof(BasedOnProperty1))
    //        {
    //            // Create a new BrowsablePropertyDescriptor to control visibility
    //            propertyArray[i] = new BrowsablePropertyDescriptor(property, ShowOtherAttribute);
    //        }
    //        else
    //        {
    //            propertyArray[i] = property;
    //        }
    //    }

    //    return new PropertyDescriptorCollection(propertyArray);
    //}

    ////Custom Type Descriptor to manage visibility
    //private class OperatorButtonTypeDescriptor : CustomTypeDescriptor
    //{
    //    private readonly OperatorButton _button;

    //    public OperatorButtonTypeDescriptor(ICustomTypeDescriptor parent, OperatorButton button)
    //        : base(parent)
    //    {
    //        _button = button;
    //    }

    //    public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
    //    {
    //        var properties = base.GetProperties(attributes);
    //        var propertyArray = new PropertyDescriptor[properties.Count];

    //        for (int i = 0; i < properties.Count; i++)
    //        {
    //            var property = properties[i];

    //            // Modify the visibility of BasedOnProperty1
    //            if (property.Name == nameof(OperatorButton.BasedOnProperty1))
    //            {
    //                // Set Browsable based on ShowOtherAttribute
    //                propertyArray[i] = new BrowsablePropertyDescriptor(property, _button.ShowOtherAttribute);
    //            }
    //            else
    //            {
    //                propertyArray[i] = property;
    //            }
    //        }

    //        return new PropertyDescriptorCollection(propertyArray);
    //    }
    //}

    //// Custom Property Descriptor to conditionally show/hide properties
    //private class BrowsablePropertyDescriptor : PropertyDescriptor
    //{
    //    private readonly PropertyDescriptor _baseProperty;
    //    private readonly bool _isBrowsable;

    //    public BrowsablePropertyDescriptor(PropertyDescriptor baseProperty, bool isBrowsable) : base(baseProperty)
    //    {
    //        _baseProperty = baseProperty;
    //        _isBrowsable = isBrowsable;
    //    }

    //    public override bool CanResetValue(object component) => _baseProperty.CanResetValue(component);
    //    public override object GetValue(object component) => _baseProperty.GetValue(component);
    //    public override bool IsReadOnly => _baseProperty.IsReadOnly;
    //    public override Type ComponentType => _baseProperty.ComponentType;
    //    public override bool ShouldSerializeValue(object component) => _baseProperty.ShouldSerializeValue(component);
    //    public override void ResetValue(object component) => _baseProperty.ResetValue(component);
    //    public override void SetValue(object component, object value) => _baseProperty.SetValue(component, value);
    //    public override bool IsBrowsable => _isBrowsable; // Control visibility here
    //    public override Type PropertyType => _baseProperty.PropertyType;
    //}



















    < Project Sdk = "Microsoft.NET.Sdk" >



    < PropertyGroup >

        < OutputType > WinExe </ OutputType >

        < TargetFramework > net8.0 - windows </ TargetFramework >

        < Nullable > enable </ Nullable >

        < UseWindowsForms > true </ UseWindowsForms >

        < ImplicitUsings > enable </ ImplicitUsings >


        < XpressionNetPath1 > ..\..\..\..\..\..\..\..\Windows\assembly\GAC_MSIL\xpression.net\10.5.0.5508__9632b4b433765424\xpression.net.dll</XpressionNetPath1>
		<!--<XpToolsLibPath1>..\..\..\..\..\..\..\..\Windows\assembly\GAC_MSIL\xpToolsLib.net\10.5.0.5508__28a9dbdb95a1c855\xptoolslib.net.dll</XpToolsLibPath>-->
	</PropertyGroup>


	<ItemGroup>
		<Compile Remove="!AssemblyLoader.cs" />
		<Compile Remove="OldCode.cs" />
	</ItemGroup>

	<ItemGroup>
		<None Include="!AssemblyLoader.cs" />
		<None Include="OldCode.cs" />
	</ItemGroup>

	<ItemGroup>
		<PackageReference Include="Dapper" Version="2.1.35" />
		<PackageReference Include="Emitter" Version="1.0.41" />
		<PackageReference Include="EntityFramework" Version="6.5.1" />
		<PackageReference Include="ExcelDataReader" Version="3.7.0" />
		<PackageReference Include="ExcelDataReader.DataSet" Version="3.7.0" />
		<PackageReference Include="Rug.Osc" Version="1.2.5" />
		<PackageReference Include="SQLite" Version="3.13.0" />
		<PackageReference Include="Stub.System.Data.SQLite.Core.NetFramework" Version="1.0.119" />
		<PackageReference Include="System.Data.SQLite" Version="1.0.119" />
		<PackageReference Include="System.Data.SQLite.Core" Version="1.0.119" />
		<PackageReference Include="System.Data.SQLite.EF6" Version="1.0.119" />
		<PackageReference Include="System.Data.SQLite.Linq" Version="1.0.119" />
	</ItemGroup>





	<!--Dynamicly checks for if the dll files of XPression exist and will load them if they do, and set the constant so that the code associated will be used-->
	<!-- Use Wildcards to Include DLL Files -->
	<ItemGroup>
		<XpressionNetFiles Include="..\..\..\..\..\..\..\..\Windows\assembly\GAC_MSIL\xpression.net\*\xpression.net.dll" />
		<XpToolsLibFiles Include="..\..\..\..\..\..\..\..\Windows\assembly\GAC_MSIL\xpToolsLib.net\*\xptoolslib.net.dll" />
	</ItemGroup>

	<!-- Set Properties Based on Included Files -->
	<PropertyGroup Condition="'$(XpressionNetFiles)' != '' And '$(XpToolsLibFiles)' != ''">
		<XpressionNetPath>@(XpressionNetFiles)</XpressionNetPath>
		<XpToolsLibPath>@(XpToolsLibFiles)</XpToolsLibPath>
	</PropertyGroup>


	<PropertyGroup Condition="'$(XpressionNetPath)' != ''">
		<DefineConstants>$(DefineConstants); HAS_XPRESSION </ DefineConstants >

    </ PropertyGroup >



    < ItemGroup Condition = "Exists($(XpressionNetPath))" >

        < Reference Include = "xpression.net" >

            < HintPath >$(XpressionNetPath) </ HintPath >

        </ Reference >

    </ ItemGroup >


    < ItemGroup Condition = "Exists($(XpToolsLibPath))" >

        < Reference Include = "xpToolsLib.net" >

            < HintPath >$(XpToolsLibPath) </ HintPath >

        </ Reference >

    </ ItemGroup >



    < Target Name = "Test" AfterTargets = "ResolveReferences" Condition = "$(MyVariable) != 'true'" >

        < Message Text = "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!|  @(XpressionNetFiles)  |!!!!!" Importance = "High" />

        < Message Text = "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!|  $(XpressionNetPath)  |!!!!!" Importance = "High" />

    </ Target >

</ Project >