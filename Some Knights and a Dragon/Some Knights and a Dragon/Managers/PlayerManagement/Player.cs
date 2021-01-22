using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Some_Knights_and_a_Dragon.Entities.Creatures;
using Some_Knights_and_a_Dragon.Items;
using Some_Knights_and_a_Dragon.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Managers.PlayerManagement
{
    public class Player
    {
        public Creature Creature { get; private set; } // Creature of the player
        public InventoryManager Inventory { get; private set; } // The inventory system check the class for more info

        bool isAttacking;
        public Player(Creature creature)
        {
            Creature = creature;
            Inventory = new InventoryManager();
        }

        public void Update(ref GameTime gameTime)
        {
            Inventory.Update(ref gameTime); // Updates the inventory

            // If player close to dropped item, that item is added to the inventory
            for (int i = Game1.WindowManager.GetGameplayWindow().CurrentLevel.DroppedItems.Count - 1; i >= 0; --i)
            {
                if (Vector2.Distance(Game1.WindowManager.GetGameplayWindow().CurrentLevel.DroppedItems[i].Position, Creature.Position) <= 50)
                {
                    if (Inventory.InventoryFull() && 
                        Inventory.ItemInInventory(Game1.WindowManager.GetGameplayWindow().CurrentLevel.DroppedItems[i].Item.Name) ||
                        !Inventory.InventoryFull())
                    {
                        Inventory.NewItem(Game1.WindowManager.GetGameplayWindow().CurrentLevel.DroppedItems[i].Item);
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.DroppedItems.RemoveAt(i);
                    }
                    
                }
            }

            if(Creature.CurrentHealth > 0) // if player is alive, they can move
                PlayerMovementAndInput(gameTime);
        }

        public void Draw(ref SpriteBatch _spritebatch)
        {

            if (Creature.CurrentHealth > 0) // If player is alive, their life and texture is displayed
            {
                HealthBar.FloatingBar(Creature, ref _spritebatch);
                Inventory.Draw(ref _spritebatch);
                if (Inventory.CurrentItem != null)
                {
                    Inventory.CurrentItem.Item.DrawOn(
                        ref _spritebatch,
                        Creature,
                        Creature.TextureDirection
                        );
                }
            }
        }

        public void PlayerMovementAndInput(GameTime gameTime) // Handles the movement and its animation: uses WASD format and space for jump. Mouse is used for attacks
        {
            Creature.MultiplyWithVelocity(new Vector2(0, 1));
            Creature.Sprite.Freeze();
            if (Game1.InputManager.KeyPressed(Keys.D))
            {
                Creature.AddToVelocity(new Vector2(Creature.Speed.X, 0));
                Creature.WalkAnimation();
            }
            if (Game1.InputManager.KeyPressed(Keys.A))
            {
                Creature.AddToVelocity(new Vector2(-Creature.Speed.X, 0));
                Creature.WalkAnimation();
            }
            if (Game1.InputManager.KeyClicked(Keys.W) && Creature.Velocity.Y == 0)
                Creature.AddToVelocity(new Vector2(0, -Creature.Speed.Y));

            if (Game1.InputManager.LeftMousePressed())
            {
                if (Inventory.CurrentItem != null)
                {
                    Inventory.CurrentItem.Item.OnUse(gameTime);
                }
                Creature.Attack();
                isAttacking = true;
            }
            else if (isAttacking)
            {
                if (Inventory.CurrentItem != null)
                {
                    Inventory.CurrentItem.Item.AfterUse();
                }
                isAttacking = false;
                Creature.ResetPose();
            }

            if (Game1.InputManager.KeyClicked(Keys.Q) && Inventory.CurrentItem != null)
            {
                Game1.WindowManager.GetGameplayWindow().CurrentLevel.AddDroppedItem(Creature.Position + new Vector2(Creature.TextureDirection == Entities.TextureDirection.Right ? 100 : -100, 30),
                    Inventory.RemoveAtCurrentIndex());
            }
        }
    }
}
