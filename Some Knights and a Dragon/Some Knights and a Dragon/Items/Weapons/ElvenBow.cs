using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Some_Knights_and_a_Dragon.Entities.Projectiles;
using Some_Knights_and_a_Dragon.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Items.Weapons
{
    public class ElvenBow : Weapon
    {
        int power;
        bool shoot;
        public ElvenBow()
        {
            Name = "Elven Bow";
            Description = "This bow of an elf was not left on the shelf.";
            LoadSprite("bow", 16, 16);
            Damage = 10;
            Handle = new Vector2(Sprite.Width / 2, 8);
            power = 0;
            shoot = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void OnUse(GameTime gameTime)
        {
            power = power > 10 ? 10 : power + 1;
            if (((GameplayWindow)Game1.CurrentWindow).Player.Inventory.ItemInInventory("Arrow"))
            {
                Sprite.AnimateAndFreeze(0, 4);
                shoot = true;
                Handle = new Vector2((Sprite.Width - 9) / 2, 5);
            }
        }

        public override void AfterUse()
        {
            base.AfterUse();

            if (shoot)
            {
                ((GameplayWindow)Game1.CurrentWindow).Player.Inventory.RemoveItem("Arrow");
                ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Projectiles.Add(new Arrow(
                    ((GameplayWindow)Game1.CurrentWindow).Player.Creature,
                    ((GameplayWindow)Game1.CurrentWindow).Player.Creature.Position,
                    Vector2.Normalize(Game1.InputManager.GetCursor() - ((GameplayWindow)Game1.CurrentWindow).Player.Creature.Position), power
                    ));
                shoot = false;
                power = 0;
                Sprite.Freeze(0,0);
                Handle = new Vector2(Sprite.Width / 2, 8);
            }
        }
    }
}
