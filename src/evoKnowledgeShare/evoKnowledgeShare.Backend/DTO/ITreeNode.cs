using System.Text.Json.Serialization;

namespace evoKnowledgeShare.Backend.DTO
{
    public class ITreeNode
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ITreeNode[] Children { get; set; }

        public ITreeNode() { }

        [JsonConstructor]
        public ITreeNode(Guid id, string name, ITreeNode[] children = null)
        {
            Id = id;
            Name = name;
            Children = children ?? new ITreeNode[0];
        }
    }
}
