using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyGuardClient
{
    /// <summary>
    /// Класс, содержащий информацию
    /// о ключе: iButton, №модуля, №ячейки
    /// </summary>
    public class Key
    {
        public uint Addr { get; }               //<- адрес записи в БД
        public byte Module { get; }             //<- номер блока/номер модуля в блоке
        public byte Cell { get; }               //<- номер ячейки в модуле
        uint key_number;                        //<- номер ключа
        ushort type;                            //<- доп. пар-ры
        ushort fix_time_ret;                    //<- вернуть до этого времени
        ushort delay_ret;                       //<- задержка на возврат
        ushort det_arm;                         //<- номер зоны охраны
        ushort temp;                            //<- не используется
        public byte[] IButton { get; }          //<- номер iButton - 8 байт
        public byte[] Name { get; }             //<- название ключа(текст) - 24 байт
        public string SiButton { get; }         //<- инфа о ключе для вывода
        // ctors
        public Key()
        {
            Addr = 1;
            Module = 1;
            Cell = 1;
            IButton = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        }
        public Key(uint addr, byte module, byte cell, byte[] iButton)
        {
            Addr = 10; //addr;
            Module = module;
            Cell = cell;
            if (iButton.Length == 8)
            {
                IButton = iButton;
            }
            else
            {
                IButton = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
            }
            // - debug
            string sIbutton = BitConverter.ToString(IButton, 0, 8);
            SiButton = "№Ключа = " + sIbutton + " №модуля = " + Module.ToString() + " №ячейки = " + Cell.ToString();
            // - debug
            string s = "Ключ %s" + Addr + "M:%d" + Module + "N:%d" + Cell;
            Name = new byte[24];
            Array.Copy(Encoding.ASCII.GetBytes(s), Name, s.Length);
        }
        // метод преобразует поля класса в массив байт
        public byte[] GetBytesKey()
        {
            List<byte[]> listBytesKey = new List<byte[]>();
            listBytesKey.Add(BitConverter.GetBytes(Addr));
            listBytesKey.Add(BitConverter.GetBytes((ushort)Module));
            listBytesKey.Add(BitConverter.GetBytes((ushort)Cell));
            listBytesKey.Add(BitConverter.GetBytes(key_number));
            listBytesKey.Add(BitConverter.GetBytes(type));
            listBytesKey.Add(BitConverter.GetBytes(fix_time_ret));
            listBytesKey.Add(BitConverter.GetBytes(delay_ret));
            listBytesKey.Add(BitConverter.GetBytes(det_arm));
            listBytesKey.Add(BitConverter.GetBytes(temp));
            listBytesKey.Add(IButton);
            listBytesKey.Add(Name);
            return listBytesKey
                .SelectMany(a => a)
                .ToArray();
        }
    }
}
