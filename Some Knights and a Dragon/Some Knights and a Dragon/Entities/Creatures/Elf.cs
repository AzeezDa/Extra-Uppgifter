using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Some_Knights_and_a_Dragon.Entities.Projectiles;
using Some_Knights_and_a_Dragon.Managers;
using Some_Knights_and_a_Dragon.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Entities.Creatures
{
    public class Elf : Creature
    {

        public Elf() 
        {
            LoadSprite("elf", 11, 32);
            Position = new Vector2(400, 400);
            Speed = new Vector2(250, 1000);
            CurrentHealth = 100;
            MaxHealth = 100;
            HandPosition = new Vector2(0, 2);
        }
        public override void Attack()
        {
            base.Attack();
            HandPosition = new Vector2(-3, 0);
            Sprite.OneTimeAnimation(1, 2);
        }


        public override void ResetPose()
        {
            base.ResetPose();
            HandPosition = new Vector2(0, 2);
            Sprite.Unfreeze();
        }

        public override void Walk()
        {
            base.Walk();
            Sprite.Unfreeze();
            Sprite.Animate(0, 9);
        }

        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            base.Draw(ref _spriteBatch);
        }

        public override void Update(ref GameTime gameTime)
        {

            // Controls animation of the sprite and the bow when charging and shooting


            base.Update(ref gameTime);
        }
    }
}
