

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MyGame;

public class GameScene : IScene
{
    
    private ContentManager _contentManager;
    private GraphicsDeviceManager _graphics;
    private List<Sprite> _sprites;
    private Texture2D _enemySpriteSheet;

    private Player _player;
    private SpriteFont _font;
    private List<EnemyData> _enemies; // Храним и анимацию, и позицию
    private int _coins = 0;

    private Song _song;
    
    private KeyboardState _prevKBDstate = Keyboard.GetState();
    
    public GameScene(ContentManager contentManager)
    {
        _contentManager = contentManager;
        _enemies = new List<EnemyData>();
    }

    public void Load()
    {
        _player = new Player(new Vector2(300, 400), _contentManager);
        _enemySpriteSheet = _contentManager.Load<Texture2D>("images/enemy/gorgon_idle");
        _song = _contentManager.Load<Song>("audio/knife");
        
        
        var gap = 100;
        for (var i = 0; i < 10; i++)
        {
            var animation = new AnimationManager(7, 7, new Vector2(65, 87), 63);
            // Сохраняем позицию каждого врага
            Vector2 position = new Vector2(gap * i, gap * i);
            _enemies.Add(new EnemyData(animation, position));
        }

        MediaPlayer.Play(_song);
        _font = _contentManager.Load<SpriteFont>("fonts/chicle-font");
    }



    public void Update(GameTime gameTime)
    {
        
        
        var currKBDstate = Keyboard.GetState();
        if (currKBDstate.IsKeyDown(Keys.P) && !(_prevKBDstate.IsKeyUp(Keys.P)))
        {
            MediaPlayer.Pause();
        }

        if (currKBDstate.IsKeyDown(Keys.R) && _prevKBDstate.IsKeyUp(Keys.R))
        {
            MediaPlayer.Resume();
        }
        _prevKBDstate = currKBDstate;
        // Обновляем анимации всех врагов
        foreach (var enemy in _enemies)
        {
            enemy.Animation.Update();
        }

        var heroRect = _player.Rectangle;
        
        // Проверяем столкновения (идем с конца, чтобы безопасно удалять)
        for (var i = _enemies.Count - 1; i >= 0; i--)
        {
            var enemy = _enemies[i];
            var enemyRect = new Rectangle(
                (int)enemy.Position.X, 
                (int)enemy.Position.Y, 
                65, 87
            );
            
            if (heroRect.Intersects(enemyRect))
            {
                _enemies.RemoveAt(i);
                _coins++; // Увеличиваем счет монет
                Console.WriteLine($"Collision! Coins: {_coins}");
            }
        }

        _player.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // Рисуем игрока
        _player.Draw(spriteBatch);
        
        // Рисуем всех врагов
        foreach (var enemy in _enemies)
        {
            spriteBatch.Draw(
                _enemySpriteSheet, 
                new Rectangle((int)enemy.Position.X, (int)enemy.Position.Y, 65, 87), 
                enemy.Animation.GetFrame(), 
                Color.White
            );
        }
        
        // Рисуем счет монет
        spriteBatch.DrawString(_font, $"Coins: {_coins}", new Vector2(10, 10), Color.Red);
        
    }


    private class EnemyData
    {
        public AnimationManager Animation { get; set; }
        public Vector2 Position { get; set; }

        public EnemyData(AnimationManager animation, Vector2 position)
        {
            Animation = animation;
            Position = position;
        }
    }
}
