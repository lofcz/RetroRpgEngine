using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroRpgAnimationDesigner
{
    class Designer
    {
        List<List<string>> frameList = new List<List<string>>();
        int frameNumber = 0;
        List<string> currentFrame = new List<string>();
        FileStream ostrm = new FileStream("./Redirect.txt", FileMode.OpenOrCreate, FileAccess.Write);
        StreamWriter writer;
        TextWriter oldOut = Console.Out;

        public void runDesigner()
        {
            while (true)
            {
                input();
                drawInfo();

            }
        }

        void input()
        {
            writer = new StreamWriter(ostrm);
            Console.SetOut(writer);

            Console.SetOut(oldOut);
            Console.WriteLine("TEEEST");

            ConsoleKeyInfo k = Console.ReadKey(true);
            ConsoleKey key = k.Key;

            if (key == ConsoleKey.UpArrow)
            {
                if (Console.CursorTop > 0) { Console.CursorTop--; }               
            }
            else if (key == ConsoleKey.DownArrow)
            {
                if (Console.CursorTop < 20) { Console.CursorTop++; }
            }
            else if (key == ConsoleKey.RightArrow)
            {
                if (Console.CursorLeft < 50) { Console.CursorLeft++; }
            }
            else if (key == ConsoleKey.LeftArrow)
            {
                if (Console.CursorLeft > 0) { Console.CursorLeft--; }
            }
            else
            {
                string cKey = key.ToString().ToLower();
                if (k.Modifiers == ConsoleModifiers.Shift) { cKey = cKey.ToUpper(); }

                if (key == ConsoleKey.Enter)
                        {
                         cKey = "";
                         newFrame();
                        }
                if (key == ConsoleKey.Backspace) { cKey = " "; Console.CursorLeft--; }


                Console.Write(cKey);
                if (key == ConsoleKey.Backspace) { Console.CursorLeft--; }
                
           }

            writer.Close();
            ostrm.Close();

        }

        void drawInfo()
        {
            int xx = Console.CursorLeft;
            int yy = Console.CursorTop;

            Console.SetCursorPosition(0, 15);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("ENTER");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" - Přidat snímek.");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("CTRL + ArrowUp / ArrowDown");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" - Procházet mezi snímky.");
            Console.WriteLine();


            Console.SetCursorPosition(xx, yy);
        }

        void newFrame()
        {          
            try
            {
                ostrm = new FileStream("./Redirect.txt", FileMode.OpenOrCreate, FileAccess.Write);
                writer = new StreamWriter(ostrm);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            Console.SetOut(writer);
            Console.WriteLine("This is a line of text");
            writer.Close();
            ostrm.Close();

            Console.SetOut(oldOut);
            Console.WriteLine("TEEEST");
        }

    }
}
