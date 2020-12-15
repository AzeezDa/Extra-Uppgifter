﻿using Microsoft.Xna.Framework;
using Some_Knights_and_a_Dragon.Entities.Creatures;
using Some_Knights_and_a_Dragon.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Items.Weapons
{
    public class Sword : Weapon
    {
        bool attacked = false;
        public Sword()
        {
            Name = "Sword";
            Description = "This sword can cut enemies, pretty good, ey?";
            LoadSprite("sword");
            Damage = 15;
            Handle = new Vector2(0, 2);
        }

        public override void OnUse(GameTime gameTime)
        {
            base.OnUse(gameTime);
            if (!attacked)
            {
                foreach (Creature creature in ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Creatures)
                {
                    if (creature == ((GameplayWindow)Game1.CurrentWindow).Player.Creature)
                        continue;

                    if (creature.HitBox.Contains(((GameplayWindow)Game1.CurrentWindow).Player.Creature.Position +
                        (((GameplayWindow)Game1.CurrentWindow).Player.Creature.TextureDirection == Entities.TextureDirection.Left ? new Vector2(30, 0) : new Vector2(-30, 0))))
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
