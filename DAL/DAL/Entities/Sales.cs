using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Sales
{
    public class Customer
    {
        public virtual int Id { get; protected set; }
        public virtual string AccountNumber { get; set; }
        public virtual Person.Person Person { get; set; }
    }
}
