using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;


namespace DAL.Mappings.Person
{
    
    public class BusinessEntityMap : ClassMap<Entities.Person.BusinessEntity>
    {
        public BusinessEntityMap()
        {
            Table("Person.BusinessEntity");
            Id(b => b.Id).Column("BusinessEntityID").GeneratedBy.Identity();
        }
    }
    
    public class PersonMap : SubclassMap<DAL.Entities.Person.Person>
    {
        public PersonMap()
        {
            Table("Person.Person");
            Map(p => p.Title);
            Map(p => p.LastName);
            Map(p => p.FirstName);
            Map(p => p.PersonType);
            Map(p => p.EmailPromotion);
            KeyColumn("BusinessEntityID");
        }
    }
}
