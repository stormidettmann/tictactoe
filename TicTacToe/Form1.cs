using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

/* Stormi Sprague
 * This is intended as the "2nd" project for extra credit
 * Added "sound" for click, sound on if you'd like to hear it!
 * I also attempted extra credit for intelligent computer (see below)
 *
 * I implemented basic strategy in the computer player but you can trick it to win.
 * For example, choosing square 1, 5, and then 3 will give you 2 options to win (2 or 7). 
 * The computer can only choose one, so you can force the win.
 * I didn't find a way for the computer to anticipate this so it's not invincible.
 */


namespace TicTacToe
{
    public partial class Form1 : Form
    {
        Graphics formGraphics;
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();
       
            
        int[][] spaceCoordinates;
        int[] s1, s2, s3, s4, s5, s6, s7, s8, s9;
        int[] board;
        bool isP1;
        bool playComputer = true;



        public Form1()
        {
            InitializeComponent();
            fillArrays();

            playerSelect();

            player.SoundLocation = "blop.wav";
            board = new int[] { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            // 0 is an empty square, 1 has p1, 2 has p2.    index[0] is empty.

            if (!isP1) { turn_label.Text = "Player 2"; if (playComputer) { SmartComputer(); } }
            else { turn_label.Text = "Player 1"; }


        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            drawBoard();
            drawPieces();


        }

        private void drawPieces()
        {
            for (int i = 1; i< board.Length;i++) // space in board)
            {
                if(board[i] == 1)
                {
                    drawSquareX(i);
                }
                else if(board[i] == 2)
                {
                    drawSquareO(i);
                }
            }
        }

        private void drawSquareX(int space)
        {
            int[] s = spaceCoordinates[space];
            drawX(s[0],s[1], s[6],s[7]);
        }

        private void drawSquareO(int space)
        {
            int[] s = spaceCoordinates[space];
            drawO(s[0], s[1]);
        }

        private void drawO(int x, int y)
        {
            Pen myPen = new Pen(Color.White, 8);
            Rectangle rec = new Rectangle(x, y, 150, 150);
            formGraphics.DrawEllipse(myPen, rec);
            myPen.Dispose();
        }

        private void drawBoard()
        {
            drawRectangle(15, 200, 480, 15);
            drawRectangle(15, 365, 480, 15);
            drawRectangle(165, 50, 15, 480);
            drawRectangle(330, 50, 15, 480);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.DoubleBuffered = true;
        }

        private void drawRectangle(int x, int y, int w, int h)
        {
            SolidBrush myBrush = new SolidBrush(Color.White);
            formGraphics = CreateGraphics();
            formGraphics.FillRectangle(myBrush, new Rectangle(x, y, w, h));
            myBrush.Dispose();
        }

        private void drawX(int X1, int Y1, int X2, int Y2)
        {
            Pen myPen = new Pen(Color.White, 8);
            formGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            formGraphics.DrawLine(myPen, X1, Y1, X2, Y2);
            formGraphics.DrawLine(myPen, X1, Y2, X2, Y1);
            myPen.Dispose();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.Location.X;
            int y = e.Location.Y;
            int square = 0;

            if (x > 15 && x < 495 && y > 50 && y < 530)
            {
                //click was on the board

                //s1
                if (x > s1[0] && x < s1[6])
                {
                    //COL 1
                    if (y > s1[1] && y < s1[7]) { square = 1; }
                    else if (y > s4[1] && y < s4[7]) { square = 4; }
                    else if (y > s7[1] && y < s7[7]) { square = 7; }
                }
                else if (x > s2[0] && x < s2[6])
                {
                    //COL 2
                    if (y > s2[1] && y < s2[7]) { square = 2; }
                    else if (y > s5[1] && y < s5[7]) { square = 5; }
                    else if (y > s8[1] && y < s8[7]) { square = 8; }
                }
                else if (x > s3[0] && x < s3[6])
                {
                    //COL 3
                    if (y > s3[1] && y < s3[7]) { square = 3; }
                    else if (y > s6[1] && y < s6[7]) { square = 6; }
                    else if (y > s9[1] && y < s9[7]) { square = 9; }
                }
            }

            if (square != 0)
            {
                addPiece(square);          
            }
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void playToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
        }

        private void addPiece(int square)
        {
            int playerVal;

            if (isP1) { playerVal = 1; }
            else { playerVal = 2; }

            if (board[square] == 0)
            {
                board[square] = playerVal;
                if (isP1) { isP1 = false; turn_label.Text = "Player 2"; }
                else { isP1 = true; turn_label.Text = "Player 1"; }

                player.Play();

                this.Invalidate();

                if (!checkBoard())
                {
                    if (playComputer) { this.Update(); Thread.Sleep(2000); SmartComputer(); }
                }

            }
        }

        private void fillArrays()
        {            
            // Included a photo in the debug folder that explains these magic numbers
            s1 = new int[] { 15, 50, 165, 50, 15, 200, 165, 200 };
            s2 = new int[] { 180, 50, 330, 50, 180, 200, 330, 200 };
            s3 = new int[] { 345, 50, 495, 50, 345, 200, 495, 200 };
            s4 = new int[] { 15, 215, 165, 215, 15, 365, 165, 365 };
            s5 = new int[] { 180, 215, 330, 215, 180, 365, 330, 365 };
            s6 = new int[] { 345, 215, 495, 215, 345, 365, 495, 365 };
            s7 = new int[] { 15, 380, 165, 380, 15, 530, 165, 530 };
            s8 = new int[] { 180, 380, 330, 380, 180, 530, 330, 530 };
            s9 = new int[] { 345, 380, 495, 380, 345, 530, 495, 530 };

            spaceCoordinates = new int[][] { s1, s1, s2, s3, s4, s5, s6, s7, s8, s9 };
        }

        private bool checkBoard()
        {
            int winner = 0;

            if (board[1] == board[2] && board[1] == board[3])
            {
                if (board[1] == 1) { winner = 1; }
                else if (board[1] == 2) { winner = 2; }
            }
            if (board[4] == board[5] && board[4] == board[6])
            {
                if (board[4] == 1) { winner = 1; }
                else if (board[4] == 2) { winner = 2; }
            }
            if (board[7] == board[8] && board[7] == board[9])
            {
                if (board[7] == 1) { winner = 1; }
                else if (board[7] == 2) { winner = 2; }
            }
            if (board[1] == board[4] && board[1] == board[7])
            {
                if (board[7] == 1) { winner = 1; }
                else if (board[7] == 2) { winner = 2; }
            }
            if (board[2] == board[5] && board[2] == board[8])
            {
                if (board[2] == 1) { winner = 1; }
                else if (board[2] == 2) { winner = 2; }
            }
            if (board[3] == board[6] && board[3] == board[9])
            {
                if (board[3] == 1) { winner = 1; }
                else if (board[3] == 2) { winner = 2; }
            }
            if (board[1] == board[5] && board[1] == board[9])
            {
                if (board[1] == 1) { winner = 1; }
                else if (board[1] == 2) { winner = 2; }
            }
            if (board[3] == board[5] && board[3] == board[7])
            {
                if (board[3] == 1) { winner = 1; }
                else if (board[3] == 2) { winner = 2; }
            }

            bool isTie = true;
            for(int i  = 1; i < board.Length; i++)
            {
                if(board[i]== 0) { isTie = false; break; }
            }

            if (winner > 0)
            {
                gameOver("Player "+ winner +" wins!");
                return true;
            }
            else if (isTie)
            {
                gameOver("It's a tie!");
                return true;
            }
            return false;
        }

        private void gameOver(string winner)
        {
            DialogResult result = MessageBox.Show(winner + "\n\nPlay Again?","", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                reset();
            }

            else
            {
                this.Close();
            }
            
        }

        private void reset()
        {
            playerSelect();

            board = new int[] { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            if (!isP1) { turn_label.Text = "Player 2"; if (playComputer) { SmartComputer(); } }
            else { turn_label.Text = "Player 1"; }

            this.Invalidate();
        }

        private void playerSelect()
        {
            using (Form2 f2 = new Form2())
            {
                f2.ShowDialog();
                playComputer = f2.playComp;
                if(f2.firstPlayer == 1) { isP1 = true; }
                else { isP1 = false; }
            }
        }

        private void computer_move()
        {
            bool added = false;
            Random ran = new Random();
            int row = ran.Next(1, 3);

            while (!added)
            {
                if (row == 1)
                {
                    added = compAdd(1);
                    if (!added) { added = compAdd(2); }
                    if (!added) { added = compAdd(3); }

                    row = 2;
                }

                else if (row == 2)
                {
                    added = compAdd(4);
                    if (!added) { added = compAdd(5); }
                    if (!added) { added = compAdd(6); }

                    row = 3;
                }
                else if (row == 3)
                {
                    added = compAdd(7);
                    if (!added) { added = compAdd(8); }
                    if (!added) { added = compAdd(9); }

                    row = 1;
                }

            }

            checkBoard();

            isP1 = true; turn_label.Text = "Player 1";
        }

        private bool compAdd(int square)
        {
            int playerVal = 2;

            if (board[square] == 0)
            {
                board[square] = playerVal;
                player.Play();

                this.Invalidate();

                return true;
            }

            else
            {
                return false;
            }
        }

        private void SmartComputer()
        {
            int move = 0;

            move = CanIWin();
            if (move > 0)
            {
                compAdd(move);
                checkBoard();
                isP1 = true; turn_label.Text = "Player 1";
            }

            else
            {
                move = ShouldIBlock();
                if (move > 0)
                {
                    compAdd(move);
                    checkBoard();
                    isP1 = true; turn_label.Text = "Player 1";
                }

                else
                {
                    computer_move();
                }
            }            
        }

        private int ShouldIBlock()
        {
            int moveSquare;
            moveSquare = TwoInARow(1); //checking for p1 so I can move to block them!
            return moveSquare;
        }

        private int CanIWin()
        {
            int moveSquare;
            moveSquare = TwoInARow(2); //checking for p2 so I can move to win!
            return moveSquare;
        }

        /* checks for 2 in a row of specified number 1 for p1 2 for p2 */
        private int TwoInARow(int xo)
        {
            //int xo = 1; //checking for p1

            //computer is always o. So I'm looking for 2 x's in a row.
            // ie p1 is 1, p2 is 2, empty is 0

            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == xo)
                {
                    //special case check for corners
                    //if board[i] is a corner, check square 5.
                    if (i == 1 || i == 3 || i == 7 || i == 9)
                    {
                        if (board[5] == xo)
                        {
                            //we need to block a diagonal move
                            if (i == 1 && board[9] == 0) { return 9; }
                            if (i == 3 && board[7] == 0) { return 7; }
                            if (i == 7 && board[3] == 0) { return 3; }
                            if (i == 9 && board[1] == 0) { return 1; }
                        }

                        if(board[5] == 0)
                        {
                            if (i == 1 && board[9] == xo) { return 5; }
                            if (i == 3 && board[7] == xo) { return 5; }
                            if (i == 7 && board[3] == xo) { return 5; }
                            if (i == 9 && board[1] == xo) { return 5; }
                        }

                    }

                    if (i == 2 || i == 4 || i == 6 || i == 8)
                    {
                        if (board[5] == xo)
                        {
                            if (i == 2 && board[8] == 0) { return 8; }
                            if (i == 4 && board[6] == 0) { return 6; }
                            if (i == 6 && board[4] == 0) { return 4; }
                            if (i == 8 && board[2] == 0) { return 2; }
                        }

                        if(board[5] == 0)
                        {
                            if (i == 2 && board[8] == xo) { return 5; }
                            if (i == 4 && board[6] == xo) { return 5; }
                            if (i == 6 && board[4] == xo) { return 5; }
                            if (i == 8 && board[2] == xo) { return 5; }
                        }

                    }

                    if (i == 7)
                    {
                        if (board[8] == xo && board[9] == 0) { return 9; }
                        if (board[9] == xo && board[8] == 0) { return 8; }
                    }
                    if (i == 1)
                    {
                        if (board[2] == xo && board[3] == 0) { return 3; }
                        if (board[4] == xo && board[7] == 0) { return 7; }

                        if (board[3] == xo && board[2] == 0) { return 2; }
                        if (board[7] == xo && board[4] == 0) { return 4; }
                    }
                    if (i == 3)
                    {
                        if (board[6] == xo && board[9] == 0) { return 9; }
                        if (board[9] == xo && board[6] == 0) { return 6; }
                    }

                }
            }

            return 0;
        }

        



    }
}
