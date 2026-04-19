using System;
using System.Collections.Generic;
using Gum.Forms.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGameGum;
using MyGame.Screens;

namespace MyGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    GumService GumUI => GumService.Default;
    private SpriteBatch _spriteBatch;
    private SceneManager _sceneManager;

    public Button button;
    public int clickCount;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _graphics.PreferredBackBufferWidth = 1200;
        _graphics.PreferredBackBufferHeight = 900;
        _graphics.ApplyChanges();
        _sceneManager = new();
    }

    protected override void Initialize()
    {
        var gumProject = GumUI.Initialize(this,
            // This is relative to Content:
            "ui/ui.gumx");
        
        // Получаем экран
        var screen = gumProject.Screens.Find(item => item.Name == "TitleScreen").ToGraphicalUiElement();
        
        button = new Button();
        button.AddToRoot();
        button.X = 50;
        button.Y = 50;
        button.Width = 100;
        button.Height = 50;
        button.Text = "Hello MonoGame!";
        clickCount = 0;
        button.Click += (sender, args) =>
        {
            clickCount++;
            button.Text = $"Clicked {clickCount} times";
        };
        ///screen.AddToRoot();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
       // _sceneManager.AddScene(new StartScene(Content, _sceneManager, _graphics));
        
        
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        //_sceneManager.GetCurrentScene().Update(gameTime);
        GumUI.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Beige);
        
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        
        
        // All textures and animations
       // _sceneManager.GetCurrentScene().Draw(_spriteBatch);
        
        _spriteBatch.End();
        GumUI.Draw();
        
        base.Draw(gameTime);
    }
}