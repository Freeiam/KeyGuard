using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace KeyGuardClient
{
    public class KeyGuardConnect:IConnect
    {        
        // variables
        public int ReceiveDataLength = 0;                   // количество принятых байт
        public StringBuilder DebugMess { get; set; }        // debug string  
        // ctor
        public KeyGuardConnect(string ipAddr /*ip-адрес устр-ва*/, int port /*порт для работы с устр-вом*/)
        {
            KeyGuardIP = IPAddress.Parse(ipAddr);
            Port = port;
            InBuf = new Queue<Telegram>();
            OutBuf = new Queue<Telegram>();
        }
        // IConnect
        public int Port { get; set; }
        public IPAddress KeyGuardIP { get; set; }
        public Queue<Telegram> InBuf { get; set; }          //<- очередь входящих от устройства телеграмм
        public Queue<Telegram> OutBuf { get; set; }         //<- очередь исходящих от клиента телеграмм
        public event EventHandler GetPacket;
        int startMinute = 0;
        // главная работа
        public async void Work()
        {
            TcpListener ListenKeyGuard = new TcpListener(KeyGuardIP, Port);
            ListenKeyGuard.Start();                         // начнем слушать входящие подключения
            TcpClient TcpConnect = ListenKeyGuard.AcceptTcpClient();
            try
            {
                using (NetworkStream netStream = TcpConnect.GetStream())
                {
                    DebugMess = new StringBuilder();
                    byte[] inBytes = new byte[2048];        // буфер приема пакетов
                    int checkTime = 0;                      // проверка связи не реже 120 сек. 
                    ushort len = 0;
                    int startIndex = 0;                     // индекс начала пакета в inBytes
                    
                    await Task.Run(() =>
                    {
                        // main cycle 
                        while (true)//(ClientFlags & WorkFlags.isClosed) == 0)
                        {
                            // - прием пакетов
                            if (netStream.CanRead)
                            {                               
                                netStream.Read(inBytes, 0, 4);
                                if(BitConverter.ToUInt32(inBytes, 0) == Telegram.start)
                                {
                                    netStream.Read(inBytes, 4, 14);                                          //- читаем дальше до/и слово длины
                                    len = BitConverter.ToUInt16(inBytes, Telegram.lenPtr);
                                    if(len >= 36 && (netStream.Read(inBytes, 18, len - 10) == len - 10))     //- прочитаем оставшиеся байты вместе с конечной последов.(4байта))
                                    {
                                        if (BitConverter.ToUInt32(inBytes, len + 4) == Telegram.end)
                                        {    
                                            // - debug
                                            //if(inBytes[Telegram.commandPtr] == 0x83 || inBytes[Telegram.commandPtr] == 0x85 || inBytes[Telegram.identPtr] == 0xFE)
                                            {
                                                DebugMess.Append("<--- Read ---<<");
                                                DebugMess.Append(BitConverter.ToString(inBytes, 0, len + 8));
                                            }         
                                            // - debug
                                            ReceiveDataLength += len;
                                            InBuf.Enqueue(new Telegram(inBytes));               // запишем телеграмму в очередь  
                                            GetPacket(this, new EventArgs());                   // инициируем событие отправки пакетов клиенту                                          
                                        }
                                    }
                                }
                                Array.Clear(inBytes, 0, inBytes.Length);                        // очистим буфер приема пакетов
                            }
                            // - отправка пакетов
                            if (netStream.CanWrite)
                            {
                                // - проверка связи
                                /*Timer checkConnect = new Timer((x) =>
                                {
                                    //if ((ClientFlags & WorkFlags.isCheckConnection) == 0)
                                    {
                                        Telegram.DstID = 0x0000015D;
                                        Telegram.SrcID = 0xF0000001;
                                        Telegram conn = (Telegram)x;// телеграмма проверки связи с устройством
                                        OutBuf.Enqueue(conn);       // запишем телеграмму в очередь
                                        //ClientFlags |= WorkFlags.isCheckConnection;
                                        //checkTime = 0;
                                    }
                                    //else
                                    {
                                        //if (++checkTime == 2)
                                        //throw new Exception("Связь с устройством потеряна. Перезапустим подключение");                                 
                                    }

                                }, new Telegram(0xA2, 0xE5, 0x31), 0, 60000);*/
                                // --
                                DateTime dt = DateTime.Now;
                                if((dt.Minute - startMinute) >= 1)
                                {
                                    OutBuf.Enqueue(new Telegram(0xA2, 0xE5, 0x31));
                                    if(dt.Minute == 59)
                                    {
                                        startMinute = 0;
                                    }
                                    else
                                    {
                                        startMinute = dt.Minute;
                                    }                                    
                                }

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
        // --
    }
}
