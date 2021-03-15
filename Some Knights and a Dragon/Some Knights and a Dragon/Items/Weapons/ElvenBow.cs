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

        // WEAPON: Consumes an arrow to shoot it. Base damage is 10

        int power; // Charge power of the bow, the more the player holds the more power it gains and the more damage it deals
        bool shoot;
        public ElvenBow()
        {
            Name = "Elven Bow";
            Description = "This bow of an elf was not left on the shelf.";
            LoadSprite("bow", 4, 1);
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
            power = power > 10 ? 10 : power + 1; // Max power is 10
            if (Game1.WindowManager.GetGameplayWindow().Player.Inventory.ItemInInventory("Arrow")) // If Arrow in inventory, charge
            {
                Sprite.AnimateAndFreeze(0, 4);
                shoot = true;
                Handle = new Vector2(Sprite.Width / 2, 8);

                UseAnimation(gameTime);
            }
        }

        public override void AfterUse()
        {
            base.AfterUse();

            if (shoot) // Consumes the arrow and shoots it
            {
                Game1.WindowManager.GetGameplayWindow().Player.Inventory.RemoveItem("Arrow");
                Game1.WindowManager.GetGameplayWindow().CurrentLevel.AddProjectile(new Arrow(
                    Game1.WindowManager.GetGameplayWindow().Player.Creature,
                    Game1.WindowManager.GetGameplayWindow().Player.Creature.Position,
                    Vector2.Normalize(Game1.InputManager.GetCursor() - Game1.WindowManager.GetGameplayWindow().Player.Creature.Position), power
                    ));
                shoot = false;
                power = 0;
                Sprite.Freeze(0,0);
                Handle = new Vector2(Sprite.Width / 2, 8);
                Sprite.Rotation = 0;
            }
        }

        public override void UseAnimation(GameTime gameTime)
        {
            base.UseAnimation(gameTime);
            if (Game1.WindowManager.GetGameplayWindow().Player.Creature.TextureDirection == Entities.TextureDirection.Left)
            {
                Sprite.Rotation *= Sprite.Rotation < 0 ? -1 : 1;
                Sprite.Rotation = Sprite.Rotation >= (float)Math.PI / 2 ? (float)Math.PI / 2 : Sprite.Rotation + 6.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                Sprite.Rotation *= Sprite.Rotation > 0 ? -1 : 1;
                Sprite.Rotation = Sprite.Rotation <= -(float)Math.PI / 2 ? -(float)Math.PI / 2 : Sprite.Rotation - 6.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
