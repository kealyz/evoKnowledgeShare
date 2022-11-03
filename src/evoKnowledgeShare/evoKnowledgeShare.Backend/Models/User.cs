using System.ComponentModel.DataAnnotations;

namespace evoKnowledgeShare.Backend.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }

        public User(int id, string userName, string firstName, string lastName)
        {
            Id = id;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
        }

        public bool Equals(User other)
        {
            if (GetHashCode() == other.GetHashCode())
            {
                return Id == other.Id &&
                UserName == other.UserName &&
                FirstName == other.FirstName &&
                LastName == other.LastName;
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
    }
}
