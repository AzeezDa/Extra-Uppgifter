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
        bool attacked = false;
        double fireDuration = 0;
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
            if (timer.TimerOn && fireDuration <= 0 && r.NextDouble() < 0.01)
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
            if (!attacked)
            {
                foreach (Creature creature in Game1.WindowManager.GetGameplayWindow().CurrentLevel.Creatures)
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
            attacked = false;
        }
    }
}
