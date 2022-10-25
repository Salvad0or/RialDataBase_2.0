using RialDataBase_2._0.EntityClasses.BaseConnectClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace RialDataBase_2._0.Services.TgBot
{
    public class ButtonsMaster : TheWorkerBot


    {
     

        /// <summary>
        /// Обработчик кнопок
        /// </summary>
        /// <param name="buttonText"></param>
        /// <param name="chatId"></param>
        /// <returns></returns>
        public static async Task ButtonHandlerAsync(string buttonText,long chatId)
        {
   
            switch (buttonText)
            {
                case "Мой авто":
                    await ShowGarageToClient(chatId);
                    break;

                case "Мой баланс":
                    await ShowBalanceToClient(chatId);
                    break;

                case "Адрес" :
                    break;

                default:
                    break;
            }
      
        }

        /// <summary>
        /// Метод отправляющий клиенту данные о его автомобиле
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns></returns>
        private static async Task ShowGarageToClient(long chatId)
        {
            using (Context context = new Context())
            {
                var _garage = (from b in context.Bots
                               where b.ChatId == chatId

                               join c in context.Clients
                               on b.ClientId equals c.Id

                               join car in context.Cars
                               on c.Id equals car.ClientId

                               join carch in context.CarCharacteristics
                               on car.Id equals carch.CarId
 
                              

                              select new
                              {
                                  Auto = car.CarName ?? "Не указано",
                                  Vin = car.Vin ?? "Не указан",
                                  Oil = carch.Oil ?? "Не указано",
                                  OilFilter = carch.OilFilter ?? "Не указан",
                                  AirFilter = carch.AirFilter ?? "Не указан",
                                  SalonFilter = carch.SalonFilter ?? "Не указан",
                                  Candles = carch.Сandles ?? "Не указаы",
                                  PadsFront = carch.PadsFront ?? "Не указаы",
                                  PadsRear = carch.PadsRear ?? "Не указаы",
                                  FuelFilter = carch.FuelFilter ?? "Не указан"

                              }).FirstOrDefault();

                if (_garage is null)
                {
                    await WorkerBot.SendTextMessageAsync(chatId, "Произошла ошибка, обратитесь в Риальный магазин, что-то сломалось.");
                    return;
                }

                await WorkerBot.SendTextMessageAsync(chatId,

                    $"Авто: {_garage.Auto}\n" +
                    $"VIN : {_garage.Vin}\n" +
                    $"Масло моторное : {_garage.Oil}\n" +
                    $"Масляный фильтр : {_garage.OilFilter}\n" +
                    $"Воздушный фильтр : {_garage.AirFilter}\n" +
                    $"Салонный фильтр : {_garage.SalonFilter}\n" +
                    $"Свечи: {_garage.Candles}\n" +
                    $"Колодки передние: {_garage.PadsFront}\n" +
                    $"Колодки задние: {_garage.PadsRear}\n" +
                    $"Топливный фильтр: {_garage.FuelFilter}\n"
                    );              
            }
        }

        private static async Task ShowBalanceToClient(long chatId)
        {
            using (Context context = new Context())
            {
                var _balance = (from b in context.Bots
                               where b.ChatId == chatId

                               join c in context.Clients
                               on b.ClientId equals c.Id

                               join st in context.ClientStatuses
                               on c.StatusId equals st.Id

                               join cba in context.ClientBankAccouts
                               on c.Id equals cba.ClientId

                               select new
                               {      
                                   CashBack = cba.CashBack,
                                   Status = st.Status

                               }).FirstOrDefault();

                if(_balance is null)
                {
                    await WorkerBot.SendTextMessageAsync(chatId, "Произошла ошибка, обратитесь в Риальный магазин, что-то сломалось.");
                }

                await WorkerBot.SendTextMessageAsync(chatId,

                    $"Ваш кешбек - {_balance.CashBack}₽\n" +
                    $"Ваш статус - {_balance.Status}");
            };
        }

    }
}
