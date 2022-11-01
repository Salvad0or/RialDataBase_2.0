using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RialDataBase_2._0.EntityClasses.Objects
{
    public class Promocode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sum { get; set; }

        public virtual ICollection<Bot> Bots { get; set; }
    }
}
