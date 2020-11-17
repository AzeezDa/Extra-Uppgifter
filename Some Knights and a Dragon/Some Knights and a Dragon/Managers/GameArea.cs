using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Entities;
using Some_Knights_and_a_Dragon.Entities.Creatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Managers
{
    public class GameArea
    {
        public List<Creature> Creatures { get; protected set; }

        Texture2D Background;

        public Vector2 Gravity { get; set; }
        public Rectangle Boundries { get; set; }
        public GameArea(string FilePath)
        {
            Background = Game1.ContentManager.Load<Texture2D>(FilePath);
            Creatures = new List<Creature>();
        }

        public void Update(ref GameTime gameTime)
        {
            for (int i = 0; i < Creatures.Count; i++)
            {
                Creatures[i].Update(ref gameTime);
                if (Creatures[i].CurrentHealth <= 0)
                {
                    Creatures.RemoveAt(i);
                }
            }

        }

        public void Draw(ref SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(Background, new Rectangle(0, 0, 1280, 960), Color.White);
            foreach (Creature creature in Creatures)
            {
                creature.Draw(ref _spriteBatch);
            }
        }

        public void AddCreature(Creature creature)
        {
            Creatures.Add(creature);
        }
    }
}
