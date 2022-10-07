using RialDataBase_2._0.EntityClasses.BaseConnectClass;
using RialDataBase_2._0.EntityClasses.Objects;
using RialDataBase_2._0.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        public static void AddCashBackAsync(EntityClient client, int cashback)
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

                Task task = Task.Run(context.SaveChanges);

                task.ContinueWith((t) =>
                {
                    MessageBox.Show(
                        $"Кешбек добавлен, " +
                        $"баланс {client.CashBack} руб.");
                });
                             
            }
        }

        /// <summary>
        /// Метод списывания кешбека
        /// </summary>
        /// <param name="client"></param>
        /// <param name="cashback"></param>
        public static void SpendCashBackAsync(EntityClient client, int cashback)
        {
            using (Context context = new Context())
            {
                ClientBankAccout cba = context.ClientBankAccouts.Single
                    (i => i.ClientId ==
                    context.Clients.Single(c => c.Phone == client.Phone).Id);

                cba.CashBack -= cashback;

                Task task = Task.Run(context.SaveChanges);

                task.ContinueWith((t) =>
                {
                    MessageBox.Show
                    (
                    "Кешбек успешно списан, " +
                    $"баланс равен {cba.CashBack} руб"
                    );
                });        
            }
        }

        public static void ChangeClientDataAsync(EntityClient client, ref bool flag, string phone)
        {
            using (Context context = new Context())
            {
                Client EntityClient = context.Clients.Single(p => p.Phone == phone);

                EntityClient.Fname = client.Name;
                EntityClient.Phone = client.Phone;
                EntityClient.Comment = client.Comment;

                Car car = context.Cars.Single(c => c.Id == EntityClient.Id);

                car.Vin = client.Vin;
                car.CarName = client.Vin;

                CarCharacteristic carCharacteristic = context.CarCharacteristics.Single(ch => ch.Id == car.Id);

                carCharacteristic.Oil = client.Oil;
                carCharacteristic.OilFilter = client.OilFilter;
                carCharacteristic.AirFilter = client.AirFilter;
                carCharacteristic.SalonFilter = client.SalonFilter;
                carCharacteristic.Сandles = client.Ngk;
                carCharacteristic.PadsFront = client.Padsfront;
                carCharacteristic.PadsRear = client.Padsrear;
                carCharacteristic.FuelFilter = client.Fuelfilter;

                Task task = Task.Run(context.SaveChanges);

                task.ContinueWith((t) => MessageBox.Show("Данные были успешно изменены"));

                client = new EntityClient();
                flag = false;
            }
        }
    }
}
