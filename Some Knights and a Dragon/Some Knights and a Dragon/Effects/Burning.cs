using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Entities.Creatures;
using Some_Knights_and_a_Dragon.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Effects
{
    public class Burning : Effect // BURNING EFFECT: Take damage every second
    {
        Timer timer;
        public Burning(int duration) : base("Burning", duration)
        {
            Description = "On fire! Burn! Burn! BURN!";
            timer = new Timer(1000);
        }

        public override void Draw(SpriteBatch spriteBatch, Creature creature)
        {
            base.Draw(spriteBatch, creature);
        }

        public override void Update(GameTime gameTime, Creature creature)
        {
            base.Update(gameTime, creature);
            
            // Player takes damage every timer duration
            timer.CheckTimer(gameTime);
            if (timer.TimerOn)
                creature.TakeDamage(3);
        }
    }
}
