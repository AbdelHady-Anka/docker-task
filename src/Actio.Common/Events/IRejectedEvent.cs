namespace Actio.Common.Events
{
    public interface IRejectedEvent : IEvent
    {
        string Message { get; }
        string Code { get; }
    }
}