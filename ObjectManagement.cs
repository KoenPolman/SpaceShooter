using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpaceShooter
{
    internal class ObjectManagement
    {
        public List<SpaceShip> spaceShips = new List<SpaceShip>();
        public List<Lazer>     lazers     = new List<Lazer>(); //fun fact: "laser" is a abreviation for "Light Amplification by Stimulated Emission of Radiation" but is spelled intentionally with a z because: cool
        public List<Asteroid>  asteroids  = new List<Asteroid>();

        Vector2 windowSize;

        public ObjectManagement(int currentWindowHeigth, int currentWindowWidth)
        {
            windowSize.X = currentWindowWidth;
            windowSize.Y = currentWindowHeigth;
        }

        public void UpdateObjects(GameTime gameTime)
        {
            foreach (SpaceShip activeSpaceShip in spaceShips)
            {
                activeSpaceShip.Update(gameTime);
            }
            foreach (Lazer activeLazer in lazers)
            {
                activeLazer.Update();
            }
            foreach (Asteroid activeAsteroid in asteroids)
            {
                //activeAsteroid.Update();
            }
        }
        public void ClearEveryThing()
        {
            spaceShips.Clear();
            lazers.Clear();
            asteroids.Clear();
        }
        public void PutPlayersInStartingPosition(int playerCount, Texture2D spaceShipTexture)
        {
            {
                ClearEveryThing();
                for (int i = 0; i < playerCount; i++)
                {
                    int playerIndex = i;
                    SpaceShip spaceShip = new SpaceShip(spaceShipTexture, new Vector2(0,0), windowSize, 0, i, playerCount);
                    spaceShips.Add(spaceShip);
                }
            }
        }
    }
}
