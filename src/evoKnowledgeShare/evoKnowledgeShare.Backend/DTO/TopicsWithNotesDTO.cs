using System.Text.Json.Serialization;

namespace evoKnowledgeShare.Backend.DTO
{
    public class TopicsWithNotesDTO
    {
        public Guid TopicId { get; set; }
        public string Title { get; set; }
        public IEnumerable<NoteByTopicDTO> Notes { get; set; }

        [JsonConstructor]
        public TopicsWithNotesDTO(Guid topicId, string title, IEnumerable<NoteByTopicDTO> notes)
        {
            TopicId = topicId;
            Title = title;
            Notes = notes;
        }
    }
}
