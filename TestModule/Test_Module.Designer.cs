using OperatorsSolution.Controls;

namespace TestModule
{
    partial class Test_Module
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
            PreviewBox = new PictureBox();
            logic_Button3 = new Logic_Button();
            logic_Button4 = new Logic_Button();
            operatorButton9 = new OperatorButton();
            operatorButton10 = new OperatorButton();
            operatorButton11 = new OperatorButton();
            ((System.ComponentModel.ISupportInitialize)PreviewBox).BeginInit();
            SuspendLayout();
            // 
            // PreviewBox
            // 
            PreviewBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PreviewBox.Location = new Point(30, 693);
            PreviewBox.Name = "PreviewBox";
            PreviewBox.Size = new Size(653, 389);
            PreviewBox.TabIndex = 9;
            PreviewBox.TabStop = false;
            // 
            // logic_Button3
            // 
            logic_Button3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            logic_Button3.Location = new Point(30, 25);
            logic_Button3.Name = "logic_Button3";
            logic_Button3.Size = new Size(297, 223);
            logic_Button3.TabIndex = 10;
            logic_Button3.Text = "logic_Button3";
            // 
            // logic_Button4
            // 
            logic_Button4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            logic_Button4.Location = new Point(560, 449);
            logic_Button4.Name = "logic_Button4";
            logic_Button4.Size = new Size(297, 223);
            logic_Button4.TabIndex = 10;
            logic_Button4.Text = "logic_Button3";
            // 
            // operatorButton9
            // 
            operatorButton9.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            operatorButton9.Location = new Point(651, 25);
            operatorButton9.Name = "operatorButton9";
            operatorButton9.PreviewBox = null;
            operatorButton9.Scene = null;
            operatorButton9.Size = new Size(193, 105);
            operatorButton9.TabIndex = 11;
            operatorButton9.UseVisualStyleBackColor = false;
            // 
            // operatorButton10
            // 
            operatorButton10.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            operatorButton10.Location = new Point(651, 143);
            operatorButton10.Name = "operatorButton10";
            operatorButton10.PreviewBox = null;
            operatorButton10.Scene = null;
            operatorButton10.Size = new Size(193, 105);
            operatorButton10.TabIndex = 11;
            operatorButton10.UseVisualStyleBackColor = false;
            // 
            // operatorButton11
            // 
            operatorButton11.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            operatorButton11.Location = new Point(30, 567);
            operatorButton11.Name = "operatorButton11";
            operatorButton11.PreviewBox = null;
            operatorButton11.Scene = null;
            operatorButton11.Size = new Size(193, 105);
            operatorButton11.TabIndex = 11;
            operatorButton11.UseVisualStyleBackColor = false;
            // 
            // Test_Module
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1479, 1109);
            Controls.Add(operatorButton11);
            Controls.Add(operatorButton10);
            Controls.Add(operatorButton9);
            Controls.Add(logic_Button4);
            Controls.Add(logic_Button3);
            Controls.Add(PreviewBox);
            Name = "Test_Module";
            Text = "Test_Module";
            ((System.ComponentModel.ISupportInitialize)PreviewBox).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private PictureBox PreviewBox;
        private OperatorButton operatorButton1;
        private OperatorButton operatorButton2;
        private Logic_Button logic_Button1;
        private OperatorButton operatorButton3;
        private OperatorButton operatorButton4;
        private OperatorButton operatorButton5;
        private OperatorButton operatorButton6;
        private OperatorButton operatorButton7;
        private OperatorButton operatorButton8;
        private Logic_Button logic_Button2;
        private Logic_Button logic_Button3;
        private Logic_Button logic_Button4;
        private OperatorButton operatorButton9;
        private OperatorButton operatorButton10;
        private OperatorButton operatorButton11;
    }
}