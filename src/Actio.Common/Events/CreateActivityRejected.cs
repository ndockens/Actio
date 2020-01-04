using System;

namespace Actio.Common.Events
{
    public class CreateActivityRejected
    {
        public string Reason { get; set; }
        public string Code { get; set; }
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        protected CreateActivityRejected()
        {

        }

        public CreateActivityRejected(Guid id, Guid userId)
        {
            Id = id;
            UserId = userId;
        }
    }
}