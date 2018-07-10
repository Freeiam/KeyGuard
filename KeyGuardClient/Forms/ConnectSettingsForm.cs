using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace KeyGuardClient
{
    public partial class ConnectSettingsForm : Form
    {
        public ConnectSettingsForm()
        {
            InitializeComponent();
            ActiveControl = ipAddrmaskedTextBox;
        }
        // variables
        public string IpAddrMask;
        public int PortMask;
        private void okbutton_Click(object sender, EventArgs e)
        {
            if(ipAddrmaskedTextBox.MaskCompleted && portmaskedTextBox.MaskCompleted && int.TryParse(portmaskedTextBox.Text, out PortMask))
            {
                IpAddrMask = ipAddrmaskedTextBox.Text;                
                IpAddrMask = IpAddrMask.Replace(',', '.');
                IpAddrMask = IpAddrMask.Replace(" ", "");
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.None;
            }
        }
        private void cancelbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
