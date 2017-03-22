using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignement4
{
    class Blocks
    {
        //Variables
        public int height = 10;
        public int width;

        

        //velocity to move up at
        public int yVelocity { get; set; }

        public int level;

        //display stuff
        private Rectangle blockDisplayArea;
        public Rectangle displayArea { get { return blockDisplayArea; } }
        private Rectangle gamePlayArea;

        /// <summary>
        /// Contructor for Blocks
        /// </summary>
        /// <param name="gameArea">the area for the game</param>
        /// <param name="level">the current level</param>
        public Blocks(Rectangle gameArea, int level)
        {
            //set some stuff
            this.gamePlayArea = gameArea;
            blockDisplayArea.Height = height;
            blockDisplayArea.Width = 750/*gameArea.Width - 100*/;

            //new random object
            Random rand = new Random();

            //set the block to be somewhere randomly
            blockDisplayArea.X = rand.Next(200,800);

            //set a thing
            this.level = level;

            //velocity
            yVelocity = (this.level*2);

            //where to start a block
            blockDisplayArea.Y = gameArea.Bottom + 40;
            //blockDisplayArea.Y = gameArea.Left;

        }
        /// <summary>
        /// the move method for the blocks
        /// </summary>
        public void Move()
        {
            //move the block upwards
            blockDisplayArea.Y -= yVelocity;
        }

        /// <summary>
        /// Draw method for blocks
        /// </summary>
        /// <param name="graphix"></param>
        public void Draw(Graphics graphix)
        {
            //draw a blue platform
            SolidBrush brush = new SolidBrush(Color.Blue);
            graphix.FillRectangle(brush, blockDisplayArea);
        }
    }
}
