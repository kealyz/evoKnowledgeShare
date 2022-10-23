namespace evoKnowledgeShare.Backend.Models {
    public class Topic {
        public Guid TopicID { get; set; }
        public string Title { get; set; }

        public Topic(Guid TopicID, string Title) {
            this.TopicID = TopicID;
            this.Title = Title;
        }
    }
}
