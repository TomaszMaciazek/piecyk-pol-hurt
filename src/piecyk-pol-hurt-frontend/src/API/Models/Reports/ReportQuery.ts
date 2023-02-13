export interface ReportQuery {
    pageSize: number;
    pageNumber: number;
    title?: string;
    group?: string;
    sortOption?: ReportSortOption;
}

export enum ReportSortOption
{
    TitleAsc = 1,
    TitleDesc = 2,
    GroupAsc = 3,
    GroupDesc = 4
}