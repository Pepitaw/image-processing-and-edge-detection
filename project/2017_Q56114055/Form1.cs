using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Forms.DataVisualization.Charting;

namespace _2017_Q56114055
{
    public partial class Form1 : Form
    {
        private Bitmap openImg;
        private Bitmap preImg;
        private Bitmap Img;
        private Bitmap ImgA;
        private Random rd = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "All Files|*.*|Bitmap Files (.bmp)|*.bmp|Jpeg File(.jpg)|*.jpg";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                openImg = new Bitmap(openFileDialog1.FileName);
                pictureBox1.Image = openImg;
                pictureBox2.Image = openImg;
                pictureBox3.Image = openImg;
                Img = new Bitmap(openImg);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "All Files|*.*|Bitmap Files (.bmp)|*.bmp|Jpeg File(.jpg)|*.jpg";

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pictureBox2.Image.Save(sfd.FileName);
            }
        }

        private void button3_Click(object sender, EventArgs e) //Undo
        {
            pictureBox1.Image = preImg;
            pictureBox2.Image = preImg;
            Img = preImg;
        }

        private void button4_Click(object sender, EventArgs e) //Extract Red
        {
            preImg = new Bitmap(Img);
            pictureBox1.Image = preImg;
            for (int y = 0; y < Img.Height; y++)
            {
                for (int x = 0; x < Img.Width; x++)
                {
                    Color RGB = Img.GetPixel(x, y);
                    Img.SetPixel(x, y, Color.FromArgb(RGB.R, RGB.R, RGB.R));
                }
            }
            pictureBox2.Image = Img;
        }

        private void button5_Click(object sender, EventArgs e) //Extract Green
        {
            preImg = new Bitmap(Img);
            pictureBox1.Image = preImg;
            for (int y = 0; y < Img.Height; y++)
            {
                for (int x = 0; x < Img.Width; x++)
                {
                    Color RGB = Img.GetPixel(x, y);
                    Img.SetPixel(x, y, Color.FromArgb(RGB.G, RGB.G, RGB.G));
                }
            }
            pictureBox2.Image = Img;
        }

        private void button6_Click(object sender, EventArgs e) //Extract Blue
        {
            preImg = new Bitmap(Img);
            pictureBox1.Image = preImg;
            for (int y = 0; y < Img.Height; y++)
            {
                for (int x = 0; x < Img.Width; x++)
                {
                    Color RGB = Img.GetPixel(x, y);
                    Img.SetPixel(x, y, Color.FromArgb(RGB.B, RGB.B, RGB.B));
                }
            }
            pictureBox2.Image = Img;
        }

        private void button7_Click(object sender, EventArgs e) //Grayscale
        {
            preImg = new Bitmap(Img);
            pictureBox1.Image = preImg;
            for (int y = 0; y < Img.Height; y++)
            {
                for (int x = 0; x < Img.Width; x++)
                {
                    Color RGB = Img.GetPixel(x, y);
                    int grayScale = (int)((RGB.R * 0.3) + (RGB.G * 0.59) + (RGB.B * 0.11));
                    Img.SetPixel(x, y, Color.FromArgb(grayScale, grayScale, grayScale));
                }
            }
            pictureBox2.Image = Img;
        }

        private void button8_Click(object sender, EventArgs e) //Mean Filter
        {
            preImg = new Bitmap(Img);
            pictureBox1.Image = preImg;
            for (int y = 0; y < Img.Height - 2; y++)
            {
                for (int x = 0; x < Img.Width - 2; x++)
                {
                    int tmp = 0;
                    for (int m = 0; m < 3; m++)
                    {
                        for (int n = 0; n < 3; n++)
                        {
                            Color RGB = Img.GetPixel(x + n, y + m);
                            tmp += (int)RGB.R / 27 + (int)RGB.G / 27 + (int)RGB.B / 27;
                        }
                    }
                    Img.SetPixel(x + 1, y + 1, Color.FromArgb(tmp, tmp, tmp));
                }
            }
            pictureBox2.Image = Img;
        }

        private void button9_Click(object sender, EventArgs e) //Median Filter
        {
            preImg = new Bitmap(Img);
            pictureBox1.Image = preImg;
            for (int y = 0; y < Img.Height - 2; y++)
            {
                for (int x = 0; x < Img.Width - 2; x++)
                {
                    int[] tmp = new int[9];
                    for (int m = 0; m < 3; m++)
                    {
                        for (int n = 0; n < 3; n++)
                        {
                            Color RGB = Img.GetPixel(x + n, y + m);
                            tmp[m * 3 + n] = (int)RGB.R / 3 + (int)RGB.G / 3 + (int)RGB.B / 3;
                        }
                    }
                    QuickSort(tmp, 0, 8);
                    Img.SetPixel(x + 1, y + 1, Color.FromArgb(tmp[4], tmp[4], tmp[4]));
                }
            }
            pictureBox2.Image = Img;
        }

        static void Swap(int[] array, int i, int j)
        {
            int tmp = array[i];
            array[i] = array[j];
            array[j] = tmp;
        }

        static void QuickSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int i = left - 1;   //left margin
                int j = right + 1;  //right margin
                int axle = array[(left + right) / 2];  //axle

                while (true)
                {
                    while (array[++i] < axle) ;
                    while (array[--j] > axle) ;
                    if (i >= j)
                        break;

                    Swap(array, i, j);
                }

                QuickSort(array, left, i - 1);
                QuickSort(array, j + 1, right);
            }
        }

        private void button10_Click(object sender, EventArgs e) //Histogram
        {
            //pictureBox3.Image = draw();

            preImg = new Bitmap(Img);
            pictureBox1.Image = preImg;
            Dictionary<int, double> dictr = new Dictionary<int, double>();
            Dictionary<int, double> dictg = new Dictionary<int, double>();
            Dictionary<int, double> dictb = new Dictionary<int, double>();
            for (int y = 0; y < Img.Height; y++)
            {
                for (int x = 0; x < Img.Width; x++)
                {
                    Color RGB = Img.GetPixel(x, y);
                    if (!dictr.ContainsKey((int)RGB.R))
                    {
                        dictr.Add((int)RGB.R, 1);
                    }
                    else
                    {
                        dictr[(int)RGB.R] += 1;
                    }
                    if (!dictg.ContainsKey((int)RGB.G))
                    {
                        dictg.Add((int)RGB.G, 1);
                    }
                    else
                    {
                        dictg[(int)RGB.G] += 1;
                    }
                    if (!dictb.ContainsKey((int)RGB.B))
                    {
                        dictb.Add((int)RGB.B, 1);
                    }
                    else
                    {
                        dictb[(int)RGB.B] += 1;
                    }
                }
            }
            Dictionary<int, double> countImg = new Dictionary<int, double>(dictr);
            int pre_r = dictr.Keys.Min(), pre_g = dictg.Keys.Min(), pre_b = dictb.Keys.Min();
            for (int i = pre_r + 1; i <= 255; i++)
            {
                if (dictr.ContainsKey(i))
                {
                    dictr[i] += dictr[pre_r];
                    pre_r = i;
                }
            }
            for (int i = pre_g + 1; i <= 255; i++)
            {
                if (dictg.ContainsKey(i))
                {
                    dictg[i] += dictg[pre_g];
                    pre_g = i;
                }
            }
            for (int i = pre_b + 1; i <= 255; i++)
            {
                if (dictb.ContainsKey(i))
                {
                    dictb[i] += dictb[pre_b];
                    pre_b = i;
                }
            }
            for (int i = 0; i <= 255; i++)
            {
                if (dictr.ContainsKey(i))
                    dictr[i] = (int)Math.Round((dictr[i] - 1) / (Img.Width * Img.Height - 1) * 255);
                if (dictg.ContainsKey(i))
                    dictg[i] = (int)Math.Round((dictg[i] - 1) / (Img.Width * Img.Height - 1) * 255);
                if (dictb.ContainsKey(i))
                    dictb[i] = (int)Math.Round((dictb[i] - 1) / (Img.Width * Img.Height - 1) * 255);
            }
            for (int y = 0; y < Img.Height; y++)
            {
                for (int x = 0; x < Img.Width; x++)
                {
                    Color RGB = Img.GetPixel(x, y);
                    Img.SetPixel(x, y, Color.FromArgb((int)dictr[(int)RGB.R], (int)dictg[(int)RGB.G], (int)dictb[(int)RGB.B]));
                }
            }
            pictureBox2.Image = Img;

            //pictureBox4.Image = draw();

            chart1.Series.Remove(chart1.Series["Series1"]);
            chart2.Series.Remove(chart2.Series["Series1"]);
            

            //chart1.Titles.Add("Origin");
            Series series = chart1.Series.Add("Series1");
            chart1.Series["Series1"].IsVisibleInLegend = false;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            chart1.ChartAreas[0].AxisX.Interval = 50;
            chart1.ChartAreas[0].AxisX.Minimum = 0;

            series.ChartType = SeriesChartType.Column;
            for (int index = 0; index < 256; index++)
            {
                if (countImg.ContainsKey(index))
                {
                    series.Points.AddXY(index, countImg[index]);
                }
                else
                {
                    series.Points.AddXY(index, 0);
                }
            }
            series.Color = Color.Black;
            chart1.Series["Series1"].BorderWidth = 5;
            chart1.Series["Series1"].XValueType = ChartValueType.Int64;
            chart1.Series["Series1"].YValueType = ChartValueType.Int64;

            dictr = new Dictionary<int, double>();
            for (int y = 0; y < Img.Height; y++)
            {
                for (int x = 0; x < Img.Width; x++)
                {
                    Color RGB = Img.GetPixel(x, y);
                    if (!dictr.ContainsKey((int)RGB.R))
                    {
                        dictr.Add((int)RGB.R, 1);
                    }
                    else
                    {
                        dictr[(int)RGB.R] += 1;
                    }
                }
            }
            //chart2.Titles.Add("Histogram Equation");
            series = chart2.Series.Add("Series1");
            chart2.Series["Series1"].IsVisibleInLegend = false;
            chart2.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            chart2.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            chart2.ChartAreas[0].AxisX.Interval = 50;
            chart2.ChartAreas[0].AxisX.Minimum = 0;
            series.ChartType = SeriesChartType.Column;
            for (int index = 0; index < 256; index++)
            {
                if (dictr.ContainsKey(index))
                {
                    series.Points.AddXY(index, dictr[index]);
                }
                else
                {
                    series.Points.AddXY(index, 0);
                }
            }
            series.Color = Color.Black;
            chart2.Series["Series1"].BorderWidth = 5;
            chart2.Series["Series1"].XValueType = ChartValueType.Int64;
            chart2.Series["Series1"].YValueType = ChartValueType.Int64;
        }

        private Image draw()
        {
            Bitmap bmp = new Bitmap(Img);
            int[] histogram_r = new int[256];
            float max = 0;

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    int redValue = bmp.GetPixel(i, j).R;
                    histogram_r[redValue]++;
                    if (max < histogram_r[redValue])
                        max = histogram_r[redValue];
                }
            }

            int histHeight = 128;
            Bitmap img = new Bitmap(256, histHeight + 10);
            using (Graphics g = Graphics.FromImage(img))
            {
                for (int i = 0; i < histogram_r.Length; i++)
                {
                    float pct = histogram_r[i] / max;   // What percentage of the max is this value?
                    g.DrawLine(Pens.Black,
                        new Point(i, img.Height - 5),
                        new Point(i, img.Height - 5 - (int)(pct * histHeight))  // Use that percentage of the height
                        );
                }
            }
            return img;
        }

        private void button11_Click(object sender, EventArgs e) //Threshold
        {
            preImg = new Bitmap(Img);
            pictureBox1.Image = preImg;
            for (int y = 0; y < Img.Height; y++)
            {
                for (int x = 0; x < Img.Width; x++)
                {
                    Color RGB = Img.GetPixel(x, y);
                    if ((int)RGB.R >= trackBar1.Value)
                        Img.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                    else
                        Img.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                }
            }
            pictureBox2.Image = Img;

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox1.Text = "" + trackBar1.Value;
        }

        private void button12_Click(object sender, EventArgs e) //Vertical
        {
            preImg = new Bitmap(Img);
            pictureBox1.Image = preImg;
            int[,] gx = new int[,] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            Bitmap res = new Bitmap(Img);
            int limit = Int32.Parse(textBox9.Text); //30
            for (int y = 0; y < Img.Height - 2; y++)
            {
                for (int x = 0; x < Img.Width - 2; x++)
                {
                    int tmp = 0;
                    for (int m = 0; m < 3; m++)
                    {
                        for (int n = 0; n < 3; n++)
                        {
                            Color RGB = Img.GetPixel(x + n, y + m);
                            tmp += RGB.R * gx[m, n];
                        }
                    }
                    if (tmp * tmp > limit * limit)
                        res.SetPixel(x + 1, y + 1, Color.FromArgb(255, 255, 255));
                    else
                        res.SetPixel(x + 1, y + 1, Color.FromArgb(0, 0, 0));
                }
            }
            Img = res;
            pictureBox2.Image = Img;
        }

        private void button13_Click(object sender, EventArgs e) //Horizontal
        {
            preImg = new Bitmap(Img);
            pictureBox1.Image = preImg;
            int[,] gy = new int[,] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };
            Bitmap res = new Bitmap(Img);
            int limit = Int32.Parse(textBox9.Text); //40
            for (int y = 0; y < Img.Height - 2; y++)
            {
                for (int x = 0; x < Img.Width - 2; x++)
                {
                    int tmp1 = 0;
                    for (int m = 0; m < 3; m++)
                    {
                        for (int n = 0; n < 3; n++)
                        {
                            Color RGB = Img.GetPixel(x + n, y + m);
                            tmp1 += RGB.R * gy[m, n];
                        }
                    }
                    if (tmp1 * tmp1 > limit * limit)
                        res.SetPixel(x + 1, y + 1, Color.FromArgb(255, 255, 255));
                    else
                        res.SetPixel(x + 1, y + 1, Color.FromArgb(0, 0, 0));
                }
            }
            Img = res;
            pictureBox2.Image = Img;
        }

        private void button14_Click(object sender, EventArgs e) //Combined
        {
            preImg = new Bitmap(Img);
            pictureBox1.Image = preImg;
            int[,] gx = new int[,] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            int[,] gy = new int[,] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };
            Bitmap res = new Bitmap(Img);
            int limit = Int32.Parse(textBox9.Text) * Int32.Parse(textBox9.Text); //128
            for (int y = 0; y < Img.Height - 2; y++)
            {
                for (int x = 0; x < Img.Width - 2; x++)
                {
                    int tmp = 0, tmp1 = 0;
                    for (int m = 0; m < 3; m++)
                    {
                        for (int n = 0; n < 3; n++)
                        {
                            Color RGB = Img.GetPixel(x + n, y + m);
                            tmp += RGB.R * gx[m, n];
                            tmp1 += RGB.R * gy[m, n];
                        }
                    }
                    if (tmp * tmp + tmp1 * tmp1 > limit)
                        res.SetPixel(x + 1, y + 1, Color.FromArgb(255, 255, 255));
                    else
                        res.SetPixel(x + 1, y + 1, Color.FromArgb(0, 0, 0));
                }
            }
            Img = res;
            pictureBox2.Image = Img;
        }

        private void button15_Click(object sender, EventArgs e) //Overlapping
        {
            preImg = new Bitmap(Img);
            pictureBox1.Image = preImg;
            for (int y = 0; y < Img.Height; y++)
            {
                for (int x = 0; x < Img.Width; x++)
                {
                    Color RGB = openImg.GetPixel(x, y);
                    if (Img.GetPixel(x, y).R == 255 && x != 0 && y != 0)
                        Img.SetPixel(x, y, Color.FromArgb(0, 255, 0));
                    else
                        Img.SetPixel(x, y, Color.FromArgb(RGB.R, RGB.G, RGB.B));
                }
            }
            pictureBox2.Image = Img;
        }
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            textBox9.Text = "" + trackBar2.Value;
        }

        private void button16_Click(object sender, EventArgs e) //Connected Component 
        {
            preImg = new Bitmap(Img);
            pictureBox1.Image = preImg;
            int count = 0, count1 = 0, count2 = 0, total_count = 0;
            for (int y = 0; y < Img.Height; y++)
            {
                for (int x = 0; x < Img.Width; x++)
                {
                    if (Img.GetPixel(x, y).R == 0)  //black
                    {
                        total_count += 1;
                        count = rd.Next(100, 255);
                        count1 = rd.Next(100, 255);
                        count2 = rd.Next(100, 255);
                        Queue myq = new Queue();
                        myq.Enqueue(x);
                        myq.Enqueue(y);
                        Img.SetPixel(x, y, Color.FromArgb(count, count1, count2));
                        while (myq.Count != 0)
                        {
                            int u = (int)myq.Dequeue();
                            int v = (int)myq.Dequeue();
                            for (int m = -1; m < 2; m++)
                            {
                                for (int n = -1; n < 2; n++)
                                {
                                    if (u + n >= 0 && u + n < Img.Width && v + m >= 0 && v + m < Img.Height && Img.GetPixel(u + n, v + m).R == 0)
                                    {
                                        Img.SetPixel(u + n, v + m, Color.FromArgb(count, count1, count2));
                                        myq.Enqueue(u + n);
                                        myq.Enqueue(v + m);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            textBox18.Text = "" + total_count;
            pictureBox2.Image = Img;
            /*for (int y = 0; y < Img.Height; y++)
            {
                for (int x = 0; x < Img.Width; x++)
                {
                    Console.Write("{0} ", Img.GetPixel(x, y).R);
                }
                Console.WriteLine();
            }*/
        }

        private void button17_Click(object sender, EventArgs e) //Registration
        {
            preImg = new Bitmap(Img);
            pictureBox1.Image = preImg;
            //r = arccos ( ( a * x ) + ( b * y ) + ( c * z ) ) / ( √( a2 + b2 + c2) * √( x2 + y2 + z2) )
            double vec1_x = x2 - x1, vec1_y = y2 - y1, vec2_x = x6 - x5, vec2_y = y6 - y5;
            double angle = Math.Acos((vec1_x * vec2_x + vec1_y * vec2_y) / (Math.Sqrt(vec1_x * vec1_x + vec1_y * vec1_y) * Math.Sqrt(vec2_x * vec2_x + vec2_y * vec2_y)));
            //norm(a) * norm(b) * sin < a,b >
            double n_p = Math.Sqrt(vec1_x * vec1_x + vec1_y * vec1_y) * Math.Sqrt(vec2_x * vec2_x + vec2_y * vec2_y) * Math.Sin(angle);
            if (n_p > 0)
                angle = -angle;
            double scale = Math.Sqrt(vec2_x * vec2_x + vec2_y * vec2_y) / Math.Sqrt(vec1_x * vec1_x + vec1_y * vec1_y);
            textBox7.Text = Math.Round(angle * 180 / Math.PI, 4) + "度";
            textBox6.Text = Math.Round(scale, 4) + "";

            Bitmap tmp = new Bitmap(ImgA.Width, ImgA.Height);
            double newx = (x5 * Math.Cos(angle) + y5 * Math.Sin(angle)) / scale;
            double newy = (-x5 * Math.Sin(angle) + y5 * Math.Cos(angle)) / scale;
            double offsetx = Math.Abs(newx - x1);
            double offsety = Math.Abs(newy - y1);
            for (int y = 0; y < ImgA.Height; y++)
            {
                for (int x = 0; x < ImgA.Width; x++)
                {
                    newx = (x * Math.Cos(angle) + y * Math.Sin(angle)) / scale + offsetx;
                    newy = (-x * Math.Sin(angle) + y * Math.Cos(angle)) / scale + offsety;
                    if ((int)Math.Round(newx) >= 0 && (int)Math.Round(newy) >= 0 && (int)Math.Round(newx) < Img.Width && (int)Math.Round(newy) < Img.Height)
                        tmp.SetPixel(x, y, Color.FromArgb(Img.GetPixel((int)Math.Round(newx), (int)Math.Round(newy)).R, Img.GetPixel((int)Math.Round(newx), (int)Math.Round(newy)).G, Img.GetPixel((int)Math.Round(newx), (int)Math.Round(newy)).B));
                    else
                        tmp.SetPixel(x, y, Color.Black);
                }
            }

            double res = 0;
            for (int x = 0; x < ImgA.Width; x++)
            {
                for (int y = 0; y < ImgA.Height; y++)
                {
                    res += Math.Abs(tmp.GetPixel(x, y).R - ImgA.GetPixel(x, y).R);
                }
            }
            res = res / (ImgA.Width * ImgA.Height);
            textBox8.Text = Math.Round(res, 4).ToString();

            Img = tmp;
            pictureBox2.Image = Img;
        }

        private void button18_Click(object sender, EventArgs e) //Load A
        {
            openFileDialog1.Filter = "All Files|*.*|Bitmap Files (.bmp)|*.bmp|Jpeg File(.jpg)|*.jpg";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ImgA = new Bitmap(openFileDialog1.FileName);
                pictureBox4.Image = ImgA;
            }
        }

        int count = 0;
        double x1 = 0, y1 = 0, x2 = 0, y2 = 0, x3 = 0, y3 = 0, x4 = 0, y4 = 0;

        private List<double> trans(int x, int y, Bitmap A)
        {
            List<double> tmp = new List<double>();
            if (((double)A.Width / A.Height) >= ((double)pictureBox2.Width / pictureBox2.Height))
            {
                double ratio = (double)pictureBox2.Width / A.Width;
                double scaledHeight = A.Height * ratio;
                double filler = Math.Abs(pictureBox2.Height - scaledHeight) / 2;
                tmp.Add(x / ratio);
                tmp.Add((y - filler) / ratio);
            }
            else
            {
                double ratio = (double)pictureBox2.Height / A.Height;
                double scaledWidth = A.Width * ratio;
                double filler = Math.Abs(pictureBox2.Width - scaledWidth) / 2;
                tmp.Add((x - filler) / ratio);
                tmp.Add(y / ratio);
            }
            return tmp;
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            List<double> tmp = new List<double>();
            MouseEventArgs me = (MouseEventArgs)e;
            switch (count % 4)
            {
                case 0:
                    tmp = trans(me.Location.X, me.Location.Y, Img);
                    x1 = tmp[0];
                    y1 = tmp[1];
                    textBox10.Text = Math.Round(x1).ToString() + ", " + Math.Round(y1).ToString();
                    count = 0;
                    break;
                case 1:
                    tmp = trans(me.Location.X, me.Location.Y, Img);
                    x2 = tmp[0];
                    y2 = tmp[1];
                    textBox11.Text = Math.Round(x2).ToString() + ", " + Math.Round(y2).ToString();
                    break;
                case 2:
                    tmp = trans(me.Location.X, me.Location.Y, Img);
                    x3 = tmp[0];
                    y3 = tmp[1];
                    textBox12.Text = Math.Round(x3).ToString() + ", " + Math.Round(y3).ToString();
                    break;
                case 3:
                    tmp = trans(me.Location.X, me.Location.Y, Img);
                    x4 = tmp[0];
                    y4 = tmp[1];
                    textBox13.Text = Math.Round(x4).ToString() + ", " + Math.Round(y4).ToString();
                    break;
            }
            count += 1;
        }

        int count1 = 0;
        double x5 = 0, y5 = 0, x6 = 0, y6 = 0, x7 = 0, y7 = 0, x8 = 0, y8 = 0;
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            List<double> tmp = new List<double>();
            MouseEventArgs me = (MouseEventArgs)e;
            switch (count1 % 4)
            {
                case 0:
                    tmp = trans(me.Location.X, me.Location.Y, ImgA);
                    x5 = tmp[0];
                    y5 = tmp[1];
                    textBox14.Text = Math.Round(x5).ToString() + ", " + Math.Round(y5).ToString();
                    count1 = 0;
                    break;
                case 1:
                    tmp = trans(me.Location.X, me.Location.Y, ImgA);
                    x6 = tmp[0];
                    y6 = tmp[1];
                    textBox15.Text = Math.Round(x6).ToString() + ", " + Math.Round(y6).ToString();
                    break;
                case 2:
                    tmp = trans(me.Location.X, me.Location.Y, ImgA);
                    x7 = tmp[0];
                    y7 = tmp[1];
                    textBox16.Text = Math.Round(x7).ToString() + ", " + Math.Round(y7).ToString();
                    break;
                case 3:
                    tmp = trans(me.Location.X, me.Location.Y, ImgA);
                    x8 = tmp[0];
                    y8 = tmp[1];
                    textBox17.Text = Math.Round(x8).ToString() + ", " + Math.Round(y8).ToString();
                    break;
            }
            count1 += 1;
        }
    }
}
