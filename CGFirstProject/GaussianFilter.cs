using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGFirstProject
{
    class GaussianFilter : MatrixFilter
    {
        public GaussianFilter(int radius) { 
            createGaussianKernel(radius, 2);
        }
        public GaussianFilter() {
            createGaussianKernel(3, 2);
        }
        public void createGaussianKernel(int radius, float sigma) {
            if (radius == 0) throw new ArgumentException("radius equals zero");
            int size = 2 * radius + 1; // kernel size
            kernel = new float[size,size];
            float normal = 0; // normaliztion coefficient
            for (int i = -radius; i < radius; i++) { 
                for (int j = -radius; j < radius; j++)
                {
                    kernel[i+radius,j+radius] = (float)(Math.Exp(-(i*i+j*j) / (sigma*sigma)));
                    normal += kernel[i + radius, j + radius];
                }
            }
            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++)
                {
                    kernel[i, j] /= normal;
                }
            }
        }

    }
}
