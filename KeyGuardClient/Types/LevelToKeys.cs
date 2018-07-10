using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyGuardClient
{
    public class LevelToKeys
    {
        /// <summary>
        /// Адрес элемента в БД
        /// </summary>
        public uint Addr { get; set; }
        /// <summary>
        /// Временная зона
        /// </summary>
        public ushort[] TimeZn { get; set; } = new ushort[16];
        /// <summary>
        /// Ключи/список ключей
        /// </summary>
        public ushort[] Dzlist { get; set; } = new ushort[16];
        // ctor
        public LevelToKeys(uint addr, ushort[] timeZn, ushort[] dZist)
        {
            // адрес в БД
            Addr = addr;
            // посмотрим, какие ключи и временные зоны есть
            if(timeZn.Length > 0)
            {
                Array.Copy(timeZn, TimeZn, timeZn.Length);
                Array.Copy(dZist, Dzlist, dZist.Length);
            }
        }
        // метод преобразует поля класса в массив байт
        public byte[] GetBytesLKeys()
        {
            List<byte[]> listBytesLKeys = new List<byte[]>();
            listBytesLKeys.Add(BitConverter.GetBytes(Addr));
            // - преобразуем массив временных зон
            for(int i = 0; i < 16; i++)
            {
                listBytesLKeys.Add(BitConverter.GetBytes(TimeZn[i]));
            }
            // - затем массив ключей
            for(int i = 0; i < 16; i++)
            {
                listBytesLKeys.Add(BitConverter.GetBytes(Dzlist[i]));
            }
            return listBytesLKeys
                .SelectMany(a => a)
                .ToArray();
        }
    }
}
