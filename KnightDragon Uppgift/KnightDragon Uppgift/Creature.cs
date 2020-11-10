using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightDragon_Uppgift
{
    class Creature
    {
        public int Health;
        public int Shield;
        protected string Name;

        public Creature(string name)
        {
            Name = name;
        }

        public string GetInfo()
        {
            return $"=={Name}==\nHealth: {Health}\nShield: {Shield}\n";
        }
    }
}
