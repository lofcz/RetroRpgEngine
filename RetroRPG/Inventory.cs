﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int itemSelected = 0;
            int actualItem = 0;
            int length = 0;

            while (choosing)
            {
                Render.getInstance.Buffer.Clear();
                Console.SetCursorPosition(0, 0);
                actualItem = 0;
                GameItem choosingItem = null;

                foreach (GameItem item in items)
                {
                    actualItem++;
                    if (itemSelected == actualItem) { choosingItem = item; } 

                    if (itemSelected == actualItem) { Render.getInstance.Buffer.Draw(" > ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Green); }
                    else
                    {
                        Render.getInstance.Buffer.Draw("> ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
                    }
                    item.drawItemStats();
                    Render.getInstance.Buffer.NewLine();
                }

                ConsoleKey key = Console.ReadKey().Key;

                if (key == ConsoleKey.W)
                {
                    if (itemSelected > 0) { itemSelected--; }
                    else { itemSelected = items.Count; }
                }
                if (key == ConsoleKey.S)
                {
                    if (itemSelected < items.Count) { itemSelected++; }
                    else { itemSelected = 0; }
                }

                if (key == ConsoleKey.Enter)
                {
                    if (choosingItem != null && choosingItem.attributes[(int)GameItem.atr.equiped] == 0)
                    {
                        choosingItem.Equip();
                        choosingItem.attributes[(int)GameItem.atr.equiped] = 1;
                    }

                    choosing = false;
                }

                Render.getInstance.Buffer.Print();
            }
        }

        public void itemAdd(GameItem item)
           {
               items.Add(item);
           }

    }
}
