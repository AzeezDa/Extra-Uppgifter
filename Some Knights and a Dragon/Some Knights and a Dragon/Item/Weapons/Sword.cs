using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Item.Weapons
{
    public class Sword : Weapon
    {
        public Sword() : base("Sword", "Sword", 15)
        {
            Description = "This sword can cut enemies, pretty good ey?";
        }
    }
}
