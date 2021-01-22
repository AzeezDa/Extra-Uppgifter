using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Entities.Creatures
{
    public class Snake : Creature
    {
        public Snake()
        {
            LoadSprite("snake", 8, 2);
            Position = new Vector2(900, 850);
            Speed = new Vector2(50, 0);
            MaxHealth = 100;
            CurrentHealth = MaxHealth;
            Sprite.Scale = 4;
        }

        public override void WalkAnimation()
        {
            Sprite.Animate(1, 8);
        }

        public override void Attack()
        {
            base.Attack();
            Sprite.OneTimeAnimation(0, 3);
        }
    }
}
