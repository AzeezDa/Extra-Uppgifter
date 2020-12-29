using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Entities;
using Some_Knights_and_a_Dragon.Entities.Creatures;
using Some_Knights_and_a_Dragon.Entities.Projectiles;
using Some_Knights_and_a_Dragon.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Managers
{
    public class GameArea // This class is represents the "world" or "place" where creatures are and how they interaect with the enviroment
    {
        public List<Creature> Creatures { get; protected set; } // All creatures in the game
        public List<DroppedItem> DroppedItems { get; protected set; }
        public List<Projectile> Projectiles { get; protected set; }

        public Background Background { get; set; } // The background image. TO BE CHANGED TO ADD SKY IMAGE AND FOREGROUNG IMAGE

        public Vector2 Gravity { get; set; } // Gravity of the area, used in the acceleration of the entities
        public Rectangle Boundries { get; set; } // The boundries of the places from which creatures cannot escape

        public Boss Boss { get; set; }

        public GameArea(string backgroundFilePath)
        {
            // Initialize game area
            Background = new Background(backgroundFilePath, 4);
            Creatures = new List<Creature>();
            DroppedItems = new List<DroppedItem>();
            Projectiles = new List<Projectile>();
        }

        public void Update(ref GameTime gameTime)
        {
            // Updates all creatures, if health is less than 0, they die. Removed from the creature less
            for (int i = Creatures.Count - 1; i >= 0; --i)
            {
                Creatures[i].Update(ref gameTime);
                if (Creatures[i].CurrentHealth <= 0)
                {
                    if (Boss.Creature == Creatures[i])
                    Creatures[i] = null;
                    Creatures.RemoveAt(i);
                }
            }

            foreach (DroppedItem droppedItem in DroppedItems)
            {
                droppedItem.Update(ref gameTime);
            }

            for (int i = Projectiles.Count - 1; i >= 0; --i)
            {
                Projectiles[i].Update(ref gameTime);
                if (Projectiles[i].LifeTime <= 0)
                {
                    Projectiles[i] = null;
                    Projectiles.RemoveAt(i);
                }
            }

            if (Boss.IsAlive)
                Boss.Update(gameTime);

            Background.Update(gameTime);
        }

        public void Draw(ref SpriteBatch _spriteBatch) // Draws background then creatures
        {
            Background.Draw(_spriteBatch);
            foreach (Creature creature in Creatures)
            {
                creature.Draw(ref _spriteBatch);
            }
            foreach (DroppedItem droppedItem in DroppedItems)
            {
                droppedItem.Draw(ref _spriteBatch);
            }
            foreach (Projectile projectile in Projectiles)
            {
                projectile.Draw(ref _spriteBatch);
            }
            if (Boss.Creature.CurrentHealth > 0)
                Boss.Draw(_spriteBatch);
        }

        public void AddCreature(Creature creature) // Used to add creatures from outside the class, such as summoning minions.
        {
            Creatures.Add(creature);
        }

        public void AddProjectile(Projectile projectile)
        {
            Projectiles.Add(projectile);
        }

        public void AddDroppedItem(Vector2 position, Item item)
        {
            DroppedItems.Add(new DroppedItem(position, item));
        }
    }
}
