using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Entities.Creatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Entities.Projectiles
{
    class FireOrb : Projectile
    {
        public FireOrb(Entity owner, Vector2 position, Vector2 direction) : base(owner, position, direction, new Vector2(500, 500), float.MaxValue)
        {
            LoadSprite("Entities/Projectiles/fireOrb");
            Sprite.Scale = 2;
            ObeysGravity = false;
        }

        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            base.Draw(ref _spriteBatch);
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);

            // Collide with walls, remove
            if (CollidingWithBoundries)
                LifeTime = 0;

            foreach (Creature creature in Game1.WindowManager.GetGameplayWindow().CurrentLevel.Creatures)
            {
                // If creature is owner, skip
                if (creature == Owner)
                    continue;

                // If it hits a creature, deal damage and add a burning effect
                if (HitBox.Intersects(creature.HitBox))
                {
                    creature.TakeDamage(50);
                    creature.AddEffect(new Effects.Burning(10));
                    LifeTime = 0;
                }
            }
        }
    }
}
