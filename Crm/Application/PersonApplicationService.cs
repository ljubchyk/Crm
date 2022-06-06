using Crm.Domain.People;

namespace Crm.Application
{
    public class PersonApplicationService : IPersonApplicationService
    {
        private readonly IPersonRepository personRepository;

        public PersonApplicationService(IPersonRepository personRepository)
        {
            this.personRepository = personRepository ??
                throw new ArgumentNullException(nameof(personRepository));
        }

        public async Task Create(Person model)
        {
            Domain.Address address = null;
            if(model.Address is not null)
            {
                address = new Domain.Address(
                    model.Address.Country,
                    model.Address.City,
                    model.Address.PostalCode,
                    model.Address.Line1,
                    model.Address.Line2);
            }

            var person = new Domain.People.Person(
                Guid.NewGuid(),
                model.FirstName,
                model.LastName,
                address);

            await personRepository.Create(person);
        }

        public async Task<Person> Get(Guid id)
        {
            var person = await personRepository.Get(id);
            if (person is null)
            {
                return null;
            }

            return Person.Create(person);
        }

        public async Task<List<Person>> GetList()
        {
            var people = await personRepository.GetList();
            return people.Select(person => Person.Create(person)).ToList();
        }

        public async Task Update(Person model)
        {
            var person = await personRepository.Get(model.Id);
            if (person is null)
            {
                throw new InvalidOperationException($"Unknown person with id: {model.Id}");
            }

            person.Rename(model.FirstName, model.LastName);
            if(model.Address is null)
            {
                person.Relocate(null);
            }
            else
            {
                person.Relocate(
                    new Domain.Address(
                        model.Address.Country,
                        model.Address.City,
                        model.Address.PostalCode,
                        model.Address.Line1,
                        model.Address.Line2));
            }

            await personRepository.Update(person);
            Person.Fill(model, person);
        }
    }
}