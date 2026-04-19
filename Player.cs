using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    internal class Player : Sprite
    {
        private Dictionary<string, AnimationManager> _animations;
        private Dictionary<string, Texture2D> _textures;
        private string _playerState = "idle";
        private string _previousState = "idle";
        private ContentManager _content;
        private float _attackTimer = 0f;
        private float _attackDuration = 0.4f; // Увеличил длительность
        private bool _animationComplete = true;
        
        
        
        public bool isAttacking = false;
        
        
        // Смещения для разных анимаций (чтобы персонаж не прыгал)
        private Dictionary<string, Vector2> _offsets;
        
        public Player(Vector2 position, ContentManager content) : base(null, position)
        {
            _content = content;
            LoadAnimations();
        }

        public void LoadAnimations()
        {
            _animations = new Dictionary<string, AnimationManager>();
            _textures = new Dictionary<string, Texture2D>();
            _offsets = new Dictionary<string, Vector2>();
            
            // Idle анимация
            var idleSpriteSheet = _content.Load<Texture2D>("images/hero/lancer_idle");
            var idleAnimation = new AnimationManager(12, 12, new Vector2(70, 175), 250);
            _textures.Add("idle", idleSpriteSheet);
            _animations.Add("idle", idleAnimation);
            _offsets.Add("idle", Vector2.Zero);

            // Down attack анимация
            var downAttackTexture = _content.Load<Texture2D>("images/hero/attack/down");
            var downAttackAnimation = new AnimationManager(3, 3, new Vector2(45, 166), 275); 
            _textures.Add("down_attack", downAttackTexture);
            _animations.Add("down_attack", downAttackAnimation);
            _offsets.Add("down_attack", new Vector2(12, 5)); 

            // Right attack анимация
            var rightAttackTexture = _content.Load<Texture2D>("images/hero/attack/right");
            var rightAttackAnimation = new AnimationManager(3, 3, new Vector2(163, 78), 150);
            _textures.Add("right_attack", rightAttackTexture);
            _animations.Add("right_attack", rightAttackAnimation);
            _offsets.Add("right_attack", new Vector2(-46, 48)); 

            // Up attack анимация
            var upAttackTexture = _content.Load<Texture2D>("images/hero/attack/up");
            var upAttackAnimation = new AnimationManager(3, 3, new Vector2(48, 151), 270);
            _textures.Add("up_attack", upAttackTexture);
            _animations.Add("up_attack", upAttackAnimation);
            _offsets.Add("up_attack", new Vector2(11, 12)); 
            
            this.texture = idleSpriteSheet;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 cameraPosition)
        {
            if (_animations != null && _animations.ContainsKey(_playerState))
            {
                var frame = _animations[_playerState].GetFrame();
                var currentTexture = _textures[_playerState];
                
                // Применяем смещение для центрирования
                Vector2 offset = _offsets.ContainsKey(_playerState) ? _offsets[_playerState] : Vector2.Zero;
                Vector2 drawPosition = position - cameraPosition + offset;
                
                spriteBatch.Draw(currentTexture, drawPosition, frame, Color.White);
            }
            else if (texture != null)
            {
                spriteBatch.Draw(texture, position - cameraPosition, Color.White);
            }
        }
        
        public override void Update(GameTime gameTime)
        {
            var currentState = Keyboard.GetState();
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            // Обновляем таймер атаки
            if (_attackTimer > 0)
            {
                _attackTimer -= deltaTime;
                if (_attackTimer <= 0)
                {
                    // Анимация закончилась, возвращаемся к idle
                    _playerState = "idle";
                    _animationComplete = true;
                    isAttacking = false;
                }
            }
            
            // Обновляем текущую анимацию
            if (_animations != null && _animations.ContainsKey(_playerState))
            {
                _animations[_playerState].Update();
                
                // Для атак: сбрасываем анимацию при переключении
                if (_playerState != _previousState && _playerState != "idle")
                {
                    _animations[_playerState].Reset();
                }
            }
            
            _previousState = _playerState;
            
            // Если сейчас атака - не двигаемся
            if (_attackTimer > 0)
            {
                return;
            }
            
            var isMoving = false;
            
            // Обработка движения и атак
            // Влево
            if (currentState.IsKeyDown(Keys.D) || currentState.IsKeyDown(Keys.Left))
            {
                position.X -= 3;
                isMoving = true;
            }

            // Вправо с атакой
            if ((currentState.IsKeyDown(Keys.A) || currentState.IsKeyDown(Keys.Right)))
            {
                if (currentState.IsKeyDown(Keys.Space) && _attackTimer <= 0)
                {
                    _playerState = "right_attack";
                    isAttacking = true;
                    _attackTimer = _attackDuration;
                    _animationComplete = false;
                }
                else if (_attackTimer <= 0)
                {
                    position.X += 3;
                    isMoving = true;
                }
            }

            // Вверх с атакой
            if ((currentState.IsKeyDown(Keys.W) || currentState.IsKeyDown(Keys.Up)))
            {
                if (currentState.IsKeyDown(Keys.Space) && _attackTimer <= 0)
                {
                    _playerState = "up_attack";
                    isAttacking = true;
                    _attackTimer = _attackDuration;
                    _animationComplete = false;
                }
                else if (_attackTimer <= 0)
                {
                    position.Y -= 3;
                    isMoving = true;
                }
            }

            // Вниз с атакой
            if ((currentState.IsKeyDown(Keys.S) || currentState.IsKeyDown(Keys.Down)))
            {
                if (currentState.IsKeyDown(Keys.Space) && _attackTimer <= 0)
                {
                    _playerState = "down_attack";
                    isAttacking = true;
                    _attackTimer = _attackDuration;
                    _animationComplete = false;
                }
                else if (_attackTimer <= 0)
                {
                    position.Y += 3;
                    isMoving = true;
                }
            }
            
            // Если не атакуем и не двигаемся - idle
            if (_attackTimer <= 0 && !isMoving)
            {
                _playerState = "idle";
            }
        }
    }
}
    


