namespace _17.S7_Monitor
{
    partial class PopupAlarm
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
            this.lb_message = new System.Windows.Forms.Label();
            this.bt_yes = new System.Windows.Forms.Button();
            this.bt_no = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb_message
            // 
            this.lb_message.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_message.BackColor = System.Drawing.Color.Red;
            this.lb_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_message.ForeColor = System.Drawing.Color.White;
            this.lb_message.Location = new System.Drawing.Point(12, 9);
            this.lb_message.Name = "lb_message";
            this.lb_message.Size = new System.Drawing.Size(708, 107);
            this.lb_message.TabIndex = 13;
            this.lb_message.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bt_yes
            // 
            this.bt_yes.BackColor = System.Drawing.Color.DodgerBlue;
            this.bt_yes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bt_yes.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.bt_yes.ForeColor = System.Drawing.Color.White;
            this.bt_yes.Location = new System.Drawing.Point(12, 136);
            this.bt_yes.Name = "bt_yes";
            this.bt_yes.Size = new System.Drawing.Size(222, 92);
            this.bt_yes.TabIndex = 14;
            this.bt_yes.Text = "Yes";
            this.bt_yes.UseVisualStyleBackColor = false;
            this.bt_yes.Click += new System.EventHandler(this.bt_yes_Click);
            // 
            // bt_no
            // 
            this.bt_no.BackColor = System.Drawing.Color.Red;
            this.bt_no.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bt_no.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.bt_no.ForeColor = System.Drawing.Color.White;
            this.bt_no.Location = new System.Drawing.Point(498, 136);
            this.bt_no.Name = "bt_no";
            this.bt_no.Size = new System.Drawing.Size(222, 92);
            this.bt_no.TabIndex = 15;
            this.bt_no.Text = "No";
            this.bt_no.UseVisualStyleBackColor = false;
            this.bt_no.Click += new System.EventHandler(this.bt_no_Click);
            // 
            // PopupAlarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(732, 243);
            this.Controls.Add(this.bt_no);
            this.Controls.Add(this.bt_yes);
            this.Controls.Add(this.lb_message);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PopupAlarm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PopupAlarm";
            this.Load += new System.EventHandler(this.PopupAlarm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lb_message;
        private System.Windows.Forms.Button bt_yes;
        private System.Windows.Forms.Button bt_no;
    }
}