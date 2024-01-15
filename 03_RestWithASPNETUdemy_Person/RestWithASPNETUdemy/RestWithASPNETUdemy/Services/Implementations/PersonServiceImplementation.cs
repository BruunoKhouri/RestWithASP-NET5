using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RestWithASPNETUdemy.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        private volatile int count;
        private MySQLContext _context;

        public PersonServiceImplementation(MySQLContext context) 
        { 
            _context = context;
        }

        public Person Create(Person person)
        {
            return person;        
        }

        public void Delete(long id)
        {

        }

        public List<Person> FindAll()
        {

           
            return _context.Persons.ToList();
        }


        public Person FindById(long id)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = "Brunno",
                LastName = "Borges",
                Address = "Colombo - Parana - Brasil",
                Gender = "Masculino"
            };
        }

        public Person Update(Person person)
        {
            return person;
        }    

        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }
    }
}
