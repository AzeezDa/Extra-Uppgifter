using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Item
{
    public abstract class Consumable : Item
    {
        public Consumable(string name, string filePath) : base(name, filePath)
        {

        }

        public virtual void OnUse()
        {

        }
    }
}
