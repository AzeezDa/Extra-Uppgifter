using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Entities.Projectiles
{
    class Fireball : Projectile
    {
        Timer animation;

        public Fireball(Entity owner, Vector2 position, Vector2 direction) : base(owner, position, direction)
        {
            Speed = new Vector2(100, 100);
            Sprite = new Sprite("fireball", 32, 32);
            animation = new Timer(1000 / 12);
            HitBoxWidth = 30;
            HitBoxHeight = 30;
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
            animation.CheckTimer(ref gameTime);
            if (animation.TimerOn)
                SpriteAnimationColumn = (SpriteAnimationColumn + 1) % 3;
        }
    }
}
