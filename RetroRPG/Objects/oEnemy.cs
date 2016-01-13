using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroRPG.Objects;
using System.Windows.Forms;

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
        public GameItem[] lootItems;
        public int defense;
        EnemyType type;

        public enum EnemyType
        {
            Goblin
        };

        public oEnemy(char symbol, string accessName, ConsoleColor color, int x, int y, int hp, string imageFile, string quote, string battleTag, int fDamage, int pDamage, int mana, int energy, int defense, params GameItem[] lootItems) : base(symbol, accessName, color, x, y, hp)
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
            this.lootItems = lootItems;
            this.defense = defense;
        }




        public static void addEnemy(int x, int y, EnemyType type)
        {
            oEnemy enemy = null;

            switch (type)
            {
                case EnemyType.Goblin:
                    {
                        enemy = new oEnemy('E', "Goblin", ConsoleColor.Red, x, y, 20, ResourceTree.graphicsFoes + "enemyGoblinSimple.txt", "Vysměju se smrti do tváře!", "#g>#x #rGoblin#x si tě zlostně prohlíží. Boji se nevyhneš.", 20, 5, 30, 40, 7);
                        break;
                    }
            }

            GameWorld.getInstance.enemyList.Add(enemy);
        }


        public void EnemyPlayTurn(oEnemy enemy)
        {
            getBasicDecision(enemy);

            switch (enemy.type)
            {
                case EnemyType.Goblin:
                    {

                        break;
                    }
            }

            dealDamage(Combat.attackType.vyvazenyUder, enemy);
        }

        void getBasicDecision(oEnemy enemy)
        {
            decimal percentHp = (enemy.hp / enemy.max_hp);
            decimal percentMana = (enemy.mana / enemy.max_mana);
            decimal percentStamina = (enemy.energy / enemy.max_energy);

            // Decide, which stat is priority;          
            Random random = new Random();

            // Decide, which attack will we do;
            // At first choose random attack, and calc chance to do it

       //     combat.enemy_log = "> " + enemy.accessName + " prudce vyrazil vpřed a zaútočil střechou";






        }

        void dealDamage(Combat.attackType type, oEnemy enemy)
        {
            Combat combat = PreRender.getInstance.combat;

            oPlayer player = GameWorld.getInstance.player;

            switch (type)
            {
                case Combat.attackType.vyvazenyUder:
                    {
                        double redukce = 6 * Math.Log(player.Vlastnosti[(int)oPlayer.vlastnosti.odolnost]) * Math.Log(player.Vlastnosti[(int)oPlayer.vlastnosti.odolnost], Math.E);
                        redukce = Math.Min(redukce, 90);

                        double vysledek = enemy.physicalDamge * ((100 - redukce) / 100);
                        // MessageBox.Show(Convert.ToString(vysledek));
                        //  MessageBox.Show(Convert.ToString(physicalDamge));
                        player.hp -= Convert.ToInt32(Math.Round(vysledek));
                        combat.enemy_log =  "> " + enemy.accessName + " prudce vyrazil vpřed a zaútočil střechou, čímž ti způsobil #y" + Convert.ToInt32(Math.Round(vysledek)) + "#x bodů poškození.";
                        break;
                    }
                case Combat.attackType.energickyUder:
                    {
                       
                        break;
                    }
              
            }

        }
    }
}
