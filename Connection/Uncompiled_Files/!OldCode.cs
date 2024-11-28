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