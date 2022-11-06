using RialDataBase_2._0.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RialDataBase_2._0.HelperClasses
{
    internal static class Helper
    {
        public static EntityClient Cleaner(EntityClient client)
        {

            client.Vin = default;
            client.Name = default;
            client.Phone = String.Empty;
            client.Car = default;
            client.Oil = default;
            client.OilFilter = default;
            client.AirFilter = default;
            client.SalonFilter = default;
            client.CashBack = default;
            client.Ngk = default;
            client.Padsfront = default;
            client.Padsrear = default;
            client.Fuelfilter = default;
            client.Comment = default;
            client.Date = default;
            client.Status = default;
            client.TotalPurchaseAmount = default;

            return client;

        }



        
    }
}
