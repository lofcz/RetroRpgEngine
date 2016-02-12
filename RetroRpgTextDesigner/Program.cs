using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroRpgTextDesigner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetBufferSize(100, 40);
            Console.SetWindowSize(100, 25);
            Console.Title = "RetroRPG Engine - Text designer";
            StreamWriter sw = new StreamWriter("output.txt");
            bool typing = true;
            string output = "";
            bool insertColorflag = false;
            ConsoleColor lastColor = Console.ForegroundColor;

            while(typing)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                int xx = Console.CursorLeft;
                int yy = Console.CursorTop;


                if (key.Key != ConsoleKey.Tab)
                {
                    if (key.Key == ConsoleKey.Tab)
                    {
                        typing = false;
                    }
                    else if (key.Key == ConsoleKey.UpArrow)
                    {
                        xx--;
                        Console.ForegroundColor++;
                        insertColorflag = true;
                       
                    }
                    else if (key.Key == ConsoleKey.DownArrow)
                    {
                        xx--;
                        Console.ForegroundColor--;
                        insertColorflag = true;
                        
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        xx = 0;
                        yy++;
                        Console.CursorLeft = 0;
                        Console.CursorTop++;
                        output = output + @"\n";
                    }
                    else if (key.Key == ConsoleKey.Backspace)
                    {
                        if (output.Length > 0)
                        {
                            // Normální backspace
                            if (Console.CursorLeft > 0)
                            {
                                Console.CursorLeft--;
                                xx--;
                                Console.Write(" ");

                                output = output.Remove(output.Length - 1);


                                try
                                {
                                    if (output[output.Length - 1] == '#') { output = output.Remove(output.Length - 4); }
                                }
                                catch { };
                            }
                            // Smazat řádek
                            else
                            {
                                xx = 30;
                                output = output.Remove(output.Length - 2);
                                Console.Write(" ");
                                Console.CursorTop--;
                                Console.CursorLeft = xx;
                            }
                        }
                    }
                    else
                    {
                        if (insertColorflag)
                        {
                            switch (Console.ForegroundColor)
                            {
                                case ConsoleColor.DarkGray:
                                    {
                                        output += "#h";
                                        break;
                                    }
                                case ConsoleColor.Cyan:
                                    {
                                        output += "#c";
                                        break;
                                    }
                                case ConsoleColor.Green:
                                    {
                                        output += "#g";
                                        break;
                                    }
                                case ConsoleColor.Blue:
                                    {
                                        output += "#b";
                                        break;
                                    }
                                case ConsoleColor.Red:
                                    {
                                        output += "#r";
                                        break;
                                    }
                                case ConsoleColor.Yellow:
                                    {
                                        output += "#y";
                                        break;
                                    }
                                case ConsoleColor.DarkGreen:
                                    {
                                        output += "#p";
                                        break;
                                    }
                                case ConsoleColor.Magenta:
                                    {
                                        output += "#m";
                                        break;
                                    }
                            }
                        }
                        if (lastColor != Console.ForegroundColor) { if (output[output.Length - 2] != '#') { output += "#x"; } lastColor = Console.ForegroundColor; }

                        output += key.KeyChar;

                      
                        if (insertColorflag) { insertColorflag = false; }     
                          
                        Console.Write(key.KeyChar);
                    }

                    Console.SetCursorPosition(0, 10);
                    Console.WriteLine("[Aktuální barva]");
                    ConsoleColor ccColor = Console.ForegroundColor;

                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine();
                    Console.Write("Hotkeys: ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("CTRL +");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("[#]");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(" - 0");
                    Console.WriteLine();

                    Console.ForegroundColor = ccColor;
                    //MessageBox.Show(output);
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter) { xx++; }
                    Console.SetCursorPosition(xx, yy);
                }
                else
                {
                    typing = false;
                }
            }
            sw.WriteLine("This document was generated by RetroRPG Engine - Text designer tool.\nText below is compactible with RetroRPG Engine Colorflag system v1.1");
            sw.WriteLine("Parsed text:");
            sw.WriteLine();
            sw.WriteLine(output);
            sw.Close();
            
        }
    }
}
