using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGFirstProject
{
    internal class Move: Filters
    {
        protected int moveX, moveY;
        public Move()
        {
          moveX = 50; // left by 50
          moveY = 50; // up   by 50
        }
        public Move(int x, int y) {
            moveX = x;
            moveY = y;
        }

        public override Bitmap ProcessImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            if (sourceImage == null) throw new ArgumentException("Move: sourse null");
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            // Применение фильтра
            for (int i = 0; i < sourceImage.Width; i++)
            {
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    // Check if the new position is within the image bounds
                    int newX = i + moveX;
                    int newY = j + moveY;

                    if (newX >= 0 && newX < sourceImage.Width && newY >= 0 && newY < sourceImage.Height)
                    {
                        resultImage.SetPixel(i, j, sourceImage.GetPixel(newX, newY));
                    }
                    else
                    {
                        // Set pixels outside the bounds to black
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
