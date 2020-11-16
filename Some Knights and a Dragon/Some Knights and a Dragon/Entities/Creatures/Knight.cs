using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Some_Knights_and_a_Dragon.Managers;
using Some_Knights_and_a_Dragon.Windows;

namespace Some_Knights_and_a_Dragon.Entities.Creatures
{

    class Knight : Creature
    {
        public Knight()
        {
            Sprite = new Sprite("knight", 32, 32);
            Position = new Vector2(200, 700);
            Speed = new Vector2(100, 100);
            HitBoxWidth = 10;
            HitBoxHeight = 30;
            Health = 100;
        }

        public override void Draw(ref SpriteBatch spriteBatch)
        {
            base.Draw(ref spriteBatch);
        }

        public override void Update(ref GameTime gameTime)
        {
            Direction = Vector2.Zero;
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            SpriteAnimationRow = 0;
            if (keyboardState.IsKeyDown(Keys.D))
            {
                Direction.X += 1;
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                Direction.X += -1;
            }
            if (keyboardState.IsKeyDown(Keys.W))
                Direction.Y += -1;
            if (keyboardState.IsKeyDown(Keys.S))
                Direction.Y += 1;

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                SpriteAnimationRow = 1;
                Attack();
            }
            base.Update(ref gameTime);
        }

        public override void Attack()
        {
            foreach (Creature creature in ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Creatures)
            {
                if (creature == this)
                    continue;

                if (TextureDirection == TextureDirection.Left &&
                    creature.HitBox.Contains(Position.X + 30, Position.Y))
                {
                    creature.TakeDamage(50);
                }
                else if(TextureDirection == TextureDirection.Right &&
                    creature.HitBox.Contains(Position.X - 30, Position.Y))
                {
                    creature.TakeDamage(50);
                }
            }

            base.Attack();
        }
    }
}
