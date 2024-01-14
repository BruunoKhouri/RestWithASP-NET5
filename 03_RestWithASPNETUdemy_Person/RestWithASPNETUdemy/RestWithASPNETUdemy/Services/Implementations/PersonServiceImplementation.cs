using RestWithASPNETUdemy.Model;
using System;
using System.Collections.Generic;
using System.Threading;

namespace RestWithASPNETUdemy.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        private volatile int count;

        public Person Create(Person person)
        {
            return person;
            throw new System.NotImplementedException();
        }

        public void Delete(long id)
        {

        }

        public List<Person> FindAll()
        {

            List<Person> persons = new List<Person>();
            for (int i = 0; i < 8; i++)
            {
                Person person = MockPerson(i);
                persons.Add(person);
            }
            return persons;
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

        private Person MockPerson(int i)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = "Person Name " + i.ToString(),
                LastName = "Person LastName",
                Address = "Colombo - Parana - Brasil",
                Gender = "Masculino"
            };
        }

        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }
    }
}
