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
        public string title = "Neznámý cestující";
        public ConsoleColor titleColor = ConsoleColor.DarkGray;
        public int stamina = 20;
        public int max_stamina = 20;
        public bool[] equiped = { false, false };
        public CharacterCreation.classes gameClass;
        public int level = 1;
        public int xp = 0;
        public int max_xp = 100;
        public int physicalDamage = 2;
        public int fyzicalDamage = 2;
        public int mana = 50;
        public int max_mana = 50;
        public int energy = 10;
        public int max_energy = 10;


        public int lvlupVlastnosti = 5;
        public int lvlupDovednosti = 0;

        public int[] Vlastnosti = new int[Enum.GetNames(typeof(vlastnosti)).Length];
        public int[] Dovednosti = new int[Enum.GetNames(typeof(dovednosti)).Length];

        public string[] vlastnostiName = { "Síla:         ", "Konstituce:   ", "Obratnost:    ", "Odolnost:     ", "Inteligence:  ", "Charisma:     ", "Vůle:         ", "Zručnost:     ", "Postřeh:      ", "Štěstí:       " };
        public string[] dovednostiName = { "Háčkování zámků:    ", "Plížení:            ", "Přesvědčování:      ", "Zastrašování:       ", "Runová magie:       ", "Elementární magie:  ", "Zaříkávání:         ", "Smlouvání:          ", "Zápal:              ", "Víra :              " };

        // Popis 
        public string[] vlastnostiPopis = { "Jednoduchý přístup k věci. S dostatečnou silou jde udělat spousta věcí.\nTřeba rozseknout soupeře jedinou ranou. #gSíla#x určuje #rfyzické poškození#x, které působíš svými útoky. ", "V zdravém těle zdravý duch. S velkou konstitucí se nemusíš bát ani těch nejstrašnějších úderů.\n#gKonstituce#x určuje hodnotu #rzdraví#x, jeho #rregeneraci#x a hodnotu #renergie#x. ", "Hbitá a nenápadná osoba bývá tou nejnebezpečnější. Schpnost vyhnout se letícímu šípu je vždy vítaná.\n#gObratnost#x zvyšuje šanci na #rvyhnutí se útoku#x, #rúspěšného zásahu#x a #rsnižuje energii potřebnou k úderu#x.", "Nikdo se ti nedostane pod kůži - doslova. Pořádné brnění tě ochrání na každé cestě.\n#gOdolnost#x snižuje #robdržené fyzické poškození#x.", "Říka se, že s chytrostí nejdál dojdeš. Myšlenka je mocnou zbraní.\n#gInteligence#x určuje #rpsychické poškození#x, které působíš magickými útoky.", "Šarm, osobní kouzlo, elegeance. Opravdu mocný jsi, když dokážeš porazit svého soupeře bez boje.\n#gCharisma#x určuje tvoje možnosti při konfrontaci s protivníkem.", "Vůle:         ", "Zručnost:     ", "Postřeh:      ", "Štěstí:       " };

        public enum vlastnosti
        {
            sila,konstituce,obratnost,odolnost,inteligence,charisma,vule,zrucnost,postreh,stesti
        };

        public enum dovednosti
        {
            hackovani_zamku,plizeni,presvedcovani,zastrasovani,runova_magie,elementarni_magie,zarikavani,smlouvani,zapal,vira
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

        public int dealDamage(oEnemy enemy)
        {
            Random random = new Random();

            double luckBonus = random.Next(Vlastnosti[(int)vlastnosti.stesti] / 3, (Vlastnosti[(int)vlastnosti.stesti] * 2) + 1);
            double redukce = 6 * Math.Log(enemy.defense) * Math.Log(enemy.defense, Math.E);
            redukce = Math.Min(redukce, 90);

            double vysledek = (enemy.physicalDamge + luckBonus) * ((100 - redukce) / 100);
            // MessageBox.Show(Convert.ToString(vysledek));
            //  MessageBox.Show(Convert.ToString(physicalDamge));
            enemy.hp -= Convert.ToInt32(Math.Round(vysledek));

            return Convert.ToInt32(Math.Round(vysledek));
        }



    }
}
