using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyGuardClient
{
    public partial class CardForm : Form
    {
        // ctors
        public CardForm()
        {
            InitializeComponent();
        }
        public CardForm(IPacket pack)
        {
            InitializeComponent();
            if(pack is KeyGuardPacket)
            {
                keyGPack = pack as KeyGuardPacket;
                // подключение callback-ов
                keyGPack.AddNewCardToComboBox += CardForm_AddNewCardToComboBox;
                keyGPack.AddNewLevelToKeys += CardForm_AddNewLevelToKeys;
            }
        }
        // props
        public GroupBox CardGroupBox
        {
            get { return cardGroupBox; }
        }
        // variables
        private KeyGuardPacket keyGPack;
        // callback
        /// <summary>
        /// добавление карты пользователя
        /// </summary>
        /// <param name="cardNumber"></param>
        public void CardForm_AddNewCardToComboBox(object cardNumber)
        {
            uint card = (uint)cardNumber;
            if (card > 0 && card < 255)
            {
                this.Invoke((Action) delegate { cardBox.Items.Add(card); });
            }
        }
        /// <summary>
        /// добавление уровня доступа к ключам
        /// </summary>
        /// <param name="lKeysNumber"></param>
        public void CardForm_AddNewLevelToKeys(object lKeysNumber)
        {
            uint level = (uint)lKeysNumber;
            if (level > 0)
            {
                this.Invoke((Action)delegate { lKeysBox.Items.Add(level); });
            }
        }
        // --
        /// <summary>
        /// выбор пользователя для просмотра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cardBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // - текущий выбранный индекс
            uint slcItem = (uint)cardBox.SelectedItem;
            // Empty mainGridView
            for (int i = mainGridView.Rows.Count; i > 0; i--)
                mainGridView.Rows.Remove(mainGridView.Rows[i - 1]);
            // Add first row
            mainGridView.Rows.Add(1);
            Card comboCard = keyGPack.Cards.Find(x => x.Addr == slcItem);
            if (comboCard != null)
            {
                mainGridView.Rows[0].Cells[0].Value = comboCard.Acnt * 0x00FFFFFF;
                mainGridView.Rows[0].Cells[1].Value = keyGPack.Texts.Find(x => x.Addr == comboCard.NameIndex)?.ToString() ?? "???";
                mainGridView.Rows[0].Cells[2].Value = comboCard.Acnt * 0xFF000000;
                mainGridView.Rows[0].Cells[3].Value = comboCard.GetDate(comboCard.Issue);
                mainGridView.Rows[0].Cells[4].Value = comboCard.GetDate(comboCard.Valid);
                mainGridView.Rows[0].Cells[5].Value = comboCard.KeyZoneIndex;
                mainGridView.Rows[0].Cells[6].Value = BitConverter.ToUInt32(comboCard.FisNumCard, 0).ToString("X");
            }
        }
        /// <summary>
        /// Добавление нового владельца карты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addCardButton_Click(object sender, EventArgs e)
        {
            // - запишем тексты
            string textStr;
            textStr = surnameTextBox.Text + ' ' + nameTextBox.Text + ' ' + patronymTextBox.Text;
            Text newText = new Text(1);  // <- bag должна быть сквозная адресация на тексты //new Text(keyGPack.Texts.Last().Addr + 1);
            byte[] text = Encoding.ASCII.GetBytes(textStr);
            if (text.Length <= 31)
            {
                Array.Copy(text, 0, newText.TextBytes, 0, text.Length);
            }
            else
            {
                Array.Copy(text, 0, newText.TextBytes, 0, 31);
            }
            keyGPack.Texts.Add(newText);
            keyGPack.SendPack(new Telegram(0x91, 0x12, 0xE1, newText.GetBytesText()));      // - отправим устр-ву
            // - запишем карту
            byte[] fisnumCard = new byte[10];
            for(int i = cardmaskedTextBox.Text.Length - 2; i >= 0 ; i -= 2)
            {
                //fisnumCard[(cardmaskedTextBox.Text.Length - 2 - i) / 2] = Convert.ToByte(cardmaskedTextBox.Text.Substring(i, 2));
                byte.TryParse(cardmaskedTextBox.Text.Substring(i, 2), NumberStyles.AllowHexSpecifier, null, out fisnumCard[(cardmaskedTextBox.Text.Length - 2 - i) / 2]);
            }
            //int key = (int)lKeysBox.SelectedItem;
            ushort uLKey = 0;                                                                                       // - уровень доступа к ключам из combobox
            if(ushort.TryParse(lKeysBox.SelectedItem.ToString(), out uLKey))
            {
                keyGPack.Cards.Add(new Card(1, 0x03000003, 0x1E1CB60A, 0x461CB60A, uLKey, (ushort)newText.Addr, fisnumCard));
                keyGPack.SendPack(new Telegram(0x91, 0x04, 0xE1, keyGPack.Cards.Last().GetBytesCard()));            // - отправим устр-ву
            }
            
        }
    }
}
