using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Entities.Creatures
{
    public class StoneQueen : Creature
    {
        public StoneQueen()
        {
            LoadSprite("stoneQueen", 11, 3);
            Position = new Vector2(1200, 400);
            Speed = new Vector2(250, 1000);
            MaxHealth = 50000;
            CurrentHealth = MaxHealth;
            Sprite.Scale = 7;
        }

        public override void ResetPose()
        {
            base.ResetPose();
            Sprite.Animate(2, 1);
        }

        public override void WalkAnimation()
        {
            base.WalkAnimation();
            Sprite.OneTimeAnimation(2, 6);
        }
    }
}
