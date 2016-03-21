using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroRPG.GameObjects;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using RetroRPG.Engine;

namespace RetroRPG
{
    // V této třídě proběhne pohyb jednotek
    class PreRender
    {
        public Combat combat;
        private static PreRender render;
        GameWorld gameWorld = GameWorld.getInstance;
        int playerXprev = 0;
        int playerYprev = 0;

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


        [DllImport("user32.dll", EntryPoint = "GetKeyboardState", SetLastError = true)]
        private static extern bool NativeGetKeyboardState([Out] byte[] keyStates);

        private static bool GetKeyboardState(byte[] keyStates)
        {
            if (keyStates == null)
                throw new ArgumentNullException("keyState");
            if (keyStates.Length != 256)
                throw new ArgumentException("The buffer must be 256 bytes long.", "keyState");
            return NativeGetKeyboardState(keyStates);
        }

        private static byte[] GetKeyboardState()
        {
            byte[] keyStates = new byte[256];
            if (!GetKeyboardState(keyStates))
                throw new Win32Exception(Marshal.GetLastWin32Error());
            return keyStates;
        }

        private static bool AnyKeyPressed()
        {
            byte[] keyState = GetKeyboardState();
            // skip the mouse buttons
            return keyState.Skip(8).Any(state => (state & 0x80) != 0);
        }


        public bool PlayerMove()
        {
            playerXprev = GameWorld.getInstance.player.x;
            playerYprev = GameWorld.getInstance.player.y;

            if (Console.KeyAvailable == true)
            {
                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.W:
                        {
                            CollisionPre(GameWorld.getInstance.player.x, GameWorld.getInstance.player.y - 1);

                            if (gameWorld.getMap(GameWorld.getInstance.player.x, GameWorld.getInstance.player.y - 1) != GameWorld.state.wall)
                            {
                                GameWorld.getInstance.player.y--;
                            }

                            break;
                        }
                    case ConsoleKey.S:
                        {
                            CollisionPre(GameWorld.getInstance.player.x, GameWorld.getInstance.player.y + 1);

                            if (GameWorld.getInstance.getMap(GameWorld.getInstance.player.x, GameWorld.getInstance.player.y + 1) != GameWorld.state.wall)
                            {
                                GameWorld.getInstance.player.y++;
                            }
                            break;
                        }
                    case ConsoleKey.A:
                        {
                            CollisionPre(GameWorld.getInstance.player.x - 1, GameWorld.getInstance.player.y);

                            if (GameWorld.getInstance.getMap(GameWorld.getInstance.player.x - 1, GameWorld.getInstance.player.y) != GameWorld.state.wall)
                            {
                                GameWorld.getInstance.player.x--;
                            }
                            break;
                        }
                    case ConsoleKey.D:
                        {
                            CollisionPre(GameWorld.getInstance.player.x + 1, GameWorld.getInstance.player.y);

                            if (GameWorld.getInstance.getMap(GameWorld.getInstance.player.x + 1, GameWorld.getInstance.player.y) != GameWorld.state.wall)
                            {
                                GameWorld.getInstance.player.x++;
                            }
                            break;
                        }

                    case ConsoleKey.I:
                        {
                            //Inventory.getInstance.drawInventory(10);
                            CharacterCreation.getInstance.increasePlayerStats(gameWorld.player.lvlupVlastnosti, gameWorld.player.lvlupDovednosti);
                            break;
                        }
                }

                CollisionAfter(GameWorld.getInstance.player.x, GameWorld.getInstance.player.y);
                return true;
            }

            return false;
        }
        

        void CollisionAfter(int x, int y)
        {

            switch (GameWorld.getInstance.getMap(GameWorld.getInstance.player.x,GameWorld.getInstance.player.y))
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
                case GameWorld.state.action:
                    {
                        foreach (oAction action in GameWorld.getInstance.actionList)
                        {
                            if (action.x == x && action.y == y)
                            {
                                action.OnHitPlayer();
                                break;
                            }
                        }
                        break;
                    }

                case GameWorld.state.item:
                    {
                        foreach (GameItem item in GameWorld.getInstance.itemsList)
                        {
                            if (item.x == x && item.y == y)
                            {
                                Inventory.getInstance.itemAdd(item);
                                if (item.messageOnPickup) { Messages.getInstance.itemGetMessage("nadpis", item.itemName, item.cleanName); }
                                GameWorld.getInstance.itemsList.Remove(item);
                                break;
                            }
                        }
                        break;
                    }

                case GameWorld.state.enemy:
                    {
                        foreach (oEnemy enemy in GameWorld.getInstance.enemyList)
                        {
                            if (enemy.x == x && enemy.y == y)
                            {
                                combat = new Combat(enemy);
                                combat.drawEntrance();
                                GameWorld.getInstance.player.setPosition(playerXprev, playerYprev);
                               // GameWorld.getInstance.enemyList.Remove(enemy);
                                break;
                            }
                        }
                        break;
                    }

                case GameWorld.state.movingWall:
                    {
                        foreach (oWallMoveable wall in GameWorld.getInstance.moveableWallList)
                        {
                            if (wall.x == x && wall.y == y)
                            {
                                wall.active = true;
                                GameWorld.getInstance.gameSpeed--;
                                break;
                            }
                        }
                        break;
                    }
            }
        }

        void CollisionPre(int x, int y)
        {
            switch (GameWorld.getInstance.getMap(x, y))
            {
                case GameWorld.state.wall:
                    {
                        oWall wall = GameWorld.getInstance.wallList.Find(i => i.x == x && i.y == y);

                        if (wall.secret.isSecret)
                        {
                            wall.secret.isSecret = false;
                            wall.color = ConsoleColor.Green;
                            LogItem lI = new LogItem(LogItem.LogPrefix.achievment, string.Format("Našel jsi tajnou zeď {0}!", wall.ToString()), 60, ConsoleColor.Gray);
                            Render.getInstance.AddLog(lI);

                            Structs.Point point = new Structs.Point();
                            point.x = wall.secret.secretPosition.x;
                            point.y = wall.secret.secretPosition.y;
                            ConsolePhysics.getInstance.Trail(wall, point);
                        }

                        break;
                    }
            }
        }
    }
}
