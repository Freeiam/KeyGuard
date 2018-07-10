using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyGuardClient
{
    public partial class DateZoneSettingsForm : Form
    {
        // ctors
        public DateZoneSettingsForm()
        {
            InitializeComponent();
        }
        public DateZoneSettingsForm(IPacket pack)
        {
            InitializeComponent();
            if(pack is KeyGuardPacket)
            {
                keyGPack = pack as KeyGuardPacket;
            }
        }
        // props
        public GroupBox DateZoneGroupBox
        {
            get { return dateZonegroupBox; }
        }
        ushort[] typeMass { get; } = new ushort[10];
        ushort[] startMass { get; } = new ushort[10];
        ushort[] endMass { get; } = new ushort[10];
        // variables
        private KeyGuardPacket keyGPack;
        // methods
        /// <summary>
        /// Сохранение формы в массивы класса
        /// </summary>
        void saveDateZone()
        {
            DateZone dateZone = new DateZone();
            int count = 0;
            foreach (Panel p in dateZonegroupBox.Controls)          // - элементы располагаются последовательно
            {                
                ushort chk = 1;
                foreach(Control cntrl in p.Controls)                // - элементы располагаются последовательно
                {
                    // дни
                    if (cntrl is CheckBox)
                    {
                        if ((cntrl as CheckBox).Checked)
                        {
                            typeMass[count] |= chk;
                        }
                        chk <<= 1;
                    }
                    // время
                    if (cntrl is DateTimePicker)
                    {
                        DateTimePicker dtPick = (cntrl as DateTimePicker);
                        if((dtPick.Location.X + dtPick.Width) < p.Width / 2)  // start/end - ?
                        {
                            // start time
                            byte[] byt = new byte[] { (byte)(dtPick.Value.Minute), (byte)(dtPick.Value.Hour) };
                            startMass[count] = BitConverter.ToUInt16(byt, 0);//new byte[] { (byte)(dtPick.Value.Minute), (byte)(dtPick.Value.Hour) }, 0);
                        }
                        else
                        {
                            // end time
                            endMass[count] = BitConverter.ToUInt16(new byte[] { (byte)(dtPick.Value.Minute), (byte)(dtPick.Value.Hour) }, 0);
                        }
                    }
                }                                
                count++;
            }            
        }
        /// <summary>
        /// Сохранение данных с формы на устр-ве
        /// </summary>
        public void SaveDateZoneToDB()
        {
            // - сначала проверим форму
            saveDateZone();
            // - затем, есть ли временная зона для добавления в устр-во?
            if(typeMass.Length > 0 || startMass.Length > 0 || endMass.Length > 0)
            {
                keyGPack.DateZones.Add(new DateZone(3, typeMass, startMass, endMass));
                keyGPack.SendPack(new Telegram(0x91, 0x06, 0xE1, keyGPack.DateZones.Last().GetBytesDateZone()));            //<- отправим устр-ву
            }
        }
        // --
    }
}
