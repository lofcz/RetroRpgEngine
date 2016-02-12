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
            string[] menuItems = { "Pokračovat", "Začít nový příběh", "Uživatelské módy", "Autoři", "Konec hry" };
            bool[] menuItemsEnabled = { false, true, true, true, true };
            bool selecting = true;
            int selectedItem = 1;

            if (File.Exists("savegame.sav")) { menuItemsEnabled[(int)ManuItemsEnum.Pokracovat] = true; }


            // Vykreslíme splashscreen
            Parser.getInstance.parseImage("castle.txt", false, ConsoleColor.Gray, Parser.Effects.none);
            buffer.Print();

            while (selecting)
            {
                ConsoleKey key = Console.ReadKey(true).Key;

                switch(key)
                {
                    case ConsoleKey.Enter:
                        {
                            selecting = false;
                            break;
                        }
                }
            }

            buffer.Clear();
            buffer.Print();
        }
    }
}
