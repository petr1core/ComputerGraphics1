using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using System.Drawing.Imaging;
using Encoder = System.Drawing.Imaging.Encoder;
using System.Drawing.Text;
using WinFormsApp1;

namespace CGFirstProject
{
    
    public partial class MyFIlters : Form
    {
        private struct Current {
            public ToolStripMenuItem item;
            public Filters filter;

            public Current(ToolStripMenuItem i, Filters f) {
                item = i;
                filter = f;
            }

            public void SetAll(ToolStripMenuItem i, Filters f) {
                item = i;
                filter = f;
            }
            public void SetItem(ToolStripMenuItem i){ item = i; }
            public void SetFilter(Filters f) { filter = f; }
            public ToolStripMenuItem GetItem() {  return item; }
            public Filters GetFilter() { return filter; }

        }

        Current cursor = new Current();
        private Bitmap initialImg;
        private Bitmap changedImg;
        string savePath;


        public MyFIlters()
        {
            InitializeComponent();
        }
        public void AddImage(String path)
        {
            if (initialImg != null)
                initialImg.Dispose();
            savePath = path;

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            initialImg = new Bitmap(path);
            pictureBox1.Image = initialImg;
        }
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            String path = listBox1.GetItemText(listBox1.SelectedItem);
            textBox1.Text = path;
            savePath = path;

            AddImage(path);
        }
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            label2.Text = trackBar1.Value.ToString();
        }
        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            label3.Text = trackBar2.Value.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            String path = textBox1.Text;
            if (path != "")
            {
                if (File.Exists(path))
                {
                    if (!listBox1.Items.Contains(path))
                        listBox1.Items.Add(path);
                    AddImage(path);
                    savePath = path;
                    label1.Text = "";
                }
                else
                {
                    label1.Text = "Wrong path!";
                    label1.ForeColor = Color.Red;
                }
            }
            else textBox1.Text = "Error, path is missing!";

        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (cursor.item == null) 
                pictureBox1.Image = initialImg;

            if (cursor.item == channelsToolStripMenuItem)
                cursor.SetFilter(new IntensityIncrease(Int32.Parse(label2.Text), CRed.Checked, CGreen.Checked, CBlue.Checked));
            
            if (cursor.item == binarizationToolStripMenuItem1)
                cursor.SetFilter(new Binarization(trackBar1.Value));

            if (cursor.item == moveToolStripMenuItem)
                cursor.SetFilter(new Move(Int32.Parse(label2.Text), Int32.Parse(label3.Text)));

            if (cursor.item == rotateToolStripMenuItem)
                cursor.SetFilter(new Rotate((float)Int32.Parse(label2.Text)));

            if (cursor.item == perfectReflectorToolStripMenuItem) {
                cursor.SetFilter(new PerfectReflector(initialImg));
            }

            if (cursor.item == blurSimpleToolStripMenuItem)
            {
                try { cursor.SetFilter(new BlurFilter(trackBar1.Value)); }
                catch (ArgumentException) { Console.WriteLine("BlurSimple arg exception" + trackBar1.Value); }
            }

            if (colorCorrectionToolStripMenuItem.Checked) {
                Color inputColor = Color.FromArgb(ColCorSelectedPB.BackColor.ToArgb());
                Color resColor = Color.FromArgb(ColCorResPB.BackColor.ToArgb());
                cursor.SetFilter(new ColorCorrection(inputColor, resColor));
            }

            if (grayWorldToolStripMenuItem.Checked) {
                cursor.SetFilter(new GrayWorld(initialImg));
            }

            try { 
                backgroundWorker1.RunWorkerAsync(cursor.filter);
            }
            catch (ArgumentException) { Console.WriteLine("BackgroundWorker exception"); }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }

        /// Mute/unmute ToolStrip, Hide/unhide UI elements ///
        private void InverseSelection(ToolStripMenuItem ts) {
            foreach (ToolStripMenuItem i in ts.DropDownItems) { 
                if (i.HasDropDownItems) InverseSelection(i);
                else i.Checked = false;
            }
        }
        private void CheckChoosen(ToolStripMenuItem ts, ToolStripMenuItem sender)
        {
            foreach (ToolStripMenuItem i in ts.DropDownItems)
            {
                InverseSelection(i);
            }
            sender.Checked = !sender.Checked;
        }
        private void HideNeedless() {
            ColCorResPB.Visible = false;
            ColCorResL.Visible = false;
            ColCorRedL.Visible = false;
            ColCorGreenL.Visible = false;
            ColCorBlueL.Visible = false;
            ColCorRedTB.Visible = false;
            ColCorGreenTB.Visible = false;
            ColCorBlueTB.Visible = false;
            ColCorSelectedPB.Visible = false;
            
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            trackBar1.Visible = false;
            trackBar2.Visible = false;
            CRed.Visible = false;
            CBlue.Visible = false;
            CGreen.Visible = false;
            CRed.Visible = false;
            CBlue.Visible = false;
            CGreen.Visible = false;
        }

        /// Save/Load functions ///
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    AddImage(dlg.FileName);
                    textBox1.Text = dlg.FileName;
                    if (!listBox1.Items.Contains(dlg.FileName))
                        listBox1.Items.Add(dlg.FileName);
                }
            }
        }
        private void SavePngToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (initialImg != null)
            {
                String[] s = savePath.Split('\\');
                String dir = "";
                for (int i = 0; i < s.Length - 1; i++)
                {
                    dir = dir + s[i] + @"\";
                }
                String g = DateTime.Now.ToString("G").Replace(':', '.').Replace(' ', ',');
                changedImg.Save(dir + g + " " + s[s.Length - 1]);
            }
        }
        private void loadFormTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open text file";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    String[] list = File.ReadAllLines(dlg.FileName);

                    foreach (String s in list) {
                        listBox1.Items.Add(s);
                    }
                    textBox1.Text = list[list.Length-1];
                    AddImage(list[list.Length - 1]);
                }
            }
            
        }

        /// BG worker ///
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Bitmap newImg = ((Filters)e.Argument).ProcessImage(initialImg, backgroundWorker1);
            if (backgroundWorker1.CancellationPending != true)
                changedImg = newImg;
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (e.Cancelled)
            {
                pictureBox1.Image = initialImg;
                pictureBox1.Refresh();
            }
            else {
                pictureBox1.Image = changedImg;
                pictureBox1.Refresh();
            }
            progressBar1.Value = 0;
        }
        private void CancelButton_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
            
        }

        /// Tool strip menu Items ///
        private void inverseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CheckChoosen(filtersToolStripMenuItem, inverseToolStripMenuItem1);
            cursor = new Current(filtersToolStripMenuItem, new InverseFilter());
            HideNeedless();
        }
        private void grayShadesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CheckChoosen(filtersToolStripMenuItem, grayShadesToolStripMenuItem1);
            cursor = new Current(grayShadesToolStripMenuItem1, new GrayShades());
            HideNeedless();
        }
        private void binarizationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CheckChoosen(filtersToolStripMenuItem, binarizationToolStripMenuItem1);
            cursor = new Current(binarizationToolStripMenuItem1, new Binarization());
            HideNeedless();
            if (binarizationToolStripMenuItem1.Checked)
            {
                label2.Visible = true;
                trackBar1.Visible = true;
                trackBar1.Maximum = 255;
                trackBar1.Minimum = 0;
                trackBar1.Value = 0;
                label3.Text = trackBar1.Value.ToString();
            }
        }
        private void channelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckChoosen(filtersToolStripMenuItem, channelsToolStripMenuItem);
            cursor = new Current(channelsToolStripMenuItem, new IntensityIncrease());
            HideNeedless();
            if (channelsToolStripMenuItem.Checked)
            {
                trackBar1.Visible = true;
                label2.Visible = true;
                trackBar1.Maximum = 255;
                trackBar1.Minimum = 0;
                trackBar1.Value = 0;
                CRed.Visible = true;
                CBlue.Visible = true;
                CGreen.Visible = true;
                label3.Text = trackBar1.Value.ToString();
            }
        }
        private void blurSimpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckChoosen(filtersToolStripMenuItem, blurSimpleToolStripMenuItem);
            cursor = new Current(blurSimpleToolStripMenuItem, new BlurFilter());
            HideNeedless();
            if (blurSimpleToolStripMenuItem.Checked)
            {
                trackBar1.Visible = true;
                label2.Visible = true;
                trackBar1.Maximum = 9;
                trackBar1.Minimum = 0;
                trackBar1.Value = 0;
                label3.Text = trackBar1.Value.ToString();
            }
        }
        private void blurGaussianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckChoosen(filtersToolStripMenuItem, blurGaussianToolStripMenuItem);
            cursor = new Current(blurGaussianToolStripMenuItem, new GaussianFilter());
            HideNeedless();
            if (blurGaussianToolStripMenuItem.Checked)
            {
                trackBar1.Visible = true;
                label2.Visible = true;
                trackBar1.Maximum = 9;
                trackBar1.Minimum = 0;
                trackBar1.Value = 0;
                label3.Text = trackBar1.Value.ToString();
            }
        }
        private void sharpenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CheckChoosen(filtersToolStripMenuItem, sharpenToolStripMenuItem1);
            cursor = new Current(sharpenToolStripMenuItem1, new Sharpness());
            HideNeedless();
        }
        private void sobelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckChoosen(filtersToolStripMenuItem, sobelToolStripMenuItem);
            cursor = new Current(sobelToolStripMenuItem, new SobelFilter());
            HideNeedless();
            if (sobelToolStripMenuItem.Checked) { 
                label2.Visible = true;
                trackBar1.Visible = true;
                trackBar1.Maximum = 9;
                trackBar1.Minimum = 0;
                trackBar1.Value = 0;
                label3.Text= trackBar1.Value.ToString();
            }
        }
        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckChoosen(filtersToolStripMenuItem, sepiaToolStripMenuItem);
            cursor = new Current(sepiaToolStripMenuItem, new SepiaFilter());
            HideNeedless();
        }
        private void glassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckChoosen(filtersToolStripMenuItem, glassToolStripMenuItem);
            cursor = new Current(glassToolStripMenuItem, new GlassFilter());
            HideNeedless();
        }
        private void moveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckChoosen(filtersToolStripMenuItem, moveToolStripMenuItem);
            cursor = new Current(moveToolStripMenuItem, new Move());
            HideNeedless();
            if (moveToolStripMenuItem.Checked) {
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                trackBar2.Visible = true;
                trackBar1.Visible = true;
                trackBar1.Maximum = 1000;
                trackBar2.Maximum = 1000;
                trackBar1.Minimum = -1000;
                trackBar2.Minimum = -1000;
                trackBar1.Value = 0;
                trackBar2.Value = 0;
                label2.Text = trackBar2.Value.ToString();
                label3.Text = trackBar1.Value.ToString();
            }
        }
        private void rotateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckChoosen(filtersToolStripMenuItem, rotateToolStripMenuItem);
            cursor = new Current(rotateToolStripMenuItem, new Rotate());
            HideNeedless();
            if (rotateToolStripMenuItem.Checked) {
                label2.Visible = true;
                trackBar1.Visible = true;
                trackBar1.Minimum = 0;
                trackBar1.Maximum = 360;
                trackBar1.Value = 0;
                label3.Text = trackBar1.Value.ToString();
            }
        }
        private void wave1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckChoosen(filtersToolStripMenuItem, wave1ToolStripMenuItem);
            cursor = new Current(wave1ToolStripMenuItem, new WaveFilter1());
            HideNeedless();
        }
        private void wave2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckChoosen(filtersToolStripMenuItem, wave2ToolStripMenuItem);
            cursor = new Current(wave2ToolStripMenuItem, new WaveFilter2());
            HideNeedless();
        }
        private void medianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckChoosen(filtersToolStripMenuItem, medianToolStripMenuItem);
            cursor = new Current(medianToolStripMenuItem, new MedianFilter());
            HideNeedless();
        }
        private void motionBlurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckChoosen(filtersToolStripMenuItem, motionBlurToolStripMenuItem);
            cursor = new Current(motionBlurToolStripMenuItem, new MotionBlur());
            HideNeedless();
        }
        private void pruitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckChoosen(filtersToolStripMenuItem, pruitToolStripMenuItem);
            cursor = new Current(pruitToolStripMenuItem, new PruitFilter());
            HideNeedless();
        }
        private void embossingToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CheckChoosen(filtersToolStripMenuItem, embossingToolStripMenuItem);
            cursor = new Current(embossingToolStripMenuItem, new EmbossingFilter());
            HideNeedless();
        }

        private void perfectReflectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckChoosen(filtersToolStripMenuItem, perfectReflectorToolStripMenuItem);
            cursor = new Current(perfectReflectorToolStripMenuItem, new PerfectReflector());
            HideNeedless();
        }
        private void colorCorrectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckChoosen(filtersToolStripMenuItem, colorCorrectionToolStripMenuItem);
            cursor = new Current(perfectReflectorToolStripMenuItem, new ColorCorrection());
            HideNeedless();
            if (colorCorrectionToolStripMenuItem.Checked) {
                ColCorResPB.Visible = true;
                ColCorSelectedPB.Visible = true;
                ColCorResPB.Visible = true;
                ColCorResL.Visible = true;
                ColCorRedL.Visible = true;
                ColCorGreenL.Visible = true;
                ColCorBlueL.Visible = true;
                ColCorRedTB.Visible = true;
                ColCorGreenTB.Visible = true;
                ColCorBlueTB.Visible = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.CancellationPending || !backgroundWorker1.IsBusy)
            {
                Point curs = pictureBox1.PointToClient(Cursor.Position);
                if (pictureBox1.Image != null)
                {
                    float scaleX = (float)initialImg.Width / pictureBox1.Width;
                    float scaleY = (float)initialImg.Height / pictureBox1.Height;

                    int mPosX = (int)(curs.X * scaleX);
                    int mPosY = (int)(curs.Y * scaleY);

                    bool InCorners = (mPosX >= 0 && mPosX < initialImg.Width
                                                 && mPosY >= 0
                                                 && mPosY < initialImg.Height
                                                 );
                    if (ColCorSelectedPB.Visible && InCorners)
                    {
                        Console.WriteLine("Mouse position {0}, {1}", mPosX, mPosY);
                        Console.WriteLine(
                            "Pixel color: {0} {1} {2}",
                            initialImg.GetPixel(mPosX, mPosY).R,
                            initialImg.GetPixel(mPosX, mPosY).G,
                            initialImg.GetPixel(mPosX, mPosY).B
                            );
                        ColCorSelectedPB.BackColor = Color.FromArgb(initialImg.GetPixel(mPosX, mPosY).ToArgb());
                    }
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (backgroundWorker1.CancellationPending || !backgroundWorker1.IsBusy) {
                Point curs = pictureBox1.PointToClient(Cursor.Position);
                if (pictureBox1.Image != null)
                {
                    float scaleX = (float)initialImg.Width / pictureBox1.Width;
                    float scaleY = (float)initialImg.Height / pictureBox1.Height;

                    int mPosX = (int)(curs.X * scaleX);
                    int mPosY = (int)(curs.Y * scaleY);

                    bool InCorners = (mPosX >= 0 && mPosX < initialImg.Width
                                                 && mPosY >= 0
                                                 && mPosY < initialImg.Height
                                                 );
                    if (SelectPB.Visible && InCorners)
                    {
                        if (pictureBox1.ClientRectangle.Contains(curs))
                        {
                            Bitmap bmp = (Bitmap)pictureBox1.Image;
                            SelectPB.BackColor = Color.FromArgb(bmp.GetPixel(mPosX, mPosY).ToArgb());
                        }
                    }
                }
            }
        }

        private void ColCorRedTB_TextChanged(object sender, EventArgs e)
        {
            int green = 255;
            int blue = 255;
            if (!ColCorBlueTB.Text.Equals(""))
            {
                if (int.TryParse(ColCorBlueTB.Text, out int bVal))
                {
                    blue = bVal;
                }
            }
            if (!ColCorGreenTB.Text.Equals("")) {
                if (int.TryParse(ColCorGreenTB.Text, out int grVal)){
                    green = grVal;
                }
            }
            if (int.TryParse(ColCorRedTB.Text, out int rVal))
            {
                ColCorRedTB.Text = rVal.ToString();

                Color newColor = Color.FromArgb(rVal % 255, green % 255, blue % 255);
                ColCorResPB.BackColor = newColor;
            }
        }

        private void ColCorGreenTB_TextChanged(object sender, EventArgs e)
        {
            int red = 255;
            int blue = 255;
            if (!ColCorBlueTB.Text.Equals(""))
            {
                if (int.TryParse(ColCorBlueTB.Text, out int bVal))
                {
                    blue = bVal;
                }
            }
            if (!ColCorRedTB.Text.Equals(""))
            {
                if (int.TryParse(ColCorRedTB.Text, out int rVal))
                {
                    red = rVal;
                }
            }
            if (int.TryParse(ColCorGreenTB.Text, out int grVal))
            {
                ColCorGreenTB.Text = grVal.ToString();

                Color newColor = Color.FromArgb(red % 255, grVal % 255, blue % 255);
                ColCorResPB.BackColor = newColor;
            }
        }

        private void ColCorBlueTB_TextChanged(object sender, EventArgs e)
        {
            int red = 255;
            int green = 255;
            if (!ColCorGreenTB.Text.Equals(""))
            {
                if (int.TryParse(ColCorGreenTB.Text, out int grVal))
                {
                    green = grVal;
                }
            }
            if (!ColCorRedTB.Text.Equals(""))
            {
                if (int.TryParse(ColCorRedTB.Text, out int rVal))
                {
                    red = rVal;
                }
            }
            if (int.TryParse(ColCorBlueTB.Text, out int bVal))
            {
                ColCorBlueTB.Text = bVal.ToString();

                Color newColor = Color.FromArgb(red % 255, green % 255, bVal % 255);
                ColCorResPB.BackColor = newColor;
            }
        }

        private void grayWorldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckChoosen(filtersToolStripMenuItem, grayWorldToolStripMenuItem);
            cursor = new Current(grayWorldToolStripMenuItem, new GrayWorld());
            HideNeedless();
        }
    }
}
