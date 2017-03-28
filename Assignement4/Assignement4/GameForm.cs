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
        //********************************
        //~~~~~~Objects and varables~~~~~~
        //********************************

        //Objects
        //Graphics graphics;
        Player player;
        HashSet<Wall> wall = new HashSet<Wall>();
        
        //hashset for multiple platforms
        HashSet<Blocks> blocks = new HashSet<Blocks>();


        //the current game level
        public int level = 1;

        public bool loss = false;
        //i dont remember why i put this here
        public int drawcheck = 0;


        //*******************************
        //~~~~~~~Form Funtionaility~~~~~~
        //*******************************

        //Create the window
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

            //redraw the whole thing
            Invalidate();
        }

        //reset the board when the level is completed
        private void resetLevel()
        {
            //stop the timer
            timer.Stop();
            //clear the blocks
            blocks.Clear();
            

        }

        //when the game is drawn
        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            
            
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

            if (player.displayArea.Top <= this.DisplayRectangle.Top)
            {
                //stop the game
                EndGame(e.Graphics);
                loss = true;
                timer.Stop();
                timer.Enabled = false;

            }
            if (checkPlayerPoints())
            {
                Console.WriteLine("~~~~~levelUp~~~~~");
                level++;
                player.points = 0;
                LevelUp(e.Graphics);
                resetLevel();
                player.setDisplayY(this.DisplayRectangle.Top + 20);
                player.setDisplayX(this.DisplayRectangle.Width / 2);
                
            }
            //draw the player
            player.Draw(e.Graphics);

        }




        //************************************************************************************
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Checks~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //************************************************************************************



        //~~~~~~~~~~~~~~~~~~~~
        //key Press Detection
        //~~~~~~~~~~~~~~~~~~~~
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
                        if (loss == false)
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
                        else
                        {
                            Close();
                            break;
                        }
                    }

            }
        }

        //~~~~~~~~~~~~~~~~~~~~
        //Collision Detections
        //~~~~~~~~~~~~~~~~~~~~
        public void CheckCollisions()
        {
            //remove each block the wits the ceiling
            blocks.RemoveWhere(playerPassesBlock);

            //for every block
            foreach (Blocks block in blocks)
            {
                

                if (player.displayArea.IntersectsWith(block.displayArea))
                {
                    //move the player up on the plat form
                    player.setDisplayY(block.displayArea.Y - (player.displayArea.Height));
                    player.onBlock = true;
                    Console.WriteLine("Collision via Intersection check");
                }

                //if the player falls off either side of the platform
                if (!(player.displayArea.X >= block.displayArea.X && player.displayArea.X <= (block.displayArea.X + block.width)))
                {
                    //start moving down
                    player.onBlock = false;
                    //Console.WriteLine("Player fell off platform");
                }

                if (player.onBlock)
                {
                    player.yVelocity = -1 * (block.yVelocity);
                }

                else
                {
                    player.yVelocity = 20;
                }

            }

            

            //if the player hits the bottom of the frame
            if ((player.displayArea.Y + player.displayArea.Height) >= (this.DisplayRectangle.Height))
            {
                //stop the player from moving down
                player.setDisplayY(this.DisplayRectangle.Height - player.displayArea.Height);
                player.yVelocity = 0;
                Console.WriteLine("Player hit the bottom");

            }
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //for when a block is above player
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        private bool playerPassesBlock(Blocks block)
        {
            //if it hits
            if(block.displayArea.Y <= player.displayArea.Y)
            {
                //true
                player.points += 1;
                Console.WriteLine(player.points);
                return true;
            }
            //if not
            else
            {
                //false
                return false;
            }
        }

        
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //Check if the player has enough points to level up
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public bool checkPlayerPoints()
        {
            if(player.points == 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //************************************************************************************
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Text drawing~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //************************************************************************************

        //game loss text
        public void EndGame(Graphics graphix)
        {
            string lose = "YOU LOSE!! Space to close....";
            Font font = new Font("Arial", 30);
            SolidBrush brush = new SolidBrush(Color.RoyalBlue);
            Point point = new Point(DisplayRectangle.Width/2 - 180, DisplayRectangle.Height/2 );
            graphix.DrawString(lose, font, brush, point);
        }

        //level up text
        public void LevelUp(Graphics graphix)
        {
            string lose = "Level Up: Space to continue...";
            Font font = new Font("Arial", 30);
            SolidBrush brush = new SolidBrush(Color.Black);
            Point point = new Point(DisplayRectangle.Width / 2 - 180, DisplayRectangle.Height / 2);
            graphix.DrawString(lose, font, brush, point);
        }

        //GUI info
        public void displayInfo(Graphics graphix)
        {
            string points = string.Format("Points: {0}", player.points);
            string level = string.Format("Level: {0}", this.level);
            Font font = new Font("Comic Sans MS", 22);
            SolidBrush brush = new SolidBrush(Color.Red);
            Point pntPoint = new Point(20, 20);
            Point lvlPoint = new Point(this.DisplayRectangle.Right - 255, 20);
            graphix.DrawString(points, font, brush, pntPoint);
            graphix.DrawString(level, font, brush, lvlPoint);
        }
    }
}
