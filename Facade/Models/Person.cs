namespace Facade.Models
{
    public class Person
    {
        public string Email { get; set; }
        public string GivenName { get; set; }
        public string Id { get; set; }
        public string SurName { get; set; }

        internal static Person From(AutoRestClients.Api.Models.Person source)
        {
            var person = new Person
            {
                Id = source.Id,
                GivenName = source.GivenName,
                SurName = source.SurName,
                Email = source.Email
            };
            return person;
        }
    }
}