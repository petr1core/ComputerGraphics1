using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CGFirstProject
{
    internal class MotionBlur:MatrixFilter
    {
        public MotionBlur()
        {
            int sizeX = 7;
            int sizeY = 7;

            kernel = new float[sizeX, sizeY];
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    if (i != j)
                    {
                        kernel[i, j] = 0.0f;
                    }
                    else
                    {
                        kernel[i, j] = 1.0f / sizeX;
                    }
                }
            }
        }
    }
}
