using CGFirstProject;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGFirstProject
{
    internal class SobelFilter : Filters
    {
        float[,] kernelX = { { -1.0f, -2.0f, -1.0f }, { 0f, 0f, 0f }, { -1.0f, -2.0f, -1.0f } };
        float[,] kernelY = { { -1.0f, 0f, 1.0f }, { -2.0f, 0f, 2.0f }, { -1.0f, 0f, 1.0f } };
        protected override Color GetNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            float resultR = 0;
            float resultG = 0;
            float resultB = 0;
            float RX = 0, GX = 0, BX = 0, RY = 0, GY = 0, BY = 0;

            for (int l = -1; l <= 1; l++)
            {
                for (int k = -1; k <= 1; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    RX += neighborColor.R * kernelX[k + 1, l + 1];
                    GX += neighborColor.G * kernelX[k + 1, l + 1];
                    BX += neighborColor.B * kernelX[k + 1, l + 1];
                }
            }
            for (int l = -1; l <= 1; l++)
            {
                for (int k = -1; k <= 1; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    RY += neighborColor.R * kernelY[k + 1, l + 1];
                    GY += neighborColor.G * kernelY[k + 1, l + 1];
                    BY += neighborColor.B * kernelY[k + 1, l + 1];
                }
            }
            resultR = (float)Math.Sqrt(RX * RX + RY * RY);
            resultG = (float)Math.Sqrt(GX * GX + GY * GY);
            resultB = (float)Math.Sqrt(BX * BX + BY * BY);
            return Color.FromArgb(
                Clamp((int)resultR, 0, 255),
                Clamp((int)resultG, 0, 255),
                Clamp((int)resultB, 0, 255)
                );
        }
    }
}


