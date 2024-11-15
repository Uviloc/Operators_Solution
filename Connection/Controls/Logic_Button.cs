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
    public partial class Logic_Button : TableLayoutPanel
    {
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
            this.Controls.Add(Main);
            this.Controls.Add(Left);
            this.Controls.Add(Right);
        }
    }
}
