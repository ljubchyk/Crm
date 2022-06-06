namespace Crm.Application
{
    public class Person
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public Address Address { get; set; }

        public static Person Create(Domain.People.Person person)
        {
            var result = new Person();
            Fill(result, person);
            return result;
        }

        public static void Fill(Person target, Domain.People.Person person)
        {
            target.Id = person.Id;
            target.FirstName = person.FirstName;
            target.FullName = person.FullName;
            target.LastName = person.LastName;

            if (person.Address is null)
            {
                target.Address = null;
            }
            else
            {
                target.Address = new Address
                {
                    City = person.Address.City,
                    Country = person.Address.Country,
                    Line1 = person.Address.Line1,
                    Line2 = person.Address.Line2,
                    PostalCode = person.Address.PostalCode
                };
            }
        }
    }
}
