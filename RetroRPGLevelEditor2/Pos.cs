using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroRPGLevelEditor2
{
    class Pos
    {
        public int x;
        public int y;
        public string text;

        public Pos(int x, int y, string text)
        {
            this.x = x;
            this.y = y;
            this.text = text;
        }

        public override string ToString()
        {
            return text;
        }
    }
}
