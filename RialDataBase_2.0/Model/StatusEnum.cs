using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RialDataBase_2._0.Model
{
    /// <summary>
    /// Перечисления статуса клиентов
    /// </summary>
    public enum StatusEnum : byte
    {
        Standart = 1, // до 30000. 1% кешбек на остаток
        Silver = 2, // до 100000. 2% кешбек на остаток
        Gold = 3, // до 200000. 3% кешбек на остаток
        Vip = 4 // от 200000 и выше. 4% кешбек на остаток
    }
}
