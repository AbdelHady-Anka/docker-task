using System;
using Actio.Common.Exceptions;

namespace Actio.Domain.Entities
{
    public class Activity
    {
        public Activity(Guid id, Guid userId, string category, string name, string description, DateTime createdAt)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ActioException("empty_activity_name",
                $"Activiy name can not be empty.");
            }
            Id = id;
            UserId = userId;
            Category = category;
            Name = name;
            Description = description;
            CreatedAt = createdAt;
        }

        protected Activity()
        {
        }


        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Category { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }
}