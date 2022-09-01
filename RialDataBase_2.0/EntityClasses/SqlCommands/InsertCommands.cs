using RialDataBase_2._0.EntityClasses.BaseConnectClass;
using RialDataBase_2._0.EntityClasses.Objects;
using RialDataBase_2._0.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RialDataBase_2._0.EntityClasses.SqlCommands
{
    internal class InsertCommands
    {
        public void InsertNewClient(EntityClient client)
        {

            using (Context context = new Context())
            {
                context.Clients.Add(new Client()
                {
                    Fname = client.Name,
                    Phone = client.Phone,
                    Date = client.Date,
                    StatusId = 1
                });

                
            }

        }
    }
}
