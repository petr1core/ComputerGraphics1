using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGFirstProject
{
    internal class WaveFilter2: Filters
    {
        public override Bitmap ProcessImage(Bitmap source, BackgroundWorker bw)
        {
            Bitmap resultImage = new Bitmap(source.Width, source.Height);

            // Применение фильтра
            for (int i = 0; i < source.Width; i++)
            {
                for (int j = 0; j < source.Height; j++)
                {
                    // Calculate rotated coordinates
                    double rotatedX = i + 20 * Math.Sin(2 * Math.PI * i / 30);
                    double rotatedY = j;

                    // Round to the nearest integer to get the nearest neighbor
                    int nearestX = (int)Math.Round(rotatedX);
                    int nearestY = (int)Math.Round(rotatedY);

                    // Check if the new position is within the image bounds
                    if (nearestX >= 0 && nearestX < source.Width && nearestY >= 0 && nearestY < source.Height)
                    {
                        resultImage.SetPixel(i, j, source.GetPixel(nearestX, nearestY));
                    }
                    else
                    {
                        // Set pixels outside the bounds to black
                        resultImage.SetPixel(i, j, Color.Black);
                    }
                }

                bw.ReportProgress((int)((float)i / source.Width * 100));
                if (bw.CancellationPending)
                    return null;
            }

            return resultImage;
        }

        protected override Color GetNewPixelColor(Bitmap source, int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
