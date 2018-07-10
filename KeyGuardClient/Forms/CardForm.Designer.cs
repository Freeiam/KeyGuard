namespace KeyGuardClient
{
    partial class CardForm
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
            this.cardGroupBox = new System.Windows.Forms.GroupBox();
            this.cardsLabel = new System.Windows.Forms.Label();
            this.mainGridView = new System.Windows.Forms.DataGridView();
            this.UserNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Issue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Valid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KeyZone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FisNumCard = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cardBox = new System.Windows.Forms.ComboBox();
            this.addCardButton = new System.Windows.Forms.Button();
            this.namePanel = new System.Windows.Forms.Panel();
            this.surnameLabel = new System.Windows.Forms.Label();
            this.surnameTextBox = new System.Windows.Forms.TextBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.patronymTextBox = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.patronymLabel = new System.Windows.Forms.Label();
            this.cardPanel = new System.Windows.Forms.Panel();
            this.cardmaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.lKeysBox = new System.Windows.Forms.ComboBox();
            this.levelTKeysLabel = new System.Windows.Forms.Label();
            this.cardLabel = new System.Windows.Forms.Label();
            this.cardGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainGridView)).BeginInit();
            this.namePanel.SuspendLayout();
            this.cardPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // cardGroupBox
            // 
            this.cardGroupBox.Controls.Add(this.cardsLabel);
            this.cardGroupBox.Controls.Add(this.mainGridView);
            this.cardGroupBox.Controls.Add(this.cardBox);
            this.cardGroupBox.Controls.Add(this.addCardButton);
            this.cardGroupBox.Controls.Add(this.namePanel);
            this.cardGroupBox.Controls.Add(this.cardPanel);
            this.cardGroupBox.Location = new System.Drawing.Point(8, 8);
            this.cardGroupBox.Name = "cardGroupBox";
            this.cardGroupBox.Size = new System.Drawing.Size(1145, 451);
            this.cardGroupBox.TabIndex = 0;
            this.cardGroupBox.TabStop = false;
            this.cardGroupBox.Text = "Владельцы карт";
            // 
            // cardsLabel
            // 
            this.cardsLabel.AutoSize = true;
            this.cardsLabel.Location = new System.Drawing.Point(541, 237);
            this.cardsLabel.Name = "cardsLabel";
            this.cardsLabel.Size = new System.Drawing.Size(45, 13);
            this.cardsLabel.TabIndex = 11;
            this.cardsLabel.Text = "Карты: ";
            // 
            // mainGridView
            // 
            this.mainGridView.AllowUserToAddRows = false;
            this.mainGridView.AllowUserToDeleteRows = false;
            this.mainGridView.AllowUserToOrderColumns = true;
            this.mainGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mainGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UserNumber,
            this.FIO,
            this.CardNumber,
            this.Issue,
            this.Valid,
            this.KeyZone,
            this.FisNumCard});
            this.mainGridView.Location = new System.Drawing.Point(25, 282);
            this.mainGridView.Name = "mainGridView";
            this.mainGridView.ReadOnly = true;
            this.mainGridView.Size = new System.Drawing.Size(784, 150);
            this.mainGridView.TabIndex = 10;
            // 
            // UserNumber
            // 
            this.UserNumber.HeaderText = "№№";
            this.UserNumber.Name = "UserNumber";
            this.UserNumber.ReadOnly = true;
            this.UserNumber.Width = 40;
            // 
            // FIO
            // 
            this.FIO.HeaderText = "Ф.И.О.";
            this.FIO.Name = "FIO";
            this.FIO.ReadOnly = true;
            // 
            // CardNumber
            // 
            this.CardNumber.HeaderText = "№ Карты";
            this.CardNumber.Name = "CardNumber";
            this.CardNumber.ReadOnly = true;
            // 
            // Issue
            // 
            this.Issue.HeaderText = "Выдано";
            this.Issue.Name = "Issue";
            this.Issue.ReadOnly = true;
            // 
            // Valid
            // 
            this.Valid.HeaderText = "Действительно";
            this.Valid.Name = "Valid";
            this.Valid.ReadOnly = true;
            // 
            // KeyZone
            // 
            this.KeyZone.HeaderText = "Уровень доступа к ключам";
            this.KeyZone.Name = "KeyZone";
            this.KeyZone.ReadOnly = true;
            this.KeyZone.Width = 160;
            // 
            // FisNumCard
            // 
            this.FisNumCard.HeaderText = "Физ. номер карты";
            this.FisNumCard.Name = "FisNumCard";
            this.FisNumCard.ReadOnly = true;
            this.FisNumCard.Width = 140;
            // 
            // cardBox
            // 
            this.cardBox.FormattingEnabled = true;
            this.cardBox.Location = new System.Drawing.Point(600, 233);
            this.cardBox.Name = "cardBox";
            this.cardBox.Size = new System.Drawing.Size(121, 21);
            this.cardBox.TabIndex = 9;
            this.cardBox.SelectedIndexChanged += new System.EventHandler(this.cardBox_SelectedIndexChanged);
            // 
            // addCardButton
            // 
            this.addCardButton.Location = new System.Drawing.Point(383, 171);
            this.addCardButton.Name = "addCardButton";
            this.addCardButton.Size = new System.Drawing.Size(75, 23);
            this.addCardButton.TabIndex = 3;
            this.addCardButton.Text = "Добавить";
            this.addCardButton.UseVisualStyleBackColor = true;
            this.addCardButton.Click += new System.EventHandler(this.addCardButton_Click);
            // 
            // namePanel
            // 
            this.namePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.namePanel.Controls.Add(this.surnameLabel);
            this.namePanel.Controls.Add(this.surnameTextBox);
            this.namePanel.Controls.Add(this.nameTextBox);
            this.namePanel.Controls.Add(this.patronymTextBox);
            this.namePanel.Controls.Add(this.nameLabel);
            this.namePanel.Controls.Add(this.patronymLabel);
            this.namePanel.Location = new System.Drawing.Point(466, 19);
            this.namePanel.Name = "namePanel";
            this.namePanel.Size = new System.Drawing.Size(343, 147);
            this.namePanel.TabIndex = 2;
            // 
            // surnameLabel
            // 
            this.surnameLabel.AutoSize = true;
            this.surnameLabel.Location = new System.Drawing.Point(63, 22);
            this.surnameLabel.Name = "surnameLabel";
            this.surnameLabel.Size = new System.Drawing.Size(59, 13);
            this.surnameLabel.TabIndex = 1;
            this.surnameLabel.Text = "Фамилия:";
            // 
            // surnameTextBox
            // 
            this.surnameTextBox.Location = new System.Drawing.Point(132, 19);
            this.surnameTextBox.Name = "surnameTextBox";
            this.surnameTextBox.Size = new System.Drawing.Size(121, 20);
            this.surnameTextBox.TabIndex = 0;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(132, 61);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(121, 20);
            this.nameTextBox.TabIndex = 0;
            // 
            // patronymTextBox
            // 
            this.patronymTextBox.Location = new System.Drawing.Point(132, 108);
            this.patronymTextBox.Name = "patronymTextBox";
            this.patronymTextBox.Size = new System.Drawing.Size(121, 20);
            this.patronymTextBox.TabIndex = 0;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(63, 65);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(32, 13);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "Имя:";
            // 
            // patronymLabel
            // 
            this.patronymLabel.AutoSize = true;
            this.patronymLabel.Location = new System.Drawing.Point(63, 111);
            this.patronymLabel.Name = "patronymLabel";
            this.patronymLabel.Size = new System.Drawing.Size(57, 13);
            this.patronymLabel.TabIndex = 1;
            this.patronymLabel.Text = "Отчество:";
            // 
            // cardPanel
            // 
            this.cardPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardPanel.Controls.Add(this.cardmaskedTextBox);
            this.cardPanel.Controls.Add(this.lKeysBox);
            this.cardPanel.Controls.Add(this.levelTKeysLabel);
            this.cardPanel.Controls.Add(this.cardLabel);
            this.cardPanel.Location = new System.Drawing.Point(29, 19);
            this.cardPanel.Name = "cardPanel";
            this.cardPanel.Size = new System.Drawing.Size(343, 147);
            this.cardPanel.TabIndex = 2;
            // 
            // cardmaskedTextBox
            // 
            this.cardmaskedTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cardmaskedTextBox.Location = new System.Drawing.Point(174, 38);
            this.cardmaskedTextBox.Name = "cardmaskedTextBox";
            this.cardmaskedTextBox.Size = new System.Drawing.Size(98, 26);
            this.cardmaskedTextBox.TabIndex = 14;
            // 
            // lKeysBox
            // 
            this.lKeysBox.FormattingEnabled = true;
            this.lKeysBox.Location = new System.Drawing.Point(174, 88);
            this.lKeysBox.Name = "lKeysBox";
            this.lKeysBox.Size = new System.Drawing.Size(121, 21);
            this.lKeysBox.TabIndex = 13;
            // 
            // levelTKeysLabel
            // 
            this.levelTKeysLabel.AutoSize = true;
            this.levelTKeysLabel.Location = new System.Drawing.Point(15, 91);
            this.levelTKeysLabel.Name = "levelTKeysLabel";
            this.levelTKeysLabel.Size = new System.Drawing.Size(148, 13);
            this.levelTKeysLabel.TabIndex = 1;
            this.levelTKeysLabel.Text = "Уровень доступа к ключам:";
            // 
            // cardLabel
            // 
            this.cardLabel.AutoSize = true;
            this.cardLabel.Location = new System.Drawing.Point(85, 43);
            this.cardLabel.Name = "cardLabel";
            this.cardLabel.Size = new System.Drawing.Size(78, 13);
            this.cardLabel.TabIndex = 1;
            this.cardLabel.Text = "Номер карты:";
            // 
            // CardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 466);
            this.Controls.Add(this.cardGroupBox);
            this.Name = "CardForm";
            this.Text = "CardForm";
            this.cardGroupBox.ResumeLayout(false);
            this.cardGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainGridView)).EndInit();
            this.namePanel.ResumeLayout(false);
            this.namePanel.PerformLayout();
            this.cardPanel.ResumeLayout(false);
            this.cardPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox cardGroupBox;
        private System.Windows.Forms.Label patronymLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label surnameLabel;
        private System.Windows.Forms.TextBox patronymTextBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox surnameTextBox;
        private System.Windows.Forms.Panel cardPanel;
        private System.Windows.Forms.Label levelTKeysLabel;
        private System.Windows.Forms.Label cardLabel;
        private System.Windows.Forms.Button addCardButton;
        private System.Windows.Forms.ComboBox lKeysBox;
        private System.Windows.Forms.ComboBox cardBox;
        private System.Windows.Forms.DataGridView mainGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn FIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Issue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Valid;
        private System.Windows.Forms.DataGridViewTextBoxColumn KeyZone;
        private System.Windows.Forms.DataGridViewTextBoxColumn FisNumCard;
        private System.Windows.Forms.Label cardsLabel;
        private System.Windows.Forms.Panel namePanel;
        private System.Windows.Forms.MaskedTextBox cardmaskedTextBox;
    }
}