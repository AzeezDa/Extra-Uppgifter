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

        public Player(Creature creature)
        {
            Creature = creature;
            Inventory = new InventoryManager();
        }

        public void Update(ref GameTime gameTime)
        {
            for (int i = 0; i < ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.DroppedItems.Count; i++)
            {
                if (Vector2.Distance(((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.DroppedItems[i].Position, Creature.Position) <= 50)
                {
                    Inventory.NewItem(((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.DroppedItems[i].Item);
                    ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.DroppedItems.RemoveAt(i);
                }
            }

            Creature.MultiplyWithVelocity(new Vector2(0, 1));
            if (Game1.InputManager.KeyPressed(Keys.D))
                Creature.AddToVelocity(new Vector2(Creature.Speed.X, 0));
            if (Game1.InputManager.KeyPressed(Keys.A))
                Creature.AddToVelocity(new Vector2(-Creature.Speed.X, 0));
            if (Game1.InputManager.KeyClicked(Keys.W) && Creature.Velocity.Y == 0)
                Creature.AddToVelocity(new Vector2(0, -Creature.Speed.Y));
        }

        public void Draw(ref SpriteBatch _spritebatch)
        {
            Inventory.Draw(ref _spritebatch);
        }
    }
}
