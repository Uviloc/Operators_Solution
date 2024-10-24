using OperatorsSolution;

namespace OperatorsSolution
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
            TreeNode treeNode4 = new TreeNode("Node2");
            TreeNode treeNode5 = new TreeNode("Node1", new TreeNode[] { treeNode4 });
            TreeNode treeNode6 = new TreeNode("Folder 1", new TreeNode[] { treeNode5 });
            GraphicsSoftwareOption = new ComboBox();
            button1 = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            treeView1 = new TreeView();
            SuspendLayout();
            // 
            // GraphicsSoftwareOption
            // 
            GraphicsSoftwareOption.FormattingEnabled = true;
            GraphicsSoftwareOption.Items.AddRange(new object[] { "1", "2", "3" });
            GraphicsSoftwareOption.Location = new Point(183, 12);
            GraphicsSoftwareOption.Name = "GraphicsSoftwareOption";
            GraphicsSoftwareOption.Size = new Size(182, 33);
            GraphicsSoftwareOption.TabIndex = 0;
            GraphicsSoftwareOption.Text = "Graphics Program";
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
            // textBox2
            // 
            textBox2.Location = new Point(150, 213);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(359, 175);
            textBox2.TabIndex = 4;
            textBox2.Text = "Other settings to include:\r\n? Visual style (Dark mode etc)\r\n? Keyboard shortcuts\r\nPreview on Hover";
            // 
            // treeView1
            // 
            treeView1.Location = new Point(37, 452);
            treeView1.Name = "treeView1";
            treeNode4.Name = "Node2";
            treeNode4.Text = "Node2";
            treeNode5.Name = "Node1";
            treeNode5.Text = "Node1";
            treeNode6.Name = "Node0";
            treeNode6.Text = "Folder 1";
            treeView1.Nodes.AddRange(new TreeNode[] { treeNode6 });
            treeView1.Size = new Size(174, 238);
            treeView1.TabIndex = 5;
            // 
            // Settings
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(50, 50, 50);
            ClientSize = new Size(772, 713);
            Controls.Add(treeView1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Controls.Add(GraphicsSoftwareOption);
            ForeColor = SystemColors.ControlText;
            Name = "Settings";
            ShowIcon = false;
            Text = "Settings";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox GraphicsSoftwareOption;
        private Button button1;
        private TextBox textBox1;
        private TextBox textBox2;
        private TreeView treeView1;
    }
}