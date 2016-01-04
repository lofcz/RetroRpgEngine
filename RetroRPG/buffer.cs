using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RetroRPG
{
    public class buffer
    {

        public enum type
        {
            common,characterCreation
        };

        public static Dictionary<char, ConsoleColor> Colors;
        public static char SpecialFGChar { get; set; }
        public static char SpecialBGChar { get; set; }
        public static char ResetChar { get; set; }

        private int width;
        private int height;
        private int windowWidth;
        private int windowHeight;
        private SafeFileHandle h;
        private CharInfo[] buf;
        private SmallRect rect;

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern SafeFileHandle CreateFile(
            string fileName,
            [MarshalAs(UnmanagedType.U4)] uint fileAccess,
            [MarshalAs(UnmanagedType.U4)] uint fileShare,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] int flags,
            IntPtr template);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteConsoleOutput(
          SafeFileHandle hConsoleOutput,
          CharInfo[] lpBuffer,
          Coord dwBufferSize,
          Coord dwBufferCoord,
          ref SmallRect lpWriteRegion);

        [StructLayout(LayoutKind.Sequential)]
        public struct Coord
        {
            private short X;
            private short Y;

            public Coord(short X, short Y)
            {
                this.X = X;
                this.Y = Y;
            }
        };

        [StructLayout(LayoutKind.Explicit)]
        public struct CharUnion
        {
            [FieldOffset(0)]
            public char UnicodeChar;
            [FieldOffset(0)]
            public short AsciiChar;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct CharInfo
        {
            [FieldOffset(0)]
            public CharUnion Char;
            [FieldOffset(2)]
            public short Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SmallRect
        {
            private short Left;
            private short Top;
            private short Right;
            private short Bottom;
            public void setDrawCord(short l, short t)
            {
                Left = l;
                Top = t;
            }
            public void setWindowSize(short r, short b)
            {
                Right = r;
                Bottom = b;
            }
        }

        private static bool SetFGColor(char value)
        {
            // look at the next char and make sure we have the color code registered
            if (Colors.ContainsKey(value))
            {
                Console.ForegroundColor = Colors[value];

                return true;
            }
            else if (value == ResetChar)
            {
                Console.ResetColor();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Consctructor class for the buffer. Pass in the width and height you want the buffer to be.
        /// </summary>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        public buffer(int Width, int Height, int wWidth, int wHeight) // Create and fill in a multideminsional list with blank spaces.
        {

            Colors = new Dictionary<char, ConsoleColor>();

            // set some defaults
            SpecialFGChar = '#';
            SpecialBGChar = '¤';
            ResetChar = 'x';

            // add some default colors
            Colors.Add('r', ConsoleColor.Red);
            Colors.Add('h', ConsoleColor.DarkGray);
            Colors.Add('y', ConsoleColor.Yellow);
            Colors.Add('g', ConsoleColor.Green);
            Colors.Add('b', ConsoleColor.Blue);
            Colors.Add('w', ConsoleColor.White);

            if (Width > wWidth || Height > wHeight)
            {
                throw new System.ArgumentException("The buffer width and height can not be greater than the window width and height.");
            }
            h = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
            width = Width;
            height = Height;
            windowWidth = wWidth;
            windowHeight = wHeight;
            buf = new CharInfo[width * height];
            rect = new SmallRect();
            rect.setDrawCord(0, 0);
            rect.setWindowSize((short)windowWidth, (short)windowHeight);
            Clear();
        }
        /// <summary>
        /// This method draws any text to the buffer with given color.
        /// To chance the color, pass in a value above 0. (0 being black text, 15 being white text).
        /// Put in the starting width and height you want the input string to be.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="attribute"></param>
        public void Draw(String str, int Width, int Height, ConsoleColor color) //Draws the image to the buffer
        {
            short parsedAtribute = 0;

            switch (color)
            {
                case ConsoleColor.Black:
                    {

                        break;
                    }

                case ConsoleColor.Gray:
                    {
                        parsedAtribute = 7;
                        break;
                    }

                case ConsoleColor.DarkGray:
                    {
                        parsedAtribute = 8;
                        break;
                    }

                case ConsoleColor.Red:
                    {
                        parsedAtribute = 12;
                        break;
                    }

                case ConsoleColor.Green:
                    {
                        parsedAtribute = 10;
                        break;
                    }

                case ConsoleColor.Yellow:
                    {
                        parsedAtribute = 14;
                        break;
                    }
            }

            if (Width > windowWidth - 1 || Height > windowHeight - 1)
            {
                //throw new System.ArgumentOutOfRangeException();
            }

            if (str != null)
            {
                Char[] temp = str.ToCharArray();
                int tc = 0;
                foreach (Char le in temp)
                {
                    bool cont = true;
                    short special = SpecialChars(le); // BYTE
                    if (special != 0) { buf[(Width + tc) + (Height * width)].Char.AsciiChar = special; cont = false; }


                    if (cont)
                    {
                        buf[(Width + tc) + (Height * width)].Char.AsciiChar = (byte)le; //Height * width is to get to the correct spot (since this array is not two dimensions).
                    }

                    if (parsedAtribute != 0)
                        buf[(Width + tc) + (Height * width)].Attributes = parsedAtribute;
                    tc++;
                }
            }

            if (str.Length > 0)
            {
                try
                {
                    Console.CursorLeft += str.Length;
                }
                catch
                {

                }
            }


        }

        public void Draw(String str, ConsoleColor color = ConsoleColor.Gray) //Draws the image to the buffer
        {
            short parsedAtribute = 0;
            int Width = Console.CursorLeft;
            int Height = Console.CursorTop;

            switch (color)
            {
                case ConsoleColor.Black:
                    {

                        break;
                    }

                case ConsoleColor.Gray:
                    {
                        parsedAtribute = 7;
                        break;
                    }

                case ConsoleColor.DarkGray:
                    {
                        parsedAtribute = 8;
                        break;
                    }

                case ConsoleColor.Red:
                    {
                        parsedAtribute = 12;
                        break;
                    }

                case ConsoleColor.Green:
                    {
                        parsedAtribute = 10;
                        break;
                    }

                case ConsoleColor.Yellow:
                    {
                        parsedAtribute = 14;
                        break;
                    }
            }

            if (Width > windowWidth - 1 || Height > windowHeight - 1)
            {
                //throw new System.ArgumentOutOfRangeException();
            }

            if (str != null)
            {
                Char[] temp = str.ToCharArray();
                int tc = 0;
                foreach (Char le in temp)
                {
                    bool cont = true;
                    short special = SpecialChars(le); // BYTE
                    if (special != 0) { buf[(Width + tc) + (Height * width)].Char.AsciiChar = special; cont = false; }


                    if (cont)
                    {
                        buf[(Width + tc) + (Height * width)].Char.AsciiChar = (byte)le; //Height * width is to get to the correct spot (since this array is not two dimensions).
                    }

                    if (parsedAtribute != 0)
                        buf[(Width + tc) + (Height * width)].Attributes = parsedAtribute;
                    tc++;
                }
            }

            if (str.Length > 0)
            {
                try
                {
                    Console.CursorLeft += str.Length;
                }
                catch
                {
                }
            }


        }




        // ***********************************************************************************************************************************
        public void DrawColored(String str, int Width, int Height, ConsoleColor color, bool insert, bool newLine = false, type type = type.common, int typeValue = 0) //Draws the image to the buffer
        {
            if (type == type.characterCreation && typeValue != 0)
            {
                string brake = "";
                if (typeValue < 10) {  brake = "  "; } else { brake = " "; }

                if (typeValue >= 11) { str = str + "#y" + typeValue + "#x" + brake + "(#g+++#x)"; }
                else if (typeValue == 10) { str = str + "#y" + typeValue + "#x" + brake + "(#g++#x)"; }
                else if (typeValue == 9) { str = str + "#y" + typeValue + "#x" + brake + "(#g+#x)"; }
                else if (typeValue == 8) { str = str + "#y" + typeValue + brake + "#x"; }
                else if (typeValue == 7) { str = str + "#y" + typeValue + "#x" + brake +  "(#r-#x)"; }
                else if (typeValue == 6) { str = str + "#y" + typeValue + "#x" + brake + "(#r--#x)"; }
                else { str = str + "#y" + typeValue + "#x" + brake +  "(#r+++#x)"; }
            }

            short parsedAtribute = 0;
            short parsedAtribute2 = 0;

            switch (color)
            {
                case ConsoleColor.Black:
                    {

                        break;
                    }

                case ConsoleColor.Gray:
                    {
                        parsedAtribute = 7;
                        break;
                    }

                case ConsoleColor.DarkGray:
                    {
                        parsedAtribute = 8;
                        break;
                    }

                case ConsoleColor.Red:
                    {
                        parsedAtribute = 12;
                        break;
                    }

                case ConsoleColor.Green:
                    {
                        parsedAtribute = 10;
                        break;
                    }

                case ConsoleColor.Yellow:
                    {
                        parsedAtribute = 14;
                        break;
                    }
            }

            if (Width > windowWidth - 1 || Height > windowHeight - 1)
            {
                //throw new System.ArgumentOutOfRangeException();
            }
            int back = 0; // Počet znaků o které vrátíme kurzor po dokončení výpisu

            if (str != null)
            {
                Char[] temp = str.ToCharArray();
                int tc = 0;
                bool isSpecialOn = false;
                bool escaping = false;
                int CurrentChar = 0;
                string strBackup = str;
                char escapeChar = '\\';

                for (int i = 0; i < temp.Count(); i++)               //  foreach (Char i in temp)
                {
                    bool cont = true;
                    bool specialColor = false;

                    if (temp[i] == '\n')  //◙
                    {            
                       NewLine();
                       CurrentChar = 0;
                       Height++;

                        DrawColored(strBackup.Remove(0, 1), 0, Height, color, false);
                        break;
                    }                   
                    else
                    {

                        if (temp[i] == SpecialFGChar)
                        {
                            strBackup = strBackup.Remove(0, 1);

                            if (SetFGColor(temp[i + 1]))
                            {
                                i += 2;
                                back += 2;
                                isSpecialOn = !isSpecialOn;
                                strBackup = strBackup.Remove(0, 1);


                                switch (Console.ForegroundColor)
                                {
                                    case ConsoleColor.Black:
                                        {

                                            break;
                                        }

                                    case ConsoleColor.Gray:
                                        {
                                            parsedAtribute2 = 7;
                                            break;
                                        }

                                    case ConsoleColor.DarkGray:
                                        {
                                            parsedAtribute2 = 8;
                                            break;
                                        }

                                    case ConsoleColor.Red:
                                        {
                                            parsedAtribute2 = 12;
                                            break;
                                        }

                                    case ConsoleColor.Green:
                                        {
                                            parsedAtribute2 = 10;
                                            break;
                                        }

                                    case ConsoleColor.Yellow:
                                        {
                                            parsedAtribute2 = 14;
                                            break;
                                        }
                                }
                            }
                        }

                        if (isSpecialOn) { specialColor = true; }

                        short special = SpecialChars(temp[i]);
                        if (special != 0) { buf[(Width + tc) + (Height * width)].Char.UnicodeChar = (char)special; cont = false; }

                        if (cont)
                        {
                            buf[(Width + tc) + (Height * width)].Char.UnicodeChar = (char)temp[i]; //Height * width is to get to the correct spot (since this array is not two dimensions).
                        }

                        if (!specialColor)
                        {
                            if (parsedAtribute != 0)
                                buf[(Width + tc) + (Height * width)].Attributes = parsedAtribute;
                        }
                        else
                        {
                            buf[(Width + tc) + (Height * width)].Attributes = parsedAtribute2;
                        }
                        tc++;

                        if (CurrentChar >= 100)
                        {
                     //       NewLine();
                       //     CurrentChar = 0;
                       //     Height++;
                          //  DrawColored(strBackup.Remove(0, 1), Width, Height, color, insert);

                        }

                        CurrentChar++;
                        strBackup = strBackup.Remove(0, 1);
                    }



                }

                if (insert)
                {
                    try
                    {
                        if (str.Length > 0)
                        {
                            Console.CursorLeft += str.Length;
                        }
                        Console.CursorLeft -= back;
                    }
                    catch
                    {

                    }
                }

            if(newLine)
                {
                    NewLine();
                }
            }




        }
        // ***********************************************************************************************************************************







        public void DrawInsert(String str, int Width, int Height, ConsoleColor color) //Draws the image to the buffer
        {
            short parsedAtribute = 0;

            switch (color)
            {
                case ConsoleColor.Black:
                    {

                        break;
                    }

                case ConsoleColor.Gray:
                    {
                        parsedAtribute = 7;
                        break;
                    }

                case ConsoleColor.Red:
                    {
                        parsedAtribute = 12;
                        break;
                    }

                case ConsoleColor.Green:
                    {
                        parsedAtribute = 10;
                        break;
                    }

                case ConsoleColor.Yellow:
                    {
                        parsedAtribute = 14;
                        break;
                    }
            }

            if (Width > windowWidth - 1 || Height > windowHeight - 1)
            {
                //throw new System.ArgumentOutOfRangeException();
            }

            if (str != null)
            {
                Char[] temp = str.ToCharArray();
                int tc = 0;
                foreach (Char le in temp)
                {
                    bool cont = true;

                    short special = SpecialChars(le); // BYTE
                    if (special != 0) { buf[(Width + tc) + (Height * width)].Char.AsciiChar = special; cont = false; }

                    if (cont)
                    {
                        buf[(Width + tc) + (Height * width)].Char.AsciiChar = (byte)le; //Height * width is to get to the correct spot (since this array is not two dimensions).
                    }

                    if (parsedAtribute != 0)
                        buf[(Width + tc) + (Height * width)].Attributes = parsedAtribute;
                    tc++;
                }
            }



        }

        public void DrawInt(String str, int Width, int Height, short pa) //Draws the image to the buffer
        {

            short parsedAtribute = pa;



            if (Width > windowWidth - 1 || Height > windowHeight - 1)
            {
                //throw new System.ArgumentOutOfRangeException();
            }

            if (str != null)
            {
                Char[] temp = str.ToCharArray();
                int tc = 0;
                foreach (Char le in temp)
                {
                    bool cont = true;

                    short special = SpecialChars(le); // BYTE
                    if (special != 0) { buf[(Width + tc) + (Height * width)].Char.AsciiChar = special; cont = false; }

                    if (cont)
                    {
                        buf[(Width + tc) + (Height * width)].Char.AsciiChar = (byte)le; //Height * width is to get to the correct spot (since this array is not two dimensions).
                    }

                    if (parsedAtribute != 0)
                        buf[(Width + tc) + (Height * width)].Attributes = parsedAtribute;
                    tc++;
                }
            }



        }

        /// <summary>
        /// Prints new line to the buffer
        /// </summary>
        /// <param name="times">Number of new lines</param>
        public void NewLine(int times = 1)
        {
            Console.CursorTop += times;
            Console.CursorLeft = 0;
        }
        /// <summary>
        /// Prints the buffer to the screen.
        /// </summary>
        public void Print() //Paint the image
        {
            if (!h.IsInvalid)
            {

                bool b = WriteConsoleOutput(h, buf, new Coord((short)width, (short)height), new Coord((short)0, (short)0), ref rect);
            }
        }
        /// <summary>
        /// Clears the buffer and resets all character values back to 32, and attribute values to 1.
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < buf.Length; i++)
            {
                buf[i].Attributes = 1;
                buf[i].Char.AsciiChar = 32;
            }
        }
        /// <summary>
        /// Pass in a buffer to change the current buffer.
        /// </summary>
        /// <param name="b"></param>
        public void setBuf(CharInfo[] b)
        {
            if (b == null)
            {
                throw new System.ArgumentNullException();
            }

            buf = b;
        }

        /// <summary>
        /// Set the x and y cordnants where you wish to draw your buffered image.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void setDrawCord(short x, short y)
        {
            rect.setDrawCord(x, y);
        }

        /// <summary>
        /// Clear the designated row and make all attribues = 1.
        /// </summary>
        /// <param name="row"></param>
        public void clearRow(int row)
        {
            for (int i = (row * width); i < ((row * width + width)); i++)
            {
                if (row > windowHeight - 1)
                {
                    throw new System.ArgumentOutOfRangeException();
                }
                buf[i].Attributes = 0;
                buf[i].Char.AsciiChar = 32;
            }
        }

        /// <summary>
        /// Clear the designated column and make all attribues = 1.
        /// </summary>
        /// <param name="col"></param>
        public void clearColumn(int col)
        {
            if (col > windowWidth - 1)
            {
                throw new System.ArgumentOutOfRangeException();
            }
            for (int i = col; i < windowHeight * windowWidth; i += windowWidth)
            {
                buf[i].Attributes = 0;
                buf[i].Char.AsciiChar = 32;
            }
        }

        /// <summary>
        /// This function return the character and attribute at given location.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>
        /// byte character
        /// byte attribute
        /// </returns>
        public KeyValuePair<byte, byte> getCharAt(int x, int y)
        {
            if (x > windowWidth || y > windowHeight)
            {
                throw new System.ArgumentOutOfRangeException();
            }
            return new KeyValuePair<byte, byte>((byte)buf[((y * width + x))].Char.AsciiChar, (byte)buf[((y * width + x))].Attributes);
        }

        private short SpecialChars(char znak)
        {

            if (znak == '╔') { return 201; }
            if (znak == '═') { return 205; }
            if (znak == '╗') { return 187; }
            if (znak == '║') { return 186; }
            if (znak == '╚') { return 200; }
            if (znak == '╝') { return 188; }
            if (znak == '◎') { return 7; }
            if (znak == '░') { return 176; }
            if (znak == 'č') { return 159; }
            if (znak == 'í') { return 161; }
            if (znak == 'á') { return 160; }
            if (znak == '█') { return 219; }
            if (znak == 'ý') { return 236; }
            if (znak == 'ž') { return 167; }
            if (znak == 'Ž') { return 166; }
            if (znak == 'š') { return 231; }
            if (znak == 'ď') { return 212; }
            if (znak == 'ě') { return 216; }
            if (znak == 'é') { return 130; }
            if (znak == 'ř') { return 253; }
            if (znak == 'ň') { return 229; }
            if (znak == '•') { return 7; }
            if (znak == ' ') { return 255; }
            if (znak == 'ů') { return 133; }
            if (znak == 'Š') { return 230; }
            if (znak == 'Ú') { return 233; }

            return 0;
        }
    }
}
