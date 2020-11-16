using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Entities.Creatures
{
    public abstract class Creature : Entity
    {
        public int CurrentHealth { get; protected set; }
        public int MaxHealth { get; protected set; }

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

        public virtual void TakeDamage(int amount)
        {
            CurrentHealth -= amount;
        }
    }
}
