using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using DataTypes;

namespace vPong
{
    class BrickWall
    {
        /// <summary>
        /// a list of brick objects
        /// </summary>
        public List<Brick> BrickList
        {
            get
            {
                return _bricks;
            }
        }

        private List<Brick> _bricks;
        private Random _rand;
        //private Color _color;
        private List<List<int>> _wall;
        private int _spacer;
        private float _brickX;
        private float _brickY;
        private Level _lvl;

        /// <summary>
        /// a wall of bricks
        /// </summary>
        /// <param name="content">loads the texture of the brick wall</param>
        /// <param name="screen">screen resolution</param>
        public BrickWall(ContentManager content, Screen screen)
        {
            _bricks = new List<Brick>();
            _rand = new Random();
            _spacer = 5;

            _lvl = content.Load<Level>("Levels\\Test");
            _wall = _lvl.BrickList;

            for (int i = 0; i < _wall.Count; i++)
            {
                for (int x = 0; x < _wall[0].Count; x++)
                {
                    // determines the location of the horizontal bricks
                    _brickX = x * ((Brick.FrameSize.Width * Brick.Scale) + _spacer);
                    _brickX += (float)(screen.Width / 2) - ((_wall[0].Count * (Brick.FrameSize.Width * Brick.Scale)) + _spacer * (_wall[0].Count - 1)) / 2;
                    // determines the location of the vertical bricks
                    _brickY = i * ((Brick.FrameSize.Height * Brick.Scale) + _spacer);
                    _brickY += (float)(screen.Height / 2) - ((_wall.Count * (Brick.FrameSize.Height * Brick.Scale)) + _spacer * (_wall.Count - 1)) / 2;

                    switch (_wall[i][x])
                    {
                        case 0:
                            _bricks.Add(new GlassBrick(content, new Vector2(_brickX, _brickY)));
                            break;
                        case 10:
                            _bricks.Add(new SteelBrick(content, new Vector2(_brickX, _brickY)));
                            break;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _bricks.Count; i++)
            {
                _bricks[i].Draw(spriteBatch);
            }
        }
    }
}
