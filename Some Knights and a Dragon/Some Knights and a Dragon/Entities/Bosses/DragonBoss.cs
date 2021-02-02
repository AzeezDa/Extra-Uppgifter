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

        // BOSS: The fire dragon shoots fire balls at the player. 
        // When at 50% fireballs rain from the sky and the player is pushed. 
        // At 10% even more fireballs rain down and the push is stronger

        Timer fireballTimer; // Timer for the fireballs thrown from the boss
        Timer rainOfFireTimer; // Timer for the fireballs that rain from the sky (phase 1 and 2)
        Random r = new Random();
        int power = 5; // The power of the Dragon's attacks
        public DragonBoss()
        {
            // Sets up the dragon and its timers.
            Creature = new Dragon();
            fireballTimer = new Timer(2000);
            rainOfFireTimer = new Timer(3000);

            // LOOT OF THE BOSS
            AddLoot(new Items.Weapons.Dragonarch(), 1);
            AddLoot(new Items.Other.Arrow(), 10);
            AddLoot(new Items.Weapons.Dragonbone(), 1);
            AddLoot(new Items.Other.Coin(), 20);
        }

        public override void Draw(SpriteBatch spriteBatch) // Draws the boss
        {
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Handles the fireball attacks.
            fireballTimer.CheckTimer(gameTime);
            if (fireballTimer.TimerOn)
            {
                foreach (Creature creature in Game1.WindowManager.GetGameplayWindow().CurrentLevel.Creatures)
                {
                    if (creature == Creature)
                        continue;

                    // Shoots the the all other creatures with a fireball
                    Game1.WindowManager.GetGameplayWindow().CurrentLevel.AddProjectile(new Fireball(Creature, Creature.Position + new Vector2(0, -100),
                    Vector2.Normalize(creature.Position - (Creature.Position + new Vector2(0, -100))), power));
                    Creature.Attack();
                }   
            }
            Phase1(ref gameTime);
            Phase2();
        }

        private void Phase1(ref GameTime gameTime)
        {
            
            // When below 50%: The player is blown to the left and fireballs start shooting from the sky
            rainOfFireTimer.CheckTimer(gameTime);
            
            if (Creature.GetHealthRatio < 0.5)
            {
                Game1.WindowManager.GetGameplayWindow().Player.Creature.AddToPosition(new Vector2(-100 * (float)gameTime.ElapsedGameTime.TotalSeconds, 0));
                if (rainOfFireTimer.TimerOn)
                {
                    power++;
                    for (int i = 1; i < 6; i++)
                    {
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.AddProjectile(new Fireball(Creature, new Vector2(i * 200 + r.Next(0, 100), 50 + r.Next(0, 50)), new Vector2(0, 0.3f), 3));
                    }
                }
            }
        }

        private void Phase2()
        {
            
            // When below 10%: The player is blown further and more fireballs rain from the sky
            if (Creature.GetHealthRatio < 0.1 && rainOfFireTimer.TimerOn)
            {
                Game1.WindowManager.GetGameplayWindow().CurrentLevel.Background.ChangeSpeed(1, 100);
                for (int i = 1; i < 4; i++)
                {
                    Game1.WindowManager.GetGameplayWindow().CurrentLevel.AddProjectile(new Fireball(Creature, new Vector2(i * 200 + r.Next(0, 100), 50 + r.Next(0, 50)), new Vector2(0, 0.5f), 10));
                }
            }
        }
    }
}
