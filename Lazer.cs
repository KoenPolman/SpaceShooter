using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    internal class Lazer : Object
    {
        void start()
        {
            //the first update the bullet is launched in the direction matching the players it originates from
            momentumDir.Y -= 3000 * (float)Math.Cos(rotation);
            momentumDir.X -= 3000 * (float)Math.Sin(rotation);
        }
        protected bool armed = false;
        void Update()
        {
            if (!armed && lifeTimeTracker >= 0.05)
            {
                armed = true;
            }
        }
    }
}
