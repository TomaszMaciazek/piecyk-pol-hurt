import { Product } from "../Product/Product";

export interface OrderLines {
    productId: number;
    product: Product;
    itemsQuantity: number;
}