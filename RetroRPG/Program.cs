using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroRPG.Objects;
using System.Diagnostics;
using System.Threading;

namespace RetroRPG
{
    class Program
    { 

        static void Main(string[] args)
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

            GameSettings.getInstance.SetResolution();
            GameLogic.getInstance.Step();
        }
    }
}
