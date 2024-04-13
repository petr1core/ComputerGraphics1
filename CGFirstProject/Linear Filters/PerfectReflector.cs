using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CGFirstProject
{
    internal class PerfectReflector : Filters
    {
        double cRed, cGreen, cBlue;

        public PerfectReflector() {
            cRed = 0;
            cGreen = 0; 
            cBlue = 0;
        }

        public PerfectReflector(Bitmap source) {
            int maxRed = 0;
            int maxGreen = 0;
            int maxBlue = 0;

            for (int i = 0; i < source.Width; i++) { 
                for (int j = 0; j < source.Height; j++)
                {
                    Color color = source.GetPixel(i, j);

                    if (maxRed < color.R) 
                        maxRed = color.R;

                    if (maxGreen < color.G)
                        maxGreen = color.G;

                    if (maxBlue < color.B)
                        maxBlue = color.B;
                }
            }
            cRed = 255.0d / maxRed; 
            cGreen = 255.0d / maxGreen; 
            cBlue = 255.0d / maxBlue;
        }
        protected override Color GetNewPixelColor(Bitmap source, int x, int y) { 
            Color color = source.GetPixel(x, y);
            Color res = Color.FromArgb
                (
                Clamp((int)(color.R * cRed), 0, 255),
                Clamp((int)(color.G * cGreen), 0, 255),
                Clamp((int)(color.B * cBlue), 0, 255)
                );
            return res;
        }
    }
}
