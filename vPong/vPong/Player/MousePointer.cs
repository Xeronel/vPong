using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vPong
{
    public static class MousePointer
    {
        public static Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);
            }
        }

        public static Vector2 Position
        {
            get
            {
                return new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            }
        }
        public static bool LeftClick
        {
            get
            {
                return (Mouse.GetState().LeftButton == ButtonState.Pressed);
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.texture, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), new Rectangle(0, 0, Game1.texture.Width, Game1.texture.Height), Color.White, 0f, Vector2.Zero, .035f, SpriteEffects.None, 0f);
        }


    }
}
