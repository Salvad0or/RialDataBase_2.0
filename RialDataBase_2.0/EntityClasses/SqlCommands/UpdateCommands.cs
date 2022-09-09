using RialDataBase_2._0.EntityClasses.BaseConnectClass;
using RialDataBase_2._0.EntityClasses.Objects;
using RialDataBase_2._0.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RialDataBase_2._0.EntityClasses.SqlCommands
{
    internal static class UpdateCommands
    {
        /// <summary>
        /// Метод добавления кешбека
        /// </summary>
        /// <param name="client"></param>
        /// <param name="cashback"></param>
        public static void AddCashBack(EntityClient client, int cashback)
        {
            bool flag = default;

            client.TotalPurchaseAmount += cashback;

            switch (client.TotalPurchaseAmount)
            {
                case > 100_000:

                    if (client.Status == StatusEnum.Vip) break;

                    client.Status = StatusEnum.Vip;

                    MessageBox.Show(
                        $"Поздравьте клиента!" +
                        $"\n{client.Name} получил VIP статус!" +
                        $"\n Кешбек равен 4%!");
                    flag = true;
                    break;

                case > 50_000:

                    if (client.Status == StatusEnum.Gold) break;

                    client.Status = StatusEnum.Gold;

                    MessageBox.Show(
                        $"Поздравьте клиента!" +
                        $"\n{client.Name} получил GOLD статус!" +
                        $"\n Кешбек равен 3%!");
                    flag = true;
                    break;

                case > 10_000:

                    if (client.Status == StatusEnum.Silver) break;

                    client.Status = StatusEnum.Silver;
             
                    MessageBox.Show(
                        $"Поздравьте клиента!" +
                        $"\n{client.Name} получил Silver статус!" +
                        $"\n Кешбек равен 2%!");
                    flag = true;
                    break;
            }

            switch (client.Status)
            {
                case StatusEnum.Standart:
                    client.CashBack += cashback / 100;       
                    break;

                case StatusEnum.Silver:
                    client.CashBack += cashback / 100 * 2;                   
                    break;

                case StatusEnum.Gold:
                    client.CashBack += cashback / 100 * 3;                    
                    break;

                case StatusEnum.Vip:
                    client.CashBack += cashback / 100 * 4;          
                    break;
            }

            using (Context context = new Context())
            {
                
                ClientBankAccout cba = context.ClientBankAccouts.First(i =>
                i.ClientId == context.Clients.First(c => c.Phone == client.Phone).Id);

                cba.TotalPurchaseAmount = client.TotalPurchaseAmount;
                cba.CashBack = client.CashBack;

                if (flag)
                {
                    Client changeClient = context.Clients.First(i => i.Phone == client.Phone);
                    changeClient.StatusId = (byte)(client.Status);
                }
                
                context.SaveChanges();

                MessageBox.Show(
                        $"Кешбек добавлен, " +
                        $"баланс {client.CashBack} руб.");
            }
        }

        public static void SpendCashBack(EntityClient client, int cashback)
        {
            using (Context context = new Context())
            {
                ClientBankAccout cba = context.ClientBankAccouts.Single
                    (i => i.ClientId ==
                    context.Clients.Single(c => c.Phone == client.Phone).Id);

                cba.CashBack -= cashback;

                context.SaveChanges();

                MessageBox.Show
                    (
                    "Кешбек успешно списан, " +
                    $"баланс равен {cba.CashBack} руб"
                    );
            }
        }
    }
}
