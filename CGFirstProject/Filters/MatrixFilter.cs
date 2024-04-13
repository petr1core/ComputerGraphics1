using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGFirstProject
{
    class MatrixFilter : Filters
    {
        protected float[,] kernel = null;
        protected MatrixFilter() { }
        public MatrixFilter(float[,] _kernel)
        {
            kernel= _kernel;    
        }
        protected override Color GetNewPixelColor(Bitmap source, int x, int y) { 
            int radiusX = kernel.GetLength(0)/2;
            int radiusY = kernel.GetLength(1)/2;
            float resR = 0;
            float resG = 0;
            float resB = 0;
            for (int i = -radiusY; i < radiusY; i++) {
                for (int j = -radiusX; j < radiusX; j++) {
                    int idX = Clamp(x + j, 0, source.Width - 1);
                    int idY = Clamp(y + i, 0, source.Height - 1);
                    Color nearby = source.GetPixel(idX, idY);
                    resR += nearby.R * kernel[j + radiusX, i + radiusY];
                    resG += nearby.G * kernel[j + radiusX, i + radiusY];
                    resB += nearby.B * kernel[j + radiusX, i + radiusY];
                }
            }
            return Color.FromArgb(
                Clamp((int)resR, 0, 255),
                Clamp((int)resG, 0, 255),
                Clamp((int)resB, 0, 255)
                );
        }
    }
}
