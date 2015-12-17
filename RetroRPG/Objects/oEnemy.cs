using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroRPG.Objects;

namespace RetroRPG
{
    class oEnemy : GameObject
    {

        public oEnemy(char symbol, string accessName, ConsoleColor color, int x, int y, int hp) : base(symbol, accessName, color, x, y, hp) { }




        public static void addEnemy(int x, int y)
        {
        
            oEnemy enemy = new oEnemy('E', "Goblin", ConsoleColor.Red, x, y, 20);
            GameWorld.getInstance.enemyList.Add(enemy);
        }
    }
    

}
