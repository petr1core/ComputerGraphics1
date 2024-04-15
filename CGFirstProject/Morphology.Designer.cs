namespace CGFirstProject
{
    partial class Morphology
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.ButtonsHolder = new System.Windows.Forms.Panel();
            this.SizeRowsUpDown = new System.Windows.Forms.NumericUpDown();
            this.SizeColumnsUpDown = new System.Windows.Forms.NumericUpDown();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.TresholdL = new System.Windows.Forms.Label();
            this.OKbtn = new System.Windows.Forms.Button();
            this.ApplyBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.SizeRowsUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SizeColumnsUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(301, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mask size: ";
            // 
            // ButtonsHolder
            // 
            this.ButtonsHolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonsHolder.Location = new System.Drawing.Point(12, 12);
            this.ButtonsHolder.Name = "ButtonsHolder";
            this.ButtonsHolder.Size = new System.Drawing.Size(243, 243);
            this.ButtonsHolder.TabIndex = 2;
            // 
            // SizeRowsUpDown
            // 
            this.SizeRowsUpDown.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.SizeRowsUpDown.Location = new System.Drawing.Point(405, 12);
            this.SizeRowsUpDown.Name = "SizeRowsUpDown";
            this.SizeRowsUpDown.Size = new System.Drawing.Size(69, 20);
            this.SizeRowsUpDown.TabIndex = 3;
            this.SizeRowsUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // SizeColumnsUpDown
            // 
            this.SizeColumnsUpDown.Location = new System.Drawing.Point(480, 12);
            this.SizeColumnsUpDown.Name = "SizeColumnsUpDown";
            this.SizeColumnsUpDown.Size = new System.Drawing.Size(69, 20);
            this.SizeColumnsUpDown.TabIndex = 4;
            this.SizeColumnsUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(399, 46);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(150, 45);
            this.trackBar1.TabIndex = 5;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(301, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Treshold: ";
            // 
            // TresholdL
            // 
            this.TresholdL.AutoSize = true;
            this.TresholdL.Location = new System.Drawing.Point(371, 46);
            this.TresholdL.Name = "TresholdL";
            this.TresholdL.Size = new System.Drawing.Size(13, 13);
            this.TresholdL.TabIndex = 7;
            this.TresholdL.Text = "0";
            // 
            // OKbtn
            // 
            this.OKbtn.Location = new System.Drawing.Point(470, 232);
            this.OKbtn.Name = "OKbtn";
            this.OKbtn.Size = new System.Drawing.Size(79, 23);
            this.OKbtn.TabIndex = 8;
            this.OKbtn.Text = "OK";
            this.OKbtn.UseVisualStyleBackColor = true;
            this.OKbtn.Click += new System.EventHandler(this.OKbtn_Click);
            // 
            // ApplyBtn
            // 
            this.ApplyBtn.Location = new System.Drawing.Point(374, 97);
            this.ApplyBtn.Name = "ApplyBtn";
            this.ApplyBtn.Size = new System.Drawing.Size(79, 23);
            this.ApplyBtn.TabIndex = 9;
            this.ApplyBtn.Text = "Apply";
            this.ApplyBtn.UseVisualStyleBackColor = true;
            this.ApplyBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // Morphology
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 267);
            this.Controls.Add(this.ApplyBtn);
            this.Controls.Add(this.OKbtn);
            this.Controls.Add(this.TresholdL);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.SizeColumnsUpDown);
            this.Controls.Add(this.SizeRowsUpDown);
            this.Controls.Add(this.ButtonsHolder);
            this.Controls.Add(this.label1);
            this.Name = "Morphology";
            this.Text = "Enter mask";
            ((System.ComponentModel.ISupportInitialize)(this.SizeRowsUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SizeColumnsUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel ButtonsHolder;
        private System.Windows.Forms.NumericUpDown SizeRowsUpDown;
        private System.Windows.Forms.NumericUpDown SizeColumnsUpDown;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label TresholdL;
        private System.Windows.Forms.Button OKbtn;
        private System.Windows.Forms.Button ApplyBtn;
    }
}