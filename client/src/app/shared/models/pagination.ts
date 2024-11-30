export interface IPagination<T> {
  pageIndex: number;
  pageSize: number;
  totalItemsCount: number;
  data: T[];
}
