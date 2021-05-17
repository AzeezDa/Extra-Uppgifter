using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Managers;

namespace Some_Knights_and_a_Dragon.Entities.Other
{
    public class Lightning
    {
        public Vector2 Position { get; private set; }
        public int Damage { get; private set; }
        public bool Done { get; private set; }

        private Sprite sprite;
        private int charge;
        double chargeTime;
        public Lightning(float horizontalPosition, int damage = 50)
        {
            Position = new Vector2(horizontalPosition, Game1.WindowManager.GetGameplayWindow().CurrentLevel.Boundries.Bottom);
            Damage = damage;
            charge = 0;
            chargeTime = 0;
            sprite = new Sprite("Other/lightning", 5, 1);
            sprite.Scale = 10;
            Done = false;
        }

        public void Update(GameTime gameTime)
        {
            chargeTime += gameTime.ElapsedGameTime.TotalSeconds;
            if (charge < 4)
                charge = (int)(0.01 * Math.Exp(chargeTime * 4) - 0.01);
            else
            {
                charge = 4;
                foreach (Creatures.Creature creature in Game1.WindowManager.GetGameplayWindow().CurrentLevel.Creatures)
                {
                    if (Vector2.Distance(Position, creature.Position) < 200)
                        creature.TakeDamage(Damage);
                }
                Done = true;
            }
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (charge > 4)
                sprite.DrawFrame(ref spriteBatch, new Vector2(Position.X, Game1.WindowManager.GetGameplayWindow().CurrentLevel.Boundries.Bottom), 0, 4, origin: new Vector2(sprite.Width / 2, sprite.Height));
            else
                sprite.DrawFrame(ref spriteBatch, new Vector2(Position.X, Game1.WindowManager.GetGameplayWindow().CurrentLevel.Boundries.Bottom), 0, charge, origin: new Vector2(sprite.Width / 2, sprite.Height));
        }
    }
}
