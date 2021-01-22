using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Entities.Creatures
{
    public class StoneGuardian : Creature
    {
        int brokenFrame;
        public StoneGuardian()
        {
            LoadSprite("stoneGuardian", 6, 1);
            Position = new Vector2(900, 400);
            Speed = new Vector2(0, 0);
            MaxHealth = 2000;
            CurrentHealth = MaxHealth;
            Sprite.Scale = 10;
            brokenFrame = 0;
        }
        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);
            brokenFrame = 5 - (int)((float)CurrentHealth  / (float)MaxHealth * 5);
            Sprite.Freeze(0, brokenFrame);
        }
    }
}
