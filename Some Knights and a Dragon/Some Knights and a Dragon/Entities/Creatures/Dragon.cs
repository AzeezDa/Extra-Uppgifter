using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Managers;
using Some_Knights_and_a_Dragon.Windows;
using Some_Knights_and_a_Dragon.Entities.Projectiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Entities.Creatures
{
    class Dragon : Creature
    {
        Timer attackTimer;
        List<Fireball> fireballs;
        

        public Dragon()
        {
            Health = 1000;
            Sprite = new Sprite("dragon", 32, 32);
            Sprite.Scale = 10;
            Position = new Vector2(1000, 700);
            Speed = Vector2.One;

            HitBoxWidth = 30;
            HitBoxHeight = 25;

            attackTimer = new Timer(5000);

            fireballs = new List<Fireball>();
        }

        public override void Attack(Creature creature)
        {
            base.Attack();
            fireballs.Add(new Fireball(this, Position, Vector2.Normalize(creature.Position - Position)));
        }

        public override void Draw(ref SpriteBatch spriteBatch)
        {
            base.Draw(ref spriteBatch);

            foreach (Fireball fireball in fireballs)
            {
                fireball.Draw(ref spriteBatch);
            }
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);
            attackTimer.CheckTimer(ref gameTime);
            if (attackTimer.TimerOn)
            {
                foreach (Creature creature in ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Creatures)
                {
                    Attack(creature);
                }
            }

            for (int i = 0; i < fireballs.Count; i++)
            {
                fireballs[i].Update(ref gameTime);
                foreach (Creature creature in ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Creatures)
                {
                    if (creature == this)
                        continue;

                    if (fireballs[i].HitBox.Intersects(creature.HitBox))
                    {
                        creature.TakeDamage(10);
                        fireballs.RemoveAt(i);
                    }
                }
            }

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
