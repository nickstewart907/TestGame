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
    abstract class Shape
    {
        public float posX { get; set; }
        public float posY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Color Color { get; set; }
        public int SpeedX { get; set; }
        public int SpeedY { get; set; }
        public int Move { get; set; }
        public int Edge { get; set; }

        public Texture2D Body { get; set; }

        public virtual void Update() { }
        public virtual void Update(Player player) { }
        public virtual void Draw()
        {
            Game1.spriteBatch.Draw(Body, new Vector2(posX, posY), Color);
        }

        public Texture2D CreateTexture(GraphicsDevice device, int width, int height, Color color)
        {
            //initialize a texture
            Texture2D texture = new Texture2D(device, width, height);

            //the array holds the color for each pixel in the texture
            Color[] data = new Color[width * height];
            for (int pixel = 0; pixel < data.Count(); pixel++)
            {
                //the function applies the color according to the specified pixel
                data[pixel] = color;
            }

            //set the color
            texture.SetData(data);

            return texture;
        }

    }
}
