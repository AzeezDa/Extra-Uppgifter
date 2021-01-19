using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Entities.Creatures;
using Some_Knights_and_a_Dragon.Items;
using Some_Knights_and_a_Dragon.Items.Other;

namespace Some_Knights_and_a_Dragon.Managers.PlayerManagement
{
    public struct TradeItem // Items sold by the mysterious man
    {
        public Item Item { get; private set; }
        public int Cost { get; private set; }
        public int Amount { get; private set; }

        public TradeItem(string name, int cost, int amount)
        {
            Item = (Item)Activator.CreateInstance(null, name).Unwrap();
            Cost = cost;
            Amount = amount;
        }

    }
    public class TradingManager // Manages the trading with the mysterious man
    {
        List<TradeItem> tradeItems; // List of items in the inventory of the trader
        Creature creature; // Creature object, the mysterious man
        bool playerClose; // True if the player is close to the mysterious man
        int tradingInventoryIndex; // Index of the current item shown in the mysterious man
        TradeItem currentItem { get => tradeItems[tradingInventoryIndex]; } // The current item displayed

        public TradingManager()
        {
            LoadTrading();
            creature = new MysteriousMan();
            tradingInventoryIndex = 0;
        }
           
        public void Update(GameTime gameTime)
        {
            creature.Update(ref gameTime);

            if (Vector2.Distance(Game1.WindowManager.GetGameplayWindow().Player.Creature.Position, creature.Position) < 100)
                playerClose = true;

            if (playerClose)
            {
                int scrollValue = Game1.InputManager.ScrollValue();

                if (Game1.InputManager.KeyClicked(Microsoft.Xna.Framework.Input.Keys.E))
                {
                    creature.Sprite.OneTimeAnimation(0, 2);
                }

                if (scrollValue != 0)
                {
                    // Change the index of the inventory from the mysterious man depending on scroll value
                    tradingInventoryIndex = tradingInventoryIndex + scrollValue < 0 ? tradeItems.Count - 1 : 
                                            tradingInventoryIndex + scrollValue >= tradeItems.Count ? 0:
                                            tradingInventoryIndex + scrollValue; 
                }

                if (Game1.InputManager.KeyClicked(Microsoft.Xna.Framework.Input.Keys.E))
                {
                    if (Game1.WindowManager.GetGameplayWindow().Player.Inventory.ItemInInventory("Coin", currentItem.Cost))
                    {
                        Game1.WindowManager.GetGameplayWindow().Player.Inventory.RemoveItem("Coin", currentItem.Cost);
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.AddDroppedItem(
                            Game1.WindowManager.GetGameplayWindow().Player.Creature.Position,
                            currentItem.Item, currentItem.Amount);
                    }
                }

                if (Game1.InputManager.KeyClicked(Microsoft.Xna.Framework.Input.Keys.F))
                {
                    if (Game1.WindowManager.GetGameplayWindow().Player.Inventory.ItemInInventory(currentItem.Item.Name, currentItem.Amount))
                    {
                        Game1.WindowManager.GetGameplayWindow().Player.Inventory.RemoveItem(currentItem.Item.Name, currentItem.Amount);
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.AddDroppedItem(
                            Game1.WindowManager.GetGameplayWindow().Player.Creature.Position,
                            new Coin(), currentItem.Cost);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            creature.Draw(ref spriteBatch);
            if (playerClose)
            {
                Game1.FontManager.WriteText(spriteBatch, "Press E to Buy, F to Sell, Scroll to Browse", creature.Position + new Vector2(0, -170));

                Game1.FontManager.WriteText(spriteBatch, 
                    $"{tradeItems[tradingInventoryIndex].Amount} {tradeItems[tradingInventoryIndex].Item.Name} for {tradeItems[tradingInventoryIndex].Cost} Coins",
                    creature.Position + new Vector2(0, -150));

                tradeItems[tradingInventoryIndex].Item.Sprite.Draw(ref spriteBatch, creature.Position + new Vector2(0, -100), Entities.TextureDirection.Right);
            }
        }

        public void LoadTrading()
        {
            tradeItems = new List<TradeItem>();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(Environment.CurrentDirectory + "/../../../Data/TradingData.xml");
            foreach (XmlNode node in xmlDocument.SelectSingleNode("Items").ChildNodes)
            {
                tradeItems.Add(new TradeItem(node.Attributes["Name"].Value, int.Parse(node.Attributes["Cost"].Value), int.Parse(node.Attributes["Amount"].Value)));
            }
        }
    }
}
