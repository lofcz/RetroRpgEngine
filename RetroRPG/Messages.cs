using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RetroRPG
{
    // Třída pro prefabriky herních zpráv
    class Messages
    {
        buffer buffer = Render.getInstance.Buffer;
        private static Messages messages;
        private Messages()
        {

        }

        public static Messages getInstance
        {
            get
            {
                if (messages == null)
                {
                    messages = new Messages();
                }

                return messages;
            }
        }

        public void rainMessage(string text, int effectSpeed, int speedChange, bool useSuppres, string continueMessage)
        {
            int animationPart = 0;
            bool nextPart = true;

            Random random = new Random();
            Console.CursorVisible = false;
            List<Structs.SimpleObject> objectList = new List<Structs.SimpleObject>();
            List<Structs.SimpleObject> newList = new List<Structs.SimpleObject>();

            int centerPoint = 50-(text.Length / 2);
            bool animationIsRunning = true;

            for (int i = 0; i < text.Length; i++)
            {
                Structs.SimpleObject simpleObject = new Structs.SimpleObject();
                simpleObject.X = centerPoint+i;
                simpleObject.Y = random.Next(1,5);
                simpleObject.symbol = text[i];
                simpleObject.color = ConsoleColor.Yellow;
                simpleObject.randomSymbol = Convert.ToChar(random.Next(40, 90));
                simpleObject.usingRandomSymbol = true;
                simpleObject.startX = simpleObject.X;
                simpleObject.visible = true;

                objectList.Add(simpleObject);
            }

            int step = 0;

            while (animationIsRunning)
            {
                nextPart = true;
                buffer.Clear();
                newList.Clear();

                if (animationPart == 0)
                {
                    foreach (Structs.SimpleObject so in objectList)
                    {
                        Structs.SimpleObject iteratedObject = so;

                        if (iteratedObject.Y < 15)
                        {
                            //newObject.y++;      
                            iteratedObject.Y++; //= iteratedObject.y + 
                            nextPart = false;
                        }
                        else
                        {
                            iteratedObject.usingRandomSymbol = false;
                        }

                        newList.Add(iteratedObject);
                    }

                    for (int i = 0; i < newList.Count; i++)
                    {
                        newList[i].drawSelf();
                        objectList.RemoveAt(i);
                        objectList.Add(newList[i]);
                    }

                    if (effectSpeed + speedChange > 1)
                    {
                        effectSpeed += speedChange;
                    }

                    //buffer.DrawColored(Strings.getInstance.horizontalLine, 0, 16, ConsoleColor.Gray, true);

                    buffer.Print();
                    Thread.Sleep(effectSpeed);
                    step++;

                    if (nextPart)
                    {
                        animationPart = 1;
                        newList.Clear();

                        foreach (Structs.SimpleObject so in objectList)
                        {
                            Structs.SimpleObject iteratedObject = so;

                           
                                iteratedObject.Y+= random.Next(1,4); 

                            newList.Add(iteratedObject);
                        }

                        for (int i = 0; i < newList.Count; i++)
                        {

                            newList[i].drawSelf();
                            objectList.RemoveAt(i);
                            objectList.Add(newList[i]);
                        }

                        newList.Clear();
                        Thread.Sleep(500+(text.Length*4));
                    }
                }

                if (animationPart == 1)
                {
                    foreach (Structs.SimpleObject so in objectList)
                    {
                        Structs.SimpleObject iteratedObject = so;

                        if (iteratedObject.Y < 30)
                        {
                            //newObject.y++;      
                            nextPart = false;
                            iteratedObject.Y++; //= iteratedObject.y + 

                            if (iteratedObject.X == iteratedObject.startX)
                            {
                                if (random.Next(1, 3) == 1)
                                {
                                    iteratedObject.X++;
                                }
                                else
                                {
                                    iteratedObject.X--;
                                }
                            }
                            else if (iteratedObject.X > iteratedObject.startX)
                            {
                                if (random.Next(1, 5) == 1)
                                {
                                    iteratedObject.X++;
                                }

                            }
                            else if (iteratedObject.X < iteratedObject.startX)
                            {
                                if (random.Next(1, 5) == 1)
                                {
                                    iteratedObject.X--;
                                }

                            }
                        }
                        else
                        {
                            iteratedObject.visible = false;
                        }

                        newList.Add(iteratedObject);
                    }

                    for (int i = 0; i < newList.Count; i++)
                    {

                        if (newList[i].visible) { newList[i].drawSelf(); }
                        objectList.RemoveAt(i);
                        objectList.Add(newList[i]);
                    }

                    buffer.Print();
                    Thread.Sleep(effectSpeed);
                    step++;

                    if (nextPart)
                    {
                        animationPart = 2;
                    }
                }
                if (animationPart == 2)
                {
                    bool animatingExit = true;
                    string animationOut = "";
                    string animatedText = continueMessage;


                    int atChar = 0;
                    while (animatingExit)
                    {
                        atChar++;
                        for (int i = 0; i < atChar; i++)
                        {
                            buffer.DrawColored(animatedText[i].ToString(), 50-(atChar/2)+i, 12, ConsoleColor.Yellow, false);
                        }

                        Thread.Sleep(15);
                        buffer.Print();

                        if (atChar >= animatedText.Length)
                        {
                            Console.ReadKey(true);
                            animationIsRunning = false;
                            animatingExit = false;
                        }

                    }

                   
                }
            }

            Console.CursorVisible = true;
            Console.SetCursorPosition(0, 0);
        }

        public void basicMessage(string text, string continueText)
        {
            buffer.Clear();
            buffer.DrawColored(text, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false);
            buffer.NewLine(2);
            buffer.DrawColored(continueText, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false);
            buffer.Print();
            Console.ReadKey(true);

            Console.SetCursorPosition(0, 0);
        }
    }
}
