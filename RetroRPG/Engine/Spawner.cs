using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroRPG.Engine
{
    // Třída pro spawnování herních entit
    class Spawner
    {
        private static Spawner spawner;
        private Spawner() { }
        Random random = new Random();

        public static Spawner getInstance
        {
            get
            {
                if (spawner == null)
                {
                    spawner = new Spawner();
                }

                return spawner;
            }
        }

        public void spawnItem(int x, int y, GameItem item, bool messageOnPickup)
        {
            item.messageOnPickup = messageOnPickup;
            item.addItem(x, y);
        }

    }
}
