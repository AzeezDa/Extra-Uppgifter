using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uppgifter_OOP_arv
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("How many sides? ");
            int sides = 0;
            bool correct_input = false;
            while (!correct_input)
            {
                correct_input = int.TryParse(Console.ReadLine(), out sides);
                if (!correct_input)
                {
                    Console.WriteLine("Error input, try again!");
                }
            }
            Console.Clear();
            Dice dice = new Dice(sides);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Click any key to throw (CTRL-C or close program to stop)");
                Console.Write("Dice: ");
                Console.Write(dice.ThrowDice());
                Console.ReadKey();
            }
        }
    }
}
