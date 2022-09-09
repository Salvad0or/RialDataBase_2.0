using RialDataBase_2._0.EntityClasses.BaseConnectClass;
using RialDataBase_2._0.EntityClasses.Objects;
using RialDataBase_2._0.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RialDataBase_2._0.Services
{
    internal static class ClassWorker
    {
        public static EntityClient FillClient(string phone, ref bool Flag)
        {
            if (Inspector.SearchClient(phone))
            {
                MessageBox.Show("Клиент не найден");
                return new EntityClient()
                {
                    Phone = phone
                };          
            }

            EntityClient newClient;

            using (Context context = new Context())
            {
                Client client = context.Clients.First(p => p.Phone == phone);
                ClientBankAccout clientBank = context.ClientBankAccouts.First(b => b.ClientId == client.Id);
                Car car = context.Cars.First(c => c.ClientId == client.Id);
                CarCharacteristic carCh = context.CarCharacteristics.First(cH => cH.CarId == car.Id);

                newClient = new EntityClient()
                {
                    Name = client.Fname,
                    Phone = phone,
                    Status = (StatusEnum)client.StatusId,
                    Comment = client.Comment,
                    CashBack = clientBank.CashBack ?? 0,
                    TotalPurchaseAmount = clientBank.TotalPurchaseAmount ?? 0,
                    Car = car.CarName,
                    Vin = car.Vin,
                    Oil = carCh.Oil,
                    OilFilter = carCh.OilFilter,
                    AirFilter = carCh.AirFilter,
                    SalonFilter = carCh.SalonFilter,
                    Ngk = carCh.Сandles,
                    Padsfront = carCh.PadsFront,
                    Padsrear = carCh.PadsRear,
                    Fuelfilter = carCh.FuelFilter
                };      
            }

            Flag = true;
            return newClient;
        }
    }
}
