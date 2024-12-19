using OperatorsSolution.Common;
using OperatorsSolution.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatorsSolution.Graphics_Program_Functions
{
    internal class CasparCG : IGraphicProgram
    {
        public GraphicsSoftwareInfo GraphicsSoftwareInfo => new("CasparCG", "CasparCG", "CasparCG files ()|");

        public static void DisplayPreview(object? sender, PictureBox previewBox)
        {
            throw new NotImplementedException();
        }

        public static void RemovePreview(PictureBox previewBox)
        {
            throw new NotImplementedException();
        }

        public static void TriggerClip(OperatorButton operatorButton, int clipIndex)
        {
            throw new NotImplementedException();
        }
    }
}
