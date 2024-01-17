using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
public class Object
{
    public char type; //type geeft aan wat voor object het is
                      //op het moment van schrijven is dit
                      //1 t/m 3 voor het ruimte schip
                      //m voor comeet/meteor
                      //en l voor laser
    public Vector2 position = new Vector2(0,0);
    public Texture2D texture;
    float baseSpeed = 3;
    float acceleration;
    float MomentumX;
    float MomentumY;
    public void Update(GameTime gameTime)
    {
        if (type == '1' || type == '2' || type == '3')
        {
            Control();
        }
        position.Y += MomentumY * (float)gameTime.ElapsedGameTime.TotalSeconds;
        position.X += MomentumX * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
    private void Control()
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
    }
}
