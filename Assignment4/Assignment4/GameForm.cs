using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment4
{
    public partial class GameForm : Form
    {
        Player player;
        Blocks block;
        public GameForm()
        {
            InitializeComponent();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            player = new Player(this.DisplayRectangle);
            block = new Blocks(this.DisplayRectangle);
            
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Left:
                    {
                        player.Move(Player.Direction.Left);
                        break;
                    }
                case Keys.Right:
                    {
                        player.Move(Player.Direction.Right);
                        break;
                    }
                case Keys.Space:
                    {
                        if (timer.Enabled)
                        {
                            
                            timer.Stop();
                            Console.WriteLine("timer stopped");
                        }
                        else
                        {
                            
                            timer.Start();
                            Console.WriteLine("Timer Start");
                        }
                        break;
                    }

            }
        }

        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            player.Draw(e.Graphics);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
