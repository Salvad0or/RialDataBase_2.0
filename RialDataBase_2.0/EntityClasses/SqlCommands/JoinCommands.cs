using RialDataBase_2._0.EntityClasses.BaseConnectClass;
using RialDataBase_2._0.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RialDataBase_2._0.EntityClasses.SqlCommands
{
    internal static class JoinCommands
    {
        public static ObservableCollection<EntityClient> JoinAllData()
        {

            List<EntityClient> EverythingJoined = new List<EntityClient>();
            ObservableCollection<EntityClient> returnClient;

            using (Context context = new Context())
            {
                EverythingJoined = (from c in context.Clients
                                    join cs in context.ClientStatuses
                                    on c.StatusId equals cs.Id
                                    join cba in context.ClientBankAccouts
                                    on c.Id equals cba.ClientId
                                    join car in context.Cars
                                    on c.Id equals car.ClientId
                                    join cha in context.CarCharacteristics
                                    on car.Id equals cha.CarId

                                    select new EntityClient
                                    {
                                        Id = c.Id,
                                        Vin = car.Vin ?? String.Empty,
                                        Name = c.Fname ?? String.Empty,
                                        Phone = c.Phone,
                                        Car = car.CarName ?? String.Empty,
                                        Oil = cha.Oil ?? String.Empty,
                                        OilFilter = cha.OilFilter ?? String.Empty,
                                        AirFilter = cha.AirFilter ?? String.Empty,
                                        SalonFilter = cha.SalonFilter ?? String.Empty,
                                        CashBack = cba.CashBack ?? 0,
                                        Ngk = cha.Сandles ?? String.Empty,
                                        Padsfront = cha.PadsFront ?? String.Empty,
                                        Padsrear = cha.PadsRear ?? String.Empty,
                                        Fuelfilter = cha.FuelFilter ?? String.Empty,
                                        Comment = c.Comment ?? String.Empty,
                                        Date = (DateTime)c.Date,
                                        Status = (StatusEnum)c.StatusId,
                                        TotalPurchaseAmount = cba.TotalPurchaseAmount ?? 0

                                    }).ToList();

                returnClient = new ObservableCollection<EntityClient>();

                for (int i = 0; i < EverythingJoined.Count; i++)
                {
                    returnClient.Add(EverythingJoined[i]);
                }

            }

            return returnClient;
        }
    }
}
