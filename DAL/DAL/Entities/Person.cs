using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Person
{
    [Serializable]
    public abstract class BusinessEntity
    {
        public virtual int Id { get; set; }
        
    }
    
    
    [Serializable]
    public class Person :BusinessEntity
    {
        public virtual string PersonType { get;  set; }
        public virtual string Title { get;  set; }
        public virtual string FirstName { get;  set; }
        public virtual string LastName { get;  set; }
        public virtual int EmailPromotion { get; set; }
    }
}
