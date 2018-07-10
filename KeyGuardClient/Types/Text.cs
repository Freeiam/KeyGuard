using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyGuardClient
{
    public class Text
    {
        /// <summary>
        /// Адрес элемента в БД
        /// </summary>
        public uint Addr { get; set; }
        /// <summary>
        /// Текстовое поле, 31 байт
        /// </summary>
        public byte[] TextBytes { get; set; } = new byte[31];
        /// <summary>
        /// Обязательное поле = 0
        /// </summary>
        public byte[] Zero { get; } = { 0 };
        // ctor
        public Text(uint addr)
        {
            Addr = addr;
        }
        // метод преобразует поля класса в массив байт
        public byte[] GetBytesText()
        {
            List<byte[]> listBytesText = new List<byte[]>();
            listBytesText.Add(BitConverter.GetBytes(Addr));
            listBytesText.Add(TextBytes);
            listBytesText.Add(Zero);
            return listBytesText
                .SelectMany(a => a)
                .ToArray();
        }
        public override string ToString()
        {
            return Encoding.ASCII.GetString(TextBytes);
        }
    }
}
