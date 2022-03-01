using Microsoft.EntityFrameworkCore;
using PersonApp.Data;
using PersonApp.Models;

namespace PersonApp.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PersonContext _context;

        public PersonRepository(PersonContext context)
        {
            _context = context;
        }
        public async Task<List<PersonModel>> GetallPersonsAsync()
        {
            var records = await _context.Persons.Select(x => new PersonModel()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Age = x.Age,
            }).ToListAsync();
            return records;
        }
        public async Task<PersonModel> GetPersonByIdAsync(Guid PersonId)
        {
            var records = await _context.Persons.Where(x => x.Id == PersonId).Select(x => new PersonModel()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Age = x.Age,
            }).FirstOrDefaultAsync();
            return records;
        }
        public async Task<Person> AddPersonAsync(PersonModel PModel)
        {
            var p = new Person()
            {
                FirstName = PModel.FirstName,
                LastName = PModel.LastName,
                Age = PModel.Age,
            };
            _context.Persons.Add(p);
            await _context.SaveChangesAsync();
            return p;
        }
        public async Task<Person> UpdatePersonAsync(PersonModel PModel, Guid PID)
        {
            var records = await _context.Persons.FindAsync(PID);
            if (records != null)
            {
                records.FirstName = PModel.FirstName;
                records.LastName = PModel.LastName;
                records.Age = PModel.Age;

                await _context.SaveChangesAsync();
            }
            return records;
        }
        public async Task<Person> DeletePersonAsync(Guid PID)
        {
            var records = _context.Persons.Where(x => x.Id == PID).Single();
            _context.Persons.Remove(records);
            await _context.SaveChangesAsync();
            return records;

        }
    }
}
