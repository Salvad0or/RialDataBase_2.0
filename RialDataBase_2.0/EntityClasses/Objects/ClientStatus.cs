using System;
using System.Collections.Generic;

namespace RialDataBase_2._0.EntityClasses.Objects
{
    public partial class ClientStatus
    {
        public ClientStatus()
        {
            Clients = new HashSet<Client>();
        }

        public byte Id { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}
