using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
varables:
- widget reference if xpression


Module has own timer build in, if widget is found it can fully influence it.
OR has own timer but can be bypassed


Time display: taken from widget if xpression, own timer as default
Start,stop,add,subtract to widget if xpression, to own timer as default

Radio buttons have variable for how far in match it starts and what text would be?


*/


namespace OperatorsSolution.Controls
{
    public partial class Timer_Module : UserControl
    {
        public Timer_Module()
        {
            InitializeComponent();
        }
    }
}
