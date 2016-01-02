using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroRpgAsciiDesigner
{
    class Program
    {
      
        [STAThread]
        public static void Main()
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "ASCII Art soubory (*.txt*)|*.txt*";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = true;

            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                string fileName = choofdlog.FileName;
                StreamReader sr = new StreamReader(fileName);
                string line;
                string[] parsed = new string[File.ReadLines(fileName).Count()];
                int i = 0;

                while ((line = sr.ReadLine()) != null)
                {
                    parsed[i] = line;
                    i++;
                }

                sr.Close();


                while (true)
                  {
                    Console.Clear();

                    foreach(string str in parsed)
                    {
                        Console.WriteLine(str);
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Offset: ");
                    int offset = int.Parse(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.Gray;

                    if (offset != 666)
                    {
                        for (int j = 0; j < parsed.Length; j++)
                        {
                            if (offset > 0)
                            {
                                for (int k = 0; k < offset; k++)
                                {
                                    parsed[j] = " " + parsed[j];
                                }
                            }
                            else
                            {
                                for (int k = 0; k < Math.Abs(offset); k++)
                                {
                                    parsed[j] = parsed[j].Remove(0, 1);
                                }
                            }
                        }
                    }
                    else
                    {
                        StreamWriter sw = new StreamWriter(fileName);

                        foreach (string str in parsed)
                        {
                            sw.WriteLine(str);
                        }

                        sw.Close();
                    }


                    
                  } 
            }

            Console.ReadKey();
        }

 
    }
}
