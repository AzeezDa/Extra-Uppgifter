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

        public Creature()
        {
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
    }
}
