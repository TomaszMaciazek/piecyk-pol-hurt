import { SendPoint } from "./SendPoint/SendPoint";

export interface Product {
    id: number;
    name: string;
    description: string;
    price: number;
    imageUrl: string;
    sendPoints: SendPoint[];
}