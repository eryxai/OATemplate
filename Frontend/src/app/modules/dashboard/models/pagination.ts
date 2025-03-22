export class Pagination {
  pageIndex: number;
  pageSize: number;
  totalCount: number;
  getTotalCount: boolean;

  constructor(
    pageIndex: number = 0,
    pageSize: number = 10,
    totalCount: number = 0,
    getTotalCount: boolean = true
  ) {
    this.pageIndex = pageIndex;
    this.pageSize = pageSize;
    this.totalCount = totalCount;
    this.getTotalCount = getTotalCount;
  }
}
