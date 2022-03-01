using PersonApp.Data;
using PersonApp.Models;

namespace PersonApp.Repository
{
    public interface IPersonRepository
    {
        Task<List<PersonModel>> GetallPersonsAsync();
        Task<PersonModel> GetPersonByIdAsync(Guid PersonId);
        Task<Person> AddPersonAsync(PersonModel PModel);
        Task<Person> UpdatePersonAsync(PersonModel PModel  ,Guid PID);
        Task<Person> DeletePersonAsync(Guid PID);
    }
}
