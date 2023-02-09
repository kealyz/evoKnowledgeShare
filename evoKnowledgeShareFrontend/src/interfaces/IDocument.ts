import IObject from "./IObject";

export default interface IDocument extends IObject
{
    title: string,
    content: string,
    description: string,
    version: string
}