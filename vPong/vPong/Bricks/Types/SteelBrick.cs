using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vPong
{
    class SteelBrick: Brick
    {
        public SteelBrick(ContentManager content, Vector2 position)
        {
            Life = 1;
            Position = position;
            SpriteSheet = content.Load<Texture2D>("Brick\\Red Brick");
            Frame = new Rectangle(0, 0, 30, 62);
        }

        public override int LifeUpdate(int life)
        {
            return life;
        }

        public override Rectangle FrameUpdate(Rectangle frame)
        {
            return frame;
        }

    }
}
