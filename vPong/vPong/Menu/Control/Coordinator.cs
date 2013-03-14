using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vPong
{
    /// <summary>
    /// Coordinator moves the play through the menu screens
    /// </summary>
    public class Coordinator
    {
        MainScreen _mainScreen;
        OptionsScreen _optionsScreen;
        BaseScreen _currentScreen;

        public Coordinator()
        {
            _optionsScreen = new OptionsScreen();
            _mainScreen = new MainScreen();
            _currentScreen = _mainScreen;
        }

        public void Update()
        {
            for (int i = 0; i < _currentScreen.MenuItems.Count; i++)
            {
                if (_currentScreen.MenuItems[i].BoundingBox.Contains(MousePointer.BoundingBox))
                {
                    if (MousePointer.LeftClick)
                    {
                        switch (_currentScreen.MenuItems[i].Text)
                        {
                            case "Play":
                                Game1.currentState = GameState.Go;
                                break;
                            case "Options":
                                _currentScreen = _optionsScreen;
                                break;
                            case "Back":
                                if (_currentScreen == _optionsScreen)
                                    _currentScreen = _mainScreen;
                                break;
                            case "Exit":
                                Game1.currentState = GameState.Exit;
                                break;
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _currentScreen.Draw(spriteBatch);
        }
    }
}
