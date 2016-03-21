using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroRPG.Engine
{
    public class LogItem
    {
        public enum LogPrefix
        {
            standard, system, achievment, marquee
        }

        public LogPrefix logPrefix { get; set; }
        public string text { get; set; }
        public int time { get; set; }
        public int life { get; set; }
        public bool dead { get; set; }
        public ConsoleColor color { get; set; }

        public LogItem(LogPrefix logPrefix, string text, int life, ConsoleColor color)
        {
            this.logPrefix = logPrefix;
            this.text = text;
            this.life = life;
            this.color = color;
        }

        public void DecreaseLife()
        {
            if (life != -1)
            {
                life--;

                if (life == 0)
                {
                    dead = true;
                }
            }          
        }
    }
}
