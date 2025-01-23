using System;
using OperatorsSolution.Common;
using System.Data.SQLite;
using System.Data;
using Console = System.Diagnostics.Debug;
using System.Windows.Forms;
using OperatorsSolution.Controls;

namespace OperatorsSolution
{
    /// <summary>
    /// Main form for the Operators Solution application.
    /// Manages UI components, database interactions, and plugin loading.
    /// </summary>
    public partial class OpSol_Form : MainBaseForm
    {
        #region >----------------- Main Process: ---------------------
        /// <summary>
        /// Initializes the main form, sets up the UI, and loads plugins.
        /// </summary>
        public OpSol_Form()
        {
            InitializeComponent();
            //CollapseControlPanel1.Enter += CommonFunctions.ButtonHighlight;

            // Find all the registered Graphics Programs in the program
            GraphicsSoftwareRegistry.InitializeGraphicsPrograms();

            // Create and assign a timer for collapsing the side panel
            InitializeCollapseControlPanelTimer();

            // Hide tab bar from inner panel as these are controlled by side tabs
            HideTabBar();

            // Load in the nodes for both the database and form treeviews
            if (OperationTreeview != null && DatabaseTreeview != null)
                PluginLoader.LoadPlugins(OperationTreeview, DatabaseTreeview);
            else
                MessageBox.Show("Please set the OperationTreeview and DatabaseTreeview variables in the main form property window");
        }
        #endregion

        #region >----------------- Collapse Control Panel: ---------------------
        private bool controlPanelOpen = true;
        private bool controlPanelChanging = false;
        private readonly float animationDuration = 0.3f;
        private System.Windows.Forms.Timer? controlPanelAnimationTimer;

        /// <summary>
        /// Initializes the timer used for animating the collapse/expand of the control panel.
        /// </summary>
        private void InitializeCollapseControlPanelTimer()
        {
            if (ControlPanel == null)
                return;

            controlPanelAnimationTimer = new System.Windows.Forms.Timer { Interval = 1 };
            controlPanelAnimationTimer.Tick += ControlPanelTimer_Tick;
        }

        /// <summary>
        /// Toggles the control panel's collapsed state and updates the toggle button text.
        /// </summary>
        private void CollapseControlPanel(object? sender, EventArgs e)
        {
            if (ControlPanel == null || controlPanelAnimationTimer == null)
                return;

            controlPanelAnimationTimer.Start();
            controlPanelChanging = true; // Prevent constant scaling of inner form

            if (sender is Button collapseButton)
                collapseButton.Text = controlPanelOpen ? ">" : "<";
        }

        /// <summary>
        /// Handles the animation for collapsing or expanding the control panel.
        /// </summary>
        private void ControlPanelTimer_Tick(object? sender, EventArgs e)
        {
            if (ControlPanel == null || controlPanelAnimationTimer == null)
                return;

            int animationSpeed = (int)Math.Round(15 / animationDuration);
            ControlPanel.Width += controlPanelOpen ? -animationSpeed : animationSpeed;

            // Stop when the panel reaches its target size
            if ((controlPanelOpen && ControlPanel.Width <= ControlPanel.MinimumSize.Width) ||
                (!controlPanelOpen && ControlPanel.Width >= ControlPanel.MaximumSize.Width))
            {
                controlPanelOpen = !controlPanelOpen;
                controlPanelChanging = false;
                controlPanelAnimationTimer.Stop();
                ScaleFormToFitPanel(ContentsPanel);
            }
        }
        #endregion

        #region >----------------- OpenForm: ---------------------
        /// <summary>
        /// Opens a form corresponding to the selected node in the tree view.
        /// </summary>
        private void OpenExternalForm(object? sender, TreeNodeMouseClickEventArgs e)
        {
            if (ContentsPanel == null || TabControl == null)
                return;

            if (e.Node.Tag is not Common.IFormPlugin pluginForm)
                return;

            // Get the form from the IFormPlugin instance
            Form form = pluginForm.GetForm();

            // Store form in Operation tab to use for scaling and open when set.
            OpenFormInPanel(form, ContentsPanel.SelectedTab);
        }

        /// <summary>
        /// Embeds a form into the specified container.
        /// </summary>
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

        #region >----------------- Plugin Form Settings: ---------------------
        /// <summary>
        /// Displays a settings button for a tree node on mouse enter.
        /// </summary>
        private void DisplayNodeButton(object? sender, MouseEventArgs e)
        {
            if (sender is not TreeView treeView)
                return;

            // Get the newly entered node:
            if (treeView.GetNodeAt(e.Location) is not TreeNode newlyEnteredNode ||
                newlyEnteredNode.Nodes.Count > 0)
                return;

            // Get the button of the previous node:
            var tag = treeView.Tag;

            if (tag is Button lastEnteredButton)
            {
                // if we did not change node > exit:
                if (lastEnteredButton.Tag == newlyEnteredNode)
                    return;

                // Remove the button as a new one will be made:
                treeView.Parent?.Controls.Remove(lastEnteredButton);
                lastEnteredButton.Dispose();
            }

            // Create new button:
            Button dotMenu = new()
            {
                Bounds = newlyEnteredNode.Bounds,
                Width = newlyEnteredNode.Bounds.Height,
                Left = treeView.Width - 100,
                Text = "...",                                                                   // REPLACE WITH GEAR ICON SPINNING
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(255, 0, 0, 0),
                FlatStyle = FlatStyle.Flat,
                Tag = newlyEnteredNode,
            };

            dotMenu.FlatAppearance.BorderSize = 0;

            dotMenu.MouseEnter += CommonFunctions.ButtonHighlight;
            dotMenu.Click += OpenFormSettings;

            treeView.Parent?.Controls.Add(dotMenu);
            treeView.Tag = dotMenu;

            dotMenu.BringToFront();
        }

        /// <summary>
        /// Opens a settings panel for the selected tree node.
        /// </summary>
        private void OpenFormSettings(object? sender, EventArgs e)
        {
            if (sender is not Button button ||
                button.Tag is not TreeNode node ||
                node.Tag is not Common.IFormPlugin pluginForm)
                return;

            // Create backdrop:
            CustomPanel backPanel = new()
            {
                Size = this.Size,
                BackColor = Color.FromArgb(100, 20, 20, 20),
            };
            this.Controls.Add(backPanel);
            backPanel.BringToFront();

            // Create a settings panel:
            FormSettings settingsPopup = new()
            {
                Location = this.PointToClient(Cursor.Position),
                LinkedForm = pluginForm,
            };
            settingsPopup.InitializeSettings();

            this.Controls.Add(settingsPopup);
            settingsPopup.BringToFront();

            backPanel.MouseClick += (s, e) =>
            {
                settingsPopup.Hide();
                backPanel.Hide();
                settingsPopup.Dispose();
                backPanel.Dispose();
            };
        }
        #endregion

        #region >----------------- Scale Form: ---------------------
        /// <summary>
        /// Scales a form to fit within a specified panel, preserving the aspect ratio.
        /// </summary>
        private static void ScaleFormToFitPanel(object? formModulePanel)
        {
            if (formModulePanel is TabControl tabControl) formModulePanel = tabControl.SelectedTab;
            if (formModulePanel is not Panel panel || panel.Tag is not Form form)
                return;

            // Check if the form's original size is already stored
            if (form.Tag is not Size originalSize)
            {
                // Store the original size of the form in its Tag property
                form.Tag = form.Size;
                originalSize = form.Size;

                // Store the original size and location of each control
                foreach (Control control in form.Controls)
                    control.Tag = (control.Size, control.Location);
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

            // Reset the form to its original size
            form.Size = originalSize;

            // Calculate scaling factors for width and height
            float scaleX = (float)panel.Width / originalSize.Width;
            float scaleY = (float)panel.Height / originalSize.Height;

            // Use non-cumulative scaling
            form.Scale(new SizeF(scaleX, scaleY));
        }

        /// <summary>
        /// Handles resizing of the form module panel.
        /// </summary>
        private void FormModulePanel_SizeChanged(object? sender, EventArgs e)
        {
            if (controlPanelChanging)
                return;
            ScaleFormToFitPanel(sender);
        }
        #endregion

        #region >----------------- Control Tab stuff: ---------------------
        /// <summary>
        /// Synchronizes the content panel with the selected tab in the tab control.
        /// </summary>
        private void TabChange(object? sender, EventArgs e)
        {
            if (sender is not TabControl tabControl || ContentsPanel == null)
                return;

            // If its the settings tab, do not execute auto switch
            if (tabControl.SelectedIndex == 2)
                return;

            ContentsPanel.SelectedIndex = tabControl.SelectedIndex;
        }

        /// <summary>
        /// Hides the tab bar of the content panel by adjusting its appearance and size settings.
        /// </summary>
        private void HideTabBar()
        {
            if (ContentsPanel == null)
                return;

            ContentsPanel.Appearance = TabAppearance.FlatButtons;
            ContentsPanel.ItemSize = new Size(0, 1);
            ContentsPanel.SizeMode = TabSizeMode.Fixed;
        }
        #endregion

        #region >----------------- Database stuff: ---------------------
        /// <summary>
        /// Opens a database and populates the selected content panel tab with data.
        /// </summary>
        private void OpenDatabase(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (ContentsPanel == null)
                return;

            if (e.Node.Tag is not string dbPath)
                return;

            string connectionString = $"Data Source={dbPath};Version=3;";
            SQLiteConnection connection = DBFunctions.InitializeConnection(connectionString);

            // Clear any previous controls in the selected tab
            ContentsPanel.SelectedTab?.Controls.Clear();

            // Create a new tab control for displaying database data
            TabControl tabControl = new()
            {
                Dock = DockStyle.Fill,
                Appearance = TabAppearance.FlatButtons,
            };

            // Add the tab control to the selected tab
            ContentsPanel.SelectedTab?.Controls.Add(tabControl);

            // Load data from the database into the tab control
            DBFunctions.LoadData(tabControl, connection);
        }
        #endregion
    }
}