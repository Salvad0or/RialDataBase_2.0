using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RialDataBase_2._0.EntityClasses.Objects
{
    public class Bot
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public long ChatId { get; set; }

        public int? PromocodeId { get; set; }

        public virtual Promocode? Promocode { get; set; }
     
        public virtual Client? Cient { get; set; }
    }
}
