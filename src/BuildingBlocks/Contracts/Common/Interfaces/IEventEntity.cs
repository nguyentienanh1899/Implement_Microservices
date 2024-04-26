﻿using Contracts.Common.Events;
using Contracts.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Common.Interfaces
{
    public interface IEventEntity
    {
        void AddDomainEvent(BaseEvent domainEvent);
        void RemoveDomainEvent(BaseEvent domainEvent);
        void ClearDomainEvent();
        IReadOnlyCollection<BaseEvent> DomainEvents(); //Get list domain event
    }

    public interface IEventEntity<T> : IEntityBase<T>, IEventEntity
    {

    }
}
