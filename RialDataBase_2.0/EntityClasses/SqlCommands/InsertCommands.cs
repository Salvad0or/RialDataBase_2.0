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
    internal class InsertCommands
    {
        public void InsertNewClient(EntityClient client)
        {

            if (client is null) return;

            using (Context context = new Context())
            {
                Client newClient = new Client()
                {
                    Fname = client.Name,
                    Phone = client.Phone,
                    Date = DateTime.Now,
                    StatusId = 1
                };
     
                context.Clients.Add(newClient);

                context.SaveChanges();

                int i = context.Clients.Count();

                context.ClientBankAccouts.Add(new ClientBankAccout()
                {
                    CashBack = client.CashBack,
                    TotalPurchaseAmount = client.CashBack * 100,
                    ClientId = context.Clients.Max(c => c.Id)
                }) ;
               
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
                }) ;

                context.SaveChanges();
            }

        }
    }
}
