using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Some_Knights_and_a_Dragon.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Items.Weapons
{
    public class ElvenBow : Weapon
    {

        public ElvenBow()
        {
            Name = "Elven Bow";
            Description = "This bow of an elf was not left on the shelf.";
            LoadSprite("bow", 16, 16);
            Damage = 10;
            Handle = new Vector2(0, 2 * Sprite.Scale);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void OnUse()
        {
            if(((GameplayWindow)Game1.CurrentWindow).Player.Inventory.ItemInInventory("Arrow"))
            {
                Sprite.Freeze(0, 4);
                Handle = new Vector2(-5 * Sprite.Scale, 2 * Sprite.Scale);
            }
        }

        public override void ResetSprite()
        {
            base.ResetSprite();
            Handle = Handle = new Vector2(0, 2 * Sprite.Scale);
        }
    }
}
