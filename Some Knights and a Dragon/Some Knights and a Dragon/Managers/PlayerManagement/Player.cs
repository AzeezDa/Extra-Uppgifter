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
        public Creature Creature { get; private set; }
        public InventoryManager Inventory { get; private set; }

        bool attacking;
        public Player(Creature creature)
        {
            Creature = creature;
            Inventory = new InventoryManager();
        }

        public void Update(ref GameTime gameTime)
        {
            Inventory.Update(ref gameTime);

            for (int i = 0; i < ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.DroppedItems.Count; i++)
            {
                if (Vector2.Distance(((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.DroppedItems[i].Position, Creature.Position) <= 50)
                {
                    Inventory.NewItem(((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.DroppedItems[i].Item);
                    ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.DroppedItems.RemoveAt(i);
                }
            }

            
            Creature.MultiplyWithVelocity(new Vector2(0, 1));
            Creature.Sprite.Freeze();
            if (Game1.InputManager.KeyPressed(Keys.D))
            {
                Creature.AddToVelocity(new Vector2(Creature.Speed.X, 0));
                Creature.Walk();
            }
            if (Game1.InputManager.KeyPressed(Keys.A))
            {
                Creature.AddToVelocity(new Vector2(-Creature.Speed.X, 0));
                Creature.Walk();
            }
            if (Game1.InputManager.KeyClicked(Keys.W) && Creature.Velocity.Y == 0)
                Creature.AddToVelocity(new Vector2(0, -Creature.Speed.Y));

            if (Game1.InputManager.LeftMousePressed())
            {
                Inventory.CurrentItem.Item.OnUse(gameTime);
                Creature.Attack();
                attacking = true;
            }
            else if(attacking)
            {
                if (Inventory.CurrentItem != null)
                {
                    Inventory.CurrentItem.Item.AfterUse();
                }
                attacking = false;
                Creature.ResetPose();
            }
        }

        public void Draw(ref SpriteBatch _spritebatch)
        {
            Inventory.Draw(ref _spritebatch);
            if (Inventory.CurrentItem != null)
            {
                Inventory.CurrentItem.Item.DrawOn(
                    ref _spritebatch,
                    Creature,
                    Creature.TextureDirection,
                    attacking && Creature.TextureDirection == Entities.TextureDirection.Left ? (float)Math.PI / 2 : attacking ? -(float)Math.PI / 2 : 0
                    );
            }
            
        }
    }
}
