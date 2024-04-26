using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    internal class SpaceShip : Object
    {
        private bool[,] controlScheme = null;

        public int playerIndex = 3;
        public void Start()
        {

        }
        public void Update(GameTime gameTime, int windowHeigth, int windowWidth)
        {
            var kstate = Keyboard.GetState(); //BEHOLD ... the control scheme
            controlScheme = new bool[,] {
            { kstate.IsKeyDown(Keys.W), kstate.IsKeyDown(Keys.S), kstate.IsKeyDown(Keys.A), kstate.IsKeyDown(Keys.D), kstate.IsKeyDown(Keys.C) },  //control scheme for player 1
            { kstate.IsKeyDown(Keys.I), kstate.IsKeyDown(Keys.K), kstate.IsKeyDown(Keys.J), kstate.IsKeyDown(Keys.L), kstate.IsKeyDown(Keys.M) },  //control scheme for player 2
            { kstate.IsKeyDown(Keys.T), kstate.IsKeyDown(Keys.G), kstate.IsKeyDown(Keys.F), kstate.IsKeyDown(Keys.H), kstate.IsKeyDown(Keys.N) } };//control scheme for player 3

            Control();
            UpdateObject(gameTime, windowHeigth, windowWidth);
        }
        private void Control()
        {
            if (controlScheme[playerIndex, 0]) //move forwards
            {
                momentumDir.Y -= baseSpeed * (float)Math.Cos(rotation); //adds momentum in the direction the player points in
                momentumDir.X -= baseSpeed * (float)Math.Sin(rotation);
            }
            else if (controlScheme[playerIndex, 1]) //move backwards
            {
                momentumDir.Y += baseSpeed * (float)Math.Cos(rotation); //adds momentum in the opposit direction the player points to
                momentumDir.X += baseSpeed * (float)Math.Sin(rotation);
            }
            else //slow down to a halt
            {
                if (momentumDir.Y > 0) { momentumDir.Y--; }
                if (momentumDir.Y < 0) { momentumDir.Y++; }
                if (momentumDir.X > 0) { momentumDir.X--; }
                if (momentumDir.X < 0) { momentumDir.X++; }
            }
            if (controlScheme[playerIndex, 2]) //rotate left
            {
                rotation -= rotationSpeed;
            }
            if (controlScheme[playerIndex, 3]) //rotate right
            {
                rotation += rotationSpeed;
            }
            if (controlScheme[playerIndex, 4] && !fired) //fire laser
            {
                fireBullet = true;
                fired = true;
            }
            else if (!controlScheme[playerIndex, 4] && fired) //reset the boolean (its a t flip-flop)
            {
                fired = false;
            }
        }
    }
}
