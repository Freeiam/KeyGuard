using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyGuardClient
{
    /// <summary>
    /// работа с пакетам от hardware
    /// </summary>
    public interface IPacket
    {
        void SubscribeOnPacket();           //<- подписка на пакеты от железа, подписка формы
        void Start();                       //<- старт работы с оборудованием
    }
}
