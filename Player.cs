

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace  MyGame
{
    internal class Player: Sprite
    {
        public Player(Texture2D texture, Vector2 position) : base(texture, position)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            var currentState = Keyboard.GetState();
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

