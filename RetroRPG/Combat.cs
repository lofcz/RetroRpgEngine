using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroRPG.Objects;

namespace RetroRPG
{
    // Třída pro souboje
    class Combat
    {
        oEnemy enemy;
        string enemyStatus;
        oPlayer player = GameWorld.getInstance.player;
        buffer buffer = Render.getInstance.Buffer;
        List<Mark> markListEnemy = new List<Mark>();
        List<Mark> markListPlayer = new List<Mark>();
        string[] znameniSymbol = { "[#yX]", "[#gY]", "[#cX]" };
        public string enemyMarkLog = "";
        public int tempInt = 0;
        public string drawEnemyMarkStr = "";
        public int  drawEnemyMarkStrLenght = 0;

        string kombo = "";

        enum attackType
        {
            vyvazenyUder,energickyUder,obrannyUder
        };


        public Combat(oEnemy enemy)
        {
            this.enemy = enemy;
        }

        public void drawEntrance()
        {
            Console.CursorVisible = false;
            string drawEnemyName = "+- " + enemy.accessName + " -+";

            Console.SetCursorPosition(0, 0);
            Render.getInstance.Buffer.Clear();
            Render.getInstance.Buffer.Draw(Strings.getInstance.horizontalLine, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
            Render.getInstance.Buffer.NewLine();
            Render.getInstance.Buffer.DrawColored(drawEnemyName, Console.CursorLeft + ((Console.WindowWidth / 2) - drawEnemyName.ToString().Length / 2), Console.CursorTop, enemy.color, true);
            Render.getInstance.Buffer.NewLine();
            drawEnemyName = "\"" + enemy.quote + "\"";
            Render.getInstance.Buffer.DrawColored(drawEnemyName, Console.CursorLeft + ((Console.WindowWidth / 2) - drawEnemyName.ToString().Length / 2), Console.CursorTop, ConsoleColor.DarkGray, true);
            Render.getInstance.Buffer.NewLine(2);
            Parser.getInstance.parseImage(enemy.imageFile, false, ConsoleColor.Gray, Parser.Effects.none);
            Render.getInstance.Buffer.NewLine();
            Render.getInstance.Buffer.Draw(Strings.getInstance.horizontalLine, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray);
            Render.getInstance.Buffer.NewLine();
            Render.getInstance.Buffer.DrawColored(enemy.battleTag, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, true);
            Render.getInstance.Buffer.Draw(" [Enter] ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Yellow);
            Render.getInstance.Buffer.Print();
            Console.ReadKey();
            Console.CursorVisible = true;
            DrawCombat(enemy);
        }

        public void DrawCombat(oEnemy enemy)
        {
            Console.SetCursorPosition(0, 0);
            buffer.Clear();
            buffer.Print();

            string playerText = "Odhaduješ svého soupeře";
            int kolo = 0;

            string combo = "";
            int comboCount = 0;
            oPlayer player = GameWorld.getInstance.player;

            // TEST
            Mark mark = new Mark("Znamení osudu", Mark.Znameni.znameniOsudu);
            markListEnemy.Add(mark);
            // /TEST

            bool combatFlow = true;
            string enemy_log = enemy.battleTag;
            bool showHp = true;
            bool showMaxHp = true;
            bool showDamage = false;
            bool showMana = false;
            bool showEnergy = false;

            string enemyOutput = "Zdraví: ";


            while (combatFlow)
            {
                enemyOutput = "Zdraví: ";
                bool choosingAction = true;

                // Status soupeře
                if (showHp) { enemyOutput += "#g" + enemy.hp; } else { enemyOutput += "#h" + "???"; }
                if (showMaxHp) { enemyOutput += "#x / #g" + enemy.max_hp; } else { enemyOutput += "#x / #h" + "???"; }
                if (showDamage) { enemyOutput += "#x   Útok: #r" + enemy.fyzicalDamage + "#x / #b" + enemy.physicalDamge; } else { enemyOutput += "#x   Útok: #h" + "???" + "#x / #h" + "???"; }
                if (showMana) { enemyOutput += "#x   Mana: #c" + enemy.mana; } else { enemyOutput += "#x   Mana: #h" + "???"; }
                if (showEnergy) { enemyOutput += "#x    Energie: #y" + enemy.energy + "#x "; } else { enemyOutput += "#x    Energie: #h" + "???" + "#x "; }

                buffer.DrawColored(enemy.accessName, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
                buffer.DrawColored(Strings.getInstance.horizontalLine, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
                buffer.DrawColored(enemy_log, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
                buffer.NewLine();
                buffer.DrawColored(enemyOutput, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
                Render.getInstance.DrawBar(enemy.hp, enemy.max_hp, "no_text", ConsoleColor.Green, ':', false, 60);
                buffer.NewLine(2);
                buffer.DrawColored(enemyMarkLog, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
                tempInt = 0;
                enemyMarkLog = "";

                // Status hráče
                buffer.NewLine(4);
                buffer.DrawColored(player.name + " (#y" + player.level + "#x)" + " - #y" + player.xp + "#x / #y" + player.max_xp + "#x ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
                buffer.DrawColored(Strings.getInstance.horizontalLine, Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);

                // DRAW ENEMY MARKS
                Console.CursorTop = 3;
                int drawMarkX = 80;
                int drawMarkY = Console.CursorTop;

                drawEnemyMarkStr = "";
                foreach (Mark enemyMark in markListEnemy)
                {
                    enemyMark.drawMark(Mark.Vlastnik.souper);
                }
                if (drawEnemyMarkStr.Length > 0) { drawEnemyMarkStr = drawEnemyMarkStr.Remove(drawEnemyMarkStr.Length - 1, 1); }// odstraníme poslední čárku z řetězce
                Render.getInstance.Buffer.DrawColored(drawEnemyMarkStr,66, Console.CursorTop, ConsoleColor.Gray, true, false);
                Render.getInstance.drawBox(65, 2, 33, 4, ConsoleColor.Gray, Render.Outline.singleLine);

                //buffer.Print();

                // INSERTED
                Console.CursorTop = 23;
                int choosed = 0;
                string[] items = { "Vyvážený úder", "Energický úder", "Obranný úder", "Nabrat dech" };
                int top = Console.CursorTop;
                string choosing;
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
                                if (k != choosed) { buffer.DrawColored("> " + items[k] + " (#g" + Convert.ToString(player.lvlupDovednosti + player.lvlupVlastnosti) + "#x)", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true); }
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
                                            attack(attackType.vyvazenyUder);
                                            Mark addmMark = new Mark("Znamení osudu", Mark.Znameni.znameniOsudu);
                                            markListEnemy.Add(addmMark);
                                            break;
                                        }
                                    case 1:
                                        {
                                            attack(attackType.energickyUder);
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

                                choosingAction = false;
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

                Console.SetCursorPosition(0, 0);
                buffer.Clear();
                //enemyMarkLog = "";
                //tempInt = 0;
               // buffer.Print();
            }
        }

        void attack(attackType type)
        {
           List<Mark> markToEdit = null;

            switch (type)
            {
                case attackType.vyvazenyUder:
                    {
                        foreach (Mark mark in markListEnemy)
                        {
                            if (mark.timeToExplode != -1 && mark.znameni == Mark.Znameni.znameniOsudu)
                            {
                                mark.timeToExplode = 3; // Zresetuje čas všech znamení
                            }


                        }

                        break;
                    }
            }

            // Snížíme časomíru u značek
            string[] tempOutputStr = new string[Enum.GetNames(typeof(Mark.Znameni)).Length];
            int[] tempOutputInt    = new int[Enum.GetNames(typeof(Mark.Znameni)).Length];

            for (int i = markListEnemy.Count - 1; i >= 0; i--)
            {
                if (markListEnemy[i].timeToExplode != -1)
                {
                    markListEnemy[i].timeToExplode--;
                    if (markListEnemy[i].timeToExplode <= 0 && markListEnemy[i].znameni == Mark.Znameni.znameniOsudu) { markListEnemy[i].explode(); tempOutputStr[(int)Mark.Znameni.znameniOsudu] = "#y" + tempInt + "#x znamení osudu vybouchly a způsobily #y" + (tempInt*2)*tempInt + "#x bodů přímého poškození."; markListEnemy.Remove(markListEnemy[i]); }
                }

            }
           

            if (markToEdit != null)
            {
                markToEdit.Clear(); 
                markToEdit = null;
            }
        }
    }
}
