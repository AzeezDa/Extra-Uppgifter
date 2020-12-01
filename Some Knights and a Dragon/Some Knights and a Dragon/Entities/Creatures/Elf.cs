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
        }
        public override void Attack()
        {
            base.Attack();
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
