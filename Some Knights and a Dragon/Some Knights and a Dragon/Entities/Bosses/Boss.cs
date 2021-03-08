using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Items;
using Some_Knights_and_a_Dragon.Managers;
using Some_Knights_and_a_Dragon.Managers.PlayerManagement;
using Some_Knights_and_a_Dragon.Windows;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Some_Knights_and_a_Dragon.Entities.Creatures
{
    public abstract class Boss
    {
        public Creature Creature { get; protected set; } // The Creature of the boss
        protected List<InventoryItem> Loot; // List of all the loot dropped from the boss
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
            HealthBar.BossHealthBar(Creature, ref spriteBatch); // Mega healthbar on the screen
        }

        // On death: the boss Drops all the loot and update the high score recorder
        public virtual void OnDeath()
        {
            Game1.WindowManager.GetGameplayWindow().HighScoreRecorder.BossDefeatedUpdate();
            Random r = new Random();
            for (int i = 0; i < Loot.Count; i++)
            {
                for (int j = 0; j < Loot[i].Amount; j++)
                {
                    Game1.WindowManager.GetGameplayWindow().CurrentLevel.AddDroppedItem(Creature.Position + new Vector2(r.Next(-10, 10), 0), Loot[i].Item);
                }
            }
        }

        // Adds to the loot dropped from the boss
        protected void AddLoot(Item item, int amount)
        {
            Loot.Add(new InventoryItem(item, amount));
        }
    }
}
