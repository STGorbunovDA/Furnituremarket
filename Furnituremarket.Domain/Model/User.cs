using Furnituremarket.Domain.Enum;

namespace Furnituremarket.Domain.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        
        public User(int id, string name, string password, int role)
        {
            Id = id;
            Name = name;
            Password = password;
            Role = (Role)role;
        }

        public User() { }
        
    }
}
