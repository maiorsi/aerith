export default interface Patch {
    op: string;
    path: string;
    value?: string | string[];
}