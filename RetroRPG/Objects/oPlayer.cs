using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroRPG.Objects
{
    class oPlayer : GameObject
    {
        public oPlayer(char symbol, string accessName, ConsoleColor color, int x, int y, int hp) : base(symbol, accessName, color, x, y, hp) {  }

         int gold = 0;
        public string name = "LordOfFlies";
        public int stamina = 20;
        public int max_stamina = 20;
        public bool[] equiped = { false, false };
        public CharacterCreation.classes gameClass;

        public int[] Vlastnosti = new int[Enum.GetNames(typeof(vlastnosti)).Length];
        public int[] Dovednosti = new int[Enum.GetNames(typeof(dovednosti)).Length];

        public enum vlastnosti
        {
            sila,konstituce,obratnost,odolnost,inteligence,charisma,vule,zrucnost,postreh,stesti
        };

        public enum dovednosti
        {
            hackovani_zamku,plizeni,presvedcovani,zastrasovani,runova_magie,elementarni_magie,zatikavani,smlouvani,zapal,vira
        };

        public enum ItemsEquiped
        {
            Weapon,Armor,Consumable
        };

        public int Gold
            {
            get { return gold; }
            set { gold = value; }
            }
        public void setPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
      

        
    }
}
