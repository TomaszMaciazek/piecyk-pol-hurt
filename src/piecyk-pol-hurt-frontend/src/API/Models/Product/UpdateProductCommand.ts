export interface UpdateProductCommand {
    id: number;
    name: string;
    code: string;
    description: string;
    price: number;
    imageUrl: string;
    isActive: boolean;
}