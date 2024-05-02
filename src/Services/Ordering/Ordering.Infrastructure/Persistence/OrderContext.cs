﻿using Contracts.Domains;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Infrastructure.Extensions;
using Serilog;
using Contracts.Common.Events;
using Contracts.Common.Interfaces;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContext : DbContext
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public OrderContext(DbContextOptions<OrderContext> options, IMediator mediator, ILogger logger) : base(options)
        {
            _mediator = mediator;
            _logger = logger;
        }

        private List<BaseEvent> _baseEvent;
        private void SetBaseEventsBeforeSaveChanges()
        {
            var domainEntities = ChangeTracker.Entries<IEventEntity>().Select(x => x.Entity).Where(x => x.DomainEvents().Any()).ToList();
            _baseEvent = domainEntities.SelectMany(x => x.DomainEvents()).ToList();

            domainEntities.ForEach(x => x.ClearDomainEvent());
        }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SetBaseEventsBeforeSaveChanges();
            var modified = ChangeTracker.Entries()
                                .Where(e => e.State == EntityState.Modified ||
                                            e.State == EntityState.Added ||
                                            e.State == EntityState.Detached);

            foreach (var item in modified)
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        if (item.Entity is IDateTracking addedEntity)
                        {
                            addedEntity.CreatedDate = DateTime.UtcNow;
                            item.State = EntityState.Added;
                        }
                        break;

                    case EntityState.Modified:
                        Entry(item.Entity).Property("Id").IsModified = false;
                        if (item.Entity is IDateTracking modifiedEntity)
                        {
                            modifiedEntity.LastModifiedDate = DateTime.UtcNow;
                            item.State = EntityState.Modified;
                        }
                        break;
                }
            }

            var result = base.SaveChangesAsync(cancellationToken);
            _mediator.DispatchDomainEventAsync(_baseEvent, _logger);
            return result;
        }
    }
}
