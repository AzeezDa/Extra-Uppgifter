using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Entities.Creatures
{
    public class Bird : Creature 
    {
        public Bird()
        {
            LoadSprite("bird", 8, 1);
            Position = new Vector2(900, 400);
            Speed = new Vector2(250, 8000);
            MaxHealth = 500;
            CurrentHealth = MaxHealth;
            Sprite.Scale = 4;
        }

        public override void WalkAnimation()
        {
            Sprite.Animate(0, 8);
        }
    }
}
