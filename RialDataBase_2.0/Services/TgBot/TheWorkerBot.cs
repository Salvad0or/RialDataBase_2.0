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

        protected static TelegramBotClient ClientBot { get; set; }
        private CancellationTokenSource _cts { get; set; }
        private Bot _bot { get; set; }
        private bool registerFlag { get; set; } = true;

        const string ApiToken = "5156177003:AAFGMepLTciboz5oG6Yglm2usY3EchASwRc";

        #endregion

        public TheWorkerBot()
        {
            ClientBot = new TelegramBotClient(ApiToken);
            _cts = new CancellationTokenSource();

            CancellationToken cancellationToken = _cts.Token;

            ClientBot.StartReceiving(UpdateHandler, PoolingHandleError);

        }

        private async Task UpdateHandler(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
        {
            long ChatId = update.Message.Chat.Id;
            string _message = update.Message.Text;

            await using (Context context = new Context())
            {
                _bot = (from b in context.Bots
                       where b.ChatId == ChatId
                       select b).FirstOrDefault();
            }

            if (_bot is null)
            {
                if (registerFlag)
                {
                    await botClient.SendTextMessageAsync(ChatId, "Кажется вы обратились ко мне впервые.\n" +
                                                                 "Давайте вас зарегестрируем - это не долго.\n" +
                                                                 "Введите номер телефона: ");
                    registerFlag = false;
                    return;
                }

                await MessageHandler.AddToBaseAsync(_message, ChatId, this);

            }
            else
            {
                await MessageHandler.WorkWithExistClientAsync(_bot, ChatId);
            }


        }

        private async Task PoolingHandleError(ITelegramBotClient Bot, Exception e, CancellationToken token)
        {

        }

        public async void SendInformationAboutCashBack(string message, long chatId)
            =>
            await ClientBot.SendTextMessageAsync(chatId, message);


    }
}
