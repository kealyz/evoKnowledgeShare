using System.ComponentModel.DataAnnotations;
using System.Drawing;

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
        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            User u = (User)obj;
            return Id.Equals(u.Id) && UserName.Equals(u.UserName) && FirstName.Equals(u.FirstName) && LastName.Equals(u.LastName);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, UserName, FirstName, LastName);
        }
    }
}
