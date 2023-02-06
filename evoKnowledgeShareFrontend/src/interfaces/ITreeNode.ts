export default interface ITreeNode {
    id: string;
    name: string;
    isClosed: boolean;
    children?: ITreeNode[];
}