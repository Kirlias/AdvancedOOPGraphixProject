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

        //directions player can move
        public enum Direction { Left, Right };

        public int yVelocity { get; set; }

        //display stuff
        private Rectangle playerDisplayArea;
        public Rectangle displayArea { get { return playerDisplayArea; }}
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
                case Direction.Left:
                    {
                        Console.WriteLine("Left");
                        playerDisplayArea.X -= 25;
                        break;
                    }
                case Direction.Right:
                    {
                        Console.WriteLine("Right");
                        playerDisplayArea.X += 25;
                        break;
                    }
            }
            

        }
        public void MoveViaGravity()
        {
            this.playerDisplayArea.Y += yVelocity;
        }
        /// <summary>
        /// The draw method for the player
        /// </summary>
        /// <param name="graphix">The grphics object to draw the player</param>
        public void Draw(Graphics graphix)
        {
            SolidBrush brush = new SolidBrush(Color.Orange);
            graphix.FillEllipse(brush, playerDisplayArea);
        }
    }
}
