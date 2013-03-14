using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vPong
{
    public class GlassBrick : Brick
    {
        public GlassBrick(ContentManager content, Vector2 position)
        {
            Life = 3;
            Position = position;
            SpriteSheet = content.Load<Texture2D>("Brick\\White Brick");
            Frame = new Rectangle(0, 0, 30, 62);
        }
    }
}
