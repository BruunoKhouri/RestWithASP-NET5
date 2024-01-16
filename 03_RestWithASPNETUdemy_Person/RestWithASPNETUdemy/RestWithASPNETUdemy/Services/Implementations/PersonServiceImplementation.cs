using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;
using System;
using System.Collections.Generic;
using System.IO;
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
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return person;
        }

        public void Delete(long id)
        {
            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null)
            {
                try
                {
                    _context.Persons.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        public List<Person> FindAll()
        {
            return _context.Persons.ToList();
        }


        public Person FindById(long id)
        {
            return _context.Persons.SingleOrDefault(p => p.Id == id);
        }

        public Person Update(Person person)
        {
            if (!Exists(person.Id)) return new Person();

            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return person;
        }

        public void UploadCurriculoPdf(long id, IFormFile curriculoPdf)
        {
            var person = _context.Persons.SingleOrDefault(p => p.Id == id);

            if (person == null) throw new InvalidOperationException("Person not found");

            if (curriculoPdf == null) throw new InvalidOperationException("File not found");

            byte[] curriculoPdfBytes;
            using (var memoryStream = new MemoryStream())
            {
                curriculoPdf.CopyTo(memoryStream);
                curriculoPdfBytes = memoryStream.ToArray();
            }

            _context.Attach(person);
            person.CurriculoPdf = curriculoPdfBytes;
            _context.SaveChanges();
        }

        public byte[] GetCurriculoPdf(long id)
        {
            var person = _context.Persons.SingleOrDefault(p => p.Id == id);

            if (person == null || person.CurriculoPdf == null || person.CurriculoPdf.Length == 0) throw new InvalidOperationException("Person not found || File not found");

            return person.CurriculoPdf;
        }

        private bool Exists(long id)
        {
            return _context.Persons.Any(p => p.Id.Equals(id));
        }
    }
}
