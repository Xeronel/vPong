using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace vPong
{
    /// <summary>
    /// MainScreen starts up when executable is run
    /// </summary>
    public class MainScreen : BaseScreen
    {
        public MainScreen()
        {
            base.Initialize();
            MenuItems = MenuBuilder(new string[] { "Play", "Options", "Exit" }, MenuItems, Game1.font);
        }
    }
}
