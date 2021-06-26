using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Knight_Chess
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static int n;
        static readonly int[,] moves = { { 1, -2 }, { 2, -1 }, { 2, 1 }, { 1, 2 }, { -1, 2 }, { -2, 1 }, { -2, -1 }, { -1, -2 } };
        static bool waywasfound;
        static int[,] passedchessboard;
        private void buttonFoS_Click(object sender, EventArgs e)
        {
            int x0;
            int y0;
            if(!Int32.TryParse(textBoxN.Text, out n))
            {
                MessageBox.Show("N entered incorrectly", "Error");
                return;
            }
            if(!Int32.TryParse(textBoxXo.Text, out x0))
            {
                MessageBox.Show("Xo entered incorrectly", "Error");
                return;
            }
            if(!Int32.TryParse(textBoxYo.Text, out y0))
            {
                MessageBox.Show("Yo entered incorrectly", "Error");
                return;
            }
            if(n < 1 || x0 >= n || y0 >= n || x0 < 0 || y0 < 0)
            {
                MessageBox.Show("Invalid input data", "Error");
                return;
            }
            if(n > 6)
            {
                DialogResult result = MessageBox.Show("For the given N, Xo, Yo, the solution may not be found in a reasonable time.\nSearch a solution?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if(result != DialogResult.Yes)
                {
                    return;
                }
            }
            int[,] chessboard = new int[n, n];
            waywasfound = false;
            passedchessboard = null;
            FindingWays(chessboard, 1, x0, y0);
            if(waywasfound == true)
            {
                Form2 form2 = new Form2(passedchessboard, n);
                form2.Show();
                form2.PlayAnimation(passedchessboard, n);
            }
            else
            {
                MessageBox.Show("Solution wasn't found", "Warning");
            }
        }
        private static void FindingWays(int[,] chessboard, int num, int x0, int y0)
        {
            chessboard[x0, y0] = num;
            num++;
            if(num > n*n)
            {
                waywasfound = true;
                passedchessboard = new int[n, n];
                for(int i = 0; i < n; i++)
                {
                    for(int j = 0; j < n; j++)
                    {
                        passedchessboard[i, j] = chessboard[i, j];
                    }
                }
                return;
            }
            for(int i = 0; i < 8; i++)
            {
                int x1 = x0 + moves[i, 0];
                int y1 = y0 + moves[i, 1];
                if (x1 < 0 || y1 < 0 || x1 >= n || y1 >= n || chessboard[x1, y1] != 0)
                {
                    continue;
                }
                if(waywasfound == false)
                {
                    FindingWays(chessboard, num, x1, y1);
                }
                chessboard[x1, y1] = 0;
            }

        }
    }
}
