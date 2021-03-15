using Microsoft.Xna.Framework;
using Some_Knights_and_a_Dragon.Entities.Creatures;
using Some_Knights_and_a_Dragon.Windows;
using Some_Knights_and_a_Dragon.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Items.Weapons
{
    public class Dragonbone : Weapon
    {

        // WEAPON: This is a sword that occasionally lights up to burn enemies. Base damage is 30

        bool attacked = false;
        double fireDuration = 0; // The time the fire mechanic is on
        Random r = new Random();
        Timer timer;
        public Dragonbone()
        {
            Name = "Dragonbone";
            Description = "This sword seem to radiate and vary in heat. OW! IT'S BURNING!";
            LoadSprite("Dragonbone", 5, 1);
            Damage = 30;
            Handle = new Vector2(0, 2);
            timer = new Timer(1000);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            timer.CheckTimer(gameTime);
            if (timer.TimerOn && fireDuration <= 0 && r.NextDouble() < 0.01) // 1% Chance every second to light the sword on fire
                fireDuration = 10;

            fireDuration -= gameTime.ElapsedGameTime.TotalSeconds;

            if (fireDuration > 0)
                Sprite.Animate(0, 5);
            else
                Sprite.Animate(0, 1);
        }

        public override void OnUse(GameTime gameTime)
        {
            base.OnUse(gameTime);
            if (!attacked) // If is not attacking it attacks the creature in front of the user
            {
                foreach (Creature creature in Game1.WindowManager.GetGameplayWindow().CurrentLevel.Creatures.Values)
                {
                    if (creature == Game1.WindowManager.GetGameplayWindow().Player.Creature)
                        continue;

                    if (creature.HitBox.Contains(Game1.WindowManager.GetGameplayWindow().Player.Creature.Position +
                        (Game1.WindowManager.GetGameplayWindow().Player.Creature.TextureDirection == Entities.TextureDirection.Left ? new Vector2(30, 0) : new Vector2(-30, 0))))
                    {
                        creature.TakeDamage(Damage);
                        attacked = true;
                    }
                }
            }
        }

        public override void AfterUse()
        {
            base.AfterUse();
            attacked = false; // Resets
        }
    }
}
