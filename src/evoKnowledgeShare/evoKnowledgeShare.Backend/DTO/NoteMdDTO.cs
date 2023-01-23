using evoKnowledgeShare.Backend.Models;

namespace evoKnowledgeShare.Backend.DTO {
    public class NoteMdDTO {
        public NoteMdDTO(Note note, string mdRaw, int incrementSize) {
            this.Note = note;
            this.MdRaw = mdRaw;
            this.IncrementSize = incrementSize;
        }

        public Note Note { get; set; }
        public string MdRaw{ get; set; }
        public int IncrementSize { get; set; }
    }
}
