using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private List<Sprite> _sprites;
    private Texture2D _enemySpriteSheet;

    private Player _player;
    private AnimationManager _gorgonIdle;
    

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _sprites = new();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        var heroTexture = Content.Load<Texture2D>("images/hero/lancer_idle");
        _player = new Player(heroTexture, Vector2.Zero);
        _enemySpriteSheet  = Content.Load<Texture2D>("images/enemy/gorgon_idle");
        _gorgonIdle = new AnimationManager(7, 7, new Vector2(65, 87));
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _gorgonIdle.Update();

        // TODO: Add your update logic here
        _player.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Beige);
        
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        _spriteBatch.Draw(_player.texture, _player.position, Color.White);
        _spriteBatch.Draw(_enemySpriteSheet, new Rectangle(100, 100, 65, 87), _gorgonIdle.GetFrame(), Color.White );
        _spriteBatch.End();
        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}