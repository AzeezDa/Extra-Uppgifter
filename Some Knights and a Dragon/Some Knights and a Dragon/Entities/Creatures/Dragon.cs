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
        // Timer for interval between fireball attacks.
        Timer attackTimer;

        // Stores all fireballs
        List<Fireball> fireballs;

        // The power of the fireballs, used to increase damage and speed
        float power = 0;
        public Dragon()
        {
            CurrentHealth = 3000;
            MaxHealth = CurrentHealth;
            Sprite = new Sprite("dragon", 32, 32);
            Sprite.Scale = 10;
            Position = new Vector2(1000, 700);
            Speed = Vector2.One;

            HitBoxWidth = 30;
            HitBoxHeight = 25;

            attackTimer = new Timer(4000);

            fireballs = new List<Fireball>();
        }

        public override void Attack(Creature creature)
        {
            base.Attack();
            fireballs.Add(new Fireball(this, Position, Vector2.Normalize((creature.Position - (new Vector2(0, 70))) - Position), (int)power));
        }

        public override void Draw(ref SpriteBatch spriteBatch)
        {
            base.Draw(ref spriteBatch);
            HealthBar.BossHealthBar(this, ref spriteBatch);

            

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
                Sprite.Animate(0, 1);
                foreach (Creature creature in ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Creatures)
                {
                    if (creature == this)
                        continue;
                    Attack(creature);
                }
            }

            for (int i = 0; i < fireballs.Count; i++)
            {
                fireballs[i].Update(ref gameTime);
                if (fireballs[i].LifeTime < 0 || fireballs[i].CollidingWithBoundries)
                {
                    fireballs[i] = null;
                    fireballs.RemoveAt(i);
                    continue;
                }
                foreach (Creature creature in ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Creatures)
                {
                    if (creature == this)
                        continue;

                    if (fireballs[i].HitBox.Intersects(creature.HitBox))
                    {
                        creature.TakeDamage(10);
                        power++; // If dragons deals damage, increase power
                        attackTimer.NewTime(4000 - power * 190);
                        fireballs[i] = null;
                        fireballs.RemoveAt(i);
                    }
                }
            }
        }

        // If dragon takes damage, increase power
        public override void TakeDamage(int amount)
        {
            base.TakeDamage(amount);
            power += (float)amount / 100;
            attackTimer.NewTime(4000 - power * 190);
        }
    }
}
