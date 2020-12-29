using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Items;
using Some_Knights_and_a_Dragon.Managers;
using Some_Knights_and_a_Dragon.Managers.PlayerManagement;
using Some_Knights_and_a_Dragon.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Entities.Creatures
{
    public abstract class Boss
    {
        public Creature Creature { get; protected set; }
        protected List<InventoryItem> Loot;
        public bool IsAlive { get; protected set; }
        public Boss()
        {
            Loot = new List<InventoryItem>();
            IsAlive = true;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (Creature.CurrentHealth <= 0 && IsAlive)
            {
                OnDeath();
                IsAlive = false;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            HealthBar.BossHealthBar(Creature, ref spriteBatch);
        }

        public virtual void OnDeath()
        {
            Random r = new Random();
            for (int i = 0; i < Loot.Count; i++)
            {
                for (int j = 0; j < Loot[i].Amount; j++)
                {
                    ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.AddDroppedItem(Creature.Position + new Vector2(r.Next(-10, 10), 0), Loot[i].Item);
                }
            }
        }

        protected void AddLoot(Item item, int amount)
        {
            Loot.Add(new InventoryItem(item, amount));
        }
    }
}
