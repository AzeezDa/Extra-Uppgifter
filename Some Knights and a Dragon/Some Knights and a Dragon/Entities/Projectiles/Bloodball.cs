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

            // Animate the projectile
            Sprite.Animate(0, 4);

            // If hits a wall, then remove
            if (CollidingWithBoundries)
                LifeTime = 0;

            // Distance to the nearest non-owner creature
            float proximity = float.MaxValue;

            // New distance to the nearest non-owner creature
            float distance;

            // Checks all players
            foreach (Creature creature in Game1.WindowManager.GetGameplayWindow().CurrentLevel.Creatures)
            {
                // Skip if creature is owner
                if (creature == Owner)
                    continue;

                // If it hits a creature deal massive damage and remove the ball
                if (HitBox.Intersects(creature.HitBox))
                {
                    creature.TakeDamage(15000);
                    LifeTime = 0;
                }

                // Calculate distance
                distance = Vector2.Distance(creature.Position, Position);

                // Check if another creature is closer and change who the ball follows
                if (distance < proximity)
                    following = creature;

                // Normalise velocity based on difference of position and angles
                Velocity = Vector2.Normalize(following.Position - Position) * Speed;
            }
        }
    }
}
