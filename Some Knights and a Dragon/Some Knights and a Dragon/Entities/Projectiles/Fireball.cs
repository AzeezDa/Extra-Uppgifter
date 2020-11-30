using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Entities.Projectiles
{
    class Fireball : Projectile // A fireball projectile
    {

        public Fireball(Entity owner, Vector2 position, Vector2 direction, int power) : base(owner, position, direction, new Vector2(100 + 10 * power, 100 + 10 * power), 5)
        {
            Sprite = new Sprite("fireball", 32, 32);
            HitBoxWidth = 30;
            HitBoxHeight = 30;
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
        }
    }
}
