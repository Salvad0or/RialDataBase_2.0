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
    public class TheWorkerBot
    {
        #region private поля

        private TelegramBotClient Bot { get; set; }
        private CancellationTokenSource _cts { get; set; }
        private Bot bot { get; set; }
        private bool registerFlag { get; set; } = true;

        const string ApiToken = "5156177003:AAFGMepLTciboz5oG6Yglm2usY3EchASwRc";

        #endregion

        public TheWorkerBot()
        {
            Bot = new TelegramBotClient(ApiToken);
            _cts = new CancellationTokenSource();

            CancellationToken cancellationToken = _cts.Token;

            Bot.StartReceiving(UpdateHandler, PoolingHandleError);

        }

        private async Task UpdateHandler(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
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
                await MessageHandler.WorkWithExistClientAsync(bot, chatId, botClient);
            }


        }

        private async Task PoolingHandleError(ITelegramBotClient Bot, Exception e, CancellationToken token)
        {

        }

        public async void SendInformationAboutCashBack(string message, long chatId)
        {          
            await MessageHandler.SendMessageAboutCashbackAsync(Bot, message, chatId);
        }

    }
}
