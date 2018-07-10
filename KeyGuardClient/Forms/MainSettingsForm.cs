using System;
using System.Globalization;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Linq;

namespace KeyGuardClient
{
    public partial class MainSettingsForm : Form
    {      
        // ctors  
        public MainSettingsForm()
        {
            InitializeComponent();            
        }
        public MainSettingsForm(IPacket pack)
        {
            InitializeComponent();
            if(pack is KeyGuardPacket)
            {
                keyGPack = pack as KeyGuardPacket;
                // подключение callback-ов
                keyGPack.AddNewUnkKey += MainSettingsForm_AddNewUnkKey;
                keyGPack.AddNewTimeZone += MainSettingsForm_AddNewTimeZone;
            }
        }
        // props
        /*public ComboBox UserBox
        {
            get
            {
                return userBox;
            }            
        }
        public ComboBox LKeysBox
        {
            get { return lKeysBox; }
        }
        public TextBox TextBoxCmd
        {
            get { return textBoxCmd_T; }
        }
        public TextBox TextBoxIdent
        {
            get { return textBoxIdent; }
        }
        public TextBox TextBoxValue
        {
            get { return textBoxValue; }
        }
        public TextBox TextBoxData
        {
            get { return textBoxData; }
        }
        public TextBox Snif
        {
            get { return snif; }
        }
        public Label ReceivedByteLabel
        {
            get { return receivedByteLabel; }
        }
        public DataGridView MainGridView
        {
            get { return mainGridView; }
        }*/
        public GroupBox MainGroupBox
        {
            get { return maingroupBox; }
        }
        // variables
        private KeyGuardPacket keyGPack;
        // callback        
        /// <summary>
        /// добавление временной зоны
        /// </summary>
        /// <param name="numbTimeZone"></param>
        public void MainSettingsForm_AddNewTimeZone(uint numbTimeZone)
        {
            if(numbTimeZone > 0)
            {
                this.Invoke((Action)delegate { dateZoneComboBox.Items.Add(numbTimeZone); });
            }
        }
        /// <summary>
        /// добавление неизвестного ключа
        /// </summary>
        /// <param name="key"></param>
        public void MainSettingsForm_AddNewUnkKey(uint key)
        {
            {
                if (key > 0 && !keyComboBox.Items.Contains(key))
                {
                    this.Invoke((Action) delegate{ keyComboBox.Items.Add(key); });
                }
            }            
        }
        // --        
        private void connect_button_Click(object sender, EventArgs e)
        {
            /*string address = "192.168.21.153";           // адрес устр-ва
            int port = 8000;                            // порт устр-ва
            KeyGPack = new KeyGuardPacket(new KeyGuardConnect(address, port), this);
            KeyGPack.SubscribeOnPacket();
            KeyGPack.Start();*/
        }
        
        private void timerText_Tick(object sender, EventArgs e)
        {
            //snif.Text += keyGPack.GetDebugMess.ToString();
            keyGPack.GetDebugMess.Clear();
            receivedByteLabel.Text = "Кол-во принятых байт: " + keyGPack.GetReceiveByte;
        }
                
        /// <summary>
        /// отправка телеграммы с формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSendCommand_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] bytesComm = new byte[3];
                Byte.TryParse(textBoxCmd_T.Text, NumberStyles.AllowHexSpecifier, null, out bytesComm[0]);
                Byte.TryParse(textBoxIdent.Text, NumberStyles.AllowHexSpecifier, null, out bytesComm[1]);
                Byte.TryParse(textBoxValue.Text, NumberStyles.AllowHexSpecifier, null, out bytesComm[2]);
                uint data;
                byte[] bytesData;
                if (UInt32.TryParse(textBoxData.Text, NumberStyles.AllowHexSpecifier, null, out data))
                    bytesData = BitConverter.GetBytes(data);
                else
                    bytesData = new byte[0];
                // - send Telegram
                Telegram send = new Telegram(bytesComm[0], bytesComm[1], bytesComm[2], (ushort)bytesData.Length);
                Array.Copy(bytesData, send.Data, bytesData.Length);             //<- data
                keyGPack.SendPack(send);
            }
            catch (Exception exp)
            {
                snif.Text = exp.Message.ToString();
            }            
        }
        /// <summary>
        /// Добавление нового уровня доступа к ключам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addLevelToKeysButton_Click(object sender, EventArgs e)
        {
            ushort[] timeZn = new ushort[16];           // - временная зона
            ushort[] dZlist = new ushort[16];           // - ключи
            if (ushort.TryParse(dateZoneComboBox.SelectedItem.ToString(), out timeZn[0])
                && ushort.TryParse(keyComboBox.SelectedItem.ToString(), out dZlist[0]))
            {
                keyGPack.LKeys.Add(new LevelToKeys(3, timeZn, dZlist));
                keyGPack.SendPack(new Telegram(0x91, 0x10, 0xE1, keyGPack.LKeys.Last().GetBytesLKeys()));       // - отправим устр-ву
            }                           
        }

        private void keyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            keysInfoLabel.Text = String.Empty;
            Key keyForLabel = keyGPack.UnkKeys.Find(x => x.Addr == (uint)keyComboBox.SelectedItem);
            if( keyForLabel != null)
            {
                keysInfoLabel.Text = keyForLabel.SiButton;
            }            
        }
    }
}
