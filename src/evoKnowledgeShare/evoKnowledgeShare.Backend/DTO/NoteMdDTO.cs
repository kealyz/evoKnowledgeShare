using evoKnowledgeShare.Backend.Models;

namespace evoKnowledgeShare.Backend.DTO {
    public class NoteMdDTO {
        public Note note { get; set; }
        public string mdRaw{ get; set; }
        public int incrementSize { get; set; }
    }
}
