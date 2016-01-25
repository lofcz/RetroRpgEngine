using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroRPG.Objects;

namespace RetroRPG
{
    class oWallMoveable : GameObject
    {
        public oWallMoveable(char symbol, string accessName, ConsoleColor color, int x, int y)
        {
            this.x = x;
            this.y = y;
            this.symbol = symbol;
            this.accessName = accessName;
            this.color = color;
        }

        public static void addWall(int x, int y)
        {

            oWallMoveable wall = new oWallMoveable('#', "Wall", ConsoleColor.Yellow, x, y);
            GameWorld.getInstance.moveableWallList.Add(wall);
        }
    }
}
