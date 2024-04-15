using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGFirstProject
{
    internal class Erosion : MorphologyFilter
    {
        public Erosion() : base() { }
        public Erosion(bool[,] kernel, int threshold) : base(kernel, threshold){}

        protected override Color GetNewPixelColor(Bitmap source, int _x, int _y)
        {
            if (kernel == null) return source.GetPixel(_x, _y);
            int min = 255;
            for (int x = -height / 2; x < height / 2; x++) { 
                for (int y = -width / 2; y < width / 2; y++)
                {
                    if (kernel[y + width/2,x+height/2] && (source.GetPixel(Clamp(_x + y, 0, source.Width - 1), Clamp(_y + x, 0, source.Width - 1)).R < min))
                    {
                        min = source.GetPixel(_x + y, _y + x).R;    
                    }
                }
            }
            return Color.FromArgb(min, min, min);
        }
    }
}
