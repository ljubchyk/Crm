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
            var person = new Domain.People.Person(
                Guid.NewGuid(),
                model.FirstName,
                model.LastName,
                TryCreateAddress(model.Address));

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
            person.Relocate(TryCreateAddress(model.Address));

            await personRepository.Update(person);
            Person.Fill(model, person);
        }

        private static Domain.Address TryCreateAddress(Address address)
        {
            if (address is null)
            {
                return null;
            }

            return new Domain.Address(
                address.Country,
                address.City,
                address.PostalCode,
                address.Line1,
                address.Line2);
        }
    }
}