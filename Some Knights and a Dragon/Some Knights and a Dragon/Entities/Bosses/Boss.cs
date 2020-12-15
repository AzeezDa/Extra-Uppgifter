using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Items;
using Some_Knights_and_a_Dragon.Managers;
using Some_Knights_and_a_Dragon.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Entities.Creatures
{
    public abstract class Boss
    {
        public Creature Creature { get; protected set; }
        protected List<Item> Loot;
        public Boss()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            HealthBar.BossHealthBar(Creature, ref spriteBatch);
        }

        public virtual void OnDeath()
        {
            for (int i = 0; i < Loot.Count; i++)
            {
                ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.AddDroppedItem(Creature.Position, Loot[i]);
            }
        }
    }
}
