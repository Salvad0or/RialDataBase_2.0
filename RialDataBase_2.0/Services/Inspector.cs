using RialDataBase_2._0.EntityClasses.BaseConnectClass;
using RialDataBase_2._0.EntityClasses.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RialDataBase_2._0.Services
{
    /// <summary>
    /// Класс проверяющий
    /// </summary>
    internal static class Inspector
    {
        /// <summary>
        /// Команда поиска клиента по номеру телефону
        /// для возможности добавления.
        /// Если номер в базе уже присутствует, добавление невозможно.
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static bool SearchClient(string phone)
        {
            phone ??= String.Empty;

            if (phone.Length != 11) return false;
            

            using (Context contex = new Context())
            {
                Client search = contex.Clients.FirstOrDefault(s => s.Phone == phone);

                if (search is null) return true;

                return false;

            }           
        }
    }
}
