using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CGFirstProject
{
    internal class GrayShades : Filters
    {
        protected override Color GetNewPixelColor(Bitmap source, int x, int y)
        {
            Color color = source.GetPixel(x, y);
            byte gray = (byte)(0.21 * color.R + 0.71 * color.G + 0.071 * color.B);
            Color res = Color.FromArgb(gray, gray, gray);
            return res;
        }
    }
}
