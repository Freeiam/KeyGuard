namespace KeyGuardClient
{
    partial class MainSettingsForm
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
            this.maingroupBox = new System.Windows.Forms.GroupBox();
            this.keysInfoLabel = new System.Windows.Forms.Label();
            this.addLevelToKeysButton = new System.Windows.Forms.Button();
            this.snif = new System.Windows.Forms.TextBox();
            this.keyComboBox = new System.Windows.Forms.ComboBox();
            this.dateZoneComboBox = new System.Windows.Forms.ComboBox();
            this.receivedByteLabel = new System.Windows.Forms.Label();
            this.keyLabel = new System.Windows.Forms.Label();
            this.dateZoneLabel = new System.Windows.Forms.Label();
            this.labelCmd_T = new System.Windows.Forms.Label();
            this.labelIdent = new System.Windows.Forms.Label();
            this.labelValue = new System.Windows.Forms.Label();
            this.dataLabel = new System.Windows.Forms.Label();
            this.buttonSendCommand = new System.Windows.Forms.Button();
            this.textBoxCmd_T = new System.Windows.Forms.TextBox();
            this.textBoxData = new System.Windows.Forms.TextBox();
            this.textBoxIdent = new System.Windows.Forms.TextBox();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.timerText = new System.Windows.Forms.Timer(this.components);
            this.maingroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // maingroupBox
            // 
            this.maingroupBox.Controls.Add(this.keysInfoLabel);
            this.maingroupBox.Controls.Add(this.addLevelToKeysButton);
            this.maingroupBox.Controls.Add(this.snif);
            this.maingroupBox.Controls.Add(this.keyComboBox);
            this.maingroupBox.Controls.Add(this.dateZoneComboBox);
            this.maingroupBox.Controls.Add(this.receivedByteLabel);
            this.maingroupBox.Controls.Add(this.keyLabel);
            this.maingroupBox.Controls.Add(this.dateZoneLabel);
            this.maingroupBox.Controls.Add(this.labelCmd_T);
            this.maingroupBox.Controls.Add(this.labelIdent);
            this.maingroupBox.Controls.Add(this.labelValue);
            this.maingroupBox.Controls.Add(this.dataLabel);
            this.maingroupBox.Controls.Add(this.buttonSendCommand);
            this.maingroupBox.Controls.Add(this.textBoxCmd_T);
            this.maingroupBox.Controls.Add(this.textBoxData);
            this.maingroupBox.Controls.Add(this.textBoxIdent);
            this.maingroupBox.Controls.Add(this.textBoxValue);
            this.maingroupBox.Location = new System.Drawing.Point(12, 12);
            this.maingroupBox.Name = "maingroupBox";
            this.maingroupBox.Size = new System.Drawing.Size(1147, 423);
            this.maingroupBox.TabIndex = 1;
            this.maingroupBox.TabStop = false;
            // 
            // keysInfoLabel
            // 
            this.keysInfoLabel.AutoSize = true;
            this.keysInfoLabel.Location = new System.Drawing.Point(611, 149);
            this.keysInfoLabel.Name = "keysInfoLabel";
            this.keysInfoLabel.Size = new System.Drawing.Size(0, 13);
            this.keysInfoLabel.TabIndex = 17;
            // 
            // addLevelToKeysButton
            // 
            this.addLevelToKeysButton.Location = new System.Drawing.Point(704, 113);
            this.addLevelToKeysButton.Name = "addLevelToKeysButton";
            this.addLevelToKeysButton.Size = new System.Drawing.Size(158, 23);
            this.addLevelToKeysButton.TabIndex = 16;
            this.addLevelToKeysButton.Text = "Добавить уровень доступа";
            this.addLevelToKeysButton.UseVisualStyleBackColor = true;
            this.addLevelToKeysButton.Click += new System.EventHandler(this.addLevelToKeysButton_Click);
            // 
            // snif
            // 
            this.snif.Location = new System.Drawing.Point(17, 27);
            this.snif.Multiline = true;
            this.snif.Name = "snif";
            this.snif.Size = new System.Drawing.Size(321, 194);
            this.snif.TabIndex = 1;
            // 
            // keyComboBox
            // 
            this.keyComboBox.FormattingEnabled = true;
            this.keyComboBox.Location = new System.Drawing.Point(468, 141);
            this.keyComboBox.Name = "keyComboBox";
            this.keyComboBox.Size = new System.Drawing.Size(121, 21);
            this.keyComboBox.TabIndex = 12;
            this.keyComboBox.SelectedIndexChanged += new System.EventHandler(this.keyComboBox_SelectedIndexChanged);
            // 
            // dateZoneComboBox
            // 
            this.dateZoneComboBox.FormattingEnabled = true;
            this.dateZoneComboBox.Location = new System.Drawing.Point(468, 93);
            this.dateZoneComboBox.Name = "dateZoneComboBox";
            this.dateZoneComboBox.Size = new System.Drawing.Size(121, 21);
            this.dateZoneComboBox.TabIndex = 12;
            // 
            // receivedByteLabel
            // 
            this.receivedByteLabel.AutoSize = true;
            this.receivedByteLabel.Location = new System.Drawing.Point(105, 10);
            this.receivedByteLabel.Name = "receivedByteLabel";
            this.receivedByteLabel.Size = new System.Drawing.Size(0, 13);
            this.receivedByteLabel.TabIndex = 2;
            // 
            // keyLabel
            // 
            this.keyLabel.AutoSize = true;
            this.keyLabel.Location = new System.Drawing.Point(407, 144);
            this.keyLabel.Name = "keyLabel";
            this.keyLabel.Size = new System.Drawing.Size(42, 13);
            this.keyLabel.TabIndex = 11;
            this.keyLabel.Text = "Ключи:";
            // 
            // dateZoneLabel
            // 
            this.dateZoneLabel.AutoSize = true;
            this.dateZoneLabel.Location = new System.Drawing.Point(355, 96);
            this.dateZoneLabel.Name = "dateZoneLabel";
            this.dateZoneLabel.Size = new System.Drawing.Size(94, 13);
            this.dateZoneLabel.TabIndex = 11;
            this.dateZoneLabel.Text = "Временная зона:";
            // 
            // labelCmd_T
            // 
            this.labelCmd_T.AutoSize = true;
            this.labelCmd_T.Location = new System.Drawing.Point(366, 43);
            this.labelCmd_T.Name = "labelCmd_T";
            this.labelCmd_T.Size = new System.Drawing.Size(41, 13);
            this.labelCmd_T.TabIndex = 3;
            this.labelCmd_T.Text = "Cmd_T";
            // 
            // labelIdent
            // 
            this.labelIdent.AutoSize = true;
            this.labelIdent.Location = new System.Drawing.Point(451, 43);
            this.labelIdent.Name = "labelIdent";
            this.labelIdent.Size = new System.Drawing.Size(31, 13);
            this.labelIdent.TabIndex = 3;
            this.labelIdent.Text = "Ident";
            // 
            // labelValue
            // 
            this.labelValue.AutoSize = true;
            this.labelValue.Location = new System.Drawing.Point(522, 43);
            this.labelValue.Name = "labelValue";
            this.labelValue.Size = new System.Drawing.Size(34, 13);
            this.labelValue.TabIndex = 3;
            this.labelValue.Text = "Value";
            // 
            // dataLabel
            // 
            this.dataLabel.AutoSize = true;
            this.dataLabel.Location = new System.Drawing.Point(616, 43);
            this.dataLabel.Name = "dataLabel";
            this.dataLabel.Size = new System.Drawing.Size(30, 13);
            this.dataLabel.TabIndex = 3;
            this.dataLabel.Text = "Data";
            // 
            // buttonSendCommand
            // 
            this.buttonSendCommand.Location = new System.Drawing.Point(704, 61);
            this.buttonSendCommand.Name = "buttonSendCommand";
            this.buttonSendCommand.Size = new System.Drawing.Size(75, 23);
            this.buttonSendCommand.TabIndex = 7;
            this.buttonSendCommand.Text = "Send";
            this.buttonSendCommand.UseVisualStyleBackColor = true;
            this.buttonSendCommand.Click += new System.EventHandler(this.buttonSendCommand_Click);
            // 
            // textBoxCmd_T
            // 
            this.textBoxCmd_T.Location = new System.Drawing.Point(358, 61);
            this.textBoxCmd_T.MaxLength = 2;
            this.textBoxCmd_T.Name = "textBoxCmd_T";
            this.textBoxCmd_T.Size = new System.Drawing.Size(67, 20);
            this.textBoxCmd_T.TabIndex = 4;
            // 
            // textBoxData
            // 
            this.textBoxData.Location = new System.Drawing.Point(577, 61);
            this.textBoxData.MaxLength = 2;
            this.textBoxData.Name = "textBoxData";
            this.textBoxData.Size = new System.Drawing.Size(102, 20);
            this.textBoxData.TabIndex = 6;
            // 
            // textBoxIdent
            // 
            this.textBoxIdent.Location = new System.Drawing.Point(431, 61);
            this.textBoxIdent.MaxLength = 2;
            this.textBoxIdent.Name = "textBoxIdent";
            this.textBoxIdent.Size = new System.Drawing.Size(67, 20);
            this.textBoxIdent.TabIndex = 5;
            // 
            // textBoxValue
            // 
            this.textBoxValue.Location = new System.Drawing.Point(504, 61);
            this.textBoxValue.MaxLength = 2;
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(67, 20);
            this.textBoxValue.TabIndex = 6;
            // 
            // timerText
            // 
            this.timerText.Enabled = true;
            this.timerText.Interval = 1000;
            this.timerText.Tick += new System.EventHandler(this.timerText_Tick);
            // 
            // MainSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 466);
            this.Controls.Add(this.maingroupBox);
            this.Name = "MainSettingsForm";
            this.Text = "MainSettingsForm";
            this.maingroupBox.ResumeLayout(false);
            this.maingroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox maingroupBox;
        private System.Windows.Forms.TextBox snif;
        private System.Windows.Forms.Label receivedByteLabel;
        private System.Windows.Forms.Label labelCmd_T;
        private System.Windows.Forms.Label labelIdent;
        private System.Windows.Forms.Label labelValue;
        private System.Windows.Forms.Label dataLabel;
        private System.Windows.Forms.Button buttonSendCommand;
        private System.Windows.Forms.TextBox textBoxCmd_T;
        private System.Windows.Forms.TextBox textBoxData;
        private System.Windows.Forms.TextBox textBoxIdent;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.Timer timerText;
        private System.Windows.Forms.ComboBox dateZoneComboBox;
        private System.Windows.Forms.Label dateZoneLabel;
        private System.Windows.Forms.ComboBox keyComboBox;
        private System.Windows.Forms.Label keyLabel;
        private System.Windows.Forms.Button addLevelToKeysButton;
        private System.Windows.Forms.Label keysInfoLabel;
    }
}