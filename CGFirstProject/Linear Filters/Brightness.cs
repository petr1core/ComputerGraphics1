using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics.Contracts;

namespace CGFirstProject
{
    internal class Brightness : Filters
    {
        float brightness;
        //float[][] matrix;
        public Brightness() { 
            brightness = 1;
            /*float[][] _matrix =
            {
                new float[] {1,0,0,0,0}, // R
                new float[] {0,1,0,0,0}, // G
                new float[] {0,0,1,0,0}, // B
                new float[] {0,0,0,1,0}, // A
                new float[] {brightness,brightness,brightness,0,1} // W
            };
            matrix = _matrix;*/
        }

        public Brightness(int brightness_perc)
        {
            brightness = brightness_perc / 100.0f;
            /*float Contrast = 1.0f;
            float[][] matrix =
            {
                new float[] {Contrast,0,0,0,0}, // R
                new float[] {0,Contrast,0,0,0}, // G
                new float[] {0,0,Contrast,0,0}, // B
                new float[] {0,0,0,1,0}, // A
                new float[] {brightness,brightness,brightness,0,1} // W
            };*/ // Можно сделать изменение и по контрасту используя матричное преобразование
                 // (в таком случае будет изменено всё изображние, а не пиксель)
        }

        protected override Color GetNewPixelColor(Bitmap source, int x, int y)
        {
            Color color = source.GetPixel(x, y);
            Color res = Color.FromArgb(
                Clamp((int)(color.R * brightness), 0, 255),
                Clamp((int)(color.G * brightness), 0, 255),
                Clamp((int)(color.B * brightness), 0, 255)
                );
            return res;
        }
    }
}
