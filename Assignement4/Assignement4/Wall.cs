using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignement4
{
    class Wall
    {
        public int height = 1000;
        public int width = 275;

        public string side;

        private Rectangle wallDisplayArea;
        public Rectangle displayArea { get { return wallDisplayArea; } }
        private Rectangle gamePlayArea;

        public Wall(Rectangle gameArea, string side)
        {
            this.gamePlayArea = gameArea;
            wallDisplayArea.Height = height;
            wallDisplayArea.Width = width;

            if (side.Equals("left"))
            {
                this.side = "left";
                wallDisplayArea.X = 0;
            }
            if (side.Equals("right"))
            {
                this.side = "right";
                wallDisplayArea.X = gameArea.Right-width;
                //Console.WriteLine(gameArea.Right);
                //Console.WriteLine(width);
            }
           

        }

        public void draw(Graphics graphix)
        {
            //draw a blue platform
            SolidBrush brush = new SolidBrush(Color.Black);
            graphix.FillRectangle(brush, wallDisplayArea);
        }
        
    }
}
