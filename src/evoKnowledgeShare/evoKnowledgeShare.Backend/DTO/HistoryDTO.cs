

namespace evoKnowledgeShare.Backend.DTO
{
    public class HistoryDTO
    {
        public string Activity { get; set; }
        public DateTimeOffset ChangeDate { get; set; }
        public string Version { get; set; }
        public Guid NoteId { get; set; }
        public string UserId { get; set; }

        public HistoryDTO(string activity, DateTimeOffset changeDate, string version, Guid noteId, string userId)
        {
            Activity = activity;
            ChangeDate = changeDate;
            Version = version;
            NoteId = noteId;
            UserId = userId;
        }

        
    }
}
