// Code generated by Microsoft (R) AutoRest Code Generator 0.17.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Services.AutoRestClients.Facade.Models
{
    using System.Linq;

    public partial class Person
    {
        /// <summary>
        /// Initializes a new instance of the Person class.
        /// </summary>
        public Person() { }

        /// <summary>
        /// Initializes a new instance of the Person class.
        /// </summary>
        public Person(string email = default(string), string firstName = default(string), string lastName = default(string), string id = default(string))
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Id = id;
        }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "FirstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "LastName")]
        public string LastName { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

    }
}
