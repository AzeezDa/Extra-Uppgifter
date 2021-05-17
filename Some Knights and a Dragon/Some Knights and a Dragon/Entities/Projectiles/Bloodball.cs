using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Some_Knights_and_a_Dragon.Entities.Creatures;

namespace Some_Knights_and_a_Dragon.Entities.Projectiles
{
    public class Bloodball : Projectile
    {
        Creature following;
        public Bloodball(Entity owner, Vector2 position, Vector2 direction) : base(owner, position, direction, new Vector2(200, 200))
        {
            LoadSprite("Entities/Projectiles/bloodball", 4, 1);
            Sprite.Scale = 4;
            ObeysGravity = false;
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);
            Sprite.Animate(0, 4);
            if (CollidingWithBoundries)
            {
                LifeTime = 0;
            }

            float proximity = float.MaxValue;
            float distance;
            foreach (Creature creature in Game1.WindowManager.GetGameplayWindow().CurrentLevel.Creatures)
            {
                if (creature == Owner)
                    continue;

                if (HitBox.Intersects(creature.HitBox))
                {
                    creature.TakeDamage(15000);
                    LifeTime = 0;
                }

                distance = Vector2.Distance(creature.Position, Position);
                if (distance < proximity)
                {
                    following = creature;
                }

                Velocity = Vector2.Normalize(following.Position - Position) * Speed;
            }
        }
    }
}
