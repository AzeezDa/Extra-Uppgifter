using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Some_Knights_and_a_Dragon.Entities.Creatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Items.Weapons
{
    public class HornOfTheWild : Weapon
    {

        // WEAPON: This weapon can be used to summon the Stone Queen to attack the boss of the current level dealing massive damage, 2500.
        
        float cooldown; // Cooldown of the use
        StoneQueen stoneQueen; // The stone queen
        float upTime; // How long the queen is up
        public HornOfTheWild()
        {
            Name = "Horn of the Wild";
            Description = "The Queen can hear this horn from pretty far away.";
            LoadSprite("hornOfTheWild");
            Damage = 2;
            Handle = new Vector2(0, 10);
            cooldown = 0;
            upTime = 0;
            stoneQueen = new StoneQueen();
        }

        public override void OnUse(GameTime gameTime)
        {
            base.OnUse(gameTime);
            if (cooldown <= 0) // Can be used if the cooldown is less or equal to 0. COOLDOWN is 2 minutes and up time is 10 seconds
            {
                stoneQueen.SetHealthToMax();
                cooldown = 120;
                upTime = 10;
                Game1.WindowManager.GetGameplayWindow().CurrentLevel.AddCreature(stoneQueen);
                stoneQueen.ChangePosition(Game1.WindowManager.GetGameplayWindow().Player.Creature.Position);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (upTime > 0) // The queen is up while the up time is greater than 0
            {
                upTime -= (float)gameTime.ElapsedGameTime.TotalSeconds; // Reduce the up time
                if (stoneQueen.MoveTo(gameTime, Game1.WindowManager.GetGameplayWindow().CurrentLevel.Boss.Creature.Position, new Vector2(10000, 0), 100)) // Stone queen moves to the boss and if is in range
                {
                    Game1.WindowManager.GetGameplayWindow().CurrentLevel.Boss.Creature.TakeDamage(2500); // Boss takes damage
                    upTime = 0; // Uptime is zero
                    stoneQueen.Kill(); // Queen is removed
                }
            }
            if (cooldown > 0) // Manages cooldown
               cooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
