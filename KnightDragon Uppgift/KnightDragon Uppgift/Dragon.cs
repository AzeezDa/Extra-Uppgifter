using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightDragon_Uppgift
{
    class Dragon : Creature
    {
        protected string Color;

        public Dragon(string name, string color) : base(name)
        {
            Color = color;
        }

        public void Attack()
        {
            Console.WriteLine($"The {Color} Dragon attacked!");
        }
    }
}
