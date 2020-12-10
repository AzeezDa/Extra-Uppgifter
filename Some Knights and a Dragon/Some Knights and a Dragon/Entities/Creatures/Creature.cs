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

        // Body part positions Used for items:

        private Vector2 handPosition;
        public Vector2 HandPosition { 
            get { return handPosition * (TextureDirection == TextureDirection.Right? new Vector2(-1, 1) : Vector2.One) + Position; } 
            protected set { handPosition = value * Sprite.Scale; } 
        }

        public Creature()
        {
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
            CurrentHealth -= amount;
        }

        // Animation Methods
        public virtual void ResetPose()
        {

        }

        public virtual void Walk()
        {

        }
    }
}
