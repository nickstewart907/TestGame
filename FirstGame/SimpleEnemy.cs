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
    class SimpleEnemy : Enemy
    {


        public SimpleEnemy(GraphicsDevice device, int width, int height, Color color, int speed, int x, int y, int move, int edge)
        {
            Width = width;
            Height = height;
            Color = color;
            SpeedX = SpeedY = speed;
            Body = CreateTexture(device, Width, Height, Color);
            Move = move;
            Edge = edge;
            posX = x;
            posY = y;
            Active = true;

        }

        public override void Update(Player player)
        {
            switch (Move)
            {
                case 1: Movement.Auto(this, SpeedX, SpeedY); break;
                case 2: Movement.FollowTarget(this, player); break;
            }

            switch (Edge)
            {
                case 1: EdgeBehavior.Bounce(this); break;
            }
            
            if (HitPlayer(this, player))
            {
                player.GetHit();
            }
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
