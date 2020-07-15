using MediatR;
using System;
using System.Collections.Generic;

namespace CarsIsland.Rent.Domain.Common
{
    public abstract class Entity
    {
        private Guid _id;
        public virtual Guid Id
        {
            get
            {
                return _id;
            }
            protected set
            {
                _id = value;
            }
        }

        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}
