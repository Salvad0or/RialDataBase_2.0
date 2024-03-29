﻿using RialDataBase_2._0.EntityClasses.BaseConnectClass;
using RialDataBase_2._0.EntityClasses.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
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

            if (buttonText.StartsWith('#'))
            {
                await ActivatePromocode(chatId, buttonText);
                return;
            }

            switch (buttonText)
            {
                case "🚘 Авто":
                    await ShowGarageToClient(chatId);
                    break;

                case "💰 Баланс":
                    await ShowBalanceToClient(chatId);
                    break;

                case "📍 Адрес":

                    await SendAdress(chatId);
                    
                    break;

                case "💎 Промокод":
                    await HelloPromoCode(chatId);
                    break;         
         
                default:
                    DefaultMessage(chatId);
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

        /// <summary>
        /// Метод отправляющий клиенту данные о его балансе
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns></returns>
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

       /// <summary>
       /// Метод просто отправляет текст о вводе промокода
       /// </summary>
       /// <param name="chatId"></param>
       /// <returns></returns>
        private static async Task HelloPromoCode(long chatId)
        {
            await WorkerBot.SendTextMessageAsync(chatId,
                "Введите промокод начиная с <b>#</b>: ",
                ParseMode.Html);
        }

        /// <summary>
        /// Метод работающий с промокодом
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="promocodeName"></param>
        /// <returns></returns>
        private static async Task ActivatePromocode(long chatId,string promocodeName)
        {
            using (Context context = new Context())
            {

                Promocode promocode = context.Promocodes.First();

                Bot chatClient = (from b in context.Bots
                                  where b.ChatId == chatId
                                  select b).Single();

                var client = (from b in context.Bots
                              where b.ChatId == chatId

                              from c in context.Clients
                              where b.ClientId == c.Id

                              from cba in context.ClientBankAccouts
                              where cba.ClientId == c.Id

                              select new
                              {
                                  Name = c.Fname,
                                  CashBack = cba.CashBack
                              }).Single();



                if (promocode is null || !Equals(promocode.Name.ToLower(), promocodeName.ToLower()))
                {
                    await WorkerBot.SendTextMessageAsync(chatId,
                                                         $"Уважаемый {client.Name}\n" +
                                                         $"Вы ввели неверный промокод или его срок уже истек\n" +
                                                         $"Попробуйте еще раз или обратитесь к нам в магазин за помощью");
                    return;
                }

                if (chatClient.PromocodeId is not null)
                {
                    await WorkerBot.SendTextMessageAsync(chatId,
                                                        $"Бонус за промокод <strong>{promocode.Name}</strong> уже\n" +
                                                        $"был зачислен.\n" +
                                                        $"Дождитесь нового промокода, он будет скоро 🙂",
                                                        parseMode: ParseMode.Html);
                    return;
                }

                ClientBankAccout bankAccount = (from b in context.Bots
                                                where b.ChatId == chatId

                                                from c in context.Clients
                                                where b.ClientId == c.Id

                                                from cba in context.ClientBankAccouts
                                                where cba.ClientId == c.Id

                                                select cba).Single();

                bankAccount.CashBack += promocode.Sum;

                
                chatClient.PromocodeId = promocode.Id;


                await WorkerBot.SendTextMessageAsync(chatId,
                                                         $"Уважаемый {client.Name}\n" +
                                                         $"Промокод введен верно\n" +
                                                         $"На ваш баланс было добавлено {promocode.Sum} рублей\n" +
                                                         $"Текущий баланс составляет: {bankAccount.CashBack} рублей\n" +
                                                         $"Ждем Вас к нам с нетерпением!");

                context.SaveChanges();

                WriteMessageToAdmin($"{client.Name} ввел промокод {promocodeName}");

            }



        }

        /// <summary>
        /// Метод отправляющий адрес линками
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns></returns>
        private static async Task SendAdress(long chatId)
        {
            await WorkerBot.SendTextMessageAsync(chatId, "Мы находимся по адресу:\n" +
                                                         "Проспект Раиса Беляева,2Г \n" +
                                                         "ГСК Чайка\n" +
                                                         "Магазин - Риальный");
            await WorkerBot.SendPhotoAsync(chatId,
                photo: "https://vk.com/photo-47211478_457241338");

            InlineKeyboardMarkup keyBoard = new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithUrl("🚙 2-Гис",url: "https://go.2gis.com/4rh27"),

                    InlineKeyboardButton.WithUrl("🏎 Yandex",
                                    url: "https://yandex.ru/navi/?whatshere%5Bpoint%5D=52.417271%2C55.726727&whatshere%5Bzoom%5D=18")

                }
            });

            await WorkerBot.SendTextMessageAsync(chatId, "<b>Навигатор:</b>", 
                                                 replyMarkup: keyBoard, 
                                                 parseMode:ParseMode.Html);
            return;

        }

        private static async Task DefaultMessage(long chatId)
        {
            await WorkerBot.SendTextMessageAsync(chatId,
                "⚙️ <b>Кажется была введена неверная команда</b>\n" +
                "💰 Если Вы хотели ввести промокод, то он должен начинаться с <b>#</b>\n" +
                "⤵️ Для навигации используйте кнопки",
                ParseMode.Html
                );
        }

    }
}
