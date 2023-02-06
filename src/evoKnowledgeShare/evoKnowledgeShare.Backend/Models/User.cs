using evoKnowledgeShare.Backend.DTO;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace evoKnowledgeShare.Backend.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }

        [JsonConstructor]
        public User(Guid id, string userName, string firstName, string lastName)
        {
            Id = id;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
        }

        public User(UserDTO userDTO)
        {
            Id = Guid.NewGuid();
            UserName = userDTO.UserName;
            FirstName = userDTO.FirstName;
            LastName = userDTO.LastName;
        }

        public bool Equals(User other)
        {
            if (GetHashCode() == other.GetHashCode())
            {
                return Id == other.Id &&
                UserName.Equals(other.UserName, StringComparison.InvariantCulture) &&
                FirstName.Equals(other.FirstName, StringComparison.InvariantCulture) &&
                LastName.Equals(other.LastName, StringComparison.InvariantCulture);
            }

            return false;
        }

        public override bool Equals(object? other)
        {
            if (other is User otherUser)
            {
                return Equals(otherUser);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, UserName, FirstName, LastName);
        }

        public User() {
            Id = Guid.Empty;
            UserName = "";
            FirstName = "";
            LastName = "";
        }
    }
}
