namespace _17.S7_Monitor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bt_history = new System.Windows.Forms.Button();
            this.bt_config = new System.Windows.Forms.Button();
            this.bt_alarm = new System.Windows.Forms.Button();
            this.bt_running = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tb_productName = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tb_productCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lb_message = new System.Windows.Forms.Label();
            this.dgv_home = new System.Windows.Forms.DataGridView();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tm_blink = new System.Windows.Forms.Timer(this.components);
            this.lb_flag = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_home)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // bt_history
            // 
            this.bt_history.BackColor = System.Drawing.Color.DodgerBlue;
            this.bt_history.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bt_history.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.bt_history.ForeColor = System.Drawing.Color.White;
            this.bt_history.Location = new System.Drawing.Point(12, 12);
            this.bt_history.Name = "bt_history";
            this.bt_history.Size = new System.Drawing.Size(222, 92);
            this.bt_history.TabIndex = 2;
            this.bt_history.Text = "Data history";
            this.bt_history.UseVisualStyleBackColor = false;
            this.bt_history.Click += new System.EventHandler(this.bt_history_Click);
            // 
            // bt_config
            // 
            this.bt_config.BackColor = System.Drawing.Color.DodgerBlue;
            this.bt_config.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bt_config.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.bt_config.ForeColor = System.Drawing.Color.White;
            this.bt_config.Location = new System.Drawing.Point(12, 118);
            this.bt_config.Name = "bt_config";
            this.bt_config.Size = new System.Drawing.Size(222, 92);
            this.bt_config.TabIndex = 3;
            this.bt_config.Text = "PLC \r\nComunication";
            this.bt_config.UseVisualStyleBackColor = false;
            this.bt_config.Click += new System.EventHandler(this.bt_config_Click);
            // 
            // bt_alarm
            // 
            this.bt_alarm.BackColor = System.Drawing.Color.DodgerBlue;
            this.bt_alarm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bt_alarm.Enabled = false;
            this.bt_alarm.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.bt_alarm.ForeColor = System.Drawing.Color.White;
            this.bt_alarm.Location = new System.Drawing.Point(12, 226);
            this.bt_alarm.Name = "bt_alarm";
            this.bt_alarm.Size = new System.Drawing.Size(222, 92);
            this.bt_alarm.TabIndex = 4;
            this.bt_alarm.Text = "ALARM \r\nAcknowledge";
            this.bt_alarm.UseVisualStyleBackColor = false;
            this.bt_alarm.Click += new System.EventHandler(this.bt_alarm_Click);
            // 
            // bt_running
            // 
            this.bt_running.BackColor = System.Drawing.Color.Gray;
            this.bt_running.Cursor = System.Windows.Forms.Cursors.Default;
            this.bt_running.Enabled = false;
            this.bt_running.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.bt_running.ForeColor = System.Drawing.Color.White;
            this.bt_running.Location = new System.Drawing.Point(12, 337);
            this.bt_running.Name = "bt_running";
            this.bt_running.Size = new System.Drawing.Size(222, 92);
            this.bt_running.TabIndex = 5;
            this.bt_running.Text = "SYSTEM \r\nRUNNING";
            this.bt_running.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1034, 591);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "V.1.0.1";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.DodgerBlue;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(312, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(210, 66);
            this.label2.TabIndex = 7;
            this.label2.Text = "Product Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel1.Controls.Add(this.tb_productName);
            this.panel1.Location = new System.Drawing.Point(541, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(534, 66);
            this.panel1.TabIndex = 9;
            // 
            // tb_productName
            // 
            this.tb_productName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_productName.Enabled = false;
            this.tb_productName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.tb_productName.ForeColor = System.Drawing.Color.SteelBlue;
            this.tb_productName.Location = new System.Drawing.Point(18, 15);
            this.tb_productName.Name = "tb_productName";
            this.tb_productName.Size = new System.Drawing.Size(495, 38);
            this.tb_productName.TabIndex = 10;
            this.tb_productName.TextChanged += new System.EventHandler(this.tb_productName_TextChanged);
            this.tb_productName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_productName_KeyDown);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel2.Controls.Add(this.tb_productCode);
            this.panel2.Location = new System.Drawing.Point(541, 103);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(534, 66);
            this.panel2.TabIndex = 11;
            // 
            // tb_productCode
            // 
            this.tb_productCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_productCode.Enabled = false;
            this.tb_productCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.tb_productCode.ForeColor = System.Drawing.Color.SteelBlue;
            this.tb_productCode.Location = new System.Drawing.Point(18, 15);
            this.tb_productCode.Name = "tb_productCode";
            this.tb_productCode.Size = new System.Drawing.Size(495, 38);
            this.tb_productCode.TabIndex = 10;
            this.tb_productCode.TextChanged += new System.EventHandler(this.tb_productCode_TextChanged);
            this.tb_productCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_productCode_KeyDown);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.DodgerBlue;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(312, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(210, 66);
            this.label3.TabIndex = 10;
            this.label3.Text = "Product Code";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_message
            // 
            this.lb_message.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_message.BackColor = System.Drawing.Color.DodgerBlue;
            this.lb_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_message.ForeColor = System.Drawing.Color.White;
            this.lb_message.Location = new System.Drawing.Point(312, 226);
            this.lb_message.Name = "lb_message";
            this.lb_message.Size = new System.Drawing.Size(763, 92);
            this.lb_message.TabIndex = 12;
            this.lb_message.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgv_home
            // 
            this.dgv_home.AllowUserToAddRows = false;
            this.dgv_home.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_home.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgv_home.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_home.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Date,
            this.Time,
            this.ProductName,
            this.ProductCode,
            this.Description});
            this.dgv_home.Location = new System.Drawing.Point(312, 357);
            this.dgv_home.Name = "dgv_home";
            this.dgv_home.RowHeadersVisible = false;
            this.dgv_home.Size = new System.Drawing.Size(763, 231);
            this.dgv_home.TabIndex = 13;
            // 
            // Date
            // 
            this.Date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Date.FillWeight = 50F;
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            // 
            // Time
            // 
            this.Time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Time.FillWeight = 40F;
            this.Time.HeaderText = "Time";
            this.Time.Name = "Time";
            // 
            // ProductName
            // 
            this.ProductName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ProductName.FillWeight = 50F;
            this.ProductName.HeaderText = "Product Name";
            this.ProductName.Name = "ProductName";
            // 
            // ProductCode
            // 
            this.ProductCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ProductCode.FillWeight = 50F;
            this.ProductCode.HeaderText = "Product Code ";
            this.ProductCode.Name = "ProductCode";
            // 
            // Description
            // 
            this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            // 
            // dataGridView2
            // 
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.DeepSkyBlue;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dataGridView2.Location = new System.Drawing.Point(312, 337);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.Size = new System.Drawing.Size(763, 22);
            this.dataGridView2.TabIndex = 14;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle9;
            this.Column1.HeaderText = "HOT DEFECT DETECTION DATA LOG";
            this.Column1.Name = "Column1";
            // 
            // tm_blink
            // 
            this.tm_blink.Interval = 500;
            this.tm_blink.Tick += new System.EventHandler(this.tm_blink_Tick);
            // 
            // lb_flag
            // 
            this.lb_flag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lb_flag.BackColor = System.Drawing.Color.DodgerBlue;
            this.lb_flag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_flag.ForeColor = System.Drawing.Color.White;
            this.lb_flag.Location = new System.Drawing.Point(12, 449);
            this.lb_flag.Name = "lb_flag";
            this.lb_flag.Size = new System.Drawing.Size(222, 139);
            this.lb_flag.TabIndex = 15;
            this.lb_flag.Text = "System running = False\r\nSteel defect detected = False\r\nVision controller = False\r" +
    "\nPLC = False\r\nAir presssure pump = False\r\nSensor Trigger = False\r\nAcknowledge = " +
    "False\r\nNo product name or code = False";
            this.lb_flag.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1087, 613);
            this.Controls.Add(this.lb_flag);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dgv_home);
            this.Controls.Add(this.lb_message);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bt_running);
            this.Controls.Add(this.bt_alarm);
            this.Controls.Add(this.bt_config);
            this.Controls.Add(this.bt_history);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SYS : HOT DEFECT DETECTION SYSTEM";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_home)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button bt_history;
        private System.Windows.Forms.Button bt_config;
        private System.Windows.Forms.Button bt_alarm;
        private System.Windows.Forms.Button bt_running;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tb_productName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox tb_productCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lb_message;
        private System.Windows.Forms.DataGridView dgv_home;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.Timer tm_blink;
        private System.Windows.Forms.Label lb_flag;
    }
}

