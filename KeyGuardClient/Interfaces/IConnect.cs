using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;


namespace KeyGuardClient
{
    public class TelegramEventArgs:EventArgs
    {
        public Queue<Telegram> Buffer;
        // ctor
        public TelegramEventArgs(Queue<Telegram> buffer)
        {
            Buffer = buffer;
        }
    }
    public interface IConnect
    {
        int Port { get; set; }                              //<- порт устр-ва
        IPAddress KeyGuardIP { get; set; }                  //<- IP-адрес устр-ва
        Queue<Telegram> InBuf { get; set; }                 //<- очередь входящих от устройства телеграмм
        Queue<Telegram> OutBuf { get; set; }                //<- очередь исходящих от клиента телеграмм
        event EventHandler GetPacket;                       //<- событие для приема пакетов клиентом
        void Work();                                        //<- главный цикл работы
    }
}
