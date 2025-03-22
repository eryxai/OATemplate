import { Pagination } from './pagination.model';

export class GenericResultTotalModel<T, Statistics> {
  collection?: T;
  pagination?: Pagination;
  statisticsData: Statistics | undefined;
}
