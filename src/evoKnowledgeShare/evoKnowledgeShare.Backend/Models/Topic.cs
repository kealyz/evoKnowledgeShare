using System.ComponentModel.DataAnnotations;

namespace evoKnowledgeShare.Backend.Models
{
    public class Topic
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public Topic(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public bool Equals(Topic other)
        {
            if (GetHashCode() == other.GetHashCode())
            {
                return Id == other.Id &&
                       Title.Equals(other.Title, StringComparison.InvariantCulture);
            }

            return false;

        }
        public override bool Equals(object? obj)
        {
            if (obj is Topic otherTopic)
            {
                return Equals(otherTopic);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Title);
        }
    }
}