using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGFirstProject
{
    class BlurFilter : MatrixFilter
    {
        public BlurFilter(int radius) {
            //int sizeX = radius;
            //int sizeY = radius;
            if (radius < 2) throw new ArgumentException("Radius equals zero");
            int size = radius;
            kernel = new float[size, size];
            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    kernel[i,j] = 1.0f / (float)(size * size);
                }
            }
        }
    }
}
