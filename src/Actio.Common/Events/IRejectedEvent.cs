namespace Actio.Common.Events
{
    public interface IRejectedEvent : IEvent
    {
        string Reason { get; set; }
        string Code { get; set; }
    }
}