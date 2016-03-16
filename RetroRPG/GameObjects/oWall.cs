using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroRPG.GameObjects;

namespace RetroRPG
{
    class oWall : GameObject
    {
        public Structs.SecretMove secret;

        public oWall (char symbol, string accessName, ConsoleColor color, int x, int y, Structs.SecretMove secret)
        {
            this.x = x;
            this.y = y;
            this.symbol = symbol;
            this.accessName = accessName;
            this.color = color;
            this.secret = secret;
        }

        public static void addWall(int x, int y, bool isSecret, Structs.Point point)
        {
            oWall wall = new oWall('#', "Wall", ConsoleColor.Gray, x, y, new Structs.SecretMove(isSecret,point));
            GameWorld.getInstance.wallList.Add(wall);
        }
    }
}
