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
        public InventoryItem[] Inventory { get; private set; }
        public InventoryManager()
        {
            Inventory = new InventoryItem[5];
        }

        public void Update(ref GameTime gameTime)
        {

        }

        public void Draw(ref SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 5; i++)
            {
                if (Inventory[i] != null)
                {
                    spriteBatch.Draw(
                        Inventory[i].Item.Sprite.SpriteTexture,
                        new Rectangle(1280/2 + 50 * (i - 2), 960 - 50, Inventory[i].Item.Sprite.Width * 3, Inventory[i].Item.Sprite.Height * 3),
                        new Rectangle(0, 0, Inventory[i].Item.Sprite.Width, Inventory[i].Item.Sprite.Height),
                        Color.White, 0,
                        new Vector2(Inventory[i].Item.Sprite.Width / 2, Inventory[i].Item.Sprite.Height / 2), SpriteEffects.None, 0
                        );
                    Game1.FontManager.WriteText(spriteBatch, Inventory[i].Amount.ToString(), new Vector2(1280 / 2 + 50 * (i - 2), 960 - 50), Color.Red);
                }
            }
        }

        public void NewItem(Item item)
        {
            for (int i = 0; i < 5; i++)
            {
                if (Inventory[i] == null)
                {
                    Inventory[i] = new InventoryItem(item, 1);
                    break;
                }
                if (item.Name == Inventory[i].Item.Name)
                {
                    Inventory[i].Amount++;
                    break;
                }
            }
        }
    }
}
