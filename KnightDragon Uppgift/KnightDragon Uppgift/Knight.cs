using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightDragon_Uppgift
{
    class Knight : Creature
    {
        protected string Weapon;

        public Knight(string name, string weapon) : base(name)
        {
            Weapon = weapon;
        }

        public void Attack()
        {
            Console.WriteLine($"Knight {Name} attacked with {Weapon}!");
        }
    }
}
