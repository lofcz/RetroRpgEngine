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

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            shtb = new SyntaxHighlightingTextBox();
            shtb.Location = new Point(0, 0);
            shtb.Dock = DockStyle.Left;
            shtb.Width = 800;
            shtb.Font = new Font("LucidaConsole", 12, FontStyle.Bold);
           
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
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("#", Color.DarkSlateGray, null, DescriptorType.Word, DescriptorRecognition.Contains, false));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("World", Color.Blue, null, DescriptorType.Word, DescriptorRecognition.WholeWord, false));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("//", Color.Green, null, DescriptorType.ToEOL, DescriptorRecognition.StartsWith, false));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("[", "]", Color.Red, null, DescriptorType.ToCloseToken, DescriptorRecognition.WholeWord, false));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("@DataPipeline", Color.Orange, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("@DesignPipeline", Color.Orange, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
           
        }



        private void oSave_Click(object sender, EventArgs e)
        {
            StreamWriter save = new StreamWriter("map.RetroRpgMap");

            bool foundDataLine = false;
            int dataLine = 0;
            foreach (string line in shtb.Lines)
            {
                dataLine++;

                if (line == "#DataPipe")
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
                else if (line.StartsWith("#DataLine"))
                {
                    save.WriteLine(line);
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
                            
                                save.Write("[blank]");
                        }
                        save.WriteLine();

                    }
                    save.WriteLine("#NewLine");
                }
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
        }
    }
}
