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
        public string imageFile;
        public string quote;
        public string battleTag;

        public oEnemy(char symbol, string accessName, ConsoleColor color, int x, int y, int hp, string imageFile, string quote, string battleTag) : base(symbol, accessName, color, x, y, hp) 
        {
            this.imageFile = imageFile;
            this.quote = quote;
            this.battleTag = battleTag;
        }




        public static void addEnemy(int x, int y)
        {
        
            oEnemy enemy = new oEnemy('E', "Goblin", ConsoleColor.Red, x, y, 20, ResourceTree.graphicsFoes + "enemyGoblinSimple.txt", "Vysměju se smrti do tváře!", "#g>#x #rGoblin#x si tě zlostně prohlíží. Boji se nevyhneš.");
            GameWorld.getInstance.enemyList.Add(enemy);
        }
    }
    

}
