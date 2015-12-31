using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroRPG
{
    // Třída pro crafting herních předmětů
    class Crafting
    {
        private static Crafting crafting;
        private Crafting() { }

        public static Crafting getInstance
        {
            get
            {
                if (crafting == null)
                {
                    crafting = new Crafting();
                }

                return crafting;
            }
        }



    }
}
