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
    class Player : Shape
    {
        private int Speed = 3;

        public Player(GraphicsDevice device, int width, int height, Color color, int speed)
        {
            Width = width;
            Height = height;
            Color = color;
            Speed = SpeedX = SpeedY = speed;
            Body = CreateTexture(device, Width, Height, Color);

            posX = Game1.screenWidth / 2;
            posY = Game1.screenHeight / 2;
            
        }

        public override void Update()
        {
            Movement.WASD(this, Speed);
            Movement.Arrows(this, Speed);
            EdgeBehavior.OppositeSide(this);
        }

        public override void Draw()
        {
            base.Draw();
        }

        public void GetHit()
        {
            Speed = 0;
        }

        public void Reset()
        {
            posX = Game1.screenWidth / 2;
            posY = Game1.screenHeight / 2;
        }
    }
}
