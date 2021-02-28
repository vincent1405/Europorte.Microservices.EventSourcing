﻿namespace Europorte.Microservices.EventSourcing.Common.Entities
{
    public interface IEntity<out TKey>
    {
        TKey Id { get; }
    }
}
