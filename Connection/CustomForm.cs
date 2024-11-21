using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace OperatorsSolution
{
    public class CustomForm : Form
    {
        #region >----------------- Add properties: ---------------------
        // Panel
        [Category(".Operation")]
        [Description("The panel control where the loaded forms will be displayed.")]
        [Browsable(true)]
        public Panel? FormModulePanel { get; set; }

        // TreeView
        [Category(".Operation")]
        [Description("The treeview control where the forms will be loaded into.")]
        [Browsable(true)]
        public TreeView? TreeviewExplorer { get; set; }

        // Control Panel
        [Category(".Operation")]
        [Description("The control panel containing the TreeviewExplorer and menu buttons.")]
        [Browsable(true)]
        public Panel? ControlPanel { get; set; }
        #endregion

        // SHOULD MAYBE HAVE SETTINGS CONTROL HERE SO THAT EXTERNAL FORMS CAN USE THOSE FUNCTIONS EASILY

#if False // Custom buttons with borderless window. Not needed honestly
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
#endif
    }
}
