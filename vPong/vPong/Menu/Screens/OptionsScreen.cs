using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace vPong
{
    public class OptionsScreen : BaseScreen
    {
        /// <summary>
        /// OptionsScreen will contain all the settings and store them for use in Game1
        /// </summary>
        public OptionsScreen()
        {
            base.Initialize();
            MenuItems = MenuBuilder(new string[] { "Resolution", "Quality", "Shadows" , "Back" }, MenuItems, Game1.font);
        }
    }

}
