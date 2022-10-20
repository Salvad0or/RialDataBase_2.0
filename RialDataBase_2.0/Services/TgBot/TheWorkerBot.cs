using RialDataBase_2._0.EntityClasses.BaseConnectClass;
using RialDataBase_2._0.EntityClasses.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;

namespace RialDataBase_2._0.Services.TgBot
{
    internal class TheWorkerBot
    {
        #region private поля

        private TelegramBotClient Bot { get; set; }
        private CancellationTokenSource _cts { get; set; }
        private Bot bot { get; set; }
        private bool registerFlag { get; set; } = true;

        #endregion

        public TheWorkerBot(string token)
        {
            Bot = new TelegramBotClient(token);
            _cts = new CancellationTokenSource();

            CancellationToken cancellationToken = _cts.Token;

            Bot.StartReceiving(UpdateHandler, PoolingHandleError);

        }

        public async Task UpdateHandler(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
        {
            long chatId = update.Message.Chat.Id;
            string message = update.Message.Text;

            await using (Context context = new Context())
            {
                bot = (from b in context.Bots
                       where b.ChatId == chatId
                       select b).FirstOrDefault();
            }

            if (bot is null)
            {
                if (registerFlag)
                {
                    await botClient.SendTextMessageAsync(chatId, "Давайте Вас зарегестрируем. Введите номер телефона: ");
                    registerFlag = false;
                    return;
                }

                await MessageHandler.AddToBaseAsync(message, chatId, botClient);

            }
            else
            {
                await MessageHandler.WorkWithExistClient(bot, chatId, botClient);
            }


        }



        public async Task PoolingHandleError(ITelegramBotClient Bot, Exception e, CancellationToken token)
        {

        }

    }
}
