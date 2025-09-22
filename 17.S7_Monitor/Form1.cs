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
using System.Diagnostics;
using System.Security.Claims;

namespace _17.S7_Monitor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Forms
        public FormData FormData;
        public FormPLC_Comunication FormPLC;

        // PLC Configuration
        public string PLC_IP;
        public short PLC_RACK;
        public short PLC_SLOT;

        // PLC Data
        private bool SystemRunning;
        private bool visionControllerFailure;
        private bool PLCFailure;
        private bool AirPresssurePumpFailure;
        private bool SensorTriggerFailure;
        private bool SteelDefectDetected;
        private bool SwitchOn = true;

        // Flags
        private bool NoProductNameOrCode;
        private bool blink;

        // Database
        public MySQL db;

        // Polling Tasks
        private Task pollingTask;
        private CancellationTokenSource pollingCts;
        private Task timeStampTask;
        private CancellationTokenSource timeStampCts;

        // MySQL Configuration
        private string mySQL_server = "localhost";
        private string mySQL_database = "db_s7_monitor";
        private string mySQL_user = "root";
        private string mySQL_password = "11111111";

        // Form Load Event
        private async void Form1_Load(object sender, EventArgs e)
        {
            FormData = new FormData(this);
            FormPLC = new FormPLC_Comunication(this);

            Console.WriteLine("Load PLC configuration...");
            PLC_IP = FormPLC.ReadIP();
            PLC_RACK = FormPLC.ReadRACK();
            PLC_SLOT = FormPLC.ReadSLOT();

            Console.WriteLine("Connect to database...");
            db = new MySQL(mySQL_server, mySQL_database, mySQL_user, mySQL_password);

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

                    Console.WriteLine("Start polling...");
                    pollingCts = new CancellationTokenSource();
                    pollingTask = StartPollingAsync(pollingCts.Token);

                    Console.WriteLine("Start timestamp checking...");
                    timeStampCts = new CancellationTokenSource();
                    timeStampTask = CheckTimeStampAsync(timeStampCts.Token);

                    Console.WriteLine("Set NoProductNameOrCode to true...");
                    SystemRunning = plc.ReadBit(MapDataBlock.SystemRunning);
                    if (SystemRunning)
                    {
                        bt_running.BackColor = Color.Lime;
                        Console.WriteLine("System is running");
                    }
                    else
                    {
                        bt_running.BackColor = Color.Gray;
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
        
        // Button Click Events
        private void bt_history_Click(object sender, EventArgs e)
        {
            FormData.ShowDialog();
        }
        private void bt_config_Click(object sender, EventArgs e)
        {
            FormPLC.ShowDialog();
        }
        private void bt_alarm_Click(object sender, EventArgs e)
        {
            SetAcknowledge();
        }

        // Polling and Data Handling
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
        private async Task CheckTimeStampAsync(CancellationToken token)
        {
            bool SteelDefectDetected = false;
            bool timeStampSup = false;

            while (true)
            {
                token.ThrowIfCancellationRequested(); // Check if it has been canceled

                using (DataBlock plc = new DataBlock(PLC_IP, PLC_RACK, PLC_SLOT))
                {
                    if (plc.Connect())
                    {
                        bool timeStamp = plc.ReadBit(MapDataBlock.TimeStamp);

                        if (timeStamp != timeStampSup)
                        {
                            timeStampSup = timeStamp;

                            if (timeStamp)
                            {
                                SteelDefectDetected = plc.ReadBit(MapDataBlock.SteelDefectDetected);
                                this.SteelDefectDetected = SteelDefectDetected;

                                if (SteelDefectDetected)
                                {
                                    StampData("Steel defect detected (NG)");
                                    AlarmBlink("Steel defect detected (NG)");
                                }
                                else
                                {
                                    StampData("Good");
                                }
                            }
                        }
                    }
                }

                await Task.Delay(100, token);
            }
        }

        // Alarm and Data Logging
        private void AlarmBlink(string text)
        {
            lb_message.Invoke((Action)(() => lb_message.Text = text));
            blink = true;
            tm_blink.Enabled = true;
        }
        private void ReadDataBlock()
        {
            bool SystemRunning = false;
            bool visionControllerFailure = false;
            bool PLCFailure = false;
            bool AirPresssurePumpFailure = false;
            bool SensorTriggerFailure = false;
            bool SwitchOn = true;

            using (DataBlock plc = new DataBlock(PLC_IP, PLC_RACK, PLC_SLOT))
            {
                if (plc.Connect())
                {
                    bt_config.Invoke((Action)(() => bt_config.BackColor = Color.DodgerBlue));

                    SystemRunning = plc.ReadBit(MapDataBlock.SystemRunning);
                    if (SystemRunning)
                    {
                        bt_running.Invoke((Action)(() => bt_running.Text = "SYSTEM \r\nRUNNING"));
                        bt_running.Invoke((Action)(() => bt_running.BackColor = Color.Lime));
                    }
                    else
                    {
                        bt_running.Invoke((Action)(() => bt_running.Text = "SYSTEM \r\nSTOP"));
                        bt_running.Invoke((Action)(() => bt_running.BackColor = Color.Gray));
                    }
                    //Console.WriteLine($"SystemRunning: {SystemRunning}");

                    visionControllerFailure = plc.ReadBit(MapDataBlock.visionControllerFailure);
                    PLCFailure = plc.ReadBit(MapDataBlock.PLCFailure);
                    AirPresssurePumpFailure = plc.ReadBit(MapDataBlock.AirPresssurePumpFailure);
                    SensorTriggerFailure = plc.ReadBit(MapDataBlock.SensorTriggerFailure);
                    SwitchOn = plc.ReadBit(MapDataBlock.SwitchOn);

                    //Console.WriteLine($"visionControllerFailure: {visionControllerFailure}");
                    //Console.WriteLine($"PLCFailure: {PLCFailure}");
                    //Console.WriteLine($"AirPresssurePumpFailure: {AirPresssurePumpFailure}");
                    //Console.WriteLine($"SensorTriggerFailure: {SensorTriggerFailure}");
                    //Console.WriteLine($"SwitchOn: {SwitchOn}");
                }
                else
                {
                    Console.WriteLine("Connection failed");
                    bt_config.Invoke((Action)(() => bt_config.BackColor = Color.Red));
                    return;
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
                if (visionControllerFailure)
                {
                    StampData("Vision Controller Failure");
                    AlarmBlink("Vision Controller Failure");
                }
                change = true;
            }
            if (this.PLCFailure != PLCFailure)
            {
                this.PLCFailure = PLCFailure;
                if (PLCFailure)
                {
                    StampData("PLC Failure");
                    AlarmBlink("PLC Failure");
                }
                change = true;
            }
            if (this.AirPresssurePumpFailure != AirPresssurePumpFailure)
            {
                this.AirPresssurePumpFailure = AirPresssurePumpFailure;
                if (AirPresssurePumpFailure)
                {
                    StampData("Air Pressure Pump Failure");
                    AlarmBlink("Air Pressure Pump Failure");
                }
                change = true;
            }
            if (this.SensorTriggerFailure != SensorTriggerFailure)
            {
                this.SensorTriggerFailure = SensorTriggerFailure;
                if (SensorTriggerFailure)
                {
                    StampData("Sensor Trigger Failure");
                    AlarmBlink("Sensor Trigger Failure");
                }
                change = true;
            }
            if (this.SwitchOn != SwitchOn)
            {
                this.SwitchOn = SwitchOn;

                if (SwitchOn)
                {
                    this.visionControllerFailure = false;
                    this.PLCFailure = false;
                    this.AirPresssurePumpFailure = false;
                    this.SensorTriggerFailure = false;
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
            status += $"No Product Name Or Code = {NoProductNameOrCode}\r\n";
            status += $"Switch On = {SwitchOn}\r\n";

            //lb_flag.Text = status;
            lb_flag.Invoke((Action)(() => lb_flag.Text = status));
        }
        private void StampData(string description)
        {
            DateTime today = DateTime.Now;
            DateTime now = DateTime.Now;
            string formattedDate = today.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            string formattedTime = now.ToString("HH:mm");

            dgv_home.Invoke((Action)(() => dgv_home.Rows.Insert(0,
                formattedDate, formattedTime, tb_productName.Text, tb_productCode.Text, description)));

            if (dgv_home.Rows.Count > 30)
            {
                dgv_home.Invoke((Action)(() => dgv_home.Rows.RemoveAt(dgv_home.Rows.Count - 1)));
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
        public bool SetAcknowledge()
        {
            Console.WriteLine("Acknowledge alarm...");
            using (DataBlock plc = new DataBlock(PLC_IP, PLC_RACK, PLC_SLOT))
            {
                if (plc.Connect())
                {
                    plc.WriteBit(MapDataBlock.Acknowledge, true);
                    DelaymS(500);
                    plc.WriteBit(MapDataBlock.Acknowledge, false);
                    Console.WriteLine("Alarm acknowledged");

                    blink = false;
                    return true;
                }
                else
                {
                    Console.WriteLine("Connection failed");
                }
            }
            return false;
        }

        // Delay function without freezing UI
        public static void DelaymS(int mS)
        {
            Stopwatch stopwatchDelaymS = new Stopwatch();
            stopwatchDelaymS.Restart();
            while (mS > stopwatchDelaymS.ElapsedMilliseconds)
            {
                if (!stopwatchDelaymS.IsRunning) stopwatchDelaymS.Start();
                Application.DoEvents();
            }
            stopwatchDelaymS.Stop();
        }


        // TextBox Events
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

        // Form Closing Event
        private async void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Stop Polling
            pollingCts.Cancel();
            try
            {
                await pollingTask; // Wait for the task to finish.
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Polling Has been cancelled.");
            }

            timeStampCts.Cancel();
            try
            {
                await timeStampTask; // Wait for the task to finish.
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("timeStamp Has been cancelled.");
            }
        }

        // Blink Timer Event
        private void tm_blink_Tick(object sender, EventArgs e)
        {
            if (blink)
            {
                if (lb_message.BackColor == Color.Red)
                {
                    lb_message.BackColor = Color.DodgerBlue;
                }
                else
                {
                    lb_message.BackColor = Color.Red;
                }
            }
            else
            {
                lb_message.Invoke((Action)(() => lb_message.Text = "System Normal"));
                lb_message.Invoke((Action)(() => lb_message.BackColor = Color.DodgerBlue));
                tm_blink.Enabled = false;
            }   
        }

        // Clear NoProductNameOrCode when user start typing
        private async void tb_productName_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_productName.Text))
            {
                using (DataBlock plc = new DataBlock(PLC_IP, PLC_RACK, PLC_SLOT))
                {
                    if (await plc.ConnectAsync())
                    {
                        plc.WriteBit(MapDataBlock.NoProductNameOrCode, true);
                        NoProductNameOrCode = true;
                    }
                }
                        
            }
        }
        private async void tb_productCode_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_productCode.Text))
            {
                using (DataBlock plc = new DataBlock(PLC_IP, PLC_RACK, PLC_SLOT))
                {
                    if (await plc.ConnectAsync())
                    {
                        plc.WriteBit(MapDataBlock.NoProductNameOrCode, true);
                        NoProductNameOrCode = true;
                    }
                }

            }
        }
    }
}
