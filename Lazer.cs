using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace SpaceShooter
{
    internal class Lazer : Object
    {
        protected bool armed = false;
        private Lazer(Texture2D newTexture, Vector2 startingPosition, float startingRotation) : base(newTexture, startingPosition, startingRotation)
        {
            momentumDir.Y -= 3000 * (float)Math.Cos(rotation);
            momentumDir.X -= 3000 * (float)Math.Sin(rotation);
        }
        public void Update()
        {
            if (!armed && lifeTimeTracker >= 0.05)
            {
                armed = true;
            }
        }
    }
}
