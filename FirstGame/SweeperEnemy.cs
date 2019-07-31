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
    class SweeperEnemy : Enemy
    {
        
        public SweeperEnemy(GraphicsDevice device, Color color, int speed, int dir, int edge)
        {
            Color = color;
            Width = 40;
            Height = 40;
            Edge = edge;
            //case 1: top to bottom
            //case 2: right to left
            //case 3: bottom to top
            //case 4: left to right
            switch (dir)
            {
                case 1:
                    Width = Game1.screenWidth;
                    posX = 0; posY = -Height;
                    SpeedX = 0; SpeedY = speed;
                    break;
                case 2:
                    Height = Game1.screenHeight;
                    posX = Game1.screenWidth + Width; posY = 0;
                    SpeedX = -speed; SpeedY = 0;
                    break;
                case 3:
                    Width = Game1.screenWidth;
                    posX = 0; posY = Game1.screenHeight + Height;
                    SpeedX = 0; SpeedY = -speed;
                    break;
                case 4:
                    Height = Game1.screenHeight;
                    posX = -Width; posY = 0;
                    SpeedX = speed; SpeedY = 0;
                    break;
            }

            Body = CreateTexture(device, Width, Height, Color);

            Active = true;
        }

        public override void Update(Player player)
        {
            Movement.Auto(this, SpeedX, SpeedY);

            switch (Edge)
            {
                case 1: EdgeBehavior.Bounce(this); break;
                case 2: EdgeBehavior.Deactivate(this); break;
            }

            EdgeBehavior.Deactivate(this);
            if (HitPlayer(this, player))
                player.GetHit();
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
