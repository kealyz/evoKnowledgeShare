import IObject from "./IObject";

export default interface IUser extends IObject
{
    userName: string
    firstName: string,
    lastName: string
}