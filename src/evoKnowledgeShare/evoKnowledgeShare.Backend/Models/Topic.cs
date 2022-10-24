namespace evoKnowledgeShare.Backend.Models {
    public class Topic {
        public int TopicID { get; set; }
        public string Title { get; set; }

        public Topic(int TopicID, string Title) {
            this.TopicID = TopicID;
            this.Title = Title;
        }
    }
}
