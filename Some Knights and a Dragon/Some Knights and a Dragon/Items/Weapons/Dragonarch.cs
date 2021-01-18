using Microsoft.Xna.Framework;
using Some_Knights_and_a_Dragon.Entities.Projectiles;
using Some_Knights_and_a_Dragon.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Items.Weapons
{
    class Dragonarch : Weapon
    {
        int power = 0;
        bool shoot = false;
        public Dragonarch()
        {
            Name = "Dragonarch";
            Description = "This bow was found inside the dragon. It is hot but still holdable. Arrows seem to lit up while close to it.";
            LoadSprite("dragonarch", 4, 1);
            Damage = 20;
            Handle = new Vector2(Sprite.Width / 2, 8);
            power = 0;
            shoot = false;
        }

        public override void OnUse(GameTime gameTime)
        {
            power = power > 20 ? 20 : power + 3;
            if (Game1.WindowManager.GetGameplayWindow().Player.Inventory.ItemInInventory("Arrow"))
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

            if (shoot)
            {
                Game1.WindowManager.GetGameplayWindow().Player.Inventory.RemoveItem("Arrow");
                Game1.WindowManager.GetGameplayWindow().CurrentLevel.Projectiles.Add(new Fireball(
                    Game1.WindowManager.GetGameplayWindow().Player.Creature,
                    Game1.WindowManager.GetGameplayWindow().Player.Creature.Position,
                    Vector2.Normalize(Game1.InputManager.GetCursor() - Game1.WindowManager.GetGameplayWindow().Player.Creature.Position), power
                    ));
                shoot = false;
                power = 0;
                Sprite.Freeze(0, 0);
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
                Sprite.Rotation = Sprite.Rotation >= (float)Math.PI / 2 ? (float)Math.PI / 2 : Sprite.Rotation + power * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                Sprite.Rotation *= Sprite.Rotation > 0 ? -1 : 1;
                Sprite.Rotation = Sprite.Rotation <= -(float)Math.PI / 2 ? -(float)Math.PI / 2 : Sprite.Rotation - power * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
