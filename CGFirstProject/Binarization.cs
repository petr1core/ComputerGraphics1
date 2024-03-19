using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CGFirstProject
{
    internal class Binarization : Filters
    {
        public Binarization(int _threshold)
        {
            threshold = _threshold;
            chanelR = false;
            chanelG = false;
            chanelB = false;
        }
        protected override Color GetNewPixelColor(Bitmap source, int x, int y)
        {
            if (source == null) throw new ArgumentNullException("Can't solve new pixel color! Source image is null.");
            Color color = source.GetPixel(x, y);
            int t = threshold; // threshold .. порог
            byte gray = (byte)(0.21 * color.R + 0.71 * color.G + 0.071 * color.B);
            return (gray > t) ? Color.FromArgb(255, 255, 255) : Color.FromArgb(0, 0, 0);
        }
    }
}
