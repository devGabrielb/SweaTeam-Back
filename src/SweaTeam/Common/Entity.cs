using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SweaTeam.Common
{
    public abstract class Entity
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        protected Entity(Guid id)
        {
            Id = id;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        protected Entity()
        {
        }

        public Guid Id { get; init; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public IReadOnlyList<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents.ToList();
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

#pragma warning disable CA1030 // Use events where appropriate
        protected void RaiseDomainEvent(IDomainEvent domainEvent)
#pragma warning restore CA1030 // Use events where appropriate
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
