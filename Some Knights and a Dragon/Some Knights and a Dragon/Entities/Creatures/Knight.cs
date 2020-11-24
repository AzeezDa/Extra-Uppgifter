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
        // Checks if the knights shield is up
        bool shieldUp = false;
        public Knight()
        {
            Sprite = new Sprite("knight", 32, 32, 12);
            Position = new Vector2(200, 700);
            Speed = new Vector2(100, 100);
            HitBoxWidth = 10;
            HitBoxHeight = 20;
            CurrentHealth = 1000;
            MaxHealth = CurrentHealth;
        }

        public override void Draw(ref SpriteBatch spriteBatch)
        {
            HealthBar.FloatingBar(this, ref spriteBatch);
            base.Draw(ref spriteBatch);
        }

        public override void Update(ref GameTime gameTime)
        {
            // Updates animation for knight
            Direction = Vector2.Zero;
            Sprite.Animate(0, 1);
            if (Game1.InputManager.KeyPressed(Keys.D))
                Direction.X += 1;
            if (Game1.InputManager.KeyPressed(Keys.A))
                Direction.X += -1;
            if (Game1.InputManager.KeyClicked(Keys.W) && Acceleration.Y == 0)
                Acceleration.Y += -10;
            if (Game1.InputManager.KeyPressed(Keys.S))
                Direction.Y += 1;

            if (Direction != Vector2.Zero)
            {
                Sprite.Animate(0, 4);
            }

            if (Game1.InputManager.LeftMouseClicked())
            {
                Sprite.OneTimeAnimation(1, 10);
                Attack();
            }
            if (Game1.InputManager.RightMousePressed())
            {
                Sprite.Animate(2, 1);
                shieldUp = true;
                Speed = new Vector2(20, 20);
            }
            else
            {
                shieldUp = false;
                Speed = new Vector2(100, 100);
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

        public override void TakeDamage(int amount)
        {
            amount /= shieldUp ? 2 : 1;
            base.TakeDamage(amount);
        }
    }
}
