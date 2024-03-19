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

namespace CGFirstProject
{
    
    public partial class MyFIlters : Form
    {
        private Bitmap initialImg;
        private Bitmap changedImg;

        private Encoder encoder;
        private ImageCodecInfo imageCodec;
        private EncoderParameter encoderParameter;
        private EncoderParameters encoderParameters;
        private String savePath;

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
            if (inverseToolStripMenuItem1.Checked)
            {
                Filters inversedImg = new InverseFilter();
                backgroundWorker1.RunWorkerAsync(inversedImg);
            }
            if (channelsToolStripMenuItem.Checked)
            {
                Filters intensityImg = new IntensityIncrease(Int32.Parse(label2.Text), CRed.Checked, CGreen.Checked, CBlue.Checked); 
                backgroundWorker1.RunWorkerAsync(intensityImg);
            }
            if (binarizationToolStripMenuItem1.Checked)
            {
                Filters binarizaiedImg = new Binarization(trackBar1.Value);
                backgroundWorker1.RunWorkerAsync(binarizaiedImg);
            }
            if (grayShadesToolStripMenuItem1.Checked)
            {
                Filters grayImg = new GrayShades();
                backgroundWorker1.RunWorkerAsync(grayImg);
            }
            if (blurSimpleToolStripMenuItem.Checked) 
            {
                try
                {
                    Filters blurImg = new BlurFilter(trackBar1.Value);
                    backgroundWorker1.RunWorkerAsync(blurImg);
                }
                catch (ArgumentException) { }
            }
            if (blurGaussianToolStripMenuItem.Checked)
            {
                try
                {
                    Filters gaussBlurImg = new GaussianFilter(trackBar1.Value);
                    backgroundWorker1.RunWorkerAsync(gaussBlurImg);
                }
                catch (ArgumentException) { }
                
            }
            if (
                !inverseToolStripMenuItem1.Checked && !channelsToolStripMenuItem.Checked
                && !binarizationToolStripMenuItem1.Checked && !grayShadesToolStripMenuItem1.Checked
                && !blurSimpleToolStripMenuItem.Checked && !blurGaussianToolStripMenuItem.Checked
                )
            {
                pictureBox1.Image = initialImg;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
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
            inverseToolStripMenuItem1.Checked = !inverseToolStripMenuItem1.Checked;
            grayShadesToolStripMenuItem1.Checked = false;
            channelsToolStripMenuItem.Checked = false;
            binarizationToolStripMenuItem1.Checked = false;
            blurSimpleToolStripMenuItem.Checked = false;
            sharpenToolStripMenuItem1.Checked = false;
            label2.Visible = false;
            trackBar1.Visible = false;
            CRed.Visible = false;
            CBlue.Visible = false;
            CGreen.Visible = false;
        }
        private void grayShadesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            inverseToolStripMenuItem1.Checked = false;
            grayShadesToolStripMenuItem1.Checked = !grayShadesToolStripMenuItem1.Checked;
            channelsToolStripMenuItem.Checked = false;
            binarizationToolStripMenuItem1.Checked = false;
            blurSimpleToolStripMenuItem.Checked = false;
            sharpenToolStripMenuItem1.Checked = false;
            label2.Visible = false;
            trackBar1.Visible = false;
            CRed.Visible = false;
            CBlue.Visible = false;
            CGreen.Visible = false;
        }
        private void binarizationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            inverseToolStripMenuItem1.Checked = false;
            grayShadesToolStripMenuItem1.Checked = false;
            channelsToolStripMenuItem.Checked = false;
            binarizationToolStripMenuItem1.Checked = !binarizationToolStripMenuItem1.Checked;
            blurSimpleToolStripMenuItem.Checked = false;
            sharpenToolStripMenuItem1.Checked = false;
            if (binarizationToolStripMenuItem1.Checked)
            {
                label2.Visible = true;
                trackBar1.Visible = true;
            }
            CRed.Visible = false;
            CBlue.Visible = false;
            CGreen.Visible = false;
            trackBar1.Maximum = 255;
            trackBar1.Minimum = 0;
            label2.Text = trackBar1.Value.ToString();
        }
        private void channelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inverseToolStripMenuItem1.Checked = false;
            grayShadesToolStripMenuItem1.Checked = false;
            channelsToolStripMenuItem.Checked = !channelsToolStripMenuItem.Checked;
            binarizationToolStripMenuItem1.Checked = false;
            blurSimpleToolStripMenuItem.Checked = false;
            sharpenToolStripMenuItem1.Checked = false;
            if (channelsToolStripMenuItem.Checked)
            {
                label2.Visible = true;
                trackBar1.Visible = true;

                CRed.Visible = true;
                CBlue.Visible = true;
                CGreen.Visible = true;
            }
            trackBar1.Maximum = 255;
            trackBar1.Minimum = 0;
            label2.Text = trackBar1.Value.ToString();
        }
        private void blurSimpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inverseToolStripMenuItem1.Checked = false;
            grayShadesToolStripMenuItem1.Checked = false;
            channelsToolStripMenuItem.Checked = false;
            binarizationToolStripMenuItem1.Checked = false;
            blurSimpleToolStripMenuItem.Checked = !blurSimpleToolStripMenuItem.Checked;
            sharpenToolStripMenuItem1.Checked = false;
            if (blurSimpleToolStripMenuItem.Checked)
            {
                trackBar1.Visible = true;
                label2.Visible = true;
            }
            CRed.Visible = false;
            CBlue.Visible = false;
            CGreen.Visible = false;
            trackBar1.Maximum = 9;
            trackBar1.Minimum = 0;
            label2.Text = trackBar1.Value.ToString();
        }
        private void blurGaussianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inverseToolStripMenuItem1.Checked = false;
            grayShadesToolStripMenuItem1.Checked = false;
            channelsToolStripMenuItem.Checked = false;
            binarizationToolStripMenuItem1.Checked = false;
            blurSimpleToolStripMenuItem.Checked = false;
            blurGaussianToolStripMenuItem.Checked = !blurGaussianToolStripMenuItem.Checked;
            sharpenToolStripMenuItem1.Checked = false;
            if (blurGaussianToolStripMenuItem.Checked)
            {
                trackBar1.Visible = true;
                label2.Visible = true;
            }
            CRed.Visible = false;
            CBlue.Visible = false;
            CGreen.Visible = false;
            trackBar1.Maximum = 9;
            trackBar1.Minimum = 0;
            label2.Text = trackBar1.Value.ToString();

        }
        private void sharpenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            inverseToolStripMenuItem1.Checked = false;
            grayShadesToolStripMenuItem1.Checked = false;
            channelsToolStripMenuItem.Checked = false;
            binarizationToolStripMenuItem1.Checked = false;
            blurSimpleToolStripMenuItem.Checked = false;
            sharpenToolStripMenuItem1.Checked = !sharpenToolStripMenuItem1.Checked;

            label2.Visible = false;
            trackBar1.Visible = false;

            CRed.Visible = false;
            CBlue.Visible = false;
            CGreen.Visible = false;
        }
    }
}
