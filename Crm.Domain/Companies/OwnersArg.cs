using Crm.Domain.People;

namespace Crm.Domain.Companies;

public class OwnerArg
{
    public OwnerArg(Person person, double share)
    {
        Person = person;
        Share = share;
    }

    public Person Person { get; }
    public double Share { get; }
}