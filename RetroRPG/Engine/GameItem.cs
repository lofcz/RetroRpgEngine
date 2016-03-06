using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroRPG.GameObjects;

namespace RetroRPG
{
    class GameItem
    {
        const int total_attributes = 4;
        public int[] attributes = new int[total_attributes];      
        string[] attributesNames = {"Život","Poškození","Equiped","Typ předmětu" };
        public string itemName;
        public string cleanName;
        public ConsoleColor itemColor;
        string itemDescription = "";
        public string[] itemOptions;
        public itemActions act;
        public int starMarked = 0;
        public int x, y;
        public bool messageOnPickup = false;
        //string[] itemOptions = {"Nasadit};

        public enum atr
        {
            hp,damage,equiped,type
        };

        public GameItem(string itemName, ConsoleColor itemColor, string itemDescription, oPlayer.ItemsEquiped itemType, itemActions action)
        {      
            for (int i = 0; i < total_attributes; i++)
            {
                attributes[i] = 0;
            }

            this.itemName = itemName;
            this.cleanName = itemName;
            this.itemColor = itemColor;
            this.itemDescription = itemDescription;
            this.attributes[(int)atr.type] = (int)itemType;
            this.x = 0;
            this.y = 0;

            this.act = action;
            switch (action)
            {
               
                case itemActions.basicItem:
                    {
                        itemOptions = new string[4];
                        itemOptions[0] = "{EQUIP_SPECIAL}";
                        itemOptions[1] = "{STAR_SPECIAL}";
                        itemOptions[2] = "Zahodit";
                        itemOptions[3] = "Zničit";
                        break;
                    }
            }

            string str = "";
            bool escape = false;

            for(int i = 0; i < cleanName.Length; i++)
            {
                if (cleanName[i] == '#')
                {
                    escape = true;
                    continue;
                }

                if (!escape) { str += cleanName[i];  } else { escape = false; }

            }

            cleanName = str;
            //System.Windows.Forms.MessageBox.Show(cleanName);
        }

        public void drawItemStats()
        {
            string output = "";
            for (int i = 0; i < total_attributes; i++)
            {
                if (attributes[i] > 0)
                {
                    if (i != (int)GameItem.atr.equiped) // Speciální atributy vyškrteme ze seznamu na vykreslení
                    {
                        output += attributesNames[i] + ":" + attributes[i] + ", ";
                    }
                }
            }

            if (output != "")
            {
                output = output.Remove(output.Length - 2); // Odstraním poslední čárku v řetěztci.
                Render.getInstance.Buffer.Draw(output, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
               // Render.getInstance.Buffer.Print();
            }
         
        }

        public double distanceToPoint(int xx, int yy)
        {
            double distance = -1;

            distance = Math.Round(Math.Sqrt(Math.Pow(Math.Abs(x - xx), 2) + Math.Pow(Math.Abs(y - yy), 2)));
            return distance;
        }

        public void drawItemName()
        {
            string star = "";
            if (starMarked == 0) { star = ""; }
            if (starMarked == 1) { star = " #g▲#x "; }
            if (starMarked == 2) { star = " #r▼#x "; }
            Render.getInstance.Buffer.DrawColored(itemName + star, Console.CursorLeft, Console.CursorTop, itemColor, true);
        }

        public void drawItemDescription()
        {
            Render.getInstance.Buffer.Draw(itemDescription, Console.CursorLeft, Console.CursorTop, ConsoleColor.DarkGray);
        }

        public void drawItemOptions(int horizontalIndex)
        {
            Render.getInstance.Buffer.Draw("   ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);

            for (int i = 0; i < itemOptions.Length; i++)
            {
                string outText = itemOptions[i];

                if (outText == "{EQUIP_SPECIAL}") { if (attributes[(int)GameItem.atr.equiped] == 0) { outText = "Nasadit"; } else { outText = "Sundat"; } }
                if (outText == "{STAR_SPECIAL}") { if (starMarked == 0) { outText = "Oblíbené"; } if (starMarked == 1) { outText = "Odpad"; } if (starMarked == 2) { outText = "Obyčejné"; } }

                if (i == horizontalIndex)
                {
                    Render.getInstance.Buffer.Draw(" [" + outText + "] ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Yellow);
                }
                else
                {
                    Render.getInstance.Buffer.Draw(" [" + outText + "] ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
                }
            }
        }

        public void Equip()
        {
            for (int i = 0; i < total_attributes; i++)
            {
                if (i == (int)atr.hp)
                {
                    GameWorld.getInstance.player.hp += attributes[i];
                    GameWorld.getInstance.player.max_hp += attributes[i];
                }
              
            }
        }

        public void UnEquip()
        {
            for (int i = 0; i < total_attributes; i++)
            {
                if (i == (int)atr.hp)
                {
                    GameWorld.getInstance.player.hp -= attributes[i];
                    GameWorld.getInstance.player.max_hp -= attributes[i];
                }

            }
        }

        public enum itemActions
        {
            basicItem, myItem1
        }

        public void ItemActions(int actionIndex)
        {
            switch(act)
            {
                case itemActions.basicItem:
                    {
                        switch(actionIndex)
                        {
                            // Equip
                            case 0:
                                {
                                    // Equip
                                    if (attributes[(int)GameItem.atr.equiped] == 0 && (GameWorld.getInstance.player.equiped[attributes[(int)GameItem.atr.type]] == false))
                                    {
                                        Equip();
                                        attributes[(int)GameItem.atr.equiped] = 1;
                                        GameWorld.getInstance.player.equiped[attributes[(int)GameItem.atr.type]] = true;
                                    }
                                    // Unequip
                                    else if (attributes[(int)GameItem.atr.equiped] == 1 && (GameWorld.getInstance.player.equiped[attributes[(int)GameItem.atr.type]] == true))
                                    {
                                        UnEquip();
                                        attributes[(int)GameItem.atr.equiped] = 0;
                                        GameWorld.getInstance.player.equiped[attributes[(int)GameItem.atr.type]] = false;
                                    }
                                    break;
                                }

                            // Star
                            case 1:
                                {
                                    if (starMarked < 2) { starMarked++; }
                                    else { starMarked = 0; }
                                    break;
                                }

                            // Drop
                            case 2:
                                {
                                    Inventory.getInstance.itemRemove(this); // TO-DO Respawn item
                                    break;
                                }

                            // Destroy
                            case 3:
                                {
                                    Inventory.getInstance.itemRemove(this);
                                    break;
                                }

                        }

                        break;
                    }

            }
        }

        public void addItem(int x, int y)
        {
            this.x = x;
            this.y = y;

            GameWorld.getInstance.itemsList.Add(this);
        }

    }
}
