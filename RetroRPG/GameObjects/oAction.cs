using RetroRPG.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroRPG.GameObjects
{
    // Třída pro aktivační pole
    class oAction
    {
        public bool playerTouched = false;
        public int x;
        public int y;


        List<string> parameters = new List<string>();

        public oAction(int x, int y, List<string> parameters)
        {
            this.x = x;
            this.y = y;
            this.parameters = parameters;
        }
        enum funkce
        {
            LoglostAdd
        }

        public void OnHitPlayer()
        {
            if (!playerTouched)
            {
                playerTouched = true;

                // Parsování
                bool parsujuFunkci = false;
                int parsujuText = 0;
                funkce parsovanaFunkce = funkce.LoglostAdd;
              

                foreach (string ln in parameters)
                {
                    string line = ln;

                    // Pokud neparsuju FCI
                    if (!parsujuFunkci)
                    {
                        if (line.StartsWith("logListAdd"))
                        {
                            line = line.Replace("logListAdd", "");
                            parsujuFunkci = true;
                            parsovanaFunkce = funkce.LoglostAdd;

                            // odstraním začátek závorky
                            int i = 0;
                            while(line[i] != '(')
                            {
                                i++;
                            }
                            line = line.Remove(i, 1);
                        }
                    }

                     if (parsovanaFunkce == funkce.LoglostAdd)
                        {

                        LogItem.LogPrefix prefix = LogItem.LogPrefix.standard; 
                        string textFunkce = "";
                        int i = 0;

                        foreach(char znak in line)
                        {
                            if (znak == '"')
                            {
                                parsujuText++;
                                if (parsujuText == 2) { parsujuText = 0;  break; } // Doparsuju text
                            }
                            else
                            {
                                textFunkce += znak;
                            }

                            i++;
                        }

                        int substringStart = 0;

                       for(int j = 0; j < line.Length; j++)
                        {
                            if (line[j] == ',' && j > i)
                            {
                                substringStart = j;
                            }
                        }

                        line = line.Substring(substringStart + 1, line.Length - substringStart - 1);
                        string parametr = "";

                        foreach (char znak in line)
                        {
                            if (znak != ')')
                            {
                                parametr += znak;
                            }
                        }

                        if (parametr == "normalLog")
                        {
                            prefix = LogItem.LogPrefix.standard;
                        }
                        if (parametr == "achievmentLog")
                        {
                            prefix = LogItem.LogPrefix.achievment;
                        }

                        LogItem logItem = new LogItem(prefix, textFunkce, 60, ConsoleColor.Gray);
                        Render.getInstance.AddLog(logItem);

                        }
                }
            }

        }

        public static void addAction(int x, int y, List<string> parameters)
        {
            oAction action = new oAction(x, y, parameters);
            GameWorld.getInstance.actionList.Add(action);
        }
    }
}
