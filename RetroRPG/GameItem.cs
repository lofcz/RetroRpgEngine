using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroRPG
{
    class GameItem
    {
        public int[] attributes;
        const int total_attributes = 2;
        string[] attributesNames = {"Život","Poškození" };

        public enum atr
        {
            hp,damage
        };

        public GameItem()
        {
            for (int i = 0; i < total_attributes; i++)
            {
                attributes[i] = 0;
            }
        }

        public void drawItemStats()
        {
            for (int i = 0; i < total_attributes; i++)
            {
                if (attributes[i] > 0)
                {
                    Render.getInstance.Buffer.Draw(attributesNames[i] + ":" + attributes[i], Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
                }
            }
        }

    }
}
