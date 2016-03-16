using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroRPGLevelEditor2
{
    public partial class NewMap : Form
    {
        Form1 form;

        public NewMap(Form1 form)
        {
            InitializeComponent();
            this.form = form;
        }

        private void NewMap_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
