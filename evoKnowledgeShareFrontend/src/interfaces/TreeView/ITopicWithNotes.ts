import INoteByTopicId from "./INoteByTopicId";

export default interface ITopicWithNotes{
    topicId: string,
    title: string,
    notes: INoteByTopicId[]
}