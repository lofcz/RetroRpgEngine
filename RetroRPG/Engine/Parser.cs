using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RetroRPG.GameObjects;
using System.Threading;
using System.Xml;
using System.Windows.Forms;

namespace RetroRPG
{
    class Parser
    {
        private static Parser mapParser;
        private Parser() { }
        Random random = new Random();

        public enum Effects
        {
            none,slide,typewriter,numbersToText
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
            string dataLine = "";
            int x = 0, y = 0;
            List<String> Comments = new List<string>();

            StreamReader sr = new StreamReader("map.retroRpgMap");
            string[] metaTags = { "#MapName:", "#MapAuthor:", "#MapVersion:" };
            
            
            while ((line = sr.ReadLine()) != null)
            {
                bool cont = true;

                if (line == "") { cont = false; }
                if (line.StartsWith("//", StringComparison.Ordinal) )
                {
                    Comments.Add(line);
                    cont = false;
                }

                if (cont)
                {
                    if (line.Contains("[oWall]"))
                    {
                        Structs.CSVList constructor = getCsv(line, "[oWall]");
                        bool isSecret = false;
                        int secretX = x;
                        int secretY = y;

                        for (int i = 0; i < constructor.CSVText.Count; i++)
                        {
                            string value = constructor.CSVText[i];

                            if (value == "isSecret")
                            {
                                if (constructor.CSVValue[i] == "true")
                                {
                                    isSecret = true;
                                }
                                else
                                {
                                    isSecret = false;
                                }

                                continue;
                            }

                            if (value == "secretX")
                            {
                                string l = constructor.CSVValue[i];
                                int xPos = 0;
                                bool parsing = false;
                                string parsingText = "";
                                int ii = 0;

                                foreach(char znak in l)
                                {
                                    if (znak == 'x') { xPos += x; }
                                    else if (znak == '+' || znak == '|')
                                        {
                                        if (parsing == false)
                                            {

                                            }
                                        else
                                        {
                                            if (parsingText != "")
                                            {
                                                xPos += Convert.ToInt32(parsingText);
                                                parsingText = "";
                                            }
                                        }

                                        parsing = !parsing;
                                    }
                                else
                                    {
                                        if (znak != ' ') { parsingText += znak; }
                                    }

                                    ii++;

                                    if (ii == l.Count())
                                    {
                                        secretX = xPos;
                                    }
                                }

                            }

                        }

                        Structs.Point point = new Structs.Point();
                        point.x = secretX;
                        point.y = secretY;

                        oWall.addWall(x, y, isSecret, point);
                    }
                    else if (line.Contains("[oGold]"))
                    {
                        Structs.CSVList constructor = getCsv(line, "[oGold]");
                        int coinValue = 5;

                        for (int i = 0; i < constructor.CSVText.Count; i++)
                        {
                            string value = constructor.CSVText[i];

                            if (value == "value")
                            {
                                coinValue = Convert.ToInt32(constructor.CSVValue[i]);
                            }
                        }

                        oGold.addGold(x, y, coinValue);
                    }
                    x++;
                }
                if (line == "#NewLine") { y++; x = 0; }       
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

        Structs.CSVList getCsv(string line, string entityName)
        {
            string str = line;
            List<string> csvDeserializedText = new List<string>();
            List<string> csvDeserializedValue = new List<string>();

            line = line.Replace(entityName, "");
            string currentValueText = "";
            string currentValueValue = "";
            bool parsingValueText = true;
            int i = 0;


            foreach (char znak in line)
            {
                if (znak != ' ' && znak != '[' && znak != ']' && znak != '=' && znak != ';' && znak != '×')
                {
                    if (parsingValueText)
                    {
                        currentValueText += znak;
                    }
                    else
                    {
                        currentValueValue += znak;
                    }
                }
                if (znak == '=')
                {
                    parsingValueText = !parsingValueText;
                }
                if (znak == ';')
                {
                    csvDeserializedText.Add(currentValueText);
                    csvDeserializedValue.Add(currentValueValue);
                    currentValueText = "";
                    currentValueValue = "";
                }
                if (znak == '×')
                {
                    csvDeserializedText.Add("void");
                    csvDeserializedValue.Add("void");
                    currentValueText = "";
                    currentValueValue = "";
                }

            }

            return new Structs.CSVList(csvDeserializedText, csvDeserializedValue);
        }

        public void parseImage(string file, bool center, ConsoleColor color, Effects effect)
        {
            string line;
            StreamReader sr = new StreamReader(file);
            string[] fileLines = new string[File.ReadLines(file).Count()];
            int fileLinesInt = File.ReadLines(file).Count();
            Console.CursorVisible = false;

            if (effect != Effects.numbersToText)
            {
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
                        if (center)
                        {
                            Render.getInstance.Buffer.DrawColored(line, Console.CursorLeft + (Console.WindowWidth / 5) + 5, Console.CursorTop, color, true); // + (Console.WindowWidth / 5) + 5 //+ ((Console.WindowWidth / 2) - (line.Length / 2))
                            Render.getInstance.Buffer.NewLine();
                        }
                        else
                        {
                            Render.getInstance.Buffer.DrawColored(line, Console.CursorLeft, Console.CursorTop, color, true);
                            Render.getInstance.Buffer.NewLine();
                        }
                    }
                }
            }
            else
            {
                int originalLine = Console.CursorTop;
                int currentLine = 0;

                while ((line = sr.ReadLine()) != null)
                {
                    fileLines[currentLine] = line;
                    currentLine++;    
                }

                for (int i = 0; i <= 20; i++)
                {
                    Console.SetCursorPosition(0, originalLine);

                    for (int j = 0; j < fileLinesInt; j++)
                    {
                        foreach(char znak in fileLines[j])
                        {
                            char randomChar = (char)random.Next(32, 128);
                            if ((Math.Abs((int)randomChar - (int)znak) < i * 5) || (i == 20)) { randomChar = znak; }
                            if (znak == ' ') {randomChar = ' ';}

                            Render.getInstance.Buffer.Draw(Convert.ToString(randomChar), Console.CursorLeft, Console.CursorTop, color);                       
                        }

                       Render.getInstance.Buffer.NewLine();
                       Render.getInstance.Buffer.Print();
                    }

                    Thread.Sleep(100);
                }

              
            }

            Console.CursorVisible = true;
            Render.getInstance.Buffer.Print();
            sr.Close();

        }

        public void parseAnimatedImage(string file, int animationSpeed)
        {
            Console.CursorVisible = false;

            buffer buffer = Render.getInstance.Buffer;

            List<string> currentFrameList = new List<string>();
            List<List<string>> frameList = new List<List<string>>();

            string line;
            StreamReader sr = new StreamReader(file);

            // Parsování
            while ((line = sr.ReadLine()) != null)
            {
                if (line != "#NewFrame" && line != "#EndAnimation")
                {
                    currentFrameList.Add(line);
                }
                else
                {
                    frameList.Add(currentFrameList.ToList());
                    currentFrameList.Clear();
                }
            }

            int frameNumber = frameList.Count;

            // Vykreslení
            int minY, maxY = 0,i = 0;

            minY = Console.CursorTop;
            maxY = minY;

            while(frameNumber > 0)
            {
                List<string> frame = frameList[i];
                      
                    foreach (string ln in frame)
                    {
                       //buffer.Draw(ln);
                       // buffer.NewLine();

                    buffer.DrawColored(ln, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);

                    if (Console.CursorTop > maxY) { maxY = Console.CursorTop; }                    
                    }

                    buffer.Print();


               

                Thread.Sleep(animationSpeed);

                for (int j = 0; j < frame.Count; j++)
                {
                     //buffer.clearColumn(minY + j);
                    buffer.Clear();
                }

                Console.CursorTop = minY;
                frameNumber--;
                i++;
                Console.CursorLeft = 0;
                Console.CursorTop = minY;
            }

            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.ReadKey();
        }
    }
}
