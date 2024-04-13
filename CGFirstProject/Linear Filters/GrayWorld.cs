using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CGFirstProject
{
    internal class GrayWorld : Filters
    {
        double cRed;
        double cGreen;
        double cBlue;

        public GrayWorld() {
             cRed = 1;
             cGreen = 1;
             cBlue = 1;
        }

        public GrayWorld(Bitmap source)
        {
            int count = source.Width * source.Height;
            double mid = 0;

            int Rsum = 0;
            int Gsum = 0;
            int Bsum = 0;
            for (int i = 0;i < source.Width; i++)
            {
                for (int j = 0;j < source.Height; j++)
                {
                    Color color = source.GetPixel(i,j);
                    Rsum += color.R;
                    Gsum += color.G;
                    Bsum += color.B;
                }
            }
            mid = (((double)Rsum / count) + ((double)Gsum / count) + ((double)Bsum / count)) / 3;

            cRed = mid / ((double)Rsum / count);
            cGreen = mid / ((double)Gsum / count);
            cBlue = mid / ((double)Bsum / count);
        }

        protected override Color GetNewPixelColor(Bitmap source, int x, int y)
        {
            Color color = source.GetPixel(x, y);
            Color res = Color.FromArgb(
                    Clamp((int)(color.R * cRed), 0, 255),
                    Clamp((int)(color.R * cRed), 0, 255),
                    Clamp((int)(color.R * cRed), 0, 255)
                );
            return res;
        }
    }
}
