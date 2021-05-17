using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Some_Knights_and_a_Dragon.Managers;

namespace Some_Knights_and_a_Dragon.Entities.Creatures
{
    public class BellyFlame : Creature
    {
        bool dying = false;
        Timer deathTimer = new Timer(500);
        public BellyFlame()
        {
            LoadSprite("bellyFlame", 6, 2);
            Position = new Vector2(900, 400);
            Speed = new Vector2(250, 8000);
            MaxHealth = 100;
            CurrentHealth = MaxHealth;
            Sprite.Scale = 3;
        }

        public override void Attack()
        {
            base.Attack();
            Sprite.OneTimeAnimation(1, 6);
            dying = true;
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);
            if (dying)
            {
                deathTimer.CheckTimer(gameTime);
                if (deathTimer.TimerOn)
                {
                    foreach (Creature creature in Game1.WindowManager.GetGameplayWindow().CurrentLevel.Creatures)
                    {
                        if (creature == this)
                            continue;

                        if (Vector2.Distance(Position, creature.Position) <= 200)
                        {
                            creature.TakeDamage(10);
                            creature.AddEffect(new Effects.Burning(3000));
                        }
                    }
                    Kill();
                }
            }
        }
    }
}
