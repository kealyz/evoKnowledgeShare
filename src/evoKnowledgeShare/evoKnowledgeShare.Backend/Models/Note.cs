using System.ComponentModel.DataAnnotations;

namespace evoKnowledgeShare.Backend.Models
{
    public class Note
    {
        public Note(Guid noteId, string userId, int topicId, DateTimeOffset createdAt, string description, string title)
        {
            NoteId = noteId;
            UserId = userId;
            TopicId = topicId;
            CreatedAt = createdAt;
            Description = description;
            Title = title;
        }
        [Key]
        public Guid NoteId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int TopicId { get; set; }
        [Required]
        public DateTimeOffset CreatedAt { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Title { get; set; }

        public override bool Equals(object? obj)
        {
            obj?.GetHashCode();
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NoteId, UserId, TopicId, CreatedAt, Description, Title);
        }
    }

    

}
