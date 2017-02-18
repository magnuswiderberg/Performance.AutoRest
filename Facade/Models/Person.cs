namespace Facade.Models
{
    public class Person
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Id { get; set; }

        internal static Person From(AutoRestClients.Api.Models.Person source)
        {
            var person = new Person
            {
                Id = source.Id,
                FirstName = source.GivenName,
                LastName = source.SurName,
                Email = source.Email
            };
            return person;
        }
    }
}