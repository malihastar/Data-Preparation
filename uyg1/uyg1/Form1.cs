using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Cuda;
using ClosedXML.Excel;

namespace uyg1
{
    public partial class Form1 : Form
    {
        Image<Bgr, byte> _Inputimage;
        Image<Gray, byte> _Grayimage;
        Image<Gray, byte> _Binarizeimage;
        Image<Bgr, byte> _Tempimage;
        Rectangle rect;
        string excelFileName = "";
        //DataTable dt = new DataTable();
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Resim Seç";


            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _Inputimage = new Image<Bgr, byte>(ofd.FileName);
                pictureBox1.Image = _Inputimage.Bitmap;
                
                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                //pictureBox1.Width = Convert.ToInt32(txWidth.Text);
                //pictureBox1.Height = Convert.ToInt32(txHeight.Text);
            }
            //MessageBox.Show(pictureBox1.Width.ToString());
            //MessageBox.Show(pictureBox1.Height.ToString());

        }

        private void cizdirDortgen(Graphics g, int x, int y, int w, int h)
        {
            Pen dl = new Pen(Color.GreenYellow, 1);
            Rectangle r = new Rectangle(x, y, w, h);
            
            g.DrawRectangle(dl, r);
        }
        private void doldurDortgen(Graphics g, int x, int y, int w, int h,int alpha)
        {
            if (radioButton1.Checked)
            {
                SolidBrush dl = new SolidBrush(Color.FromArgb(alpha, 0, 204, 0));
                Rectangle r = new Rectangle(x, y, w, h);
                g.FillRectangle(dl, r);
            }
            if (radioButton2.Checked)
            {
                SolidBrush dl = new SolidBrush(Color.FromArgb(alpha, 0, 120, 255));
                Rectangle r = new Rectangle(x, y, w, h);
                g.FillRectangle(dl, r);
            }
            if (radioButton3.Checked)
            {
                SolidBrush dl = new SolidBrush(Color.FromArgb(alpha, 255, 0, 0));
                Rectangle r = new Rectangle(x, y, w, h);
                g.FillRectangle(dl, r);
            }
            if (radioButton4.Checked)
            {
                SolidBrush dl = new SolidBrush(Color.FromArgb(alpha, 204, 0, 102));
                Rectangle r = new Rectangle(x, y, w, h);
                g.FillRectangle(dl, r);
            }
            if (radioButton5.Checked)
            {
                SolidBrush dl = new SolidBrush(Color.FromArgb(alpha, 0, 0, 204));
                Rectangle r = new Rectangle(x, y, w, h);
                g.FillRectangle(dl, r);
            }
            if (radioButton6.Checked)
            {
                SolidBrush dl = new SolidBrush(Color.FromArgb(alpha, 0, 204, 204));
                Rectangle r = new Rectangle(x, y, w, h);
                g.FillRectangle(dl, r);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            gridTemizle();
            int pixelBoyutu = Convert.ToInt32(txPixel.Text);
            Graphics g = pictureBox1.CreateGraphics();
            int pWidth = pictureBox1.Width / pixelBoyutu;
            int pHeiht = pictureBox1.Height / pixelBoyutu;

            for (int i = 0; i < pWidth; i++)
            {
                for (int j = 0; j < pHeiht; j++)
                {
                    cizdirDortgen(g, i * pixelBoyutu, j * pixelBoyutu, pixelBoyutu, pixelBoyutu);
                }
            }
            gridAyarla();
        }
        
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {


            if (radioButton1.Checked)
            {
                //MessageBox.Show(pictureBox1.Width.ToString());
                Graphics g = pictureBox1.CreateGraphics();
                int pixelBoyutu = Convert.ToInt32(txPixel.Text);
                int x = e.X, y = e.Y, xYer = (x / pixelBoyutu) + 1, yYer = (y / pixelBoyutu) + 1;
                int pWidth = pictureBox1.Width / pixelBoyutu;
                int konum = ((yYer - 1) * pWidth) + xYer;


                if (dataGridView1[1, konum - 1].Value.ToString() == "0")

                {
                    // MessageBox.Show("X=" + xYer + " Y=" + yYer + " Konum=" + konum + " Boş");
                    doldurDortgen(g, (xYer - 1) * pixelBoyutu, (yYer - 1) * pixelBoyutu, pixelBoyutu, pixelBoyutu, 100);

                    dataGridView1[1, (konum - 1)].Value = 1;
                }
                else
                {
                    //MessageBox.Show("X=" + xYer + " Y=" + yYer + " Konum=" + konum+" Dolu"); oktay---
                    doldurDortgen(g, (xYer - 1) * pixelBoyutu, (yYer - 1) * pixelBoyutu, pixelBoyutu, pixelBoyutu, 0);
                    dataGridView1[1, (konum - 1)].Value = 0;
                    Rectangle rc = new Rectangle(((xYer - 1) * pixelBoyutu), ((yYer - 1) * pixelBoyutu), pixelBoyutu, pixelBoyutu);
                    pictureBox1.Invalidate(rc);
                }

            }



            else if (radioButton2.Checked)
            {
                Graphics g = pictureBox1.CreateGraphics();
                int pixelBoyutu = Convert.ToInt32(txPixel.Text);
                int x = e.X, y = e.Y, xYer = (x / pixelBoyutu) + 1, yYer = (y / pixelBoyutu) + 1;
                int pWidth = pictureBox1.Width / pixelBoyutu;
                int konum = ((yYer - 1) * pWidth) + xYer;


                if (dataGridView1[1, konum - 1].Value.ToString() == "0")

                {
                    // MessageBox.Show("X=" + xYer + " Y=" + yYer + " Konum=" + konum + " Boş");
                    doldurDortgen(g, (xYer - 1) * pixelBoyutu, (yYer - 1) * pixelBoyutu, pixelBoyutu, pixelBoyutu, 100);

                    dataGridView1[1, (konum - 1)].Value = 2;
                }
                else
                {
                    //MessageBox.Show("X=" + xYer + " Y=" + yYer + " Konum=" + konum+" Dolu"); oktay---
                    doldurDortgen(g, (xYer - 1) * pixelBoyutu, (yYer - 1) * pixelBoyutu, pixelBoyutu, pixelBoyutu, 0);
                    dataGridView1[1, (konum - 1)].Value = 0;
                    Rectangle rc = new Rectangle(((xYer - 1) * pixelBoyutu), ((yYer - 1) * pixelBoyutu), pixelBoyutu, pixelBoyutu);
                    pictureBox1.Invalidate(rc);
                }

            }

            else if (radioButton3.Checked)
            {
                Graphics g = pictureBox1.CreateGraphics();
                int pixelBoyutu = Convert.ToInt32(txPixel.Text);
                int x = e.X, y = e.Y, xYer = (x / pixelBoyutu) + 1, yYer = (y / pixelBoyutu) + 1;
                int pWidth = pictureBox1.Width / pixelBoyutu;
                int konum = ((yYer - 1) * pWidth) + xYer;


                if (dataGridView1[1, konum - 1].Value.ToString() == "0")

                {
                    // MessageBox.Show("X=" + xYer + " Y=" + yYer + " Konum=" + konum + " Boş");
                    doldurDortgen(g, (xYer - 1) * pixelBoyutu, (yYer - 1) * pixelBoyutu, pixelBoyutu, pixelBoyutu, 100);

                    dataGridView1[1, (konum - 1)].Value = 3;
                }
                else
                {
                    //MessageBox.Show("X=" + xYer + " Y=" + yYer + " Konum=" + konum+" Dolu"); oktay---
                    doldurDortgen(g, (xYer - 1) * pixelBoyutu, (yYer - 1) * pixelBoyutu, pixelBoyutu, pixelBoyutu, 0);
                    dataGridView1[1, (konum - 1)].Value = 0;
                    Rectangle rc = new Rectangle(((xYer - 1) * pixelBoyutu), ((yYer - 1) * pixelBoyutu), pixelBoyutu, pixelBoyutu);
                    pictureBox1.Invalidate(rc);
                }

            }
            else if (radioButton4.Checked)
            {
                Graphics g = pictureBox1.CreateGraphics();
                int pixelBoyutu = Convert.ToInt32(txPixel.Text);
                int x = e.X, y = e.Y, xYer = (x / pixelBoyutu) + 1, yYer = (y / pixelBoyutu) + 1;
                int pWidth = pictureBox1.Width / pixelBoyutu;
                int konum = ((yYer - 1) * pWidth) + xYer;


                if (dataGridView1[1, konum - 1].Value.ToString() == "0")

                {
                    // MessageBox.Show("X=" + xYer + " Y=" + yYer + " Konum=" + konum + " Boş");
                    doldurDortgen(g, (xYer - 1) * pixelBoyutu, (yYer - 1) * pixelBoyutu, pixelBoyutu, pixelBoyutu, 100);

                    dataGridView1[1, (konum - 1)].Value = 4;
                }
                else
                {
                    //MessageBox.Show("X=" + xYer + " Y=" + yYer + " Konum=" + konum+" Dolu"); oktay---
                    doldurDortgen(g, (xYer - 1) * pixelBoyutu, (yYer - 1) * pixelBoyutu, pixelBoyutu, pixelBoyutu, 0);
                    dataGridView1[1, (konum - 1)].Value = 0;
                    Rectangle rc = new Rectangle(((xYer - 1) * pixelBoyutu), ((yYer - 1) * pixelBoyutu), pixelBoyutu, pixelBoyutu);
                    pictureBox1.Invalidate(rc);
                }

            }
            else if (radioButton5.Checked)
            {
                Graphics g = pictureBox1.CreateGraphics();
                int pixelBoyutu = Convert.ToInt32(txPixel.Text);
                int x = e.X, y = e.Y, xYer = (x / pixelBoyutu) + 1, yYer = (y / pixelBoyutu) + 1;
                int pWidth = pictureBox1.Width / pixelBoyutu;
                int konum = ((yYer - 1) * pWidth) + xYer;


                if (dataGridView1[1, konum - 1].Value.ToString() == "0")

                {
                    // MessageBox.Show("X=" + xYer + " Y=" + yYer + " Konum=" + konum + " Boş");
                    doldurDortgen(g, (xYer - 1) * pixelBoyutu, (yYer - 1) * pixelBoyutu, pixelBoyutu, pixelBoyutu, 100);

                    dataGridView1[1, (konum - 1)].Value = 5;
                }
                else
                {
                    //MessageBox.Show("X=" + xYer + " Y=" + yYer + " Konum=" + konum+" Dolu"); oktay---
                    doldurDortgen(g, (xYer - 1) * pixelBoyutu, (yYer - 1) * pixelBoyutu, pixelBoyutu, pixelBoyutu, 0);
                    dataGridView1[1, (konum - 1)].Value = 0;
                    Rectangle rc = new Rectangle(((xYer - 1) * pixelBoyutu), ((yYer - 1) * pixelBoyutu), pixelBoyutu, pixelBoyutu);
                    pictureBox1.Invalidate(rc);
                }



               
            }


            else if (radioButton6.Checked)
            {
                Graphics g = pictureBox1.CreateGraphics();
                int pixelBoyutu = Convert.ToInt32(txPixel.Text);
                int x = e.X, y = e.Y, xYer = (x / pixelBoyutu) + 1, yYer = (y / pixelBoyutu) + 1;
                int pWidth = pictureBox1.Width / pixelBoyutu;
                int konum = ((yYer - 1) * pWidth) + xYer;


                if (dataGridView1[1, konum - 1].Value.ToString() == "0")

                {
                    // MessageBox.Show("X=" + xYer + " Y=" + yYer + " Konum=" + konum + " Boş");
                    doldurDortgen(g, (xYer - 1) * pixelBoyutu, (yYer - 1) * pixelBoyutu, pixelBoyutu, pixelBoyutu, 100);

                    dataGridView1[1, (konum - 1)].Value = 6;
                }
                else
                {
                    //MessageBox.Show("X=" + xYer + " Y=" + yYer + " Konum=" + konum+" Dolu"); oktay---
                    doldurDortgen(g, (xYer - 1) * pixelBoyutu, (yYer - 1) * pixelBoyutu, pixelBoyutu, pixelBoyutu, 0);
                    dataGridView1[1, (konum - 1)].Value = 0;
                    Rectangle rc = new Rectangle(((xYer - 1) * pixelBoyutu), ((yYer - 1) * pixelBoyutu), pixelBoyutu, pixelBoyutu);
                    pictureBox1.Invalidate(rc);
                }




            }



        }

        private void gridAyarla()
        {
            int pixelBoyutu = Convert.ToInt32(txPixel.Text);
            int pWidth = pictureBox1.Width / pixelBoyutu;
            int pHeiht = pictureBox1.Height / pixelBoyutu;
            dataGridView1.ColumnCount = 11;
            dataGridView1.Columns[0].Name = "No";
            dataGridView1.Columns[1].Name = "Sec";
            dataGridView1.Columns[2].Name = "R";
            dataGridView1.Columns[3].Name = "G";
            dataGridView1.Columns[4].Name = "B";
            dataGridView1.Columns[5].Name = "H";
            dataGridView1.Columns[6].Name = "S";
            dataGridView1.Columns[7].Name = "V";
            dataGridView1.Columns[8].Name = "L";
            dataGridView1.Columns[9].Name = "A";
            dataGridView1.Columns[10].Name = "B";
            dataGridView1.RowCount = pWidth * pHeiht;
            for (int i = 0; i < 11; i++)
            {
                dataGridView1.Columns[i].Width = 41;
            }

            for (int i = 0; i < pWidth * pHeiht; i++)
            {
                dataGridView1[0, i].Value = (i + 1);
                dataGridView1[1, i].Value = 0;
                
            }
            renkDegerleriBul(pixelBoyutu, pWidth, pHeiht);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void renkDegerleriBul(int pixelBoyut, int pwd, int phg)
        {
            Image<Rgb, Byte> rgbImage = new Image<Rgb, byte>(_Inputimage.ToBitmap());//(new Bitmap(pictureBox1.Image));
            Image<Hsv, Byte> hsvImage = new Image<Hsv, byte>(_Inputimage.ToBitmap());
            Image<Lab, Byte> LabImage = new Image<Lab, byte>(_Inputimage.ToBitmap());

            int[,] renkDeger = new int[(pwd*phg),9];

            int h1 = 0, sayac1 = 0, sayac2 = 0, sayac3 = 0, h2 = 0, h3 = 0, h4 = 0, h5 = 0, h6 = 0, h7 = 0, h8 = 0, h9 = 0;

            int DonguSay = (hsvImage.Width / pixelBoyut) * (hsvImage.Height / pixelBoyut);
            int satir = pictureBox1.Width / pixelBoyut;
            int sutun = pictureBox1.Height / pixelBoyut;

            double[,] hsvMean = new double[DonguSay, 4];
            int[] bolge = new int[0];

            for (int l = 0; l < sutun; l++)
            {
                for (int k = 0; k < satir; k++)
                {
                    for (int i = 0; i < pixelBoyut; i++)
                    {
                        for (int j = 0; j < pixelBoyut; j++)
                        {
                            h1 += rgbImage.Data[sayac2 + i, sayac1 + j, 0]; // r
                            h2 += rgbImage.Data[sayac2 + i, sayac1 + j, 1]; //g
                            h3 += rgbImage.Data[sayac2 + i, sayac1 + j, 2]; //b
                            h4 += hsvImage.Data[sayac2 + i, sayac1 + j, 0];        //h
                            h5 += hsvImage.Data[sayac2 + i, sayac1 + j, 1];        //s
                            h6 += hsvImage.Data[sayac2 + i, sayac1 + j, 2];        //v
                            h7 += LabImage.Data[sayac2 + i, sayac1 + j, 0] * 100 / 256;      //L
                            h8 += LabImage.Data[sayac2 + i, sayac1 + j, 1] - 128;      //A
                            h9 += LabImage.Data[sayac2 + i, sayac1 + j, 2] - 128;      //B
                        }
                    }

                    dataGridView1[2,sayac3].Value = h1 / (pixelBoyut*pixelBoyut);
                    dataGridView1[3, sayac3].Value = h2 / (pixelBoyut * pixelBoyut);
                    dataGridView1[4, sayac3].Value = h3 / (pixelBoyut * pixelBoyut);
                    dataGridView1[5, sayac3].Value = h4 / (pixelBoyut * pixelBoyut);
                    dataGridView1[6, sayac3].Value = h5 / (pixelBoyut * pixelBoyut);
                    dataGridView1[7, sayac3].Value = h6 / (pixelBoyut * pixelBoyut);
                    dataGridView1[8, sayac3].Value = h7 / (pixelBoyut * pixelBoyut);
                    dataGridView1[9, sayac3].Value = h8 / (pixelBoyut * pixelBoyut);
                    dataGridView1[10, sayac3].Value = h9 / (pixelBoyut * pixelBoyut);

                    // grid yazılacak 
                    h1 = 0; h2 = 0; h3 = 0;h4 =0; h5 = 0; h6 = 0; h7 = 0; h8 = 0; h9 = 0;
                    sayac1 += pixelBoyut;
                    sayac3++;
                }
                sayac1 = 0;
                sayac2 += pixelBoyut;
            }
        }

        private DataTable getTable(string tableName)
        {
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            dt.Columns.Add("No", typeof(int));
            dt.Columns.Add("Sec", typeof(int));
            dt.Columns.Add("R", typeof(int));
            dt.Columns.Add("G", typeof(int));
            dt.Columns.Add("B", typeof(int));
            dt.Columns.Add("H", typeof(int));
            dt.Columns.Add("S", typeof(int));
            dt.Columns.Add("V", typeof(int));
            dt.Columns.Add("L", typeof(int));
            dt.Columns.Add("A", typeof(int));
            dt.Columns.Add("BB", typeof(int));
            return dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Excel Dosyasını Seç";
            

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                gridTemizle();
                var wb = new XLWorkbook(ofd.FileName);
                excelFileName = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
                var ws = wb.Worksheet(1);
                DataTable dt = getTable("okunanVeriler");
                int sayac = ws.RowsUsed().Count();
                for (int i = 1; i <= sayac; i++)
                {
                    dt.Rows.Add();
                    dt.Rows[i - 1][0] = ws.Cell(i,1).Value;
                    dt.Rows[i - 1][1] = ws.Cell(i,2).Value;
                    dt.Rows[i - 1][2] = ws.Cell(i,3).Value;
                    dt.Rows[i - 1][3] = ws.Cell(i,4).Value;
                    dt.Rows[i - 1][4] = ws.Cell(i,5).Value;
                    dt.Rows[i - 1][5] = ws.Cell(i,6).Value;
                    dt.Rows[i - 1][6] = ws.Cell(i,7).Value;
                    dt.Rows[i - 1][7] = ws.Cell(i,8).Value;
                    dt.Rows[i - 1][8] = ws.Cell(i,9).Value;
                    dt.Rows[i - 1][9] = ws.Cell(i,10).Value;
                    dt.Rows[i - 1][10] = ws.Cell(i,11).Value;
                }
                dataGridView1.DataSource = dt;
            }

        }

        private void gridTemizle()
        {
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int pixelBoyutu = Convert.ToInt32(txPixel.Text);
            //int hucreNo = Convert.ToInt32(textBox1.Text);
            int pWidth = pictureBox1.Width / pixelBoyutu;
            int pHeiht = pictureBox1.Height / pixelBoyutu;
            int yer=0,yer1=0,yer2=0;

            for (int i = 0; i < pHeiht; i++)
            {
                for (int j = 0; j < pWidth; j++)
                {
                    if (dataGridView1[1, yer].Value.ToString() == "0")
                    {
                        yerKaydet(0, (yer + 1), (j * pixelBoyutu), (i * pixelBoyutu), pixelBoyutu, pixelBoyutu, excelFileName);
                        //yer2++;
                    }
                    else
                    {
                        yerKaydet(1, (yer + 1), (j * pixelBoyutu), (i * pixelBoyutu), pixelBoyutu, pixelBoyutu, excelFileName);
                        //yer1++;
                    }
                    yer++;
                    //if (dataGridView1[1, yer].Value.ToString() == "1")
                    //{
                    //    yerKaydet(1, (yer + 1), (j * pixelBoyutu), (i * pixelBoyutu), pixelBoyutu, pixelBoyutu, excelFileName);
                    //}
                    //else
                    //{
                    //    yerKaydet(0, (yer + 1), (j * pixelBoyutu), (i * pixelBoyutu), pixelBoyutu, pixelBoyutu, excelFileName);

                    //}
                    //yer++;
                }
            }
            MessageBox.Show(excelFileName + " dosyasına ait işlem tamamlandı...","Bilgi Ekranı",MessageBoxButtons.OK,MessageBoxIcon.Information);


        }

        private void yerKaydet(int durum, int sayi, int x, int y, int w, int h, string fn)
        {
            rect.X = x;
            rect.Y = y;
            rect.Width = w;
            rect.Height = h;
            _Inputimage.ROI = rect;
            Image<Bgr, byte> temp = _Inputimage.CopyBlank();
            _Inputimage.CopyTo(temp);
            _Inputimage.ROI = Rectangle.Empty;
            pictureBox2.Image = temp.Bitmap;
            string fileName;
            if (durum == 1)
            {
                fileName = @"./secilen_toy/" + fn +"_"+ sayi.ToString() + ".jpg";
                //fileName = @"./secilen_toy/"+ sayi.ToString() + ".jpg";
            }
            else
            {
                //fileName = @"./secilmeyen_toy/"+ sayi.ToString() + ".jpg";
                fileName = @"./secilmeyen_toy/" + fn + "_" + sayi.ToString() + ".jpg";
            }
            pictureBox2.Image.Save(fileName, ImageFormat.Jpeg);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Excel Dosyasını Seç";


            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Graphics g = pictureBox1.CreateGraphics();
                int pixelBoyutu = Convert.ToInt32(txPixel.Text);

                gridTemizle();
                #region matris halinde gelirse--------------------------------------
                //var wb = new XLWorkbook(ofd.FileName);
                //excelFileName = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
                //var ws = wb.Worksheet(1);
                ////DataTable dt = getTable("okunanVeriler");
                //DataTable dt = new DataTable();
                //dataGridView1.ColumnCount = ws.ColumnsUsed().Count();
                //dataGridView1.RowCount = ws.RowsUsed().Count();
                //int sayac = 0;
                //for (int i = 0; i < dataGridView1.RowCount; i++)
                //{
                //    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                //    {
                //        dataGridView1[j,i].Value = ws.Cell((i+1), (j+1)).Value;
                //        if (dataGridView1[j, i].Value.ToString() == textBox1.Text)
                //        {
                //            doldurDortgen(g, j * pixelBoyutu, i * pixelBoyutu, pixelBoyutu, pixelBoyutu, 100);
                //            sayac++;
                //        }

                //    }
                //}
                //MessageBox.Show(sayac.ToString());
                #endregion

                #region sutun vektoru seklinde gelirse--------------------------------------
                var wb = new XLWorkbook(ofd.FileName);
                excelFileName = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
                var ws = wb.Worksheet(1);
                DataTable dt = getTable("okunanVeriler");
                //DataTable dt = new DataTable();
                dataGridView1.ColumnCount = ws.ColumnsUsed().Count();
                dataGridView1.RowCount = ws.RowsUsed().Count();
                int sayac = ws.RowsUsed().Count();
                for (int i = 1; i <= sayac; i++)
                {
                    dt.Rows.Add();
                    dt.Rows[i - 1][0] = ws.Cell(i, 1).Value;
                    dt.Rows[i - 1][1] = ws.Cell(i, 2).Value;
                    dt.Rows[i - 1][2] = ws.Cell(i, 3).Value;
                    dt.Rows[i - 1][3] = ws.Cell(i, 4).Value;
                    dt.Rows[i - 1][4] = ws.Cell(i, 5).Value;
                    dt.Rows[i - 1][5] = ws.Cell(i, 6).Value;
                    dt.Rows[i - 1][6] = ws.Cell(i, 7).Value;
                    dt.Rows[i - 1][7] = ws.Cell(i, 8).Value;
                    dt.Rows[i - 1][8] = ws.Cell(i, 9).Value;
                    dt.Rows[i - 1][9] = ws.Cell(i, 10).Value;
                    dt.Rows[i - 1][10] = ws.Cell(i, 11).Value;
                }
                dataGridView1.DataSource = dt;

                int konum = 0,x,y,sutun = pictureBox1.Width/pixelBoyutu, satir = pictureBox1.Height/pixelBoyutu;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    x = konum %  sutun;
                    y = konum / sutun;
                        if (dt.Rows[konum][1].ToString() == "1")
                        {
                            doldurDortgen(g, (x * pixelBoyutu), (y * pixelBoyutu), pixelBoyutu, pixelBoyutu, 100);
                        }
                    konum++;
                }
#endregion
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

       }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        public void ClickedButton(object sender, EventArgs e)
        { 
        
        
        
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ss");
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        public void button6_Click_1(object sender, EventArgs e)
        {
            
            }

        private void button6_Click_2(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }
    }
}
