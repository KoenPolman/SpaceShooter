using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;

public class SpaceShip
{
    public Vector2 Position = new Vector2(0, 0);
    float baseSpeed = 3;
    float acceleration;
    float MomentumX;
    float MomentumY;
    public SpaceShip()
	{
		void update(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.LeftShift))
            {
                acceleration = baseSpeed * 2;
            }
            else
            {
                acceleration = baseSpeed;
            }
            if (kstate.IsKeyDown(Keys.Up))
            {
                MomentumY -= acceleration;
            }
            else if (MomentumY <= 0)
            {
                MomentumY += acceleration;
            }
            if (kstate.IsKeyDown(Keys.Down))
            {
                MomentumY += acceleration;
            }
            else if (MomentumY >= 0)
            {
                MomentumY -= acceleration;
            }
            if (kstate.IsKeyDown(Keys.Left))
            {
                MomentumX -= acceleration;
            }
            else if (MomentumX <= 0)
            {
                MomentumX += acceleration;
            }
            if (kstate.IsKeyDown(Keys.Right))
            {
                MomentumX += acceleration;
            }
            else if (MomentumX >= 0)
            {
                MomentumX -= acceleration;
            }
            Position.Y += MomentumY * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position.X += MomentumX * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
	}
}
