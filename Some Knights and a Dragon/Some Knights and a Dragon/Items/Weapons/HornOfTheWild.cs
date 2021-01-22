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
        float cooldown;
        StoneQueen stoneQueen;
        float upTime;
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
            if (cooldown <= 0)
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
            if (upTime > 0)
            {
                upTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (stoneQueen.MoveTo(gameTime, Game1.WindowManager.GetGameplayWindow().CurrentLevel.Boss.Creature.Position, new Vector2(10000, 0), 100))
                {
                    Game1.WindowManager.GetGameplayWindow().CurrentLevel.Boss.Creature.TakeDamage(2500);
                    upTime = 0;
                    stoneQueen.Kill();
                }
            }
            if (cooldown > 0)
               cooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
