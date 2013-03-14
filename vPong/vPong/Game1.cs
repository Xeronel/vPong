using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace vPong
{
    /// <summary>
    /// screen resolution
    /// </summary>
    public struct Screen
    {
        public int Width;
        public int Height;
    }
    public enum GameState
    {
        Go,
        Pause,
        Stop,
        Exit
    }
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        RenderTarget2D target;

        public static Screen ViewPort;
        public static SpriteFont font { get; set; }
        public static Texture2D texture { get; set; }
        public static GameState currentState;

        Paddle leftBat;
        Paddle rightBat;
        Ball ball;
        BrickWall bWall;
        CollisionDetection cDetect { get; set; }
        Texture2D line;
        Coordinator _coordinator;

        public static bool lost = false;
        public static int points = 0;
        public static Vector2 _screenScale;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            ViewPort = new Screen();
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ViewPort.Width = 800;
            ViewPort.Height = 600;
            target = new RenderTarget2D(GraphicsDevice, ViewPort.Width, ViewPort.Height);

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            font = Content.Load<SpriteFont>("SpriteFont1");
            texture = Content.Load<Texture2D>("MousePointer");
            _coordinator = new Coordinator();
            rightBat = new Paddle(Content, ViewPort, Side.right);
            leftBat = new Paddle(Content, ViewPort, Side.left);
            cDetect = new CollisionDetection();
            bWall = new BrickWall(Content, ViewPort);
            ball = new Ball(Content, ViewPort, bWall.BrickList);
            line = Content.Load<Texture2D>("line");
            currentState = GameState.Stop;
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            #region Exit_Game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape))
                this.Exit();

            if (currentState == GameState.Exit)
                this.Exit();
            #endregion


            switch (currentState)
            {
                case GameState.Stop:
                    _coordinator.Update();
                    break;

                case GameState.Go:
                    rightBat.Update();
                    leftBat.Update();
                    if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Space))
                    {
                        ball.Update(cDetect, leftBat.BoundingBox, rightBat.BoundingBox, font);
                    }
                    break;
            }
                                

            int i;
            i = (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            
            GraphicsDevice.SetRenderTarget(target);

            GraphicsDevice.Clear(Color.CornflowerBlue);
 
            spriteBatch.Begin();
            
            switch (currentState)
            {
                case GameState.Stop:
                    _coordinator.Draw(spriteBatch);
                    MousePointer.Draw(spriteBatch);
                    break;

                case GameState.Go:
                    leftBat.Draw(spriteBatch);
                    rightBat.Draw(spriteBatch);
                    bWall.Draw(spriteBatch);
                    ball.Draw(spriteBatch, font);
                    cDetect.DrawLine(spriteBatch, line, cDetect.RayTrace(ball.BallPosition, ball.BallPosition + ball.BallSpeed, ball.Radius, Content));
                    break;
            }
            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin();
            spriteBatch.Draw(target, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
