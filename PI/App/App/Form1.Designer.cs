namespace App
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioGet = new System.Windows.Forms.RadioButton();
            this.radioSend = new System.Windows.Forms.RadioButton();
            this.hostIp = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.sendMsgBtn = new System.Windows.Forms.Button();
            this.sendMsgText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.getMsgText = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.exchangeKeysTxtB = new System.Windows.Forms.TextBox();
            this.exchangeKeysBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioGet);
            this.groupBox1.Controls.Add(this.radioSend);
            this.groupBox1.Location = new System.Drawing.Point(509, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(202, 88);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Действие";
            // 
            // radioGet
            // 
            this.radioGet.AutoSize = true;
            this.radioGet.Location = new System.Drawing.Point(30, 52);
            this.radioGet.Name = "radioGet";
            this.radioGet.Size = new System.Drawing.Size(159, 17);
            this.radioGet.TabIndex = 9;
            this.radioGet.Text = "Прослушивать сообщения";
            this.radioGet.UseVisualStyleBackColor = true;
            this.radioGet.CheckedChanged += new System.EventHandler(this.radioGet_CheckedChanged);
            // 
            // radioSend
            // 
            this.radioSend.AutoSize = true;
            this.radioSend.Checked = true;
            this.radioSend.Location = new System.Drawing.Point(30, 29);
            this.radioSend.Name = "radioSend";
            this.radioSend.Size = new System.Drawing.Size(139, 17);
            this.radioSend.TabIndex = 8;
            this.radioSend.TabStop = true;
            this.radioSend.Text = "Отправить сообщение";
            this.radioSend.UseVisualStyleBackColor = true;
            this.radioSend.CheckedChanged += new System.EventHandler(this.radioSend_CheckedChanged);
            // 
            // hostIp
            // 
            this.hostIp.Location = new System.Drawing.Point(509, 104);
            this.hostIp.Name = "hostIp";
            this.hostIp.Size = new System.Drawing.Size(202, 20);
            this.hostIp.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(423, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "IP получателя:";
            // 
            // sendMsgBtn
            // 
            this.sendMsgBtn.Location = new System.Drawing.Point(547, 229);
            this.sendMsgBtn.Name = "sendMsgBtn";
            this.sendMsgBtn.Size = new System.Drawing.Size(151, 23);
            this.sendMsgBtn.TabIndex = 10;
            this.sendMsgBtn.Text = "Отправить сообщение";
            this.sendMsgBtn.UseVisualStyleBackColor = true;
            this.sendMsgBtn.Click += new System.EventHandler(this.sendMsgBtn_Click);
            // 
            // sendMsgText
            // 
            this.sendMsgText.Location = new System.Drawing.Point(509, 133);
            this.sendMsgText.Multiline = true;
            this.sendMsgText.Name = "sendMsgText";
            this.sendMsgText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.sendMsgText.Size = new System.Drawing.Size(340, 90);
            this.sendMsgText.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(364, 174);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Сообщение для отправки:";
            // 
            // getMsgText
            // 
            this.getMsgText.Location = new System.Drawing.Point(509, 267);
            this.getMsgText.Multiline = true;
            this.getMsgText.Name = "getMsgText";
            this.getMsgText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.getMsgText.Size = new System.Drawing.Size(340, 116);
            this.getMsgText.TabIndex = 13;
            this.getMsgText.TextChanged += new System.EventHandler(this.getMsgText_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(364, 319);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = " Полученные сообщения:";
            // 
            // exchangeKeysTxtB
            // 
            this.exchangeKeysTxtB.Location = new System.Drawing.Point(47, 32);
            this.exchangeKeysTxtB.Multiline = true;
            this.exchangeKeysTxtB.Name = "exchangeKeysTxtB";
            this.exchangeKeysTxtB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.exchangeKeysTxtB.Size = new System.Drawing.Size(308, 62);
            this.exchangeKeysTxtB.TabIndex = 15;
            // 
            // exchangeKeysBtn
            // 
            this.exchangeKeysBtn.Location = new System.Drawing.Point(145, 100);
            this.exchangeKeysBtn.Name = "exchangeKeysBtn";
            this.exchangeKeysBtn.Size = new System.Drawing.Size(119, 40);
            this.exchangeKeysBtn.TabIndex = 16;
            this.exchangeKeysBtn.Text = "Отослать открытые ключи";
            this.exchangeKeysBtn.UseVisualStyleBackColor = true;
            this.exchangeKeysBtn.Click += new System.EventHandler(this.exchangeKeysBtn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(280, 199);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 454);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.exchangeKeysBtn);
            this.Controls.Add(this.exchangeKeysTxtB);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.getMsgText);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.sendMsgText);
            this.Controls.Add(this.sendMsgBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.hostIp);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioGet;
        private System.Windows.Forms.RadioButton radioSend;
        private System.Windows.Forms.TextBox hostIp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button sendMsgBtn;
        private System.Windows.Forms.TextBox sendMsgText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox getMsgText;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox exchangeKeysTxtB;
        private System.Windows.Forms.Button exchangeKeysBtn;
        private System.Windows.Forms.Button button1;
    }
}

