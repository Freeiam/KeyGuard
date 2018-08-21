using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace KeyGuardClient
{
    public class KeyGuardPacket : IPacket
    {
        IConnect hardWareConnect;                                  //<- ссылка на соединение
        public UserHandler AddNewCardToComboBox;                   //<- добавление пользователя в список
        public UserHandler AddNewLevelToKeys;                      //<- добавление уровня доступа к ключам в список
        public UserHandler AddNewTimeZone;                         //<- добавление временной зоны в список
        public UserHandler AddNewUnkKey;                           //<- добавление неизвестного ключа
        public UserHandler AddMessageBox;                          //<- информационное сообщение
        Telegram someTelegram;                                     //<- телеграмма для отправки
        // prop
        public List<Card> Cards { get; } = new List<Card>();               //<- список карт
        public List<Text> Texts { get; } = new List<Text>();               //<- список текстов
        public List<LevelToKeys> LKeys { get; } = new List<LevelToKeys>(); //<- список уровней доступа к ключам
        public List<DateZone> DateZones { get; } = new List<DateZone>();   //<- список временных зон
        public List<Key> UnkKeys { get; } = new List<Key>();               //<- список неизвестных ключей в системе
        private uint addrKey { get; set; } = 1;                            //<- индекс записи ключа в БД(временно)

        public int GetReceiveByte
        {
            get
            {
                if (hardWareConnect is KeyGuardConnect)
                {
                    return (hardWareConnect as KeyGuardConnect).ReceiveDataLength;
                }
                else
                    return 0;
            }
        }
        public StringBuilder GetDebugMess
        {
            get
            {
                if (hardWareConnect is KeyGuardConnect)
                {
                    return (hardWareConnect as KeyGuardConnect).DebugMess;
                }
                else
                    return new StringBuilder("--New Stringbuilder--");
            }
        }        
        [Flags]
        public enum WorkFlags { None = 0, isClosed = 1, isConnect = 2, isCheckConnection = 4, isGetNumber = 8, isSendSystemTime = 16,
                                isSendstartCMD = 32}
        public WorkFlags ClientFlags;               // флаги клиента        
        // ctor        
        public KeyGuardPacket(IConnect connect)
        {
            hardWareConnect = connect;
        }
        public KeyGuardPacket() { }
        /// <summary>
        /// отправка телеграммы на HW
        /// </summary>         
        public void SendPack(Telegram tg)
        {            
            hardWareConnect.OutBuf.Enqueue(tg);
        }
        // IPacket
        /// <summary>
        /// Подписка на пакеты от железа
        /// </summary>
        public void SubscribeOnPacket()
        {
            try
            {
                hardWareConnect.GetPacket += KeyGuardPacket_GetPacket;
            }
            catch (Exception e)
            {
                GetDebugMess.Append("!--- Excpt ---!");
                GetDebugMess.Append(e.ToString());
            }
        }        
        /*public void Subscibe(System.Windows.Forms.Form sbcForm)
        {
            if(sbcForm is CardForm)
            {
                AddNewCardToComboBox += (sbcForm as CardForm).CardForm_AddNewCardToComboBox;
                AddNewLevelToKeys += (sbcForm as CardForm).CardForm_AddNewLevelToKeys;
            }
        }*/
        /// <summary>
        /// Запуск процесса работы с железом
        /// </summary>
        public void Start()
        {
            try
            {                
                //someTelegram = new Telegram(0xA2, 0xE5, 0x31);  // - проверка связи
                //hardWareConnect.OutBuf.Enqueue(someTelegram);           
                hardWareConnect.Work();                 //<- запустим работу с оборудованием
            }
            catch (Exception e)
            {
                GetDebugMess.Append("!--- Excpt ---!");
                GetDebugMess.Append(e.ToString());
            }
        }
        /// <summary>
        /// Отсылка начальных команд
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void startCMD()
        {
            // - запросим пользователей
            // someTelegram = new Telegram(0x91, 0x04, 0xE2, 4);
            // someTelegram.Data[0] = 2;
            // hardWareConnect.OutBuf.Enqueue(someTelegram);
            //someTelegram = new Telegram(0x91, 0x04, 0xE2, 4);
            //someTelegram.Data[0] = 3;
            //hardWareConnect.OutBuf.Enqueue(someTelegram);
            // - запрос уровней доступа к ключам
            //someTelegram = new Telegram(0x91, 0x10, 0xE2, 1);
            //someTelegram.Data[0] = 1;
            //hardWareConnect.OutBuf.Enqueue(someTelegram);
            //someTelegram = new Telegram(0x91, 0x10, 0xE2, 2);
            //someTelegram.Data[0] = 2;
            //hardWareConnect.OutBuf.Enqueue(someTelegram);
            // - debug - удалим ключ
            //someTelegram = new Telegram(0x91, 0x0F, 0xE3, new byte[4] { 10, 0, 0, 0 });
            //hardWareConnect.OutBuf.Enqueue(someTelegram);
            // - debug - удалим пользователя
            //someTelegram = new Telegram(0x91, 0x04, 0xE3, new byte[4] { 4, 0, 0, 0 });
            //hardWareConnect.OutBuf.Enqueue(someTelegram);
            // - debug - удалим пользователя
            // someTelegram = new Telegram(0x91, 0x04, 0xE3, new byte[4] { 2, 0, 0, 0 });
            //hardWareConnect.OutBuf.Enqueue(someTelegram);
            // - format
            someTelegram = new Telegram(0x91, 0xFF, 0xEA, new byte[4] { 0x78, 0x56, 0x34, 0x12 });
           ///hardWareConnect.OutBuf.Enqueue(someTelegram);
            // - запрос состояния неизвестных ключей        
            //someTelegram = new Telegram(0x82, 0x0F, 0xF3);
            // - запрос состояния списка
            //someTelegram = new Telegram(0x82, 0x0E, 0xF3);
            // - заголовок
            //someTelegram = new Telegram(0x91, 0xFE, 0xE2, new byte[4] { 0,0,0,0} );
            hardWareConnect.OutBuf.Enqueue(someTelegram);


        }
        // callback
        private void KeyGuardPacket_GetPacket(object sender, EventArgs e)
        {
            if(sender == hardWareConnect)
            {
                Telegram tg = hardWareConnect.InBuf.Dequeue();
                // - разбор сообщений
                switch(tg.Cmd_T[0])
                {
                    case 0x90:                      // - ответ на команду
                        switch(tg.Value[0])
                        {
                            // - операция чтение
                            case 0xE2:              
                                // - users
                                if (tg.Ident[0] == 0x04)
                                {
                                    byte[] fisNumCard = new byte[10];       // буфер под физ. номер карты пользователя
                                    Array.Copy(tg.Data, 30, fisNumCard, 0, fisNumCard.Length);
                                    Card newCard = new Card(BitConverter.ToUInt32(tg.Data, 0), BitConverter.ToUInt32(tg.Data, 4), BitConverter.ToUInt32(tg.Data, 16),
                                        BitConverter.ToUInt32(tg.Data, 20), BitConverter.ToUInt16(tg.Data, 28),
                                        BitConverter.ToUInt16(tg.Data, 40), fisNumCard);
                                    if (Cards.Find(x => x.Addr == newCard.Addr) == null)
                                    {
                                        Cards.Add(newCard);
                                        AddNewCardToComboBox(newCard.Addr);
                                        // запросим тексты Ф.И.О.
                                        someTelegram = new Telegram(0x91, 0x12, 0xE2, 4);
                                        Array.Copy(BitConverter.GetBytes(newCard.NameIndex), someTelegram.Data, 2);
                                        hardWareConnect.OutBuf.Enqueue(someTelegram);
                                    }
                                }
                                // - уровень доступа к ключам
                                if (tg.Ident[0] == 0x10 && tg.Len == 0x68)
                                {
                                    ushort[] timeZn = new ushort[16];       // - временная зона
                                    ushort[] dZlist = new ushort[16];       // - ключи
                                    for (int i = 0; i < 8; i++)
                                    {
                                        // - поищем пары: временная зона/ключи
                                        if (BitConverter.ToUInt16(tg.Data, 4 + i * 2) != 0 && BitConverter.ToUInt16(tg.Data, 4 + i * 2 + 16) != 0)
                                        {
                                            Array.Copy(tg.Data, 4 + i * 2, timeZn, i * 2, 2);
                                            Array.Copy(tg.Data, 4 + i * 2 + 16, dZlist, i * 2, 2);
                                        }
                                    }
                                    if (timeZn.Length > 0) //<- есть что-нибудь? 
                                    {
                                        LevelToKeys newLKeys = new LevelToKeys(BitConverter.ToUInt32(tg.Data, 0), timeZn, dZlist);
                                        if (LKeys.Find(x => x.Addr == newLKeys.Addr) == null)
                                        {
                                            LKeys.Add(newLKeys);
                                            AddNewLevelToKeys(newLKeys.Addr);
                                        }
                                    }
                                }
                                // - Тексты          
                                if (tg.Ident[0] == 0x12 && tg.Len == 72)
                                {
                                    Text newText = new Text(BitConverter.ToUInt32(tg.Data, 0));
                                    if (Texts.Find(x => x.Addr == newText.Addr) == null)
                                    {
                                        for (int i = 0; i < 31; i++)
                                            newText.TextBytes[i] = tg.Data[4 + i];
                                        Texts.Add(newText);
                                        GetDebugMess.Append(Encoding.ASCII.GetString(tg.Data, 4, tg.Len - 36 - 4));
                                    }
                                }
                                // - заголовок
                                if(tg.Ident[0] == 0xFE)
                                {

                                }
                                break;
                            // - операция запись
                            case 0xE1: 
                                // - временная зона
                                if (tg.Ident[0] == 0x06 && tg.Value[0] == 0xE1)
                                {
                                    AddNewTimeZone(DateZones.Last().Addr);
                                }
                                // - Ключи(Key)
                                if (tg.Ident[0] == 0x0F && tg.Value[0] == 0xE1)
                                {
                                    //AddNewUnkKey(UnkKeys.Last().Addr);
                                    AddNewUnkKey(BitConverter.ToUInt32(tg.Data, 0));
                                }
                                // - Уровень доступа к ключам
                                if (tg.Ident[0] == 0x10 && tg.Value[0] == 0xE1)
                                {
                                    AddNewLevelToKeys(LKeys.Last().Addr);
                                }
                                // - пользователи
                                if (tg.Ident[0] == 0x04 && tg.Value[0] == 0xE1)
                                {
                                    AddNewCardToComboBox(Cards.Last().Addr);
                                }
                                // - Тексты
                                if (tg.Ident[0] == 0x12 && tg.Value[0] == 0xE1)
                                {

                                }
                                break;
                            // - форматирование
                            case 0xEA:
                                // - форматирование памяти
                                if(tg.Ident[0] == 0xFF)
                                {
                                    // - чтение заголовка
                                    //someTelegram = new Telegram(0x91, 0xFE, 0xE2, new byte[8] { 0x00, 0x00, 0x00, 0x01, 0x78, 0x56, 0x34, 0x12 });
                                    //hardWareConnect.OutBuf.Enqueue(someTelegram);
                                    // - запрос состояния неизвестных ключей        
                                    //someTelegram = new Telegram(0x82, 0x0F, 0xF3);
                                    //hardWareConnect.OutBuf.Enqueue(someTelegram);
                                }
                                break;                            
                        }                                                
                        break;
                    // - события от ключницы
                    case 0x80:
                        // - посмотрим какое событие пришло
                        switch (tg.Ident[0])
                        {
                            // - событие от ключей
                            case 0x0F:
                                // - ключ выдан
                                if(tg.Value[0] == 0x32 || tg.Value[0] == 0x3B)   //<- 3B - ?? почему-то приходит "Ключ выдан из другой ключницы"
                                {
                                    if(UnkKeys.Find(x => x.Addr == BitConverter.ToUInt32(tg.Data, 0)) != null)
                                    {
                                        string key_name = Encoding.ASCII.GetString( UnkKeys.Find(x => x.Addr == BitConverter.ToUInt32(tg.Data, 0)).Name );
                                        string mess = "Ключ выдан - " + key_name;
                                        AddMessageBox(mess);
                                    }                                    
                                }
                                // - ключ вернут 
                                if (tg.Value[0] == 0x72 || tg.Value[0] == 0x7B)   //<- 7B - ?? почему-то приходит "Ключ вернут в другую ключницу"
                                {
                                    if (UnkKeys.Find(x => x.Addr == BitConverter.ToUInt32(tg.Data, 0)) != null)
                                    {
                                        string key_name = Encoding.ASCII.GetString( UnkKeys.Find(x => x.Addr == BitConverter.ToUInt32(tg.Data, 0)).Name );
                                        string mess = "Ключ вернут - " + key_name;
                                        AddMessageBox(mess);
                                    }
                                }
                                break;
                            // - системное время
                            case 0x19:
                                if (tg.Value[0] == 0xEC)
                                    ClientFlags |= WorkFlags.isSendSystemTime;
                                break;
                        }
                        break;
                    // - ответ на запросы состояния
                    case 0x83:
                    case 0x85:
                        // - неизвестные ключи
                        if (tg.Ident[0] == 0x0F && tg.Value[0] == 0x73)
                        {
                            if(tg.Len == 50)
                            {
                                Key newKey = new Key(addrKey++/*BitConverter.ToUInt32(tg.Data, 0)*/, tg.Data[4], tg.Data[5], new byte[] {tg.Data[6], tg.Data[7],
                                                                        tg.Data[8], tg.Data[9], tg.Data[10], tg.Data[11], tg.Data[12], tg.Data[13]});
                                if(UnkKeys.Find(x => x.Addr == newKey.Addr) == null)
                                {
                                    UnkKeys.Add(newKey);
                                    // - запишем данный ключ в БД
                                    someTelegram = new Telegram(0x91, 0x0F, 0xE1, newKey.GetBytesKey());
                                    hardWareConnect.OutBuf.Enqueue(someTelegram);
                                }
                            }                            
                        }
                        break;
                        // - проверка связи
                    case 0xA3:
                        if (tg.Ident[0] == 0xE5 && tg.Value[0] == 0x31)
                        {
                            // - запрос проверки связи
                            //someTelegram = new Telegram(0xA2, 0xE5, 0x31);
                            //hardWareConnect.OutBuf.Enqueue(someTelegram);
                            if((ClientFlags & WorkFlags.isSendstartCMD) == 0)
                            {
                                startCMD();                                 // - отправим начальные телеграммы
                                ClientFlags |= WorkFlags.isSendstartCMD;
                            }
                            /*ClientFlags &= ~WorkFlags.isCheckConnection;
                            if ((ClientFlags & WorkFlags.isSendSystemTime) == 0)
                            {
                                // - системное время
                                someTelegram = new Telegram(0x81, 0x19, 0xEC, 4);
                                // - data = Time
                                someTelegram.Data = BitConverter.GetBytes(someTelegram.Time);
                                //OutBuf.Enqueue(someTelegram);
                                ClientFlags |= WorkFlags.isSendSystemTime;*/
                                /*
                                someTelegram = new Telegram(0x91);
                                someTelegram.Ident[0] = 0x05;
                                someTelegram.Value[0] = 0xE2;
                                someTelegram.Dst = dst;
                                dt = DateTime.Now;
                                tmp = (dt.Second & 0x3F) | ((dt.Minute & 0x3F) << 6)
                                | ((dt.Hour & 0x1F) << 12) | ((dt.Day & 0x1F) << 17)
                                | ((dt.Month & 0x0F) << 22) | (((dt.Year - 2010) & 0x3F) << 26);
                                someTelegram.Data = BitConverter.GetBytes(tmp);
                                someTelegram.Ref = reF;
                                OutBuf.Enqueue(someTelegram);}*/                            
                        }
                        break;                     
                    default:
                        break;
                }
            }
        }
    }
}
