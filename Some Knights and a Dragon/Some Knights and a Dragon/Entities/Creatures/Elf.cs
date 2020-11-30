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
        // List of all arrows, the elf shoots
        List<Arrow> arrows;

        // The drawback charge of the bow
        float bowCharge = 0;

        public Elf() 
        {
            Sprite = new Sprite("elf", 32, 32, 12);
            Position = new Vector2(200, 700);
            Speed = new Vector2(180, 1000);
            HitBoxWidth = 10;
            HitBoxHeight = 20;
            CurrentHealth = 800;
            MaxHealth = CurrentHealth;

            Name = "Thranduil";

            arrows = new List<Arrow>();
        }
        public override void Attack()
        {
            base.Attack();
            arrows.Add(new Arrow(this, Position, Vector2.Normalize(Game1.InputManager.GetCursor() - Position), (int)bowCharge));
        }

        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            base.Draw(ref _spriteBatch);
            HealthBar.FloatingBar(this, ref _spriteBatch);

            Game1.FontManager.WriteText(_spriteBatch, Name, Position - new Vector2(0, Sprite.Height * Sprite.Scale / 2 + 10), Color.Aqua);

            foreach (Arrow arrow in arrows)
            {
                arrow.Draw(ref _spriteBatch);
            }
        }

        public override void Update(ref GameTime gameTime)
        {

            // Controls animation of the sprite and the bow when charging and shooting
            Velocity.X = 0;
            Sprite.Animate(0, 1);
            if (Game1.InputManager.KeyPressed(Keys.D))
                Velocity.X += Speed.X;
            if (Game1.InputManager.KeyPressed(Keys.A))
                Velocity.X += -Speed.X;
            if (Game1.InputManager.KeyClicked(Keys.W) && Velocity.Y == 0)
                Velocity.Y += -Speed.Y;
            if (Game1.InputManager.KeyPressed(Keys.S))
                Velocity.Y += 1;

            if (Velocity != Vector2.Zero)
            {
                Sprite.Animate(0, 9);
            }

            if (Game1.InputManager.LeftMousePressed())
            {
                Sprite.Freeze(1, 9);
                bowCharge += bowCharge >= 10 ? 0 : (float)gameTime.ElapsedGameTime.TotalSeconds * 15f;
            }
            if (!Game1.InputManager.LeftMousePressed() && bowCharge > 0)
            {
                Sprite.Unfreeze();
                Attack();
                bowCharge = 0;
            }

            for (int i = 0; i < arrows.Count; i++)
            {
                arrows[i].Update(ref gameTime);
                if (arrows[i].LifeTime < 0)
                {
                    arrows[i] = null;
                    arrows.RemoveAt(i);
                    continue;
                }
                foreach (Creature creature in ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Creatures)
                {
                    if (creature == this)
                        continue;

                    if (arrows[i].HitBox.Intersects(creature.HitBox))
                    {
                        creature.TakeDamage(arrows[i].Damage);
                        arrows[i] = null;
                        arrows.RemoveAt(i);
                    }
                }
            }

            base.Update(ref gameTime);
        }
    }
}
