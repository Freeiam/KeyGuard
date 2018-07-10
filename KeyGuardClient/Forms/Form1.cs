using System;
using System.Windows.Forms;
using System.Globalization;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using System.Threading;

namespace KeyGuardClient
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// KeyGuardClient
        /// </summary>
        public KeyGuardPacket KeyGPack { get; set; }
        public Form1()
        {
            InitializeComponent();
            //dopInit();
        }
        private void dopInit()
        {                        
            mSForm = new MainSettingsForm();            
            dZForm = new DateZoneSettingsForm();            
            cDForm = new CardForm();            
        }
        private void connect_button_Click(object sender, EventArgs e)
        {
            /*IPAddress keyGuardIP = IPAddress.Parse(address);
            TcpListener listenKeyGuard = new TcpListener(keyGuardIP, port);            
            listenKeyGuard.Start();             // начнем слушать входящие подключения
            //if(listenKeyGuard.Pending())
            {
                TcpClient tcpCLient = listenKeyGuard.AcceptTcpClient();
                ClientKeyGuard = new Client(tcpCLient, this);
                ClientKeyGuard.Work();
                ClientKeyGuard.AddNewUserToComboBox += ClientKeyGuard_AddNewUserToComboBox;
                ClientKeyGuard.AddNewLevelToKeys += ClientKeyGuard_AddNewLevelToKeys;
                //snif.Text = string.Empty;
                //snif.Text = ClientKeyGuard.Connect(ref receiveDataLength);
                //ClientKeyGuard.Connect(ref receiveDataLength);
                //snif.Text = ClientKeyGuard.DebugMess.ToString();
            } */                       
        }                               
        /// <summary>
        /// Выбор узла дерева
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            switch (e.Node.Text)
            {
                case "Главная":
                    splitContainer1.Panel2.Controls.Clear();
                    splitContainer1.Panel2.Controls.Add(mSForm.MainGroupBox);
                    mSForm.MainGroupBox.Dock = DockStyle.Fill;
                    break;
                case "Временные зоны":
                    splitContainer1.Panel2.Controls.Clear();
                    splitContainer1.Panel2.Controls.Add(dZForm.DateZoneGroupBox);
                    break;
                case "Владельцы карт":
                    splitContainer1.Panel2.Controls.Clear();
                    splitContainer1.Panel2.Controls.Add(cDForm.CardGroupBox);
                    cDForm.CardGroupBox.Dock = DockStyle.Fill;
                    break;
                default:
                    break;
            }
        }
        private void saveToDBButton_Click(object sender, EventArgs e)
        {
            switch(treeView1.SelectedNode.Text)
            {
                case "Главная":
                    break;
                case "Временные зоны":
                    dZForm.SaveDateZoneToDB();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Подключение к устр-ву
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void подключитьсяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectSettingsForm connect = new ConnectSettingsForm();
            if(connect.ShowDialog() == DialogResult.OK)
            {
                KeyGPack = new KeyGuardPacket(new KeyGuardConnect(connect.IpAddrMask, connect.PortMask));
                mSForm = new MainSettingsForm(KeyGPack);
                mSForm.MdiParent = this;
                dZForm = new DateZoneSettingsForm(KeyGPack);
                dZForm.MdiParent = this;
                cDForm = new CardForm(KeyGPack);
                cDForm.MdiParent = this;
                // ---
                //KeyGPack.Subscibe(mSForm);
                KeyGPack.SubscribeOnPacket();
                KeyGPack.Start();
                // - покажем дерево 
                treeView1.Enabled = true;
                treeView1.Visible = true;
                // debug Proc();
            }
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// debug
        /// </summary>
        public async void Proc()
        {
            await Task.Run(() =>
            {
                uint i = 0;
                while (true)
                {
                    Thread.Sleep(3000);
                    //KeyGPack.AddNewUnkKey(++i);
                }
            });
        }
    }
}
