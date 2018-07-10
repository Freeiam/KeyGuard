using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyGuardClient
{
    public class Card
    {
        /// <summary>
        /// Адрес элемента в БД
        /// </summary>
        public uint Addr { get; set; }
        /// <summary>
        /// Номер карты
        /// </summary>
        //public uint Card { get; set; }                  // \
        /// <summary>                                       \
        /// Номер пользователя                               -> Номер пользователя(ХХ|ХХХХХХ - №польз|№карты)
        /// </summary>                                      /
        //public uint UserNumber { get; set; }            // /
        /// <summary>
        /// Пользователь
        /// </summary>
        public uint Acnt { get; set; }                  //-> Номер пользователя(ХХ|ХХХХХХ - №польз|№карты)
        /// <summary>
        /// Пароль
        /// </summary>
        public uint Password { get; set; }
        /// <summary>
        /// Тип карточки
        /// </summary>
        public uint Type { get; set; }
        /// <summary>
        /// Дата выдачи карты
        /// </summary>
        public uint Issue { get; set; }
        /// <summary>
        /// Дата окончания действия карты
        /// </summary>
        public uint Valid { get; set; }
        /// <summary>
        /// Уровень доступа (с 1 до ..)
        /// </summary>
        public ushort Auth { get; set; } = 1;
        /// <summary>
        /// Зона охраны (с 1 до ..)
        /// </summary>
        public ushort DetZone { get; set; }
        /// <summary>
        /// Индекс элемента уровеня доступа к ключам
        /// </summary>
        public ushort KeyZoneIndex { get; set; }
        /// <summary>
        /// Физический номер карты
        /// </summary>
        public byte[] FisNumCard { get; set; }
        /// <summary>
        /// Индекс Ф.И.О.
        /// </summary>        
        public ushort NameIndex { get; set; }
        /// <summary>
        /// Индекс телефона
        /// </summary>
        public ushort PhoneIndex { get; set; }
        // ctor
        public Card(uint addr, uint acnt /*номер пользователя*/, uint issue, uint valid, ushort keyzone, ushort name, byte[] fisNumCard)
        {
            Addr = addr;
            Acnt = acnt;
            Issue = issue;
            Valid = valid;
            KeyZoneIndex = keyzone;
            NameIndex = name;
            FisNumCard = fisNumCard;
        }
        // метод преобразует поля класса в массив байт
        public byte[] GetBytesCard()
        {
            List<byte[]> listBytesCard = new List<byte[]>();
            listBytesCard.Add(BitConverter.GetBytes(Addr));
            listBytesCard.Add(BitConverter.GetBytes(Acnt));
            listBytesCard.Add(BitConverter.GetBytes(Password));
            listBytesCard.Add(BitConverter.GetBytes(Type));
            listBytesCard.Add(BitConverter.GetBytes(Issue));
            listBytesCard.Add(BitConverter.GetBytes(Valid));
            listBytesCard.Add(BitConverter.GetBytes(Auth));
            listBytesCard.Add(BitConverter.GetBytes(DetZone));
            listBytesCard.Add(BitConverter.GetBytes(KeyZoneIndex));
            listBytesCard.Add(FisNumCard);
            listBytesCard.Add(BitConverter.GetBytes(NameIndex));
            listBytesCard.Add(BitConverter.GetBytes(PhoneIndex));
            return listBytesCard
                .SelectMany(a => a)
                .ToArray();
        }
        public string GetDate(uint someDate)
        {
            int day, mounth, year;
            day = (int)((someDate >> 17) & 0x1F);
            mounth = (int)((someDate >> 22) & 0x0F);
            year = (int)((someDate >> 26) & 0x3F) + 2010;
            return day.ToString() + '.' + mounth.ToString() + '.' + year.ToString();
        }
    }
}
