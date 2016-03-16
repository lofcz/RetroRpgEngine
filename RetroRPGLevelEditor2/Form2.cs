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

namespace RetroRPGLevelEditor2
{
    public partial class Form2 : Form
    {
        Form1 parentForm;
        int tileIndex;
        public SyntaxHighlightingTextBox input = new SyntaxHighlightingTextBox();

        public Form2(Form1 parentForm, int tileIndex)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            this.tileIndex = tileIndex;
        }

        private void oCompile_Click(object sender, EventArgs e)
        {
            var result = string.Join("\n", input.Lines);
            parentForm.ParseCode(result, tileIndex);
        }

        private void oSave_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            input.Location = new Point(0, 0);
            input.Dock = DockStyle.Left;
            input.Width = 600;
            input.Font = new Font("Lucida Console", 12, FontStyle.Regular);
            input.Seperators.Add(' ');
            input.Seperators.Add('\r');
            input.Seperators.Add('\n');
            input.Seperators.Add(',');
            input.Seperators.Add('.');
            input.Seperators.Add('-');
            input.Seperators.Add('+');
            Controls.Add(input);
            input.WordWrap = true;
            input.ScrollBars = RichTextBoxScrollBars.Both;// & RichTextBoxScrollBars.ForcedVertical;

            input.FilterAutoComplete = true;
            input.HighlightDescriptors.Add(new HighlightDescriptor("//", Color.Green, null, DescriptorType.ToEOL, DescriptorRecognition.StartsWith, false));
            // Color Constants
            input.HighlightDescriptors.Add(new HighlightDescriptor("clRed", Color.Orange, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            input.HighlightDescriptors.Add(new HighlightDescriptor("clBlue", Color.Orange, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            input.HighlightDescriptors.Add(new HighlightDescriptor("clGreen", Color.Orange, null, DescriptorType.Word, DescriptorRecognition.Contains, true));
            input.HighlightDescriptors.Add(new HighlightDescriptor("saveBookmark", Color.Brown, null, DescriptorType.Word, DescriptorRecognition.Contains, true));
            input.HighlightDescriptors.Add(new HighlightDescriptor("isSecret", Color.Chocolate, null, DescriptorType.Word, DescriptorRecognition.Contains, true));
            input.HighlightDescriptors.Add(new HighlightDescriptor("secretX", Color.Chocolate, null, DescriptorType.Word, DescriptorRecognition.Contains, true));
            input.HighlightDescriptors.Add(new HighlightDescriptor("secretY", Color.Chocolate, null, DescriptorType.Word, DescriptorRecognition.Contains, true));

            // AutoComplete
            input.Text += " ";
            input.Text = input.Text.Remove(input.Text.Length - 1);
        }
    }
}
