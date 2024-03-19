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
            if (val < min) return min;
            if (val > max) return max;
            return val;
        }

        public Bitmap ProcessImage(Bitmap source, BackgroundWorker bw) {
            Bitmap res = new Bitmap(source.Width, source.Height);
            for (int i = 0; i < source.Width; i++) {
                bw.ReportProgress((int)((float)i / res.Width * 100));
                if (bw.CancellationPending) return null;
                for (int j = 0; j < source.Height; j++) {
                    res.SetPixel(i, j, GetNewPixelColor(source, i, j));
                }
            }
            return res;
        }

    }
   /* public Color newPixel(Bitmap image, int x, int y)
    {
        Color color = image.GetPixel(x, y);
        Color res = new Color();
        if (inverseToolStripMenuItem.Checked)
        {
            res = Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B);
        }
        if (channelIntensityToolStripMenuItem.Checked)
        {
            int c = Int32.Parse(label2.Text);
            int red = color.R, green = color.G, blue = color.B;
            if (CRed.Checked)
                red = (red + c) % 255;
            if (CGreen.Checked)
                green = (green + c) % 255;
            if (CBlue.Checked)
                blue = (blue + c) % 255;
            res = Color.FromArgb(red, green, blue);
        }
        if (binarizationToolStripMenuItem.Checked)
        {
            int t = trackBar1.Value;
            byte gray = (byte)(0.21 * color.R + 0.71 * color.G + 0.071 * color.B);
            if (gray > t)
                res = Color.FromArgb(255, 255, 255);
            else
                res = Color.FromArgb(0, 0, 0);
        }
        if (grayShadesToolStripMenuItem.Checked)
        {
            byte gray = (byte)(0.21 * color.R + 0.71 * color.G + 0.071 * color.B);
            res = Color.FromArgb(gray, gray, gray);
        }
        if (!inverseToolStripMenuItem.Checked && !channelIntensityToolStripMenuItem.Checked &&
            !binarizationToolStripMenuItem.Checked && !grayShadesToolStripMenuItem.Checked)
        {
            return color;
        }
        return res;
    }

    public Bitmap Changeimage()
    {
        if (MyImage != null)
        {
            Bitmap res = new Bitmap(MyImage.Width, MyImage.Height);
            for (int i = 0; i < sX; i++)
            {
                for (int j = 0; j < sY; j++)
                {
                    res.SetPixel(i, j, newPixel(MyImage, i, j));
                }
            }
            return res;
        }
        return null;
    }*/

}
