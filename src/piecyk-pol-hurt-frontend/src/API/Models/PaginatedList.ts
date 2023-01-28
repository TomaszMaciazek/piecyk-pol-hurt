export interface PaginatedList<T> {
  items: T[];
  pageIndex: number;
  totalPages: number;
  totalCount: number;
}
