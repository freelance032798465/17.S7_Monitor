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
    public partial class FormPLC_Comunication : Form
    {
        public FormPLC_Comunication(Form1 form)
        {
            InitializeComponent();
            this.form = form;
        }

        private Form1 form;
        private ConfigManager config = new ConfigManager("config.ini");

        private void FormPLC_Comunication_Load(object sender, EventArgs e)
        {
            tb_ip.Text = ReadIP();
            tb_rack.Text = ReadRACK().ToString();
            tb_slot.Text = ReadSLOT().ToString();
        }

        public string ReadIP()
        {
            return config.Read("PLC", "IP", "192.168.1.10");
        }
        public short ReadRACK()
        {
            return Convert.ToInt16(config.Read("PLC", "Rack", "0"));
        }
        public short ReadSLOT()
        {
            return Convert.ToInt16(config.Read("PLC", "Slot", "1"));
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            config.Write("PLC", "IP", tb_ip.Text);
            config.Write("PLC", "Rack", tb_rack.Text);
            config.Write("PLC", "Slot", tb_slot.Text);

            form.PLC_IP = tb_ip.Text;
            form.PLC_RACK = Convert.ToInt16(tb_rack.Text);
            form.PLC_SLOT = Convert.ToInt16(tb_slot.Text);

            MessageBox.Show("Configuration saved. Please restart the application for " +
                "changes to take effect.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
