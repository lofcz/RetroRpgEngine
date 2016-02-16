using RetroRPG.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroRPG
{
    // Třída pro vytvoření postavy
    class CharacterCreation
    {

        private static CharacterCreation character;
        private CharacterCreation() { }

        public static CharacterCreation getInstance
        {
            get
            {
                if (character == null)
                {
                    character = new CharacterCreation();
                }

                return character;
            }
        }

        buffer buffer = Render.getInstance.Buffer;

        public enum classes
        {
            barbarian, mage, warrior, monk, thief
        };

        /// <summary>
        /// Shows a list of current classes and force player to pick one.
        /// </summary>
        public void GetPlayerClass(int index = 0)
        {
            bool choosing = true;
            int choosedIndex = index;
            string[] items = { "Barbar", "Mág", "Válečník", "Mnich", "Zloděj" };
            string[] itemsDescs =
            {

            };

            while (choosing)
            {
                Render.getInstance.Buffer.Clear();
                Console.SetCursorPosition(0, 0);


                buffer.Draw(Strings.getInstance.horizontalLine, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
                buffer.NewLine(2);
                // INTRO HERE
                switch (choosedIndex)
                {
                    case 0:
                        {
                            buffer.DrawColored("+- Barbar -+", Console.CursorLeft + ((Console.WindowWidth / 2) - "+- Barbar +-".ToString().Length / 2) +3, Console.CursorTop - 1, ConsoleColor.Red, true);
                            Console.CursorTop = 3;
                            Parser.getInstance.parseImage(ResourceTree.graphicsCharCreation + "/barbarian.txt", true, ConsoleColor.Gray, Parser.Effects.none);
                            break;
                        }

                    case 1:
                        {
                            buffer.DrawColored("#c+- Mág -+#x ", Console.CursorLeft + ((Console.WindowWidth / 2) - "#c+- Mág -+#x ".ToString().Length / 2) +4, Console.CursorTop - 1, ConsoleColor.Gray, false);
                            Console.CursorTop = 3;
                            Parser.getInstance.parseImage(ResourceTree.graphicsCharCreation + "/mage.txt", true, ConsoleColor.Gray, Parser.Effects.none);
                            break;
                        }
                    case 2:
                        {
                            buffer.DrawColored("#y+- Válečník -+#x ", Console.CursorLeft + ((Console.WindowWidth / 2) - "#y+- Válečník -+#x ".ToString().Length / 2) + 2, Console.CursorTop - 1, ConsoleColor.Gray, false);
                            Console.CursorTop = 2;
                            Parser.getInstance.parseImage(ResourceTree.graphicsCharCreation + "/warrior.txt", true, ConsoleColor.Gray, Parser.Effects.none);
                            break;
                        }
                    case 3:
                        {
                            buffer.DrawColored("#g+- Mnich -+#x ", Console.CursorLeft + ((Console.WindowWidth / 2) - "#g+- Mnich -+#x ".ToString().Length / 2) + 2, Console.CursorTop - 1, ConsoleColor.Gray, false);
                            Console.CursorTop = 2;
                            Parser.getInstance.parseImage(ResourceTree.graphicsCharCreation + "/priest.txt", true, ConsoleColor.Gray, Parser.Effects.none);
                            break;
                        }
                    case 4:
                        {
                            buffer.DrawColored("#m+- Zloděj -+#x ", Console.CursorLeft + ((Console.WindowWidth / 2) - "#m+- Zloděj -+#x ".ToString().Length / 2) + 2, Console.CursorTop - 1, ConsoleColor.Gray, false);
                            Console.CursorTop = 2;
                            Parser.getInstance.parseImage(ResourceTree.graphicsCharCreation + "/thief.txt", true, ConsoleColor.Gray, Parser.Effects.none);
                            break;
                        }
                }
                // /INTRO
                Console.CursorLeft = 0;
                buffer.Draw(Strings.getInstance.horizontalLine, Console.CursorLeft, 14, ConsoleColor.Gray);
                Console.CursorTop = 14;
                buffer.NewLine();
                // CHARACTER STATS
                switch (choosedIndex)
                {
                    case 0:
                        {
                            buffer.DrawColored("Už stovky let podnikají barbaři z #hMlžných hor#x nájezdy na království. Válečný řev, který je\npředchází na míle daleko je postrachem všech vesničanů v okolí. Barbaři jsou divocí válečnící,\nkteří neznají strach a bojiště ovládají zbraněmi děsivých velikostí - od #hnordických seker#x, přes\n#hobouruční meče#x, až po #hobří kyje#x. V boji získávají #rzuřivost#x, díky které mohou provádět mocná komba.\nDíky vysoké konstituci pasivně #rsnižují#x přijaté poškození a mají zvýšenou odolnost proti nemocím.", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, true);
                            break;
                        }

                    case 1:
                        {
                            buffer.DrawColored("Mýty opředená skupina vzdělanců, posouvající čas i prostor do nových dimenzí. Sídlí na\n#hBouřném ostrovu#x, který od zbytku světa odděluje magická bariéra. Mágové propadli #cliteratuře#x,\nza každou přečtenou knihu získávají bonusové zkušenosti. Jejich slabostí jsou #hšpičaté klobouky#x,\n#hplnovousy#x, #hhole#x a #hartefakty#x. Mágové mohou v boji využívat sílu #cčtyř elementů#x a #crunové magie#x.\nPasivně dokáží v blízkém okolí vycítit #czřídla many#x.", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, true);
                            break;
                        }
                    case 2:
                        {
                            buffer.DrawColored("Elitní skupina královských vojáků, jejichž posvátným úkolem je chránit #hLetní palác#x. Naprostá\nšpička jednotek království prošla smrtícím tréninkem, kterýpřežil každý pátý uchazeč.\nVýsledkem jsou precizní stroje na zabíjení, ovládající taje boje s #ymečem#x a #yštítem#x.\nV boji využívají #ystandartu království#x, která jim poskytuje bonusové značky.\nKromě štítů se chrání #ytěžkou zbrojí#x, což maximalizuje jejich obranu.", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, true);
                            break;
                        }
                    case 3:
                        {
                            buffer.DrawColored("Řád #hČerného orla#x existuje déle než samotné království. Složitá organizace kněží, zabývajících se \n#gléčením#x a #gzaklínaním#x, stejně jako #grunami#x. Kněží mohou v boji očarovat značky a tím jim #gpřidat \nbonusové účinky. Pasivně mají velkou odolnost proti nemocem a magii. Kněží používají #glehké zbroje#x,\n#gbitevní hole#x a v menší míře #grituální dýky#x. U občanů království mají kněží díky svým léčícím \nschopnostem velmi dobrou pověst.", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, true);
                            break;
                        }
                    case 4:
                        {
                            buffer.DrawColored("Cech zlodějů má bohatou a dlouholetou tradici. #hMistři noci#x, vůdci cechu ví o každém šustnutí v\nkrálovství dřív, než se stane. Není tajemstvím, že nedávnou vraždu krále #hJarvana#x má cech na svědomí. \nZloději mají #mhbité prsty#x, dokonale ovládají #mplížení#x a umí #msplynout se stínem#x. Skvěle \n#mvyhrožují#x, stejně jako #mlichotí#x. Rozumí bylinám a #mjedům#x, v noci rychleji regenerují zdraví. \nV boji používají #mdýky#x, #mluky#x, #mskryté čepele#h #xa #mpasti#x. Obrana je nesmí zpomalovat a proto se odívají\npouze do lehké látky.", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, true);
                            break;
                        }
                }
                // /CHARACTER STATS
                buffer.NewLine();
                buffer.Draw(Strings.getInstance.horizontalLine, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
                buffer.NewLine();

                for (int i = 0; i < items.Length; i++)
                {
                    if (choosedIndex != i) { buffer.Draw("> " + items[i]); }
                    else { buffer.Draw(">  " + items[i], ConsoleColor.Yellow); }

                    buffer.NewLine();
                }

                buffer.Print();
                ConsoleKey key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.W:
                        {
                            if (choosedIndex > 0)
                            {
                                choosedIndex--;
                            }
                            else
                            {
                                choosedIndex = items.Length - 1;
                            }

                            break;
                        }

                    case ConsoleKey.S:
                        {
                            if (choosedIndex < items.Length - 1)
                            {
                                choosedIndex++;
                            }
                            else
                            {
                                choosedIndex = 0;
                            }

                            break;
                        }
                    case ConsoleKey.Enter:
                        {

                            buffer.Clear();
                            choosing = false;
                            Console.SetCursorPosition(0, 0);
                            buffer.Print();
                            GetPlayerClassPartTwo(choosedIndex);
                            break;
                        }
                }
            }
        }

        void GetPlayerClassPartTwo(int index)
        {

            string fileString = "";

            switch (index)
            {
                case (int)classes.barbarian:
                    {
                        fileString = "class_barbarian.txt";
                        break;
                    }
            }


            StreamReader sr = new StreamReader(ResourceTree.dataClasses + fileString, Encoding.UTF8);
            string line = "";
            string header = "";
            string[] parsed = new string[File.ReadLines(ResourceTree.dataClasses + fileString).Count()];
            int i = 0;
            int[] parsedVlastnosti = new int[Enum.GetNames(typeof(oPlayer.vlastnosti)).Length];
            int[] parsedDovednosti = new int[Enum.GetNames(typeof(oPlayer.dovednosti)).Length];
            string[] parsedVlastnostiText = new string[Enum.GetNames(typeof(oPlayer.vlastnosti)).Length];
            string[] parsedDovednostiText = new string[Enum.GetNames(typeof(oPlayer.dovednosti)).Length];
            int parsingStatus = 0;
            List<string> specializaceList = new List<string>();
            List<string> bonusyList = new List<string>();

            for (int j = 0; j < parsed.Length; j++)
            {
                parsed[j] = "";
            }
            while ((line = sr.ReadLine()) != null)
            {
                parsed[i] = line;
                i++;
            }

            sr.Close();

            for (int j = 0; j < parsed.Length; j++)
            {
                if (parsingStatus == 0)
                {
                    // Vlastnosti [int]                            
                    if (parsed[j].StartsWith("[vlastnosti]"))
                    {
                        parsed[j] = parsed[j].Replace("[vlastnosti]", "");
                        parsedVlastnosti = parsed[j].Split(',').Select(n => Convert.ToInt32(n)).ToArray();
                    }

                    // Dovednosti [int]
                    if (parsed[j].StartsWith("[dovednosti]"))
                    {
                        parsed[j] = parsed[j].Replace("[dovednosti]", "");
                        parsedDovednosti = parsed[j].Split(',').Select(n => Convert.ToInt32(n)).ToArray();
                    }

                    // Specializace [string]
                    if (parsed[j].StartsWith("[specializace]"))
                    {
                        parsed[j] = parsed[j].Replace("[specializace]", "");
                        parsingStatus = 1;
                    }

                    // Bonusy [string]
                    if (parsed[j].StartsWith("[bonusy_povolani]"))
                    {
                        parsed[j] = parsed[j].Replace("[bonusy_povolani]", "");
                        parsingStatus = 2;
                    }

                    // Header [string]
                    if (parsed[j].StartsWith("[nazev]"))
                    {
                        parsed[j] = parsed[j].Replace("[nazev]", "");
                        header = parsed[j];
                    }
                }

                // PARSING SPECIALIZACE
                if (parsingStatus == 1)
                {
                    if (parsed[j].StartsWith("[/specializace]")) // Koncový metaTag
                    {
                        parsed[j] = parsed[j].Replace("[/specializace]", "");
                        parsingStatus = 0;
                    }
                    else
                    {
                        specializaceList.Add(parsed[j]);
                    }
                }

                // PARSING BONUSY POVOLANI
                if (parsingStatus == 2)
                {
                    if (parsed[j].StartsWith("[/bonusy_povolani]")) // Koncový metaTag
                    {
                        parsed[j] = parsed[j].Replace("[/bonusy_povolani]", "");
                        parsingStatus = 0;
                    }
                    else
                    {
                        bonusyList.Add(parsed[j]);
                    }
                }
            }

            // Hlavička
            buffer.Draw(Strings.getInstance.horizontalLine);
            buffer.NewLine();
            buffer.DrawColored(header, Console.CursorLeft + ((Console.WindowWidth / 2) - header.ToString().Length / 2), Console.CursorTop, ConsoleColor.Yellow, true);

            buffer.NewLine();
            buffer.Draw(Strings.getInstance.horizontalLine);
            buffer.NewLine();

            // Vlastnosti
            buffer.DrawColored("[Vlastnosti]", Console.CursorLeft, Console.CursorTop, ConsoleColor.Yellow, false, true);
            buffer.NewLine();
            buffer.DrawColored("Síla:         ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true, buffer.type.characterCreation, parsedVlastnosti[(int)oPlayer.vlastnosti.sila]);
            buffer.DrawColored("Konstituce:   ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true, buffer.type.characterCreation, parsedVlastnosti[(int)oPlayer.vlastnosti.konstituce]);
            buffer.DrawColored("Obratnost:    ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true, buffer.type.characterCreation, parsedVlastnosti[(int)oPlayer.vlastnosti.obratnost]);
            buffer.DrawColored("Odolnost:     ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true, buffer.type.characterCreation, parsedVlastnosti[(int)oPlayer.vlastnosti.odolnost]);
            buffer.DrawColored("Inteligence:  ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true, buffer.type.characterCreation, parsedVlastnosti[(int)oPlayer.vlastnosti.inteligence]);
            buffer.DrawColored("Charisma:     ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true, buffer.type.characterCreation, parsedVlastnosti[(int)oPlayer.vlastnosti.charisma]);
            buffer.DrawColored("Vůle:         ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true, buffer.type.characterCreation, parsedVlastnosti[(int)oPlayer.vlastnosti.vule]);
            buffer.DrawColored("Zručnost:     ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true, buffer.type.characterCreation, parsedVlastnosti[(int)oPlayer.vlastnosti.zrucnost]);
            buffer.DrawColored("Postřeh:      ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true, buffer.type.characterCreation, parsedVlastnosti[(int)oPlayer.vlastnosti.postreh]);
            buffer.DrawColored("Štěstí:       ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true, buffer.type.characterCreation, parsedVlastnosti[(int)oPlayer.vlastnosti.stesti]);

            // Dovednosti
            Console.CursorTop = 3;
            buffer.DrawColored("[Dovednosti]", 30, Console.CursorTop, ConsoleColor.Yellow, false, true);
            buffer.NewLine();
            buffer.DrawColored("Háčkování zámků:    ", 30, Console.CursorTop, ConsoleColor.Gray, false, true, buffer.type.characterCreation, parsedDovednosti[(int)oPlayer.dovednosti.hackovani_zamku]);
            buffer.DrawColored("Plížení:            ", 30, Console.CursorTop, ConsoleColor.Gray, false, true, buffer.type.characterCreation, parsedDovednosti[(int)oPlayer.dovednosti.plizeni]);
            buffer.DrawColored("Přesvědčování:      ", 30, Console.CursorTop, ConsoleColor.Gray, false, true, buffer.type.characterCreation, parsedDovednosti[(int)oPlayer.dovednosti.presvedcovani]);
            buffer.DrawColored("Zastrašování:       ", 30, Console.CursorTop, ConsoleColor.Gray, false, true, buffer.type.characterCreation, parsedDovednosti[(int)oPlayer.dovednosti.zastrasovani]);
            buffer.DrawColored("Runová magie:       ", 30, Console.CursorTop, ConsoleColor.Gray, false, true, buffer.type.characterCreation, parsedDovednosti[(int)oPlayer.dovednosti.runova_magie]);
            buffer.DrawColored("Elementární magie:  ", 30, Console.CursorTop, ConsoleColor.Gray, false, true, buffer.type.characterCreation, parsedDovednosti[(int)oPlayer.dovednosti.elementarni_magie]);
            buffer.DrawColored("Zaříkávání:         ", 30, Console.CursorTop, ConsoleColor.Gray, false, true, buffer.type.characterCreation, parsedDovednosti[(int)oPlayer.dovednosti.zarikavani]);
            buffer.DrawColored("Smlouvání:          ", 30, Console.CursorTop, ConsoleColor.Gray, false, true, buffer.type.characterCreation, parsedDovednosti[(int)oPlayer.dovednosti.smlouvani]);
            buffer.DrawColored("Zápal:              ", 30, Console.CursorTop, ConsoleColor.Gray, false, true, buffer.type.characterCreation, parsedDovednosti[(int)oPlayer.dovednosti.zapal]);
            buffer.DrawColored("Víra :              ", 30, Console.CursorTop, ConsoleColor.Gray, false, true, buffer.type.characterCreation, parsedDovednosti[(int)oPlayer.dovednosti.vira]);

            // Specializace
            Console.CursorTop = 3;
            buffer.DrawColored("[Specializace]", 63, Console.CursorTop, ConsoleColor.Yellow, false, true);
            foreach (string str in specializaceList)
            {
                buffer.DrawColored(str, 63, Console.CursorTop, ConsoleColor.Gray, false, true);
            }

            // Bonusy povolání
            Console.CursorTop = 16;
            buffer.DrawColored("[Bonusy povolání]", Console.CursorLeft, Console.CursorTop, ConsoleColor.Yellow, false, true);
            foreach (string str in bonusyList)
            {
                buffer.DrawColored(str, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
            }

            // Nabídka
            buffer.Print();
            buffer.NewLine();
            Console.CursorLeft = 0;
            int top = Console.CursorTop;
            bool choosing = true;
            int choosed = 0;
            string[] items = { "Zpět k výběru povolání", "Potvrdit výběr" };

            while (choosing)
            {
                for (int k = 0; k < items.Length; k++)
                {
                    if (k != choosed) { buffer.DrawColored("> " + items[k], Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true); }
                    else
                    {
                        buffer.DrawColored(" #g> #x" + items[k], Console.CursorLeft, Console.CursorTop, ConsoleColor.Yellow, false, true);
                    }
                }

                buffer.Print();
                ConsoleKey key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.W:
                        {
                            if (choosed > 0)
                            {
                                choosed--;
                            }
                            else
                            {
                                choosed = items.Length - 1;
                            }
                            break;
                        }

                    case ConsoleKey.S:
                        {
                            if (choosed < items.Length - 1)
                            {
                                choosed++;
                            }
                            else
                            {
                                choosed = 0;
                            }
                            break;
                        }

                    case ConsoleKey.Enter:
                        {
                            switch (choosed)
                            {
                                case 0:
                                    {
                                        choosing = false;
                                        buffer.Clear();
                                        Console.SetCursorPosition(0, 0);
                                        GetPlayerClass(index);
                                        break;
                                    }

                                case 1:
                                    {
                                        choosing = false;
                                        buffer.Clear();
                                        Console.SetCursorPosition(0, 0);
                                        GameWorld.getInstance.player.gameClass = (classes)index;
                                        oPlayer player = GameWorld.getInstance.player;

                                        for (int j = 0; j < player.Vlastnosti.Length; j++)
                                        {
                                            player.Vlastnosti[j] = parsedVlastnosti[j];
                                        }

                                        for (int j = 0; j < player.Dovednosti.Length; j++)
                                        {
                                            player.Dovednosti[j] = parsedDovednosti[j];
                                        }

                                        getName();
                                        increasePlayerStats(5, 5);
                                        break;
                                    }
                            }

                            break;
                        }
                }

                for (int l = 0; l < items.Length; l++)
                {
                    buffer.clearRow(top + l);
                }

                Console.CursorTop = top;
                Console.CursorLeft = 0;
            }


        }

        public void increasePlayerStats(int vlastnostiBody, int dovednostiBody)
        {
            oPlayer player = GameWorld.getInstance.player;
            string choosing = "main_menu";
            bool choosingAction = true;
            string[] vlastnostiName = player.vlastnostiName;
            string[] dovednostiName = player.dovednostiName;
            int choosingHor = 0;
            int choosingVer = 0;
            Console.CursorVisible = false;

            while (choosing == "main_menu")
            {
                buffer.Clear();
                Console.SetCursorPosition(0, 0);

                // Hlavička
                buffer.Draw(Strings.getInstance.horizontalLine);
                buffer.NewLine();
                buffer.DrawColored(player.name, Console.CursorLeft + ((Console.WindowWidth / 2) - player.name.ToString().Length / 2), Console.CursorTop, ConsoleColor.Yellow, true);

                buffer.NewLine();
                buffer.Draw(Strings.getInstance.horizontalLine);
                buffer.NewLine();

                // Vlastnosti
                buffer.DrawColored("[Vlastnosti]", Console.CursorLeft, Console.CursorTop, ConsoleColor.Yellow, false, true);
                buffer.NewLine();
                for (int i = 0; i < vlastnostiName.Length; i++)
                {
                    buffer.DrawColored(vlastnostiName[i] + "#y" + player.Vlastnosti[i] + "#x ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
                }

                // Dovednosti
                Console.CursorTop = 3;
                buffer.DrawColored("[Dovednosti]", 30, Console.CursorTop, ConsoleColor.Yellow, false, true);
                buffer.NewLine();
                for (int i = 0; i < dovednostiName.Length; i++)
                {
                    buffer.DrawColored(vlastnostiName[i] + "#y" + player.Dovednosti[i] + "#x ", 30, Console.CursorTop, ConsoleColor.Gray, false, true);
                }
                buffer.NewLine();

                buffer.Print();
                int choosed = 0;
                string[] items = { "Inventář", "Level-up", "Status postavy", "Statistiky","Zpět" };
                int top = Console.CursorTop;

                while (choosingAction)
                {
                    for (int k = 0; k < items.Length; k++)
                    {
                        if (k != 1)
                        {
                            if (k != choosed) { buffer.DrawColored("> " + items[k], Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true); }
                            else
                            {
                                buffer.DrawColored(" #g> #x" + items[k], Console.CursorLeft, Console.CursorTop, ConsoleColor.Yellow, false, true);
                            }
                        }
                        else
                        {
                            if (player.lvlupDovednosti + player.lvlupVlastnosti > 0)
                            {
                                if (k != choosed) { buffer.DrawColored("> " + items[k] +" (#g" + Convert.ToString(player.lvlupDovednosti + player.lvlupVlastnosti) + "#x)", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true); }
                                else
                                {
                                    buffer.DrawColored(" #g> #x" + items[k] + " (#g" + Convert.ToString(player.lvlupDovednosti + player.lvlupVlastnosti) + "#x)", Console.CursorLeft, Console.CursorTop, ConsoleColor.Yellow, false, true);
                                }
                            }
                            else
                            {
                                if (k != choosed) { buffer.DrawColored("> " + items[k] + " (#h0#x)", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true); }
                                else
                                {
                                    buffer.DrawColored(" #g> #x" + items[k] + " (#h0#x)", Console.CursorLeft, Console.CursorTop, ConsoleColor.Yellow, false, true);
                                }
                            }
                        }
                    }

                    buffer.Print();
                    ConsoleKey key = Console.ReadKey().Key;


                    switch (key)
                    {
                        case ConsoleKey.W:
                            {
                                if (choosed > 0)
                                {
                                    choosed--;
                                }
                                else
                                {
                                    choosed = items.Length - 1;
                                }
                                break;
                            }

                        case ConsoleKey.S:
                            {
                                if (choosed < items.Length - 1)
                                {
                                    choosed++;
                                }
                                else
                                {
                                    choosed = 0;
                                }
                                break;
                            }

                        case ConsoleKey.Enter:
                            {
                                switch (choosed)
                                {
                                    case 0:
                                        {
                                        //    choosingAction = false;
                                            buffer.Clear();
                                            Console.SetCursorPosition(0, 0);
                                            buffer.Print();

                                            Inventory.getInstance.drawInventory(10);
                                            choosing = "end";
                                            choosingAction = false;
                                            //   GetPlayerClass(index);
                                            break;
                                        }
                                    case 1:
                                        {
                                            //    choosingAction = false;
                                            buffer.Clear();
                                            Console.SetCursorPosition(0, 0);
                                            buffer.Print();
                                            choosing = "lvl_up";
                                            choosingAction = false;
                                            //   GetPlayerClass(index);
                                            break;
                                        }
                                    case 4:
                                        {
                                            buffer.Clear();
                                            Console.SetCursorPosition(0, 0);
                                            buffer.Print();
                                            choosing = "end";
                                            choosingAction = false;
                                            break;
                                        }
                                }

                                break;
                            }
                    }

                    for (int l = 0; l < items.Length; l++)
                    {
                        buffer.clearRow(top + l);
                    }

                    Console.CursorTop = top;
                    Console.CursorLeft = 0;
                }
            }

            // Level Up ...............................................................................................................



            while (choosing == "lvl_up")
            {
                choosingAction = true;

                buffer.Clear();
                Console.SetCursorPosition(0, 0);

                // Hlavička
                buffer.Draw(Strings.getInstance.horizontalLine);
                buffer.NewLine();
                buffer.DrawColored(player.name, Console.CursorLeft + ((Console.WindowWidth / 2) - player.name.ToString().Length / 2), Console.CursorTop, ConsoleColor.Yellow, true);

                buffer.NewLine();
                buffer.Draw(Strings.getInstance.horizontalLine);
                buffer.NewLine();

                // Vlastnosti
                buffer.DrawColored("[Vlastnosti]", Console.CursorLeft, Console.CursorTop, ConsoleColor.Yellow, false, true);
                buffer.NewLine();
                for (int i = 0; i < vlastnostiName.Length; i++)
                {
                    if (choosingHor == 0 && choosingVer == i)
                    {
                        buffer.DrawColored("#g" + vlastnostiName[i] + "#x #y[" + player.Vlastnosti[i] + "]#x ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
                        buffer.DrawColored(player.vlastnostiPopis[i], Console.CursorLeft, 17, ConsoleColor.DarkGray, false, false);
                        Console.CursorTop--;
                    }
                    else
                    {
                        buffer.DrawColored(vlastnostiName[i] + "#y" + player.Vlastnosti[i] + "#x ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
           
                    }

                }

                // Dovednosti
                Console.CursorTop = 3;
                buffer.DrawColored("[Dovednosti]", 30, Console.CursorTop, ConsoleColor.Yellow, false, true);
                buffer.NewLine();
                for (int i = 0; i < dovednostiName.Length; i++)
                {
                    if (choosingHor == 1 && choosingVer == i)
                    {
                        buffer.DrawColored("#g" + vlastnostiName[i] + "#x #y[" + player.Dovednosti[i] + "]#x ", 30, Console.CursorTop, ConsoleColor.Gray, false, true);
                    }
                    else
                    {
                        buffer.DrawColored(vlastnostiName[i] + "#y" + player.Dovednosti[i] + "#x ", 30, Console.CursorTop, ConsoleColor.Gray, false, true);
                    }
                }

                // Status
                Console.CursorTop = 3;
                buffer.DrawColored("[Status]", 63, Console.CursorTop, ConsoleColor.Yellow, false, true);
                buffer.NewLine();
                string remVlastnosti = "";
                string remDovednosti = "";

                if (player.lvlupVlastnosti > 0)
                {
                     remVlastnosti = "#g" + Convert.ToString(player.lvlupVlastnosti) + "#x ";
                }
                else
                {
                     remVlastnosti = "#h0#x ";
                }

                if (player.lvlupDovednosti > 0)
                {
                    remDovednosti = "#g" + Convert.ToString(player.lvlupDovednosti) + "#x ";
                }
                else
                {
                    remDovednosti = "#h0#x ";
                }
                buffer.DrawColored("Úroveň postavy:  " + "#y" + player.level + "#x" + " (#y" + player.xp + "#x / #y" + player.max_xp + "#x)", 63, Console.CursorTop, ConsoleColor.Gray, false, true);
                buffer.NewLine();
                buffer.DrawColored("Bodů vlastností: " + remVlastnosti, 63, Console.CursorTop, ConsoleColor.Gray, false, true);
                buffer.DrawColored("Bodů dovedností: " + remDovednosti, 63, Console.CursorTop, ConsoleColor.Gray, false, true);

                Console.CursorTop = 21;
                buffer.DrawColored("[" + "#g" + "X#x" + "]" + " - Zpět", 0, Console.CursorTop, ConsoleColor.Gray, false, true);


                int choosed = 0;
                buffer.Print();
                ConsoleKey key = Console.ReadKey(true).Key;

                using (System.Media.SoundPlayer soundPLayer = new System.Media.SoundPlayer(ResourceTree.sound + "menu.wav"))
                {
                   // soundPLayer.Play();
                }

                switch (key)
                {
                    case ConsoleKey.W:
                        {
                            if (choosingVer > 0)
                            {
                                choosingVer--;
                            }
                            else
                            {
                                if (choosingHor > 0)
                                {
                                    choosingVer = player.Vlastnosti.Length - 1;
                                    choosingHor--;
                                }
                                else
                                {
                                    choosingVer = player.Vlastnosti.Length - 1;
                                    choosingHor = 1;  
                                }
                            }
                            break;
                        }

                    case ConsoleKey.S:
                        {
                            if (choosingVer < player.Vlastnosti.Length - 1)
                            {
                                choosingVer++;
                            }
                            else
                            {
                                if (choosingHor <1)
                                {
                                    choosingVer = 0;
                                    choosingHor++;
                                }
                                else
                                {
                                   
                                    choosingVer = 0;
                                    choosingHor = 0;
                                }
                            }
                            break;
                        }

                    case ConsoleKey.D:
                        {
                            if (choosingHor < 1)
                            {
                                choosingHor++;
                            }
                            else
                            {
                                choosingHor = 0;
                            }
                            break;
                        }

                    case ConsoleKey.A:
                        {
                            if (choosingHor > 0)
                            {
                                choosingHor--;
                            }
                            else
                            {
                                choosingHor = 1;
                            }
                            break;
                        }

                    case ConsoleKey.X:
                        {
                            choosing = "main_menu";
                            choosingAction = true;
                            increasePlayerStats(player.lvlupVlastnosti, player.lvlupDovednosti);
                            break;
                        }

                    case ConsoleKey.Enter:
                        {
                            switch (choosingHor)
                            {
                                case 0:
                                    {
                                       if (player.lvlupVlastnosti > 0)
                                        {
                                            player.Vlastnosti[choosingVer]++;
                                            player.lvlupVlastnosti--;
                                        }

                                        break;
                                    }
                                case 1:
                                    {
                                        if (player.lvlupDovednosti > 0)
                                        {
                                            player.Dovednosti[choosingVer]++;
                                            player.lvlupDovednosti--;
                                        }
                                        break;
                                    }
                           
                            }

                            break;
                        }
                }             
            }

            Console.CursorVisible = true;
        }

        public void getName()
        {
            buffer.Clear();
            Console.SetCursorPosition(0, 0);
            buffer.Print();

            bool typing = true;
            string intro = "Jaké je tvé jméno?";
            string name = "Ztracená duše";

            buffer.Draw(Strings.getInstance.horizontalLine);
            buffer.NewLine();

            if (intro.Length > 0)
            {
                buffer.DrawColored(intro, Console.CursorLeft + ((Console.WindowWidth / 2) - intro.ToString().Length / 2), Console.CursorTop, ConsoleColor.Yellow, true);
            }

            buffer.NewLine();
            buffer.Draw(Strings.getInstance.horizontalLine);
            buffer.NewLine();


            while (typing)
              {
                Console.CursorTop = 4;
                Console.CursorLeft = ((Console.WindowWidth / 2) - name.ToString().Length / 2);

                buffer.clearRow(4);

                for (int i = 0; i < name.Length; i++)
                {
                    if (i != name.Length-1)
                    {
                        buffer.Draw(Convert.ToString(name[i]));
                    }
                    else
                    {
                        buffer.Draw(Convert.ToString(name[i]), ConsoleColor.Yellow);
                    }
                }

                buffer.Print();
                ConsoleKey key = Console.ReadKey().Key;
    

                if (key == ConsoleKey.Enter)
                {
                    typing = false;
                    GameWorld.getInstance.player.name = name;
                }
                else if (key == ConsoleKey.Backspace)
                {
                    if (name.Length > 0)
                    {
                        name = name.Remove(name.Length - 1, 1);
                    }
                }
                else
                {
                    string stringKey = Convert.ToString(key);

                    if (key == ConsoleKey.Spacebar) { stringKey = " "; }

                    if (Control.ModifierKeys == Keys.Shift)
                    {
                        name += stringKey;
                    }
                    if (Control.ModifierKeys == Keys.None)
                    {
                        name += stringKey.ToLower();
                    }
                }
            }
        }
    }
}
