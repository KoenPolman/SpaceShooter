using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
public class Object
{
    public Texture2D texture;
    public char type;
    int playerIndex = 3;

    private bool[,] controlScheme = null;

    private Vector2 momentumDir = new Vector2(0, 0);
    public  Vector2 position = new Vector2(0, 0);
    public  float   rotation = 0;
    private float   baseSpeed = 3f;
    private float   rotationSpeed = 0.1f;
    public  bool    fire = false;
    public void Start(Texture2D newTexture, char newType, Vector2 newPosition, float newRotation, int newPlayerIndex)
    {
        texture     = newTexture;
        type        = newType;
        position    = newPosition;
        rotation    = newRotation;
        playerIndex = newPlayerIndex;
    }
    public void Update(GameTime gameTime, int windowHeigth, int windowWidth)
    {
        var kstate = Keyboard.GetState(); //BEHOLD ... the control scheme
        controlScheme = new bool[,] {
        { kstate.IsKeyDown(Keys.W), kstate.IsKeyDown(Keys.S), kstate.IsKeyDown(Keys.A), kstate.IsKeyDown(Keys.D), kstate.IsKeyDown(Keys.C) },  //control scheme for player 1
        { kstate.IsKeyDown(Keys.I), kstate.IsKeyDown(Keys.K), kstate.IsKeyDown(Keys.J), kstate.IsKeyDown(Keys.L), kstate.IsKeyDown(Keys.M) },  //control scheme for player 2
        { kstate.IsKeyDown(Keys.T), kstate.IsKeyDown(Keys.G), kstate.IsKeyDown(Keys.F), kstate.IsKeyDown(Keys.H), kstate.IsKeyDown(Keys.N) } };//control scheme for player 3

        if (type == 's')
        {
            Control();
        }

        position.Y += momentumDir.Y * (float)gameTime.ElapsedGameTime.TotalSeconds; //calculates the position of the player
        position.X -= momentumDir.X * (float)gameTime.ElapsedGameTime.TotalSeconds; //by taking the current position and adding the momentum taking time between frames into account

        if (position.Y < 0) { position.Y = windowHeigth; } //these four lines take care of wrapping
        if (position.Y > windowHeigth) { position.Y = 0; } //meaning if the player goes over the edge of the window they appear on the other side
        if (position.X < 0) {  position.X = windowWidth; }
        if (position.X > windowWidth)  { position.X = 0; }
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
            momentumDir.Y += baseSpeed * (float)Math.Cos(rotation);
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
        if (controlScheme[playerIndex, 4]) //fire laser
        {
            fire = true;
        }
    }
}
