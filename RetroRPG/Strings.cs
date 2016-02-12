using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroRPG
{
    class Strings
    {
        // *********** Constants *************
        public string horizontalLine = "+-------------------------------------------------------------------------------------------------+";
  
        private static Strings strings;

        public static Strings getInstance
        {
            get
            {
                if (strings == null)
                {
                    strings = new Strings();
                }

                return strings;
            }
        }
    }
}
