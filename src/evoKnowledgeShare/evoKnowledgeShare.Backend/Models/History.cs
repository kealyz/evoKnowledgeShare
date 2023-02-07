using evoKnowledgeShare.Backend.DTO;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

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
        public string UserId { get; set; }

        public History(Guid id, string activity, DateTimeOffset changeDate, string version, Guid noteId, string userId)
        {
            Id = id;
            Activity = activity;
            ChangeDate = changeDate;
            Version = version;
            NoteId = noteId;
            UserId = userId;
        }

        public History (HistoryDTO historyDTO)
        {
            Id = new Guid();
            Activity = historyDTO.Activity;
            ChangeDate = historyDTO.ChangeDate;
            Version = historyDTO.Version;
            NoteId = historyDTO.NoteId;
            UserId = historyDTO.UserId;
        }

        public History() {
            Id = Guid.Empty;
            Activity = String.Empty;
            ChangeDate = DateTimeOffset.MinValue;
            Version = String.Empty;
            NoteId = Guid.Empty;
            UserId = String.Empty;
        }
    }
}