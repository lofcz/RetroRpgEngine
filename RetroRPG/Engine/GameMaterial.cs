using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroRPG
{
    // Třída materiálů pro crafting
    class GameMaterial
    {
        public enum Rarity
        {
            Junk,Common,Uncommon,Rare,Set,Mythic,Legendary,Unique
        };

        string[] rarityFlag = { "#h", "", "#w", "#g", "#b", "#v", "#y", "#r" };
        public Rarity rarity;
        public string name;
        int number = 0;

        public GameMaterial(string name, Rarity rarity, int number)
        {
            this.name = name;
            this.rarity = rarity;
            this.number = number;
        }


        public string GetName(bool colored)
        {
            
            string flag = rarityFlag[(int)rarity];
          
            return flag + name + "#x‡";
        }

    }
}
