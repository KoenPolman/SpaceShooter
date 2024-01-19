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
            var kstate = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kstate.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (textManegement.state == 'm' && (kstate.IsKeyDown(Keys.D1) || kstate.IsKeyDown(Keys.D2) || kstate.IsKeyDown(Keys.D3)))
            {
                if (kstate.IsKeyDown(Keys.D1)) { playerCount = 1; }
                if (kstate.IsKeyDown(Keys.D2)) { playerCount = 2; }
                if (kstate.IsKeyDown(Keys.D3)) { playerCount = 3; }
                for (int i = 0; i < playerCount; i++)
                {
                    int playerIndex = i; 
                    Object spaceShip = new Object();
                    spaceShip.Start(spaceShipTexture, 's', playerPlacement(playerIndex,playerCount),90 * i,playerIndex);
                    objects.Add(spaceShip);
                }
                textManegement.state = 't';
                whenDeployedTutorial = gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (textManegement.state == 't' && whenDeployedTutorial + 10 <= gameTime.TotalGameTime.TotalSeconds)
            {
                textManegement.state = 'g';
            }

            if (kstate.IsKeyDown(Keys.R) && (textManegement.state == 'g' || textManegement.state == 't'))
            {
                textManegement.state = 'm';
                objects.Clear();
            }

            foreach (Object item in objects)
            {
                item.Update(gameTime, _graphics.PreferredBackBufferHeight, _graphics.PreferredBackBufferWidth);
                if (item.fire)
                {

                }
            }
            textManegement.UIText(playerCount);

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            _spriteBatch.DrawString(font, textManegement.currentText, textManegement.position, Color.White);
            foreach (Object item in objects)
            {
                _spriteBatch.Draw(
                item.texture,
                item.position,
                null,
                Color.White,
                item.rotation,
                new Vector2(item.texture.Width / 2, item.texture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f
                );
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        private Vector2 playerPlacement(int index, int count)
        {
            Vector2 position = new Vector2();
            switch (playerCount)
            {
                case 1:
                    position.Y = _graphics.PreferredBackBufferHeight / 2;
                    position.X = _graphics.PreferredBackBufferWidth / 2;
                    break;
                case 2:
                    position.Y = _graphics.PreferredBackBufferHeight / 2;
                    if (index == 0)
                    {
                        position.X = _graphics.PreferredBackBufferWidth / 3 * 1;
                    }
                    else
                    {
                        position.X = _graphics.PreferredBackBufferWidth / 3 * 2;
                    }
                    break;
                case 3:
                    if (index == 0)
                    {
                        position.Y = _graphics.PreferredBackBufferHeight / 3 * 1;
                    }
                    else
                    {
                        position.Y = _graphics.PreferredBackBufferHeight / 3 * 2;
                    }
                    switch (index)
                    {
                        case 0:
                            position.X = _graphics.PreferredBackBufferWidth / 4 * 2;
                            break;
                        case 1:
                            position.X = _graphics.PreferredBackBufferWidth / 4 * 3;
                            break;
                        case 2:
                            position.X = _graphics.PreferredBackBufferWidth / 4 * 1;
                            break;
                    }
                    break;
            }
            return position;
        }
    }
}