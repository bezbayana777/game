using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

internal class Sprite
{
    public Texture2D texture;
    public Vector2 position;

    public Sprite(Texture2D texture, Vector2 position)
    {
        this.texture = texture;
        this.position = position;
    }

    public Rectangle Rectangle
    {
        get
        {
            return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }
    }
    public virtual void Update(GameTime gameTime)
    {
       
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, position , Color.White);
    }
}