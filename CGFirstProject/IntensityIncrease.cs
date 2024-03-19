using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace CGFirstProject
{
    internal class IntensityIncrease : Filters
    {
        public IntensityIncrease(int _threshold, bool _chanelR, bool _chanelG, bool _chanelB)
        {
            threshold = _threshold;
            chanelR = _chanelR;
            chanelG = _chanelG;
            chanelB = _chanelB;
        }

        protected override Color GetNewPixelColor(Bitmap source, int x, int y)
        {
            Color color = source.GetPixel(x, y);
            int c = threshold;
            int red = color.R, green = color.G, blue = color.B;
            if (chanelR)
                red = (red + c) % 255;
            if (chanelG)
                green = (green + c) % 255;
            if (chanelB)
                blue = (blue + c) % 255;
            Color res = Color.FromArgb(red, green, blue);
            return res;
        }
    }
}
