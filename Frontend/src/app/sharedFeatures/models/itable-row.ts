export interface ITableRow {
  key: string;
  level?: number;
  children?: any;
  items: Item[];
  index?: number;
  count?: number;
}

export interface Item {
  [key: string]: any;
}
