using OutboxPatternDemo.Domain.DomainEvents;
using OutboxPatternDemo.Domain.Entities.Base;

namespace OutboxPatternDemo.Domain.Entities
{
    public class User : AggregateRoot
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        private User(Guid id, string firstName, string lastName, string email) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
        
        public static User Create(Guid id, string firstName, string lastName, string email)
        {
            var user = new User(id, firstName, lastName, email);

            user.RaiseDomainEvent(new UserRegisteredDomainEvent(user.Id));

            return user;
        }
    }
}
