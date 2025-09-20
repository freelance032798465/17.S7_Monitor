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
    public partial class FormFilterDate : Form
    {
        public FormFilterDate()
        {
            InitializeComponent();
        }

        private void FormFilterDate_Load(object sender, EventArgs e)
        {

        }

        private void dateStart_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void dateEnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
