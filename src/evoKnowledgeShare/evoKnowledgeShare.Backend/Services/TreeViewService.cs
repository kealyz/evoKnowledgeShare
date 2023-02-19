using evoKnowledgeShare.Backend.DTO;
using evoKnowledgeShare.Backend.Interfaces;
using evoKnowledgeShare.Backend.Models;

namespace evoKnowledgeShare.Backend.Services
{
    public class TreeViewService
    {
        private readonly IRepository<Note> myNoteRepository;
        private readonly IRepository<Topic> myTopicRepository;
        public TreeViewService(IRepository<Topic> topicRepository, IRepository<Note> noteRepository)
        {
            myNoteRepository = noteRepository;
            myTopicRepository = topicRepository;
        }

        /// <summary>
        /// Gets every <see cref="Topic"/> entity with its corresponding <see cref="Note"/> entities
        /// </summary>
        /// <returns> <see cref="TopicsWithNotesDTO"/> collection </returns>
        public IEnumerable<TopicsWithNotesDTO> GetTreeView()
        {
            Topic[] topicEntities = myTopicRepository.GetAll().ToArray();
            TopicsWithNotesDTO[] topics = new TopicsWithNotesDTO[topicEntities.Length];
            for (int i = 0; i < topicEntities.Length; i++)
            {
                Note[] noteEntitiesByTopic = myNoteRepository.GetAll().Where(x => x.TopicId.Equals(topicEntities[i].Id)).ToArray();
                List<NoteByTopicDTO> notes = new List<NoteByTopicDTO>();
                if (noteEntitiesByTopic.Any())
                {
                    for (int j = 0; j < noteEntitiesByTopic.Length; j++)
                    {
                        notes.Add(new NoteByTopicDTO(noteEntitiesByTopic[j].NoteId, noteEntitiesByTopic[j].Title, noteEntitiesByTopic[j].CreatedAt));
                    }
                }
                topics[i] = new TopicsWithNotesDTO(topicEntities[i].Id, topicEntities[i].Title, notes);
            }
            return topics;
        }
    }
}
