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
            this.hostIp = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.sendMsgBtn = new System.Windows.Forms.Button();
            this.sendMsgText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.getMsgText = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.exchangeKeysTxtB = new System.Windows.Forms.TextBox();
            this.btnSynch = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.requestSecretKeyBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // hostIp
            // 
            this.hostIp.Location = new System.Drawing.Point(111, 32);
            this.hostIp.Name = "hostIp";
            this.hostIp.Size = new System.Drawing.Size(247, 20);
            this.hostIp.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "IP получателя:";
            // 
            // sendMsgBtn
            // 
            this.sendMsgBtn.Location = new System.Drawing.Point(692, 170);
            this.sendMsgBtn.Name = "sendMsgBtn";
            this.sendMsgBtn.Size = new System.Drawing.Size(151, 23);
            this.sendMsgBtn.TabIndex = 10;
            this.sendMsgBtn.Text = "Отправить сообщение";
            this.sendMsgBtn.UseVisualStyleBackColor = true;
            this.sendMsgBtn.Click += new System.EventHandler(this.sendMsgBtn_Click);
            // 
            // sendMsgText
            // 
            this.sendMsgText.Location = new System.Drawing.Point(503, 12);
            this.sendMsgText.Multiline = true;
            this.sendMsgText.Name = "sendMsgText";
            this.sendMsgText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.sendMsgText.Size = new System.Drawing.Size(340, 141);
            this.sendMsgText.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(364, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Сообщение для отправки:";
            // 
            // getMsgText
            // 
            this.getMsgText.Location = new System.Drawing.Point(503, 211);
            this.getMsgText.Multiline = true;
            this.getMsgText.Name = "getMsgText";
            this.getMsgText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.getMsgText.Size = new System.Drawing.Size(340, 133);
            this.getMsgText.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(362, 221);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = " Полученные сообщения:";
            // 
            // exchangeKeysTxtB
            // 
            this.exchangeKeysTxtB.Location = new System.Drawing.Point(12, 72);
            this.exchangeKeysTxtB.Multiline = true;
            this.exchangeKeysTxtB.Name = "exchangeKeysTxtB";
            this.exchangeKeysTxtB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.exchangeKeysTxtB.Size = new System.Drawing.Size(346, 133);
            this.exchangeKeysTxtB.TabIndex = 15;
            // 
            // btnSynch
            // 
            this.btnSynch.Location = new System.Drawing.Point(503, 171);
            this.btnSynch.Name = "btnSynch";
            this.btnSynch.Size = new System.Drawing.Size(139, 23);
            this.btnSynch.TabIndex = 18;
            this.btnSynch.Text = "Синхронизация";
            this.btnSynch.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(243, 276);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(161, 23);
            this.button4.TabIndex = 22;
            this.button4.Text = "MySecretKey/Vector";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // requestSecretKeyBtn
            // 
            this.requestSecretKeyBtn.Location = new System.Drawing.Point(111, 211);
            this.requestSecretKeyBtn.Name = "requestSecretKeyBtn";
            this.requestSecretKeyBtn.Size = new System.Drawing.Size(156, 23);
            this.requestSecretKeyBtn.TabIndex = 23;
            this.requestSecretKeyBtn.Text = "Запросить секретный ключ";
            this.requestSecretKeyBtn.UseVisualStyleBackColor = true;
            this.requestSecretKeyBtn.Click += new System.EventHandler(this.requestSecretKeyBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 454);
            this.Controls.Add(this.requestSecretKeyBtn);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btnSynch);
            this.Controls.Add(this.exchangeKeysTxtB);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.getMsgText);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.sendMsgText);
            this.Controls.Add(this.sendMsgBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.hostIp);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox hostIp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button sendMsgBtn;
        private System.Windows.Forms.TextBox sendMsgText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox getMsgText;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox exchangeKeysTxtB;
        private System.Windows.Forms.Button btnSynch;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button requestSecretKeyBtn;
    }
}

