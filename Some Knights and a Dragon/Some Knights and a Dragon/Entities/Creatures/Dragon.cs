using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Managers;
using Some_Knights_and_a_Dragon.Windows;
using Some_Knights_and_a_Dragon.Entities.Projectiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Entities.Creatures
{
    class Dragon : Creature
    {
        public Dragon()
        {
            LoadSprite("dragon", 3, 1);
            Position = new Vector2(900, 400);
            Speed = new Vector2(250, 1000);
            MaxHealth = 4000;
            CurrentHealth = MaxHealth;
            Sprite.Scale = 10;
        }

        public override void Attack()
        {
            Sprite.OneTimeAnimation(0, 3);
        }

        public override void Walk()
        {
            base.Walk();
            Sprite.Animate(0, 2);
        }

        public override void Draw(ref SpriteBatch spriteBatch)
        {
            base.Draw(ref spriteBatch);
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);
        }

        public override void TakeDamage(int amount)
        {
            base.TakeDamage(amount);
        }
    }
}
