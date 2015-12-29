using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroRPG.Objects;

namespace RetroRPG
{
    class Inventory
    {
         List<GameItem> items = new List<GameItem>();
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
        
     public void drawInventory()
        {
            bool choosing = true;
            int itemSelected = 1;
            int actualItem = 1;
            int length = 0;

            while (choosing)
            {
                Render.getInstance.Buffer.Clear();
                Console.SetCursorPosition(0, 0);
                actualItem = 0;
                GameItem choosingItem = null;
                int horizontalIndex = 0;
                string equipedItems = "";
                ConsoleColor equipedItemsColors = ConsoleColor.Gray;

                foreach (GameItem item in items)
                {
                    if (item.attributes[(int)GameItem.atr.equiped] == 1)
                    {
                        equipedItems = item.itemName;
                        equipedItemsColors = item.itemColor;
                    }

                }

                Render.getInstance.Buffer.Draw("Inventář", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
                Render.getInstance.Buffer.NewLine();
                Render.getInstance.Buffer.Draw(Strings.getInstance.horizontalLine, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
                Render.getInstance.Buffer.NewLine();
                Render.getInstance.Buffer.Draw("Používané věci", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
                Render.getInstance.Buffer.NewLine();
                Render.getInstance.Buffer.Draw(" • Zbraň: ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
                if (equipedItems == "")
                {
                    Render.getInstance.Buffer.Draw("Holé ruce", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
                }
                else
                {
                    Render.getInstance.Buffer.Draw(equipedItems, Console.CursorLeft, Console.CursorTop, equipedItemsColors);
                }
                Render.getInstance.Buffer.NewLine();
                Render.getInstance.Buffer.Draw(Strings.getInstance.horizontalLine, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
                Render.getInstance.Buffer.NewLine();
                Render.getInstance.Buffer.NewLine();

                foreach (GameItem item in items)
                {
                    actualItem++;
                    if (itemSelected == actualItem) { choosingItem = item; }

                    if (item.attributes[(int)GameItem.atr.equiped] == 1)
                    {
                        Render.getInstance.Buffer.Draw("[Nasazeno]", Console.CursorLeft, Console.CursorTop, ConsoleColor.Green);
                    }

                    // Vykreslí info o hover předmětu
                    if (itemSelected == actualItem) 
                        { 
                        Render.getInstance.Buffer.Draw(" > ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Green);
                        item.drawItemStats();
                        item.drawItemOptions();
                        Render.getInstance.Buffer.NewLine();
                        item.drawItemDescription();
                        }
                    else
                    {
                        Render.getInstance.Buffer.Draw("> ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
                        item.drawItemStats();
                    }
                   
                    Render.getInstance.Buffer.NewLine();
                }


                // Vykreslí možnost "zpět"
                if (itemSelected == items.Count + 1)
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


                ConsoleKey key = Console.ReadKey().Key;

                if (key == ConsoleKey.W)
                {
                    if (itemSelected > 0) { itemSelected--; }
                    else { itemSelected = items.Count + 1; }
                    horizontalIndex = 0;
                }
                if (key == ConsoleKey.S)
                {
                    if (itemSelected < items.Count + 1) { itemSelected++; }
                    else { itemSelected = 1; }
                    horizontalIndex = 0;
                }

                if (key == ConsoleKey.Enter)
                {
                    if (choosingItem != null && choosingItem.attributes[(int)GameItem.atr.equiped] == 0 && (GameWorld.getInstance.player.equiped[(int)oPlayer.ItemsEquiped.Weapon] == false))
                    {
                        choosingItem.Equip();
                        choosingItem.attributes[(int)GameItem.atr.equiped] = 1;
                        GameWorld.getInstance.player.equiped[(int)oPlayer.ItemsEquiped.Weapon] = true;
                    }
                    else if (choosingItem != null && choosingItem.attributes[(int)GameItem.atr.equiped] == 1 && (GameWorld.getInstance.player.equiped[(int)oPlayer.ItemsEquiped.Weapon] == true))
                    {
                        choosingItem.UnEquip();
                        choosingItem.attributes[(int)GameItem.atr.equiped] = 0;
                        GameWorld.getInstance.player.equiped[(int)oPlayer.ItemsEquiped.Weapon] = false;
                    }

                    if (itemSelected == items.Count + 1) { choosing = false; } 
                }

               
            }
        }

        public void itemAdd(GameItem item)
           {
               items.Add(item);
           }

    }
}
