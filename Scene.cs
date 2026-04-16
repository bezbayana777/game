

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace  MyGame;

public interface IScene
{
    public void Load();
    
    public void Update(GameTime gameTime);
    
    public void Draw(SpriteBatch spriteBatch);
}