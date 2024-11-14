using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OperatorsSolution.Controls
{
    public partial class Logic_Button : Button
    {
        
        
        public Button? button1;
        public Button? button2;

        public string? LogicName { get; set; }


        public Logic_Button()
        {
            InitializeComponent();
            SetupChildControls();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private void SetupChildControls()
        {
            this.Controls.Add(Hello);
            this.Controls.Add(World);

            //// Initialize button1
            //button1 = new Button
            //{
            //    Text = "Button 1",
            //    Location = new Point(10, 10)
            //};

            //// Initialize button2
            //button2 = new Button
            //{
            //    Text = "Button 2",
            //    Location = new Point(10, 50)
            //};

            //// Add buttons to the control's Controls collection
            //Controls.Add(button1);
            //Controls.Add(button2);
        }
    }
}
