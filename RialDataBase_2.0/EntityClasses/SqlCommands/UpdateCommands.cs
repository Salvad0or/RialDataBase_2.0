using RialDataBase_2._0.EntityClasses.BaseConnectClass;
using RialDataBase_2._0.EntityClasses.Objects;
using RialDataBase_2._0.Model;
using RialDataBase_2._0.Services.TgBot;
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
        public static void AddCashBackAsync(EntityClient client, TheWorkerBot botClient, int cashback)
        {
            Task.Factory.StartNew(() => AddCashBack(client, botClient, cashback));
        }

        /// <summary>
        /// Метод списывания кешбека
        /// </summary>
        /// <param name="client"></param>
        /// <param name="cashback"></param>
        public static void SpendCashBackAsync(EntityClient client, TheWorkerBot botClient, int cashback)
        {
            Task.Run(() => SpendCashBack(client, botClient, cashback));

        }

        /// <summary>
        /// Синхронный метод добавления кешбека
        /// </summary>
        /// <param name="client"></param>
        /// <param name="cashback"></param>
        public static void AddCashBack(EntityClient client,TheWorkerBot botClient, int cashback)
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

                string message = $"Кешбек добавлен, " +
                      $"баланс {client.CashBack} руб.";

                MessageBox.Show(message);
                      

                var bot = (from b in context.Bots
                           where b.ClientId == (from c in context.Clients
                                                where c.Phone == client.Phone
                                                select c).Single().Id
                           select b).FirstOrDefault();

                if (bot is null) return;

                if(flag)
                {
                    message += "\nТак же позвольте Вас поздравить с получением нового статуса!\n" +
                               $"Теперь Ваш статус - {client.Status}!";
                }

                botClient.SendInformationAboutCashBack(message, bot.ChatId);

            }
        }
   
        /// <summary>
        /// Синхронный метод списывания кешбека
        /// </summary>
        /// <param name="client"></param>
        /// <param name="cashback"></param>
        public static void SpendCashBack(EntityClient client, TheWorkerBot botClient, int cashback)
        {
            using (Context context = new Context())
            {
                ClientBankAccout cba = context.ClientBankAccouts.Single

                    (i => i.ClientId == context.Clients.Single
                    (c => c.Phone == client.Phone).Id);

                //var cba = (from c in context.ClientBankAccouts
                //          where c.ClientId == (from o in context.Clients
                //                               where o.Phone == client.Phone
                //                               select o).Single().Id
                //           select c).Single();

                cba.CashBack -= cashback;

                context.SaveChanges();


                string message = "Кешбек успешно списан, " +
                   $"баланс равен {cba.CashBack} руб";

                MessageBox.Show(message);


                var bot = (from b in context.Bots
                           where b.ClientId == (from c in context.Clients
                                                where c.Phone == client.Phone
                                                select c).Single().Id
                           select b).FirstOrDefault();

                if (bot is null) return;
               

                botClient.SendInformationAboutCashBack(message, bot.ChatId);
            }
        }

        /// <summary>
        /// Метод изменения данных о клиенте
        /// </summary>
        /// <param name="client"></param>
        /// <param name="flag"></param>
        /// <param name="phone"></param>
        public static void ChangeClientData(EntityClient client, ref bool flag, string phone)
        {
            using (Context context = new Context())
            {
                Client EntityClient = context.Clients.Single(p => p.Phone == phone);
                EntityClient.Fname = client.Name;
                EntityClient.Phone = client.Phone;
                EntityClient.Comment = client.Comment;
                

                Car car = context.Cars.Single(c => c.ClientId == EntityClient.Id);
                car.Vin = client.Vin;
                car.CarName = client.Car;
                

                CarCharacteristic carCharacteristic = context.CarCharacteristics.Single(ch => ch.CarId == car.Id);
                carCharacteristic.Oil = client.Oil;
                carCharacteristic.OilFilter = client.OilFilter;
                carCharacteristic.AirFilter = client.AirFilter;
                carCharacteristic.SalonFilter = client.SalonFilter;
                carCharacteristic.Сandles = client.Ngk;
                carCharacteristic.PadsFront = client.Padsfront;
                carCharacteristic.PadsRear = client.Padsrear;
                carCharacteristic.FuelFilter = client.Fuelfilter;

                context.SaveChanges();
                
                MessageBox.Show("Данные были успешно изменены");      

                flag = false;
            }
        }

        public static void AddNewPromocode(string promocode, int sum)
        {
            
        }
    }
}
