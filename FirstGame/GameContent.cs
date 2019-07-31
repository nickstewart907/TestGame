using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace FirstGame
{
    class GameContent
    {
        public SpriteFont Arial { get; set; }

        public GameContent(ContentManager Content)
        {
            Arial = Content.Load<SpriteFont>("Arial20");
        }
    }
}
