using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignement4
{
    public partial class GameForm : Form
    {
        Player player;
        //Blocks block;

        HashSet<Blocks> blocks = new HashSet<Blocks>();
        public int level = 1;
        public int drawcheck = 0;
        public GameForm()
        {
            InitializeComponent();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            player = new Player(this.DisplayRectangle);
            blocks.Add(new Blocks(this.DisplayRectangle, level));
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if(drawcheck == 100)
            {
                drawcheck = 0;
                blocks.Add(new Blocks(this.DisplayRectangle, level));
                //blocks.Add(new Blocks(this.DisplayRectangle, level));
                //level++;
            }
            foreach(Blocks block in blocks)
            {
                block.Move();
            }
            CheckCollisions();
            player.MoveViaGravity();
            drawcheck++;
            Invalidate();
        }

        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            player.Draw(e.Graphics);
            foreach (Blocks block in blocks)
            {
                block.Draw(e.Graphics);

            }

        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
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

        public void CheckCollisions()
        {
            blocks.RemoveWhere(blockHitsCeiling);
            foreach (Blocks block in blocks)
            {
                if(player.displayArea.Top <= this.DisplayRectangle.Top)
                {
                    timer.Stop();
                }
                else if(player.displayArea.Bottom >= this.DisplayRectangle.Bottom)
                {   
                    player.yVelocity = 0;
                }
                else if (player.displayArea.IntersectsWith(block.displayArea))
                {
                    player.yVelocity = -1*(block.yVelocity);
                }
                else
                {
                    player.yVelocity = 15;
                }
            }
        }

        private bool blockHitsCeiling(Blocks block)
        {
            if(block.displayArea.Y <= this.DisplayRectangle.Top)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
