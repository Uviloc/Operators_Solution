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
            Hello = new Button();
            World = new Button();
            SuspendLayout();
            // 
            // Hello
            // 
            Hello.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Hello.AutoSize = true;
            Hello.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Hello.BackColor = SystemColors.ControlDarkDark;
            Hello.Name = "Hello";
            Hello.Size = new Size(100, 23);
            Hello.TabIndex = 0;
            Hello.Text = "Hello";
            Hello.UseVisualStyleBackColor = false;
            // 
            // World
            // 
            World.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            World.AutoSize = true;
            World.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            World.BackColor = SystemColors.ActiveCaption;
            World.Name = "World";
            World.Size = new Size(75, 50);
            World.TabIndex = 0;
            World.Text = "World";
            World.UseVisualStyleBackColor = false;
            ResumeLayout(false);
        }

        #endregion

        private Button Hello;
        private Button World;
    }
}
