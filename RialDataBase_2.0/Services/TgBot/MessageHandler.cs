using RialDataBase_2._0.EntityClasses.BaseConnectClass;
using RialDataBase_2._0.EntityClasses.Objects;
using RialDataBase_2._0.EntityClasses.SqlCommands;
using RialDataBase_2._0.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace RialDataBase_2._0.Services.TgBot
{
    public class MessageHandler : TheWorkerBot
    {
        /// <summary>
        /// Метод обработки нового клиента
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ChatId"></param>
        /// <param name="ClientBot"></param>
        /// <returns></returns>
        public async static Task AddToBaseAsync(string message, long ChatId, TheWorkerBot theWorkerbot)
        {
            bool flag = int.TryParse(message, out int phone);

            if (!flag && message.Length != 11)
            {
                await ClientBot.SendTextMessageAsync(ChatId, "Пожалуйста проверьте ввод и попробуйте еще раз");
                return;
            }

            await using (Context context = new Context())
            {
                Client client = (from c in context.Clients
                                 where c.Phone == message
                                 select c).FirstOrDefault();

                if (client is null)
                {
                    await ClientBot.SendTextMessageAsync(ChatId, "Номер был введен корректно,но я не нашел Вас в нашей базе данных,\n " +
                                                                 "Пожалуйста обратитесь за помощью в наш магазин по адресу: \n" +
                                                                 "Проспект Раиса Беляева, ГСК Чайка 2Г. Магазин Риальный, Вам помогут\n " +
                                                                 "Номер нашего телефона: 89270482078\n " +
                                                                 "Или попробуйте еще раз.");
                    return;
                }

                else
                {

                    Bot newBot = new Bot()
                    {
                        ChatId = ChatId,
                        ClientId = client.Id
                    };

                    var lastChek = (from l in context.Bots
                                    where l.ClientId == newBot.ClientId
                                    select l).FirstOrDefault();

                    if (lastChek is not null)
                    {
                        await ClientBot.SendTextMessageAsync(ChatId, $"Уважемый {client.Fname},\n" +
                                                                     $"По каким-то причинам Вы уже числитесь в базе данных\n" +
                                                                     $"Обратитесь к нам в магазин по адресу :\n" +
                                                                     $"Проспект Раиса Беляева, ГСК Чайка 2Г. Магазин Риальный, Вам помогут");

                        return;
                    }


                    context.Bots.Add(newBot);

                    var cash = (from c in context.Clients
                                where c.Phone == client.Phone
                                from cba in context.ClientBankAccouts
                                where cba.ClientId == c.Id
                                select cba).Single();

                    cash.CashBack += 100;

                    await context.SaveChangesAsync();

                    await ClientBot.SendTextMessageAsync(ChatId, $"Уважаемый {client.Fname},\n" +
                                                                 $"Вы были успешно добавлены в базу данных.\n" +
                                                                 $"Приветственный бонус в размере 100 рублей был зачислен.");

                    
                }
            }



        }

        public async static Task WorkWithExistClientAsync(Bot clientFromChatBot, long chatId)
        {


            await using (Context contex = new Context())
            {
                var ExistClient = from c in contex.Clients
                                  where c.Id == clientFromChatBot.ClientId

                                  join status in contex.ClientStatuses
                                  on c.StatusId equals status.Id

                                  join cba in contex.ClientBankAccouts
                                  on c.Id equals cba.ClientId

                                  join car in contex.Cars
                                  on c.Id equals car.ClientId

                                  join carCh in contex.CarCharacteristics
                                  on car.Id equals carCh.CarId

                                  select new
                                  {
                                      Name = c.Fname ?? "Не было указано",
                                      Status = status.Status,
                                      CashBack = cba.CashBack,
                                      Total = cba.TotalPurchaseAmount,
                                      Oil = carCh.Oil ?? "Не было указано",
                                      Vin = car.Vin ?? "Не было указано",
                                  };


                foreach (var exist in ExistClient)
                {
                    await ClientBot.SendTextMessageAsync(chatId, $"Ваш кешбек составляет: {exist.CashBack}\n" +
                                                                 $"Ваше имя: {exist.Name}\n" +
                                                                 $"Всего вы потратили у нас: {exist.Total}\n" +
                                                                 $"Ваш клиентский статус: {exist.Status}\n" +
                                                                 $"Вы заливаете масло: {exist.Oil}\n" +
                                                                 $"Вин номер вашего авто: {exist.Vin}\n");
                }


            }

        }

        
    }
}
