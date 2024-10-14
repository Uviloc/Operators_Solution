using ExcelDataReader;
using System;
using System.Net;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using Rug.Osc;
using XPression;
using System.Linq.Expressions;
using System.Diagnostics.CodeAnalysis;



namespace OperatorsSolution
{
    public partial class OpSol_Form : Form
    {
        public OpSol_Form()
        {
            InitializeComponent();
        }


        private xpEngine XPression = new();
        public void Btn_TriggerScene_Click(object? sender, EventArgs e)
        {
            if (sender is OperatorButton button)
            {
                string? scene = button.Scene;
                string? clip = button.Clip;
                string? track = button.Track;
                int channel = button.Channel = 0;
                int layer = button.Layer = 0;


                if (string.IsNullOrWhiteSpace(scene) ||
                    string.IsNullOrWhiteSpace(clip))
                {
                    MessageBox.Show("Warning: Scene on button: " + button.Text + " must be set!");
                    return;
                }

                if (XPression.GetSceneByName(scene, out xpScene SceneGraphic, true))
                {
                    XPConnections.PlaySceneState(SceneGraphic, scene, clip, track, channel, layer);
                }
                else
                {
                    MessageBox.Show("Warning: Scene on button: " + button.Text + " could not be found!");
                }
            }
        }
    }
}