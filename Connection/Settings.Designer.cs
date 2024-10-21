using OperatorsSolution;

namespace Operators_Solution
{
    partial class Settings
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            comboBox1 = new ComboBox();
            button1 = new Button();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "XPression", "CasparCG", "VMix" });
            comboBox1.Location = new Point(183, 12);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(182, 33);
            comboBox1.TabIndex = 0;
            comboBox1.Text = "Graphics Program";
            // 
            // button1
            // 
            button1.Location = new Point(599, 643);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 1;
            button1.Text = "OK";
            button1.UseVisualStyleBackColor = true;
            button1.Click += BtnOK_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(19, 12);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(165, 33);
            textBox1.TabIndex = 3;
            textBox1.Text = "Graphics Program:";
            // 
            // Settings
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(50, 50, 50);
            ClientSize = new Size(772, 713);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Controls.Add(comboBox1);
            ForeColor = SystemColors.ControlText;
            Name = "Settings";
            ShowIcon = false;
            Text = "Settings";
            TopMost = true;
            Load += Settings_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBox1;
        private Button button1;
        private TextBox textBox1;
    }
}