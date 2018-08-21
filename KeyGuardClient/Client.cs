using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;

namespace KeyGuardClient
{
    public delegate void UserHandler(object o);

    /*public class Client
    {
        // constants
        const uint start = 0x6BB65AA5;
        const uint end = 0x9CC98BB8;
        const int commandPtr = 29;
        const int identPtr = 30;
        const int valuePtr = 31;
        const int lenPtr = 16;
        const int dataPtr = 40;
        [Flags]
        public enum WorkFlags { None = 0, isClosed = 1, isConnect = 2, isCheckConnection = 4, isGetNumber = 8, isSendSystemTime = 16 }
        public WorkFlags ClientFlags;               // флаги клиента
        public TcpClient TcpConnect { get; }        // подключение клиента
        Telegram someTelegram;
        public Queue<Telegram> OutBuf { get; set; } = new Queue<Telegram>();    //<- очередь исходящих от клиента телеграмм
        public Queue<Telegram> InBuf { get; set; } = new Queue<Telegram>();     //<- очередь входящих телеграмм
        public List<Card> Users { get; set; } = new List<Card>();               //<- список пользователей
        public List<Text> Texts { get; set; } = new List<Text>();               //<- список текстов
        public List<LevelToKeys> LKeys { get; set; } = new List<LevelToKeys>(); //<- список уровней доступа к ключам
        public StringBuilder DebugMess { get; set; }         // debug string
        // variables
        public ushort reF { get; set; } = 0;         // референс номер телеграммы, +1 для каждой последующей телеграммы. Ответ должен иметь такой же ref-номер
        public uint sourceNumber { get; set; } = 0;  // последние 24 бита серийного номера источника телеграммы(т.е клиента), для сервера резерв 0xF0000000
        public ushort encr_Type { get; set; } = 0;
        public uint dst { get; set; } = 0x0000015D;  // номер железяки
        public byte life { get; set; } = 0;
        public int receiveDataLength = 0;                  // количество принятых байт
        //
        
        public UserHandler AddNewUserToComboBox;            //<- добавление пользователя в список
        public UserHandler AddNewLevelToKeys;               //<- добавление уровня доступа к ключам в список
        Form1 formUI;
        // ctor
        public Client(TcpClient tcp, Form1 uiForm)
        {
            TcpConnect = tcp;
            formUI = uiForm;
            //ClientFlags |= WorkFlags.isCheckConnection;
        }
        void CheckConection()
        {
            if ((ClientFlags & WorkFlags.isCheckConnection) == 0)
            {
                // - Check - проверка связи
                /*Telegram check = new Telegram(0x35);
                check.Ident[0] = 0x01;
                check.Value[0] = 0x13;
                check.Ref = reF;
                OutBuf.Enqueue(check);*/
                // - LogOn
                /*Telegram logOn = new Telegram(0xA1);
                logOn.Ident[0] = 0x01;
                logOn.Value[0] = 0x01;
                logOn.Ref = ++reF;
                OutBuf.Enqueue(logOn);*/
                // - подписка
                /*Telegram subscr = new Telegram(0x31);
                subscr.Ident[0] = 0x01;
                subscr.Value[0] = 0x04;
                subscr.Ref = ++reF;
                OutBuf.Enqueue(subscr);
                // - запрос ID у сервера
                ClientFlags |= WorkFlags.isCheckConnection;
            }
        }
        public async void Work()
        {
            try
            {
                using (NetworkStream netStream = TcpConnect.GetStream())
                {
                    DebugMess = new StringBuilder();
                    byte[] inBytes = new byte[2048];
                    int checkTime = 0;      // проверка связи не реже 120 сек. 
                    ushort len = 0;
                    // - проверка связи
                    Timer checkConnect = new Timer((x) =>
                    {
                        if ((ClientFlags & WorkFlags.isCheckConnection) == 0)
                        {
                            Telegram conn = (Telegram)x;// телеграмма проверки связи с устройством
                            conn.Dst = dst;// 0x0000015D;
                            conn.Ref = ++reF;
                            OutBuf.Enqueue(conn);       // запишем телеграмму в очередь
                            ClientFlags |= WorkFlags.isCheckConnection;
                            checkTime = 0;
                        }
                        //else
                        {
                            //if (++checkTime == 2)
                            //throw new Exception("Связь с устройством потеряна. Перезапустим подключение");                                 
                        }

                    }, new Telegram(0xA2, 0xE5, 0x31), 0, 60000);
                    // - прием пакетов
                    await Task.Run(() =>
                    {
                        // - запросим пользователей
                        someTelegram = new Telegram(0x91, 0x04, 0xE2, 4);
                        someTelegram.Dst = dst;
                        someTelegram.Ref = ++reF;
                        someTelegram.Data[0] = 2;
                        OutBuf.Enqueue(someTelegram);
                        someTelegram = new Telegram(0x91, 0x04, 0xE2, 4);
                        someTelegram.Dst = dst;
                        someTelegram.Ref = ++reF;
                        someTelegram.Data[0] = 3;
                        OutBuf.Enqueue(someTelegram);
                        // - запрос уровней доступа к ключам
                        someTelegram = new Telegram(0x91, 0x10, 0xE2, 1);
                        someTelegram.Dst = dst;
                        someTelegram.Ref = ++reF;
                        someTelegram.Data[0] = 1;
                        OutBuf.Enqueue(someTelegram);
                        someTelegram = new Telegram(0x91, 0x10, 0xE2, 2);
                        someTelegram.Dst = dst;
                        someTelegram.Ref = ++reF;
                        someTelegram.Data[0] = 2;
                        OutBuf.Enqueue(someTelegram);
                        // ----
                        // main cycle 
                        while ((ClientFlags & WorkFlags.isClosed) == 0)
                        {
                            if (netStream.CanRead)
                            {
                                netStream.Read(inBytes, 0, 4);
                                {
                                    if (BitConverter.ToUInt32(inBytes, 0) == start)
                                    {
                                        netStream.Read(inBytes, 4, 13);
                                        {
                                            len = inBytes[lenPtr];
                                            if (len >= 36 && (netStream.Read(inBytes, 17, len - 9) == len - 9))     //- прочитаем оставшиеся байты вместе с конечной последов.(4байта)
                                            {
                                                if (BitConverter.ToUInt32(inBytes, len + 4) == end)
                                                {
                                                    DebugMess.Append("<--- Read ---<<");
                                                    DebugMess.Append(BitConverter.ToString(inBytes, 0, len + 8));
                                                    receiveDataLength += len;
                                                    //Telegram inTGram = new Telegram(inBytes[commandPtr]);
                                                    switch (inBytes[commandPtr])
                                                    {
                                                        /*case 0x00:
                                                            if ((ClientFlags & WorkFlags.isGetNumber) == 0)
                                                            {
                                                                if (inBytes[commandPtr + 59] == 0x25 && inBytes[commandPtr + 72] == 0x32)
                                                                {
                                                                    if (BitConverter.ToUInt16(inBytes, commandPtr + 72 - 3) == reF)
                                                                    {
                                                                        ClientFlags |= WorkFlags.isGetNumber;
                                                                        sourceNumber = inBytes[commandPtr + 83];
                                                                        Telegram getUsers =  new Telegram(0xA2);
                                                                        getUsers.Ident[0] = 0xE5;
                                                                        getUsers.Value[0] = 0x31;
                                                                        getUsers.Src |= sourceNumber;
                                                                        getUsers.Ref = ++reF;
                                                                        //OutBuf.Enqueue(getUsers);
                                                                    }
                                                                }
                                                            }
                                                            break;
                                                        case 0x90:
                                                            // - users
                                                            if (inBytes[identPtr] == 0x04 && inBytes[valuePtr] == 0xE2)
                                                            {
                                                                byte[] fisNumCard = new byte[10];       // буфер под физ. номер карты пользователя
                                                                Array.Copy(inBytes, dataPtr + 30, fisNumCard, 0, fisNumCard.Length);
                                                                Card newUser = new Card(BitConverter.ToUInt32(inBytes, dataPtr + 4), BitConverter.ToUInt32(inBytes, dataPtr + 16),
                                                                    BitConverter.ToUInt32(inBytes, dataPtr + 20), BitConverter.ToUInt16(inBytes, dataPtr + 28),
                                                                    BitConverter.ToUInt16(inBytes, dataPtr + 40), fisNumCard);
                                                                if (Users.Find(x => x.UserNumber == newUser.UserNumber && x.Card == newUser.Card) == null)
                                                                {
                                                                    Users.Add(newUser);
                                                                    formUI.Invoke(AddNewUserToComboBox, new object[] { newUser.UserNumber });
                                                                    // запросим тексты Ф.И.О.
                                                                    someTelegram = new Telegram(0x91, 0x12, 0xE2, 4);
                                                                    someTelegram.Dst = dst;
                                                                    someTelegram.Ref = ++reF;
                                                                    Array.Copy(BitConverter.GetBytes(newUser.NameIndex), someTelegram.Data, 2);
                                                                    OutBuf.Enqueue(someTelegram);
                                                                }
                                                            }
                                                            // - уровень доступа к ключам
                                                            if (inBytes[identPtr] == 0x10 && inBytes[valuePtr] == 0xE2 && inBytes[lenPtr] == 0x68)
                                                            {
                                                                ushort[] timeZn = new ushort[16];
                                                                ushort[] dZlist = new ushort[16];
                                                                for (int i = 0; i < 8; i++)
                                                                {
                                                                    // - поищем пары: временная зона/ключи
                                                                    if (BitConverter.ToUInt16(inBytes, dataPtr + 4 + i * 2) != 0 && BitConverter.ToUInt16(inBytes, dataPtr + 4 + i * 2 + 16) != 0)
                                                                    {
                                                                        Array.Copy(inBytes, dataPtr + 4 + i * 2, timeZn, i * 2, 2);
                                                                        Array.Copy(inBytes, dataPtr + 4 + i * 2 + 16, dZlist, i * 2, 2);
                                                                    }
                                                                }
                                                                if (timeZn.Length > 0) //<- есть что-нибудь? 
                                                                {
                                                                    LevelToKeys newLKeys = new LevelToKeys(BitConverter.ToUInt32(inBytes, dataPtr), timeZn, dZlist);
                                                                    if (LKeys.Find(x => x.Addr == newLKeys.Addr) == null)
                                                                    {
                                                                        LKeys.Add(newLKeys);
                                                                        formUI.Invoke(AddNewLevelToKeys, new object[] { newLKeys.Addr });
                                                                    }
                                                                }

                                                            }
                                                            // - text            
                                                            if (inBytes[identPtr] == 0x12 && inBytes[valuePtr] == 0xE2 && inBytes[lenPtr] == 72)
                                                            {
                                                                Text newText = new Text(BitConverter.ToUInt32(inBytes, dataPtr));
                                                                if (Texts.Find(x => x.Addr == newText.Addr) == null)
                                                                {
                                                                    for (int i = 0; i < 31; i++)
                                                                        newText.TextBytes[i] = inBytes[dataPtr + 4 + i];
                                                                    Texts.Add(newText);
                                                                    DebugMess.Append(Encoding.ASCII.GetString(inBytes, dataPtr + 4, len - 36 - 4));
                                                                }
                                                            }
                                                            break;
                                                        case 0xA3:
                                                            if (inBytes[identPtr] == 0xE5 && inBytes[valuePtr] == 0x31)
                                                            {
                                                                ClientFlags &= ~WorkFlags.isCheckConnection;
                                                                encr_Type = BitConverter.ToUInt16(inBytes, 4);
                                                                dst = BitConverter.ToUInt32(inBytes, lenPtr + 2);
                                                                life = inBytes[lenPtr - 2];
                                                                if ((ClientFlags & WorkFlags.isSendSystemTime) == 0)
                                                                {
                                                                    // - системное время
                                                                    someTelegram = new Telegram(0x81, 0x19, 0xEC, 4);
                                                                    someTelegram.Dst = dst;
                                                                    someTelegram.Ref = ++reF;
                                                                    // - data = Time
                                                                    someTelegram.Data = BitConverter.GetBytes(someTelegram.Time);
                                                                    OutBuf.Enqueue(someTelegram);
                                                                    ClientFlags |= WorkFlags.isSendSystemTime;
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
                                                                    OutBuf.Enqueue(someTelegram);
                                                                }
                                                            }
                                                            break;
                                                        case 0x80:
                                                            if (inBytes[identPtr] == 0x19 && inBytes[valuePtr] == 0xEC)
                                                                ClientFlags |= WorkFlags.isSendSystemTime;
                                                            break;
                                                        default:
                                                            break;
                                                    }

                                                }
                                            }
                                        }
                                    }
                                }
                                Array.Clear(inBytes, 0, inBytes.Length);    // очистим входной буфер
                            }
                            if (netStream.CanWrite)
                            {
                                // - отправка пакетов
                                byte[] bytesCommand;
                                while (OutBuf.Count > 0)
                                {
                                    bytesCommand = OutBuf.Dequeue().GetBytesTelegram();
                                    DebugMess.Append(">--- Send --->>");
                                    DebugMess.Append(BitConverter.ToString(bytesCommand, 0, bytesCommand.Length));
                                    netStream.Write(bytesCommand, 0, bytesCommand.Length);
                                }
                            }
                        }
                    });
                }
            }
            catch (Exception e)
            {
                DebugMess.Append("!--- Excpt ---!");
                DebugMess.Append(e.ToString());
            }
            finally
            {
                if (TcpConnect != null)
                    TcpConnect.Close();
            }
        }
        public void Connect(ref int dataLength)
        {
            try
            {
                //tcpClient = new TcpClient();
                //tcpClient.Connect(address, port);
                //Thread workThread = new Thread(Work);
                //workThread.Name = "workThread";
                //workThread.Start();
                Work();
                /*StringBuilder response = new StringBuilder();
                using (TcpClient tcpClient = new TcpClient())
                {
                    tcpClient.Connect(address, port); 
                    using (NetworkStream netStream = tcpClient.GetStream())
                    {
                        byte[] data = new byte[1024];
                        int bytes = 0;      // кол-во принятых байт
                        int check = 0;  // debug
                        while (check < 3)
                        {
                            response.Append("<--- Read ---<<");
                            do
                            {
                                bytes = netStream.Read(data, 0, data.Length);
                                response.Append(BitConverter.ToString(data, 0, bytes));
                                dataLength += bytes;
                            }
                            while (netStream.DataAvailable);
                            // -- посылаем команду
                            if(check++ < 1)
                            {
                                Telegram tGram = new Telegram(0x31);
                                tGram.Src = 0xF0000000;
                                tGram.Dst = 0xF0000000;
                                tGram.Ref = 1;
                                tGram.Ident[0] = 0x01;
                                tGram.Value[0] = 0x04;
                                byte[] bytesComm = new byte[tGram.LenTelegram];
                                bytesComm = tGram.GetBytesTelegram();
                                netStream.Write(bytesComm, 0, tGram.LenTelegram);
                                response.Append(">>--- Send --->");
                                response.Append(BitConverter.ToString(bytesComm));
                                tGram = new Telegram(0x31);
                                tGram.Src = 0xF0000000;
                                tGram.Dst = 0xF0000000;
                                tGram.Ref = 2;
                                tGram.Ident[0] = 0x01;
                                tGram.Value[0] = 0x01;
                                bytesComm = tGram.GetBytesTelegram();
                                netStream.Write(bytesComm, 0, tGram.LenTelegram);
                                response.Append(">>--- Send --->");
                                response.Append(BitConverter.ToString(bytesComm));
                            }
                            
                        }
                    }
            }
                return response.ToString();
            }
            catch (Exception e)
            {
                //return e.Message;
                //return false;
            }
        }
    }*/            
                    
    public struct Telegram
    {
        // constants
        public const uint start = 0x6BB65AA5;
        public const uint end = 0x9CC98BB8;
        public const int commandPtr = 29;
        public const int identPtr = 30;
        public const int valuePtr = 31;
        public const int lenPtr = 16;
        public const int sourcePtr = 18;
        public const int refPtr = 26;
        public const int dataPtr = 40;
        public const int defaultLength = 44;            // длина телеграммы по умолчанию: без Data(Data[0]) с заголовками
        // --
        public static uint SrcID = 0;                   // - иден-р сервера
        public static uint DstID = 0;                   // - иден-р устр-ва
        public static ushort Ref = 0;                   // - номер телеграммы(+1)
        // --
        public uint Start;
        public ushort Encr_Type;
        public ushort Encr_Word;
        public uint Encr_Numb;
        public ushort Dest_Real;
        public byte[] Life;
        public byte[] Vers;
        public ushort Len;
        //public uint Src;
        //uint Dst;
        //ushort Ref;
        public byte[] Bcc;
        public byte[] Cmd_T;
        public byte[] Ident;
        public byte[] Value;
        public uint Time;
        public uint Acnt;
        public byte[] Data;
        public uint End;
        //List<byte[]> listTelegram { get; }      // для отправки оборудованию
        public int LenTelegram { get; }         // общая длина телеграммы
        // ctors
        public Telegram(byte cmd_t, byte ident, byte value, ushort lenData = 0)
        {
            Start = 0x6BB65AA5;
            Encr_Type = 0;
            Encr_Word = 0;
            Encr_Numb = 0;
            Dest_Real = 0;
            Life = new byte[1] { 0 };
            Vers = new byte[1] { 0 };
            Len = (ushort)(36 + lenData);
            Bcc = new byte[1] { 0 };
            Cmd_T = new byte[1] { cmd_t }; 
            Ident = new byte[1] { ident }; 
            Value = new byte[1] { value };
            // - Time
            DateTime dt = DateTime.Now;
            int tmp = (dt.Second & 0x3F) | ((dt.Minute & 0x3F) << 6)
            | ((dt.Hour & 0x1F) << 12) | ((dt.Day & 0x1F) << 17)
            | ((dt.Month & 0x0F) << 22) | (((dt.Year - 2010) & 0x3F) << 26);
            Time = (uint)tmp;
            // --
            Acnt = 0;
            Data = new byte[lenData]; // new byte[4];
            End = 0x9CC98BB8;
            LenTelegram = defaultLength + Data.Length;
            //listTelegram = new List<byte[]>(LenTelegram);            
        }
        public Telegram(byte cmd_t, byte ident, byte value, byte[] data)
        {
            Start = 0x6BB65AA5;
            Encr_Type = 0;
            Encr_Word = 0;
            Encr_Numb = 0;
            Dest_Real = 0;
            Life = new byte[1] { 0 };
            Vers = new byte[1] { 0 };
            Len = (ushort)(36 + data.Length);
            Bcc = new byte[1] { 0 };
            Cmd_T = new byte[1] { cmd_t };
            Ident = new byte[1] { ident };
            Value = new byte[1] { value };
            // - Time
            DateTime dt = DateTime.Now;
            int tmp = (dt.Second & 0x3F) | ((dt.Minute & 0x3F) << 6)
            | ((dt.Hour & 0x1F) << 12) | ((dt.Day & 0x1F) << 17)
            | ((dt.Month & 0x0F) << 22) | (((dt.Year - 2010) & 0x3F) << 26);
            Time = (uint)tmp;
            // --
            Acnt = 0;
            Data = data;
            End = 0x9CC98BB8;
            LenTelegram = defaultLength + Data.Length;
            //listTelegram = new List<byte[]>(LenTelegram);
        }
        // ctor используется KeyGuardConect при чтении с порта, т.е входящая телеграмма
        public Telegram(byte[] inBytes)
        {
            Start = start;
            Encr_Type = 0;
            Encr_Word = 0;
            Encr_Numb = 0;
            Dest_Real = 0;
            Life = new byte[1] { 0 };
            Vers = new byte[1] { 0 };
            Len = BitConverter.ToUInt16(inBytes, lenPtr);
            DstID = BitConverter.ToUInt32(inBytes, sourcePtr);          // - источник = приемник - устр-во
            SrcID = BitConverter.ToUInt32(inBytes, sourcePtr + 4);      // - приемник = источник - сервер
            //Ref = BitConverter.ToUInt16(inBytes, refPtr);
            Bcc = new byte[1] { 0 };
            Cmd_T = new byte[1] { inBytes[commandPtr] };
            Ident = new byte[1] { inBytes[identPtr] };
            Value = new byte[1] { inBytes[valuePtr] };
            // - Time
            DateTime dt = DateTime.Now;
            int tmp = (dt.Second & 0x3F) | ((dt.Minute & 0x3F) << 6)
            | ((dt.Hour & 0x1F) << 12) | ((dt.Day & 0x1F) << 17)
            | ((dt.Month & 0x0F) << 22) | (((dt.Year - 2010) & 0x3F) << 26);
            Time = (uint)tmp;
            // --
            Acnt = 0;
            Data = new byte[Len - 36]; // new byte[4];
            Array.Copy(inBytes, dataPtr, Data, 0, Data.Length);
            End = end;
            LenTelegram = defaultLength + Data.Length;
            //listTelegram = new List<byte[]>(LenTelegram);
        }
        /// <summary>
        /// формирует массив байт из полей телеграммы
        /// </summary>
        /// <returns></returns>
        public byte[] GetBytesTelegram()
        {
            List<byte[]> listTelegram = new List<byte[]>();
            listTelegram.Add(BitConverter.GetBytes(Start));
            listTelegram.Add(BitConverter.GetBytes(Encr_Type));
            listTelegram.Add(BitConverter.GetBytes(Encr_Word));
            listTelegram.Add(BitConverter.GetBytes(Encr_Numb));
            listTelegram.Add(BitConverter.GetBytes(Dest_Real));
            listTelegram.Add(Life);
            listTelegram.Add(Vers);
            listTelegram.Add(BitConverter.GetBytes(Len));
            listTelegram.Add(BitConverter.GetBytes(SrcID));
            listTelegram.Add(BitConverter.GetBytes(DstID));
            listTelegram.Add(BitConverter.GetBytes(++Ref));
            listTelegram.Add(Bcc);
            listTelegram.Add(Cmd_T);
            listTelegram.Add(Ident);
            listTelegram.Add(Value);
            listTelegram.Add(BitConverter.GetBytes(Time));
            listTelegram.Add(BitConverter.GetBytes(Acnt));
            listTelegram.Add(Data);
            listTelegram.Add(BitConverter.GetBytes(End));
            return listTelegram
                    .SelectMany(a => a)
                    .ToArray();
        }
    }
}
