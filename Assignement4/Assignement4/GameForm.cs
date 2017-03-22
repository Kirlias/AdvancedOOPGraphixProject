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
        //player object
        Player player;
        HashSet<Wall> wall = new HashSet<Wall>();
        //Blocks block;
        
        //hashset for multiple platforms
        HashSet<Blocks> blocks = new HashSet<Blocks>();

        //the current game level
        public int level = 1;

        //i dont remember why i put this here
        public int drawcheck = 0;

        //start the game
        public GameForm()
        {
            InitializeComponent();
        }

        //when the game loads
        private void GameForm_Load(object sender, EventArgs e)
        {
            //fullscreen
            this.WindowState = FormWindowState.Maximized;
            
            //new objects
            player = new Player(this.DisplayRectangle);
            wall.Add(new Wall(this.DisplayRectangle, "left"));
            wall.Add(new Wall(this.DisplayRectangle, "right"));
            blocks.Add(new Blocks(this.DisplayRectangle, level));
        }

        //When the timer ticks
        private void timer_Tick(object sender, EventArgs e)
        {
            //ah there it is, for adding a platform
            if(drawcheck == 100)
            {
                //reset the check
                drawcheck = 0;

                //add a block
                blocks.Add(new Blocks(this.DisplayRectangle, level));
                //blocks.Add(new Blocks(this.DisplayRectangle, level));
                //level++;
            }

            //for each block that exists
            foreach(Blocks block in blocks)
            {
                //move each block
                block.Move();
            }

            //check for colliosion
            CheckCollisions();

            //pulls the player down
            player.MoveViaGravity();
            
            //increments the check
            drawcheck++;

            if (checkPlayerPoints())
            {
                level++;
                resetLevel();
            }
            //redraw the whole thing
            Invalidate();
        }

        private void resetLevel()
        {
            timer.Stop();
            blocks.Clear();
            timer.Start();

        }

        //when the game is drawn
        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            //draw the player
            player.Draw(e.Graphics);
            
            //draw each block
            foreach (Blocks block in blocks)
            {
                block.Draw(e.Graphics);

            }
            foreach (Wall wall in wall)
            {
                wall.draw(e.Graphics);
            }
            displayInfo(e.Graphics);

        }

        //when a key is pressed
        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //left arrow key
                case Keys.Left:
                    {
                        //move the player left
                        player.Move(Player.Direction.Left);
                        break;
                    }
                //right arrow key
                case Keys.Right:
                    {
                        //move the player right
                        player.Move(Player.Direction.Right);
                        break;
                    }
                //spacebar 
                case Keys.Space:
                    {
                        //Basically a pause mechanic
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

        //check for things colliding with each other
        public void CheckCollisions()
        {
            //remove each block the wits the ceiling
            blocks.RemoveWhere(blockHitsCeiling);



            //for every block
            foreach (Blocks block in blocks)
            {
                //if the player hits the roof
                if(player.displayArea.Top <= this.DisplayRectangle.Top)
                {
                    //stop the game
                    timer.Stop();
                }
                else if (player.displayArea.IntersectsWith(block.displayArea))
                {
                    //move the player up on the plat form
                    
                    player.yVelocity = -1 * (block.yVelocity);
                }
                //if the player hits the bottom of the frame
                else if(player.displayArea.Bottom >= this.DisplayRectangle.Bottom)
                {   
                    //stop the player from moving
                    player.yVelocity = 0;
                }
                //if the player hits a block
                
                //if the player is in the air
                else
                {
                    //move down at 20 px per tick
                    player.yVelocity = 20;
                }
            }
        }
        //for when a block hits the roof
        private bool blockHitsCeiling(Blocks block)
        {
            //if it hits
            if(block.displayArea.Y <= this.DisplayRectangle.Top)
            {
                //true
                player.points += 1;
                return true;
            }
            //if not
            else
            {
                //false
                return false;
            }
        }

        public void displayInfo(Graphics graphix)
        {
            string points = string.Format("Points: {0}",player.points) ;
            Font font = new Font("Comic Sans MS", 22);
            SolidBrush brush = new SolidBrush(Color.Red);
            Point point = new Point(20, 20);

            graphix.DrawString(points, font, brush, point);
        }

        public bool checkPlayerPoints()
        {
            if(player.points == 20)
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
