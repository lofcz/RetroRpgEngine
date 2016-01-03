using RetroRPG.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroRPG
{
    // Třída pro vytvoření postavy
    class CharacterCreation
    {

        private static CharacterCreation character;
        private CharacterCreation() { }

        public static CharacterCreation getInstance
        {
            get
            {
                if (character == null)
                {
                    character = new CharacterCreation();
                }

                return character;
            }
        }

        buffer buffer = Render.getInstance.Buffer;

        private enum classes
        {
            barbarian,mage,warrior,monk,thief
        };

        /// <summary>
        /// Shows a list of current classes and force player to pick one.
        /// </summary>
        public void GetPlayerClass()
        {
            bool choosing = true;
            int choosedIndex = 1;
            string[] items = { "Barbar", "Mág", "Válečník", "Mnich", "Zloděj" };
            string[] itemsDescs =
            {

            };

            while(choosing)
            {
                Render.getInstance.Buffer.Clear();
                Console.SetCursorPosition(0, 0);


                buffer.Draw(Strings.getInstance.horizontalLine, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
                buffer.NewLine(2);
                // INTRO HERE
                switch(choosedIndex)
                {
                    case 0:
                        {
                            buffer.DrawColored("+- Barbar -+", Console.CursorLeft + ((Console.WindowWidth / 2) - "+- Barbar +-".ToString().Length / 2) + 5, Console.CursorTop, ConsoleColor.Red, true);
                            Console.CursorTop = 3;
                            Parser.getInstance.parseImage(ResourceTree.graphicsCharCreation + "/barbarian.txt", true, ConsoleColor.Gray, Parser.Effects.none);
                            break;
                        }

                    case 1:
                        {
                            Console.CursorTop = 3;
                            Parser.getInstance.parseImage(ResourceTree.graphicsCharCreation + "/mage.txt", true, ConsoleColor.Gray, Parser.Effects.none);
                            break;
                        }
                    case 2:
                        {
                            Parser.getInstance.parseImage(ResourceTree.graphicsCharCreation + "/warrior.txt", true, ConsoleColor.Gray, Parser.Effects.none);
                            break;
                        }
                    case 3:
                        {
                            Parser.getInstance.parseImage(ResourceTree.graphicsCharCreation + "/priest.txt", true, ConsoleColor.Gray, Parser.Effects.none);
                            break;
                        }
                    case 4:
                        {
                            Parser.getInstance.parseImage(ResourceTree.graphicsCharCreation + "/thief.txt", true, ConsoleColor.Gray, Parser.Effects.none);
                            break;
                        }
                }
                // /INTRO
                buffer.Draw(Strings.getInstance.horizontalLine, Console.CursorLeft, 14, ConsoleColor.Gray);
                Console.CursorTop = 14;
                buffer.NewLine();               
                // CHARACTER STATS
                switch (choosedIndex)
                {
                    case 0:
                        {
                            buffer.DrawColored("Už stovky let podnikají barbaři z #hMlžných hor#x nájezdy na království. Válečný řev, který je\npředchází na míle daleko je postrachem všech vesničanů v okolí. Barbaři jsou divocí válečnící,\nkteří neznají strach a bojiště ovládají zbraněmi děsivých velikostí - od #hnordických seker#x, přes\n#hobouruční meče#x, až po #hobří kyje#x. V boji získávají #rzuřivost#x, díky které mohou provádět mocná komba.\nDíky vysoké konstituci pasivně #rsnižují#x přijaté poškození a mají zvýšenou odolnost proti nemocím.", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, true); 
                            break;
                        }

                    case 1:
                        {
                           break;
                        }
                    case 2:
                        {
                            break;
                        }
                    case 3:
                        {
                            break;
                        }
                    case 4:
                        {
                             break;
                        }
                }
                // /CHARACTER STATS
                buffer.NewLine();
                buffer.Draw(Strings.getInstance.horizontalLine, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
                buffer.NewLine();

                for (int i = 0; i < items.Length; i++)
                {
                    if (choosedIndex != i) { buffer.Draw("> " + items[i]); }
                    else { buffer.Draw(">  " + items[i], ConsoleColor.Yellow); } 

                    buffer.NewLine();
                }

                buffer.Print();
                ConsoleKey key = Console.ReadKey().Key;

                switch(key)
                {
                    case ConsoleKey.W:
                        {
                            if (choosedIndex > 0)
                            {
                                choosedIndex--;
                            }
                            else
                            {
                                choosedIndex = items.Length-1;
                            }

                            break;
                        }

                    case ConsoleKey.S:
                        {
                            if (choosedIndex < items.Length-1)
                            {
                                choosedIndex++;
                            }
                            else
                            {
                                choosedIndex = 0;
                            }

                            break;
                        }
                    case ConsoleKey.Enter:
                        {

                            buffer.Clear();
                            choosing = false;
                            Console.SetCursorPosition(0, 0);
                            GetPlayerClassPartTwo(choosedIndex);
                            break;
                        }
                }
            }
        }

        void GetPlayerClassPartTwo(int index)
        {
            switch(index)
            {
                case (int)classes.barbarian:
                    {

                        break;
                    }
            }
        }
    }
}
