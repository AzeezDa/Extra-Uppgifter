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
            LoadSprite("knight", 4, 2);
            Position = new Vector2(400, 400);
            Speed = new Vector2(250, 1000);
            CurrentHealth = 100;
            MaxHealth = 100;

        }

        public override void Draw(ref SpriteBatch spriteBatch)
        {
            base.Draw(ref spriteBatch);
        }

        public override void Update(ref GameTime gameTime)
        {

            base.Update(ref gameTime);
        }

        public override void Attack()
        {
            base.Attack();
        }

        public override void TakeDamage(int amount)
        {
            base.TakeDamage(amount);
        }
    }
}
