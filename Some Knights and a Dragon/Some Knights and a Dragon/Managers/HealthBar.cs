using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Entities;
using Some_Knights_and_a_Dragon.Entities.Creatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Managers
{
    public static class HealthBar
    {
        // Health bars textures to be loaded in a static method
        private static Texture2D barBackground;
        private static Texture2D bar;

        private static Sprite bossHealthBar;
        public static void Setup(ref SpriteBatch spriteBatch) // Sets up the textures
        {
            barBackground = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            barBackground.SetData(new Color[] { Color.White });
            bar = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            bar.SetData(new Color[] { Color.White });
            bossHealthBar = new Sprite("Menus/bossHealthbar", 1, 2);
        }
        public static void BossHealthBar(Creature creature, ref SpriteBatch spriteBatch) // A large healthbar displayed at the top of the window
        {
            bossHealthBar.DrawFrame(ref spriteBatch, new Vector2(640, 100), 0, 0);
            bossHealthBar.DrawFrame(ref spriteBatch, new Vector2(315, 35), 1, 0, creature.GetHealthRatio, 1, origin: Vector2.Zero);
            //spriteBatch.Draw(barBackground, new Rectangle(150, 100, 980, 50), Color.Red);
            //spriteBatch.Draw(bar, new Rectangle(150, 100, 980 * creature.CurrentHealth / creature.MaxHealth, 50), Color.Lime);
        }

        public static void FloatingBar(Creature creature, ref SpriteBatch spriteBatch) // A healthbar displayed over the creature
        {
            spriteBatch.Draw(barBackground, new Rectangle((int)creature.Position.X - 25, (int)creature.Position.Y - creature.Sprite.Height * creature.Sprite.Scale / 2, 50, 10), Color.Red);
            spriteBatch.Draw(bar, new Rectangle((int)creature.Position.X - 25, (int)creature.Position.Y - creature.Sprite.Height * creature.Sprite.Scale / 2,
                                                50 * creature.CurrentHealth / creature.MaxHealth, 10), Color.Lime);
        }
    }
}
