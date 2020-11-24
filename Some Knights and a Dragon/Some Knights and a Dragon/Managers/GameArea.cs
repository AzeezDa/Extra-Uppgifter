using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Entities;
using Some_Knights_and_a_Dragon.Entities.Creatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Managers
{
    public class GameArea // This class is represents the "world" or "place" where creatures are and how they interaect with the enviroment
    {
        public List<Creature> Creatures { get; protected set; } // All creatures in the game

        Texture2D Background; // The background image. TO BE CHANGED TO ADD SKY IMAGE AND FOREGROUNG IMAGE

        public Vector2 Gravity { get; set; } // Gravity of the area, used in the acceleration of the entities
        public Rectangle Boundries { get; set; } // The boundries of the places from which creatures cannot escape
        public GameArea(string backgroundFilePath)
        {
            // Initialize game area
            Background = Game1.ContentManager.Load<Texture2D>(backgroundFilePath);
            Creatures = new List<Creature>();
        }

        public void Update(ref GameTime gameTime)
        {

            // Updates all creatures, if health is less than 0, they die. Removed from the creature less
            for (int i = 0; i < Creatures.Count; i++)
            {
                Creatures[i].Update(ref gameTime);
                if (Creatures[i].CurrentHealth <= 0)
                {
                    Creatures.RemoveAt(i);
                }
            }

        }

        public void Draw(ref SpriteBatch _spriteBatch) // Draws background then creatures
        {
            

            _spriteBatch.Draw(Background, new Rectangle(0, 0, 1280, 960), Color.White);
            foreach (Creature creature in Creatures)
            {
                creature.Draw(ref _spriteBatch);
            }
        }

        public void AddCreature(Creature creature) // Used to add creatures from outside the class, such as summoning minions.
        {
            Creatures.Add(creature);
        }
    }
}
