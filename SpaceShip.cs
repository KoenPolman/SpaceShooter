using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Reflection;

namespace SpaceShooter
{
    internal class SpaceShip : Object //holy shit inheritance, no wayy
    {
        Vector2 windowSize;
        Controls controls;
        private bool[] currentInput;
        public int playerIndex;
        public SpaceShip(Texture2D newTexture, Vector2 startingPosition, Vector2 currentWindowSize, float startingRotation, int newPlayerIndex, int amountOfPlayers) : base(newTexture, startingPosition, startingRotation)
        {
            windowSize = currentWindowSize;
            playerIndex = newPlayerIndex;
            controls = new Controls(newPlayerIndex);
            Vector2 position = new Vector2();
            position.Y = windowSize.Y / 2;
            position.X = windowSize.X / (amountOfPlayers + 1) * (playerIndex + 1);
        }
        public void Update(GameTime gameTime)
        {
            Control();
            UpdateObject(gameTime, windowSize);
            controls.GetCurrentInput();
        }
        private void Control()
        {
            if (currentInput[0]) //move forwards
            {
                momentumDir.Y -= baseSpeed * (float)Math.Cos(rotation); //adds momentum in the direction the player points in
                momentumDir.X -= baseSpeed * (float)Math.Sin(rotation);
            }
            else if (currentInput[1]) //move backwards
            {
                momentumDir.Y += baseSpeed * (float)Math.Cos(rotation); //adds momentum in the opposit direction the player points to
                momentumDir.X += baseSpeed * (float)Math.Sin(rotation);
            }
            else //slow down to a halt when not steered
            {
                if (momentumDir.Y > 0) { momentumDir.Y--; }
                if (momentumDir.Y < 0) { momentumDir.Y++; }
                if (momentumDir.X > 0) { momentumDir.X--; }
                if (momentumDir.X < 0) { momentumDir.X++; }
            }
            if (currentInput[2]) //rotate left
            {
                rotation -= rotationSpeed;
            }
            if (currentInput[3]) //rotate right
            {
                rotation += rotationSpeed;
            }
            if (currentInput[4] && !readyToFire) //fire laser
            {
                fireBullet = true;
                readyToFire = true;
            }
            else if (!currentInput[4] && readyToFire) //reset the boolean (its a t flip-flop)
            {
                readyToFire = false;
            }
        }
    }
}
