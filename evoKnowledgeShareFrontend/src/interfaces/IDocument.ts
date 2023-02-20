import IObject from "./IObject";

export default interface IDocument extends IObject
{
    noteId: string;
    title: string,
    content: string,
    description: string,
    createdAt: string,
}