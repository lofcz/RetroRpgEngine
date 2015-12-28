using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroRPG
{
    class GameItem
    {
        const int total_attributes = 3;
        public int[] attributes = new int[total_attributes];      
        string[] attributesNames = {"Život","Poškození" };
        string itemName;
        ConsoleColor itemColor;

        public enum atr
        {
            hp,damage
        };

        public GameItem(string itemName, ConsoleColor itemColor)
        {      
            for (int i = 0; i < total_attributes; i++)
            {
                attributes[i] = 0;
            }

            this.itemName = itemName;
            this.itemColor = itemColor;
        }

        public void drawItemStats()
        {
            string output = "";
            for (int i = 0; i < total_attributes; i++)
            {
                if (attributes[i] > 0)
                {
                    output += attributesNames[i] + ":" + attributes[i] + ", ";                
                }
            }

            if (output != "")
            {
                output = output.Remove(output.Length - 2); // Odstraním poslední čárku v řetěztci.
                Render.getInstance.Buffer.Draw(itemName, Console.CursorLeft, Console.CursorTop, itemColor);
                Render.getInstance.Buffer.Draw(": " + output, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
                Render.getInstance.Buffer.Print();
            }
         
        }

    }
}
