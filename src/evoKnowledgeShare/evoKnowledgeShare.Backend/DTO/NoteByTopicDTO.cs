namespace evoKnowledgeShare.Backend.DTO
{
    
    public class NoteByTopicDTO
    {
        public Guid NoteId { get; set; }
        public string Title { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public NoteByTopicDTO(Guid noteId, string title, DateTimeOffset createdAt)
        {
            NoteId = noteId;
            Title = title;
            CreatedAt = createdAt;
        }
    }
}