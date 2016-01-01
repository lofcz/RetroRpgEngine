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
        }
    }
}
