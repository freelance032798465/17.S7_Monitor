using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestReadDataBlock;
using TestMySQL;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _17.S7_Monitor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public FormData FormData;
        public FormPLC_Comunication FormPLC;
        public string PLC_IP;
        public short PLC_RACK;
        public short PLC_SLOT;

        private bool SystemRunning;
        private bool visionControllerFailure;
        private bool PLCFailure;
        private bool AirPresssurePumpFailure;
        private bool SensorTriggerFailure;
        private bool SteelDefectDetected;

        private bool Acknowledge;
        private bool NoProductNameOrCode;

        public MySQL db;
        private Task pollingTask;
        private CancellationTokenSource pollingCts;

        private async void Form1_Load(object sender, EventArgs e)
        {
            FormData = new FormData(this);
            FormPLC = new FormPLC_Comunication(this);

            Console.WriteLine("Load PLC configuration...");
            PLC_IP = FormPLC.ReadIP();
            PLC_RACK = FormPLC.ReadRACK();
            PLC_SLOT = FormPLC.ReadSLOT();

            db = new MySQL("localhost", "db_s7_monitor", "root", "11111111");

            //return;

            Console.WriteLine("Connect PLC...");
            Console.WriteLine($"PLC IP: {PLC_IP}, RACK: {PLC_RACK}, SLOT: {PLC_SLOT}");
            using (DataBlock plc = new DataBlock(PLC_IP, PLC_RACK, PLC_SLOT))
            {
                if (await plc.ConnectAsync())
                {
                    Console.WriteLine("Connection successful!");

                    tb_productCode.Enabled = true;
                    tb_productName.Enabled = true;
                    bt_alarm.Enabled = true;
                    bt_running.Enabled = true;

                    //Console.WriteLine("Start reading data block...");
                    //tm_read.Enabled = true;

                    pollingCts = new CancellationTokenSource();
                    pollingTask = StartPollingAsync(pollingCts.Token);

                    // Stop Polling
                    //cts.Cancel();
                    //try
                    //{
                    //    await pollingTask; // Wait for the task to finish.
                    //}
                    //catch (OperationCanceledException)
                    //{
                    //    Console.WriteLine("Polling Has been cancelled.");
                    //}

                    Console.WriteLine("Set NoProductNameOrCode to true...");
                    SystemRunning = plc.ReadBit(MapDataBlock.SystemRunning);
                    if (SystemRunning)
                    {
                        bt_running.BackColor = Color.Lime;
                        Console.WriteLine("System is running");
                    }
                    else
                    {
                        bt_running.BackColor = Color.Red;
                        Console.WriteLine("System is not running");
                    }

                    plc.WriteBit(MapDataBlock.NoProductNameOrCode, true);
                    NoProductNameOrCode = true;
                    Console.WriteLine("NoProductNameOrCode set to true");

                    Console.WriteLine("Clear Product Name and Product Code...");
                    plc.WriteString(1, MapDataBlock.ProductName, 200, "");
                    plc.WriteString(1, MapDataBlock.ProductCode, 200, "");
                }
                else
                {
                    Console.WriteLine("Connection failed");
                    MessageBox.Show("Connection to PLC failed! Please check the PLC IP, RACK, SLOT settings.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void bt_history_Click(object sender, EventArgs e)
        {
            FormData.ShowDialog();
        }

        private void bt_config_Click(object sender, EventArgs e)
        {
            FormPLC.ShowDialog();
        }

        private void tm_read_Tick(object sender, EventArgs e)
        {
            tm_read.Enabled = false;
            //ReadDataBlock();
            //DisplayStatus();
            //tm_read.Enabled = true;
        }
        private async Task StartPollingAsync(CancellationToken token)
        {
            while (true)
            {
                token.ThrowIfCancellationRequested(); // Check if it has been canceled

                ReadDataBlock();
                DisplayStatus();

                await Task.Delay(1000, token);
            }
        }

        private void ReadDataBlock()
        {
            bool SystemRunning = false;
            bool visionControllerFailure = false;
            bool PLCFailure = false;
            bool AirPresssurePumpFailure = false;
            bool SensorTriggerFailure = false;
            bool SteelDefectDetected = false;

            using (DataBlock plc = new DataBlock(PLC_IP, PLC_RACK, PLC_SLOT))
            {
                if (plc.Connect())
                {
                    SystemRunning = plc.ReadBit(MapDataBlock.SystemRunning);
                    if (SystemRunning)
                    {
                        //bt_running.BackColor = Color.Lime;
                        bt_running.Invoke((Action)(() => bt_running.BackColor = Color.Lime));
                    }
                    else
                    {
                        //bt_running.BackColor = Color.Red;
                        bt_running.Invoke((Action)(() => bt_running.BackColor = Color.Red));
                    }

                    visionControllerFailure = plc.ReadBit(MapDataBlock.visionControllerFailure);
                    PLCFailure = plc.ReadBit(MapDataBlock.PLCFailure);
                    AirPresssurePumpFailure = plc.ReadBit(MapDataBlock.AirPresssurePumpFailure);
                    SensorTriggerFailure = plc.ReadBit(MapDataBlock.SensorTriggerFailure);

                    SteelDefectDetected = plc.ReadBit(MapDataBlock.SteelDefectDetected);

                    if (visionControllerFailure || PLCFailure || AirPresssurePumpFailure ||
                        SensorTriggerFailure || SteelDefectDetected)
                    {
                        string message = "";
                        if (visionControllerFailure)
                        {
                            message += "-Vision Controller Failure, ";
                        }
                        if (PLCFailure)
                        {
                            message += "-PLC Failure, ";
                        }
                        if (AirPresssurePumpFailure)
                        {
                            message += "-Air Pressure Pump Failure, ";
                        }
                        if (SensorTriggerFailure)
                        {
                            message += "-Sensor Trigger Failure, ";
                        }
                        if (SteelDefectDetected)
                        {
                            message += "-Steel defect detected (NG), ";
                        }

                        message = message.Trim().TrimEnd(',');
                        //lb_message.Text = message;
                        //lb_message.BackColor = Color.Red;
                        lb_message.Invoke((Action)(() => lb_message.Text = message));
                        lb_message.Invoke((Action)(() => lb_message.BackColor = Color.Red));
                    }
                    else
                    {
                        //lb_message.Text = "System Normal";
                        //lb_message.BackColor = Color.DodgerBlue;
                        lb_message.Invoke((Action)(() => lb_message.Text = "System Normal"));
                        lb_message.Invoke((Action)(() => lb_message.BackColor = Color.DodgerBlue));
                    }

                    if (SteelDefectDetected)
                    {
                        PopupAlarm PopupAlarm = new PopupAlarm(this);
                        PopupAlarm.AlarmMessage = "Steel defect detected (NG)";
                        PopupAlarm.ShowDialog();
                    }
                }
                else
                {
                    Console.WriteLine("Connection failed");
                    //bt_running.BackColor = Color.Red;
                    bt_running.Invoke((Action)(() => bt_running.BackColor = Color.Red));
                }
            }

            bool change = false;
            if (this.SystemRunning != SystemRunning)
            {
                this.SystemRunning = SystemRunning;
                StampData(SystemRunning ? "System Start" : "System Stop");
                change = true;
            }
            if (this.visionControllerFailure != visionControllerFailure)
            {
                this.visionControllerFailure = visionControllerFailure;
                if (visionControllerFailure) StampData("Vision Controller Failure");
                change = true;
            }
            if (this.PLCFailure != PLCFailure)
            {
                this.PLCFailure = PLCFailure;
                if (PLCFailure) StampData("PLC Failure");
                change = true;
            }
            if (this.AirPresssurePumpFailure != AirPresssurePumpFailure)
            {
                this.AirPresssurePumpFailure = AirPresssurePumpFailure;
                if (AirPresssurePumpFailure) StampData("Air Pressure Pump Failure");
                change = true;
            }
            if (this.SensorTriggerFailure != SensorTriggerFailure)
            {
                this.SensorTriggerFailure = SensorTriggerFailure;
                if (SensorTriggerFailure) StampData("Sensor Trigger Failure");
                change = true;
            }
            if (this.SteelDefectDetected != SteelDefectDetected)
            {
                this.SteelDefectDetected = SteelDefectDetected;
                if (SteelDefectDetected) StampData("Steel defect detected (NG)");
                change = true;
            }
            if (change)
            {
                if (SystemRunning && !visionControllerFailure && !PLCFailure && 
                    !AirPresssurePumpFailure && !SensorTriggerFailure && !SteelDefectDetected)
                {
                    StampData("Good");
                }
            }
        }
        private void DisplayStatus()
        {
            string status = $"System Running = {SystemRunning}\r\n";
            status += $"Vision Controller = {visionControllerFailure}\r\n";
            status += $"PLC = {PLCFailure}\r\n";
            status += $"Air Presssure Pump = {AirPresssurePumpFailure}\r\n";
            status += $"Sensor Trigger = {SensorTriggerFailure}\r\n";
            status += $"Steel Defect Detected = {SteelDefectDetected}\r\n";
            status += $"Acknowledge = {Acknowledge}\r\n";
            status += $"No Product Name Or Code = {NoProductNameOrCode}\r\n";

            //lb_flag.Text = status;
            lb_flag.Invoke((Action)(() => lb_flag.Text = status));
        }

        private void bt_alarm_Click(object sender, EventArgs e)
        {
            SetAcknowledge();
        }
        public bool SetAcknowledge()
        {
            Console.WriteLine("Acknowledge alarm...");
            using (DataBlock plc = new DataBlock(PLC_IP, PLC_RACK, PLC_SLOT))
            {
                if (plc.Connect())
                {
                    plc.WriteBit(MapDataBlock.Acknowledge, true);
                    Acknowledge = true;
                    Console.WriteLine("Alarm acknowledged");
                    return true;
                }
                else
                {
                    Console.WriteLine("Connection failed");
                }
            }
            return false;
        }
        private async void tb_productName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                Console.WriteLine("Set Product Name...");
                using (DataBlock plc = new DataBlock(PLC_IP, PLC_RACK, PLC_SLOT))
                {
                    if (await plc.ConnectAsync())
                    {
                        plc.WriteString(1, MapDataBlock.ProductName, 200, tb_productName.Text);
                        Console.WriteLine($"Product Name set to: {tb_productName.Text}");

                        if (!string.IsNullOrEmpty(tb_productCode.Text))
                        {
                            plc.WriteBit(MapDataBlock.NoProductNameOrCode, false);
                            NoProductNameOrCode = false;
                            Console.WriteLine("NoProductNameOrCode set to false");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Connection failed");
                    }
                }
            }
        }
        private async void tb_productCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                Console.WriteLine("Set Product Name...");
                using (DataBlock plc = new DataBlock(PLC_IP, PLC_RACK, PLC_SLOT))
                {
                    if (await plc.ConnectAsync())
                    {
                        plc.WriteString(1, MapDataBlock.ProductCode, 200, tb_productCode.Text);
                        Console.WriteLine($"Product Name set to: {tb_productCode.Text}");

                        if (!string.IsNullOrEmpty(tb_productName.Text))
                        {
                            plc.WriteBit(MapDataBlock.NoProductNameOrCode, false);
                            NoProductNameOrCode = false;
                            Console.WriteLine("NoProductNameOrCode set to false");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Connection failed");
                    }
                }
            }
        }

        private void StampData(string description)
        {
            DateTime today = DateTime.Now;
            DateTime now = DateTime.Now;
            string formattedDate = today.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            string formattedTime = now.ToString("HH:mm");

            //dgv_home.Rows.Add(formattedDate, formattedTime, tb_productName.Text, tb_productCode.Text, description);
            dgv_home.Invoke((Action)(() => dgv_home.Rows.Add(formattedDate, formattedTime, 
                tb_productName.Text, tb_productCode.Text, description)));

            if (dgv_home.Rows.Count > 8)
            {
                //dgv_home.Rows.RemoveAt(0);
                dgv_home.Invoke((Action)(() => dgv_home.Rows.RemoveAt(0)));
            }

            //database insert
            var newLog = new TableLog
            {
                Date = new DateTime(today.Year, today.Month, today.Day),
                Time = now.TimeOfDay,
                ProductName = tb_productName.Text,
                ProductCode = tb_productCode.Text,
                Description = description
            };
            db.InsertLog(newLog);
        }
    }
}
