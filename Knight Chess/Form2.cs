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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(int[,] passedchessboard, int n): this()
        {
            dataGridView1.RowCount = n;
            dataGridView1.ColumnCount = n;
            dataGridView1.Width = 50 * n + 3;
            dataGridView1.Height = 50 * n + 3;
            Color color1 = Color.FromArgb(255, 251, 219);
            Color color2 = Color.FromArgb(142, 36, 38);
            for(int i = 0; i < n; i++)
            {
                dataGridView1.Rows[i].Height = 50;
                dataGridView1.Columns[i].Width = 50;
                for(int j = 0; j < n; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = passedchessboard[j, i];
                    if((i + j)%2 == 0)
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = color1;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = color2;
                    }
                }
            }
            this.Size = new Size(dataGridView1.Size.Width + 100, dataGridView1.Size.Height + 125);

        }
        private int[,] GetStraight(int[,] passedchessboard, int n)
        {
            int[,] straightway = new int[n * n, 2];
            for(int i = 0; i < n * n; i++)
            {
                bool flagfound = false;
                for(int j = 0; j < n; j++)
                {
                    for(int k = 0; k < n; k++)
                    {
                        if(passedchessboard[k, j] == i + 1)
                        {
                            straightway[i, 0] = k;
                            straightway[i, 1] = j;
                            flagfound = true;
                            break;
                        }
                    }
                    if(flagfound == true)
                    {
                        break;
                    }
                }
            }
            return straightway;
        }
        public void PlayAnimation(int[,] passedchessboard, int n)
        {
            int[,] way = GetStraight(passedchessboard, n);
            for(int i = 0; i < n * n; i++)
            {
                int x = way[i, 0];
                int y = way[i, 1];
                try
                {
                    dataGridView1.CurrentCell = dataGridView1[x, y];
                }
                catch
                {
                    return;
                }
                Wait();
            }
        }
        private void Wait()
        {
            int tic = System.Environment.TickCount + 500;
            while(System.Environment.TickCount < tic)
            {
                Application.DoEvents();
            }
        }
    }
}
