using System.Collections.Generic;

namespace backend_lab.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User(string id, string email, string password)
        {
            Id = id;
            Email = email;
            Password = password;
        }
    }
}
