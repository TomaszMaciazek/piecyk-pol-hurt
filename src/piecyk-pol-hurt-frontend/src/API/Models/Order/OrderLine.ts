import { Product } from "../Product/Product";

export interface OrderLine {
  product: Product;
  itemsQuantity: number;
  priceForOneItem: number;
}
