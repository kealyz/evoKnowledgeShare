import IObject from "./IObject";

export default interface IHistory extends IObject
{
    id: string,
    activity: string
}