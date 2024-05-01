using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
public class Object
{
    public Texture2D texture;

    protected Vector2 momentumDir          = new Vector2(0, 0);
    public    Vector2 position             = new Vector2(0, 0);
    public    float   rotation             = 0;
    protected float   baseSpeed            = 3f;
    protected float   rotationSpeed        = 0.1f;
    public    bool    markedForDestruction = false;
    public    bool    fireBullet           = false;
    protected bool    readyToFire          = false;
    protected float   lifeTimeTracker;
    public void Start(Texture2D newTexture, char newType, Vector2 newPosition, float newRotation, int newPlayerIndex)
    {
        texture     = newTexture;
        position    = newPosition;
        rotation    = newRotation;
    }
    public void UpdateObject(GameTime gameTime, int windowHeigth, int windowWidth)
    {
        if (lifeTimeTracker == null)
        {
            lifeTimeTracker = 0; 
        }

        position.Y += momentumDir.Y * (float)gameTime.ElapsedGameTime.TotalSeconds; //calculates the position of the player
        position.X -= momentumDir.X * (float)gameTime.ElapsedGameTime.TotalSeconds; //by taking the current position and adding the momentum taking time between frames into account

        if (position.Y < 0) { position.Y = windowHeigth; } //these four lines take care of wrapping
        if (position.Y > windowHeigth) { position.Y = 0; } //meaning if the object goes over the edge of the window they appear on the other side
        if (position.X < 0) { position.X = windowWidth; }
        if (position.X > windowWidth) { position.X = 0; }

        //made a separate tracker bc i couldnt get the gameTime.TotalGameTime.TotalSeconds to work here :(
        lifeTimeTracker += 1 * (float)gameTime.ElapsedGameTime.TotalSeconds; 
    }
}