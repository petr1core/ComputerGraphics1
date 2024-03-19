using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace CGFirstProject
{
    internal class InverseFilter : Filters
    {
        protected override Color GetNewPixelColor(Bitmap source, int x, int y) {
            if (source == null) 
                throw new ArgumentNullException("Can't solve new pixel color! Source image is null.");
            Color sourceColor = source.GetPixel(x, y);
            Color resColor = Color.FromArgb(255 - sourceColor.R, 255 - sourceColor.G, 255 - sourceColor.B);
            return resColor;
        }
    }
}
