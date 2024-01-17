using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
public class Object
{
    public char type;

    public Vector2 position = new Vector2(0, 0);
    public float rotation = 0;

    public Texture2D texture;

    Vector2 momentum = new Vector2(0, 0);
    Vector2 directionModifyer = new Vector2(0, 0);
    float baseSpeed = 3;
    float rotationSpeed = 0.1f;
    float acceleration;
    public void Update(GameTime gameTime)
    {
        if (type == '1' || type == '2' || type == '3')
        {
            Control1();
        }
        directionModifyer.Y;
        directionModifyer.X;

        position.Y += momentum.Y * directionModifyer.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
        position.X += momentum.X * directionModifyer.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
    private void Control1()
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
        if (kstate.IsKeyDown(Keys.W))
        {
            momentum.Y -= acceleration;
        }
        else if (momentum.Y <= 0)
        {
            momentum.Y += acceleration;
        }
        if (kstate.IsKeyDown(Keys.S))
        {
            momentum.Y += acceleration;
        }
        else if (momentum.Y >= 0)
        {
            momentum.Y -= acceleration;
        }
        if (kstate.IsKeyDown(Keys.A))
        {
            rotation -= rotationSpeed;
        }
        if (kstate.IsKeyDown(Keys.D))
        {
            rotation += rotationSpeed;
        }
    }
}
