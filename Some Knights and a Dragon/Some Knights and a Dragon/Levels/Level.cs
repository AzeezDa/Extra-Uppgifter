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
        public List<Creature> Creatures { get; private set; } // All creatures in the game
        [XmlIgnore]
        public List<DroppedItem> DroppedItems { get; private set; } // Items that is dropped and picked by the player
        [XmlIgnore]
        public List<Projectile> Projectiles { get; private set; } // Projectiles shot by the creatures
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
            Creatures = new List<Creature>();
            DroppedItems = new List<DroppedItem>();
            Projectiles = new List<Projectile>();

            Game1.TextureManager.Reload(TexturesToLoad);

            // Plays the song of the level
            Game1.SongManager.Play(LevelMusic);

            // Loads the background and its content
            Background.LoadContent();

            // Sets up the boss
            Boss = (Boss)Activator.CreateInstance(null, BossClassName).Unwrap();
            Creatures.Add(Boss.Creature);

            // Loads the trading system for the level
            TradingManager.LoadTrading();

            // Sets the player's position to the level's starting position
            Game1.WindowManager.GetGameplayWindow().Player.Creature.ChangePosition(PlayerStartingPosition);

        }

        public void Update(ref GameTime gameTime)
        {
            // Updates all creatures, if health is less than 0, they die. Removed from the creature less
            for (int i = Creatures.Count - 1; i >= 0; --i)
            {
                Creatures[i].Update(ref gameTime);
                if (Creatures[i].CurrentHealth <= 0)
                {
                    Creatures[i] = null;
                    Creatures.RemoveAt(i);
                }
            }

            // Updates all the items dropped on the ground
            for (int i = DroppedItems.Count - 1; i >= 0; --i)
            {
                DroppedItems[i].Update(ref gameTime);
                if (DroppedItems[i].LifeTime >= 300) // Removed after 5 mins
                {
                    DroppedItems[i] = null;
                    DroppedItems.RemoveAt(i);
                }
            }

            // Updates all the projectiles, if lifetime is less than or equal to 0, they are removed.
            for (int i = Projectiles.Count - 1; i >= 0; --i)
            {
                Projectiles[i].Update(ref gameTime);
                if (Projectiles[i].LifeTime <= 0)
                {
                    Projectiles[i] = null;
                    Projectiles.RemoveAt(i);
                }
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
            foreach (Creature creature in Creatures)
            {
                creature.Draw(ref spriteBatch);
            }
            foreach (DroppedItem droppedItem in DroppedItems)
            {
                droppedItem.Draw(ref spriteBatch);
            }
            foreach (Projectile projectile in Projectiles)
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

        }

        public void AddCreature(Creature creature) // Used to add creatures from outside the class, such as summoning minions.
        {
            Creatures.Add(creature);
        }

        public void AddProjectile(Projectile projectile) // Adds a projectiles to the projectile list
        {
            Projectiles.Add(projectile);
        }

        public void AddDroppedItem(Vector2 position, Item item) // Adds a dropped item to its list.
        {
            DroppedItems.Add(new DroppedItem(position, item));
        }

        public void AddDroppedItem(Vector2 position, Item item, int amount) // Adds a dropped item to its list.
        {
            for (int i = 0; i < amount; i++)
            {
                DroppedItems.Add(new DroppedItem(position, item));
            }
        }

        public void RemoveAllOfDroppedItem(string name) // Removes all of a dropped item
        {
            foreach (DroppedItem item in DroppedItems)
            {
                if (item.Name == name)
                {
                    item.Remove();
                }
            }
        }
    }
}
