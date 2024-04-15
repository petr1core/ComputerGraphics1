using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CGFirstProject
{
    abstract class MorphologyFilter : Filters
    {
        protected int height, width;
        protected bool[,] kernel;
        protected int mTreshold;
        public MorphologyFilter() {
            kernel = null;
            mTreshold = 0;
            width = 0;
            height = 0;
        }

        public MorphologyFilter(bool[,] kernel, int threshold)
        {
            this.mTreshold = threshold;
            this.kernel = kernel;
            this.height = kernel.GetLength(1);
            this.width = kernel.GetLength(0);
        }

        public override Bitmap ProcessImage(Bitmap source, BackgroundWorker bw)
        {
            if (kernel == null) return source;
            Bitmap binImg = new Binarization(mTreshold).ProcessImage(source, bw);
            Bitmap res = new Bitmap(source.Width, source.Height);
            for (int x = height/ 2; x < binImg.Height- height/ 2; x++)
            {
                bw.ReportProgress((int)((float)x / res.Height * 100));
                if (bw.CancellationPending) 
                    return null;
                for (int y = width/ 2; y < binImg.Width - width/ 2; y++) {
                    res.SetPixel(y,x, GetNewPixelColor(binImg, y, x));
                }
            }
            return res;
        }
    }
}
