using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroRPG.Objects;

namespace RetroRPG
{
    // Třída pro udržování seznamů herních objektů
    class GameWorld
    {
        public enum state
        {
            free,wall,enemy,player,gold,movingWall
        };

        public const int width = 1000;
        public const int height = 1000;
        public int gameSpeed = 4;

        //public state[,] map = new state[100, 100];
        public state[] map = new state[width * height];

        public state getMap(int x, int y)
        {
            try
            {
                return map[y * width + x];
            }
            catch
            {
                return state.free;
            }
        }

        public int id = 100;
        public oPlayer player = new oPlayer('P', "oPlayer",ConsoleColor.Green, 10, 3, 40);
        public List<oEnemy> enemyList = new List<oEnemy>();
        public List<oWall> wallList = new List<oWall>();
        public List<oWallMoveable> moveableWallList = new List<oWallMoveable>();
        public List<oGold> goldList = new List<oGold>();

        private static GameWorld gameWorld;
        private GameWorld()
        {
              for (int y = 0; y < height; y++)
                {
                 for (int x = 0; x < width; x++)
                  {
                  map[y * width + x] = state.free;
                  }
                }
        }

        public static GameWorld getInstance
        {
            get
            {
                if (gameWorld == null)
                {
                    gameWorld = new GameWorld();
                }

                return gameWorld;
            }
        }

   


        public void ShowInfo()
        {
           player.showStats();
           
            foreach(oEnemy enemy in enemyList)
            {
                enemy.showStats();
            }
            Console.ReadKey();
        }
    }
}
