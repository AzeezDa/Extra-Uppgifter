using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Items;

namespace Some_Knights_and_a_Dragon.Managers.PlayerManagement
{
    public class InventoryItem // Every item in the inventory consists of this class: 1 Item class and one int for the amount of that item 
    {
        public Item Item;
        public int Amount;

        public InventoryItem(Item item, int amount)
        {
            Item = item;
            Amount = amount;
        }
    }
    public class InventoryManager
    {

        // Inventory Management fields
        private InventoryItem[] inventory; // Array of the items
        private int currentItemIndex; // Array index of the current item held by the player
        public InventoryItem CurrentItem { get => inventory[currentItemIndex]; } // Gets the current item held by the player

        // Inventory Textures
        private Texture2D currentInventoryItemBackground;
        private Texture2D inventoryBackground;
        public InventoryManager()
        {
            inventory = new InventoryItem[5];
            currentItemIndex = 0;
            currentInventoryItemBackground = Game1.TextureManager.GetTexture("Menus/inventoryBack");
            inventoryBackground = Game1.TextureManager.GetTexture("Menus/inventoryTexture");
        }

        public void Update(ref GameTime gameTime) // Updates the Inventory
        {
            for (int i = 0; i < 5; i++)
            {
                if (inventory[i] != null)
                {
                    inventory[i].Item.Update(gameTime);
                }
            }

            // Scrolls wheel is used to handle the change of item in hand
            if (currentItemIndex + Game1.InputManager.ScrollValue() > 4)
                currentItemIndex = 0;
            else if (currentItemIndex + Game1.InputManager.ScrollValue() < 0)
                currentItemIndex = 4;
            else
                currentItemIndex += Game1.InputManager.ScrollValue();
        }

        // Draws the items stored in the correct place (Visualisation of the array)
        public void Draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                inventoryBackground,
                new Rectangle(1280 / 2, 960 - 64, 64 * 5, 64),
                null,
                Color.White, 0,
                new Vector2(inventoryBackground.Width / 2, inventoryBackground.Height / 2), SpriteEffects.None, 0);

            spriteBatch.Draw(
                currentInventoryItemBackground,
                new Rectangle(1280 / 2 + 64 * (currentItemIndex - 2), 960 - 64, 64, 64),
                null,
                Color.White, 0,
                new Vector2 (currentInventoryItemBackground.Width / 2, currentInventoryItemBackground.Height / 2), SpriteEffects.None, 0);
            for (int i = 0; i < 5; i++)
            {
                if (inventory[i] != null)
                {
                    spriteBatch.Draw(
                        inventory[i].Item.Sprite.SpriteTexture,
                        new Rectangle(1280/2 + 64 * (i - 2), 960 - 64, inventory[i].Item.Sprite.Width * 3, inventory[i].Item.Sprite.Height * 3),
                        new Rectangle(0, 0, inventory[i].Item.Sprite.Width, inventory[i].Item.Sprite.Height),
                        Color.White, 0,
                        new Vector2(inventory[i].Item.Sprite.Width / 2, inventory[i].Item.Sprite.Height / 2), SpriteEffects.None, 0
                        );
                    Game1.FontManager.WriteText(spriteBatch, inventory[i].Amount.ToString(), new Vector2(1280 / 2 + 64 * (i - 2), 960 - 64), Color.Red);
                }
            }

            if (CurrentItem != null)
                Game1.FontManager.WriteText(spriteBatch, CurrentItem.Item.Name, new Vector2(1280 / 2, 940));
        }

        
        public void NewItem(Item item) // Adds a new item to the first empty spot in the inventory
        {
            for (int i = 0; i < 5; i++)
            {
                if (inventory[i] != null)
                {
                    if (item.Name == inventory[i].Item.Name)
                    {
                        inventory[i].Amount++;
                        return;
                    }
                }
            }
            for (int i = 0; i < 5; i++)
            {
                if (inventory[i] == null)
                {
                    inventory[i] = new InventoryItem(item, 1);
                    return;
                }
            }
        }

        
        public Item RemoveItem(string name) // Removes the item from the inventory (Reduces its amount by 1) and returns it as an output
        {
            Item item;
            for (int i = 0; i < 5; i++)
            {
                if (inventory[i] == null)
                    continue;
                if (name == inventory[i].Item.Name)
                {
                    item = inventory[i].Item;
                    if (inventory[i].Amount == 1)
                        inventory[i] = null;
                    else
                        inventory[i].Amount--;
                    return item;
                }
            }
            return null;
        }

        public Item RemoveItem(string name, int amount) // Removes the item from the inventory (Reduces its amount by the given amount) and returns it as an output
        {
            Item item;
            for (int i = 0; i < 5; i++)
            {
                if (inventory[i] == null)
                    continue;
                if (name == inventory[i].Item.Name)
                {
                    item = inventory[i].Item;
                    if (inventory[i].Amount == amount)
                        inventory[i] = null;
                    else
                        inventory[i].Amount-= amount;
                    return item;
                }
            }
            return null;
        }

        public Item RemoveAtCurrentIndex()
        {
            Item item;
            if (inventory[currentItemIndex] != null)
            {
                item = inventory[currentItemIndex].Item;
                if (inventory[currentItemIndex].Amount == 1)
                    inventory[currentItemIndex] = null;
                else
                    inventory[currentItemIndex].Amount--;
                return item;
            }
            return null;
        }

         
        public bool ItemInInventory(string name) // Checks if an item in in the inventory
        {
            for (int i = 0; i < 5; i++)
            {
                if (inventory[i] == null)
                    continue;
                if (inventory[i].Item.Name == name)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ItemInInventory(string name, int amount) // Checks if an item in in the inventory with a specific amount
        {
            for (int i = 0; i < 5; i++)
            {
                if (inventory[i] == null)
                    continue;
                if (inventory[i].Item.Name == name && inventory[i].Amount >= amount)
                {
                    return true;
                }
            }
            return false;
        }

        public int AmountOf(string name) // Returns the amount of a specific item in the inventory
        {
            int amount = 0;
            for (int i = 0; i < 5; i++)
            {
                if (inventory[i] == null)
                    continue;
                if (inventory[i].Item.Name == name)
                {
                    amount += inventory[i].Amount;
                }
            }
            return amount;
        }

        public bool InventoryFull() // Returns true if the inventory is full
        {
            foreach (InventoryItem item in inventory)
            {
                if (item == null)
                    return false;
            }
            return true;
        }

        public void RemoveAll(string name) // Removes all amount of a specific item from the inventory
        {

            for (int i = 0; i < 5; i++)
            {
                if (inventory[i] == null)
                    continue;
                if (name == inventory[i].Item.Name)
                {
                    inventory[i]= null;
                }
            }
        }
    }
}
