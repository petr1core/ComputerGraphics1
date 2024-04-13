using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGFirstProject
{
    internal class GlassFilter : Filters
    {
        public override Bitmap ProcessImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            for (int i = 0; i < sourceImage.Width; i++)
            {
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Random rand = new Random();
                    double rotatedX = i + (rand.NextDouble() - 0.5) * 10;
                    double rotatedY = j + (rand.NextDouble() - 0.5) * 10;

                    int nearestX = (int)Math.Round(rotatedX);
                    int nearestY = (int)Math.Round(rotatedY);

                    if (nearestX >= 0 && nearestX < sourceImage.Width && nearestY >= 0 && nearestY < sourceImage.Height)
                    {
                        resultImage.SetPixel(i, j, sourceImage.GetPixel(nearestX, nearestY));
                    }
                    else
                    {
                        resultImage.SetPixel(i, j, Color.Black);
                    }
                }

                worker.ReportProgress((int)((float)i / sourceImage.Width * 100));
                if (worker.CancellationPending)
                    return null;
            }

            return resultImage;
        }

        protected override Color GetNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
