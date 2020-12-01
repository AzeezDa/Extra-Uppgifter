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
        }

        public override void Attack(Creature creature)
        {
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
