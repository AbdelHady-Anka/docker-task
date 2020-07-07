namespace Actio.Common.Events
{
    public class CreateActivityRejected : IRejectedEvent
    {
        protected CreateActivityRejected()
        {
        }

        public CreateActivityRejected(string message, string code)
        {
            Message = message;
            Code = code;
        }

        public string Message { get; }

        public string Code { get; }
    }
}