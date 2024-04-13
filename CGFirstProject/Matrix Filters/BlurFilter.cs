using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGFirstProject
{
    class BlurFilter : MatrixFilter
    {
        public BlurFilter()
        {
            kernel = new float[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    kernel[i, j] = 1.0f / (float)(3 * 3);
                }
            }
        }
        public BlurFilter(int radius) {
            if (radius < 2) throw new ArgumentException("Radius equals zero");
            kernel = new float[radius, radius];
            for (int i = 0; i < radius; i++) {
                for (int j = 0; j < radius; j++) {
                    kernel[i,j] = 1.0f / (float)(radius * radius);
                }
            }
        }
    }
}
