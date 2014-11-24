using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace la1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Bitmap image1;

        private void new_bitmap()
        {
            try
            {
                image1 = new Bitmap(ClientSize.Width-20, ClientSize.Height - 70);
                pictureBox1.Image = image1;
            }
            catch (ArgumentException)
            {

            }
        }
        private void draw_line(int[,] arr, bool Brez_or_DDA)
        {
            for (int i = 0; i < arr.Length / 2; i++)
            {                
                if (Brez_or_DDA == true)
                {
                    int k = 0;
                    int z = 0;
                    for (int x = arr[i, 0]; x < x+20-k; x++)
                    {                        
                        k++;
                        for (int y = arr[i, 1]; y < y+20-z; y++)
                        {                            
                            z++;
                            Color newColor = Color.Green;
                            image1.SetPixel(x, y, newColor);
                        }
                        z = 0;
                    }                    
                    i = i + 19;
                }
                else
                {
                    int k = 0;
                    int z = 0;
                    for (int x = arr[i, 0]; x < x + 10 - k; x++)
                    {
                        k++;
                        for (int y = arr[i, 1]; y < y + 10 - z; y++)
                        {
                            z++;
                            Color newColor = Color.Green;
                            image1.SetPixel(x, y, newColor);
                        }
                        z = 0;
                    }
                    i = i + 9;
                }                
            }            
            pictureBox1.Image = image1;            
        }
        private void calculation_and_draw()
        {
            int R = 0;
            int r = 0;
            
            if (image1.Height >= image1.Width)
            {
                R = image1.Width/2-20; 
            }
            else
            {
                R = image1.Height/2-20;                
            }
            int W = image1.Width;
            int H = image1.Height;
            r = Convert.ToInt32(0.866 * R);            
            
            int[,] array1 = Algoritm.cda_line((0) + W / 2, (-R) * (-1) + H / 2, (r) + W / 2, (-r/2) * (-1) + H / 2);
            int[,] array2 = Algoritm.cda_line((0) + W / 2, (-R) * (-1) + H / 2, (-r) + W / 2, (-r/2) * (-1) + H / 2);
            int[,] array3 = Algoritm.cda_line((0) + W / 2, (-R) * (-1) + H / 2, (r) + W / 2, (r/2) * (-1) + H / 2);
            int[,] array4 = Algoritm.cda_line((0) + W / 2, (-R) * (-1) + H / 2, (-r) + W / 2, (r/2) * (-1) + H / 2);
            draw_line(array1, true);
            draw_line(array2, true);
            draw_line(array3, true);
            draw_line(array4, true);
            int[,] array5 = Algoritm.Brez((r) + W / 2, (-r / 2) * (-1) + H / 2, (r) + W / 2, (r / 2) * (-1) + H / 2);
            int[,] array6 = Algoritm.Brez((-r) + W / 2, (-r / 2) * (-1) + H / 2, (-r) + W / 2, (r / 2) * (-1) + H / 2);
            int[,] array7 = Algoritm.Brez((-r) + W / 2, (r / 2) * (-1) + H / 2, (r) + W / 2, (r / 2) * (-1) + H / 2);
            int[,] array8 = Algoritm.Brez((r) + W / 2, (r / 2) * (-1) + H / 2, (0) + W / 2, (R) * (-1) + H / 2);
            int[,] array9 = Algoritm.Brez((-r) + W / 2, (r / 2) * (-1) + H / 2, (0) + W / 2, (R) * (-1) + H / 2);
            draw_line(array5, false);
            draw_line(array6, false);
            draw_line(array7, false);
            draw_line(array8, false);
            draw_line(array9, false);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new_bitmap();
            calculation_and_draw();            
        }

        private void resize_form(object sender, EventArgs e)
        {
            new_bitmap();
            calculation_and_draw();             
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            new_bitmap();
            calculation_and_draw();
        }       
    }

    class Algoritm
    {
        public static int[,] cda_line (float x1, float y1, float x2, float y2)
        {
             int i, L, xstart, ystart, xend, yend;
             float dX, dY;

             xstart = Convert.ToInt32(Math.Round(x1));
             ystart = Convert.ToInt32(Math.Round(y1));
             xend = Convert.ToInt32(Math.Round(x2));
             yend = Convert.ToInt32(Math.Round(y2));

             L = Math.Max(Math.Abs(xend - xstart), Math.Abs(yend - ystart));

             float[] X_ar = new float[L+1];
             float[] Y_ar = new float[L+1];

             dX = (x2-x1) / L;
             dY = (y2-y1) / L;

             i = 0;
             X_ar[i] = x1;
             Y_ar[i] = y1;
             i++;

             while (i < L)
             {
                 X_ar[i] = X_ar[i - 1] + dX;
                 Y_ar[i] = Y_ar[i - 1] + dY;
                 i++;
             }
             X_ar[i] = x2;
             Y_ar[i] = y2;

             int[,] array = new int[L, 2];            
             i = 0;
             while (i < L)
             {
                 array[i, 0] = Convert.ToInt32(Math.Round(X_ar[i]));
                 array[i, 1] = Convert.ToInt32(Math.Round(Y_ar[i]));
                 i++;
             }          
            return (array);
        }

        public static int[,] Brez(float x1, float y1, float x2, float y2)
        {
            int i, L, xstart, ystart, xend, yend;            
            
            xstart = Convert.ToInt32(Math.Round(x1));
            ystart = Convert.ToInt32(Math.Round(y1));
            xend = Convert.ToInt32(Math.Round(x2));
            yend = Convert.ToInt32(Math.Round(y2));
            int lengthX = Math.Abs(xend - xstart);
            int lengthY = Math.Abs(yend - ystart);
            L = Math.Max(lengthX, lengthY);
            float[] X_ar = new float[L+1];
            float[] Y_ar = new float[L+1];
            int dx = (xend - xstart >= 0 ? 1 : -1);
            int dy = (yend - ystart >= 0 ? 1 : -1);
            i = 0;
            if (L == 0)
            {
                X_ar[i] = xstart;
                Y_ar[i] = ystart;
                i++;
            }

            if (lengthY <= lengthX)
            {
                int x = xstart;
                int y = ystart;                
                int d = -lengthX;
                
                while (i < L)
                {
                    X_ar[i] = x;
                    Y_ar[i] = y;
                    x += dx;
                    d += 2 * lengthY;
                    if (d > 0)
                    {
                        d -= 2 * lengthX;
                        y += dy;
                    }
                    i++;
                }
            }
            else
            {
                int x = xstart;
                int y = ystart;                
                int d = -lengthY;
             
                while (i < L)
                {
                    X_ar[i] = x;
                    Y_ar[i] = y;
                    y += dy;
                    d += 2 * lengthX;
                    if (d > 0)
                    {
                        d -= 2 * lengthY;
                        x += dx;
                    }
                    i++;
                }
            }
            int[,] array = new int[L , 2];
            i = 0;
            while (i < L)
            {
                array[i, 0] = Convert.ToInt32(Math.Round(X_ar[i]));
                array[i, 1] = Convert.ToInt32(Math.Round(Y_ar[i]));
                i++;
            }
            return (array);
        }

    }
}
