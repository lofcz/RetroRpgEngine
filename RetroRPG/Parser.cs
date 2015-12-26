using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RetroRPG.Objects;
using System.Threading;

namespace RetroRPG
{
    class Parser
    {
        private static Parser mapParser;
        private Parser() { }

        public enum Effects
        {
            none,slide,typewriter
        }

        public static Parser getInstance
        {
            get
            {
                if (mapParser == null)
                {
                    mapParser = new Parser();
                }

                return mapParser;
            }
        }

        public void ParseMap()
        {
            string line = "";
            int x = 0, y = 0;
            List<String> Comments = new List<string>();

            StreamReader sr = new StreamReader("map.retroRpgMap");
            string[] metaTags = { "#MapName:", "#MapAuthor:", "#MapVersion:" };
            while ((line = sr.ReadLine()) != null)
            {
                bool cont = true;

                if (line.StartsWith("//", StringComparison.Ordinal) )
                {
                    Comments.Add(line);
                    cont = false;
                }

                if (cont)
                {
                    foreach (char znak in line)
                    {
                        switch (znak)
                        {
                            case ('#'):
                                {
                                    oWall.addWall(x, y);
                                    break;
                                }
                            case ('◎'):
                                {
                                    oGold.addGold(x, y);
                                    break;
                                }
                        }

                        x++;
                    }
                }
                y++;
                x = 0;

               
            }
         
            if (Comments.Count > 0)
            {
                Render.getInstance.Buffer.Clear();

                foreach(string ln in Comments)
                {
                    string str = ln;
                    str = str.Replace("//", "");
                    bool cont = true;

                    for (int i = 0; i < metaTags.Length; i++)
                    {
                        if (str.Contains(metaTags[i]))
                        {
                            str = str.Replace(metaTags[i] + " ", "");
                            Render.getInstance.Buffer.Draw(metaTags[i], Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
                            Render.getInstance.Buffer.Draw(str, Console.CursorLeft, Console.CursorTop, ConsoleColor.Green);
                            cont = false;
                            break;
                        }
                    }
               
                    if (cont)
                    {
                        Render.getInstance.Buffer.Draw(str, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
                    }

                    Render.getInstance.Buffer.NewLine();              
                }

                Render.getInstance.Buffer.Print();
                Console.ReadKey(true);
                Render.getInstance.Buffer.Clear();
            }
            sr.Close();
        }

        public void parseImage(string file, bool center, ConsoleColor color, Effects effect)
        {
            string line;
            StreamReader sr = new StreamReader(file);

            Console.CursorVisible = false;

            while ((line = sr.ReadLine()) != null)
            {
                if (effect == Effects.typewriter)
                {
              
                    Render.getInstance.Buffer.DrawInsert(line, Console.CursorLeft, Console.CursorTop, color);         
                    Render.getInstance.Buffer.NewLine();
                    Render.getInstance.Buffer.Print();
                    Thread.Sleep(80);
                }
                else
                {
                    Render.getInstance.Buffer.DrawInsert(line, Console.CursorLeft + (Console.WindowWidth / 5), Console.CursorTop, color);
                    Render.getInstance.Buffer.NewLine();
                }
            }

            Console.CursorVisible = true;
            Render.getInstance.Buffer.Print();
            sr.Close();

        }
    }
}
