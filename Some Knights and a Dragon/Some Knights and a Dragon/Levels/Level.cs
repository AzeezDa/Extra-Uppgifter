using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Some_Knights_and_a_Dragon.Entities;
using Some_Knights_and_a_Dragon.Entities.Creatures;
using Some_Knights_and_a_Dragon.Entities.Projectiles;
using Some_Knights_and_a_Dragon.Managers;
using Some_Knights_and_a_Dragon.Items;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using Some_Knights_and_a_Dragon.Windows;
using Some_Knights_and_a_Dragon.Managers.PlayerManagement;

namespace Some_Knights_and_a_Dragon.Levels
{
    public class Level // This class is represents the "world" or "place" where creatures are and how they interaect with the enviroment
    {
        [XmlIgnore]
        public Dictionary<int, Creature> Creatures { get; private set; } // All creatures in the game
        [XmlIgnore]
        public Dictionary<int, DroppedItem> DroppedItems { get; private set; } // Items that is dropped and picked by the player
        [XmlIgnore]
        public Dictionary<int, Projectile> Projectiles { get; private set; } // Projectiles shot by the creatures
        [XmlIgnore]
        public Boss Boss { get; set; } // The boss object
        [XmlIgnore]
        public TradingManager TradingManager { get; private set; } // The trading manager with the mysterious man

        // LOADED FROM XML
        public string Name { get; set; } // The name of the level
        public string BossClassName { get; set; } // Boss class name
        public string NextLevel { get; set; } // Xml file fo the next level
        public string LevelMusic { get; set; } // Music file name
        public string TraderData { get; set; } // XML files of the trades from the mysterious man
        public Background Background { get; set; } // The background image class
        public Vector2 Gravity { get; set; } // Gravity of the area, used in the acceleration of the entities
        public Rectangle Boundries { get; set; } // The boundries of the places from which creatures cannot escape
        public Vector2 PlayerStartingPosition { get; set; } // Self described
        public List<string> TexturesToLoad { get; set; } // This list is sent to the TextureManager during level loading to preload the textures of the levels to improve performance

        public Level()
        {
            TradingManager = new TradingManager();
        }

        public void LoadContent() // Loads the level
        {
            // Recreates the Entity lists
            Creatures = new Dictionary<int, Creature>();
            DroppedItems = new Dictionary<int, DroppedItem>();
            Projectiles = new Dictionary<int, Projectile>();

            Game1.TextureManager.Reload(TexturesToLoad);

            // Plays the song of the level
            Game1.SongManager.Play(LevelMusic);

            // Loads the background and its content
            Background.LoadContent();

            // Sets up the boss
            Boss = (Boss)Activator.CreateInstance(null, BossClassName).Unwrap();
            AddCreature(Boss.Creature);

            // Loads the trading system for the level
            TradingManager.LoadTrading();

            // Sets the player's position to the level's starting position
            Game1.WindowManager.GetGameplayWindow().Player.Creature.ChangePosition(PlayerStartingPosition);

        }

        public void Update(ref GameTime gameTime)
        {
            List<int> creaturesToRemove = new List<int>();

            // Updates all creatures, if health is less than 0, they die. Removed from the creature less
            foreach (int i in Creatures.Keys)
            {
                Creatures[i].Update(ref gameTime);
                if (Creatures[i].CurrentHealth <= 0)
                    creaturesToRemove.Add(i);
            }

            foreach (int i in creaturesToRemove)
            {
                Creatures[i] = null;
                Creatures.Remove(i);
            }

            List<int> droppedItemsToRemove = new List<int>();

            // Updates all the items dropped on the ground
            foreach (int i in DroppedItems.Keys)
            {
                DroppedItems[i].Update(ref gameTime);
                if (DroppedItems[i].LifeTime >= 300) // Removed after 5 mins
                    droppedItemsToRemove.Add(i);
            }

            foreach (int i in droppedItemsToRemove)
            {
                DroppedItems[i] = null;
                DroppedItems.Remove(i);
            }

            List<int> projectilesToRemove = new List<int>();

            // Updates all the projectiles, if lifetime is less than or equal to 0, they are removed.
            foreach (int i in Projectiles.Keys)
            {
                Projectiles[i].Update(ref gameTime);
                if (Projectiles[i].LifeTime <= 0)
                    projectilesToRemove.Add(i);
            }

            foreach (int i in projectilesToRemove)
            {
                Projectiles[i] = null;
                Projectiles.Remove(i);
            }

            // If the boss of the level is up then they are updates, when dead a text appears to press N to continue.
            Background.Update(gameTime);
            if (Boss.IsAlive)
                Boss.Update(gameTime);
            else
            {
                TradingManager.Update(gameTime);
                if (Game1.InputManager.KeyClicked(Microsoft.Xna.Framework.Input.Keys.N))
                {
                    Game1.WindowManager.GetGameplayWindow().NewLevel(NextLevel);
                }
            }
        }

        public void Draw(ref SpriteBatch spriteBatch) // Draws background then creatures
        {
            // Draws everything in this order: Background, Creatures, Items, Projectiles, Boss, Text
            Background.Draw(spriteBatch);
            foreach (Creature creature in Creatures.Values)
            {
                creature.Draw(ref spriteBatch);
            }
            foreach (DroppedItem droppedItem in DroppedItems.Values)
            {
                droppedItem.Draw(ref spriteBatch);
            }
            foreach (Projectile projectile in Projectiles.Values)
            {
                projectile.Draw(ref spriteBatch);
            }
            if (Boss.IsAlive)
                Boss.Draw(spriteBatch);
            else
            {
                TradingManager.Draw(spriteBatch);
                Game1.FontManager.WriteText(spriteBatch, "Press N to Continue!", new Vector2(640, 480));
            }

            Game1.FontManager.WriteText(spriteBatch, Name, new Vector2(640, 100));
        }

        public void AddCreature(Creature creature) // Used to add creatures from outside the class, such as summoning minions.
        {
            int id;
            for (id = Game1.Random.Next(int.MinValue, int.MaxValue); Creatures.ContainsKey(id);)
                id = Game1.Random.Next(int.MinValue, int.MaxValue);

            Creatures.Add(id, creature);
            creature.ID = id;
        }

        public void AddProjectile(Projectile projectile) // Adds a projectiles to the projectile list
        {
            int id;
            for (id = Game1.Random.Next(int.MinValue, int.MaxValue); Projectiles.ContainsKey(id);)
                id = Game1.Random.Next(int.MinValue, int.MaxValue);

            Projectiles.Add(id, projectile);
            projectile.ID = id;
        }

        public void AddDroppedItem(Vector2 position, Item item) // Adds a dropped item to its list.
        {
            int id;
            for (id = Game1.Random.Next(int.MinValue, int.MaxValue); DroppedItems.ContainsKey(id);)
                id = Game1.Random.Next(int.MinValue, int.MaxValue);

            DroppedItems.Add(id, new DroppedItem(position, item));
            DroppedItems[id].ID = id;
        }

        public void AddDroppedItem(Vector2 position, Item item, int amount) // Adds a dropped item to its list.
        {
            // FIX
            int id;
            for (id = Game1.Random.Next(int.MinValue, int.MaxValue); Creatures.ContainsKey(id);)
                id = Game1.Random.Next(int.MinValue, int.MaxValue);

            for (int i = 0; i < amount; i++)
            {
                DroppedItems.Add(id, new DroppedItem(position, item));
            }
        }

        public void RemoveAllOfDroppedItem(string name) // Removes all of a dropped item
        {
            foreach (DroppedItem item in DroppedItems.Values)
            {
                if (item.Name == name)
                {
                    item.Remove();
                }
            }
        }
    }
}
