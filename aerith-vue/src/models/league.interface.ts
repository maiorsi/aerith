import Code from './code.interface';

export default interface League {
  id: number;
  name: string;
  code: Code;
  createdDate: Date;
}
