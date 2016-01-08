using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroRPG
{
    // Třída pro souboje
    class Combat
    {
        oEnemy enemy;
        string enemyStatus;
        buffer buffer = Render.getInstance.Buffer;

        public Combat(oEnemy enemy)
        {
            this.enemy = enemy;
        }

        public void drawEntrance()
        {
            Console.CursorVisible = false;
            string drawEnemyName = "+- " + enemy.accessName + " -+"; 

            Console.SetCursorPosition(0, 0);
            Render.getInstance.Buffer.Clear();
            Render.getInstance.Buffer.Draw(Strings.getInstance.horizontalLine, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
            Render.getInstance.Buffer.NewLine();
            Render.getInstance.Buffer.DrawColored(drawEnemyName, Console.CursorLeft + ((Console.WindowWidth / 2) - drawEnemyName.ToString().Length / 2), Console.CursorTop, enemy.color, true);
            Render.getInstance.Buffer.NewLine();
            drawEnemyName = "\"" + enemy.quote + "\""; 
            Render.getInstance.Buffer.DrawColored(drawEnemyName, Console.CursorLeft + ((Console.WindowWidth / 2) - drawEnemyName.ToString().Length / 2), Console.CursorTop, ConsoleColor.DarkGray, true);
            Render.getInstance.Buffer.NewLine(2);
            Parser.getInstance.parseImage(enemy.imageFile, false, ConsoleColor.Gray, Parser.Effects.none);
            Render.getInstance.Buffer.NewLine();
            Render.getInstance.Buffer.Draw(Strings.getInstance.horizontalLine,Console.CursorLeft,Console.CursorTop,ConsoleColor.Gray);
            Render.getInstance.Buffer.NewLine();
            Render.getInstance.Buffer.DrawColored(enemy.battleTag, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, true);
            Render.getInstance.Buffer.Draw(" [Enter] ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Yellow);
            Render.getInstance.Buffer.Print();
            Console.ReadKey();
            Console.CursorVisible = true;
            DrawCombat(enemy);
        }

        public void DrawCombat(oEnemy enemy)
        {
            bool combatFlow = true;
            string enemy_log = enemy.battleTag;
            bool showHp = false;
            bool showMaxHp = false;
            bool showDamage = false;
            bool showMana = false;
            bool showEnergy = false;

            string enemyOutput = "Zdraví: ";


            while (combatFlow)
            {
                enemyOutput = "Zdraví: ";

                // Status soupeře
                if (showHp) { enemyOutput += "#g" + enemy.hp; } else { enemyOutput += "#h" + "???"; }
                if (showMaxHp) { enemyOutput += "#x / #g" + enemy.max_hp; } else { enemyOutput += "#x / #h" + "???"; }
                if (showDamage) { enemyOutput += "#x   Útok: #r" + enemy.fyzicalDamage + "#x / #b" + enemy.physicalDamge; } else { enemyOutput += "#x   Útok: #h" + "???" + "#x / #h" + "???"; }
                if (showMana) { enemyOutput += "#x   Mana: #c" + enemy.mana; } else { enemyOutput += "#x   Mana: #h" + "???"; }
                if (showEnergy) { enemyOutput += "#x    Energie: #y" + enemy.energy + "#x "; } else { enemyOutput += "#x    Energie: #h" + "???" + "#x "; }

                Console.SetCursorPosition(0, 0);
                buffer.Clear();
                buffer.Print();

                buffer.DrawColored(enemy.accessName, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
                buffer.DrawColored(Strings.getInstance.horizontalLine, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
                buffer.DrawColored(enemy_log, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
                buffer.NewLine();
                buffer.DrawColored(enemyOutput, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
                Render.getInstance.DrawBar(enemy.hp, enemy.max_hp, "no_text", ConsoleColor.Green, ':', false, 60);

                Console.CursorTop = 3;
                buffer.DrawColored("[#yX#x]", 80, Console.CursorTop, ConsoleColor.Gray, false, true);


                buffer.Print();
                ConsoleKey key = Console.ReadKey(true).Key;
            }
        }
    }
}
