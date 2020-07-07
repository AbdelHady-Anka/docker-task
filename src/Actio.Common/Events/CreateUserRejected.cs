namespace Actio.Common.Events
{
    public class CreateUserRejected : IRejectedEvent
    {
        protected CreateUserRejected()
        {
        }

        public CreateUserRejected(string email, string message, string code)
        {
            Email = email;
            Message = message;
            Code = code;
        }

        public string Message { get; }
        public string Code { get; }
        public string Email { get; }
    }
}