using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UrielGuy.SyntaxHighlightingTextBox;
using System.IO;

namespace RetroRpgLevelEditor
{
    public partial class Form1 : Form
    {
        SyntaxHighlightingTextBox shtb;
        SyntaxHighlightingTextBox shtb2;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            shtb = new SyntaxHighlightingTextBox();
            shtb.Location = new Point(0, 0);
            shtb.Dock = DockStyle.Left;
            shtb.Width = 850;
            shtb.Font = new Font("LucidaConsole", 12, FontStyle.Bold);
           // shtb.DoubleClick += new EventHandler(shtbDoubleClick);
            shtb.Seperators.Add(' ');
            shtb.Seperators.Add('\r');
            shtb.Seperators.Add('\n');
            shtb.Seperators.Add(',');
            shtb.Seperators.Add('.');
            shtb.Seperators.Add('-');
            shtb.Seperators.Add('+');
            //shtb.Seperators.Add('*');
            //shtb.Seperators.Add('/');
            Controls.Add(shtb);
            shtb.WordWrap = false;
            shtb.ScrollBars = RichTextBoxScrollBars.Both;// & RichTextBoxScrollBars.ForcedVertical;

            shtb.FilterAutoComplete = false;
            /*shtb.HighlightDescriptors.Add(new HighlightDescriptor("<", Color.Gray, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));

            shtb.HighlightDescriptors.Add(new HighlightDescriptor("<<", ">>", Color.DarkGreen, null, DescriptorType.ToCloseToken, DescriptorRecognition.StartsWith, false));
*/
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("P", Color.Blue, null, DescriptorType.Word, DescriptorRecognition.WholeWord, false));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("E", Color.Red, null, DescriptorType.Word, DescriptorRecognition.WholeWord, false));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("#", Color.Gray, null, DescriptorType.Word, DescriptorRecognition.Contains, false));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("World", Color.Blue, null, DescriptorType.Word, DescriptorRecognition.WholeWord, false));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("//", Color.Green, null, DescriptorType.ToEOL, DescriptorRecognition.StartsWith, false));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("[", "]", Color.Red, null, DescriptorType.ToCloseToken, DescriptorRecognition.WholeWord, false));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("@DataPipeline", Color.Orange, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("@DesignPipeline", Color.Orange, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
           
        }



        private void oSave_Click(object sender, EventArgs e)
        {
            StreamWriter save = new StreamWriter("map.RetroRpgMap");
            int x = 0;
            int y = 0;

            bool foundDataLine = false;
            int dataLine = 0;


            foreach (string line in shtb.Lines)
            {
                dataLine++;

                if (line == "@DataPipeline")
                {
                    break;
                }
            }


            foreach (string line in shtb.Lines)
            {
              
                    if (line.StartsWith("//"))
                    {
                        save.WriteLine(line);
                    }
                    else if (line.StartsWith("@DataPipeline"))
                    {
                      //  save.WriteLine(line);
                    }
                    else
                    {
                        foreach (char znak in line)
                        {
                            if (znak == '#')
                            {
                                save.Write("[oWall]");
                            }
                            else if (znak == ' ')
                            {
                                save.Write("[blank]");
                            }
                            else if (znak == '¤')
                            {

                            bool parsing = false;
                            int xx = 0;
                            string myID = "";

                            // ZJISTÍM SVOJE ID
                            foreach (char znak1 in line)
                            {
                                xx++;

                                if (znak1 == '[' && parsing == false && xx > x)
                                {
                                    parsing = true;
                                }
                                else if (parsing)
                                {
                                    if (znak1 != ']' && znak1 != ' ')
                                    {
                                        myID += znak1;
                                    }
                                    if (znak1 == ']')
                                    {
                                        break;
                                    }
                                }
                            }
                               string csvParametrs = "";

                                for (int i = dataLine; i < shtb.Lines.Count(); i++ )
                                {
                                    int parsingInformation = 0;
                                    string yy = "";
                                    string value = "";
                                    string line1 = shtb.Lines[i];
                                    string id = "";


                                    // Naparsování pipeliny
                                    foreach(char znak1 in line1)
                                    {
                                        if (znak1 == '[')
                                        {
                                            parsingInformation++;
                                            if (parsingInformation == 2)
                                            {
                                                if (id != myID)
                                                {
                                                    break;
                                                }
                                                else
                                                {
                                                    //MessageBox.Show("Našel jsem shodu v ID");
                                                }

                                            }
                                        }
                                        else if (znak1 != ' ' && znak1 != ']')
                                        {
                                            if (parsingInformation == 1) // Parsuju ID
                                            {
                                               // MessageBox.Show("Parsuju ID");
                                                id += znak1;
                                            }
                                            if (parsingInformation == 2) // Parsuju CSV parametry
                                            {                                             
                                                csvParametrs += znak1;
                                            }
                                        }
                                    }

                                }

                                if (csvParametrs != "")
                                {
                                    MessageBox.Show(csvParametrs);
                                }

                                save.Write("[oGold]" + "[" + csvParametrs + "]");
                            }
                            save.WriteLine();
                            x++;
                        }
                        save.WriteLine("#NewLine");
                    }

                    y++;
                    x = 0;
          
                
            }

            save.Close();
        }

        private void oLoad_Click(object sender, EventArgs e)
        {
        //    backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
        //    backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(bgw_ProgressChanged);
         //   backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
           // backgroundWorker1.WorkerReportsProgress = true;
          //  backgroundWorker1.RunWorkerAsync();


            StreamReader load = new StreamReader("map.RetroRpgMap");
            string line = "";
            int total_lines = File.ReadLines("map.RetroRpgMap").Count();
            int current_line = 0;

            while ((line = load.ReadLine()) != null)
            {
                current_line++;
                if (line.StartsWith("//"))
                {
                    SetText(line);//  shtb.Text += line;
                    SetText("\n");  //shtb.Text += "\n";
                }
                else if (line.Contains("[oWall]"))
                {
                    //shtb.Text += "#";
                    SetText("#");
                }
                else if (line.Contains("[blank]"))
                {
                    // shtb.Text += " ";
                    SetText(" ");
                }
                else if (line == "#NewLine")
                {
                    //shtb.Text += "\n";
                    SetText("\n");
                }

               // int percents = ((current_line * 100) / total_lines);
              //  backgroundWorker1.ReportProgress(percents, 1);
            }
            load.Close();
        }

        void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;          
        }

        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //do the code when bgv completes its work
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            StreamReader load = new StreamReader("map.RetroRpgMap");
            string line = "";
            int total_lines = File.ReadLines("map.RetroRpgMap").Count();
            int current_line = 0;

            while ((line = load.ReadLine()) != null)
            {
                current_line++;
                if (line.StartsWith("//"))
                {
                    SetText(line);//  shtb.Text += line;
                    SetText("\n");  //shtb.Text += "\n";
                }
                else if (line.Contains("[oWall]"))
                {
                    //shtb.Text += "#";
                    SetText("#");
                }
                else if (line.Contains("[blank]"))
                {
                   // shtb.Text += " ";
                    SetText(" ");
                }
                else if (line == "#NewLine")
                {
                    //shtb.Text += "\n";
                    SetText("\n");
                }

                int percents = ((current_line * 100) / total_lines) ;
                backgroundWorker1.ReportProgress(percents, 1);
            }
            load.Close();
        }

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.shtb.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.shtb.Text += text;
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            oIDEditor.Text = shtb.Text;
            oIDEditor.ZoomFactor = 0.02F;
        }

        private void oCompile_Click(object sender, EventArgs e)
        {
            int x = 0;
            int y = 0;
            int minID = 1;
            int point = shtb.SelectionStart;
           // MessageBox.Show(Convert.ToString(point));       
            int dataLine = 0;
            foreach (string line in shtb.Lines)
            {
                dataLine++;

                if (line == "@DataPipeline")
                {
                    break;
                }
            }


            foreach (string line in shtb.Lines)
            {               

                if (line.StartsWith("//"))
                {
 
                }
                else if (line.StartsWith("@DataPipeline"))
                {

                }
                else
                {
                    string id = "";

                    foreach (char znak in line)
                    {
                        if (znak == '#')
                        {
                           
                        }
                        else if (znak == ' ')
                        {
                           
                        }
                        else if (znak == '¤')
                        {
                            bool parsing = false;
                            int xx = 0;                            

                            foreach (char znak1 in line)
                            {
                                xx++;

                                if (znak1 == '[' && parsing == false && xx > x )
                                {
                                    parsing = true;
                                }
                                else if (parsing)
                                {
                                    if (znak1 != ']' && znak1 != ' ')
                                    {
                                        id += znak1;
                                    }
                                    if (znak1 == ']')
                                    {
                                        if (Convert.ToInt32(id) >= minID)
                                        {
                                            minID = Convert.ToInt32(id) + 1;
                                        }
                                        break;
                                    }
                                }
                            }
                            /*
                            for (int i = dataLine; i < shtb.Lines.Count(); i++ )
                            {
                                int parsingInformation = 0;
                                string xx = "";
                                string yy = "";
                                string value = "";

                                foreach(char )

                            }*/
                         
                        }                       
                        x++;
                    }
                }

                y++;
                x = 0;
            }
            //MessageBox.Show("Use ID " + Convert.ToString(minID));
            shtb.Text = shtb.Text.Insert(point, " [ " + Convert.ToString(minID) + " ]");//AppendText(" [ " + Convert.ToString(minID) + " ]");
        }

        private void oZoomPlus_Click(object sender, EventArgs e)
        {
            shtb.ZoomFactor += 0.5F;
        }

        private void oZoomMinus_Click(object sender, EventArgs e)
        {
            shtb.ZoomFactor -= 0.5F;
        }

        private void oZoomCenter_Click(object sender, EventArgs e)
        {
            shtb.ZoomFactor = 1;
        }

     
        void shtbDoubleClick(object Sender, EventArgs e)
        {
            oCompile_Click(Sender, e);
        }

        private void CompileCode_Click(object sender, EventArgs e)
        {
            int x = 0;
            int y = 0;
            int minID = 1;
            int point = shtb.SelectionStart;
            // MessageBox.Show(Convert.ToString(point));       
            int dataLine = 0;
            foreach (string line in shtb.Lines)
            {
                dataLine++;

                if (line == "@DataPipeline")
                {
                    break;
                }
            }

            if (!shtb.Text.Contains("@DataPipeline"))
            {
                shtb.AppendText("\n@DataPipeline\n");
            }

            foreach (string line in shtb.Lines)
            {

                if (line.StartsWith("//"))
                {

                }
                else if (line.StartsWith("@DataPipeline"))
                {

                }
                else
                {
                    string id = "";

                    foreach (char znak in line)
                    {
                        if (znak == '#')
                        {

                        }
                        else if (znak == ' ')
                        {

                        }
                        else if (znak == '¤')
                        {
                            bool parsing = false;
                            int xx = 0;

                            foreach (char znak1 in line)
                            {
                                xx++;

                                if (znak1 == '[' && parsing == false && xx > x)
                                {
                                    parsing = true;
                                }
                                else if (parsing)
                                {
                                    if (znak1 != ']' && znak1 != ' ')
                                    {
                                        id += znak1;
                                    }
                                    if (znak1 == ']')
                                    {
                                        int returnToY = y;
                                       // ScrollToLine(dataLine);
                                        shtb.AppendText("[ " + id + " ]" + "[ oCoin ]" + "\n");
                                     //   ScrollToLine(returnToY);
                                        break;
                                    }
                                }
                            }
                            /*
                            for (int i = dataLine; i < shtb.Lines.Count(); i++ )
                            {
                                int parsingInformation = 0;
                                string xx = "";
                                string yy = "";
                                string value = "";

                                foreach(char )

                            }*/

                        }
                        x++;
                    }
                }

                y++;
                x = 0;
            }
            //MessageBox.Show("Use ID " + Convert.ToString(minID));
            //shtb.Text = shtb.Text.Insert(point, " [ " + Convert.ToString(minID) + " ]");//AppendText(" [ " + Convert.ToString(minID) + " ]");
        }

        void ScrollToLine(int lineNumber)
        {
            if (lineNumber > shtb.Lines.Count()) return;

            try
            {
                shtb.SelectionStart = shtb.Find(shtb.Lines[lineNumber]);
                shtb.ScrollToCaret();
            }
            catch
            {

            }
        }
    }
}
