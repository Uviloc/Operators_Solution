using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Threading.Channels;
using System.Reflection;
using OperatorsSolution.Program;
using OperatorsSolution.Common;
using System.Data.Common;
using System.Data.SQLite;
using System.Data;
using Console = System.Diagnostics.Debug;
using System.Windows.Forms;
using Dapper;
using Emitter;
using System.Xml.Linq;
using OperatorsSolution.Controls;

namespace OperatorsSolution
{
    public partial class OpSol_Form : BaseForm
    {
        #region >----------------- Main Process: ---------------------
        public OpSol_Form()
        {
            InitializeComponent();
            GraphicsSoftwareRegistry.InitializeGraphicsPrograms();
            InitializeCollapseControlPanelTimer();
            HideTabBar();

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

        #region >----------------- OpenForm: ---------------------
        private void OpenExternalForm(object? sender, TreeNodeMouseClickEventArgs e)
        {
            if (ContentsPanel == null || TabControl == null) return;

            if (e.Node.Tag is not Common.IFormPlugin pluginForm) return;
            // Get the form from the IFormPlugin instance
            Form form = pluginForm.GetForm();

            // Store form in Operation tab to use for scaling and open when set.
            OpenFormInPanel(form, ContentsPanel.SelectedTab);
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

        #region >----------------- Plugin Form Settings: ---------------------
        private void DisplayNodeButton(object? sender, MouseEventArgs e)
        {
            //if (sender is not TreeView treeView || treeView.Parent?.Controls is not Control.ControlCollection controls) return;
            if (sender is not TreeView treeView) return;

            // Get the newly entered node:
            if (treeView.GetNodeAt(e.Location) is not TreeNode newlyEnteredNode || newlyEnteredNode.Nodes.Count > 0) return;

            // Get the button of the previous node:
            var tag = treeView.Tag;

            if (tag is Button lastEnteredButton)
            {
                // if we did not change node > exit:
                if (lastEnteredButton.Tag == newlyEnteredNode) return;

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
                Text = "...",
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


        private void OpenFormSettings(object? sender, EventArgs e)
        {
            if (sender is not Button button) return;
            if (button.Tag is not TreeNode node) return;
            if (node.Tag is not Common.IFormPlugin pluginForm) return;

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


        #region >----------------- Control Tab stuff: ---------------------
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
            if (ContentsPanel == null) return;

            if (e.Node.Tag is string dbPath)
            {
                string connectionString = $"Data Source={dbPath};Version=3;";
                SQLiteConnection connection = DBFunctions.InitializeConnection(connectionString);

                // Clear any previous controls
                ContentsPanel.SelectedTab?.Controls.Clear();

                TabControl tabControl = new()
                {
                    Dock = DockStyle.Fill,
                    Appearance = TabAppearance.FlatButtons,
                };

                ContentsPanel.SelectedTab?.Controls.Add(tabControl);

                DBFunctions.LoadData(tabControl, connection);
            }
        }
        #endregion

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
                Program.Settings.Default.Test = textBox.Text;
            Program.Settings.Default.Save();
        }
    }
}