using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Windows;
using Some_Knights_and_a_Dragon.Managers;
using Some_Knights_and_a_Dragon.Entities.Projectiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Entities.Creatures
{
    public class DragonBoss : Boss
    {
        Timer fireballTimer;
        Timer rainOfFireTimer;
        Random r = new Random();
        int power = 5;
        public DragonBoss()
        {
            Creature = new Dragon();
            fireballTimer = new Timer(2000);
            rainOfFireTimer = new Timer(3000);

            AddLoot(new Items.Weapons.Dragonarch(), 1);
            AddLoot(new Items.Other.Arrow(), 10);
            AddLoot(new Items.Weapons.Dragonbone(), 1);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void OnDeath()
        {
            base.OnDeath();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            fireballTimer.CheckTimer(ref gameTime);
            if (fireballTimer.TimerOn)
            {
                foreach (Creature creature in ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Creatures)
                {
                    if (creature == Creature)
                        continue;

                    ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.AddProjectile(new Fireball(Creature, Creature.Position + new Vector2(0, -100),
                    Vector2.Normalize(creature.Position - (Creature.Position + new Vector2(0, -100))), power));
                    Creature.Attack();
                }   
            }
            Phase1(ref gameTime);
            Phase2(ref gameTime);
        }

        private void Phase1(ref GameTime gameTime)
        {
            
            rainOfFireTimer.CheckTimer(ref gameTime);
            
            if (Creature.CurrentHealth <= Creature.MaxHealth / 2)
            {
                ((GameplayWindow)Game1.CurrentWindow).Player.Creature.AddToPosition(new Vector2(-100 * (float)gameTime.ElapsedGameTime.TotalSeconds, 0));
                if (rainOfFireTimer.TimerOn)
                {
                    power++;
                    for (int i = 1; i < 6; i++)
                    {
                        ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.AddProjectile(new Fireball(Creature, new Vector2(i * 200 + r.Next(0, 100), 50 + r.Next(0, 50)), new Vector2(0, 0.3f), 2));
                    }
                }
            }
        }

        private void Phase2(ref GameTime gameTime)
        {
            
            if (Creature.CurrentHealth <= Creature.MaxHealth / 10 && rainOfFireTimer.TimerOn)
            {
                ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Background.ChangeSpeed(1, 100);
                for (int i = 1; i < 4; i++)
                {
                    ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.AddProjectile(new Fireball(Creature, new Vector2(i * 200 + r.Next(0, 100), 50 + r.Next(0, 50)), new Vector2(0, 0.5f), 10));
                }
            }
        }
    }
}
