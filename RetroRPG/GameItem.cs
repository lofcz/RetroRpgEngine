using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroRPG.Objects;

namespace RetroRPG
{
    class GameItem
    {
        const int total_attributes = 4;
        public int[] attributes = new int[total_attributes];      
        string[] attributesNames = {"Život","Poškození","Equiped","Typ předmětu" };
        public string itemName;
        public ConsoleColor itemColor;
        string itemDescription = "";
        //string[] itemOptions = {"Nasadit};

        public enum atr
        {
            hp,damage,equiped,type
        };

        public GameItem(string itemName, ConsoleColor itemColor, string itemDescription, oPlayer.ItemsEquiped itemType)
        {      
            for (int i = 0; i < total_attributes; i++)
            {
                attributes[i] = 0;
            }

            this.itemName = itemName;
            this.itemColor = itemColor;
            this.itemDescription = itemDescription;
            this.attributes[(int)atr.type] = (int)itemType;
        }

        public void drawItemStats()
        {
            string output = "";
            for (int i = 0; i < total_attributes; i++)
            {
                if (attributes[i] > 0)
                {
                    if (i != (int)GameItem.atr.equiped) // Speciální atributy vyškrteme ze seznamu na vykreslení
                    {
                        output += attributesNames[i] + ":" + attributes[i] + ", ";
                    }
                }
            }

            if (output != "")
            {
                output = output.Remove(output.Length - 2); // Odstraním poslední čárku v řetěztci.
                Render.getInstance.Buffer.Draw(output, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
               // Render.getInstance.Buffer.Print();
            }
         
        }

        public void drawItemName()
        {
            Render.getInstance.Buffer.DrawColored(itemName, Console.CursorLeft, Console.CursorTop, itemColor, true);
        }

        public void drawItemDescription()
        {
            Render.getInstance.Buffer.Draw(itemDescription, Console.CursorLeft, Console.CursorTop, ConsoleColor.DarkGray);
        }

        public void drawItemOptions()
        {
            Render.getInstance.Buffer.Draw("   ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);

            if (attributes[(int)GameItem.atr.equiped] == 0) { Render.getInstance.Buffer.Draw("[Nasadit]", Console.CursorLeft, Console.CursorTop, ConsoleColor.Yellow); }
            else { Render.getInstance.Buffer.Draw("[Sundat]", Console.CursorLeft, Console.CursorTop, ConsoleColor.Yellow); }
        }

        public void Equip()
        {
            for (int i = 0; i < total_attributes; i++)
            {
                if (i == (int)atr.hp)
                {
                    GameWorld.getInstance.player.hp += attributes[i];
                    GameWorld.getInstance.player.max_hp += attributes[i];
                }
              
            }
        }

        public void UnEquip()
        {
            for (int i = 0; i < total_attributes; i++)
            {
                if (i == (int)atr.hp)
                {
                    GameWorld.getInstance.player.hp -= attributes[i];
                    GameWorld.getInstance.player.max_hp -= attributes[i];
                }

            }
        }



    }
}
