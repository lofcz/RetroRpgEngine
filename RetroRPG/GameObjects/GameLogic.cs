using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroRPG;

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
    

            while (true)
            {
              //  Render.getInstance.Buffer.Clear();
                Console.SetCursorPosition(0, 0);
              //  Console.Clear();
              //  Render.getInstance.DrawHeader("Informace: ");
               // Render.getInstance.DrawMapInfo();
                Enemy.addEnemy(random.Next(1,5),random.Next(1,5));
              //  GameWorld.getInstance.ShowInfo();
           //     Render.getInstance.drawWorld();
             //   Render.getInstance.Buffer.Print();
             //   Render.getInstance.Buffer.DrawPlayerStats();
          //      PreRender.getInstance.PlayerMove();
            }
        }
    }
}
