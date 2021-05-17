using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Entities.Creatures
{
    public abstract class Creature : Entity // Creature
    {
        public int CurrentHealth { get; protected set; } // The current health of the creature
        public int MaxHealth { get; protected set; } // The maximum health of the creature

        public float GetHealthRatio { get => (float)CurrentHealth / (float)MaxHealth; } // Gets the ratio current / max health

        public List<Effects.Effect> CurrentEffects { get; private set; } // List of effects on the creature

        // Body part positions Used for items:

        private Vector2 handPosition;
        public Vector2 HandPosition { 
            get { return handPosition * (TextureDirection == TextureDirection.Right? new Vector2(-1, 1) : Vector2.One) + Position; } 
            protected set { handPosition = value * Sprite.Scale; } 
        }

        public Creature()
        {
            CurrentEffects = new List<Effects.Effect>();
        }


        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);
            for (int i = CurrentEffects.Count - 1; i >= 0; --i)
            {
                CurrentEffects[i].Update(gameTime, this);
                if (CurrentEffects[i].Duration <= 0)
                    CurrentEffects.RemoveAt(i);
            }
        }

        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            base.Draw(ref _spriteBatch);
            foreach (Effects.Effect effect in CurrentEffects)
            {
                effect.Draw(_spriteBatch, this);
            }
        }


        protected override void LoadSprite(string filepath)
        {
            Sprite = new Sprite("Entities/Creatures/" + filepath);
        }

        protected override void LoadSprite(string filepath, int width, int height)
        {
            Sprite = new Sprite("Entities/Creatures/" + filepath, width, height, 12);
        }

        public virtual void Attack()
        {

        }

        public virtual void Attack(Creature creature)
        {

        }

        public virtual void Attack(List<Creature> creatures)
        {

        }

        // Reduce current health if creature takes damage. Virtual because some creatures can reduce damage taken
        public virtual void TakeDamage(int amount)
        {
            CurrentHealth -= Math.Max(0, amount);
        }

        public virtual void AddHealth(int amount) // Add health to the creature
        {
            CurrentHealth = CurrentHealth + amount > MaxHealth ? MaxHealth : CurrentHealth + amount;
        }

        public void ChangeMaxHealth(int amount)
        {
            MaxHealth = amount;
        }

        public void ChangeCurrentHealth(int amount)
        {
            CurrentHealth = amount > MaxHealth ? MaxHealth : amount;
        }

        public void AddToMaxHealth(int amount)
        {
            MaxHealth += amount;
        }

        public void SetHealthToMax()
        {
            CurrentHealth = MaxHealth;
        }

        public void Kill()
        {
            CurrentHealth = 0;
        }

        // Animation Methods
        public virtual void ResetPose()
        {

        }

        public virtual void WalkAnimation()
        {

        }

        public bool MoveTo(GameTime gameTime, Vector2 position, Vector2 speed,  float range = 1) // Change velocity of creature to move to position, returns true if creature in range of the position
        {
            if (Vector2.Distance(position, Position) > range)
            {
                ChangeVelocity(Vector2.Normalize(position - Position) * speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                WalkAnimation();
                return false;
            }
            else
                return true;
        }

        // Effect methods
        public void AddEffect(Effects.Effect effect)
        {
            CurrentEffects.Add(effect);
        }

        public void RemoveEffect(string name)
        {
            for (int i = CurrentEffects.Count; i >= 0; --i)
            {
                if (CurrentEffects[i].Name == name)
                {
                    CurrentEffects.RemoveAt(i);
                }
            }
        }
    }
}
