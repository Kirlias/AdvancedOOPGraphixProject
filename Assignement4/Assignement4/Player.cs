using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignement4
{
    class Player
    {
        //Variables 
        private int height = 25;
        private int width = 25;

        public int points { get; set; }

        //directions player can move
        public enum Direction { Left, Right };

        public int yVelocity { get; set; }

        //display stuff
        private Rectangle playerDisplayArea;
        public Rectangle displayArea { get { return playerDisplayArea; } }
        private Rectangle gamePlayArea;

        /// <summary>
        /// Constructor for the player
        /// </summary>
        /// <param name="gameArea">The game area to hold the player object</param>
        public Player(Rectangle gameArea)
        {
            //width of the game area
            gamePlayArea.Width = 1440;
            //height and width of the player
            playerDisplayArea.Height = this.height;
            playerDisplayArea.Width = this.width;
            //star display area for the player
            playerDisplayArea.X = gamePlayArea.Width / 2 - (width / 2);
            playerDisplayArea.Y = gamePlayArea.Top + 50;
            this.yVelocity = 15;

        }
        /// <summary>
        /// Move method for the player
        /// </summary>
        /// <param name="direction">The direction the player will move in</param>
        public void Move(Direction direction)
        {
            switch (direction)
            {
                //if direction is left
                case Direction.Left:
                    {
                        //keeps the player off the left wall
                        if (!(playerDisplayArea.Left <= 300))
                        {
                            //move player left ont he x
                            Console.WriteLine("Left");
                            playerDisplayArea.X -= 25;
                        }
                        break;
                    }
                //if direction is right
                case Direction.Right:
                    {
                        //keeps the player off the right wall
                        if (!(playerDisplayArea.Right >= (gamePlayArea.Width-300)))
                        {
                            Console.Write(playerDisplayArea.Right);
                            //move right on x
                            Console.WriteLine("Right");
                            playerDisplayArea.X += 25;
                        }
                        break;
                    }
            }
            

        }

        /// <summary>
        /// Pulls the player down constantly
        /// </summary>
        public void MoveViaGravity()
        {
            //move the player down on Y
            this.playerDisplayArea.Y += yVelocity;
        }
        /// <summary>
        /// The draw method for the player
        /// </summary>
        /// <param name="graphix">The grphics object to draw the player</param>
        public void Draw(Graphics graphix)
        {
            //draw an orange circle
            SolidBrush brush = new SolidBrush(Color.Orange);
            graphix.FillEllipse(brush, playerDisplayArea);
        }
    }
}
