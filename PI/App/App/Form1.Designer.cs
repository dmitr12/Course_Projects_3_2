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
            this.label1 = new System.Windows.Forms.Label();
            this.forEncryptTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.afterEncryptTxt = new System.Windows.Forms.TextBox();
            this.encryptBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.decryptTxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ipTxt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.msgSendTcpTxt = new System.Windows.Forms.TextBox();
            this.sendTcpBtn = new System.Windows.Forms.Button();
            this.msgTxtFromTcp = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Для шифрования:";
            // 
            // forEncryptTxt
            // 
            this.forEncryptTxt.Location = new System.Drawing.Point(137, 29);
            this.forEncryptTxt.Name = "forEncryptTxt";
            this.forEncryptTxt.Size = new System.Drawing.Size(216, 20);
            this.forEncryptTxt.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "После шифрования:";
            // 
            // afterEncryptTxt
            // 
            this.afterEncryptTxt.Location = new System.Drawing.Point(137, 73);
            this.afterEncryptTxt.Name = "afterEncryptTxt";
            this.afterEncryptTxt.Size = new System.Drawing.Size(216, 20);
            this.afterEncryptTxt.TabIndex = 3;
            // 
            // encryptBtn
            // 
            this.encryptBtn.Location = new System.Drawing.Point(188, 111);
            this.encryptBtn.Name = "encryptBtn";
            this.encryptBtn.Size = new System.Drawing.Size(100, 23);
            this.encryptBtn.TabIndex = 4;
            this.encryptBtn.Text = "Зашифровать";
            this.encryptBtn.UseVisualStyleBackColor = true;
            this.encryptBtn.Click += new System.EventHandler(this.encryptBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "После дешифрования:";
            // 
            // decryptTxt
            // 
            this.decryptTxt.Location = new System.Drawing.Point(137, 153);
            this.decryptTxt.Name = "decryptTxt";
            this.decryptTxt.Size = new System.Drawing.Size(216, 20);
            this.decryptTxt.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(609, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Проверка tcp";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(518, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "айпи хоста";
            // 
            // ipTxt
            // 
            this.ipTxt.Location = new System.Drawing.Point(612, 54);
            this.ipTxt.Name = "ipTxt";
            this.ipTxt.Size = new System.Drawing.Size(253, 20);
            this.ipTxt.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(528, 91);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Сообщение";
            // 
            // msgSendTcpTxt
            // 
            this.msgSendTcpTxt.Location = new System.Drawing.Point(612, 88);
            this.msgSendTcpTxt.Name = "msgSendTcpTxt";
            this.msgSendTcpTxt.Size = new System.Drawing.Size(253, 20);
            this.msgSendTcpTxt.TabIndex = 11;
            // 
            // sendTcpBtn
            // 
            this.sendTcpBtn.Location = new System.Drawing.Point(635, 124);
            this.sendTcpBtn.Name = "sendTcpBtn";
            this.sendTcpBtn.Size = new System.Drawing.Size(182, 23);
            this.sendTcpBtn.TabIndex = 12;
            this.sendTcpBtn.Text = "Отправить сбщ";
            this.sendTcpBtn.UseVisualStyleBackColor = true;
            this.sendTcpBtn.Click += new System.EventHandler(this.sendTcpBtn_Click);
            // 
            // msgTxtFromTcp
            // 
            this.msgTxtFromTcp.Location = new System.Drawing.Point(453, 167);
            this.msgTxtFromTcp.Multiline = true;
            this.msgTxtFromTcp.Name = "msgTxtFromTcp";
            this.msgTxtFromTcp.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.msgTxtFromTcp.Size = new System.Drawing.Size(412, 140);
            this.msgTxtFromTcp.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 454);
            this.Controls.Add(this.msgTxtFromTcp);
            this.Controls.Add(this.sendTcpBtn);
            this.Controls.Add(this.msgSendTcpTxt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ipTxt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.decryptTxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.encryptBtn);
            this.Controls.Add(this.afterEncryptTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.forEncryptTxt);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox forEncryptTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox afterEncryptTxt;
        private System.Windows.Forms.Button encryptBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox decryptTxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ipTxt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox msgSendTcpTxt;
        private System.Windows.Forms.Button sendTcpBtn;
        private System.Windows.Forms.TextBox msgTxtFromTcp;
    }
}

