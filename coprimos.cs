using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCD
{
    class Program
    {
        /// <summary>
        /// Euclid's theorem
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        static int GCD (int a, int b)
        {
            int aux = 0;
            if(a < b)
            {
                aux = a;
                a = b;
                b = aux;
            }

            while (b != 0)
            {
                aux = a % b;
                a = b;
                b = aux;
            }

            return a;
        }

        static void Main(string[] args)
        {
            int a, b, result = 0;
            a = Convert.ToInt32(Console.ReadLine());
            b = Convert.ToInt32(Console.ReadLine());

            result = GCD(a, b);

            if(result == 1 || result == -1)
            {
                Console.WriteLine($"Son números coprimos.");
            }
            else
            {
                Console.WriteLine($"No son números coprimos.");
            }
            Console.ReadLine();
        }
    }
}
