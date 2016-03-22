using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interpretator
{
    public partial class BrowserF : Form
    {
        public BrowserF()
        {
            InitializeComponent();
        }
        private void BrowserF_Activated(object sender, EventArgs e)
        {
            this.Left = 0;
            this.Top = 0;
        }
	}
}
