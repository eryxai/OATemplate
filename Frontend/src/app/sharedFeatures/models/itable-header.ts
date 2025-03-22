import { TableHeaderType } from '../enum/table/table-header-type';

export interface ITableHeader {
  field: string;
  name: string;
  recordKey?: boolean;
  hidden?: boolean;
  sortable?: boolean;
  removeFlitter?: boolean;
  removeSorting?: boolean;
  isPrimary?: boolean;
  isClickable?: boolean;
  canGroup?: boolean;
  cssClass?: string;
  type?: TableHeaderType;
  enum?: any;
  width?: number;
  minWidth?: string;
  extraData?: any;
  dateFormat?: string;
}
