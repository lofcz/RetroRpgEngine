using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            int currentMsStep = 0;

            Console.SetCursorPosition(0, 0);
            Render.getInstance.Buffer.Clear();

            CharacterCreation.getInstance.GetPlayerClass();
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(0, 0);
            Render.getInstance.Buffer.Clear();
            Render.getInstance.Buffer.Print();
            //    Intro.getInstance.DisplayIntro();
    //        Parser.getInstance.parseImage("award.txt",true,ConsoleColor.Gray, Parser.Effects.typewriter);
            Console.SetCursorPosition(0, 0);
            Console.ReadKey();
            Render.getInstance.Buffer.Clear();

            // TEST INVENTÁŘE   
            GameItem item = new GameItem("Pirátská šavle", ConsoleColor.Yellow, "Stará šavle nějakého piráta.", oPlayer.ItemsEquiped.Weapon);
            GameItem item1 = new GameItem("Kožená vesta", ConsoleColor.Gray, "Zahřeje když je zima.", oPlayer.ItemsEquiped.Armor);

            item.attributes[(int)GameItem.atr.damage] = 10;
            item1.attributes[(int)GameItem.atr.damage] = 4;
            item.attributes[(int)GameItem.atr.hp] = 7;

            Inventory.getInstance.itemAdd(item);
            Inventory.getInstance.itemAdd(item1);

            Render.getInstance.Buffer.Print();
            Console.ReadKey();

            Parser.getInstance.ParseMap();

            // TEST
            for (int i = 0; i < 5; i++)
            {
                oWallMoveable testWall = new oWallMoveable('#', "movingWall", ConsoleColor.Yellow, 5, 5 + i);
                GameWorld.getInstance.moveableWallList.Add(testWall);
            }
            // END TEST


            while (true)
            {           
                Console.SetCursorPosition(0, 0);
                Render.getInstance.Buffer.Clear();

                Thread oThread = new Thread(new ThreadStart(Render.getInstance.drawWorld));
                oThread.Start();
                oThread.Join();



                //  Console.Clear();
                //  Render.getInstance.DrawHeader("Informace: ");
                // Render.getInstance.DrawMapInfo();


               bool activeStep = PreRender.getInstance.PlayerMove();

                // ACTIVE STEP

                if (activeStep)
                {
                    Render.getInstance.fps();
                    currentMsStep = 0;
                    oEnemy.addEnemy(random.Next(1, 5), random.Next(1, 5), oEnemy.EnemyType.Goblin);
                }
                else if (currentMsStep > GameWorld.getInstance.gameSpeed)
                {
                    Render.getInstance.fps();
                    currentMsStep = 0;
                }




                Render.getInstance.DrawPlayerStats();
                currentMsStep++;
                Thread.Sleep(1);
            }
        }
    }
}
