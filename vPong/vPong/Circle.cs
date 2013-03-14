using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vPong
{
    class Circle
    {
        public Vector2 Origin { get; set; }
        public float Radius { get; set; }
        public List<Vector2> Points { get; set; }

        private Texture2D _texture;

        public Circle(float radius, Vector2 origin, ContentManager content)
        {
            Radius = radius;
            Origin = origin;
            _texture = content.Load<Texture2D>("pixel");
            Points = new List<Vector2>();

            //i = angle
            for (int i = 0; i < 360; i++)
            {
                float x = origin.X + radius * (float)Math.Cos(MathHelper.ToRadians(i));
                float y = origin.Y + radius * (float)Math.Sin(MathHelper.ToRadians(i));
                Points.Add(new Vector2(x, y));
            }
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Points.Count; i++)
                spriteBatch.Draw(_texture, Points[i], new Rectangle(0, 0, _texture.Width, _texture.Height), Color.White);
        }
    }
}
