using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace vPong
{
    public abstract class Brick
    {
        public int Life { get; set; }
        public Vector2 Position { get; set; }
        public static float Scale
        {
            get
            {
                return 1f;
            }
        }
        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)(Frame.Width * Scale), (int)(Frame.Height * Scale));
            }
        }
        public Rectangle Frame;

        public static Rectangle FrameSize = new Rectangle(0, 0, 30, 62);

        protected Texture2D SpriteSheet { get; set; }

        public virtual int LifeUpdate(int life)
        {
            life--;
            return life;
        }

        public virtual Rectangle FrameUpdate(Rectangle frame)
        {
            frame.X = frame.Width * Life;
            return frame;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpriteSheet, Position, Frame, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }
    }
}
