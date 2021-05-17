using Microsoft.Xna.Framework;
using Some_Knights_and_a_Dragon.Entities.Creatures;
using Some_Knights_and_a_Dragon.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Effects
{
    class Plagued : Effect
    {
        Timer timer = new Timer(1000);
        public Plagued(int duration) : base("Plagued", duration)
        {
            Description = "The Plague is upon you!";
        }

        public override void Update(GameTime gameTime, Creature creature)
        {
            base.Update(gameTime, creature);
            timer.CheckTimer(gameTime);

            // Player takes damage based on health. Less health, more damage.
            if (timer.TimerOn)
            {
                creature.TakeDamage((int)(50.0f * (1 - creature.GetHealthRatio)));
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Creature creature)
        {
            base.Draw(spriteBatch, creature);
        }
    }
}
