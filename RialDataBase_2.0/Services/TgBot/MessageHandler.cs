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
                await WorkerBot.SendTextMessageAsync(ChatId, "Пожалуйста проверьте ввод и попробуйте еще раз");
                return;
            }

            await using (Context context = new Context())
            {
                Client client = (from c in context.Clients
                                 where c.Phone == message
                                 select c).FirstOrDefault();

                if (client is null)
                {
                    await WorkerBot.SendTextMessageAsync(ChatId, "Номер был введен корректно,но я не нашел Вас в нашей базе данных,\n " +
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
                        await WorkerBot.SendTextMessageAsync(ChatId, $"Уважемый(ая) {client.Fname},\n" +
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

                    await WorkerBot.SendTextMessageAsync(ChatId, $"Уважаемый(ая) {client.Fname},\n" +
                                                                 $"Вы были успешно добавлены в базу данных.\n" +
                                                                 $"Приветственный бонус в размере 100 рублей был зачислен.");

                }
            }
        }           
    }
}
