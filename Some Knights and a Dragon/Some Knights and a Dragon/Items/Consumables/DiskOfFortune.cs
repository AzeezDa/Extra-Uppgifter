using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Items.Consumables
{
    public class DiskOfFortune : Consumable
    {
        public DiskOfFortune()
        {
            LoadSprite("diskOfFortune");
            Name = "Disk of Fortune";
            Description = "This Disk is said to bless its user with great health, forever.";
            Sprite.Scale = 4;
            Handle = new Vector2(Sprite.Width / 2, Sprite.Height / 2 - 2);
        }
        public override void OnUse(GameTime gameTime)
        {
            base.OnUse(gameTime);
            Game1.WindowManager.GetGameplayWindow().Player.Inventory.RemoveAtCurrentIndex();
            Game1.WindowManager.GetGameplayWindow().Player.Creature.AddToMaxHealth(80);
        }
    }
}
