using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uppgifter_OOP_arv
{
    class Dice
    {
        int sides;

        public Dice(int sides)
        {
            this.sides = sides;
        }

        public int ThrowDice()
        {
            return (new Random().Next(1, sides + 1));
        }
    }
}
