using RialDataBase_2._0.EntityClasses.BaseConnectClass;
using RialDataBase_2._0.EntityClasses.Objects;
using RialDataBase_2._0.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telegram.Bot;

namespace RialDataBase_2._0.EntityClasses.SqlCommands
{
    internal class InsertCommands
    {
        #region Команда добавления нового клиента

        /// <summary>
        /// Команда добавления клиента.
        /// </summary>
        /// <param name="client"></param>
        public void InsertNewClient(EntityClient client)
        {
            using (Context context = new Context())
            {

                Client newClient = new Client()
                {
                    Fname = client.Name,
                    Phone = client.Phone,
                    Date = DateTime.Now,
                    Comment = client.Comment,
                    StatusId = 1
                };

                context.Clients.Add(newClient);

                context.SaveChanges();

                context.ClientBankAccouts.Add(new ClientBankAccout()
                {
                    CashBack = client.CashBack / 100,
                    TotalPurchaseAmount = client.CashBack,
                    ClientId = context.Clients.Max(c => c.Id)
                });

                context.Cars.Add(new Car()
                {
                    CarName = client.Car,
                    Vin = client.Vin,
                    ClientId = context.Clients.Max(c => c.Id)
                });

                context.SaveChanges();

                context.CarCharacteristics.Add(new CarCharacteristic()
                {
                    AirFilter = client.AirFilter,
                    FuelFilter = client.Fuelfilter,
                    Oil = client.Oil,
                    OilFilter = client.OilFilter,
                    PadsFront = client.Padsfront,
                    PadsRear = client.Padsrear,
                    SalonFilter = client.SalonFilter,
                    Сandles = client.Ngk,
                    CarId = context.Cars.Max(p => p.Id)
                });

                context.SaveChanges();

                MessageBox.Show
                ($"Клиент {client.Name} успешно внесен в список " +
                 $"постоянных клиентов.");

            }


        }

        #endregion

        #region Команда добавления кешбека

        public void AddNewPromoCode( string promocodeName,  int promocodeSum)
        {        
            Task.Run(() => AddNewPromoCodeAsync(promocodeName, promocodeSum));    
        }

        public async Task AddNewPromoCodeAsync(string promocodeName, int promocodeSum)
        {
            await using (Context context = new Context())
            {
                Promocode promocode = context.Promocodes.First();

                if (promocode.Name == promocodeName || !promocodeName.StartsWith('#'))
                {
                    MessageBox.Show("Промокод с таким названием уже существует или отсутствует #");
                    return;
                }

                context.Promocodes.Remove(promocode);

                Promocode newPromocode = new Promocode()
                {
                    Name = promocodeName,
                    Sum = promocodeSum
                };

                context.Promocodes.Add(newPromocode);
           
                context.SaveChanges();                 
            }

            MessageBox.Show($"Промокод {promocodeName} добавлен");
        }

        #endregion
    }
}
