using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Entities.Creatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Effects
{
    public abstract class Effect
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        protected Texture2D Texture;
        public double Duration { get; protected set; }
        public Effect(string name, int duration)
        {
            Duration = duration;
            Name = name;
        }

        public virtual void Update(GameTime gameTime, Creature creature)
        {
            Duration -= gameTime.ElapsedGameTime.TotalSeconds;
        }

        public virtual void Draw(SpriteBatch spriteBatch, Creature creature)
        {
            // spriteBatch.Draw(Texture, creature.Position, Color.White);
        }
    }
}
