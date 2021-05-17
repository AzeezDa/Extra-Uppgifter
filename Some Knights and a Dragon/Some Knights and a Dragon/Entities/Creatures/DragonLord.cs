using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Some_Knights_and_a_Dragon.Entities.Creatures
{
    public class DragonLord : Creature
    {
        public bool NightmareMode;
        public DragonLord()
        {
            LoadSprite("dragonLord", 10, 5);
            Position = new Vector2(1200, 700);
            Speed = new Vector2(200, 700);
            MaxHealth = 100000;
            CurrentHealth = MaxHealth;
            Sprite.Scale = 7;
            NightmareMode = false;
        }

        public override void ResetPose()
        {
            base.ResetPose();
            if (NightmareMode)
                Sprite.Animate(3, 4);
            else
                Sprite.Animate(0, 4);
        }

        public override void Attack()
        {
            if (NightmareMode)
                Sprite.OneTimeAnimation(4, 5);
            else
                Sprite.OneTimeAnimation(1, 5);
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);
            if (NightmareMode)
                Sprite.Animate(3, 4);
            else
                Sprite.Animate(0, 4);
        }
    }
}
