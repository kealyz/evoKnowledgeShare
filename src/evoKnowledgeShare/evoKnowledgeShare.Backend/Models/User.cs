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
    }
}
