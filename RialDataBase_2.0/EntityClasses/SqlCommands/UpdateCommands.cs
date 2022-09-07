using RialDataBase_2._0.EntityClasses.BaseConnectClass;
using RialDataBase_2._0.EntityClasses.Objects;
using RialDataBase_2._0.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RialDataBase_2._0.EntityClasses.SqlCommands
{
    internal static class UpdateCommands
    {
        public static void AddCashBack(EntityClient client)
        {
            using (Context context = new Context())
            {

                int id = context.Clients.First(i => i.Phone == client.Phone).Id;

                ClientBankAccout cba = context.ClientBankAccouts.First(i =>
                i.ClientId == context.Clients.First(c => c.Phone == client.Phone).Id);

                cba.TotalPurchaseAmount = client.TotalPurchaseAmount;
                cba.CashBack = client.CashBack;

                context.SaveChanges();
            }
        }
    }
}
