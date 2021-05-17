using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Items.Consumables
{
    public class HealingPotion : Consumable
    {
        // CONSUMABLE: When used it adds 20 to the player's current health

        public HealingPotion()
        {
            LoadSprite("healingPotion");
            Name = "Healing Potion";
            Description = "A special brew! Drink and be healed!";
            Sprite.Scale = 3;
            Handle = new Vector2(Sprite.Width / 2, Sprite.Height / 2 - 2);
        }

        public override void OnUse(GameTime gameTime)
        {
            base.OnUse(gameTime);
            if (Consuming)
                return;
            Game1.WindowManager.GetGameplayWindow().Player.Inventory.RemoveAtCurrentIndex();
            Game1.WindowManager.GetGameplayWindow().Player.Creature.AddHealth(20);
            Consuming = true;
        }

        public override void AfterUse()
        {
            base.AfterUse();
            Consuming = false;
        }
    }
}
