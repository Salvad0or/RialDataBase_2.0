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
        Standart = 1, //1% кешбек
        Silver = 2, // 2% кешбек от 25000
        Gold = 3, // 3% кешбек от 50000
        Vip = 4 // 4% кешбек от 100000
    }
}
