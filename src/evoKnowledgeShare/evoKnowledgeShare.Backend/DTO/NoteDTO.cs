using evoKnowledgeShare.Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace evoKnowledgeShare.Backend.DTO
{
    public class NoteDTO
    {
        public NoteDTO(Guid noteId, Guid userId, Guid topicId, DateTimeOffset createdAt, string description, string title)
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
        public Guid UserId { get; set; }

        [Required]
        public Guid TopicId { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Title { get; set; }

    }
}
