using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Items;

namespace Some_Knights_and_a_Dragon.Managers.PlayerManagement
{
    public class InventoryItem
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
        private InventoryItem[] inventory;
        private int currentItemIndex;
        public InventoryItem CurrentItem { get => inventory[currentItemIndex]; }
        // Inventory Textures
        private Texture2D currentInventoryItemBackground;
        private Texture2D inventoryBackground;
        public InventoryManager()
        {
            inventory = new InventoryItem[5];
            currentItemIndex = 0;
            currentInventoryItemBackground = Game1.ContentManager.Load<Texture2D>("Menus/inventoryBack");
            inventoryBackground = Game1.ContentManager.Load<Texture2D>("Menus/inventoryTexture");
        }

        public void Update(ref GameTime gameTime)
        {
            for (int i = 0; i < 5; i++)
            {
                if (inventory[i] != null)
                {
                    inventory[i].Item.Update(gameTime);
                }
            }

            if (currentItemIndex + Game1.InputManager.ScrollValue() > 4)
                currentItemIndex = 0;
            else if (currentItemIndex + Game1.InputManager.ScrollValue() < 0)
                currentItemIndex = 4;
            else
                currentItemIndex += Game1.InputManager.ScrollValue();
        }

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
        }

        public void NewItem(Item item)
        {
            for (int i = 0; i < 5; i++)
            {
                if (inventory[i] == null)
                {
                    inventory[i] = new InventoryItem(item, 1);
                    break;
                }
                if (item.Name == inventory[i].Item.Name)
                {
                    inventory[i].Amount++;
                    break;
                }
            }
        }

        public void RemoveItem(string name)
        {
            for (int i = 0; i < 5; i++)
            {
                if (inventory[i] == null)
                    continue;
                if (name == inventory[i].Item.Name)
                {
                    if (inventory[i].Amount == 1)
                        inventory[i] = null;
                    else
                        inventory[i].Amount--;
                    break;
                }
            }
        }

        public bool ItemInInventory(string name)
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

    }
}
