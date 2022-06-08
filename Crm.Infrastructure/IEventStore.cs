using Crm.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Infrastructure
{
    public interface IEventStore
    {
        Task<StoredEvent> Add(DomainEvent domainEvent);
        Task Remove(StoredEvent storedEvent);
        Task<List<StoredEvent>> GetList();
    }
}
