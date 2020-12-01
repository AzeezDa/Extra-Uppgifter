using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Items.Weapons
{
    public class ElvenBow : Weapon
    {

        public ElvenBow()
        {
            Name = "Elven Bow";
            Description = "This bow of an elf was not left on the shelf.";
            LoadSprite("bow", 16, 16);
            Damage = 10;
        }
    }
}
