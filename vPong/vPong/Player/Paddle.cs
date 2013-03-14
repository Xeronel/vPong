using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace vPong
{
    /// <summary>
    /// represents left and right sides of the screen
    /// used to determine where to draw the paddle
    /// </summary>
    public enum Side
    {
        left,
        right
    }

    class Paddle
    {
        /// <summary>
        /// paddle texture height times scale
        /// </summary>
        public int Height
        {
            get
            {
                return (int)(_paddleTexture.Height * _scaleH);
            }
        }
        /// <summary>
        /// paddle texture width times scale
        /// </summary>
        public int Width
        {
            get
            {
                return (int)(_paddleTexture.Width * _scaleW);
            }
        }
        /// <summary>
        /// the hitbox for a paddle
        /// </summary>
        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)_paddleLoc.X - (Width / 2), (int)_paddleLoc.Y - (Height / 2), Width, Height);
            }
        }
        /// <summary>
        /// the location of the top left corner of the paddle
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return _paddleLoc;
            }
        }

        #region Private_Variables
        private Texture2D _paddleTexture;
        public Vector2 _paddleLoc;
        private float _scaleH = 1.0f;
        private float _scaleW = 1.0f;
        private int _upperBound;
        private int _lowerBound;
        private Screen _screenSize;
        private Side _side;
        private Vector2 _origin;
        private Rectangle _srcRect;
        #endregion

        /// <summary>
        /// a player controlled paddle
        /// </summary>
        /// <param name="content">content manager for loading textures</param>
        /// <param name="screenRes">screen resolution</param>
        /// <param name="side">which side the paddle is on</param>
        public Paddle(ContentManager content, Screen screenRes, Side side)
        {
            _paddleTexture = content.Load<Texture2D>("Paddle\\WhitePaddle");
            _screenSize = screenRes;
            _side = side;
            _origin = new Vector2(_paddleTexture.Width / 2, _paddleTexture.Height / 2);
            _srcRect = new Rectangle(0, 0, _paddleTexture.Width, _paddleTexture.Height);

            #region Paddle_Side
            switch (side)
            {
                case Side.left:
                    _paddleLoc = new Vector2((Width / 2) + 5, _screenSize.Height / 2);
                    break;

                case Side.right:
                    _paddleLoc = new Vector2(screenRes.Width - (Width / 2) - 5, _screenSize.Height / 2);
                    break;
            }
            #endregion

        }

        /// <summary>
        /// checks to see which button on a controller or keyboard is pressed and moves each paddle up or down
        /// </summary>
        public void Update()
        {
            _upperBound = (int)(_scaleH);
            _lowerBound = _screenSize.Height - (int)(_scaleH);

            if (_side == Side.left)
            {
                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.W) && _paddleLoc.Y >= _upperBound)
                    _paddleLoc.Y -= 10;

                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.S) && _paddleLoc.Y <= _lowerBound)
                    _paddleLoc.Y += 10;

                if (((GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0.25) && (_paddleLoc.Y >= _upperBound)) || ((GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < -0.25) && (_paddleLoc.Y <= _lowerBound)))
                    _paddleLoc.Y -= GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y * 10;
            }

            if (_side == Side.right)
            {
                if (Keyboard.GetState(PlayerIndex.Two).IsKeyDown(Keys.Up) && _paddleLoc.Y >= _upperBound)
                    _paddleLoc.Y -= 10;

                if (Keyboard.GetState(PlayerIndex.Two).IsKeyDown(Keys.Down) && _paddleLoc.Y <= _lowerBound)
                    _paddleLoc.Y += 10;

                if ((GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.Y > 0.25 && (_paddleLoc.Y >= _upperBound)) || ((GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.Y < -0.25) && (_paddleLoc.Y <= _lowerBound)))
                    _paddleLoc.Y -= GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.Y * 10;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_paddleTexture, _paddleLoc, _srcRect, Color.White, 0.0f, _origin, new Vector2(_scaleW, _scaleH), SpriteEffects.None, 0.0f);
        }

    }
}
