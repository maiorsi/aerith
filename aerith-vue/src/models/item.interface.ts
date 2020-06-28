export default interface item {
  icon?: string;
  title: string;
  active?: boolean;
  route?: string;
  role?: string;
  items?: item[];
}
