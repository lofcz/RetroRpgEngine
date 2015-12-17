using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RetroRPG
{
    // Třída pro vykreslení úvodu hry
    public class Intro
    {
        private static Intro intro;
        private Intro() { }

        public static Intro getInstance
        {
            get
            {
                if (intro == null)
                {
                    intro = new Intro();
                }

                return intro;
            }
        }

        public void DisplayIntro()
        {
            Console.CursorVisible = false;
   
            for (int i = 0; i < 100; i++)
            {
                Render.getInstance.Buffer.Clear();
                Render.getInstance.Buffer.DrawInsert("Načítám", 18, 8, ConsoleColor.Gray);
                Render.getInstance.Buffer.DrawInsert(i + "%", 20,10, ConsoleColor.Green);

                Render.getInstance.Buffer.Print();
                Thread.Sleep(5);
            }

            Console.CursorVisible = true;
  
        }
    }
}
