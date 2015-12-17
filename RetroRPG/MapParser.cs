using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RetroRPG.Objects;

namespace RetroRPG
{
    class Parser
    {
        private static Parser mapParser;
        private Parser() { }

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

            while ((line = sr.ReadLine()) != null)
            {
                bool cont = true;

                if (line.StartsWith("//"))
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
                    Render.getInstance.Buffer.Draw(str, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
                    Render.getInstance.Buffer.NewLine();              
                }

                Render.getInstance.Buffer.Print();
                Console.ReadKey(true);
                Render.getInstance.Buffer.Clear();
            }
            sr.Close();
        }

        public void parseImage(string file, bool center)
        {
            List<string> lineList = new List<string>();
            
        }
    }
}
