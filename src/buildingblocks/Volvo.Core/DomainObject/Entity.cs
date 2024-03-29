﻿using System;
using System.Collections.Generic;
using System.Text;
using Volvo.Core.Messages;

namespace Volvo.Core.DomainObject
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        private List<Event> _notification;
        public IReadOnlyCollection<Event> Notification => _notification?.AsReadOnly();

        public void AddEvent(Event evento)
        {
            _notification = _notification ?? new List<Event>();
            _notification.Add(evento);
        }

        public void RemoveEvent(Event eventItem)
        {
            _notification?.Remove(eventItem);
        }

        public void CleanEvent()
        {
            _notification?.Clear();
        }

        #region Comparações

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }

        #endregion
    }
}
