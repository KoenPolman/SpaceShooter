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
        List<Object> objects = new List<Object>();
        TextManegement textManegement = new TextManegement();
        Texture2D spaceShipTexture;
        Texture2D asteroidTexture;
        Texture2D laserTexture;
        SpriteFont font;
        bool actionExecutedSpace = false;
        bool actionExecutedR = false;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferHeight = 1440;
            _graphics.PreferredBackBufferWidth = 2560;
            _graphics.IsFullScreen = true;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            spaceShipTexture = Content.Load<Texture2D>("spaceship");
            asteroidTexture = Content.Load<Texture2D>("ball");
            laserTexture = Content.Load<Texture2D>("laser");
            font = Content.Load<SpriteFont>("File");
        }

        protected override void Initialize()
        {
            base.Initialize();
            Object spaceShip = new Object();
            spaceShip.texture = spaceShipTexture;
            spaceShip.type = 's';
            objects.Add(spaceShip);
            textManegement.SetScore();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            foreach (Object item in objects)
            {
                item.Update(gameTime);
            }
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Space) && actionExecutedSpace)
            {
                textManegement.AddScore();
                actionExecutedSpace = true;
            }
            else
            {
                actionExecutedSpace = false;
            }

            if (kstate.IsKeyDown(Keys.R))
            {
                if (textManegement.state == 'g' && !actionExecutedR)
                {
                    textManegement.GameOver();
                    actionExecutedR = true;
                }
                else if (textManegement.state == 'o' && !actionExecutedR)
                {
                    textManegement.SetScore();
                    actionExecutedR = true;
                }
            }
            else
            {
                actionExecutedR = false;
            }

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            _spriteBatch.DrawString(font, textManegement.currentText, new Vector2(200, 200), Color.Red);
            foreach (Object item in objects)
            {
                _spriteBatch.Draw(
                item.texture,
                item.position,
                null,
                Color.White,
                0f,
                new Vector2(item.texture.Width /2, item.texture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f
                );
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}