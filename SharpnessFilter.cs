using CGFirstProject;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGFirstProject
{
    internal class Sharpness : MatrixFilter
    {
        public Sharpness()
        {
            int sizeX = 3;
            int sizeY = 3;
            kernel = new float[sizeX, sizeY];
            kernel[0, 0] = -1; kernel[0, 1] = -1; kernel[0, 2] = -1;
            kernel[1, 0] = -1; kernel[1, 1] = 9; kernel[1, 2] = -1;
            kernel[2, 0] = -1; kernel[2, 1] = -1; kernel[2, 1] = -1;
        }
        public Sharpness(int size)
        {
            int sizeX = size;
            int sizeY = size;
            kernel = new float[sizeX, sizeY];
            kernel[0, 0] = -1; kernel[0, 1] = -1; kernel[0, 2] = -1;
            kernel[1, 0] = -1; kernel[1, 1] = 9; kernel[1, 2] = -1;
            kernel[2, 0] = -1; kernel[2, 1] = -1; kernel[2, 1] = -1;
        }
    }
}
