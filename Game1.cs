using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace SpaceShooter
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D ballTexture;
        Vector2 ballPosition = new Vector2(0, 0);
        float baseBallSpeed = 3;
        float acceleration;
        float ballMomentumX;
        float ballMomentumY;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferHeight = 1440;
            _graphics.PreferredBackBufferWidth = 2560;
            _graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ballPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ballTexture = Content.Load<Texture2D>("spaceship");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.LeftShift))
            {
                acceleration = baseBallSpeed * 2;
            }
            else
            {
                acceleration = baseBallSpeed;
            }
            if (kstate.IsKeyDown(Keys.Up))
            {
                ballMomentumY -= acceleration;
            }
            else if (ballMomentumY <= 0)
            {
                ballMomentumY += acceleration;
            }
            if (kstate.IsKeyDown(Keys.Down))
            {
                ballMomentumY += acceleration;
            }
            else if (ballMomentumY >= 0)
            {
                ballMomentumY -= acceleration;
            }
            if (kstate.IsKeyDown(Keys.Left))
            {
                ballMomentumX -= acceleration;
            }
            else if (ballMomentumX <= 0)
            {
                ballMomentumX += acceleration;
            }
            if (kstate.IsKeyDown(Keys.Right))
            {
                ballMomentumX += acceleration;
            }
            else if (ballMomentumX >= 0)
            {
                ballMomentumX -= acceleration;
            }
            ballPosition.Y += ballMomentumY * (float)gameTime.ElapsedGameTime.TotalSeconds;
            ballPosition.X += ballMomentumX * (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            _spriteBatch.Draw(
            ballTexture,
            ballPosition,
            null,
            Color.White,
            0f,
            new Vector2(ballTexture.Width / 2, ballTexture.Height / 2),
            Vector2.One,
            SpriteEffects.None,
            0f
            );
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}