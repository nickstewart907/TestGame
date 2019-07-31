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
    class Enemy : Shape
    {
        public bool Active;

        public bool HitPlayer(Enemy enemy, Player player)
        {
            //convert player and enemy to Rectangle types to use the Intersect method

            Rectangle enemyRect = new Rectangle((int)enemy.posX, (int)enemy.posY, enemy.Width, enemy.Height);
            Rectangle playerRect = new Rectangle((int)player.posX, (int)player.posY, player.Width, player.Height);
            if (Rectangle.Intersect(enemyRect, playerRect) != Rectangle.Empty)
                return true;
            else
                return false;
        }

        public bool IsActive()
        {
            return Active;
        }

        public void Deactivate()
        {
            Active = false;
        }



    }
}
