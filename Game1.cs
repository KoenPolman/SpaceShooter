using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Threading;

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
        int playerCount = 1;
        double whenDeployedTutorial;

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
        }

        protected override void Update(GameTime gameTime)
        {
            textManegement.UIText(playerCount);
            var kstate = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kstate.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (textManegement.state == 'm' && (kstate.IsKeyDown(Keys.D1) || kstate.IsKeyDown(Keys.D2) || kstate.IsKeyDown(Keys.D3)))
            {
                Object spaceShipOne = new Object();
                spaceShipOne.texture = spaceShipTexture;
                spaceShipOne.type = '1';
                objects.Add(spaceShipOne);
                if (kstate.IsKeyDown(Keys.D2) || kstate.IsKeyDown(Keys.D3))
                {
                    Object spaceShipTwo = new Object();
                    spaceShipTwo.texture = spaceShipTexture;
                    spaceShipTwo.type = '2';
                    objects.Add(spaceShipTwo);
                    playerCount++;
                }
                if (kstate.IsKeyDown(Keys.D3))
                {
                    Object spaceShipThree = new Object();
                    spaceShipThree.texture = spaceShipTexture;
                    spaceShipThree.type = '3';
                    objects.Add(spaceShipThree);
                    playerCount++;
                }
                textManegement.state = 't';
                whenDeployedTutorial = gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (textManegement.state == 't' && whenDeployedTutorial + 10 <= gameTime.TotalGameTime.TotalSeconds)
            {
                textManegement.state = 'g';
            }

            if (kstate.IsKeyDown(Keys.R) && textManegement.state == 'g')
            {
                textManegement.state = 'm';
                objects.Clear();
            }

            foreach (Object item in objects)
            {
                item.Update(gameTime);
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
                item.rotation,
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