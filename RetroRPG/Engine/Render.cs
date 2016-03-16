using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroRPG.GameObjects;
using System.Windows.Forms;

namespace RetroRPG
{
    // Zde proběhne vykreslení herního světa
    public class Render
    {
        #region Informace
        // Verze: 1.1
        // Stabilní: Ne
        #endregion
        #region UML
        // Singleton

        private static Render render;
        private Render() { }

        public static Render getInstance
        {
            get
            {
                if (render == null)
                {
                    render = new Render();
                }

                return render;
            }
        }
        #endregion

        public buffer Buffer = new buffer(110, 60, 110, 60);

        const int viewWidth = 60;
        const int viewHeight = 20;

        int cameraX = 0;
        int cameraY = 0;
        public int actualID = 100;
        string renderOutput = "";

       

        /// <summary>
        /// Vykreslí herní svět
        /// </summary>
        public void drawWorld()
        {
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
            renderOutput = "";

            cameraX = GameWorld.getInstance.player.x - (viewWidth / 2);
            cameraY = GameWorld.getInstance.player.y - (viewHeight / 2);
            
            for (int y = Math.Max(0, cameraY); y < Math.Min(cameraY + viewHeight, GameWorld.height); y++)
            {
                for (int x = Math.Max(0, cameraX + 10); x < Math.Min(cameraX + viewWidth, GameWorld.width); x++)
                {
                    GameWorld.getInstance.map[y * GameWorld.width + x] = GameWorld.state.free;
                }
            }

            
            foreach (oEnemy enemy in GameWorld.getInstance.enemyList)
            {
                GameWorld.getInstance.map[enemy.y * GameWorld.width + enemy.x] = GameWorld.state.enemy;         
            }

            foreach (oWallMoveable wall in GameWorld.getInstance.moveableWallList)
            {
                GameWorld.getInstance.map[wall.y * GameWorld.width + wall.x] = GameWorld.state.movingWall;
            }

            foreach (oWall wall in GameWorld.getInstance.wallList)
            {
                GameWorld.getInstance.map[wall.y * GameWorld.width + wall.x] = GameWorld.state.wall;
            }

            foreach (oGold gold in GameWorld.getInstance.goldList)
            {
                GameWorld.getInstance.map[gold.y * GameWorld.width + gold.x] = GameWorld.state.gold;
            }

            foreach (GameItem item in GameWorld.getInstance.itemsList)
            {
                GameWorld.getInstance.map[item.y * GameWorld.width + item.x] = GameWorld.state.item;
            }

            GameWorld.getInstance.map[GameWorld.getInstance.player.y * GameWorld.width + GameWorld.getInstance.player.x] = GameWorld.state.player;


            for (int y = Math.Max(0, cameraY); y < Math.Min(cameraY + viewHeight, GameWorld.height); y++) // Interujeme sloupce v dohledu kamery
            {
                if (y == Math.Max(0, cameraY))
                {
                    Buffer.Draw("╔", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
                    for (int i = Math.Max(0, cameraX + 10); i < Math.Min(cameraX + viewWidth, GameWorld.width); i++) { Buffer.Draw("═", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray); }
                     Buffer.Draw("╗", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
                    Buffer.NewLine();
                }

                for (int x = Math.Max(0, cameraX + 10); x < Math.Min(cameraX+viewWidth,GameWorld.width); x++) // Iterujeme řádky v dohledu kamery
                {
                    if (x == Math.Max(0, cameraX + 10)) { Buffer.Draw("║", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray); }

                    GameWorld.state stav = GameWorld.getInstance.map[y * GameWorld.width + x];
                    DrawIndex(stav,x,y);
     
                    if (x == Math.Min(cameraX + viewWidth, GameWorld.width) - 1) { renderOutput += "║"; }
                }

                Buffer.DrawColored(renderOutput, 1, Console.CursorTop, ConsoleColor.Gray, false);
                Buffer.NewLine();
                if (y == Math.Min(cameraY + viewHeight, GameWorld.height) - 1) { Buffer.Draw("╚", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray); for (int i = Math.Max(0, cameraX + 10); i < Math.Min(cameraX + viewWidth, GameWorld.width); i++) { Buffer.Draw("═", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray); } Buffer.Draw("╝", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray); Buffer.NewLine(); }

                renderOutput = "";
            }

            // DEBUG 
            Buffer.Draw("Drawing Y: " + Convert.ToString(Math.Min(cameraY + viewHeight, GameWorld.height)));
            Buffer.NewLine();
            Buffer.Draw("Drawing X: FROM:" + Convert.ToString(Math.Max(0, cameraX + 10)) + " TO: " +  Convert.ToString(Math.Min(cameraX + viewWidth, GameWorld.width)));
            Buffer.NewLine();
            Buffer.DrawColored("Game speed: #y" + Convert.ToString(GameWorld.getInstance.gameSpeed) + "#x ", 0, Console.CursorTop, ConsoleColor.Gray, false, true);
            
    }

        private void DrawIndex(GameWorld.state stav,int x, int y)
        {

            switch(stav)
            {
                case (GameWorld.state.free):
                    {
                        renderOutput += " ";
                        break;
                    }
                case (GameWorld.state.player):
                    {
                        renderOutput += "‡#gP#x‡";
                        break;
                    }
                case (GameWorld.state.movingWall):
                    {
                        oWallMoveable target = GameWorld.getInstance.moveableWallList.Find(i => i.x == x && i.y == y);
                        AddRenderOutput('~', target.color);
                        break;
                    }
                case (GameWorld.state.enemy):
                    {
                        oEnemy target = GameWorld.getInstance.enemyList.Find(i => i.x == x && i.y == y);
                        AddRenderOutput('~', target.color);
                        break;
                    }
                case (GameWorld.state.wall):
                    {
                        oWall target = GameWorld.getInstance.wallList.Find(i => i.x == x && i.y == y);
                        AddRenderOutput('~', target.color);
                        break;
                    }
                case (GameWorld.state.gold):
                    {
                        oGold target = GameWorld.getInstance.goldList.Find(i => i.x == x && i.y == y);

                        int dis = (int)target.distanceToPoint(GameWorld.getInstance.player.x, GameWorld.getInstance.player.y);
                        string str = "Hodnota: " + target.value;

                         if (dis < 5)
                            {
                             Buffer.DrawInsert(str, (int)(x - Math.Max(0, cameraX + 10) -  Math.Round((Convert.ToDouble(str.Length / 2)))) + 1, Console.CursorTop - 1, ConsoleColor.Green);
                            }

                        AddRenderOutput('◎', target.color);
                        break;
                    }

                case (GameWorld.state.item):
                    {
                        GameItem target = GameWorld.getInstance.itemsList.Find(i => i.x == x && i.y == y);

                        int dis = (int)target.distanceToPoint(GameWorld.getInstance.player.x, GameWorld.getInstance.player.y);
                        string str = target.cleanName;

                        if (dis < 5)
                                {
                                    Buffer.DrawColored(target.itemName, (int)(x - Math.Round((Convert.ToDouble(str.Length / 2)))) + 1, Console.CursorTop - 1, ConsoleColor.Gray, true);
                                }

                        AddRenderOutput('I', target.itemColor);
                        break;
                    }
            }                    
        }

        void AddRenderOutput(char znak, ConsoleColor color)
        {
            if (color == ConsoleColor.Green)
            {
                renderOutput += "‡#g" + znak + "#x‡";
            }
            else if (color == ConsoleColor.Red)
            {
                renderOutput += "‡#r" + znak + "#x‡";
            }
            else if (color == ConsoleColor.Yellow)
            {
                renderOutput += "‡#y" + znak + "#x‡";
            }
            else if (color == ConsoleColor.DarkGray)
            {
                renderOutput += "‡#h" + znak + "#x‡";
            }
            else
            {
                renderOutput += znak;
            }
        }


        public void DrawHeader(string text)
        {
            string line = Strings.getInstance.horizontalLine;

            Console.WriteLine(line);
            Console.WriteLine(text);
            Console.WriteLine(line);
        }

        public void DrawMapInfo()
        {
            string line = Strings.getInstance.horizontalLine;

            Console.WriteLine(line);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Souřadnice: (");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(GameWorld.getInstance.player.x);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" ; ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(GameWorld.getInstance.player.y);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(")");
            Console.WriteLine();
            Console.WriteLine(line);
        }

        public void DrawPlayerStats()
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 1);
            int startX = 54;

            Buffer.DrawColored("#y" + GameWorld.getInstance.player.name + "#x (#g" + GameWorld.getInstance.player.level + ")#x (#h" + GameWorld.getInstance.player.xp + "#x/#h" + GameWorld.getInstance.player.max_xp + "#x)",startX,Console.CursorTop,ConsoleColor.Gray,false);
            Buffer.NewLine();
            Console.CursorLeft = startX;
            Buffer.Draw("--------------------------------------------");
            Buffer.NewLine();
            Console.CursorLeft = startX;
            DrawBar(GameWorld.getInstance.player.hp, GameWorld.getInstance.player.max_hp, "Zdraví", ConsoleColor.Red);
            Console.CursorLeft = startX;
            DrawBar(GameWorld.getInstance.player.stamina, GameWorld.getInstance.player.max_stamina, "Výdrž ", ConsoleColor.Green);

            Buffer.Print();
        }

        public void DrawBar(decimal variable, decimal variable_max, string text, ConsoleColor color, char znak = '■', bool newLine = true, int size = 20)
        {
            decimal number = Math.Round((variable / variable_max) * size);

            if (text != "no_text") { Buffer.DrawColored(text + " (#g" + variable + "#x / #g" + variable_max + "#x): ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, true); }

            Buffer.Draw("[", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
            int cl = Console.CursorLeft;
            for (int i = 0; i < number; i++)
            {
                Buffer.Draw(Convert.ToString(znak), Console.CursorLeft, Console.CursorTop, color);
            }
            Buffer.Draw("]", cl + size, Console.CursorTop, ConsoleColor.Gray);

            if (newLine) { Buffer.NewLine(); }
        }

        public void DrawItem()
        {
            Buffer.Clear();
            Buffer.Draw(Strings.getInstance.horizontalLine,Console.CursorLeft,Console.CursorTop,ConsoleColor.Gray);
            Buffer.NewLine();

            Console.ReadKey();

        }

        public enum Outline
        {
            doubleLine,singleLine
        }

        public void drawBox(int x, int y, int width, int height, ConsoleColor color, Outline outline)
        {
            int top = Console.CursorTop;
            buffer buffer = render.Buffer;

            switch(outline)
            {
                case (Outline.doubleLine): { buffer.Draw("╔", x, y, color);  break; }
                case (Outline.singleLine): { buffer.Draw("┌", x, y, color); break; }
            }
    

            for(int i = 1; i < width; i++)
            {
                switch (outline)
                {
                    case (Outline.doubleLine): { buffer.Draw("═", x + i, y, color); break; }
                    case (Outline.singleLine): { buffer.Draw("─", x + i, y, color); break; }
                }              
            }

            switch (outline)
            {
                case (Outline.doubleLine): { buffer.Draw("╗", x + width, y, color);  break; }
                case (Outline.singleLine): { buffer.Draw("┐", x + width, y, color); break; }
            }
           
            Console.CursorLeft = 0;

            for (int i = 1; i < height; i++)
            {
                switch (outline)
                {
                    case (Outline.doubleLine): { buffer.Draw("║", x, y + i, color); break; }
                    case (Outline.singleLine): { buffer.Draw("│", x, y + i, color); break; }
                }
            }

            switch (outline)
            {
                case (Outline.doubleLine): { buffer.Draw("╚", x, y + height, color); break; }
                case (Outline.singleLine): { buffer.Draw("└", x, y + height, color); break; }
            }
           

            for (int i = 1; i < width; i++)
            {
                switch (outline)
                {
                    case (Outline.doubleLine): { buffer.Draw("═", x + i, y + height, color); break; }
                    case (Outline.singleLine): { buffer.Draw("─", x + i, y + height, color); break; }
                }
            }

            switch (outline)
            {
                case (Outline.doubleLine): { buffer.Draw("╝", x + width, y + height, color); break; }
                case (Outline.singleLine): { buffer.Draw("┘", x + width, y + height, color);  break; }
            }
            

            Console.CursorLeft = 0;
            for (int i = 1; i < height; i++)
            {
                switch (outline)
                {
                    case (Outline.doubleLine): { buffer.Draw("║", x + width, y + i, color);  break; }
                    case (Outline.singleLine): { buffer.Draw("│", x + width, y + i, color);  break; }
                }
               
            }

            Console.CursorTop = top;
        }

        public void fps()
        {
            foreach (oWallMoveable wall in GameWorld.getInstance.moveableWallList)
            {
                if (wall.active)
                {
                    if (GameWorld.getInstance.getMap(wall.x + 1, wall.y) != GameWorld.state.wall)
                    {
                        wall.x++;
                    }
                    else
                    {
                        wall.active = false;
                        Structs.Point point = new Structs.Point();
                        point.x = wall.x;
                        point.y = wall.y;

                        ConsolePhysics.getInstance.explode(point, 10, 10);
                    }
                }
          }

            foreach (oWall wall in GameWorld.getInstance.wallList)
            {
                if (wall.active && wall.targetX.Count > 0)
                {
                    if (GameWorld.getInstance.getMap(wall.x + wall.targetX.Peek(), wall.y + wall.targetY.Peek()) != GameWorld.state.wall)
                    {
                        wall.x = wall.targetX.Dequeue();
                        wall.y = wall.targetY.Dequeue();
                    }
                    else
                    {
                        wall.targetX.Clear();
                        wall.targetY.Clear();
                    }

                    if (wall.targetX.Count == 0) { wall.active = false; wall.xx = wall.x; wall.yy = wall.y; }
                }
            }
        }
    }
}
