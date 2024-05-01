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
        List<SpaceShip> spaceShips = new List<SpaceShip>();
        List<Lazer>     lazers     = new List<Lazer>(); //fun fact: "laser" is een afkorting voor "Light Amplification by Stimulated Emission of Radiation" maar word hier met een z gespeld want cool
        List<Asteroid>  asteroids  = new List<Asteroid>();
        List<Texture2D> textures   = new List<Texture2D>();
        TextManagement textManagement = new TextManagement();
        
        SpriteFont font;
        int playerCount = 1;
        double whenDeployedTutorial;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.PreferredBackBufferWidth = 2000;
            _graphics.IsFullScreen = false;
        }

        protected override void LoadContent()
        {
            textures.Add(Content.Load<Texture2D>("spaceship"));
            textures.Add(Content.Load<Texture2D>("ball"));
            textures.Add(Content.Load<Texture2D>("laser"));
            _spriteBatch = new SpriteBatch(GraphicsDevice);
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

            if (textManagement.state == 'm' && (kstate.IsKeyDown(Keys.D1) || kstate.IsKeyDown(Keys.D2) || kstate.IsKeyDown(Keys.D3)))
            {   //the gameplay starts, depending on wich button is pressed the amount of players will play
                if (kstate.IsKeyDown(Keys.D1)) { playerCount = 1; }
                if (kstate.IsKeyDown(Keys.D2)) { playerCount = 2; }
                if (kstate.IsKeyDown(Keys.D3)) { playerCount = 3; }
                StartRound();
                textManagement.state = 't'; //t stands for tutorial
                whenDeployedTutorial = gameTime.TotalGameTime.TotalSeconds; //registers when the tutorial is deployed to be removed within a certain amount of seconds
            }

            if (textManagement.state == 't' && whenDeployedTutorial + 5 <= gameTime.TotalGameTime.TotalSeconds)
            {
                textManagement.state = 'g'; // g stands for game

                textManagement.ResetScore();
            }

            if (kstate.IsKeyDown(Keys.R) && (textManagement.state == 'g' || textManagement.state == 't'))
            {   //return to the mainmenu
                spaceShips.Clear(); //makes the plaing area empty
                lazers.Clear();

                textManagement.state = 'm'; //m stands for menu/mainmenu
                textManagement.ResetScore();
            }

            for (int j = 0; j < objects.Count; j++)
            {   //in this for loop items in the list "objects" are checked if they need to be removed or are in a certain amount of proximity of eachother
                if (objects[j].markedForDestruction)
                {
                    objects.RemoveAt(j); //removes the object
                }
                else
                {
                    //objects[j].Update(gameTime, _graphics.PreferredBackBufferHeight, _graphics.PreferredBackBufferWidth);
                    if (objects[j].fireBullet)
                    {   //here a new bullet is created and added to the list to be renderd
                        Object bullet = new Object();
                        bullet.Start(laserTexture, 'b', objects[j].position, objects[j].rotation, 3);
                        objects.Add(bullet);
                        objects[j].fireBullet = false;
                    }

                    if (objects[j].armed)
                    {   //if the object is a armed laser check for proximity to players
                        for (int i = 0; i < objects.Count; i++)
                        {
                            if (objects[i].type == 's' && i != j)
                            {   //check for proximity to players
                                float distance = Vector2.Distance(objects[j].position, objects[i].position);
                                float distanceThreshold = 50.0f;
                                float angleThreshold = 30.0f;

                                if (distance < distanceThreshold)
                                {
                                    float angle = MathHelper.ToDegrees((float)Math.Atan2(objects[i].position.Y - objects[j].position.Y, objects[i].position.X - objects[j].position.X));
                                    angle = MathHelper.WrapAngle(angle);
                                    if (Math.Abs(angle - objects[j].rotation) < angleThreshold)
                                    {   //if the proximity checks pass the object will be destroyed
                                        objects.RemoveAt(i);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            int activePlayers = 0;
            foreach (Object obj in objects)
            {   //checks for the amount of active players
                if (obj.type == 's')
                {
                    activePlayers++;
                }
            }
            if (activePlayers == 1 && textManagement.state == 'g' || textManagement.state == 't')
            {   //if the amount of active players is one they will be awarded a point and a new round starts
                textManagement.AddScore(objects[0].playerIndex);
                StartRound();
            }
            if (activePlayers == 0 && textManagement.state == 'g' || textManagement.state == 't')
            {   //if the amount of active players is zero (very unlikely) the game will just reset with no points awarded
                StartRound();
            }

            textManagement.UIText(playerCount);

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            _spriteBatch.DrawString(font, textManagement.currentText, textManagement.position, Color.White);
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
        {   //place the players in a nice tidy line
            Vector2 position = new Vector2();
            position.Y = _graphics.PreferredBackBufferHeight / 2;
            position.X = _graphics.PreferredBackBufferWidth / (count + 1) * (index + 1);
            return position;
        }
        private void StartRound()
        {   //create the players to start the round
            objects.Clear();
            for (int i = 0; i < playerCount; i++)
            {
                int playerIndex = i;
                Object spaceShip = new Object();
                spaceShip.Start(spaceShipTexture, 's', playerPlacement(playerIndex, playerCount), 0, playerIndex);
                objects.Add(spaceShip);
            }
        }
    }
}