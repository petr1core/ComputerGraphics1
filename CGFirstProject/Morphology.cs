using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CGFirstProject
{
    public partial class Morphology : Form
    {
        public int treshold;
        public bool[,] kernel;
        public Morphology()
        {
            InitializeComponent();
            trackBar1.Value = 0;
            trackBar1.Maximum = 255;
            trackBar1.Minimum = 0;
        }

        private void UpdateBtnHolder(int rows, int cols) { 
            Console.WriteLine(String.Format("W:{0}, H:{1}", ButtonsHolder.Width, ButtonsHolder.Height));
            Console.WriteLine(String.Format("r:{0}, c:{1}", rows, cols));
            Console.WriteLine(String.Format("bW:{0}, bH:{1}", ButtonsHolder.Width/cols, ButtonsHolder.Height / rows));

            int bWidth = ButtonsHolder.Width/cols;
            int bHeight = ButtonsHolder.Height/rows;
            Console.WriteLine(String.Format("BHx:{0}, BHy:{1}", ButtonsHolder.Location.X, ButtonsHolder.Location.Y));
            int pX, pY;

            if (ButtonsHolder.Controls.Count != 0) { 
                ButtonsHolder.Controls.Clear();
            }
            for (int j = 0; j < cols; j++)
            {
                pX = bWidth * j;
                for (int i = 0; i < rows; i++)
                {
                    pY = bHeight * i;
                    Button button = new Button();
                    button.Name = String.Empty;
                    button.Location = new Point(pX, pY);
                    button.Size = new Size(bWidth, bHeight);
                    button.BackColor = Color.White;
                    button.Click += Button_Click;
                    ButtonsHolder.Controls.Add(button);
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.BackColor != Color.Black)
            {
                button.BackColor = Color.Black;
                button.Name = "1";
            }
            else { 
                button.BackColor = Color.White;
                button.Name = String.Empty;
            }
            Console.WriteLine(button.Name);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int rows = (int)SizeRowsUpDown.Value;
            int cols = (int)SizeColumnsUpDown.Value;
            treshold = Int32.Parse(TresholdL.Text);
            kernel = new bool[rows, cols];
            UpdateBtnHolder(rows, cols);
        }

        private void OKbtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < SizeRowsUpDown.Value; i++) {
                for (int j = 0; j < SizeColumnsUpDown.Value; j++) {
                    kernel[i, j] = (ButtonsHolder.Controls[i + j].BackColor == Color.Black) ? true : false;
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            TresholdL.Text = trackBar1.Value.ToString();
        }
    }
}
