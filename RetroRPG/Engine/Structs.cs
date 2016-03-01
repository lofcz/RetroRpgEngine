using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroRPG
{ 
    // Třída pro struktury
    static public class Structs
    {
        public struct Point
        {
            public int x;
            public int y;
        };

        public struct SimpleObject
        {
             int x;
             int y;

            public int X
            {
                get
                {
                    return x;
                }
                set
                {
                    x = value;
                }
            }

            public int Y
            {
                get
                {
                    return y;
                }
                set
                {
                    y = value;
                }
            }


            public char symbol;
            public char randomSymbol;
            public ConsoleColor color;
            public bool usingRandomSymbol;
            public int startX;
            public int startY;
            public bool visible;

            public void drawSelf()
            {
                if (!usingRandomSymbol)
                {
                    Render.getInstance.Buffer.DrawColored(symbol.ToString(), x, y, color, true);
                }
                else
                {
                    Render.getInstance.Buffer.DrawColored(randomSymbol.ToString(), x, y, color, true);
                }
            }
        };
    }
}
