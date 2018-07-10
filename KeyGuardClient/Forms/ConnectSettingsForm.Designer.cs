namespace KeyGuardClient
{
    partial class ConnectSettingsForm
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
            this.okbutton = new System.Windows.Forms.Button();
            this.cancelbutton = new System.Windows.Forms.Button();
            this.ipAddrLabel = new System.Windows.Forms.Label();
            this.portLabel = new System.Windows.Forms.Label();
            this.ipAddrmaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.portmaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // okbutton
            // 
            this.okbutton.Location = new System.Drawing.Point(63, 143);
            this.okbutton.Name = "okbutton";
            this.okbutton.Size = new System.Drawing.Size(70, 30);
            this.okbutton.TabIndex = 0;
            this.okbutton.Text = "OK";
            this.okbutton.UseVisualStyleBackColor = true;
            this.okbutton.Click += new System.EventHandler(this.okbutton_Click);
            // 
            // cancelbutton
            // 
            this.cancelbutton.Location = new System.Drawing.Point(160, 143);
            this.cancelbutton.Name = "cancelbutton";
            this.cancelbutton.Size = new System.Drawing.Size(70, 30);
            this.cancelbutton.TabIndex = 0;
            this.cancelbutton.Text = "Отмена";
            this.cancelbutton.UseVisualStyleBackColor = true;
            this.cancelbutton.Click += new System.EventHandler(this.cancelbutton_Click);
            // 
            // ipAddrLabel
            // 
            this.ipAddrLabel.AutoSize = true;
            this.ipAddrLabel.Location = new System.Drawing.Point(45, 44);
            this.ipAddrLabel.Name = "ipAddrLabel";
            this.ipAddrLabel.Size = new System.Drawing.Size(53, 13);
            this.ipAddrLabel.TabIndex = 2;
            this.ipAddrLabel.Text = "IP адрес:";
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(63, 90);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(35, 13);
            this.portLabel.TabIndex = 2;
            this.portLabel.Text = "Порт:";
            // 
            // ipAddrmaskedTextBox
            // 
            this.ipAddrmaskedTextBox.Location = new System.Drawing.Point(113, 41);
            this.ipAddrmaskedTextBox.Mask = "000.999.999.999";
            this.ipAddrmaskedTextBox.Name = "ipAddrmaskedTextBox";
            this.ipAddrmaskedTextBox.Size = new System.Drawing.Size(89, 20);
            this.ipAddrmaskedTextBox.TabIndex = 3;
            // 
            // portmaskedTextBox
            // 
            this.portmaskedTextBox.Location = new System.Drawing.Point(113, 85);
            this.portmaskedTextBox.Mask = "0000";
            this.portmaskedTextBox.Name = "portmaskedTextBox";
            this.portmaskedTextBox.Size = new System.Drawing.Size(38, 20);
            this.portmaskedTextBox.TabIndex = 3;
            // 
            // ConnectSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 198);
            this.Controls.Add(this.portmaskedTextBox);
            this.Controls.Add(this.ipAddrmaskedTextBox);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.ipAddrLabel);
            this.Controls.Add(this.cancelbutton);
            this.Controls.Add(this.okbutton);
            this.Name = "ConnectSettingsForm";
            this.Text = "ConnectSettingsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okbutton;
        private System.Windows.Forms.Button cancelbutton;
        private System.Windows.Forms.Label ipAddrLabel;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.MaskedTextBox portmaskedTextBox;
        public System.Windows.Forms.MaskedTextBox ipAddrmaskedTextBox;
    }
}