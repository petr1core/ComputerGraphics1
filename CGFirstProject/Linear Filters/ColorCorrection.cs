using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace CGFirstProject
{
    internal class ColorCorrection : Filters
    {
        double cRed, cGreen, cBlue;
        public ColorCorrection()
        {
            cRed = 1;
            cGreen = 1;
            cBlue = 1;
        }
        public ColorCorrection(Color source, Color res) {
            if (!source.Equals(Color.FromArgb(0, 0, 0)))
            {
                cRed = res.R / source.R;
                cGreen = res.G / source.G;
                cBlue = res.B / source.B;
            }
            else {
                cRed = 0;
                cGreen = 0;
                cBlue = 0;
            }
        }

        protected override Color GetNewPixelColor(Bitmap source, int x, int y)
        {
            Color color = source.GetPixel(x, y);
            Color res = Color.FromArgb(
                    Clamp((int)(color.R * cRed), 0, 255),
                    Clamp((int)(color.G * cGreen), 0, 255),
                    Clamp((int)(color.B * cBlue), 0, 255)
                );
            return res;
        }
    }
}
