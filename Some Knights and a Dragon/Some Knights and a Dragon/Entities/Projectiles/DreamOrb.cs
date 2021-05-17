using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Entities.Creatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Entities.Projectiles
{
    public class DreamOrb : Projectile
    {
        public DreamOrb(Entity owner, Vector2 position, Vector2 direction) : base(owner, position, direction, new Vector2(500, 500), float.MaxValue)
        {
            LoadSprite("Entities/Projectiles/dreamOrb");
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

            // Collide with walls, get removed
            if (CollidingWithBoundries)
                LifeTime = 0;

            foreach (Creature creature in Game1.WindowManager.GetGameplayWindow().CurrentLevel.Creatures)
            {
                // If creature is owner, skip
                if (creature == Owner)
                    continue;

                // If it hits a creature, deal damage to the creature and heal the owner
                if (HitBox.Intersects(creature.HitBox))
                {
                    creature.TakeDamage(20);
                    ((Creature)Owner).AddHealth(50);
                    LifeTime = 0;
                }
            }
        }
    }
}
