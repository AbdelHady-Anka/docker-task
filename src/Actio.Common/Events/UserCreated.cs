namespace Actio.Common.Events
{
    public class UserCreated : IEvent
    {
        public UserCreated(string email, string name)
        {
            Name = name;
            Email = email;
        }
        protected UserCreated() { }

        public string Name { get; }
        public string Email { get; }

    }
}