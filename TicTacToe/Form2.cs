using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form2 : Form
    {
        Graphics formGraphics;
        public bool playComp = false;
        public int firstPlayer;

        public Form2()
        {
            InitializeComponent();

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;

        private void button1_Click(object sender, EventArgs e)
        {
            playComp = true;

            button2.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.BorderSize = 3;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            drawBorder(0, 0, 322, 250, Color.White, false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            playComp = false;

            button1.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.BorderSize = 3;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Random ran = new Random();
            firstPlayer = ran.Next(1, 3);

            MessageBox.Show("Player " + firstPlayer + " goes first!", "Coin Toss");
            this.Close();
        }

        private void drawBorder(int x, int y, int w, int h, Color color, bool solid)
        {
            SolidBrush myBrush = new SolidBrush(color);
            Pen myPen = new Pen(color);
            myPen.Width = 10;
            formGraphics = CreateGraphics();
            Rectangle myRec = new Rectangle(x, y, w, h);

            if (solid)
            {
                formGraphics.FillRectangle(myBrush, myRec);
            }
            else
            {
                formGraphics.DrawRectangle(myPen, myRec);
            }

            myBrush.Dispose();
        }


    }
}
