using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Entities.Projectiles
{
    class Arrow : Projectile // An Arrow projectile
    {
        public int Damage { get; private set; } // How much damage the arrows deals upon hit
        public Arrow(Entity owner, Vector2 position, Vector2 direction, float power) : base(owner, position, direction, 2)
        {
            Speed = new Vector2(10 + 1000 * power, 10 + 1000 * power);
            Damage = (int)(power * 10);
            Sprite = new Sprite("arrow");
            HitBoxWidth = 5;
            HitBoxHeight = 10;
            ObeysGravity = false;
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
            if (CollidingWithBoundries)
            {
                Speed = Vector2.Zero;
            }
        }
    }
}
