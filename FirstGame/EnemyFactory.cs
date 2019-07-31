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
    class EnemyFactory
    {
        public EnemyFactory() { }
        

        public Enemy CreateEnemy(GraphicsDevice device, Color color, int speed, int dir, int edge)
        {
            return new SweeperEnemy(device, color, speed, dir, edge);
        }

        public Enemy CreateEnemy(GraphicsDevice device, int width, int height, Color color, int speed, int x, int y, int move, int edge)
        {
            return new SimpleEnemy(device, width, height, color, speed, x, y, move, edge);
        }
    }
}
