import { FilterMetadata } from 'primeng/api';
import { Pagination } from './pagination.model';

export class BaseFilter {
  sorting: string = 'id';
  filterMetadata: { [s: string]: FilterMetadata[] };
  pagination: Pagination = new Pagination();
}
