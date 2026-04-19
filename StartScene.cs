
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MyGame;

public class StartScene: IScene
{

    private ContentManager _contentManager;
    private SceneManager _sceneManager;
    private GraphicsDeviceManager _graphics;
    private Texture2D _background;
    private Song _song;

    public StartScene(ContentManager contentManager, SceneManager sceneManager, GraphicsDeviceManager graphics)
    {
        _sceneManager =  sceneManager;
        _contentManager = contentManager;
        _graphics = graphics;
    }
    public void Load()
    {
        _song = _contentManager.Load<Song>("audio/start-scene-bg");
        MediaPlayer.Play(_song);
        _background = _contentManager.Load<Texture2D>("images/bg");
        
    }

    public void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Space))
        {
            _sceneManager.AddScene(new GameScene(_contentManager, _graphics));
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_background, new Rectangle(0, 0, _background.Width, _background.Height), Color.White);
    }
}