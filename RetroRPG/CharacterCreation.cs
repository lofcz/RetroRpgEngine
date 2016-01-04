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

        public enum classes
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
                         //   Parser.getInstance.parseImage(ResourceTree.graphicsCharCreation + "/barbarian.txt", true, ConsoleColor.Gray, Parser.Effects.none);
                            break;
                        }

                    case 1:
                        {
                            Console.CursorTop = 3;
                     //       Parser.getInstance.parseImage(ResourceTree.graphicsCharCreation + "/mage.txt", true, ConsoleColor.Gray, Parser.Effects.none);
                            break;
                        }
                    case 2:
                        {
                //            Parser.getInstance.parseImage(ResourceTree.graphicsCharCreation + "/warrior.txt", true, ConsoleColor.Gray, Parser.Effects.none);
                            break;
                        }
                    case 3:
                        {
                     //       Parser.getInstance.parseImage(ResourceTree.graphicsCharCreation + "/priest.txt", true, ConsoleColor.Gray, Parser.Effects.none);
                            break;
                        }
                    case 4:
                        {
                //            Parser.getInstance.parseImage(ResourceTree.graphicsCharCreation + "/thief.txt", true, ConsoleColor.Gray, Parser.Effects.none);
                            break;
                        }
                }
                // /INTRO
                Console.CursorLeft = 0;
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
                            buffer.Print();
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
                        buffer.Draw(Strings.getInstance.horizontalLine);
                        buffer.NewLine();
                        buffer.DrawColored("+- Barbar -+", Console.CursorLeft + ((Console.WindowWidth / 2) - "+- Barbar +-".ToString().Length / 2), Console.CursorTop, ConsoleColor.Red, true);

                        buffer.NewLine();
                        buffer.Draw(Strings.getInstance.horizontalLine);
                        buffer.NewLine();
                        // Základní vlastnosti
                        buffer.DrawColored("[Vlastnosti]", Console.CursorLeft, Console.CursorTop, ConsoleColor.Yellow, false, true);
                        buffer.NewLine();
                        buffer.DrawColored("Síla:         #y12#x (#g+++#x)", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
                        buffer.DrawColored("Konstituce:   #y10#x (#g++#x)", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
                        buffer.DrawColored("Obratnost:    #y6#x  (#r--#x)", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
                        buffer.DrawColored("Odolnost:     #y8#x ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
                        buffer.DrawColored("Inteligence:  #y4#x  (#r---#x)", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
                        buffer.DrawColored("Charisma:     #y4#x  (#r---#x)", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
                        buffer.DrawColored("Vůle:         #y6#x  (#r--#x)", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
                        buffer.DrawColored("Zručnost:     #y5#x  (#r---#x)", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
                        buffer.DrawColored("Postřeh:      #y7#x ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
                        buffer.DrawColored("Štěstí:       #y6#x  (#r-#x)", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
                        // Dovednosti
                        Console.CursorTop = 3;

                        buffer.DrawColored("[Dovednosti]", 30, Console.CursorTop, ConsoleColor.Yellow, false, true);
                        buffer.NewLine();
                        buffer.DrawColored("Háčkování zámků:    #y4#x (#r---#x)", 30, Console.CursorTop, ConsoleColor.Gray, false, true);
                        buffer.DrawColored("Plížení:            #y5#x (#r---#x)", 30, Console.CursorTop, ConsoleColor.Gray, false, true);
                        buffer.DrawColored("Přesvědčování:      #y8#x ", 30, Console.CursorTop, ConsoleColor.Gray, false, true);
                        buffer.DrawColored("Zastrašování:       #y11#x (#g+++#x)", 30, Console.CursorTop, ConsoleColor.Gray, false, true);
                        buffer.DrawColored("Runová magie:       #y4#x (#r---#x)", 30, Console.CursorTop, ConsoleColor.Gray, false, true);
                        buffer.DrawColored("Elementární magie:  #y4#x (#r---#x)", 30, Console.CursorTop, ConsoleColor.Gray, false, true);
                        buffer.DrawColored("Zaříkávání:         #y4#x (#r---#x)", 30, Console.CursorTop, ConsoleColor.Gray, false, true);
                        buffer.DrawColored("Smlouvání:          #y7#x ", 30, Console.CursorTop, ConsoleColor.Gray, false, true);
                        buffer.DrawColored("Zápal:              #y11#x (#g+++#x)", 30, Console.CursorTop, ConsoleColor.Gray, false, true);
                        buffer.DrawColored("Víra :              #y8#x ", 30, Console.CursorTop, ConsoleColor.Gray, false, true);
                        // Specializace
                        Console.CursorTop = 3;

                        buffer.DrawColored("[Specializace]", 63, Console.CursorTop, ConsoleColor.Yellow, false, true);
                        buffer.NewLine();
                        buffer.DrawColored("Reflexe poškození:  #y+5%#x  - level 5", 63, Console.CursorTop, ConsoleColor.Gray, false, true);
                        buffer.DrawColored("Ochrana vůči jedu:  #y+10%#x - level 15", 63, Console.CursorTop, ConsoleColor.Gray, false, true);
                        buffer.DrawColored("Prodloužené kombo:  #y+1#x   - level 30", 63, Console.CursorTop, ConsoleColor.Gray, false, true);
                        buffer.DrawColored("Ochrana vůči ohni:  #y+10%#x - level 50", 63, Console.CursorTop, ConsoleColor.Gray, false, true);

                        Console.CursorTop = 16;


                        buffer.DrawColored("[Bonusy povolání]", Console.CursorLeft, Console.CursorTop, ConsoleColor.Yellow, false, true);
                        buffer.NewLine();
                        buffer.DrawColored("Nemoci trvají o #gpolovinu#x kol méně.", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
                        buffer.DrawColored("Úspěšně dokončené kombo má #g5%#x šanci na aplikování efektu #hzlomenina#x.", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
                        buffer.DrawColored("Útoky generují #rzuřivost#x, kterou můžeš uvolnit do mocného komba.", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
                        buffer.DrawColored("Omráčení protivníka má #g25%#x šanci na prodloužení svého efektu o #gdalší kolo#x.", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);

                        break;
                    }
            }
            buffer.Print();
            buffer.NewLine();
            Console.CursorLeft = 0;
            int top = Console.CursorTop;
            bool choosing = true;
            int choosed = 0;
            string[] items = { "Zpět k výběru povolání", "Potvrdit výběr" };

            while(choosing)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (i != choosed) {buffer.DrawColored("> " + items[i], Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true); }
                    else
                    {
                        buffer.DrawColored(" #g> #x" + items[i], Console.CursorLeft, Console.CursorTop, ConsoleColor.Yellow, false, true);
                    }
                }

                buffer.Print();
                ConsoleKey key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.W:
                        {
                            if (choosed > 0)
                            {
                                choosed--;
                            }
                            else
                            {
                                choosed = items.Length - 1;
                            }
                            break;
                        }

                    case ConsoleKey.S:
                        {
                            if (choosed < items.Length - 1)
                            {
                                choosed++;
                            }
                            else
                            {
                                choosed = 0;
                            }
                            break;
                        }

                    case ConsoleKey.Enter:
                        {
                           switch(choosed)
                            {
                                case 0:
                                    {
                                        choosing = false;
                                        buffer.Clear();
                                        Console.SetCursorPosition(0, 0);
                                        GetPlayerClass();
                                        break;
                                    }

                                case 1:
                                    {
                                        choosing = false;
                                        buffer.Clear();
                                        Console.SetCursorPosition(0, 0);
                                        GameWorld.getInstance.player.gameClass = (classes)index;
                                        oPlayer player = GameWorld.getInstance.player;

                                        switch(index)
                                        {
                                            case (int)classes.barbarian:
                                                {
                                                    player.Vlastnosti[(int)oPlayer.vlastnosti.sila] = 12;
                                                    break;
                                                }
                                        }
                                        increasePlayerStats(5,5);
                                        break;
                                    }
                            }

                            break;
                        }
                }

                for (int i = 0; i < items.Length; i++)
                {
                    buffer.clearRow(top + i);
                }

                Console.CursorTop = top;
                Console.CursorLeft = 0;
            }

         
        }

        void increasePlayerStats(int vlastnostiBody, int dovednostiBody)
        {

        }
    }
}
