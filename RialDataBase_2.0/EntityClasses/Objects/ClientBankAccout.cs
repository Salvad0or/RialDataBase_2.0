using System;
using System.Collections.Generic;

namespace RialDataBase_2._0.EntityClasses.Objects
{
    public partial class ClientBankAccout
    {
        public int Id { get; set; }
        public decimal? CashBack { get; set; }
        public decimal? TotalPurchaseAmount { get; set; }
        public int? ClientId { get; set; }

        public virtual Client? Client { get; set; }
    }
}
