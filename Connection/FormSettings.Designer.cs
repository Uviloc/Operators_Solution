namespace OperatorsSolution
{
    partial class FormSettings
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
            components = new System.ComponentModel.Container();
            settingsGraphicsProgram = new TextBox();
            button1 = new Button();
            button2 = new Button();
            projectFileTextBox = new TextBox();
            GraphicsSoftwareOption = new ComboBox();
            textBox3 = new TextBox();
            ToolTip = new ToolTip(components);
            textBox8 = new TextBox();
            SuspendLayout();
            // 
            // settingsGraphicsProgram
            // 
            settingsGraphicsProgram.Enabled = false;
            settingsGraphicsProgram.Location = new Point(7, 12);
            settingsGraphicsProgram.Multiline = true;
            settingsGraphicsProgram.Name = "settingsGraphicsProgram";
            settingsGraphicsProgram.ReadOnly = true;
            settingsGraphicsProgram.Size = new Size(165, 33);
            settingsGraphicsProgram.TabIndex = 25;
            settingsGraphicsProgram.Text = "Graphics Program:";
            // 
            // button1
            // 
            button1.Location = new Point(449, 88);
            button1.Name = "button1";
            button1.Size = new Size(35, 31);
            button1.TabIndex = 29;
            button1.Text = "X";
            button1.UseVisualStyleBackColor = true;
            button1.Click += RemoveProjectFileRef;
            // 
            // button2
            // 
            button2.BackColor = Color.White;
            button2.ForeColor = Color.Black;
            button2.Location = new Point(7, 125);
            button2.Name = "button2";
            button2.Size = new Size(200, 34);
            button2.TabIndex = 23;
            button2.Text = "Open project";
            button2.UseVisualStyleBackColor = false;
            button2.Click += OpenProject;
            // 
            // projectFileTextBox
            // 
            projectFileTextBox.Location = new Point(171, 88);
            projectFileTextBox.Name = "projectFileTextBox";
            projectFileTextBox.PlaceholderText = "Select project file";
            projectFileTextBox.ReadOnly = true;
            projectFileTextBox.Size = new Size(283, 31);
            projectFileTextBox.TabIndex = 28;
            projectFileTextBox.Click += ProjectSelection;
            // 
            // GraphicsSoftwareOption
            // 
            GraphicsSoftwareOption.FormattingEnabled = true;
            GraphicsSoftwareOption.Items.AddRange(new object[] { "1", "2", "3" });
            GraphicsSoftwareOption.Location = new Point(171, 12);
            GraphicsSoftwareOption.Name = "GraphicsSoftwareOption";
            GraphicsSoftwareOption.Size = new Size(182, 33);
            GraphicsSoftwareOption.TabIndex = 24;
            GraphicsSoftwareOption.Text = "Graphics Program";
            GraphicsSoftwareOption.SelectionChangeCommitted += SaveGraphicsSoftwareOption;
            // 
            // textBox3
            // 
            textBox3.Enabled = false;
            textBox3.Location = new Point(7, 88);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(165, 31);
            textBox3.TabIndex = 27;
            textBox3.Text = "Project file:";
            // 
            // textBox8
            // 
            textBox8.Location = new Point(7, 204);
            textBox8.Name = "textBox8";
            textBox8.ReadOnly = true;
            textBox8.Size = new Size(200, 31);
            textBox8.TabIndex = 30;
            textBox8.Text = "Linked Database";
            textBox8.TextChanged += textBox8_TextChanged;
            // 
            // FormSettings
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            Controls.Add(textBox8);
            Controls.Add(settingsGraphicsProgram);
            Controls.Add(button1);
            Controls.Add(button2);
            Controls.Add(projectFileTextBox);
            Controls.Add(GraphicsSoftwareOption);
            Controls.Add(textBox3);
            Name = "FormSettings";
            Size = new Size(500, 264);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox settingsGraphicsProgram;
        private Button button1;
        private Button button2;
        private TextBox projectFileTextBox;
        private ComboBox GraphicsSoftwareOption;
        private TextBox textBox3;
        private ToolTip ToolTip;
        private TextBox textBox8;
    }
}
