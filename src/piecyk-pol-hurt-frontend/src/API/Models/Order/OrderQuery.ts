import { SortOption } from "../../../Constants/Enums/SortOption";

export interface OrderQuery {
    code?: string;
    name?: string;
    isActive?: boolean;
    sortOption?: SortOption;
    pageSize: number;
    pageNumber: number;
}