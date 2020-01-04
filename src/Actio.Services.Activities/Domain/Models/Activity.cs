using System;
using Actio.Common.Exceptions;

namespace Actio.Services.Activities.Domain.Models
{
    public class Activity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        protected Activity()
        {

        }

        public Activity(Guid id, string name, Category category,
            string description, Guid userId, DateTime createdAt)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ActioException("invalid_activity_name",
                    $"Activity name cannot be empty.");

            Id = id;
            Name = name.ToLowerInvariant();
            Category = category.Name;
            Description = description;
            UserId = userId;
            CreatedAt = createdAt;
        }
    }
}