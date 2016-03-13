using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroRPGLevelEditor2
{
    class AdvancedNode : TreeNode
    {
        public string name;

        public AdvancedNode(string name)
        {
            this.name = name;
            this.Text = name;
            this.Name = name;
        }

        public override string ToString()
        {
            return name;
        }


    }
}
