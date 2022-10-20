using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RialDataBase_2._0.Services.TgBot
{
    public class TgBot
    {
        const string ApiToken = "5156177003:AAFGMepLTciboz5oG6Yglm2usY3EchASwRc";

        public void Start()
        {
            TheWorkerBot telegramBot = new TheWorkerBot(ApiToken);
            Console.Read();
        }

    }
}
