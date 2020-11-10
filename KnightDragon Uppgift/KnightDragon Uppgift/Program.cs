using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KnightDragon_Uppgift
{
    class Program
    {
        static void Main(string[] args)
        {
            Knight knight1 = new Knight("Bilbo Baggins", "Sting");
            knight1.Health = 100;
            knight1.Shield = 5;

            Knight knight2 = new Knight("Thorin Oakenshield", "Orcrist");
            knight2.Health = 150;
            knight2.Shield = 10;
            
            Knight knight3 = new Knight("Bard the Bowman", "The Black Arrow");
            knight3.Health = 120;
            knight3.Shield = 0;

            Dragon dragon = new Dragon("Smaug", "Black");
            dragon.Health = 1000;
            dragon.Shield = 100;

            Random r = new Random();
            int damage = 0;

            
            Console.WriteLine("Bilbo Baggins of Bag's End and Thorin Oakenshield try to attack Smaug the Terrible.\nBard the Bowman is ready with his Crossbow.\nPress enter to continue.");
            Console.ReadLine();
            Thread.Sleep(1000);
            while (knight1.Health > 0 && knight2.Health > 0 && knight3.Health > 0 && dragon.Shield > 10)
            {
                dragon.Attack();
                damage = r.Next(30, 50);

                knight1.Health -= damage - knight1.Shield;
                knight1.Shield -= knight1.Shield - damage < 0 ? 0 : damage;
                knight2.Health -= damage - knight2.Shield;
                knight2.Shield -= knight2.Shield - damage < 0 ? 0 : damage;

                Thread.Sleep(2000);

                knight1.Attack();
                damage = r.Next(1, 10);
                knight2.Attack();
                damage += r.Next(5, 15);

                dragon.Health -= damage - dragon.Shield;
                dragon.Shield -= dragon.Shield - damage < 0 ? 0 : damage;

                Thread.Sleep(1000);

                Console.WriteLine(dragon.GetInfo());
                Console.WriteLine(knight1.GetInfo());
                Console.WriteLine(knight2.GetInfo());
                Console.WriteLine("Press enter to continue to next round.");
                Console.ReadLine();
                Console.Clear();
            }

            knight3.Attack();
            damage = r.Next(900, 2000);

            dragon.Health -= damage - dragon.Shield;
            dragon.Shield -= dragon.Shield - damage < 0 ? 0 : damage;

            Thread.Sleep(2000);

            Console.WriteLine(dragon.GetInfo());
            Console.WriteLine(knight1.GetInfo());
            Console.WriteLine(knight2.GetInfo());
            Console.WriteLine("Press enter to see the end of the story!");
            Console.ReadLine();
            Console.Clear();

            if (dragon.Health < 0)
                Console.WriteLine("Smaug is defeated and Laketown and Erebor are safe!");
            else
                Console.WriteLine("Smaug survived and its greed swallowed Laketown and Erebor.");
            Console.ReadKey();
        }
    }
}
