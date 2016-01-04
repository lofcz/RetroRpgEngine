using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroRpgPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 400; i++)
            {
                // Get ascii character.
                char c = (char)i;
                Console.Write("\n");
            }

            Console.ReadKey();
        }
    }
}