using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS_mouseColor
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			timer1.Start();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			Color color = GetPixelColor(Cursor.Position);
			lblCoordX.Text = "X = " + Cursor.Position.X.ToString();
			lblCoordY.Text = "Y = " + Cursor.Position.Y.ToString();
			lblA.Text = " A = " + color.A;
			lblR.Text = " R = " + color.R;
			lblG.Text = " G = " + color.G;
			lblB.Text = " B = " + color.B;
			pictureBox1.BackColor = color;

            byte[] HSV = bgr2hsv(color.B, color.G, color.R);
            lblH.Text = " H = " + HSV[0];
            lblS.Text = " S = " + HSV[1];
            lblV.Text = " V = " + HSV[2];
        }
		static Color GetPixelColor(Point position)
		{
			using (var bitmap = new Bitmap(1, 1))
			{
				using (var graphics = Graphics.FromImage(bitmap))
				{
					graphics.CopyFromScreen(position, new Point(0, 0), new Size(1, 1));
				}
				return bitmap.GetPixel(0, 0);
			}
		}
        public  byte[] bgr2hsv(byte b,byte g,byte r)
        {
            double B = b;
            double G = g;
            double R = r;

            double Max = Math.Max(Math.Max(B, G), R);
            double Min = Math.Min(Math.Min(B, G), R);
            double V = Max;
            double S = 0;
            if (Max == 0)
                S = 0;
            else
                S = 1 - (Min / Max);
            double H = 0;
            if (Max == Min)
                H = 0;
            else if (Max == R && G >= B)
                H = 60 * ((G - B) / (Max - Min)) + 0;
            else if (Max == R && G < B)
                H = 60 * ((G - B) / (Max - Min)) + 360;
            else if (Max == G)
                H = 60 * ((B - R) / (Max - Min)) + 120;
            else if (Max == B)
                H = 60 * ((R - G) / (Max - Min)) + 240;

            H /= 2;
            S *= 255;
            byte[] rtn = new byte[3];
            rtn[0] = (byte)H;
            rtn[1] = (byte)S;
            rtn[2] = (byte)V;

            return rtn;
        }

    }
}
