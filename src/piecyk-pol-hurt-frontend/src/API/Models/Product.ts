import { SendPoint } from "./SendPoint";

export interface Product {
    id: number;
    name: string;
    description: string;
    price: number;
    imageUrl: string;
    sendPoints: SendPoint[];
}