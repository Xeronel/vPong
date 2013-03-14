using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vPong
{
    /// <summary>
    /// Side of the ball that collides with an object
    /// </summary>
    public enum SideList
    {
        Top,
        Bottom,
        Left,
        Right,
        None
    }

    class CollisionDetection
    {
        public int Degree
        {
            get
            {
                return _degree;
            }
        }
        public Vector2 Position
        {
            get
            {
                return _position;
            }
        }
        public SideList Side
        {
            get
            {
                return _side;
            }
        }

        private int _degree;
        private float angle;
        private Vector2 _position;
        private SideList _side;
        private List<Vector2> _line;
        private Circle circle;

        public void Test(Vector2 circleOrigin, float circleRadius, Rectangle rectangle)
        {
            List<Vector2> Top = new List<Vector2>();
            List<Vector2> Left = new List<Vector2>();
            List<Vector2> Bottom = new List<Vector2>();
            List<Vector2> Right = new List<Vector2>();

            _side = SideList.None;
            _degree = -1;

            if (Math.Abs(circleOrigin.X - rectangle.Left) <= circleRadius + rectangle.Width)
            {
                if (Math.Abs(circleOrigin.Y - rectangle.Top) <= circleRadius + rectangle.Height)
                {
                    for (int i = rectangle.Left; i <= rectangle.Right; i++)
                    {
                        Top.Add(new Vector2(i, rectangle.Top));
                        Bottom.Add(new Vector2(i, rectangle.Bottom));
                    }

                    for (int i = rectangle.Top; i <= rectangle.Bottom; i++)
                    {
                        Left.Add(new Vector2(rectangle.Left, i));
                        Right.Add(new Vector2(rectangle.Right, i));
                    }

                    for (int i = 0; i <= 360; i++)
                    {
                        int x = (int)((circleOrigin.X + circleRadius * Math.Cos(i * (Math.PI / 180))));
                        int y = (int)((circleOrigin.Y + circleRadius * Math.Sin(i * (Math.PI / 180))));

                        for (int n = 0; n < Left.Count; n++)
                        {
                            if (new Vector2(x, y) == Left[n])
                            {
                                _side = SideList.Right;
                                _degree = i;
                                _position = Left[n];
                                break;
                            }

                            if (new Vector2(x, y) == Right[n])
                            {
                                _side = SideList.Left;
                                _degree = i;
                                _position = Right[n];
                                break;
                            }
                        }

                        if (_side == SideList.None)
                        {
                            for (int n = 0; n < Top.Count; n++)
                            {
                                if (new Vector2(x, y) == Top[n])
                                {
                                    _side = SideList.Bottom;
                                    _degree = i;
                                    _position = Top[n];
                                    break;
                                }

                                if (new Vector2(x, y) == Bottom[n])
                                {
                                    _side = SideList.Top;
                                    _degree = i;
                                    _position = Bottom[n];
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        public List<Vector2> RayTrace(Vector2 origin, Vector2 destination, float radius, ContentManager content)
        {
            circle = new Circle(100, origin, content);    
            _line = new List<Vector2>();
            float m = (origin.Y - destination.Y) / (origin.X - destination.X);
            float b = origin.Y - (m * origin.X);

            double deltaY = destination.Y - origin.Y;
            double deltaX = destination.X - origin.X;
            angle = (float)(Math.Atan2(deltaY, deltaX) * 180 / Math.PI);

            //Center Line
            float x1 = origin.X + radius * (float)Math.Cos(MathHelper.ToRadians(angle));
            float y1 = origin.Y + radius * (float)Math.Sin(MathHelper.ToRadians(angle));

            //Up & Right or Down & Right
            if (m < 0 && origin.X < destination.X || m > 0 && origin.X < destination.X)
            {
                destination = new Vector2(destination.X + radius, destination.Y + radius);
            }

            //Up & Left or Down & Left
            if (m > 0 && origin.X > destination.X || m < 0 && origin.X > destination.X)
            {
                destination = new Vector2(destination.X - radius, destination.Y - radius);
            }

            origin = new Vector2(x1, y1);

            for (int x = (int)origin.X; x < (int)destination.X; x++)
            {
                float y2 = (m * x) + b;
                _line.Add(new Vector2(x, y2));
            }

            for (int x = (int)origin.X; x > (int)destination.X; x--)
            {
                float y2 = (m * x) + b;
                _line.Add(new Vector2(x, y2));
            }

            return _line;
        }

        public void PerPixel(Texture2D texture)
        {
            Color[] _obj1TextureData = new Color[texture.Width * texture.Height];
            texture.GetData(_obj1TextureData);
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, int degree)
        {
            spriteBatch.DrawString(font, degree.ToString(), new Vector2(50, 0), Color.White);
        }

        public void DrawLine(SpriteBatch spriteBatch, Texture2D texture, List<Vector2> line)
        {
            circle.Draw(spriteBatch, (int)angle);
            int lastPoint = line.Count;
            for (int i = 0; i < line.Count; i++)
            {
                if (i == line.Count - 1)
                    spriteBatch.Draw(texture, new Rectangle((int)line[i].X, (int)line[i].Y, 1, 1), Color.White);
                else
                    spriteBatch.Draw(texture, new Rectangle((int)line[i].X, (int)line[i].Y, 1, 1), Color.Red);
            }
        }
    }
}