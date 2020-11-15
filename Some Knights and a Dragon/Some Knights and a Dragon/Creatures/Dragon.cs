using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Creatures
{
    class Dragon : Creature
    {
        Timer attackTimer;
        public Dragon()
        {
            Health = 1000;
            Sprite = new Sprite("dragon", 32, 32);
            Sprite.Scale = 10;
            Position = new Vector2(1000, 700);
            Speed = Vector2.One;

            attackTimer = new Timer(1000, 1000);
        }
        public override void Attack()
        {
            base.Attack();
        }

        public override void Draw(ref SpriteBatch spriteBatch)
        {
            base.Draw(ref spriteBatch);
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);
            attackTimer.CheckTimer(ref gameTime);
            if (attackTimer.TimerOn && SpriteAnimationRow == 1)
            {
                SpriteAnimationRow = 1;
            }
            else
            {
                SpriteAnimationRow = 0;
            }
        }

        public override void TakeDamage(int amount)
        {
            base.TakeDamage(amount);
            SpriteAnimationRow = 1;
        }
    }
}
