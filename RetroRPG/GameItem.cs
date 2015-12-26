using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroRPG
{
    class GameItem
    {
        public int[] attributes;
        const int total_attributes = 10;

        public enum atr
        {

        };

        public GameItem()
        {
            for (int i = 0; i < total_attributes; i++)
            {
                attributes[i] = 0;
            }
        }


    }
}
