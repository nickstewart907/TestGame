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
    class Movement
    {
        private static KeyboardState oldKeyboardState;

        //Controls movment with W, A, S and D keys
        public static void WASD(Shape shape, int speed)
        {
            KeyboardState newKeyboardState = Keyboard.GetState();

            if (newKeyboardState.IsKeyDown(Keys.W)) MoveUp(shape, speed);
            if (newKeyboardState.IsKeyDown(Keys.A)) MoveLeft(shape, speed);
            if (newKeyboardState.IsKeyDown(Keys.S)) MoveDown(shape, speed);
            if (newKeyboardState.IsKeyDown(Keys.D)) MoveRight(shape, speed);

            oldKeyboardState = newKeyboardState;
        }

        //Controls movement with Arrow keys
        public static void Arrows(Shape shape, int speed)
        {
            KeyboardState newKeyboardState = Keyboard.GetState();

            if (newKeyboardState.IsKeyDown(Keys.Up)) MoveUp(shape, speed);
            if (newKeyboardState.IsKeyDown(Keys.Left)) MoveLeft(shape, speed);
            if (newKeyboardState.IsKeyDown(Keys.Down)) MoveDown(shape, speed);
            if (newKeyboardState.IsKeyDown(Keys.Right)) MoveRight(shape, speed);

            oldKeyboardState = newKeyboardState;
        }

        //Automatically moves the shape based on a given speed
        public static void Auto(Shape shape, int speedX, int speedY)
        {
            shape.posX += speedX;
            shape.posY += speedY;
        }

        public static void FollowTarget(Shape shape, Shape target)
        {
            if (shape.posX > target.posX) shape.posX += shape.SpeedX;
            else if (shape.posX < target.posX) shape.posX -= shape.SpeedX;

            if (shape.posY > target.posY) shape.posY += shape.SpeedY;
            else if (shape.posY < target.posY) shape.posY -= shape.SpeedY;
        }

        //util functions
        public static void MoveUp(Shape shape, int speed)
        {
            shape.posY -= speed;
        }
        private static void MoveLeft(Shape shape, int speed)
        {
            shape.posX -= speed;
        }
        private static void MoveDown(Shape shape, int speed)
        {
            shape.posY += speed;
        }
        private static void MoveRight(Shape shape, int speed)
        {
            shape.posX += speed;
        }
    }
}
