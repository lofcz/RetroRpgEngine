using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroRPG
{
    // Třída pro efekty s výsledky metod třídy Render.cs
    class ConsolePhysics
    {
        private static ConsolePhysics cp;
        private ConsolePhysics() { }

        public static ConsolePhysics getInstance
        {
            get
            {
                if (cp == null)
                {
                    cp = new ConsolePhysics();
                }

                return cp;
            }
        }

        public double getAngle(int x1, int y1, int x2, int y2, int roundTo = -1)
        {
            float xDiff = x2 - x1;
            float yDiff = y2 - y1;

            double result = Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;

            if (roundTo != -1)
            {
                result = (result % roundTo) * roundTo;
            }
            //MessageBox.Show(Convert.ToString(result));
            return result;
        }

        public void explode(Structs.Point point, int radius, int cycles)
        {
            // Vybereme si instance, se kterými budeme pracovat
            int currentRadius = 0;

            // Provedeme explozi
         
                foreach (oWall wall in GameWorld.getInstance.wallList)
                {
                currentRadius = 5;

                    if (wall.distanceToPoint(point.x, point.y) <= currentRadius)
                    {
                    for (int i = 0; i < cycles; i++)
                    {
                        // Máme vybranou zeď, spočítáme úhel plánovaného dopadu
                        double angle = getAngle(point.x, point.y, wall.x, wall.y);
                        wall.active = true;

                        // Pohneme zdí
                        switch(Convert.ToInt32(angle))
                        {
                            case 0:
                                {
                                    wall.xx = wall.x + i;
                                    wall.yy = wall.y;
                                    wall.addPositionToQueue();
                                    break;
                                }
                            case -45:
                                {
                                    if (i % 2 == 0)
                                    {
                                        wall.xx = wall.x + i;
                                        wall.yy = wall.y + i;
                                    }
          
                                    wall.addPositionToQueue();
                                    break;
                                }
                            case 45:
                                {
                                    if (i % 2 == 0)
                                    {
                                        wall.xx = wall.x + i;
                                        wall.yy = wall.y - i;
                                    }
                                    wall.addPositionToQueue();
                                    break;
                                }
                        }
                        if (currentRadius < radius) { currentRadius++; }
                    }

                   
                }

               
            }
        }
    }
}
