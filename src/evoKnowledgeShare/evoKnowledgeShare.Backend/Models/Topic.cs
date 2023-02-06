using evoKnowledgeShare.Backend.DTO;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace evoKnowledgeShare.Backend.Models
{
    public class Topic
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [JsonConstructor]
        public Topic(Guid id, string title)
        {
            Id = id;
            Title = title;
        }

        public Topic(TopicDTO topicDTO)
        {
            Id = Guid.NewGuid();
            Title = topicDTO.Title;
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

        public Topic() {
            Id = Guid.Empty;
            Title = "";
        }
    }
}