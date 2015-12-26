using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroRPG
{
    class Inventory
    {
        public List<GameItem> items;
        private static Inventory inventory;

        Inventory()
        {
    
        }

    public static Inventory getInstance
        {
            get
            {
                if (inventory == null)
                {
                    inventory = new Inventory();
                }

                return inventory;
            }
        }
        
     public void drawInventory()
        {
            int lenght = 0;

            foreach (GameItem item in items)
            {
               // Render.getInstance.Buffer.Draw()
            }
        }

    }
}
