using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroRPG
{
    // Třída pro nastavení hry
    class GameSettings
    {
        private static GameSettings gameSettings;
        private GameSettings() { }

        public static GameSettings getInstance
        {
            get
            {
                if (gameSettings == null)
                {
                    gameSettings = new GameSettings();
                }

                return gameSettings;
            }
        }

        public void SetResolution()
        {
            Console.Title = "RetroRPG";
            Console.SetWindowSize(100, 28);
            Console.SetBufferSize(100, 40);
        }
    }
}
