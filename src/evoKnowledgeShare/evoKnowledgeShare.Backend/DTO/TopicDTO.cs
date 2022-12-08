using System.ComponentModel.DataAnnotations;

namespace evoKnowledgeShare.Backend.DTO {
    public class TopicDTO {
        [Required]
        public string Title { get; set; }

        public TopicDTO(string title) {
            Title = title;
        }
    }
}
