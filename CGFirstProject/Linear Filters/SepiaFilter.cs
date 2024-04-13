using CGFirstProject;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGFirstProject
{
    internal class SepiaFilter : Filters
    {
        protected override Color GetNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            byte k = 8;
            Color sourceColor = sourceImage.GetPixel(x, y);
            int intensity = (int)(0.36 * sourceColor.R + 0.53 * sourceColor.G + 0.11 * sourceColor.B);
            Color resultColor = Color.FromArgb(Clamp(intensity + 2 * k, 0, 255),
                                               Clamp((int)(intensity + 0.5 * k), 0, 255),
                                               Clamp(intensity - k, 0, 255));
            return resultColor;
        }
    }
}