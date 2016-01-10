using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Učím se C# na Svobodném fóru (.eu)";

            string slovo = "Text";

            foreach (char znak in slovo)
            {
                Console.WriteLine(znak);
            }

            Console.ReadKey();

        }
    }
}
