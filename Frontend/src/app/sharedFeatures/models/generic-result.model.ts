import { Pagination } from './pagination.model';

export class GenericResultModel<T> {
  collection?: T;
  pagination?: Pagination;
  data: any;
}
