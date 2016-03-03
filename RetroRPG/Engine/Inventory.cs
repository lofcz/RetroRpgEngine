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
        int itemMin = 0, itemMax;
        int itemSelected = 1;
        int actualItem = 0;

        public void drawInventory(int drawItemsCount)
        {
            int showItemsCount = drawItemsCount;

            bool choosing = true;
            int length = 0;
            int horizontalIndex = 0; // Update inventáře v 1.2

            string tempOutput = ""; // Refactoring inventáře v1.1 - Výsledek vykreslení sjednotíme do stringu

            while (choosing)
            {
                Render.getInstance.Buffer.Clear(true);

                itemMax = Math.Min(itemMin + showItemsCount, items.Count);
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


                Render.getInstance.Buffer.Draw(Strings.getInstance.horizontalLine, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
                Render.getInstance.Buffer.NewLine(2);
                Render.getInstance.Buffer.DrawColored("Předmět " + itemSelected.ToString() + " / " + items.Count, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, true, true);


                for (int i = itemMin; i < itemMax; i++ )
                {
                    actualItem++;
                    GameItem item = items[i];

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
                    if (itemMin > 0 && (items.Count - itemSelected > 5)) 
                    {
                        itemMin--;
                    }

                    if (itemSelected > 1) { itemSelected--; }
                    else { itemSelected = items.Count + 1; itemMin = items.Count - showItemsCount; }
                    horizontalIndex = 0;
                }
                if (key == ConsoleKey.S)
                {
                    if (itemMin < items.Count - showItemsCount && itemSelected > 5)
                    {
                        itemMin++;
                    }

                    if (itemSelected < items.Count + 1) { itemSelected++; }
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

                    if (itemSelected == items.Count + 1) { choosing = false; } 
                }

               
            }
        }
        void getCommand()
        {
            Console.CursorVisible = true;
            string command = Console.ReadLine();
            Console.CursorVisible = false;

            Render.getInstance.Buffer.Clear();
            Console.SetCursorPosition(0, 0);

            if (command.StartsWith("find", StringComparison.Ordinal))
            {
                command = command.Replace("find", "");
                int i = 0;

                foreach(GameItem itm in items)
                {
                    
                    if (itm.cleanName.Contains(command))
                    {
                        itm.drawItemName();
                        Render.getInstance.Buffer.Draw(i.ToString());
                        Render.getInstance.Buffer.NewLine();
                    }

                    i++;
                }
            }

            Render.getInstance.Buffer.Print();

            Console.ReadKey(true);
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
