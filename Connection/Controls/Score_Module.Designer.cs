namespace OperatorsSolution.Controls
{
    partial class Score_Module
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
            groupBox1 = new GroupBox();
            button5 = new Button();
            button4 = new Button();
            button3 = new Button();
            label1 = new Label();
            button2 = new Button();
            button1 = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button5);
            groupBox1.Controls.Add(button4);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(button1);
            groupBox1.Location = new Point(33, 19);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(477, 482);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Manual Score";
            // 
            // button5
            // 
            button5.Location = new Point(285, 406);
            button5.Name = "button5";
            button5.Size = new Size(174, 61);
            button5.TabIndex = 3;
            button5.Text = "Reset Score";
            button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(361, 265);
            button4.Name = "button4";
            button4.Size = new Size(64, 45);
            button4.TabIndex = 2;
            button4.Text = "-1";
            button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(21, 265);
            button3.Name = "button3";
            button3.Size = new Size(64, 45);
            button3.TabIndex = 2;
            button3.Text = "-1";
            button3.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(159, 118);
            label1.Name = "label1";
            label1.Size = new Size(125, 65);
            label1.TabIndex = 1;
            label1.Text = "1 - 1";
            // 
            // button2
            // 
            button2.Location = new Point(306, 76);
            button2.Name = "button2";
            button2.Size = new Size(119, 170);
            button2.TabIndex = 0;
            button2.Text = "+1 Team B";
            button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(13, 76);
            button1.Name = "button1";
            button1.Size = new Size(119, 170);
            button1.TabIndex = 0;
            button1.Text = "+1 Team A";
            button1.UseVisualStyleBackColor = true;
            // 
            // Score_Module
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox1);
            Name = "Score_Module";
            Size = new Size(517, 512);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button button5;
        private Button button4;
        private Button button3;
        private Label label1;
        private Button button2;
        private Button button1;
    }
}
