import { OrderStatus } from "../../../Constants/Enums/OrderStatus";

export interface Order {
    id: number;
    buyerId: number;
    requestedReceptionDate: Date;
    receptionDate: Date;
    status: OrderStatus;
}