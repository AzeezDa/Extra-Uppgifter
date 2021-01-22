using Microsoft.Xna.Framework;
using Some_Knights_and_a_Dragon.Entities.Creatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Items.Weapons
{
    public class QueensBlade : Weapon
    {
        bool attacked = false;
        int strength = 0;
        public QueensBlade()
        {
            Name = "Queen's Blade";
            Description = "This Sword is blessed by the Queen. You probably shouldn't lose it.";
            LoadSprite("queensBlade");
            Damage = 70;
            Handle = new Vector2(0, 4);
            Sprite.Scale = 10;
        }
        public override void AfterUse()
        {
            base.AfterUse();
            attacked = false;
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
                        creature.TakeDamage(Damage + strength * 2); // Attack increase every time the player attacks
                        attacked = true;
                        strength++;
                    }
                }
            }
        }
    }
}
