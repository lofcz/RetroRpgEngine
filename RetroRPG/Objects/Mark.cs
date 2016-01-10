using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroRPG.Objects
{
    // Znamení
    class Mark
    {
        Combat combat = PreRender.getInstance.combat;
        public enum Znameni
        {
            znameniOsudu, znameniHada, znameniRustu
        };

        public enum Udalosti
        {
            vyvazenyUtok,obrannyUtok,energickyUtok
        };

        public enum Vlastnik
        {
            hrac, souper
        };

        public string name;
        public ConsoleColor color;
        public Znameni znameni;
        string accesName = "";
        public int timeToExplode = -1;

        public Mark(string name, Znameni znameni)
        {
            this.name = name;
            this.color = color;
            this.znameni = znameni;

            switch(znameni)
            {
                case Znameni.znameniOsudu:
                    {
                        accesName = "[#yX#x";
                        timeToExplode = 2;
                        break;
                    }
            }
        }


        public void drawMark(Vlastnik vlastnik)
        {
            string output = accesName;
            if (timeToExplode != -1) { output += " - #r" + timeToExplode + "#x"; }

            output += "]";
            if (vlastnik == Vlastnik.souper) {combat.drawEnemyMarkStr += output + ","; combat.drawEnemyMarkStrLenght += output.Length + 1; if (combat.drawEnemyMarkStrLenght > 48 && combat.drawEnemyMarkStr.Length > 49) { combat.drawEnemyMarkStr += "\a"; combat.drawEnemyMarkStrLenght = 0; } } // 64 MAX
           //Render.getInstance.Buffer.DrawColored(output, x + Console.CursorLeft, y, ConsoleColor.Gray, false, false);
        }

        public void explode()
        {
            switch (znameni)
            {
                case Znameni.znameniOsudu:
                    {
                        combat.tempInt++;
                        break;
                    }
            }
        }
    }
}
