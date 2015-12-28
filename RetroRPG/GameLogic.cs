using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroRPG.Objects
{
    // Třída pro herní smyčku
    class GameLogic
    {
        Random random = new Random();

        private static GameLogic gameLogic;
        private GameLogic()
        {
               
         }

        public static GameLogic getInstance
        {
            get
            {
                if (gameLogic == null)
                {
                    gameLogic = new GameLogic();
                }

                return gameLogic;
            }
        }
 

        public void Step()
        {
            Console.SetCursorPosition(0, 0);
            Render.getInstance.Buffer.Clear();

            //    Intro.getInstance.DisplayIntro();
            Parser.getInstance.parseImage("award.txt",true,ConsoleColor.Gray, Parser.Effects.typewriter);
            Console.SetCursorPosition(0, 0);
            Console.ReadKey();
            GameItem item = new GameItem("Pirátská šavle", ConsoleColor.Green);
            Render.getInstance.Buffer.Clear();
            item.attributes[(int)GameItem.atr.damage] = 10;
            item.attributes[(int)GameItem.atr.hp] = 7;
            item.drawItemStats();
            Render.getInstance.Buffer.NewLine();
            Render.getInstance.Buffer.DrawColored("This is a fg of #rRed #xand now #ywe're#x back to normal.", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
            Render.getInstance.Buffer.Print();
            Console.ReadKey();
            Parser.getInstance.ParseMap();

            while (true)
            {
                Console.SetCursorPosition(0, 0);
                Render.getInstance.Buffer.Clear();
              //  Console.Clear();
              //  Render.getInstance.DrawHeader("Informace: ");
               // Render.getInstance.DrawMapInfo();
                oEnemy.addEnemy(random.Next(1,5),random.Next(1,5));
              //  GameWorld.getInstance.ShowInfo();
                Render.getInstance.drawWorld();             
                Render.getInstance.DrawPlayerStats();
                PreRender.getInstance.PlayerMove();
            }
        }
    }
}
