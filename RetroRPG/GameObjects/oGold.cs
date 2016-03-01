using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace RetroRPG.GameObjects
{
    class oGold  : GameObject
    {
        public oGold (char symbol, string accessName, ConsoleColor color, int x, int y, int value)
        {
            this.x = x;
            this.y = y;
            this.symbol = symbol;
            this.accessName = accessName;
            this.color = color;
            this.value = value;
        }

        public static void addGold(int x, int y)
        {

            oGold gold = new oGold('◎', "Gold coin", ConsoleColor.Yellow, x, y, 5);
            GameWorld.getInstance.goldList.Add(gold);
        }

        public static void addGold(int x, int y, int value)
        {

            oGold gold = new oGold('◎', "Gold coin", ConsoleColor.Yellow, x, y, value);
            GameWorld.getInstance.goldList.Add(gold);
        }

        
    
    }
}
