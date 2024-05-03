using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    internal class Controls
    {
        int playerIndex;
        public Controls(int playerindex)
        {
            playerIndex = playerindex;
        }
        public bool[] GetCurrentInput()
        {
            bool[] currentinput = new bool[5];
            var kstate = Keyboard.GetState();

            bool[,] controlScheme = new bool[,] {
            { kstate.IsKeyDown(Keys.W), kstate.IsKeyDown(Keys.S), kstate.IsKeyDown(Keys.A), kstate.IsKeyDown(Keys.D), kstate.IsKeyDown(Keys.C) },  //control scheme for player 1
            { kstate.IsKeyDown(Keys.I), kstate.IsKeyDown(Keys.K), kstate.IsKeyDown(Keys.J), kstate.IsKeyDown(Keys.L), kstate.IsKeyDown(Keys.M) },  //control scheme for player 2
            { kstate.IsKeyDown(Keys.T), kstate.IsKeyDown(Keys.G), kstate.IsKeyDown(Keys.F), kstate.IsKeyDown(Keys.H), kstate.IsKeyDown(Keys.N) } };//control scheme for player 3
            
            for (int i = 0; i < currentinput.Length; i++)
            {
                currentinput[i] = controlScheme[i , playerIndex];
            }

            return currentinput;
        }
    }
}
