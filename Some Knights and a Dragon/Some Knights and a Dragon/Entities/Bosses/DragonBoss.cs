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
        Timer timer;
        int power = 10;
        public DragonBoss()
        {
            Creature = new Dragon();
            timer = new Timer(2000);
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
            timer.CheckTimer(ref gameTime);
            if (timer.TimerOn)
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
            
        }
    }
}
