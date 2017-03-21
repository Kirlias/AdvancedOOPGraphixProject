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
        public int width = 10000;

        //velocity to move up at
        public int yVelocity { get; set; }

        public int level;

        //display stuff
        private Rectangle blockDisplayArea;
        private Rectangle gamePlayArea;

        public Blocks(Rectangle gameArea, int level)
        {
            this.gamePlayArea = gameArea;
            blockDisplayArea.Height = height;
            blockDisplayArea.Width = width;


            yVelocity = 1+(level * 2);

            blockDisplayArea.X = gameArea.Top + 10;
            //blockDisplayArea.Y = gameArea.Left;

        }

        public void Move()
        {
            blockDisplayArea.Y += yVelocity;
        }

        public void Draw(Graphics graphix)
        {
            SolidBrush brush = new SolidBrush(Color.Blue);
            graphix.FillRectangle(brush, blockDisplayArea);
        }
    }
}
