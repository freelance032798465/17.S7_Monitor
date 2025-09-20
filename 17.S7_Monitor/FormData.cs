using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _17.S7_Monitor
{
    public partial class FormData : Form
    {
        public FormData(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;
        }

        private Form1 form1;
    }
}
