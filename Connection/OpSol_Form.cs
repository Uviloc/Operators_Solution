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
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OperatorsSolution
{
    public partial class OpSol_Form : CustomForm
    {
        public OpSol_Form()
        {
            InitializeComponent();
            InitializeSettings();
            InitializeForms();
            InitializeCollapseControlPanelTimer();
            HideTabBar();

            if (TreeviewExplorer != null)
                ModuleLoader.LoadModules(TreeviewExplorer);
        }

        private void InitializeForms()
        {
            if (ContentsPanel == null) return;
            Form form = new DataBaseForm();
            OpenFormInPanel(form, ContentsPanel.TabPages[1]);
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
            controlPanelChanging = true;
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

            if (e.Node.Tag is Common.IModuleForm pluginForm)
            {
                // Get the form from the IModuleForm instance
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
        //private static void ScaleFormToFitPanel(Form form, Panel panel)
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
        //private static void ScaleFormToFitPanel(object? formModulePanel)
        //{
        //    if (formModulePanel is TabControl tabControl)
        //        formModulePanel = tabControl.SelectedTab;

        //    if (formModulePanel is not Panel panel || panel.Tag is not Form form)
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


        //private readonly List<Form> formHistory = [];

        //private void SaveFormToHistory(Form form)
        //{
        //    if (formHistory.Count > 10) formHistory.Remove(formHistory.Last());
        //    formHistory.Prepend(form);
        //}

        //private Form? GetLastFromFormHistory(string formType) //                 CHANGE TO TYPE INSTEAD OF STRING
        //{
        //    Form? form = formHistory.Find(match: x => x.Tag?.ToString() == formType);
        //    return form;
        //}


        private void TabChange(object? sender, EventArgs e)
        {
            if (sender is not TabControl tabControl) return;
            if (ContentsPanel == null) return;

            ContentsPanel.SelectedIndex = tabControl.SelectedIndex;

            //if (tabs.SelectedTab is not TabPage tab) return;



            //switch (tabControl.SelectedIndex)
            //{
            //    case 0: // Operation Tab

            //        //if (tab.Tag == null)
            //        //{
            //        //    tab.Tag = OpenFormInPanel(, ContentsPanel);
            //        //}
            //        //else
            //        //{
            //        //    OpenFormInPanel((Form)tab.Tag, ContentsPanel);
            //        //}
            //        break;
            //    case 1: // Database Tab


            //        Form form = new DataBaseForm();
            //        //if (tab.Tag == null)
            //        //{
            //        //    tab.Tag = OpenFormInPanel(form, ContentsPanel);
            //        //} else
            //        //{
            //        //    OpenFormInPanel((Form)tab.Tag, ContentsPanel);
            //        //}
            //        //tabs.SelectedTab.Tag = OpenFormInPanel(form, ContentsPanel);
            //        //ContentsPanel.Visible = false;
            //        break;
            //    case 2: // Settings Tab
            //        ContentsPanel.Visible = true;
            //        break;
            //    default:
            //        ContentsPanel.Visible = true;
            //        break;
            //}
        }

        private void HideTabBar()
        {
            if (ContentsPanel == null) return;
            ContentsPanel.Appearance = TabAppearance.FlatButtons;
            ContentsPanel.ItemSize = new Size(0, 1);
            ContentsPanel.SizeMode = TabSizeMode.Fixed;
        }
    }
}