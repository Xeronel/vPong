using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace vPong
{
    class Ball
    {
        /// <summary>
        /// radius of the ball
        /// </summary>
        public float Radius
        {
            get
            {
                return (_ballTexture.Width / 2) * _scale;
            }
        }
        public Rectangle BallRectangle;
        public Vector2 BallPosition;
        public Vector2 BallSpeed;
        public Vector2 Origin;
        public Texture2D Texture
        {
            get
            {
                return _ballTexture;
            }
        }

        private Screen _screenSize;
        private Texture2D _ballTexture;
        private float _rotation;

        private float _scale;
        private CollisionDetection _leftPaddleCollision;
        private CollisionDetection _rightPaddleCollision;
        private CollisionDetection _brickCollision;
        private IEnumerable<Brick> _brickWall;
        private Vector2 _fez;

        private int _leftMax;
        private int _topMax;
        private int _rightMax;
        private int _botMax;


        private SpriteBatch _spriteBatch;

        public Ball(ContentManager content, Screen screenXY, List<Brick> brickW)
        {
            _ballTexture = content.Load<Texture2D>("Ball\\BlackBall");
            BallPosition = new Vector2(0, 0);
            BallRectangle = new Rectangle(0, 0, 50, 50);
            Origin = new Vector2(_ballTexture.Width / 2, _ballTexture.Height / 2);
            _screenSize = screenXY;
            _scale = 1f;

            _rightMax = _screenSize.Width - (int)((_ballTexture.Width * _scale) / 2);
            _botMax = _screenSize.Height - (int)((_ballTexture.Height * _scale) / 2);
            _leftMax = (int)((_ballTexture.Width * _scale) / 2);
            _topMax = (int)((_ballTexture.Height * _scale) / 2);

            _leftPaddleCollision = new CollisionDetection();
            _rightPaddleCollision = new CollisionDetection();
            _brickCollision = new CollisionDetection();
            _brickWall = brickW;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collisionDetection"></param>
        /// <param name="leftBat"></param>
        /// <param name="rightBat"></param>
        /// <param name="font"></param>
        public void Update(CollisionDetection collisionDetection, Rectangle leftBat, Rectangle rightBat, SpriteFont font)
        {
            if (!Game1.lost)
            {
                #region Wall_Collisions
                //left
                if (BallPosition.X <= _leftMax)
                {
                    BallSpeed.X = 3f;
                }

                //right
                if (BallPosition.X >= _rightMax)
                {
                    BallSpeed.X = -3f;
                }

                //top
                if (BallPosition.Y <= _topMax)
                {
                    BallSpeed.Y = 3f;
                }

                //bottom
                if (BallPosition.Y >= _botMax)
                {
                    BallSpeed.Y = -3f;
                }
                #endregion

                #region Ball Speed Reset
                if (BallSpeed.X > 20)
                    BallSpeed.X = 20;
                if (BallSpeed.Y > 20)
                    BallSpeed.Y = 20;
                #endregion

                #region Paddle_Collisions
                _leftPaddleCollision.Test(BallPosition, Radius, leftBat);
                _rightPaddleCollision.Test(BallPosition, Radius, rightBat);

                if (_leftPaddleCollision.Side != SideList.None)
                {
                    switch (_leftPaddleCollision.Side)
                    {
                        case SideList.Left:
                            BallSpeed.X = -BallSpeed.X;
                            break;
                    }

                }

                if (_rightPaddleCollision.Side != SideList.None)
                {
                    switch (_rightPaddleCollision.Side)
                    {
                        case SideList.Right:
                            BallSpeed.X = -BallSpeed.X;
                            break;
                        //case SideList.Top:
                        //    ballSpeed.Y = -ballSpeed.Y;
                        //    break;
                        //case SideList.Bottom:
                        //    ballSpeed.Y = -ballSpeed.Y;
                        //    break;
                    }
                }
                #endregion

                #region Brick_Collisions
                foreach (Brick b in _brickWall)
                {
                    CollisionDetection colDetect = new CollisionDetection();

                    colDetect.Test(BallPosition, Radius, b.BoundingBox);

                    switch (colDetect.Side)
                    {
                        case SideList.Right:
                            PositionBall("X", -1, b, colDetect);
                            break;
                        case SideList.Left:
                            PositionBall("X", 1, b, colDetect);
                            break;

                        case SideList.Top:
                            PositionBall("Y", 1, b, colDetect);
                            break;

                        case SideList.Bottom:
                            PositionBall("Y", -1, b, colDetect);
                            break;
                    }
                    if (colDetect.Side != SideList.None)
                        break;
                }
                #endregion  
                BallPosition += BallSpeed;

                _rotation += (float)BallSpeed.X * 0.02f;
            }
        }

        /// <summary>
        /// Moves Ball Based On Collisions
        /// </summary>
        /// <param name="Var"></param>
        /// <param name="i"></param>
        /// <param name="b"></param>
        /// <param name="colDetect"></param>
        public void PositionBall(string Var, int i, Brick b, CollisionDetection colDetect)
        {
            #region Left & Right
            if (Var.ToUpper() == "X")
            {
                BallPosition.X = (colDetect.Position.X + Radius + (5 * i));
                BallSpeed.X *= -1;
                //b.Life = b.LifeUpdate(b.Life);
                //b.frame = b.FrameUpdate(b.Frame);

                colDetect.Test(BallPosition, Radius, b.BoundingBox);

                while (colDetect.Side != SideList.None)
                {
                    BallPosition.X = BallPosition.X + i;
                    colDetect.Test(BallPosition, Radius, b.BoundingBox);
                }
            }
            #endregion

            #region Top & Bottom
            if (Var.ToUpper() == "Y")
            {

                BallPosition.Y = (colDetect.Position.Y + ((Radius + 5) * i));
                BallSpeed.Y = -BallSpeed.Y;
                //b.Life = b.LifeUpdate(b.Life);
                //b.Frame = b.FrameUpdate(b.Frame);

                colDetect.Test(BallPosition, Radius, b.BoundingBox);
                while (colDetect.Side != SideList.None)
                {
                    BallPosition.Y = BallPosition.Y + i;
                    colDetect.Test(BallPosition, Radius, b.BoundingBox);
                }
            }
            #endregion
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Draw(_ballTexture, BallPosition, new Rectangle(0, 0, _ballTexture.Width, _ballTexture.Height), Color.White, _rotation, Origin, _scale, SpriteEffects.None, 0.0f);
            
        }
    }
}
