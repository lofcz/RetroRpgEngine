using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroRPG.GameObjects
{
    // Třida pro vestavěné promměné, dědí z ní každý objekt
    class GameObject
    {
        public int x,y,xx,yy;
        public ConsoleColor color;
        public int hp;
        public int max_hp;
        public char symbol;
        public int value;
        public string accessName;
        public Queue<int> targetX = new Queue<int>();
        public Queue<int> targetY = new Queue<int>();
        public bool active = false;

        int id;

        // Přetížení pro unikátní objekty
        public GameObject()
        {
            Render.getInstance.actualID++;
            id = Render.getInstance.actualID;
        }

        // Přetížení pro NPC based objekty
        public GameObject(char symbol, string accessName, ConsoleColor color, int x, int y, int hp)
        {

            Render.getInstance.actualID++;
            this.x = x;
            this.y = y;
            xx = x;
            yy = y;
            this.hp = hp;
            this.symbol = symbol;
            id = Render.getInstance.actualID;
            this.accessName = accessName;
            this.color = color;

            max_hp = hp;
        }

        public double distanceToPoint(int xx, int yy, int startX, int startY)
        {
             double distance = -1;

            distance = Math.Round(Math.Sqrt(Math.Pow(Math.Abs(startX - xx),2) + Math.Pow(Math.Abs(startY - yy), 2)));
            return distance;
        }

        public void showStats()
        {
            Console.WriteLine("--- {0} (id: {1}) ---", accessName, id);
            Console.WriteLine("Hp: " + hp);
            Console.WriteLine("Max hp: " + max_hp);

        }

        public void addPositionToQueue()
        {
            targetX.Enqueue(xx);
            targetY.Enqueue(yy);
        }

        public override string ToString()
        {
            return id.ToString();
        }
    }
}
