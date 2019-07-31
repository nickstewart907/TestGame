using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FirstGame
{
    static class EdgeBehavior
    {
        public static void OppositeSide(Shape shape)
        {
            //horizontal edges
            if (shape.posX + shape.Width > Game1.screenWidth)
                shape.posX = 0;
            if (shape.posX < 0)
                shape.posX = Game1.screenWidth - shape.Width;

            //vertical edges
            if (shape.posY + shape.Height > Game1.screenHeight)
                shape.posY = 0;
            if (shape.posY < 0)
                shape.posY = Game1.screenHeight - shape.Height;
        }

        public static void Hold(Shape shape)
        {
            //horizontal edges
            if (shape.posX + shape.Width > Game1.screenWidth)
                shape.posX = Game1.screenWidth - shape.Width;
            if (shape.posX < 0)
                shape.posX = 0;

            //vertical edges
            if (shape.posY + shape.Height > Game1.screenHeight)
                shape.posY = Game1.screenHeight - shape.Height;
            if (shape.posY < 0)
                shape.posY = 0;
        }

        public static void Bounce(Shape shape)
        {
            //check which edge, then bounce accordingly

            //horizontal edges
            if (shape.posX + shape.Width > Game1.screenWidth || shape.posX < 0)
                shape.SpeedX *= -1;

            //vertical edges
            if (shape.posY + shape.Height > Game1.screenHeight || shape.posY < 0)
                shape.SpeedY *= -1;
        }

        //check if the enemy is out of bounds (further than its initial spawn) then deactivate it
        public static void Deactivate(Enemy enemy)
        {
            if (enemy.posX < -enemy.Width || enemy.posX > Game1.screenWidth + enemy.Width || 
                enemy.posY < -enemy.Height || enemy.posY > Game1.screenHeight + enemy.Height)
                enemy.Deactivate();
        }
    }
}
