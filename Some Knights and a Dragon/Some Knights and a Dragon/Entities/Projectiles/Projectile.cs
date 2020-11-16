using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Entities.Projectiles
{
    class Projectile : Entity
    {
        public Entity Owner { get; protected set; }

        float rotation = 0f;

        public Projectile(Entity owner, Vector2 position, Vector2 direction)
        {
            Direction = direction;
            Owner = owner;
            Position = position;
        }

        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            Sprite.Draw(ref _spriteBatch, Position, TextureDirection, rotation);
        }

        public override void Update(ref GameTime gameTime)
        {
            rotation = (float)Math.Atan2(Direction.Y, Direction.X) + (float)Math.PI / 2;
            base.Update(ref gameTime);
        }

        public virtual void Ability()
        {

        }
    }
}
