using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vPong
{
    /// <summary>
    /// The Base Screen for all screens
    /// </summary>
    public abstract class BaseScreen
    {
        public List<Entry> MenuItems { get; set; }
        public Vector2 ScreenCenter
        {
            get
            {
                return new Vector2(Game1.ViewPort.Width / 2, Game1.ViewPort.Height / 2);
            }
        }

        public void Initialize()
        {
            if (MenuItems == null)
            {
                MenuItems = new List<Entry>();
            }
        }
        public virtual List<Entry> MenuBuilder(string[] menuText, List<Entry> menuItems, SpriteFont font)
        {
            Vector2 _position = new Vector2();
            //_position.X = ScreenCenter.X - (font.MeasureString(menuText[0]).X/2);
            //_position.Y = ScreenCenter.Y - (font.MeasureString(menuText[0]).Y/2);
            for (int x = 0; x < menuText.Length; x++)
            {
                _position.X = ScreenCenter.X - (font.MeasureString(menuText[x]).X / 2);

                _position.Y = ScreenCenter.Y - (font.MeasureString(menuText[x]).Y / 2);
                _position.Y += font.MeasureString(menuText[x]).Y * x;

                menuItems.Add(new Entry(Color.White, _position, menuText[x]));
            }
            return menuItems;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < MenuItems.Count; i++)
            {

                MenuItems[i].Draw(spriteBatch);
            }
        }
    }
}
