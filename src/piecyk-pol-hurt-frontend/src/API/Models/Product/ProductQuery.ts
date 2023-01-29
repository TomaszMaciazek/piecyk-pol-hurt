import { SortOption } from "../../../Constants/Enums/SortOption";

export interface ProductQuery {
    code?: string;
    name?: string;
    isActive?: boolean;
    sortOption?: SortOption;
    pageSize: number;
    pageNumber: number;
}