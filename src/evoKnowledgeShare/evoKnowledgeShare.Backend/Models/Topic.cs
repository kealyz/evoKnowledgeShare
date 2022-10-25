namespace evoKnowledgeShare.Backend.Models {
    public class Topic {
        public int TopicID { get; set; }
        public string Title { get; set; }

        public Topic(int TopicID, string Title) {
            this.TopicID = TopicID;
            this.Title = Title;
        }

        public override bool Equals(object? obj) {
            obj?.GetHashCode();
            return false;
        }

        public override int GetHashCode() {
            return HashCode.Combine(TopicID, Title);
        }
    }
}
