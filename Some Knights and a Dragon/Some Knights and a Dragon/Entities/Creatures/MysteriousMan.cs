using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Some_Knights_and_a_Dragon.Managers;
using Some_Knights_and_a_Dragon.Items;

namespace Some_Knights_and_a_Dragon.Entities.Creatures
{

    public class MysteriousMan : Creature
    {
        bool playerClose;
        public MysteriousMan()
        {
            LoadSprite("mysteriousMan", 4, 2);
            Position = new Vector2(1000, 400);
            Speed = new Vector2(250, 1000);
            CurrentHealth = 100;
            MaxHealth = 100;
            HandPosition = new Vector2(0, 2);
        }

        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            base.Draw(ref _spriteBatch);
            if (playerClose)
            {
                Game1.FontManager.WriteText(_spriteBatch, "Press E to Interact", Position + new Vector2(0, -100));
            }
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);
            playerClose = false;

        }
        public override void TakeDamage(int amount)
        {
            // Does not take damage
        }
    }
}
