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
    class Fireball : Projectile // A fireball projectile
    {
        int damage = 10;
        public Fireball(Entity owner, Vector2 position, Vector2 direction, int power) : base(owner, position, direction, new Vector2(100 + 10 * power, 100 + 10 * power), 5)
        {
            LoadSprite("Entities/Projectiles/fireball", 5, 1);
            Sprite.Scale = 3;
            ObeysGravity = false;
            damage = power * 2;
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
            Sprite.Animate(0, 5);
            if (CollidingWithBoundries)
            {
                LifeTime = 0;
            }

            foreach (Creature creature in Game1.WindowManager.GetGameplayWindow().CurrentLevel.Creatures.Values)
            {
                if (creature == Owner)
                    continue;

                if (HitBox.Intersects(creature.HitBox))
                {
                    creature.TakeDamage(damage);
                    creature.AddEffect(new Effects.Burning(3));
                    LifeTime = 0;
                }
            }
        }
    }
}
