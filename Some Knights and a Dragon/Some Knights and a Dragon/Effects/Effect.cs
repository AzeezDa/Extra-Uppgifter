using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Entities.Creatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Effects
{
    public abstract class Effect // Effects such as poisoned, burning, sleeping etc. derive from this class.
    {
        public string Name { get; protected set; } // Name of the effect
        public string Description { get; protected set; } // Description of the effect
        protected Texture2D Texture; // Texture displayed when having this effect (WORK IN PROGRESS)
        public double Duration { get; protected set; } // Duration of the effect
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
