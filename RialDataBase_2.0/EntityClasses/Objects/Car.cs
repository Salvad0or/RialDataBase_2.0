using System;
using System.Collections.Generic;

namespace RialDataBase_2._0.EntityClasses.Objects
{
    public partial class Car
    {
        public int Id { get; set; }
        public string? CarName { get; set; }
        public string? Vin { get; set; }
        public int? ClientId { get; set; }

        public virtual Client? Client { get; set; }
        public virtual CarCharacteristic? CarCharacteristic { get; set; }
    }
}
