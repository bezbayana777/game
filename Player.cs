

using System;
using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace  MyGame
{
    internal class Player: Sprite
    {
        private Dictionary<string, AnimationManager> _animations;
        private string _playerState = "idle";
        private ContentManager _content;
        public Player(Vector2 position, ContentManager content) : base(null, position)
        {
            _content = content;
            
            LoadAnimations();
        }

        public void LoadAnimations()
        {
            _animations = new Dictionary<string, AnimationManager>();
            var idleSpriteSheet = _content.Load<Texture2D>("images/hero/lancer_idle");
            var idleAnimation = new AnimationManager(12, 12, new Vector2(70, 175), 250);
            this.texture = idleSpriteSheet;
            _animations.Add("idle", idleAnimation);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_animations != null && _animations.ContainsKey(_playerState))
            {
                var frame = _animations[_playerState].GetFrame();
                spriteBatch.Draw(texture, position, frame, Color.White);
            }
            else if (texture != null)
            {
                spriteBatch.Draw(texture, position, Color.White);
            }
        }
        private AnimationManager GetAnimation(string name)
        {
            return _animations[name];
        }
        public override void Update(GameTime gameTime)
        {
            var currentState = Keyboard.GetState();
            if (_animations != null && _animations.ContainsKey(_playerState))
            {
                _animations[_playerState].Update();
            }
            
            if ((currentState.IsKeyDown(Keys.D) || currentState.IsKeyDown(Keys.Left)))
            {
                Console.WriteLine("hero go to left!");
                position.X -= 3;
            }

            if ((currentState.IsKeyDown(Keys.A) || currentState.IsKeyDown(Keys.Right)))
            {
                Console.WriteLine("hero go to right!");
                position.X += 3;
            }

            if ((currentState.IsKeyDown(Keys.W) || currentState.IsKeyDown(Keys.Up)))
            {
                Console.WriteLine("hero go to up!");
                position.Y -= 3;
            }

            if ((currentState.IsKeyDown(Keys.S) || currentState.IsKeyDown(Keys.Down)))
            {
                Console.WriteLine("hero go to down!");
                position.Y += 3;
            }
        }
        
    }
    
    
}

