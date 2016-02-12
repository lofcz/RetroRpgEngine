using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroRPG.RetroLanguage
{
    class RetroLanguageInterpreter
    {
        string[] newVar = { "","","","" }; // typ,název,hodnota
        buffer buffer = Render.getInstance.Buffer;
        List<variable> variables = new List<variable>();
        variable myVar = new variable();
        bool creatingVar = false;

        enum dataType
        {
            String,Int,Float,Bool
        };

        struct variable
        {
            public dataType Datatype;
            public object value;
            public string name;

        };

        string[] dataTypes = { "string", "int", "float", "bool" };
        string[] funkce = { "print", "read", "newLine" };
        string[] keywords = { "for" };


        private static RetroLanguageInterpreter rli;
        private RetroLanguageInterpreter() { }

        public static RetroLanguageInterpreter getInstance
        {
            get
            {
                if (rli == null)
                {
                    rli = new RetroLanguageInterpreter();
                }

                return rli;
            }
        }

        public void interpretCode(string file)
        {
            Console.SetCursorPosition(0, 0);
            buffer.Clear();

            StreamReader sr = new StreamReader(file);
            string line = "";
            bool readingContent = false;
            string currentWord = "";
            int uvozovky = 0;
            bool nalezenaFunkce = false;


            while((line = sr.ReadLine()) != null)
            {

                if (line == "#Content") { readingContent = true; line = line.Remove(0, 8); /* odstraníme metatag */ }
                if (line == "#EndContent") { readingContent = false; }

               if (readingContent)
                {
                    line = line + " ";

                    foreach (char znak in line)
                    {
                        if (newVar[1] != "" && creatingVar)
                        {
                            if (znak != '"' && znak != '=' )
                                {
                                if (uvozovky > 0 || znak == ';') 
                                {
                                    currentWord += znak.ToString();
                                }                              
                                }
                            else if (znak == '"') { uvozovky++; }

                            if (uvozovky == 2)
                            {
                                if (currentWord != "") { interpretCommand(currentWord); currentWord = ""; uvozovky = 0; }
                            }
                            else
                            {
                                if (currentWord == ";")
                                {
                                    interpretCommand(currentWord); currentWord = "";
                                 
                                }
                            }

                        }
                        else
                        {
                            if (znak == '"') { uvozovky++; }

                            if (znak != ' ' || (uvozovky == 1)) {currentWord += znak.ToString();}
                            else { if (currentWord != "" && (uvozovky == 0 || uvozovky == 2)) { interpretCommand(currentWord); currentWord = ""; uvozovky = 0; } }

                        }
                    }
                }
            }

            debugOutput(); // TEST
        }

        void interpretCommand(string command)
        {

            if (command == "string") { newVar[0] = "string"; creatingVar = true; return; }

            if (creatingVar)
            {

               
                if (newVar[0] != "")
                {
                    if (newVar[1] == "") { newVar[1] = command; return; }
                }

                if (newVar[2] == "")
                {
                    if (command == "=") { return; }
                    else
                    {
                        if (command == ";")
                        {
                            myVar = new variable();
                            if (newVar[0] == "string") { myVar.Datatype = dataType.String; }
                            myVar.name = newVar[1];
                            newVar[2] = ";";

                            switch (myVar.Datatype)
                            {
                                case dataType.String:
                                    {
                                        myVar.value = "";
                                        break;
                                    }
                            }

                            creatingVar = false;
                            for (int i = 0; i < 4; i++)
                            {
                                newVar[i] = "";
                            }
                        }
                        else
                        {
                            newVar[2] = command;
                            myVar = new variable();

                            if (newVar[0] == "string") { myVar.Datatype = dataType.String; }
                            myVar.name = newVar[1];


                            switch (myVar.Datatype)
                            {
                                case dataType.String:
                                    {
                                        myVar.value = newVar[2];
                                        break;
                                    }
                            }
                        }

                        variables.Add(myVar);
                    }

                    return;
                }

                if (command == ";")
                {
                    //uffer.Draw("Ukončuji definici proměnné: " + newVar[1]);
                    creatingVar = false;
                    for (int i = 0; i < 4; i++)
                    {
                        newVar[i] = "";
                    }

                    return;
                }




            }
            else
            {
                for (int i = 0; i < keywords.Length; i++)
                {
                    if (command.StartsWith(keywords[i]))
                    {
                        interpretKeyword(command);
                        return;

                    }
                }

                foreach(variable myvar in variables)
                {
                    if (command.StartsWith(myvar.name))
                    {
                        modifyVariable(command);
                        return;
                    }
                }

                interpretFce(command);           
            }
        }

        void debugOutput()
        {
            foreach(variable iteratedVar in variables)
            {
                buffer.DrawColored("#y" + iteratedVar.Datatype + "#x " + iteratedVar.name + " = " + "#g" + iteratedVar.value + "#x ", Console.CursorLeft, Console.CursorTop, ConsoleColor.Gray, false, true);
            }

            buffer.Print();
            //Console.ReadKey();
        }

        void interpretFce(string command)
        {
            List<object> parametry = new List<object>();
            int pocetParametru = 0;

            foreach(char znak in command)
            {
                if (znak == ',') { pocetParametru++; }
            }

            // Funkce PRINT
            if (command.StartsWith("print"))
            {
                bool uvozovky = false;

                command = command.Replace("print(", "");
                command = command.Replace(")", "");
                command = command.Replace(";", "");

                string output = "";
                //buffer.Draw("BUDU TISKNOUT (PRINT)");
                //buffer.NewLine();

                // Přetížení pro 0 parametrů
                if (pocetParametru == 0)
                {
                   
                    foreach(char znak in command)
                    {
                        if (znak == '"') { uvozovky = !uvozovky;  continue; }

                        if (uvozovky)
                        {
                            output += znak;
                        }

                        if (znak == '+')
                            {
                              
                            }
                        }
                    }
                    // Vypsání textu
                    if (command.Contains("\""))
                    {
                        command = command.Replace("\"", "");
                       
                        buffer.Draw(command);
                    }
                    else // Vypsání hodnoty proměnné
                    {
                        foreach(variable iteratedVar in variables)
                        {
                            if (iteratedVar.name == command)
                            {
                                buffer.Draw(iteratedVar.value.ToString());
                                break;
                            }
                        }
                    }
                }
                //buffer.Draw("BUDU TISKNOUT (PRINT)");
                buffer.Print();
            

            // Funkce pro počkání na stisk klávesy
            if (command.StartsWith("read"))
            {
                command = command.Replace("read(", "");
                command = command.Replace(")", "");
                command = command.Replace(";", "");

                if (command.Length > 0 && pocetParametru == 0)
                {
                    if (command == "true")
                    {
                        Console.ReadKey(true);
                    }
                    else
                    {
                        Console.ReadKey();
                    }

                }
                else if (pocetParametru == 0)
                {
                    Console.ReadKey();
                }
            }

            // Nová řádka v konzoli
            if (command.StartsWith("newLine"))
            {
                buffer.NewLine();
                buffer.Print();
            }
        }

        void modifyVariable(string command)
        {

        }

        void interpretKeyword(string command)
        {

        }
    }

}
