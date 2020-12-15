using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Entities.Creatures;
using Some_Knights_and_a_Dragon.Managers;
using Some_Knights_and_a_Dragon.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Entities.Projectiles
{
    class Arrow : Projectile // An Arrow projectile
    {
        public int Damage { get; private set; } // How much damage the arrows deals upon hit
        public Arrow(Entity owner, Vector2 position, Vector2 direction, int power) : base(owner, position, direction, new Vector2(1000 + 200 * power, 1000 + 200 * power), 5)
        {
            Damage = (power * 10);
            LoadSprite("Items/Other/arrow");
            ObeysGravity = true;
            
        }

        public override void Ability()
        {
            base.Ability();
        }

        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            base.Draw(ref _spriteBatch);
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);
            foreach (Creature creature in ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Creatures)
            {
                if (creature == Owner)
                    continue;

                if (creature.HitBox.Contains(Position))
                {
                    creature.TakeDamage(Damage);
                    LifeTime = 0;
                }
            }
        }
    }
}
