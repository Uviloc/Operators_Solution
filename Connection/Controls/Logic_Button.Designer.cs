namespace OperatorsSolution.Controls
{
    partial class Logic_Button
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Main = new OperatorButton();
            Left = new OperatorButton();
            Right = new OperatorButton();
            SuspendLayout();
            // 
            // Main
            // 
            Main.Dock = DockStyle.Fill;
            Main.Name = "Main";
            Main.PreviewBox = null;
            Main.Scene = null;
            Main.Size = new Size(75, 23);
            Main.TabIndex = 0;
            Main.UseVisualStyleBackColor = false;
            // 
            // Left
            // 
            Left.Dock = DockStyle.Left;
            Left.Name = "Left";
            Left.PreviewBox = null;
            Left.Scene = null;
            Left.Size = new Size(75, 23);
            Left.TabIndex = 0;
            Left.UseVisualStyleBackColor = false;
            // 
            // Right
            // 
            Right.Dock = DockStyle.Right;
            Right.Name = "Right";
            Right.PreviewBox = null;
            Right.Scene = null;
            Right.Size = new Size(75, 23);
            Right.TabIndex = 0;
            Right.UseVisualStyleBackColor = false;
            // 
            // Logic_Button
            // 
            ColumnCount = 1;
            RowCount = 2;
            ResumeLayout(false);
        }

        #endregion

        private OperatorButton Main;
        private new OperatorButton Left;
        private new OperatorButton Right;
    }
}
