using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RetroRPG
{
    // Hlavní menu aplikace
    class MainMenu
    {
        private static MainMenu menu;
        private MainMenu() { }
        buffer buffer = Render.getInstance.Buffer;

        public static MainMenu getInstance
        {
            get
            {
                if (menu == null)
                {
                    menu = new MainMenu();
                }

                return menu;
            }
        }

        enum ManuItemsEnum
        {
            Pokracovat,NovyPribeh,UzivatelskeMody,Autori,KonecHry
        };

        public void showMainMenu()
        {
            Console.CursorVisible = false;

            string[] menuItems = { "Pokračovat", "Začít nový příběh", "Rozšíření", "Autoři", "Konec hry" };
            bool[] menuItemsEnabled = { false, true, true, true, true };
            bool selecting = true;
            int selectedItem = 1;

            if (File.Exists("savegame.sav")) { menuItemsEnabled[(int)ManuItemsEnum.Pokracovat] = true; }


            // Vykreslíme splashscreen
            Parser.getInstance.parseImage("castle.txt", false, ConsoleColor.Gray, Parser.Effects.none);
            Console.CursorTop += 2;

            int y = Console.CursorTop;

            while (selecting)
            {
                //Console.SetCursorPosition(0, 20);

                // vykreslíme položky
                for (int i = 0; i < menuItems.Length; i++)
                {
                    ConsoleColor color = ConsoleColor.Gray;

                    if (selectedItem == i)
                    {
                        color = ConsoleColor.Yellow;
                        buffer.DrawColored("> " + menuItems[i], Console.CursorLeft, Console.CursorTop, color, false, true);
                    }
                    else
                    {
                        if (menuItemsEnabled[i] == false)
                        {
                            color = ConsoleColor.DarkGray;
                            buffer.DrawColored(menuItems[i], Console.CursorLeft, Console.CursorTop, color, false, true);
                        }
                        else
                        {
                            buffer.DrawColored(menuItems[i], Console.CursorLeft, Console.CursorTop, color, false, true);
                        }
                    }

                    //buffer.DrawColored(menuItems[i], Console.CursorLeft, Console.CursorTop, color, false, true);
                }

                buffer.Print();

                ConsoleKey key = Console.ReadKey(true).Key;

                switch(key)
                {
                    case ConsoleKey.Enter:
                        {
                            selecting = false;
                            break;
                        }
                    case ConsoleKey.S:
                        {
                            if (selectedItem < menuItems.Length - 1)
                            {
                                selectedItem++;
                                int j = selectedItem; while (menuItemsEnabled[j] == false) { j++; selectedItem++; }
                            }
                            else { selectedItem = 0; int j = 0; while (menuItemsEnabled[j] == false) { j++; selectedItem++; } }

                            break;
                        }
                    case ConsoleKey.W:
                        {
                            if (selectedItem > 0)
                            {
                                selectedItem--;
                                int j = selectedItem; while (menuItemsEnabled[j] == false) { if (j > 0) { j--; selectedItem--; } else { selectedItem = menuItems.Length-1; j = menuItems.Length-1; break; } }
                            }
                            else { selectedItem = menuItems.Length; int j = menuItems.Length; while (menuItemsEnabled[j] == false) { j--; selectedItem--; } }

                            break;
                        }
                }

                // Vyčistíme výběr
                for (int i = 0; i < menuItems.Length; i++)
                {
                    buffer.clearRow(y + i);
                }

                Console.SetCursorPosition(0, y);
            }

            buffer.Clear();
            buffer.Print();
            Console.CursorVisible = true;
        }
    }
}
