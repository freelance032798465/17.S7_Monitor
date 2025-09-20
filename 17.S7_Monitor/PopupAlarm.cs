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
    public partial class PopupAlarm : Form
    {
        public PopupAlarm(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;
        }

        private Form1 form1;
        public string AlarmMessage;

        private void PopupAlarm_Load(object sender, EventArgs e)
        {
            lb_message.Text = AlarmMessage;
        }

        private void bt_yes_Click(object sender, EventArgs e)
        {
            if (form1.SetAcknowledge())
            {
                this.Close();
            }
        }

        private void bt_no_Click(object sender, EventArgs e)
        {

        }
    }
}
