using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Entities.Projectiles
{
    class Projectile : Entity // An object that is shot from a creature or other, has same properties as entity.
    {
        public Entity Owner { get; protected set; } // The creature who shot the projectile

        float rotation = 0f; // How much the texture is rotated
        
        public float LifeTime { get; protected set; } // How long it exists in the game

        public Projectile(Entity owner, Vector2 position, Vector2 direction, Vector2 speed, float LifeTime = float.MaxValue)
        {
            Velocity = Vector2.Normalize(direction) * speed;
            Owner = owner;
            Position = position;
            this.LifeTime = LifeTime;
        }

        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            Sprite.Draw(ref _spriteBatch, Position, TextureDirection, rotation);
        }

        public override void Update(ref GameTime gameTime)
        {
            // Rotation based on trigonometry
            rotation = (float)Math.Atan2(Velocity.Y, Velocity.X) + (float)Math.PI / 2;

            // If it does not obej gravity, then acceleration is 0
            Acceleration *= ObeysGravity ? 1 : 0;
            LifeTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(ref gameTime);
        }

        public virtual void Ability() // A special ability for the projectile
        {

        }
    }
}
