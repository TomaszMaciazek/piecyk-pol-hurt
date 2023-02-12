export interface CreateOrderLineCommand {
    productId: number;
    itemsQuantity: number;
    priceForOneItem: number;
}