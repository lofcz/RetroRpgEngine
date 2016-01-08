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
        public int fyzicalDamage;
        public int mana;
        public int max_mana;
        public int energy;
        public int max_energy;
        public int physicalDamge;

        public oEnemy(char symbol, string accessName, ConsoleColor color, int x, int y, int hp, string imageFile, string quote, string battleTag, int fDamage, int pDamage, int mana, int energy) : base(symbol, accessName, color, x, y, hp) 
        {
            this.imageFile = imageFile;
            this.quote = quote;
            this.battleTag = battleTag;
            this.fyzicalDamage = fDamage;
            this.physicalDamge = pDamage;
            this.mana = mana;
            this.max_mana = mana;
            this.energy = energy;
            this.max_energy = energy;
        }




        public static void addEnemy(int x, int y)
        {
        
            oEnemy enemy = new oEnemy('E', "Goblin", ConsoleColor.Red, x, y, 20, ResourceTree.graphicsFoes + "enemyGoblinSimple.txt", "Vysměju se smrti do tváře!", "#g>#x #rGoblin#x si tě zlostně prohlíží. Boji se nevyhneš.", 20, 5, 30, 40);
            GameWorld.getInstance.enemyList.Add(enemy);
        }
    }
    

}
