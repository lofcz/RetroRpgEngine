using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroRPG.Objects;

namespace RetroRPG
{
    // V této třídě proběhne pohyb jednotek
    class PreRender
    {
        private static PreRender render;

        public static PreRender getInstance
        {
            get
            {
                if (render == null)
                {
                    render = new PreRender();
                }

                return render;
            }
        }

        public void PlayerMove()
        {
            ConsoleKey key = Console.ReadKey(true).Key;

            switch(key)
            {
                case ConsoleKey.W:
                    {
                        if (GameWorld.getInstance.map[GameWorld.getInstance.player.x, GameWorld.getInstance.player.y-1] != GameWorld.state.wall)
                        {
                            GameWorld.getInstance.player.y--;
                        }
                       
                        break;
                    }
                case ConsoleKey.S:
                    {
                        if (GameWorld.getInstance.map[GameWorld.getInstance.player.x, GameWorld.getInstance.player.y + 1] != GameWorld.state.wall)
                        {
                            GameWorld.getInstance.player.y++;
                        }
                        break;
                    }
                case ConsoleKey.A:
                    {
                        if (GameWorld.getInstance.map[GameWorld.getInstance.player.x - 1, GameWorld.getInstance.player.y] != GameWorld.state.wall)
                        {
                            GameWorld.getInstance.player.x--;
                        }
                        break;
                    }
                case ConsoleKey.D:
                    {
                        if (GameWorld.getInstance.map[GameWorld.getInstance.player.x + 1, GameWorld.getInstance.player.y] != GameWorld.state.wall)
                        {
                            GameWorld.getInstance.player.x++;
                        }
                        break;
                    }

                case ConsoleKey.I:
                    {
                        Inventory.getInstance.drawInventory();
                        break;
                    }
            }

            CollisionAfter(GameWorld.getInstance.player.x, GameWorld.getInstance.player.y);
        }

        void CollisionAfter(int x, int y)
        {

            switch (GameWorld.getInstance.map[GameWorld.getInstance.player.x,GameWorld.getInstance.player.y])
            {
                case GameWorld.state.gold:
                    {                     
                        foreach(oGold gold in GameWorld.getInstance.goldList)
                        {
                            if (gold.x == x && gold.y == y)
                            {
                                GameWorld.getInstance.player.Gold += gold.value;
                                GameWorld.getInstance.player.hp -= 5;

                                GameWorld.getInstance.goldList.Remove(gold);
                                break;
                            }
                        }
                        break;
                    }
            }
        }
    }
}
