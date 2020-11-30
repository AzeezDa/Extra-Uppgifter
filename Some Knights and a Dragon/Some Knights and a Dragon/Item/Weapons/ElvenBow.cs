using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Item.Weapons
{
    public class ElvenBow : Weapon
    {

        public ElvenBow() : base("Elven Bow", "ElvenBow", 10)
        {
            Description = "This bow of an elf was not left on the shelf.";
        }
    }
}
