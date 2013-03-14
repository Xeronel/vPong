using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace vPong
{
    /// <summary>
    /// Entry creates each object in every menu screen
    /// </summary>
    public class Entry
    {
        public Rectangle BoundingBox { get; set; }
        public string Text { get; set; }

        Color _color;
        Vector2 _position;
        
        public Entry(Color color, Vector2 position, string text)
        {
            _color = color;
            _position = position;
            Text = text;
            BoundingBox = new Rectangle((int)position.X, (int)position.Y, (int)Game1.font.MeasureString(Text).X, (int)Game1.font.MeasureString(Text).Y);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Game1.texture, BoundingBox, Color.Black);
            spritebatch.DrawString(Game1.font, Text, _position, _color);
        }
    }
}
