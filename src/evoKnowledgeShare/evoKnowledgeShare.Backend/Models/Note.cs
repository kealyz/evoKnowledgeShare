using evoKnowledgeShare.Backend.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace evoKnowledgeShare.Backend.Models
{
    public class Note
    {
        public Note(Guid userId, Guid topicId, DateTimeOffset createdAt, string description, string title)
        {
            NoteId = Guid.NewGuid();
            UserId = userId;
            TopicId = topicId;
            CreatedAt = createdAt;
            Description = description;
            Title = title;
        }
        public Note(NoteDTO noteDTO)
        {
            NoteId = Guid.NewGuid();
            UserId = noteDTO.UserId;
            TopicId = noteDTO.TopicId;
            CreatedAt = noteDTO.CreatedAt;
            Description = noteDTO.Description;
            Title = noteDTO.Title;
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


        public override bool Equals(object? obj)
        {
            return obj is Note note &&
                   GetHashCode() == note.GetHashCode() &&
                   NoteId.Equals(note.NoteId) &&
                   UserId == note.UserId &&
                   TopicId == note.TopicId &&
                   CreatedAt.Equals(note.CreatedAt) &&
                   Description == note.Description &&
                   Title == note.Title; 
        }
        public bool NoteFieldsMatch(object? obj)
        {
            return obj is Note note &&
                   UserId == note.UserId &&
                   TopicId == note.TopicId &&
                   CreatedAt.Equals(note.CreatedAt) &&
                   Description == note.Description &&
                   Title == note.Title;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(NoteId, UserId, TopicId, CreatedAt, Description, Title);
        }
        
        public Note() {
            NoteId = Guid.Empty;
            UserId = Guid.Empty;
            TopicId = Guid.Empty;
            CreatedAt = DateTimeOffset.MinValue;
            Description = String.Empty;
            Title = String.Empty;
        }
    }
}
