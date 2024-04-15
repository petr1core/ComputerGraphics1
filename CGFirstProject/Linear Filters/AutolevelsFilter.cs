using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CGFirstProject
{
    internal class Autolevels : Filters
    {
        int Rmin = 255, Rmax = 0;
        int Gmin = 255, Gmax = 0;
        int Bmin = 255, Bmax = 0;

        public Autolevels() { }

        public Autolevels(Bitmap source) {
            for (int i = 0; i < source.Width; i++)
            {
                for (int j = 0; j < source.Height; j++)
                {
                    Color color = source.GetPixel(i, j);
                    if (Rmin > color.R) Rmin = color.R;
                    if (Rmax < color.R) Rmax = color.R;
                    if (Gmin > color.G) Gmin = color.G;
                    if (Gmax < color.G) Gmax = color.G;
                    if (Bmin > color.B) Bmin = color.B;
                    if (Bmax < color.B) Bmax = color.B;
                }
            }
        }

        protected override Color GetNewPixelColor(Bitmap source, int x, int y)
        {
            Color color = source.GetPixel(x, y);
            Color res = Color.FromArgb(
                    Clamp((color.R - Rmin) * 255 / (Rmax - Rmin), 0, 255),
                    Clamp((color.G - Gmin) * 255 / (Gmax - Gmin), 0, 255),
                    Clamp((color.B - Bmin) * 255 / (Bmax - Bmin), 0, 255)
                );
            return res;
        }
    }
}
