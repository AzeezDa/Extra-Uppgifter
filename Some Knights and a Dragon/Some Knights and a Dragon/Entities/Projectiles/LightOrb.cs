using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Entities.Creatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Entities.Projectiles
{
    public class LightOrb : Projectile
    {
        public LightOrb(Entity owner, Vector2 position, Vector2 direction) : base(owner, position, direction, new Vector2(500, 500), float.MaxValue)
        {
            LoadSprite("Entities/Projectiles/lightOrb");
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
            if (CollidingWithBoundries)
            {
                LifeTime = 0;
            }

            foreach (Creature creature in Game1.WindowManager.GetGameplayWindow().CurrentLevel.Creatures)
            {
                if (creature == Owner)
                    continue;

                if (HitBox.Intersects(creature.HitBox))
                {
                    creature.TakeDamage(100);
                    LifeTime = 0;
                }
            }
        }
    }
}
