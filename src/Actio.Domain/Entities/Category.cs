using System;

namespace Actio.Domain.Entities
{
    public class Category
    {
        protected Category()
        {
        }

        public Category(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
    }
}