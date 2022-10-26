using System.ComponentModel.DataAnnotations;

namespace evoKnowledgeShare.Backend.Models
{
    public class History
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Activity { get; set; }
        [Required]
        public DateTimeOffset ChangeDate { get; set; }
        [Required]
        public string Version { get; set; }
        [Required]
        public Guid NoteId { get; set; }
        [Required]
        public string PKKey { get; set; }

        public History(Guid id, string activity, DateTimeOffset changeDate, string version, Guid noteId, string pkKey)
        {
            Id = id;
            Activity = activity;
            ChangeDate = changeDate;
            Version = version;
            NoteId = noteId;
            PKKey = pkKey;
        }

        public History()
        {
        }
    }
}
