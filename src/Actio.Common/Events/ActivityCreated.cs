using System;

namespace Actio.Common.Events
{
    public class ActivityCreated : IAuthenticatedEvent
    {
        public ActivityCreated(Guid id, Guid userId, string category, string name, string description, DateTime createdAt)
        {
            Id = id;
            UserId = userId;
            Category = category;
            Name = name;
            Description = description;
            CreatedAt = createdAt;
        }

        protected ActivityCreated()
        {
        }

        public Guid Id { get;  }
        public Guid UserId { get;  }
        public string Category { get;  }
        public string Name { get;  }
        public string Description { get;  }
        public DateTime CreatedAt { get;  }
    }
}