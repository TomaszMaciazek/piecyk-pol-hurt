export interface CreateProductCommand {
    name: string;
    code: string;
    description: string;
    price: number;
    imageUrl: string;
    isActive: boolean;
}