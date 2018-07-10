using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyGuardClient
{
    /// <summary>
    /// Класс временной зоны
    /// описывает время доступа на объект
    /// может содержать до 10 временных интервалов,
    /// с учетом дней недели и выходных
    /// </summary>
    public class DateZone
    {
        public uint Addr { get; }              //<- адрес записи в БД
        public ushort[] Type { get; }          //<- дни недели в данной временной зоне
        public ushort[] Start { get; }         //<- время начала действия данной временной зоны
        public ushort[] End { get; }           //<- время окончания действия данной временной зоны
        // ctors            
        public DateZone()
        {
            Addr = 1;
            // ини-ия массивов, 10 интервалов max.
            Type = new ushort[10];
            Start = new ushort[10];
            End = new ushort[10];
        }
        public DateZone(uint addr, ushort[] type, ushort[] start, ushort[] end)
        {
            Addr = addr;
            Type = type;
            Start = start;
            End = end;
        }
        // метод преобразует поля класса в массив байт
        public byte[] GetBytesDateZone()
        {
            List<byte[]> listBytesDateZone = new List<byte[]>();
            listBytesDateZone.Add(BitConverter.GetBytes(Addr));
            for(int i = 0; i < Type.Length; i++)
            {
                listBytesDateZone.Add(BitConverter.GetBytes(Type[i]));
            }
            for (int i = 0; i < Start.Length; i++)
            {
                listBytesDateZone.Add(BitConverter.GetBytes(Start[i]));
            }
            for (int i = 0; i < End.Length; i++)
            {
                listBytesDateZone.Add(BitConverter.GetBytes(End[i]));
            }
            return listBytesDateZone
                    .SelectMany(a => a)
                    .ToArray();
        }            
    }
}
