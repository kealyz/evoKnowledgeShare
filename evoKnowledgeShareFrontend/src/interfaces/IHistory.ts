import IObject from "./IObject";

export default interface IHistory extends IObject
{
    activity: string,
    changeDate: string,
    version: string,
    noteId: string,
    userId: string
}