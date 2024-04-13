using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.ComponentModel;

namespace CGFirstProject
{
    abstract class Filters
    {
        public int threshold;
        public bool chanelR, chanelG, chanelB;

        protected abstract Color GetNewPixelColor(Bitmap source, int x, int y);

        public int Clamp(int val, int min, int max) {
            if (val < min) 
                return min;
            if (val > max) 
                return max;
            
            return val;
        }

        public virtual Bitmap ProcessImage(Bitmap source, BackgroundWorker bw) {
            Bitmap res = new Bitmap(source.Width, source.Height);
            for (int i = 0; i < source.Width; i++) {
                bw.ReportProgress((int)((float)i / res.Width * 100));
                if (bw.CancellationPending) 
                    return null;
                for (int j = 0; j < source.Height; j++) {
                    res.SetPixel(i, j, GetNewPixelColor(source, i, j));
                }
            }
            return res;
        }

    }
}
