using Crm.Domain.People;

namespace Crm.Domain.Company;

public class SetOwnersArg
{
    public SetOwnersArg(Person person, double share)
    {
        Person = person;
        Share = share;
    }

    public Person Person { get; }
    public double Share { get; }
}