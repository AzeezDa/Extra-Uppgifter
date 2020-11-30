using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Item
{
    public abstract class Weapon : Item
    {
        public int Damage { get; protected set; }
        public Weapon(string name, string filePath, int damage) : base (name, filePath)
        {

        }
    }
}
