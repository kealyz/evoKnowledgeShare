import IObject from "./IObject";

export default interface IUser extends IObject
{
    id: string,
    userName: string
    firstName: string,
    lastName: string
}