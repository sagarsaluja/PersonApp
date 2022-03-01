using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonApp.Data;
using PersonApp.Models;
using PersonApp.Repository;

namespace PersonTests
{
    public class PersonFake : IPersonRepository
    {
        private readonly List<Person> _people;
        public PersonFake()
        {
            _people = new List<Person>()
            {
                new Person(){ Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),FirstName = "Alpha" , LastName = "Beeta" , Age = 25},
                new Person(){ Id = new Guid("815accac-fd5b-478a-a9d6-f171a2f6ae7f"),FirstName = "Harry" , LastName = "Potter" , Age = 34},
                new Person(){ Id = new Guid("33704c4a-5b87-464c-bfb6-51971b4d18ad"),FirstName = "Steve" , LastName = "Vai" , Age = 55}
                
            };
        }

        public Task<Person> AddPersonAsync(PersonModel PModel)
        {
            //throw new NotImplementedException();
            PModel.Id = Guid.NewGuid();
            var p = new Person();
            p.Id = PModel.Id;
            p.FirstName = PModel.FirstName;
            p.LastName = PModel.LastName;
            p.Age = PModel.Age;

            _people.Add(p);
            return Task.FromResult(p);
        }

        public Task<Person> DeletePersonAsync(Guid PID)
        {
            throw new NotImplementedException();
        }

        public Task<List<PersonModel>> GetallPersonsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PersonModel> GetPersonByIdAsync(Guid PersonId)
        {
            throw new NotImplementedException();
        }

        public Task<Person> UpdatePersonAsync(PersonModel PModel, Guid PID)
        {
            throw new NotImplementedException();
        }
    }
}
