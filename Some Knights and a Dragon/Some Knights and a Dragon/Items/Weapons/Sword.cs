using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Items.Weapons
{
    public class Sword : Weapon
    {
        public Sword()
        {
            Name = "Sword";
            Description = "This sword can cut enemies, pretty good, ey?";
            LoadSprite("sword");
            Damage = 15;
            Handle = new Vector2(0, 2);
        }
    }
}
