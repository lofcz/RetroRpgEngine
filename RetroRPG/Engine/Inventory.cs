using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroRPG.GameObjects;

namespace RetroRPG
{

    /// <summary>
    /// Třída herního inventáře.
    /// </summary>
    class Inventory
    {
        #region Informace
        // Verze: 1.0 -> 1.1
        // Stabilní: Ne
        #endregion
        #region UML
        // Singleton

        private static Inventory inventory;

        Inventory()
        {
    
        }

        public static Inventory getInstance
        {
            get
            {
                if (inventory == null)
                {
                    inventory = new Inventory();
                }

                return inventory;
            }
        }
        #endregion

        List<GameItem> items = new List<GameItem>();
        List<GameItem> filteredList = new List<GameItem>();

        int itemMin = 0, itemMax;
        int itemSelected = 1;
        int actualItem = 0;
        showFilter filter = showFilter.all;

        enum showFilter
        {
            all,starred,junk
        }

        public void drawInventory(int drawItemsCount)
        {
            int showItemsCount = drawItemsCount;

            bool choosing = true;
            int length = 0;
            int horizontalIndex = 0; // Update inventáře v 1.2

            string tempOutput = ""; // Refactoring inventáře v1.1 - Výsledek vykreslení sjednotíme do stringu

            while (choosing)
            {
                if (filter == showFilter.starred)
                {
                   filteredList = items.Where( itm => itm.starMarked == 1).ToList();
                }
                else
                {
                    filteredList = items;
                }

                Render.getInstance.Buffer.Clear(true);

                itemMax = Math.Min(itemMin + showItemsCount, filteredList.Count);
                actualItem = itemMin;

                GameItem choosingItem = null;              
                string[] equipedItems = {"","",""};
                ConsoleColor[] equipedItemsColors = {ConsoleColor.Gray,ConsoleColor.Gray,ConsoleColor.Gray};

                foreach (GameItem item in items)
                {
                    if (item.attributes[(int)GameItem.atr.equiped] == 1)
                    {
                        equipedItems[item.attributes[(int)GameItem.atr.type]] = item.itemName;
                        equipedItemsColors[item.attributes[(int)GameItem.atr.type]] = item.itemColor;
                    }

                }

                Render.getInstance.Buffer.Draw("Inventář", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
                Render.getInstance.Buffer.NewLine();
                Render.getInstance.Buffer.Draw(Strings.getInstance.horizontalLine, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
                Render.getInstance.Buffer.NewLine();
                Render.getInstance.Buffer.Draw("Používané věci", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
                Render.getInstance.Buffer.NewLine();
                Render.getInstance.Buffer.Draw(" • Zbraň: ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);

                if (equipedItems[(int)oPlayer.ItemsEquiped.Weapon] == "")
                {
                    Render.getInstance.Buffer.Draw("Holé ruce", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
                }
                else
                {
                    Render.getInstance.Buffer.DrawColored(equipedItems[(int)oPlayer.ItemsEquiped.Weapon], Console.CursorLeft, Console.CursorTop, equipedItemsColors[(int)oPlayer.ItemsEquiped.Weapon], true);
                }
                Render.getInstance.Buffer.NewLine();

                Render.getInstance.Buffer.Draw(" • Brnění: ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);

                if (equipedItems[(int)oPlayer.ItemsEquiped.Armor] == "")
                {
                    Render.getInstance.Buffer.Draw("Nic", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
                }
                else
                {
                    Render.getInstance.Buffer.Draw(equipedItems[(int)oPlayer.ItemsEquiped.Armor], Console.CursorLeft, Console.CursorTop, equipedItemsColors[(int)oPlayer.ItemsEquiped.Armor]);
                }
                Render.getInstance.Buffer.NewLine();

                int itemsCountFiltered = 0;

                for (int i = 0; i < items.Count; i++)
                {
                    GameItem item = items[i];

                    if (filter == showFilter.starred)
                    {
                        if (item.starMarked == 1)
                        {
                            itemsCountFiltered++;
                        }
                    }
                    else
                    {
                        itemsCountFiltered++;
                    }
                }

                Render.getInstance.Buffer.Draw(Strings.getInstance.horizontalLine, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
                Render.getInstance.Buffer.NewLine(2);
                Render.getInstance.Buffer.DrawColored("Předmět " + itemSelected.ToString() + " / " + itemsCountFiltered, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, true, true);


                for (int i = itemMin; i < itemMax; i++ )
                {
                    actualItem++;
                    GameItem item = filteredList[i];

                    // Filtrování
                    if (filter == showFilter.starred)
                    {
                        if (item.starMarked != 1) { continue; }
                    }

                    if (itemSelected == actualItem) { choosingItem = item; }

                    if (item.attributes[(int)GameItem.atr.equiped] == 1)
                    {
                        Render.getInstance.Buffer.Draw("[Nasazeno]", Console.CursorLeft, Console.CursorTop, ConsoleColor.Green);
                       // tempOutput += "#g[Nasazeno]#x";
                    }

                    // Vykreslí info o hover předmětu
                    if (itemSelected == actualItem)
                    {
                        Render.getInstance.Buffer.Draw(" > ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Green);
                        //tempOutput += "#g > #x";
                        item.drawItemName();

                        int returnY = Console.CursorTop;

                        Console.SetCursorPosition(0, 20);
                        Render.getInstance.Buffer.Draw(Strings.getInstance.horizontalLine);
                        Render.getInstance.Buffer.NewLine();

                        item.drawItemStats();
                        item.drawItemOptions(horizontalIndex);
                        Render.getInstance.Buffer.NewLine();
                        item.drawItemDescription();
                        Console.SetCursorPosition(0, returnY);
                    }
                    else
                    {
                        Render.getInstance.Buffer.Draw("> ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
                        //item.drawItemStats();
                        item.drawItemName();
                    }

                    Render.getInstance.Buffer.NewLine();

                }

                // Vykreslí možnost "zpět"
                if (itemSelected == itemMax + 1)
                {                

                    Render.getInstance.Buffer.Draw(" > ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Green);

                }    
                else
                {
                    Render.getInstance.Buffer.Draw("> ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
                }

                Render.getInstance.Buffer.Draw("Zpět", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
                Render.getInstance.Buffer.NewLine();
                // **********

                Render.getInstance.Buffer.Print();


                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.W)
                {
                    if (itemMin > 0 && (filteredList.Count - itemSelected > 5)) 
                    {
                        itemMin--;
                    }

                    if (itemSelected > 1) { itemSelected--; }
                    else { itemSelected = filteredList.Count + 1; itemMin = Math.Max(0,filteredList.Count - showItemsCount); }
                    horizontalIndex = 0;
                }
                if (key == ConsoleKey.S)
                {
                    if (itemMin < filteredList.Count - showItemsCount && itemSelected > 5)
                    {
                        itemMin++;
                    }

                    if (itemSelected < filteredList.Count + 1) { itemSelected++; }
                    else { itemSelected = 1; itemMin = 0; }
                    horizontalIndex = 0;
                }
                if (key == ConsoleKey.D)
                {
                    if (horizontalIndex < choosingItem.itemOptions.Length - 1)
                    {
                        horizontalIndex++;
                    }
                    else
                    {
                        horizontalIndex = 0;
                    }
                }

                if (key == ConsoleKey.I)
                {
                    choosing = false;
                }
                if (key == ConsoleKey.C)
                {
                    Render.getInstance.Buffer.DrawColored("#g>>#x ", 0, Console.CursorTop, ConsoleColor.Gray, false);
                    Render.getInstance.Buffer.Print();
                    getCommand();
                }


                if (key == ConsoleKey.H)
                {
                    drawKeyShortcuts();
                }

                if (key == ConsoleKey.Enter)
                {

                    // Klasické předměty (zbraň, brnění...)
                    if (choosingItem != null)
                    {
                        choosingItem.ItemActions(horizontalIndex);
                    }

                    /*
                        // Equip
                        if (choosingItem != null && choosingItem.attributes[(int)GameItem.atr.equiped] == 0 && (GameWorld.getInstance.player.equiped[choosingItem.attributes[(int)GameItem.atr.type]] == false))
                        {
                            choosingItem.Equip();
                            choosingItem.attributes[(int)GameItem.atr.equiped] = 1;
                            GameWorld.getInstance.player.equiped[choosingItem.attributes[(int)GameItem.atr.type]] = true;
                        }
                        // Unequip
                        else if (choosingItem != null && choosingItem.attributes[(int)GameItem.atr.equiped] == 1 && (GameWorld.getInstance.player.equiped[choosingItem.attributes[(int)GameItem.atr.type]] == true))
                        {
                            choosingItem.UnEquip();
                            choosingItem.attributes[(int)GameItem.atr.equiped] = 0;
                            GameWorld.getInstance.player.equiped[choosingItem.attributes[(int)GameItem.atr.type]] = false;
                        }
                    
                  */

                    if (itemSelected == filteredList.Count + 1) { choosing = false; } 
                }

               
            }
        }
        void getCommand()
        {
            string[] commands = { "find", "jmp" };
            bool input = true;
            string command = "";
            while(input)
            {
                Console.CursorLeft = 3;

                ConsoleKeyInfo mkey = Console.ReadKey(true);
                ConsoleKey key = mkey.Key;

                if (key != ConsoleKey.Enter && key != ConsoleKey.Backspace)
                {
                    command += mkey.KeyChar;
                }
                else
                {
                    if (key == ConsoleKey.Backspace)
                    {
                        if (command.Length > 0)
                        {
                            command = command.Remove(command.Length - 1);
                        }
                    }

                    if (key == ConsoleKey.Enter)
                    {
                        input = false;
                    }
                }
                Render.getInstance.Buffer.clearRow(Console.CursorTop);
                Render.getInstance.Buffer.DrawColored("#g>>#x ", 0, Console.CursorTop, ConsoleColor.Gray, false);

                if (command.StartsWith("find"))
                {
                    Render.getInstance.Buffer.DrawColored("#gfind#x‡#h" + command.Replace("find","") + " #x‡", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false);
                }
                else if (command.StartsWith("jmp"))
                {
                    Render.getInstance.Buffer.DrawColored("#gjmp#x‡#h" + command.Replace("jmp", "") + " #x‡", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false);
                }
                else
                {
                    Render.getInstance.Buffer.DrawColored(command, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false);

                }
                Render.getInstance.Buffer.Print();
            }
            Console.CursorVisible = false;

            Render.getInstance.Buffer.Clear();

            string searchString = command;

            for (int i = 0; i < commands.Length; i++)
            {
                if (searchString.ToLower().StartsWith(commands[i].ToLower()))
                {
                    searchString = searchString.Replace(commands[i], "");
                    searchString = searchString.Replace(" ", "");
                }
            }
            Console.SetCursorPosition(0, 0);
            Render.getInstance.Buffer.Draw(Strings.getInstance.horizontalLine);
            Render.getInstance.Buffer.NewLine();
            Render.getInstance.Buffer.DrawColored("Vyhledávání na dotaz \"#y" + searchString + "#x\"", 0, Console.CursorTop, ConsoleColor.Gray, false, true);
            Render.getInstance.Buffer.Draw(Strings.getInstance.horizontalLine);
            Console.SetCursorPosition(0, 5);

            if (command.StartsWith("find", StringComparison.Ordinal))
            {
                command = command.Replace("find", "");
                int i = 0;
                int pocetNalezu = 0;

                foreach(GameItem itm in items)
                {
                    
                    if (itm.cleanName.ToLower().Contains(command.ToLower()))
                    {
                        pocetNalezu++;
                        Render.getInstance.Buffer.Draw(" > ");
                        itm.drawItemName();

                        int xx = Console.CursorLeft;
                        string center = "";

                        while (xx < 40) { center += " "; xx++; }
                        Render.getInstance.Buffer.Draw(center + "[" +i.ToString() + "]");
                        Render.getInstance.Buffer.NewLine();
                    }

                    i++;
                }

                Console.SetCursorPosition(0, 4);
                string sklonujPocetNalezu = "";
                if (pocetNalezu == 0) { sklonujPocetNalezu = "#rNenalezeny žádné odpovídají předměty. :(#x "; }
                else if (pocetNalezu == 1) { sklonujPocetNalezu = "Nalezen #yjeden#x odpovídající předmět."; }
                else if (pocetNalezu > 1 && pocetNalezu < 5) { sklonujPocetNalezu = "Nalezeny #y" + pocetNalezu.ToString() + "#x odpovídající předměty."; }
                else { sklonujPocetNalezu = "Nalezeno #y" + pocetNalezu.ToString() + "#x odpovídajících předmětů."; }

                Render.getInstance.Buffer.DrawColored(sklonujPocetNalezu, 0, Console.CursorTop, ConsoleColor.Gray, false);
                Render.getInstance.Buffer.Print();
                Console.ReadKey(true);
            }

            if (command.StartsWith("jmp", StringComparison.Ordinal))
            {
                command = command.Replace("jmp", "");

                try
                {
                    int index = Convert.ToInt32(command);

                    if (index > -1 && index < items.Count)
                    {
                        itemSelected = index + 1;

                        if (index <= 4)
                        {
                            itemMin = 0;
                        }
                        else if (index > items.Count - 10)
                        {
                            itemMin = items.Count - 10;
                        }
                        else
                        {
                            itemMin = index - 4;
                        }
                    }
                }
                catch
                {

                }
            }

            if (command.StartsWith("filter", StringComparison.Ordinal))
            {
                command = command.Replace("filter", "");

                if (command.Contains("starred"))
                {
                    filter = showFilter.starred;
                    itemMin = 0;
                    itemSelected = 1;
                }
                if (command.Contains("all"))
                {
                    filter = showFilter.all;
                    itemMin = 0;
                    itemSelected = 1;
                }
            }

        }

        void drawKeyShortcuts()
        {
            buffer buffer = Render.getInstance.Buffer;
            buffer.Clear();
            Console.SetCursorPosition(0, 0);

            buffer.DrawColored("[#yC#x] - Konzole", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
            buffer.NewLine();

            buffer.DrawColored("jmp #gn#x     > Označí v inventáři předmět #gn#x jako aktivní.", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
            buffer.DrawColored("drop #gn#x    > Vyhodí z inventáře předmět #gn#x.", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
            buffer.DrawColored("del #gn#x     > Zničí v inventáři předmět #gn#x.", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
            buffer.DrawColored("drop junk > Vyhodí z inventáře všechny předměty označené jako odpad.", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
            buffer.DrawColored("del junk  > Zničí všechny předměty v inventáři, označené jako odpad.", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
            buffer.DrawColored("find #gitem#x > Prohledá inventář a vrátí indexy všech shod.", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);

            buffer.Print();
            Console.ReadKey();
        }

        public void itemAdd(GameItem item, int count = 1)
           {           
               items.Add(item);
           }

        public void itemRemove(GameItem item, int count = 1)
        {
             items.Remove(item);
        }
    }
}
