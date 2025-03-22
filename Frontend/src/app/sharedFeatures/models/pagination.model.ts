export class Pagination {
  setPagination(
    pageIndex: number,
    pageSize: number,
    totalCount: number,
    getTotalCount: boolean
  ): void {
    this.pageIndex = pageIndex;
    this.pageSize = pageSize;
    this.totalCount = totalCount;
    this.getTotalCount = getTotalCount;
  }

  setPaginationByInstance(pagination?: Pagination): Pagination {
    let result = new Pagination();
    result.pageIndex = pagination?.pageIndex;
    result.pageSize = pagination?.pageSize;
    result.totalCount = pagination?.totalCount;
    result.getTotalCount = pagination?.getTotalCount;

    return result;
  }

  static newPagination(
    pageIndex: number,
    pageSize: number,
    totalCount: number,
    getTotalCount: boolean = true
  ): Pagination {
    let result = new Pagination();

    result.pageIndex = pageIndex;
    result.pageSize = pageSize;
    result.totalCount = totalCount;
    result.getTotalCount = getTotalCount;

    return result;
  }
  static DefaultPageSize: number = 10;

  pageIndex?: number;
  pageSize?: number;
  totalCount?: number;
  getTotalCount?: boolean = true;
}
