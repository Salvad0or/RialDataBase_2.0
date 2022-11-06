using RialDataBase_2._0.EntityClasses.BaseConnectClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RialDataBase_2._0.EntityClasses.SqlCommands
{
    internal class SelectCommands
    {
        public static async Task<string> FindCurrentPromocodeAsync()
        {
           await using (Context context = new Context())
            {
                var _promocode = context.Promocodes.First();

                return _promocode.Name;
            }
        }  
    }
}
