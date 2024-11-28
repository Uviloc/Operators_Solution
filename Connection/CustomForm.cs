using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Console = System.Diagnostics.Debug;

namespace OperatorsSolution
{
    public class CustomForm : Form
    {
        #region >----------------- Add properties: ---------------------
        // Panel
        [Category(".Operation")]
        [Description("The panel control where the loaded forms will be displayed.")]
        [Browsable(true)]
        public TabControl? ContentsPanel { get; set; }

        // Operation TreeView
        [Category(".Operation")]
        [Description("The treeview control where the forms will be loaded into.")]
        [Browsable(true)]
        public TreeView? OperationTreeview { get; set; }

        // Data TreeView
        [Category(".Operation")]
        [Description("The treeview control where the databases will be loaded into.")]
        [Browsable(true)]
        public TreeView? DatabaseTreeview { get; set; }

        // Control Panel
        [Category(".Operation")]
        [Description("The control panel containing the OperationTreeview and menu buttons.")]
        [Browsable(true)]
        public Panel? ControlPanel { get; set; }

        // Tab Control
        [Category(".Operation")]
        [Description("The tab control that has the main program tabs in.")]
        [Browsable(true)]
        public TabControl? TabControl { get; set; }

        // Data Viewer
        [Category(".Operation")]
        [Description("The DataGridView that the databases can be loaded into.")]
        [Browsable(true)]
        public DataGridView? DataViewer { get; set; }
        #endregion

        // SHOULD MAYBE HAVE SETTINGS CONTROL HERE SO THAT EXTERNAL FORMS CAN USE THOSE FUNCTIONS EASILY
    }
}
